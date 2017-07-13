package slade.customers.infrastructure.postgresql;

import slade.customers.infrastructure.Customer;
import slade.customers.infrastructure.ICustomerRepository;

import java.sql.SQLException;
import java.util.HashMap;
import java.util.Map;

public class PostegresqlCustomerRepository implements ICustomerRepository {

    private static Map<Integer, Customer> Customers = new HashMap<>();

    public PostegresqlCustomerRepository() {

        PostgresqlDatabaseContext context = new PostgresqlDatabaseContext();

        try {
            context.connect();
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    @Override
    public Customer[] getCustomers() {
        Customer[] customers = new Customer[Customers.size()];

        for (Integer index = 0; index < Customers.size(); index++) {
            customers[index] = Customers.get(index);
        }

        return customers;
    }

    @Override
    public Customer addCustomer(Customer customer) {

        customer.id = Customers.size() + 1;
        Customers.put(customer.id, customer);

        return customer;
    }

    @Override
    public boolean updateCustomer(Customer customer) {

        if (!Customers.containsKey(customer.id)) {
            return false;
        }

        Customers.replace(customer.id, customer);

        return true;
    }

    @Override
    public boolean deleteCustomer(Integer customerId) {

        if (!Customers.containsKey(customerId)) {
            return false;
        }

        Customers.remove(customerId);

        return true;
    }
}

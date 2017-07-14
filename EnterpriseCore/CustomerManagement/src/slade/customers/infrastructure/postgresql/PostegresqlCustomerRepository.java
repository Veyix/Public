package slade.customers.infrastructure.postgresql;

import slade.customers.infrastructure.Customer;
import slade.customers.infrastructure.ICustomerRepository;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class PostegresqlCustomerRepository implements ICustomerRepository, AutoCloseable {

    private static Map<Integer, Customer> Customers = new HashMap<>();
    private final PostgresqlDatabaseContext context;

    public PostegresqlCustomerRepository() {

        this.context = new PostgresqlDatabaseContext();

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
        final ArrayList<Customer> customerList;

        try {
            customerList = this.context.getCustomers();
        } catch (SQLException e) {
            e.printStackTrace();

            return new Customer[0];
        }
        
        Customer[] customers = new Customer[customerList.size()];

        for (Integer index = 0; index < customerList.size(); index++) {
            customers[index] = customerList.get(index);
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

    @Override
    public void close() throws Exception {
        this.context.disconnect();
    }
}

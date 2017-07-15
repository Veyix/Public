package slade.customers.infrastructure.postgresql;

import slade.customers.infrastructure.Customer;
import slade.customers.infrastructure.ICustomerRepository;

import java.sql.SQLException;
import java.util.ArrayList;

public class PostegresqlCustomerRepository implements ICustomerRepository, AutoCloseable {

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
    public Customer[] getCustomers() throws SQLException {
        final ArrayList<Customer> customerList  = this.context.getCustomers();

        Customer[] customers = new Customer[customerList.size()];

        for (Integer index = 0; index < customerList.size(); index++) {
            customers[index] = customerList.get(index);
        }

        return customers;
    }

    @Override
    public Customer addCustomer(Customer customer) throws SQLException {
        return this.context.addCustomer(customer);
    }

    @Override
    public void updateCustomer(Customer customer) throws SQLException {
        this.context.updateCustomer(customer);
    }

    @Override
    public boolean deleteCustomer(Integer customerId) {

        // TODO

        return true;
    }

    @Override
    public void close() throws Exception {
        this.context.disconnect();
    }
}

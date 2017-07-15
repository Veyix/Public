package slade.customers.infrastructure;

import java.sql.SQLException;

public interface ICustomerRepository extends AutoCloseable {

    Customer[] getCustomers() throws SQLException;
    Customer addCustomer(Customer customer) throws SQLException;
    void updateCustomer(Customer customer) throws SQLException;
    boolean deleteCustomer(Integer customerId);
}

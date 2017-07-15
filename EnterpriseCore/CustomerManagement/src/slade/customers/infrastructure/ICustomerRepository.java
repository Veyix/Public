package slade.customers.infrastructure;

import java.sql.SQLException;

public interface ICustomerRepository extends AutoCloseable {

    Customer[] getCustomers() throws SQLException;
    Customer addCustomer(Customer customer) throws SQLException;
    boolean updateCustomer(Customer customer);
    boolean deleteCustomer(Integer customerId);
}

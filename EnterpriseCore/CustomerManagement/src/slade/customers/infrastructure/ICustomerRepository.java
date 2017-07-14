package slade.customers.infrastructure;

public interface ICustomerRepository extends AutoCloseable {

    Customer[] getCustomers();
    Customer addCustomer(Customer customer);
    boolean updateCustomer(Customer customer);
    boolean deleteCustomer(Integer customerId);
}

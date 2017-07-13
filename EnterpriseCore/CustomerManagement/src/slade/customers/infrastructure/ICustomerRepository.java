package slade.customers.infrastructure;

public interface ICustomerRepository {

    Customer[] getCustomers();
    Customer addCustomer(Customer customer);
    boolean updateCustomer(Customer customer);
    boolean deleteCustomer(Integer customerId);
}

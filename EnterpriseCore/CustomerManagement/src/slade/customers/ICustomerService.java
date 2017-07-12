package slade.customers;

import javax.jws.WebService;
import javax.ws.rs.core.Response;

@WebService()
public interface ICustomerService {

    Response getCustomers();
    Response createCustomer(Customer customer);
    Response updateCustomer(Customer customer);
    Response deleteCustomer(Integer customerId) ;
}

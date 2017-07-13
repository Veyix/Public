package slade.customers.infrastructure.postgresql;

import slade.customers.infrastructure.Customer;
import slade.customers.infrastructure.ICustomerRepository;

import java.util.HashMap;
import java.util.Map;

public class PostegresqlCustomerRepository implements ICustomerRepository {

    private static Map<Integer, Customer> Customers = new HashMap<>();

    @Override
    public Customer[] getCustomers() {
        Customer[] customers = new Customer[Customers.size()];

        for (Integer index = 0; index < Customers.size(); index++) {
            customers[index] = Customers.get(index);
        }

        return customers;
    }
}

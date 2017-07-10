package slade.customers;

import com.google.gson.Gson;
import java.util.ArrayList;
import javax.ws.rs.Path;
import javax.ws.rs.GET;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

/**
 * Created by samue on 10/07/2017.
 */
@Path("/customers")
public class CustomerService {

    private static ArrayList<Customer> Customers = new ArrayList<>();

    public CustomerService() {

        if (Customers.isEmpty()) {
            setupCustomer("Mr", "Samuel", "Slade");
        }
    }

    private static void setupCustomer(String title, String forename, String surname) {
        final Customer customer = new Customer();
        customer.title = title;
        customer.forename = forename;
        customer.surname = surname;

        Customers.add(customer);
    }

    @GET
    public Response getCustomers() {
        return Ok(Customers);
    }

    private static <T> Response Ok(T result) {

        Gson gson = new Gson();
        String json = gson.toJson(result);

        return Response.status(Response.Status.OK)
                .type(MediaType.APPLICATION_JSON)
                .entity(json)
                .build();
    }
}

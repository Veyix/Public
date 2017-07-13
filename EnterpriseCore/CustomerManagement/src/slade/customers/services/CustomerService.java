package slade.customers.services;

import com.google.gson.Gson;
import slade.customers.PATCH;
import slade.customers.infrastructure.Customer;

import java.util.HashMap;
import java.util.Map;
import javax.jws.WebService;
import javax.ws.rs.*;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Response;

@WebService()
@Path("/customers")
public class CustomerService implements ICustomerService {

    private static Map<Integer, Customer> Customers = new HashMap<>();

    @GET
    public Response getCustomers() {
        return ok(Customers.values());
    }

    @PUT
    @Consumes(MediaType.APPLICATION_JSON)
    public Response createCustomer(Customer customer) {

        if (customer == null) {
            return badRequest();
        }

        customer.id = Customers.size() + 1;
        Customers.put(customer.id, customer);

        return created(customer);
    }

    @PATCH
    @Consumes(MediaType.APPLICATION_JSON)
    public Response updateCustomer(Customer customer) {
        if (customer == null) {
            return badRequest();
        }

        if (!Customers.containsKey(customer.id)) {
            return notFound();
        }

        Customers.replace(customer.id, customer);

        return noContent();
    }

    @DELETE
    @Path("/{customerId}")
    public Response deleteCustomer(@PathParam("customerId") Integer customerId) {

        if (!Customers.containsKey(customerId)) {
            return notFound();
        }

        Customers.remove(customerId);

        return noContent();
    }

    private static <T> Response ok(T result) {
        return createResponse(result, Response.Status.OK);
    }

    private static <T> Response created(T result) {
        return createResponse(result, Response.Status.CREATED);
    }

    private static Response notFound() {
        return createResponse(Response.Status.NOT_FOUND);
    }

    private static Response badRequest() {
        return createResponse(Response.Status.BAD_REQUEST);
    }

    private static Response noContent() {
        return createResponse(Response.Status.NO_CONTENT);
    }

    private static Response createResponse(Response.Status status) {

        return Response.status(status)
                .type(MediaType.APPLICATION_JSON)
                .build();
    }

    private static <T> Response createResponse(T value, Response.Status status) {

        Gson gson = new Gson();
        String json = gson.toJson(value);

        return Response.status(status)
                .type(MediaType.APPLICATION_JSON)
                .entity(json)
                .build();
    }
}

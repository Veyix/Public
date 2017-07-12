package slade.customers;

import javax.ws.rs.ApplicationPath;
import javax.ws.rs.core.Application;
import java.util.HashSet;

/**
 * Created by samue on 10/07/2017.
 */
@ApplicationPath("/")
public class CustomerManagementApplication extends Application {

    @Override
    public HashSet<Class<?>> getClasses() {
        HashSet classes = new HashSet<Class<?>>();
        classes.add(CustomerService.class);

        return classes;
    }
}

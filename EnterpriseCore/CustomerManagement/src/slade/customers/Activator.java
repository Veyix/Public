package slade.customers;

import org.osgi.framework.BundleActivator;
import org.osgi.framework.BundleContext;
import org.osgi.framework.ServiceRegistration;

public class Activator implements BundleActivator {

    private ServiceRegistration registration;

    @Override
    public void start(BundleContext context) throws Exception {

            if (registration != null) {
                System.out.println("The service has already been registered!");

                return;
            }

            System.out.println("Registering the service...");

            try {
                registration = context.registerService(
                        ICustomerService.class.getName(),
                        new CustomerService(),
                        null
                );

                System.out.println("Service registered successfully!");
            }
            catch (Exception exception) {
                System.err.println(exception.getMessage());
            }
    }

    @Override
    public void stop(BundleContext context) throws Exception {

        if (registration == null) {
            System.out.println("The service has not been registered!");

            return;
        }

        System.out.println("Un-registering the service...");

        try {
            registration.unregister();

            System.out.println("Service unregistered successfully!");
        }
        catch (Exception exception) {
            System.err.println(exception.getMessage());
        }
    }
}

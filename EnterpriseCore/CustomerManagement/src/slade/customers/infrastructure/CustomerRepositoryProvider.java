package slade.customers.infrastructure;

import slade.customers.infrastructure.postgresql.PostegresqlCustomerRepository;

public final class CustomerRepositoryProvider {

    public static ICustomerRepository create() {

        // Currently we only support PostgreSQL, but this may change
        // using OSGi modules and dynamically loading either a PostgreSQL
        // or Cassandra implementation
        return new PostegresqlCustomerRepository();
    }
}

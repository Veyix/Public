package slade.customers.infrastructure.postgresql;

import slade.customers.infrastructure.Customer;

import java.sql.*;
import java.util.ArrayList;
import java.util.Properties;

public class PostgresqlDatabaseContext {

    private Connection connection;

    public void connect() throws SQLException, ClassNotFoundException {
        Class.forName("org.postgresql.Driver");

        String databaseConnectionString = "jdbc:postgresql://localhost/";

        Properties properties = new Properties();
        properties.setProperty("user", "postgres");

        this.connection = DriverManager.getConnection(databaseConnectionString, properties);

        if (!databaseExists()) {
            createDatabase();
        }
    }

    public void disconnect() throws SQLException {

        if (this.connection != null) {
            this.connection.close();
            this.connection = null;
        }
    }

    private boolean databaseExists() throws SQLException {

        Statement statement = this.connection.createStatement();
        ResultSet resultSet = statement.executeQuery("SELECT TOP 1 1 FROM INFORMATION_SCHEMA.TABLES WHERE [TABLE_NAME] = 'test';");

        return resultSet.getBoolean(0);
    }

    private void createDatabase() throws SQLException {

        Statement statement = this.connection.createStatement();
        statement.execute("CREATE DATABASE [test];");
    }

    // TODO: Make this method operate on generics.
    public ArrayList<Customer> getCustomers() throws SQLException {

        Statement statement = this.connection.createStatement();
        ResultSet resultSet = statement.executeQuery("SELECT [Id], [Title], [Forename], [Surname] FROM [Customer]");

        ArrayList<Customer> customers = new ArrayList<>();

        do {
            Customer customer = createCustomer(resultSet);
            customers.add(customer);
        }
        while (!resultSet.isLast() && resultSet.next());

        return customers;
    }

    private static Customer createCustomer(ResultSet resultSet) throws SQLException {
        final Customer customer = new Customer();

        customer.id = resultSet.getInt("Id");
        customer.title = resultSet.getString("Title");
        customer.forename = resultSet.getString("Forename");
        customer.surname = resultSet.getString("Surname");

        return customer;
    }
}

package slade.customers.infrastructure.postgresql;

import slade.customers.infrastructure.Customer;

import java.sql.*;
import java.util.ArrayList;
import java.util.Properties;

public class PostgresqlDatabaseContext {

    private Connection connection;

    public void connect() throws SQLException, ClassNotFoundException {

        boolean initializing = false;
        if (!databaseExists()) {
            initializing = true;

            createDatabase();
        }

        this.connection = createConnection("test");

        if (initializing) {
            createCustomerTable();
        }
    }

    private static Connection createConnection(String databaseName) throws SQLException, ClassNotFoundException {
        Class.forName("org.postgresql.Driver");

        String databaseConnectionString = "jdbc:postgresql://localhost/";
        if (databaseName != null && !databaseName.isEmpty()) {
            databaseConnectionString += databaseName;
        }

        Properties properties = new Properties();
        properties.setProperty("user", "postgres");

        return DriverManager.getConnection(databaseConnectionString, properties);
    }

    public void disconnect() throws SQLException {

        if (this.connection != null) {
            this.connection.close();
            this.connection = null;
        }
    }

    private boolean databaseExists() throws SQLException, ClassNotFoundException {

        try (Connection connection = createConnection(null)) {

            Statement statement = connection.createStatement();
            ResultSet resultSet = statement.executeQuery("SELECT 1 FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'test' LIMIT 1;");

            return resultSet.getRow() >= 0;
        }
    }

    private void createDatabase() throws SQLException, ClassNotFoundException {

        try (Connection connection = createConnection(null)) {
            Statement statement = this.connection.createStatement();
            statement.execute("CREATE DATABASE test;");
        }
    }

    private void createCustomerTable() throws SQLException {

        Statement statement = this.connection.createStatement();

        final String sql = "CREATE TABLE Customer ("
                + "Id SERIAL NOT NULL PRIMARY KEY,"
                + "Title VARCHAR(50) NOT NULL,"
                + "Forename VARCHAR(256) NOT NULL,"
                + "Surname VARCHAR(256) NOT NULL"
                + ");";

        statement.execute(sql);
    }

    // TODO: Make this method operate on generics.
    public ArrayList<Customer> getCustomers() throws SQLException {

        Statement statement = this.connection.createStatement();
        ResultSet resultSet = statement.executeQuery("SELECT Id, Title, Forename, Surname FROM Customer;");

        ArrayList<Customer> customers = new ArrayList<>();

        while (resultSet.next()) {
            Customer customer = createCustomer(resultSet);
            customers.add(customer);
        }

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

    public Customer addCustomer(Customer customer) throws SQLException {

        String sql = "INSERT INTO Customer (Title, Forename, Surname)"
                + "VALUES ('" + customer.title + "', '" + customer.forename + "', '" + customer.surname + "');";

        try (PreparedStatement statement = this.connection.prepareStatement(sql, Statement.RETURN_GENERATED_KEYS)) {
            int rowsAffected = statement.executeUpdate();

            if (rowsAffected == 0) {
                throw new SQLException("No records were inserted");
            }

            try (ResultSet resultSet = statement.getGeneratedKeys()) {
                if (resultSet.next()) {
                    customer.id = resultSet.getInt(1);
                }
                else {
                    throw new SQLException("Failed to retrieve the new Id");
                }
            }
        }

        return customer;
    }

    public void updateCustomer(Customer customer) throws SQLException {

        String sql = "UPDATE Customer "
                + "SET Title = '" + customer.title + "', "
                + "Forename = '" + customer.forename + "', "
                + "Surname = '" + customer.surname + "' "
                + "WHERE Id = " + customer.id.toString() + ";";

        try (PreparedStatement statement = this.connection.prepareStatement(sql)) {
            int rowsAffected = statement.executeUpdate();

            if (rowsAffected == 0) {
                throw new SQLException("No records updated");
            }
        }
    }
}

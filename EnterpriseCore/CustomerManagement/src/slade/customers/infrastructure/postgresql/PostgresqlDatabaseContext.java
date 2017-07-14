package slade.customers.infrastructure.postgresql;

import java.sql.*;
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
}

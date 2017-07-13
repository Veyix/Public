package slade.customers.infrastructure.postgresql;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.Properties;

public class PostgresqlDatabaseContext {

    public void connect() throws SQLException, ClassNotFoundException {
        Class.forName("org.postgresql.Driver");

        String databaseConnectionString = "jdbc:postgresql://localhost/";

        Properties properties = new Properties();
        properties.setProperty("user", "postgres");

        Connection connection = DriverManager.getConnection(databaseConnectionString, properties);

        Statement statement = connection.createStatement();
        statement.execute("CREATE DATABASE test;");
    }
}

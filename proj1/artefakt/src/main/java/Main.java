import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.Statement;

public class Main {

    public static void main(String[] args){

        final String DB_URL = "jdbc:hsqldb:hsql://localhost/workdb";

        Connection connection = DriverManager.getConnection(DB_URL);

        Statement statement = connection.get


    }

}

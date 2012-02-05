import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.*;


public class DatabaseTester {
	private static final String accessDBURLPrefix = "jdbc:odbc:Driver={Microsoft Access Driver (*.mdb)};DBQ=";
	private static final String accessDBURLSuffix = ";DriverID=22;READONLY=false}";
	int count = 0;
	static Connection conn;
	public static void main(String[] args){
		writeToDB();
	}
	public static void writeToDB(){


		try {
			Class.forName("sun.jdbc.odbc.JdbcOdbcDriver").newInstance();
			conn = getAccessDBConnection("John.mdb");
			PreparedStatement stmt = conn.prepareStatement("INSERT INTO Moves(MoveDate,Count,Keyword,Source,TitleName,TitleLink,Snippet) VALUES(?,?,?,?,?,?,?)");
			stmt.setString(1, "03/23/2009");
			stmt.setString(2, String.valueOf(3));
			stmt.setString(3, "Gello");
			stmt.setString(4, "Gello");
			stmt.setString(5, "Gello");
			stmt.setString(6, "Gello");
			stmt.setString(7,"Gello");
			stmt.execute();
			conn.commit();
		} catch(ClassNotFoundException e) {
			System.err.println("JdbcOdbc Bridge Driver not found!");
		} catch (InstantiationException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		
	}


	/** Creates a Connection to a Access Database */
	public static Connection getAccessDBConnection(String filename) throws SQLException {
		filename = filename.replace(' ', '/').trim();
		String databaseURL = accessDBURLPrefix + filename + accessDBURLSuffix;
		return DriverManager.getConnection(databaseURL, "", "");
	}  
}

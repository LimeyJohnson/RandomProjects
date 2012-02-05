
import java.sql.*;


public class Data {
	private static final String accessDBURLPrefix = "jdbc:odbc:Driver={Microsoft Access Driver (*.mdb)};DBQ=";
	private static final String accessDBURLSuffix = ";DriverID=22;READONLY=false}";
	private int count = 0;
	Connection conn;
	public Data(){
		try {
			Class.forName("sun.jdbc.odbc.JdbcOdbcDriver").newInstance();
			conn = getAccessDBConnection("mili2.mdb");
		} catch (InstantiationException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (ClassNotFoundException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}
	public synchronized void addCount(){
		count++;
	}
	public synchronized int getCount(){
		return count;
	}
	public synchronized int writeToMovesDB(int index, String MoveDate,  int countIn, String Keywords, String Source, String TitleName, String TitleLink, String Snippet){

		//System.out.println("INSERT INTO Moves(MoveID, MoveDate,Count,Keyword,Source,TitleName,TitleLink,Snippet) VALUES("+index+","+MoveDate+","+countIn+","+Keywords+","+Source+","+TitleName+","+TitleLink+","+Snippet);
		try {
			
			PreparedStatement stmt = conn.prepareStatement("INSERT INTO Moves(MoveID, MoveDate,Count,Keyword,Source,TitleName,TitleLink,Snippet) VALUES(?,?,?,?,?,?,?,?)");
			stmt.setInt(1, index);
			stmt.setString(2, MoveDate);
			stmt.setInt(3, countIn);
			stmt.setString(4, Keywords);
			stmt.setString(5, Source);
			stmt.setString(6, TitleName);
			stmt.setString(7, TitleLink);
			stmt.setString(8, Snippet);
			
			stmt.execute();
			conn.commit();
			
			stmt = conn.prepareStatement("SELECT MAX(ID) as max  from Moves");
			 ResultSet rs = stmt.executeQuery();
			 rs.next();
			 return rs.getInt("max");
			

		} catch (SQLException e) {
			
			e.printStackTrace();
		}
	
		return 0;
		
	}
	public void writeToRelatedDB(String MoveDate, int index, String Source, String TitleName, String TitleLink, String Snippet){


		try {
			
			PreparedStatement stmt = conn.prepareStatement("INSERT INTO RelatedMoves(Move, MoveDate,Source,TitleName,TitleLink,Snippet) VALUES(?,?,?,?,?,?)");
			stmt.setInt(1, index);
			stmt.setString(2, MoveDate);
			
			stmt.setString(3, Source);
			stmt.setString(4, TitleName);
			stmt.setString(5, TitleLink);
			stmt.setString(6, Snippet);
			stmt.execute();
			conn.commit();
		
		
		} catch (SQLException e) {
			
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

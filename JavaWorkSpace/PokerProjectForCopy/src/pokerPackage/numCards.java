package pokerPackage;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.*;
public class numCards {

	public static void main(String[] args){
		try {
			// The newInstance() call is a work around for some
			// broken Java implementations
			Class.forName("com.mysql.jdbc.Driver").newInstance();
		} catch (Exception ex) {
			// handle the error
		}
		System.out.println("Created driver");
		Scanner scanner = new Scanner(System.in);
		System.out.println("Password: ");
		String password = scanner.next();
		try {
			Connection conn = 
		DriverManager.getConnection("jdbc:mysql://database2.cs.tamu.edu/limey?user=limey&password="+password);
		System.out.println("Created connection");
			for(int i = 0; i<=51;i++){
				for(int k = i+1; k<=51; k++){
					Statement stmt = conn.createStatement();
					//k=ID2
					//i=ID1
					String suit1 = getSuit(i);
					String num1 = getCard(i);
					String suit2 = getSuit(k);
					String num2 = getCard(k);
					stmt.execute("insert into poker values('"+suit1+"','"+num1+"',"+i+",'"+suit2+"','"+num2+"',"+k+")");
					System.out.println("insert into poker values('"+suit1+"',"+num1+","+i+",'"+suit2+"',"+num2+","+k+")");
					System.out.println(i+","+num1+","+suit1+" .... "+k+","+num2+","+suit2+" inserted");
				}
			}
		} catch (SQLException ex) {
			// handle any errors
			System.out.println("SQLException: " + ex.getMessage());
			System.out.println("SQLState: " + ex.getSQLState());
			System.out.println("VendorError: " + ex.getErrorCode());
		}
	}
	//Based on A Spades is 0
	public static String getCard(int input){
		String card;
		switch(input/4){
		case 0: card = "A";
		break;
		case 1: card = "K";
		break;
		case 2: card = "Q";
		break;
		case 3: card = "J";
		break;
		case 4: card = "10";
		break;
		case 5: card = "9";
		break;
		case 6: card = "8";
		break;
		case 7: card = "7";
		break;
		case 8: card = "6";
		break;
		case 9: card = "5";
		break;
		case 10: card = "4";
		break;
		case 11: card = "3";
		break;
		case 12: card = "2";
		break;
		default: card = "unknown";
		break;
		}
		return card;
	}
	public static String getSuit(int input){
		String suit;
		switch(input%4){
		case 0: suit="Spades";
		break;
		case 1: suit = "Hearts";
		break;
		case 2: suit = "Diamonds";
		break;
		case 3: suit = "Clubs";
		break;

		default: suit = "unknown";
		break;
		}
		return suit;
	}
}

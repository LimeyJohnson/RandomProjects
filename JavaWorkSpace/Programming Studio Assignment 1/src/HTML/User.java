/*
 * A User represents one person in the input file. 
 */
package HTML;
import java.util.*;
public class User {
	String Name;
	Date DateJoined;
	public User(String name){
		Name = name; 
	}
	public String getName(){
		return Name;
	}
	public void setDateJoined(Date date){
		DateJoined = date;
	}
	public String toString(){
		return Name;
	}
}

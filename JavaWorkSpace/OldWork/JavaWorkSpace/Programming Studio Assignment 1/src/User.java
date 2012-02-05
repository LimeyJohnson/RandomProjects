import java.util.*;
public class User {
	String Name, Date_Joined;
	Vector<Game> Games = new Vector<Game>();
	public Boolean joined = false;
	public User(String name, String date){
		Name = name; Date_Joined  = date;
	}
	public String getName(){
		return Name;
	}
	public void joined(){
		joined = true;
	}
	public void addGame(Game g){
		Games.add(g);
	}
}

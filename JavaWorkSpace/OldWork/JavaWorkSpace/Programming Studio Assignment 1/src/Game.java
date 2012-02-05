import java.util.*;
public class Game {
	String Name;
	int Points;
	Vector<User> Users = new Vector<User>(); 
	public Game(String name){

		Name = name;
	}
	public String getName(){
		return Name;
	}
	public void setPoints(int points){
		Points = points;
	}
	public void addUser(User u){
		Users.add(u);
	}
}

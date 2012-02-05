package GUI;
/*
 * A game represents a particular Game in the system. A Game has a Name whereas a Play does not have a name
 * Risk would be an example of a Game, A person playing risk is an example of a Play
 */
public class Game {
	String Name;
	int Points;
	
	public Game(String name){

		Name = name;
	}
	public String getName(){
		return Name;
	}
	public void setPoints(int points){
		Points = points;
	}
	public String toString(){
		return Name;
	}
}

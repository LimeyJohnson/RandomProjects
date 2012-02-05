package GUI;
import java.io.*;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;

public class DataManager {
	//vGames, vUsers, vPlays are the main data structurs used in the program
	Vector<Game> vGames = new Vector<Game>();
	Vector<User> vUsers = new Vector<User>();
	Vector<Play> vPlays = new Vector<Play>();
	
	//DateFormat to convert to and from our given Date format
	DateFormat DF = new SimpleDateFormat("MM/dd/yyyy");
	
	//Variables to tell the GUI what to give the user
	String completeMessage = "";
	Boolean complete;
	
	
	public DataManager(String fileName){//process's the given file based on the specs of the project
		try {
			
			File file = new File(fileName);
			Scanner scanner = new Scanner(file);
			String line;
			//Process each line in the file and split it into tokens
			while((line = scanner.nextLine())!=null){
				String[] tokens = line.split(" ");

				//Does the line start with a number
				if(line.charAt(0)>=48&&line.charAt(0)<=57){
					if(tokens[1].equals("END"))break;
					String name = tokens[1];
					
					//Add the points to the game given a name
					for(int i = 2; i<tokens.length;i++)name+=" "+tokens[i];
					findGame(name).setPoints(Integer.parseInt(tokens[0]));
				}
				
				else{//line does not start with a number
					User user = findUser(tokens[0]);
					if(tokens[2].equals("JOIN")){//Line is Join Statment
						user.setDateJoined(DF.parse(tokens[1]));
					}
					else{//Line is Game Statement

						String gamename = tokens[2];
						for(int i = 3; i<tokens.length;i++)gamename+=" "+tokens[i];

						Game game = findGame(gamename);
						Play play = new Play(user,game,DF.parse(tokens[1]));
						vPlays.add(play);
					}
				}
			}
			//Tell the GUI what to display
			completeMessage = "Data Import Succeded";
			complete = true;
		} catch (FileNotFoundException e) {//Data file error, tell GUI to display error
			completeMessage = "Import Failed";
			complete = false;
			e.printStackTrace();
		} catch (ParseException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		for(Play p: vPlays){//check if players JOINED before playing game
			if(p.user.DateJoined.after(p.date)){
				System.err.println(p.user.Name+" played "+p.game+" before joining");
			}

		}
	}
	Game findGame(String s ){//Check if the game exists with a certain name. Returns a new game if no game exists
		for(Game u: vGames){
			if(u.getName().equals(s)){
				return u;
			}
		}

		Game game = new Game(s);
		vGames.add(game);
		return game;
	}
	User findUser(String name){//Check if User Exist given a name. Returns a new User if the user does not exist
		for(User u: vUsers){
			if(u.getName().equals(name)){
				return u;
			}
		}
		User user = new User(name);
		vUsers.add(user);
		return user;
	}
	public Vector<User> getUsers(){
		return vUsers;
	}
	public Vector<Game> getGames(){
		return vGames;
	}
	public String smallReport(){//List of Users and Games
		String output = "Users:\n";
		for(User u : vUsers){
			output += u.Name+"\n"; 
		}
		output += "\nGames:\n";
		for(Game g: vGames){
			output+= g.Name+"\n";

		}
		return output;
	}
	public String fullReport(){//Stats report of number of users and number of games
		String output = "Users: "+vUsers.size()+"\n";
		output+= "Games: "+vGames.size()+"\n";
		output+= "Total Games Played: "+vPlays.size();

		return output;

	}
	public String userReport(User u){//report on a single user
		int points =0;
		String output = u.Name+"\n";
		output += "Date Joined: "+DF.format(u.DateJoined)+"\n";
		output+= "Games Played:\n";
		for(Play p: vPlays){
			if(p.user==u){ 
				output+= p.game.Name +"\n";
				points+=p.game.Points;
			}			
		}
		output+="Points Earned: "+points;
		return output;
	}
	public String gameReport(Game g){//report on a single game
		Vector<User> userTemp = new Vector<User>();
		String output = g.Name+"\n"; 
		output += "Players who have played the game:\n";
		for(Play p: vPlays){
			if(p.game==g&&!userTemp.contains(p.user))userTemp.add(p.user);
		}
		for(User u: userTemp)output+=u.Name+"\n";
		return output;
	}
}

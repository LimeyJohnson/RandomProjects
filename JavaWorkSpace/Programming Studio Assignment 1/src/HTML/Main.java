package HTML;
import java.io.*;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.*;

public class Main {
	//vGames, vUsers, vPlays are the main data structurs used in the program
	static Vector<Game> Vgames = new Vector<Game>();
	static Vector<User> Vusers = new Vector<User>();
	static Vector<Play> Vplays = new Vector<Play>();
	//DateFormat to convert to and from our given Date format
	static DateFormat DateFormatter = new SimpleDateFormat("MM/dd/yyyy");
	public static void main(String[] args){//process's the given file based on the specs of the project
		try {
			Scanner consoleScanner = new Scanner(System.in);
			System.out.printf("What file would you like: ");
			File file = new File(consoleScanner.next());
			Scanner scanner = new Scanner(file);
			String line;
			//Process each line in the file and split it into tokens
			while((line = scanner.nextLine())!=null){
				String[] tokens = line.split(" ");
			
				if(line.charAt(0)>=48&&line.charAt(0)<=57){//line is a game spec or end of file
					if(tokens[1].equals("END"))break;//end of the input file

					String name = tokens[1];
					for(int i = 2; i<tokens.length;i++)name+=" "+tokens[i];
					findGame(name).setPoints(Integer.parseInt(tokens[0]));
				}
				else{//line is user spec (joined or played a game)

					User user = findUser(tokens[0]);
					if(tokens[2].equals("JOIN")){//Join statment
						user.setDateJoined(DateFormatter.parse(tokens[1]));
					}
					else{//user played a game

						String gamename = tokens[2];
						for(int i = 3; i<tokens.length;i++)gamename+=" "+tokens[i];

						Game game = findGame(gamename);
						Play play = new Play(user,game,DateFormatter.parse(tokens[1]));
						Vplays.add(play);
					}
				}
			}
			
		} catch (FileNotFoundException e) {//file input error
			
			e.printStackTrace();
		} catch (ParseException e) {//Date Parse error
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		for(Play p: Vplays){//check all users Joined before they played a game
			if(p.user.DateJoined.after(p.date)){
				System.err.println(p.user.Name+" played "+p.game+" before joining");
			}

		}
		HtmlWriter HW = new HtmlWriter();//write HTML files
		HW.writeFiles(Vusers, Vgames, Vplays);
	}
	static Game findGame(String s ){//Check if the game exists with a certain name. Returns a new game if no game exists
		for(Game u: Vgames){
			if(u.getName().equals(s)){
				return u;
			}
		}

		Game game = new Game(s);
		Vgames.add(game);
		return game;
	}
	static User findUser(String name){//Check if the user exists with a certain name. Returns a new user if no game exists
		for(User u: Vusers){
			if(u.getName().equals(name)){
				return u;
			}
		}
		User user = new User(name);
		Vusers.add(user);
		return user;
	}
	
}

import java.io.*;
import java.util.*;
public class Main {
	static Vector<Game> Games = new Vector<Game>();
	static Vector<User> Users = new Vector<User>();
	public static void main(String[] args) {
		try {
			File file = new File("scores.txt");
			Scanner scanner = new Scanner(file);
			String line;
			while((line = scanner.nextLine())!=null){
				String[] tokens = line.split(" ");
				if(line.charAt(0)>=48&&line.charAt(0)<=57){
					if(tokens[1].equals("END"))break;

					String name = tokens[1];
					for(int i = 2; i<tokens.length;i++)name+=" "+tokens[i];
					findGame(name).setPoints(Integer.parseInt(tokens[0]));
				}
				else{

					User user = findUser(tokens[0],tokens[1]);
					if(tokens[2].equals("JOIN"))user.joined();
					else{
						String gamename = tokens[2];
						for(int i = 3; i<tokens.length;i++)gamename+=" "+tokens[i];
						Game game = findGame(gamename);
						game.addUser(user);
						user.addGame(game);

					}
				}
			}
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		}
		for(User u: Users){
			System.out.println(u.getName());

		}
		for(Game g: Games){
			System.out.println(g.getName());
		}

	}
	//Check if the game exists with a certain name. Returns a new game if no game exists
	static Game findGame(String s ){
		for(Game u: Games){
			if(u.getName().equals(s)){
				return u;
			}
		}

		Game game = new Game(s);
		Games.add(game);
		return game;
	}
	static User findUser(String name, String date){
		for(User u: Users){
			if(u.getName().equals(name)){
				return u;
			}
		}
		User user = new User(name,date);
		Users.add(user);
		return user;
	}
}

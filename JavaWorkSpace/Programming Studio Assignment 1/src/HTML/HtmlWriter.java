package HTML;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.*;
import java.io.*;
public class HtmlWriter {//Class to write the HTML output file for the given scoresheet
	public HtmlWriter(){

	}
	public void writeFiles(Vector<User> users, Vector<Game> games, Vector<Play> plays){//import all data necessary to produce report
		try {
			int fileName=0;//Filename int so game and user can have the same name
			DateFormat DF = new SimpleDateFormat("MM/dd/yyyy");
			FileOutputStream fileStream = new FileOutputStream("index.html");
			PrintStream mainStream = new PrintStream(fileStream);
			mainStream.println("<html><body><h1>Results</h1><br/><b>Users</b></br>"); // write index.html file
			for(User u: users){ // for each user write Userfile and add name to index file
				if(u.DateJoined!=null){
					mainStream.println("<a href=\""+fileName+".html\">"+u.Name+"</a><br/>");
					FileOutputStream userFileStream = new FileOutputStream((fileName++)+".html");
					PrintStream userStream = new PrintStream(userFileStream);
					userStream.println("<html><body><h1>"+u.Name+"</h1><br/>");
					userStream.println("User Joined: "+DF.format(u.DateJoined)+"<br/>");
					userStream.println("Game(s) Completed:</br> ");
					int totalPoints = 0;
					for(Play p:plays){//for each game the user has played write line to profile file
						if(p.user==u){
							userStream.println(p.game.Name+"</br>");
							totalPoints += p.game.Points;
						}
					}
					userStream.println("For a total of "+totalPoints+" points");
					userStream.println("</body></html>");
				}
			}
			mainStream.println("<br/><b>Games</b></br>");
			for(Game g: games){ // for each game add game name to index file and create game file
				mainStream.println("<a href=\""+fileName+".html\">"+g.Name+"</a><br/>");
				FileOutputStream gameFileStream = new FileOutputStream((fileName++)+".html");
				PrintStream gameStream = new PrintStream(gameFileStream);
				gameStream.println("<html><body><h1>"+g.Name+"</h1><br/>");
				gameStream.println("Points earned for completion: "+g.Points+"<br/>");
				gameStream.println("User(s) completed this game:<br>");
				Vector<User> userTemp = new Vector<User>();
				for(Play p: plays){//Display all users that have played the game - check for duplicates
					if(p.game==g&&!userTemp.contains(p.user))userTemp.add(p.user);
				}
				for(User u: userTemp)gameStream.println(u.Name+"<br/>");
				gameStream.println("</body></html>");
			}
			mainStream.println("</body></html>");	
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		}
	}
}

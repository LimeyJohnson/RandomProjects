import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.PrintStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.Scanner;
import java.util.Vector;

public class ParserThread extends Thread {
	int mStart, mEnd, yStart, yEnd;
	PrintStream mainStream;
	Data data;
	Connection conn;
	
		public ParserThread(int monthStart, int monthEnd, int yearStart, int yearEnd, PrintStream pS, Data datain){
			mStart = monthStart;
			mEnd = monthEnd;
			yStart = yearStart;
			yEnd = yearEnd;
			mainStream = pS;
			data = datain;
			
		
		}
	 public void run() {
	        try {
				runURL(mStart+"",mEnd+"",yStart+"",yEnd+"","0",true);
			} catch (MalformedURLException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
	    }
	 public void runURL(String monthStart, String monthEnd, String yearStart, String yearEnd, String start, boolean Repeat)throws MalformedURLException, IOException{
			int out = 0;
			String urlString = "http://news.google.com/archivesearch?as_q=iran+us&num=100&hl=en&btnG=Search+Archives&as_epq=&as_oq=&as_eq=&as_user_ldate="+monthStart+"%2F"+yearStart+"&as_user_hdate="+monthEnd+"%2F"+yearEnd+"&lr=&as_src=&as_price=p0&as_scoring=&start="+start;	
			System.out.println(urlString);
			URL server = new URL(urlString);
			HttpURLConnection connect = (HttpURLConnection) server.openConnection();
			connect.connect();
			InputStream in = connect.getInputStream();
			String input = "";
			String temp;
			//		byte[] bytes = new byte[1024];

			BufferedReader d
			= new BufferedReader(new InputStreamReader(in));
			

			while((temp = d.readLine())!=null){
				input+=temp;
			}
			String output = input.substring(input.indexOf("<body"),input.indexOf("</body"));

			Pattern pat = Pattern.compile("<table cellpadding=0 cellspacing=0 border=0 style=\"margin-left: 1em;\"><tr><td><a href=.+?</div>.+?Related web pages");
			//Pattern pat = Pattern.compile("<td><a href.+?div class=j.+?margin-left: 1em;");
			Matcher mat = pat.matcher(output);
			
			String prev = "";
			while(mat.find()){
				String art = mat.group();

				
				if(!art.substring(0, 200).equals(prev)){
					int a, numRelated = 0;
					prev = art.substring(0,200);
					String titleLink, title, source, date, snippet,search="", keywords;
					art = art.substring(86, art.length());
					titleLink = art.substring(0,art.indexOf(">"));
					art = art.substring(art.indexOf(">")+1);
					title = art.substring(0,art.indexOf("<br>"));
					art = art.substring(art.indexOf("class=l")+8);
					source = art.substring(0, art.indexOf("<"));
					art = art.substring(art.indexOf(">")+4);
					date = art.substring(0,art.indexOf("<"));
					art = art.substring(art.indexOf("size=-1")+9);
					snippet = art.substring(0,art.indexOf("</font>"));
					if((a = art.indexOf("related"))>0){
						
						
						try{
							search = art.substring(a-15,a);
							numRelated = Integer.parseInt(search.substring(search.indexOf("All")+4,search.lastIndexOf(" ")));
						}
						catch( NumberFormatException ex){
							out ++;
							continue;
						}
						catch( StringIndexOutOfBoundsException ex){
							out ++;
							continue;
						}

					}
					art = art.substring(art.indexOf("/search?"));
					art = art.substring(art.indexOf("+")+1);
					keywords = art.substring(0,art.indexOf(">"));
					
					try {
						PreparedStatement stmt = conn.prepareStatement("INSERT INTO news.plays VALUES (null, ?,?,?,?,?,?,?)");
						stmt.setString(1, removeHTML(snippet));
						stmt.setString(2, removeHTML(title));
						stmt.setString(3, titleLink);
						stmt.setString(4, getSQLDate(date));
						stmt.setInt(5, numRelated);
						stmt.setString(6, keywords);
						stmt.setString(7,source);
						
						stmt.execute();
					} catch (SQLException e) {
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
					
//					mainStream.println("Count = " + data.getCount());
//					mainStream.println("TitleLink: "+titleLink);
//					mainStream.println("\nTitle: "+removeHTML(title));
//					mainStream.println("\nSource: "+source);
//					mainStream.println("\nDate: "+date);
//					mainStream.println("\nSnippet: "+removeHTML(snippet));
//					mainStream.println("\nNumber Related: "+numRelated);
//					mainStream.println("\nKeywords: "+keywords+"\n");
//					mainStream.println();
					data.addCount();

				}
				else{
					System.out.println("Found One");
				}
			}

			

			if(Repeat){
				pat = Pattern.compile("<td nowrap>");
				mat = pat.matcher(output);
				int countrepeat = 0;
				while(mat.find()){
					countrepeat ++;
				}
				System.out.println("Extra Pages " +(countrepeat-2) );
				
				for(int x = 1; x<=countrepeat-3;x++){
					runURL(monthStart,monthEnd,yearStart, yearEnd,(x*100)+"",false);
				}
				
			}
			System.out.println("Count: "+data.getCount()+" throughout "+out);
		
		}
		static String removeHTML(String input){

			boolean print = true;
			String output = "";
			for(int x = 0; x<input.length();x++){
				if(input.charAt(x)=='<')print = false;
				if(input.charAt(x)=='>')print = true;
				if(print && input.charAt(x)!='>') output+=(input.charAt(x));
				//		}
			}
			return output;
		}
		String getSQLDate(String input){
			Vector<String> months = new Vector<String>();
			months.add("Jan");
			months.add("Feb");
			months.add("Mar");
			months.add("Apr");
			months.add("May");
			months.add("Jun");
			months.add("Jul");
			months.add("Aug");
			months.add("Sep");
			months.add("Oct");
			months.add("Nov");
			months.add("Dec");
			
			
			String month = input.substring(0,3);
			input = input.substring(4);
			int day = Integer.parseInt(input.substring(0,input.indexOf(",")));
			int year = Integer.parseInt(input.substring(input.indexOf(",")+2,input.length()));
			
			return year+"-"+(months.indexOf(month)+1)+"-"+day;
		}
		
}

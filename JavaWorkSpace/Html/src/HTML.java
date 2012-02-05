import java.net.*;
import java.io.*;
import java.util.*;
public class HTML {
	static FileOutputStream fileStream;
	static PrintStream mainStream;
	
	public static void main(String[] args) throws MalformedURLException, IOException, InterruptedException{
		fileStream = new FileOutputStream("Full.txt");
		mainStream = new PrintStream(fileStream);
		Data data = new Data();
		
		//Vector<Thread> threads = new Vector<Thread>();
				
		int startMonth, startYear, endMonth, endYear;
			
		for(int x = 0; x<=8;x++){
			for(int y = 1; y<=12;y++){
				startYear = 2000+x;
				endYear = 2000+x;
				
				startMonth = y;
				if(y==12){
					endMonth = 1;
					endYear++;
				}
				else endMonth = y+1;
				System.out.println(startMonth +"/"+startYear+" - "+endMonth+"/"+endYear);
				ParserThread temp = new ParserThread(startMonth,endMonth,startYear,endYear,mainStream,data);
				//threads.add(temp);
				temp.start();
				temp.join();
				
			}
		}
		
	
		
		
	}
	
}

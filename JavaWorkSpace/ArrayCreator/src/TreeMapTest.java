import java.io.BufferedReader;

import java.io.DataInputStream;
import java.io.FileInputStream;

import java.io.IOException;
import java.io.InputStreamReader;
import java.util.*;
public class TreeMapTest {
	TreeMap<Integer,Integer> tm = new TreeMap<Integer,Integer>();
	public TreeMapTest(){
		FileInputStream fin;
		try{
			fin = new FileInputStream("web.txt");
			DataInputStream dim = new DataInputStream(fin);
			BufferedReader br = new BufferedReader(new InputStreamReader(dim));
			
	       
			String temp;
			while((temp = br.readLine())!=null){
				int index = Integer.parseInt(temp.substring(0, 4).trim());
				int data = num(temp.charAt(41))*num(temp.charAt(43))*num(temp.charAt(45))*num(temp.charAt(47))*num(temp.charAt(49));
				tm.put(data,(index-1));
//				System.out.println("Input: "+data+" - "+(index-1));
			}
			
		}
		catch(IOException e){
			System.out.println("Error");
		}
	
	
	}
	public int search(int a){
		try{
		return tm.get(a);	
		}
		catch(Exception e){
//			System.err.println(e.toString());
			System.out.println("Missing "+a);
		}
		return 0;
	}
	static int num(int a){
		int output = 0;
		switch(a){
		case 65: output=41;
		break;
		case 75: output=37;
		break;
		case 81: output=31;
		break;
		case 74: output=29;
		break;
		case 84: output=23;
		break;
		case 57: output=19;
		break;
		case 56: output=17;
		break;
		case 55: output=13;
		break;
		case 54: output=11;
		break;
		case 53: output=7;
		break;
		case 52: output=5;
		break;
		case 51: output=3;
		break;
		case 50: output=2;
		break;
		
		}
		return output;
	}
}

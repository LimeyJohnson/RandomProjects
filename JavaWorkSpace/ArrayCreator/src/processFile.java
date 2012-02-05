import java.io.*;
public class processFile {
	public static void main(String[] args){
		FileInputStream fin;
		try{
			fin = new FileInputStream("web.txt");
			DataInputStream dim = new DataInputStream(fin);
			BufferedReader br = new BufferedReader(new InputStreamReader(dim));
			FileWriter fstream = new FileWriter("out.txt");
	        BufferedWriter out = new BufferedWriter(fstream);
			String temp;
			while((temp = br.readLine())!=null){
				int index = Integer.parseInt(temp.substring(0, 4).trim());
				int data = num(temp.charAt(41))*num(temp.charAt(43))*num(temp.charAt(45))*num(temp.charAt(47))*num(temp.charAt(49));
				out.write("tm.put("+(data)+","+(index-1)+");"+'\n');
			}
			out.close();
		}
		catch(IOException e){
			System.out.println("Error");
		}
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

import java.net.*;
import java.io.*;
public class whois {

	/**
	 * @param args
	 * @throws Exception 
	 * @throws UnknownHostException 
	 */
	public static void main(String[] args) throws  Exception{
		URL hp = new URL("http://limeyhouse.dyndns.org:9796");
		int c;
		URLConnection hpCon = hp.openConnection();
		int len = hpCon.getContentLength();
		
		if(len !=0){
			InputStream input = hpCon.getInputStream();
			int num = 0;
			while((c = input.read())!=-1){
				System.out.print((char) c);
				num ++;
			}
			input.close();
			System.out.println("Number of characters: "+num);
		}
	}



}



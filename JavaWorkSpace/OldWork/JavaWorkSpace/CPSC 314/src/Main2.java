import java.util.*;
public class Main2 {

	public static void main(String[] args){
		Set<String> b = new HashSet<String>();
		b.add("Smelly");
		b.add("people");
		b.add("piss");
		b.add("me");
		b.add("off");
		
		WordSet B = new WordSet(b);
		
		while(B.hasNext()){
			System.out.print(B.next()+" ");
		}
		System.out.println();
	}
}

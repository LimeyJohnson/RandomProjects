import java.util.*;
public class Squared {

	public static void main(String[] args){
		Scanner scanner = new Scanner(System.in);
		
		System.out.println("What is the number you want squared ");
		int input = scanner.nextInt(), squared = 0, cubed=0;
		
		for(int x = 0; x<input; x++){
			squared+= (2*x)+1;
			cubed += (3*x*x)+x*3+1;
		}
		System.out.println(squared);
		System.out.println(cubed);
	}
}

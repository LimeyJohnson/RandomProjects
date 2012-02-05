import java.util.HashSet;
import java.util.Set;
public class Main1 {

	public static void main(String[] args) {
		Set<String> a = new HashSet<String>();
		Set<String> b = new HashSet<String>();
		
		a.add("Johnson");
		a.add("Andrew");
		
		b.add("Andrew");
		b.add("Johnson");
		
		
		WordSet A = new WordSet(a);
		WordSet B = new WordSet(b);
		System.out.println(a.equals(b));
		System.out.println(A.equals(B));
	}
}

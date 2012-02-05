import java.util.Collection;
import java.util.ArrayList;
public class Main {
	public static <T> T feedAll(Collection<String> c, Mouth<?> m) {
		 T current = null;
		for (String t : c) {
			current = (T) t;
			m.eat(t);
		}
		return current;
	}
	public static void main(String args[]) {
		Mouth<Object> m = new EatAndTalk();
		Collection<String> word = new ArrayList<String>();
		word.add("Apple");
		word.add("Hot dog");
		String str = feedAll(word, m);
	}
}
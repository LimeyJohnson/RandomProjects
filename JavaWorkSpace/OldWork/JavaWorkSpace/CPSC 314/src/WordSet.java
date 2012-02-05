import java.util.*;
public class WordSet implements Iterator<String>{

	/** Set of words. */

	/** Words of the set of words. */
	private String[] words;
	private int pos=0;
	/**
	 * Initialize the the WordSet with words.
	 */
	public WordSet(Set<String> words) {
		this.words = words.<String>toArray(new String[words.size()]);
		
		
	}
	/**
	 * Return the words in the set as an array.
	 */
	public String[] getWords() { return words.clone(); }
	public boolean equals(WordSet b){
		String[] bWords = b.getWords();
		if(bWords == null && words == null) return true;
		if(bWords == null || words == null) return false;
		
		if(bWords.length!=words.length)return false;
		boolean output = true;
		for(int x = 0; x<words.length && output; x++){
			String current = words[x];
			boolean currentBool = false;
			for(int y = 0; y<bWords.length && !currentBool; y++){
				if(bWords[y].equals(current)) currentBool = true;
			}
			output = currentBool;
		}
		return output;
		
		
	}
	
	
	public boolean hasNext() {
		
		return (pos < words.length);
	}
	
	public String next() {
		// TODO Auto-generated method stub
		if( pos >= words.length) throw new NoSuchElementException();
		return words[pos++];
	}
	
	public void remove() {
		// TODO Auto-generated method stub
		
	}
}


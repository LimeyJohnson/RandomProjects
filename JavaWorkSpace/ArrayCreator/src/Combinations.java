

import java.text.DecimalFormat;
import java.util.*;

public class Combinations {
	// public static void main(String[] args) {
	private int[] CS;
	TreeMapTest TM = new TreeMapTest();
	private int totalHands = 0;
	int[] primes = {41,37,31,29,23,19,17,13,11,7,5,3,2};
	int[] p1 = new int[7];
	int[] p2 = new int[7];
	int ties = 0, wins = 0, losses=0;
	public Combinations(int[] CS_in) {
		CS = CS_in;
		
	}

	public void Calculate() {
		if (CS[8] == 100)
			preFlop();
		
	}

	public void preFlop() {
		int x = 0;
		Vector<Integer> v = new Vector<Integer>();
		for (x = 0; x <= 51; x++)
			v.add(x);

		double start = System.currentTimeMillis();

		
		
		p1[0] = CS[0];
		p1[1] = CS[1];
		
		p2[0] = CS[2];
		p2[1] = CS[3];

		v.remove(CS[0]);
		v.remove(CS[1]);
		v.remove(CS[2]);
		v.remove(CS[3]);
		
		while (v.size() >= 5) {
			int a = v.remove(0);
			p1[2] = a;
			p2[2] = a;
			for (int b = 0; b <= (v.size() - 4); b++) {
				p1[3] = v.get(b);
				p2[3] = v.get(b);
				for (int c = b + 1; c <= (v.size() - 3); c++) {
					// cards.add(v.get(c));
					// cards[2] = v.get(c);
					p1[4] = v.get(c);
					p2[4] = v.get(c);
					for (int d = c + 1; d <= (v.size() - 2); d++) {
						// cards.add(v.get(d));
						// cards[3] = v.get(d);
						p1[5] =v.get(d);
						p2[5] = v.get(d);
						for (int e = d + 1; e <= (v.size() - 1); e++) {

							p1[6] = v.get(e);
							p2[6] = v.get(e);
							//if(H[0].HandID==6&&H[1].HandID==6)
							//TC.testTie();
							runHands();
						}
					}

				}
			}

		}


		double finish = System.currentTimeMillis();

		System.out.println("Count = " + totalHands + " Time taken = "
				+ ((finish - start) / 1000) + " sec");
		System.out.println("Wins = "+ wins+" losses = "+losses+" ties = "+ties);
	}
	void runHands(){
		
		totalHands++;
		int k =0;
		int p1b = 5000, p2b = 5000;
		if((k = run(p1[2],p1[3],p1[4],p1[5],p1[6]))<p1b) p1b = k;
		if((k = run(p1[1],p1[3],p1[4],p1[5],p1[6]))<p1b) p1b = k;
		if((k = run(p1[1],p1[2],p1[4],p1[5],p1[6]))<p1b) p1b = k;
		if((k = run(p1[1],p1[2],p1[3],p1[5],p1[6]))<p1b) p1b = k;
		if((k = run(p1[1],p1[2],p1[3],p1[4],p1[6]))<p1b) p1b = k;
		if((k = run(p1[1],p1[2],p1[3],p1[4],p1[5]))<p1b) p1b = k;
		if((k = run(p1[0],p1[3],p1[4],p1[5],p1[6]))<p1b) p1b = k;
		if((k = run(p1[0],p1[2],p1[4],p1[5],p1[6]))<p1b) p1b = k;
		if((k = run(p1[0],p1[2],p1[3],p1[5],p1[6]))<p1b) p1b = k;
		if((k = run(p1[0],p1[2],p1[3],p1[4],p1[6]))<p1b) p1b = k;
		if((k = run(p1[0],p1[2],p1[3],p1[4],p1[5]))<p1b) p1b = k;
		if((k = run(p1[0],p1[1],p1[4],p1[5],p1[6]))<p1b) p1b = k;
		if((k = run(p1[0],p1[1],p1[3],p1[5],p1[6]))<p1b) p1b = k;
		if((k = run(p1[0],p1[1],p1[3],p1[4],p1[6]))<p1b) p1b = k;
		if((k = run(p1[0],p1[1],p1[3],p1[4],p1[5]))<p1b) p1b = k;
		if((k = run(p1[0],p1[1],p1[2],p1[5],p1[6]))<p1b) p1b = k;
		if((k = run(p1[0],p1[1],p1[2],p1[4],p1[6]))<p1b) p1b = k;
		if((k = run(p1[0],p1[1],p1[2],p1[4],p1[5]))<p1b) p1b = k;
		if((k = run(p1[0],p1[1],p1[2],p1[3],p1[6]))<p1b) p1b = k;
		if((k = run(p1[0],p1[1],p1[2],p1[3],p1[5]))<p1b) p1b = k;
		if((k = run(p1[0],p1[1],p1[2],p1[3],p1[4]))<p1b) p1b = k;
		
		if((k = run(p2[2],p2[3],p2[4],p2[5],p2[6]))<p2b) p2b = k;
		if((k = run(p2[1],p2[3],p2[4],p2[5],p2[6]))<p2b) p2b = k;
		if((k = run(p2[1],p2[2],p2[4],p2[5],p2[6]))<p2b) p2b = k;
		if((k = run(p2[1],p2[2],p2[3],p2[5],p2[6]))<p2b) p2b = k;
		if((k = run(p2[1],p2[2],p2[3],p2[4],p2[6]))<p2b) p2b = k;
		if((k = run(p2[1],p2[2],p2[3],p2[4],p2[5]))<p2b) p2b = k;
		if((k = run(p2[0],p2[3],p2[4],p2[5],p2[6]))<p2b) p2b = k;
		if((k = run(p2[0],p2[2],p2[4],p2[5],p2[6]))<p2b) p2b = k;
		if((k = run(p2[0],p2[2],p2[3],p2[5],p2[6]))<p2b) p2b = k;
		if((k = run(p2[0],p2[2],p2[3],p2[4],p2[6]))<p2b) p2b = k;
		if((k = run(p2[0],p2[2],p2[3],p2[4],p2[5]))<p2b) p2b = k;
		if((k = run(p2[0],p2[1],p2[4],p2[5],p2[6]))<p2b) p2b = k;
		if((k = run(p2[0],p2[1],p2[3],p2[5],p2[6]))<p2b) p2b = k;
		if((k = run(p2[0],p2[1],p2[3],p2[4],p2[6]))<p2b) p2b = k;
		if((k = run(p2[0],p2[1],p2[3],p2[4],p2[5]))<p2b) p2b = k;
		if((k = run(p2[0],p2[1],p2[2],p2[5],p2[6]))<p2b) p2b = k;
		if((k = run(p2[0],p2[1],p2[2],p2[4],p2[6]))<p2b) p2b = k;
		if((k = run(p2[0],p2[1],p2[2],p2[4],p2[5]))<p2b) p2b = k;
		if((k = run(p2[0],p2[1],p2[2],p2[3],p2[6]))<p2b) p2b = k;
		if((k = run(p2[0],p2[1],p2[2],p2[3],p2[5]))<p2b) p2b = k;
		if((k = run(p2[0],p2[1],p2[2],p2[3],p2[4]))<p2b) p2b = k;
		
		if(p2b == p1b ) ties++;
		if(p1b<p2b) wins++;
		if(p1b>p2b) losses++;
		
		
		
	}
	public int run(int a, int b, int c, int d, int e){
		int search = primes[a/4]*primes[b/4]*primes[c/4]*primes[d/4]*primes[e/4];
		return TM.search(search);
	}
	

	public void printResults(boolean b) {}

	public double round(double d) {
		DecimalFormat twoDForm = new DecimalFormat("#.##");
		return Double.valueOf(twoDForm.format(d));
	}
}
package pokerPackage;

import java.io.FileOutputStream;
import java.io.PrintStream;
import java.util.Collections;
import java.util.Vector;

public class Debuger {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		int totalHands = 0;
		int x = 0;
		Vector<Integer> v = new Vector<Integer>();
		Vector<Hand> results = new Vector<Hand>();
		for (x = 0; x <= 51; x++)v.add(x);
		int TestingID = 3 ;
		double start = System.currentTimeMillis();

		int card1 = 51;
		int card2 = 26;
		int card3 = 36;
		int card4 = 44;


		v.remove(v.indexOf(card1));
		v.remove(v.indexOf(card2));
		v.remove(v.indexOf(card3));
		v.remove(v.indexOf(card4));

		while (v.size() >= 5) {
			int a = v.remove(0);
			for (int b = 0; b <= (v.size() - 4); b++) {
				for (int c = b + 1; c <= (v.size() - 3); c++) {
					for (int d = c + 1; d <= (v.size() - 2); d++) {
						for (int e = d + 1; e <= (v.size() - 1); e++) {
							if(results.size()<100000){
								Hand h1 = new Hand();
								h1.firstCards(card1,card2);
								h1.addCard(a, 0);
								h1.addCard(v.get(b), 1);
								h1.addCard(v.get(c), 2);
								h1.addCard(v.get(d), 3);
								h1.addCard(v.get(e), 4);
								h1.calculateHand();

								Hand h2 = new Hand();
								h2.firstCards(card3,card4);
								h2.addCard(a,0);
								h2.addCard(v.get(b), 1);
								h2.addCard(v.get(c), 2);
								h2.addCard(v.get(d), 3);
								h2.addCard(v.get(e), 4);
								h2.calculateHand();

								totalHands++;

								if(h2.HandID == TestingID)results.add(h2);
								if(h1.HandID == TestingID)results.add(h1);

							}
						}
					}

				}
			}

		}


		double finish = System.currentTimeMillis();

		System.out.println("Count = " + totalHands + " Time taken = "
				+ ((finish - start) / 1000) + " sec");
		Collections.sort(results, new HandComparator());
		FileOutputStream out; // declare a file output object
		PrintStream p; // declare a print stream object

		try
		{
			// Create a new file output stream
			// connected to “myfile.txt”
			out = new FileOutputStream("TwoPair.txt");

			// Connect print stream to the output stream
			p = new PrintStream( out );

			while(!results.isEmpty()){
				Hand y = results.remove(0);
				p.println(y.HandID+":"+getCard(y.ID1[0])+","+getCard(y.ID1[1])+","+getCard(y.ID1[2])+","+getCard(y.ID1[3])+","+getCard(y.ID1[4])+","+getCard(y.ID1[5])+","+getCard(y.ID1[6])+"-"+y.flags[0]+y.flags[1]);
			}

			p.close();
		}
		catch (Exception e)
		{
			//            System.err.println (”Error writing to file”);
		}

		System.out.println("done");

	}
	public static String getCard(int x){
		String suits[] = {"d","c","h","s"}; 
		String nums[] = {"A","K","Q","J","T","9","8","7","6","5","4","3","2"};
		return nums[x/4]+suits[x%4];
	}
}

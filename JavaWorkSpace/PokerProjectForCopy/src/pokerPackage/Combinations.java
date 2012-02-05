package pokerPackage;

import java.text.DecimalFormat;
import java.util.*;

public class Combinations {
	// public static void main(String[] args) {
	private int[] CS;
	private Hand[] H = new Hand[4];
	private int totalHands = 0, players;
	TieComparator TC = new TieComparator();


	public Combinations(int[] CS_in) {
		CS = CS_in;
		if (CS[2] != 100) {
			players = 2;
			if (CS[4] != 100) {
				players = 3;
				if (CS[6] != 100)
					players = 4;
			}

		} else
			players = 1;

	}

	public void Calculate() {
		if (CS[8] == 100)
			preFlop();
		else if (CS[8] != 100&&CS[11]==100)postFlop();
		else if(CS[11] != 100) river();
	}

	public void preFlop() {
		int x = 0;
		Vector<Integer> v = new Vector<Integer>();
		for (x = 0; x <= 51; x++)
			v.add(x);

		double start = System.currentTimeMillis();

		for (x = 0; x < players; x++) {// setup hands and remove cards from
			// evaluation

			H[x] = new Hand();
			int card1 = CS[x * 2];
			int card2 = CS[(x * 2) + 1];
			H[x].firstCards(card1, card2);
			v.remove(v.indexOf(card1));
			v.remove(v.indexOf(card2));
		}

		while (v.size() >= 5) {
			int a = v.remove(0);
			for (x = 0; x < players; x++) {
				H[x].addCard(a, 0);
			}
			for (int b = 0; b <= (v.size() - 4); b++) {
				// cards.add(v.get(b));
				// cards[1] = v.get(b);
				for (x = 0; x < players; x++) {
					H[x].addCard(v.get(b), 1);
				}
				for (int c = b + 1; c <= (v.size() - 3); c++) {
					// cards.add(v.get(c));
					// cards[2] = v.get(c);
					for (x = 0; x < players; x++) {
						H[x].addCard(v.get(c), 2);
					}
					for (int d = c + 1; d <= (v.size() - 2); d++) {
						// cards.add(v.get(d));
						// cards[3] = v.get(d);
						for (x = 0; x < players; x++) {
							H[x].addCard(v.get(d), 3);
						}
						for (int e = d + 1; e <= (v.size() - 1); e++) {

							for (x = 0; x < players; x++) {
								H[x].addCard(v.get(e), 4);
								H[x].calculateHand();
							}
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
	}
	void runHands(){
		totalHands++;
		int x =0;
		int maxHandID = 0;
		for (x = 0; x < players; x++) {
			if (H[x].HandID > maxHandID) {
				maxHandID = H[x].HandID;
			}
		}
		int winnerCount = 0;

		for (x = 0; x < players; x++) {
			if (H[x].HandID == maxHandID) {
				winnerCount++;
				//				TC.addHand(H[x]);
			}

		}
		if (winnerCount == 1 ) {
			for (x = 0; x < players; x++) {
				if (H[x].HandID == maxHandID) {
					H[x].DR[0]++;
					H[x].DR[3]++;
					
				} 
				else {
					H[x].DR[1]++;
				}
			}
		} 
		else {
			TC.removeHands();
			for (x = 0; x < players; x++) {

				if (H[x].HandID == maxHandID) {
					TC.addHand(H[x]);
				}
				else{
					H[x].DR[1]++;
				}
			}
			TC.testTie();
		}
	}
	public void postFlop() {
		int x = 0;
		Vector<Integer> v = new Vector<Integer>();
		for (x = 0; x <= 51; x++)
			v.add(x);

		double start = System.currentTimeMillis();

		for (x = 0; x < players; x++) {// setup hands and remove cards from
			// evaluation

			H[x] = new Hand();
			int card1 = CS[x * 2];
			int card2 = CS[(x * 2) + 1];
			H[x].firstCards(card1, card2);
			v.remove(v.indexOf(card1));
			v.remove(v.indexOf(card2));
			H[x].addCard(CS[8], 2);
			H[x].addCard(CS[9],3);
			H[x].addCard(CS[10],4);
		}
		v.remove(v.indexOf(CS[8]));
		v.remove(v.indexOf(CS[9]));
		v.remove(v.indexOf(CS[10]));



		while (v.size() >= 2) {
			int a = v.remove(0);
			for (x = 0; x < players; x++) {
				H[x].addCard(a, 0);
			}
			for (int b = 0; b <= (v.size() - 1); b++) {
				// cards.add(v.get(b));
				// cards[1] = v.get(b);
				for (x = 0; x < players; x++) {
					H[x].addCard(v.get(b), 1);
					H[x].calculateHand();
				}
				runHands();

			}
		}
		double finish = System.currentTimeMillis();

		System.out.println("Count = " + totalHands + " Time taken = "
				+ ((finish - start) / 1000) + " sec");
	}
	void river(){
		int x = 0;
		Vector<Integer> v = new Vector<Integer>();
		for (x = 0; x <= 51; x++)
			v.add(x);

		double start = System.currentTimeMillis();

		for (x = 0; x < players; x++) {// setup hands and remove cards from
			// evaluation

			H[x] = new Hand();
			int card1 = CS[x * 2];
			int card2 = CS[(x * 2) + 1];
			H[x].firstCards(card1, card2);
			v.remove(v.indexOf(card1));
			v.remove(v.indexOf(card2));
			H[x].addCard(CS[11], 1);
			H[x].addCard(CS[8], 2);
			H[x].addCard(CS[9],3);
			H[x].addCard(CS[10],4);
		}

		v.remove(v.indexOf(CS[8]));
		v.remove(v.indexOf(CS[9]));
		v.remove(v.indexOf(CS[10]));
		v.remove(v.indexOf(CS[11]));


		while (v.size() >= 1) {
			int a = v.remove(0);
			for (x = 0; x < players; x++) {
				H[x].addCard(a, 0);
				H[x].calculateHand();
			}
			runHands();


		}
		double finish = System.currentTimeMillis();

		System.out.println("Count = " + totalHands + " Time taken = "
				+ ((finish - start) / 1000) + " sec");
	}

	public void printResults(boolean b) {
		System.out.println(H[0].getCard(H[0].ID1[5])+" "+H[0].getCard(H[0].ID1[6]));
		System.out.println(H[1].getCard(H[1].ID1[5])+" "+H[1].getCard(H[1].ID1[6]));
		if(players>2)System.out.println(H[2].getCard(H[2].ID1[5])+" "+H[2].getCard(H[2].ID1[6]));
		String[] r = {"Win","Loose","Tie","Equity","PotOdd","HighCar","OnePair","TwoPair","Trips","Straigh","Flush","FullHou","Quads","SFlush"};
		for (int p = 0; p < 14; p++) {
			System.out.print(r[p] + "\t\t"
					+ round(((H[0].DR[p]) / totalHands) * 100));
			if (players > 1) {
				System.out.print("\t"
						+ round(((H[1].DR[p]) / totalHands) * 100));
			} //else
			//System.out.print("\t0");

			if (players > 2) {
				System.out.print("\t"
						+ round(((H[2].DR[p]) / totalHands) * 100));
			} //else
			//System.out.print("\t0");
			if (players > 3) {
				System.out.print("\t"
						+ round(((H[3].DR[p]) / totalHands) * 100));
			} //else
			//System.out.print("\t0");

			System.out.println();
		}
		System.out.println();
		for (int p = 0; p < 14; p++) {
			System.out.print(r[p] + "\t\t" + H[0].DR[p]);
			if (players > 1) {
				System.out.print("\t" + H[1].DR[p]);
			} //else
			//System.out.print("\t0");

			if (players > 2) {
				System.out.print("\t" + H[2].DR[p]);
			} //else
			//System.out.print("\t0");
			if (players > 3) {
				System.out.print("\t" + H[3].DR[p]);
			} 
			System.out.println();

		}
		
	}

	public double round(double d) {
		DecimalFormat twoDForm = new DecimalFormat("#.##");
		return Double.valueOf(twoDForm.format(d));
	}
}
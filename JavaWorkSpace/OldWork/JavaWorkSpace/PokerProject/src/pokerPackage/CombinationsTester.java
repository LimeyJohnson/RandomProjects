package pokerPackage;

public class CombinationsTester {

	public static void main(String[] args) {
		
		int[] CS = new int[12];
		
//		Player One Cards
		CS[0] = 5;
		CS[1] = 26;
		
//		Player Two Cards
		CS[2] = 9;
		CS[3] = 21;
		
//		Player Three Cards
		CS[4] = 36;
		CS[5] = 11;
		
//		Player Four Cards
		CS[6] = 0;
		CS[7] = 51;
		
//		Flop
		CS[8] = 41;
		CS[9] = 30;
		CS[10] = 17;
		
//		Turn
		CS[11] = 48;
		
		Combinations C = new Combinations(CS);
		C.Calculate();
		C.printResults(false);
		System.out.println("Flop Hands: "+getCard(CS[8])+" "+getCard(CS[9])+" "+getCard(CS[10])+" ");
	}
	public static String getCard(int x){
		String suits[] = {"d","s","h","c"}; 
		String nums[] = {"A","K","Q","J","T","9","8","7","6","5","4","3","2"};
		return nums[x/4]+suits[x%4];
	}
}

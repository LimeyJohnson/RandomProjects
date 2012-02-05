

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
		CS[4] = 100;
		CS[5] = 100;
		
//		Player Four Cards
		CS[6] = 100;
		CS[7] = 100;
		
//		Flop
		CS[8] = 100;
		CS[9] = 100;
		CS[10] = 100;
		
//		Turn
		CS[11] = 100;
		
		Combinations C = new Combinations(CS);
		C.Calculate();
		C.printResults(false);
//		System.out.println("Flop Hands: "+getCard(CS[8])+" "+getCard(CS[9])+" "+getCard(CS[10])+" ");
	}
	public static String getCard(int x){
		String suits[] = {"d","s","h","c"}; 
		String nums[] = {"A","K","Q","J","T","9","8","7","6","5","4","3","2"};
		return nums[x/4]+suits[x%4];
	}
}

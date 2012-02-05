package pokerPackage;
import java.util.*;

public class Hand {
	int[] ID1 = {0,0,0,0,0,0,0};
	int[] flags = {0,0};
	boolean[] first = {true,true,true,true,true};
	int a = 0, b = 0; // loop helpers
	int[] nums = {0,0,0,0,0,0,0,0,0,0,0,0,0};
	int[] suits = {0,0,0,0};
	public int HandID = 0;
	double[] DR = {0,0,0,0,0,0,0,0,0,0,0,0,0,0};
	public Hand(){}

	
	public void firstCards(int a, int b){
		ID1[5] = a;
		ID1[6] = b;
		nums[a/4]++;
		suits[a%4]++;
		nums[b/4]++;
		suits[b%4]++;
	}
	public void addCard(int card, int pos){

		if(!first[pos]){

			nums[ID1[pos]/4]--;
			suits[ID1[pos]%4]--;

		}
		else first[pos] = false;

		ID1[pos] = card;

		nums[card/4]++;
		suits[card%4]++;
	}
	public void calculateHand(){

		HandID = 1;
		Boolean Straight = false, Flush = false, TwoPair = false;
		if(isPR()){
			HandID = 2;
			if(isTP()){
				HandID = 3;
				TwoPair = true;
			}
			if(isTK()){
				HandID = 4;
				if(TwoPair){if(isFH())HandID = 7;}
				if(isFK())HandID = 8;
			}
		}
		else{
			HandID = 1;
		}

		if(isST()){
			HandID = 5;
			Straight = true;

		}
		if(isFL()){
			HandID = 6;
			Flush = true;
		}
		if(Straight&&Flush){
			//If Running seven cards, you must check to make sure the Straight is a Flush
			if(isSF())HandID = 9;
		}

		DR[HandID+4]++;

	}





	// Based on numbers in ascending order
	public boolean isSF(){
		Vector<Integer> ID2 = new Vector<Integer>();		
		for(int move:ID1)ID2.add(move);
		Collections.sort(ID2);
		for(int numb: ID2){
			if(numb<36){
				if(ID2.contains(numb+4)&&ID2.contains(numb+8)&&ID2.contains(numb+12)&&ID2.contains(numb+16)){
					flags[0] = numb/4;
					return true;
				}
			}
			else if(numb<40){
				if(ID2.contains(numb+4)&&ID2.contains(numb+8)&&ID2.contains(numb+12)&&ID2.contains(numb-36)){
					flags[0] = numb/4;
					return true;
				}
			}

		}
		return false;
	}

	public boolean isFK(){
		for(a = 0; a<13;a++){
			if(nums[a]>=4){
				flags[0] = a;
				return true;
			}
		}
		return false;
	}
	public boolean isFH(){
		for(a = 0; a<13;a++){
			b = nums[a];
			if(b>=3){
				for(int d = 0; d<13; d++){
					int c = nums[d];
					if(c>=2&&a!=d){
						flags[0] = a;
						flags[1] = d;
						return true;
					}
				}

			}
		}
		return false;
	}
	public boolean isFL(){
		for(a = 0; a<4;a++){
			b = suits[a];
			if(b>=5){
				flags[0] = a;
				return true;
			}

		}
		return false;

	}
	public boolean isST(){
		for(b = 0; b<=9;b++){
			if(nums[b]>0){
				if(b<=8){
					if(nums[b+1]>0&&nums[b+2]>0&&nums[b+3]>0&&nums[b+4]>0){
						flags[0] = b;
						return true;
					}
				}
				else if(b==9){
					if(nums[b+1]>0&&nums[b+2]>0&&nums[b+3]>0&&nums[0]>0){
						flags[0] = b;
						return true;
					}
				}

			}
		}
		return false;


	}
	public boolean isTK(){
		for(a = 0; a<13;a++){
			b = nums[a];
			if(b>=3){
				flags[0] = a;
				return true;
			}
		}

		return false;
	}
	public boolean isTP(){

		for(a = 0; a<13;a++){
			
			if(nums[a]>=2){
				for(int c = a+1; c<13; c++){
			
					if(nums[c]>=2){
						flags[0] = a;
						flags[1] = c;
						return true;
					}
				}
				return false;
			}
		}
			
		return false;
	}
	public boolean isPR(){
		for(a = 0; a<13;a++){
			if(nums[a]>=2){
				flags[0] = a;
				return true;
			}
			//NEEDS TO BE FIXED			
		}
		return false;
	}
	public void printHand(){
		System.out.println("Number of cards: "+ID1.length);

		for(int x = 0; x<5; x++){
			//	if(B[x]>=0)System.out.print(getCard(B[x])+getSuit(B[x])+"("+B[x]+"),");
		}

	}
	public String getCard(int x){
		String suits[] = {"d","s","h","c"}; 
		String nums[] = {"A","K","Q","J","T","9","8","7","6","5","4","3","2"};
		return nums[x/4]+suits[x%4];
	}
}

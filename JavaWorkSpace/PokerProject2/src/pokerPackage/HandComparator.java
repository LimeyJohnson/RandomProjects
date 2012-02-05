package pokerPackage;

import java.util.Comparator;


public class HandComparator implements Comparator<Hand>{

	public int compare(Hand o1, Hand o2) {
		if(o1.HandID!=o2.HandID) return o1.HandID - o2.HandID;

		

		int HandID = o1.HandID;

		int comp = 0;
		switch(HandID){
		case 1: comp = compareHighCard(o1,o2);
		break;
		case 2:  comp = comparePair(o1,o2);
		break;
		case 3: comp = compareTwoPair(o1,o2);
		break;
		case 4: comp = compareThreeKind(o1,o2);
		break;
		case 5: case 9: comp = compareStraight(o1,o2);
		break;
		case 6: comp = compareFlush(o1,o2);
		break;
		case 7: comp = compareFullHouse(o1,o2);
		break;
		case 8: comp = compareFourKind(o1,o2);
		break;
		}
		return (-1) * comp;

	}
	public int compareHighCard(Hand a, Hand b) {
		int count = 0;
		for(int f = 0; f<13; f++){
			if(a.nums[f]!= b.nums[f]){
				return a.nums[f] - b.nums[f];
			}
			if(a.nums[f]>0)count ++;
			if(count>=5){
				return 0;
			}
		}
		System.out.println("Error in HighCard");
		return 0;

	}

	public int comparePair(Hand a, Hand b){
		if(a.flags[0] != b.flags[0]){
			return b.flags[0] - a.flags[0];
		}
		int count = 0;
		for(int f = 0; f<13; f++){
			if(a.flags[0]!=f){
				if(a.nums[f]!= b.nums[f]){
					return a.nums[f] - b.nums[f];
				}
				if(a.nums[f]>0)count ++;
				if(count>=3)return 0;

			}
		}
		System.out.println("Error in Pair");
		return 0;


	}
	public int compareTwoPair(Hand a, Hand b){
		if(a.flags[0] == b.flags[0]){
			if(a.flags[1] == b.flags[1]){
				//return testHK(h,j);
				for(int x = 0; x<13; x++){
					if(x!=a.flags[0]&&x!=a.flags[1]){
						if(a.nums[x]>b.nums[x])return 1;
						if(a.nums[x]<b.nums[x])return -1;
					}
				}
				return 0;
			}
			else{
				return b.flags[1] - a.flags[1];
			}
		}
		else{

			return b.flags[0] - a.flags[0];
		}
		//		return 0;
	}
	public int compareThreeKind(Hand a, Hand b){
		if(a.flags[0] != b.flags[0]){

			return b.flags[0] - a.flags[0];
		}

		int count = 0;
		for(int f = 0; f<13; f++){
			if(f!=a.flags[0]){
				if(a.nums[f]!= b.nums[f]){
					return a.nums[f] - b.nums[f];
				}
				if(a.nums[f]>0)count ++;
				if(count>=2)return 0;

			}
		}
		System.out.println("Error in three of a Kind");
		return 0;
	}
	public int compareStraight(Hand a, Hand b){
		return b.flags[0] - a.flags[0];

	}
	public int compareFlush(Hand a, Hand b){
		int suit = a.flags[0];
		int count = 0;
		for(int x =0; x<13; x++){
			boolean hasA = false, hasB = false;

			for(int y = 0; y<7; y++){
				if(!hasA){
					if(a.ID1[y]%4 == suit && (a.ID1[y]/4)==x) hasA = true;
				}
				if(!hasB){
					if(b.ID1[y]%4 == suit && (b.ID1[y]/4)==x) hasB = true;
				}
			}
			if(hasA && !hasB) return 1;
			if(!hasA && hasB) return -1;
			if(hasA && hasB) count++;
			if(count>=5)return 0;
		}
		System.out.println("Error in Flush");
		return 0;
	}
	public int compareFullHouse(Hand a, Hand b){
		if(a.flags[0] == b.flags[0]){
			if(a.flags[1] == b.flags[1]){
				return 0;

			}
			else{
				return b.flags[1] - a.flags[1];
			}
		}
		else{
			return b.flags[0] - a.flags[0];
		}

	}
	public int compareFourKind(Hand a, Hand b){
		if(a.flags[0] != b.flags[0]){
			//			if(a.flags[0] > b.flags[0]) return 1;
			//			if(a.flags[0] < b.flags[0] )return -1;
			return b.flags[0] - a.flags[0];
		}

		for(int f = 0; f<13; f++){
			if(f!=a.flags[0]){
				if(a.nums[f]!= b.nums[f]){
					return a.nums[f] - b.nums[f];
				}
				if(a.nums[f]>0) return 0;
			}	
		}
		System.out.println("Error in Four of a Kind");
		return 0;
	}
}




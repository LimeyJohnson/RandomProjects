package pokerPackage;

import java.util.Collections;
import java.util.Vector;

public class TieComparator {
	Vector<Hand> v = new Vector<Hand>();
	Vector<Hand> winners = new Vector<Hand>();
	int HandID;
	//	int ties3 = 0;
	int size = 0;

	public void addHand(Hand hin){
		v.add(hin);

	}
	public void removeHands(){
		v.removeAllElements();
	}
	public void testTie(){


		HandID = v.get(0).HandID;
		size = v.size();
		winners.removeAllElements();
		winners.add(v.get(0));
	
		for(int a = 1;a<size;a++){
			int comp = 0;
			switch(HandID){
			case 1: comp = compareHighCard(winners.get(0),v.get(a));
			break;
			case 2:  comp = comparePair(winners.get(0),v.get(a));
			break;
			case 3: comp = compareTwoPair(winners.get(0),v.get(a));
			break;
			case 4: comp = compareThreeKind(winners.get(0),v.get(a));
			break;
			case 5: case 9: comp = compareStraight(winners.get(0),v.get(a));
			break;
			case 6: comp = compareFlush(winners.get(0),v.get(a));
			break;
			case 7: comp = compareFullHouse(winners.get(0),v.get(a));
			break;
			case 8: comp = compareFourKind(winners.get(0),v.get(a));
			break;
			}
			if(comp==0){
				winners.add(v.get(a));

			}
			if(comp<0){
				winners.removeAllElements();
				winners.add(v.get(a));
			}

		}
		if(winners.size()>1){
			for(Hand H: winners){
				H.DR[2]++;
				H.DR[3]+=(1.0/(double)winners.size());
			}
			

		}
		else{
			for(Hand H: winners){
				H.DR[0]++;
				H.DR[3]++;
			
			}
		}
		for(Hand H: v){
			if(!winners.contains(H))H.DR[1]++;
		}
	}

	public int compareHighCard(Hand a, Hand b) {
		int count = 0;
		for(int f = 0; f<13; f++){
			if(a.nums[f]!= b.nums[f]){
				return a.nums[f] - b.nums[f];
			}
			if(a.nums[f]>0)count ++;//=a.nums[f];
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
		Vector<Integer>A  = new Vector<Integer>(7);
		Vector<Integer>B  = new Vector<Integer>(7);
		
		for(int g: a.ID1)A.add(g);
		for(int h: b.ID1)B.add(h);
		
		Collections.sort(A);
		Collections.sort(B);
		 
		int count = 0;
		for(int u =0; u<7;u++){
			if(A.get(u)/4<B.get(u)/4)return 1;
			if(A.get(u)/4>B.get(u)/4)return -1;
			if(A.get(u)/4!=a.flags[0])count ++;
			if(count>=3) return 0;
		}
//		int count = 0;
//		for(int f = 0; f<13; f++){
//			if(a.flags[0]!=f){
//				if(a.nums[f]!= b.nums[f]){
//					return a.nums[f] - b.nums[f];
//				}
//				if(a.nums[f]>0)count ++;
//				count += a.nums[f];
//				if(count>=3)return 0;
//
//			}
//		}
//		System.out.println("Error in Pair");
		return 0;


	}
	public int compareTwoPair(Hand a, Hand b){
		if(a.flags[0] == b.flags[0]){
			if(a.flags[1] == b.flags[1]){
//				//return testHK(h,j);
//				for(int x = 0; x<13; x++){
//					if(x!=a.flags[0]&&x!=a.flags[1]){
//						if(a.nums[x]>b.nums[x])return 1;
//						if(a.nums[x]<b.nums[x])return -1;
//						if(a.nums[x]>0)return 0;
//					}
//					
//				}
//				return 0;
				Vector<Integer>A  = new Vector<Integer>(7);
				Vector<Integer>B  = new Vector<Integer>(7);
				for(int g: a.ID1)A.add(g);
				for(int h: b.ID1)B.add(h);
				Collections.sort(A);
				Collections.sort(B);
				int count = 0;
				for(int u =0; u<7;u++){
					if(A.get(u)/4<B.get(u)/4)return 1;
					if(A.get(u)/4>B.get(u)/4)return -1;
					if(A.get(u)/4!=a.flags[0]&&A.get(u)/4!=a.flags[1])return 0;
					if(count>=1) return 0;
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

//		int count = 0;
//		for(int f = 0; f<13; f++){
//			if(f!=a.flags[0]){
//				if(a.nums[f]!= b.nums[f]){
//					return a.nums[f] - b.nums[f];
//				}
//				if(a.nums[f]>0)count += a.nums[f];
//				if(count>=2)return 0;
//
//			}
//		}
		Vector<Integer>A  = new Vector<Integer>(7);
		Vector<Integer>B  = new Vector<Integer>(7);
		for(int g: a.ID1)A.add(g);
		for(int h: b.ID1)B.add(h);
		Collections.sort(A);
		Collections.sort(B);
		int count = 0;
		for(int u =0; u<7;u++){
			if(A.get(u)/4<B.get(u)/4)return 1;
			if(A.get(u)/4>B.get(u)/4)return -1;
			if(A.get(u)/4!=a.flags[0])count++;
			if(count>=2) return 0;
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
			if(count>=5){
				return 0;
			}
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

		Vector<Integer>A  = new Vector<Integer>(7);
		Vector<Integer>B  = new Vector<Integer>(7);
		for(int g: a.ID1)A.add(g);
		for(int h: b.ID1)B.add(h);
		Collections.sort(A);
		Collections.sort(B);
		
		for(int u =0; u<7;u++){
			if(A.get(u)/4<B.get(u)/4)return 1;
			if(A.get(u)/4>B.get(u)/4)return -1;
			if(A.get(u)/4!=a.flags[0])return 0;
//			if(count>=3) return 0;
		}
//		return 0;
//		for(int x = 0; x<13; x++){
//			if(x!=a.flags[0]){
//				if(a.nums[x]>0&&b.nums[x]==0)return 1;
//				if(a.nums[x]==0&&b.nums[x]>0)return -1;
//				if(a.nums[x]>0) return 0;
////				if(a.nums[x]>b.nums[x])return 1;
////				if(a.nums[x]<b.nums[x])return -1;
////				if(a.nums[x]>0)return 0;
//			}
//			
//		}
		System.out.println("Error in Four of a Kind");
		return 0;
	}
}

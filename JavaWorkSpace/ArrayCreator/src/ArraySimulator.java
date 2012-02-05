

import java.util.Vector;

public class ArraySimulator {
	public static void main(String[] args){
		int x = 0;
		
		Vector<Integer> v = new Vector<Integer>();
		for (x = 0; x <= 51; x++)
			v.add(x);
		int[] primes = {41,37,31,29,23,19,17,13,11,7,5,3,2};
		int count = 0;
		
//		Array AR = new Array();
//		AR.createArray();
		TreeMapTest TM = new TreeMapTest();
	
System.out.println("Array Created");
double start = System.currentTimeMillis();
		while (v.size() >= 5) {
			int a = v.remove(0);

			for (int b = 0; b <= (v.size() - 4); b++) {
				for (int c = b + 1; c <= (v.size() - 3); c++) {

					for (int d = c + 1; d <= (v.size() - 2); d++) {
						for (int e = d + 1; e <= (v.size() - 1); e++) {
//							
							count++;
//							
							int search = primes[a/4]*primes[v.get(b)/4]*primes[v.get(c)/4]*primes[v.get(d)/4]*primes[v.get(e)/4];
							TM.search(search);
							if(search == 115856201){
								System.out.println("Halt");
							}
							
						}
					}

				}
			}

		}
		double finish = System.currentTimeMillis();

		System.out.println("Count = "+count+" Time taken = "
				+ ((finish - start) / 1000) + " sec");
		
		System.out.println(count);
	}
	static int num(int a){
		int output = 0;
		switch(a/4){
		case 0: output=41;
		break;
		case 1: output=37;
		break;
		case 2: output=31;
		break;
		case 3: output=29;
		break;
		case 4: output=23;
		break;
		case 5: output=19;
		break;
		case 6: output=17;
		break;
		case 7: output=13;
		break;
		case 8: output=11;
		break;
		case 9: output=7;
		break;
		case 10: output=5;
		break;
		case 11: output=3;
		break;
		case 12: output=2;
		break;
		
		}
		return output;
	}
}

public class TestMax2 {



	/**
	 * @param args
	 */
	public static void main(String[] args) {
		long[][] l = new long [5][5];
		long filler = 0;
		for(int a = 0; a<5; a++){
			for(int b = 0; b<5; b++){
				l[a][b] = filler++; 
			}
		}
		long start = System.currentTimeMillis();
		counter c = new counter();
		sharedMax t1 = new sharedMax(l[0], c);
		sharedMax t2 = new sharedMax(l[1], c);
		sharedMax t3 = new sharedMax(l[2], c);
		sharedMax t4 = new sharedMax(l[3], c);
		sharedMax t5 = new sharedMax(l[4], c);

		try{
			t1.t.join();
		
			t2.t.join();
		
			t3.t.join();
		
			t4.t.join();
		
			t5.t.join();
		


		}
		catch(InterruptedException e){

		}
	 long finish = System.currentTimeMillis();
		System.out.println("Total sum is "+c.max + " time taken "+(finish-start));


	}



}

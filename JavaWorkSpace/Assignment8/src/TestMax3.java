
public class TestMax3 {
	public static void main(String[] args) {
		long[][] l = new long [5][5];
		long filler = 0;
		for(int a = 0; a<5; a++){
			for(int b = 0; b<5; b++){
				l[a][b] = filler++; 
			}
		}
		long start = System.currentTimeMillis();
		atomicCounter c = new atomicCounter();
		atomicMax t1 = new atomicMax(l[0], c);
		atomicMax t2 = new atomicMax(l[1], c);
		atomicMax t3 = new atomicMax(l[2], c);
		atomicMax t4 = new atomicMax(l[3], c);
		atomicMax t5 = new atomicMax(l[4], c);

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
		System.out.println("Total max is "+c.max + " time taken "+(finish-start));


	}
}

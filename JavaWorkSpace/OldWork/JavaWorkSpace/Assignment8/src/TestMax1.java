
public class TestMax1 {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		long[][] l = new long [5][5];
		long filler = 0, max = 0;
		for(int a = 0; a<5; a++){
			for(int b = 0; b<5; b++){
				l[a][b] = filler++; 
			}
		}
		
		long start = System.currentTimeMillis();
		maxOf2DArray t1 = new maxOf2DArray(l[0]);
		maxOf2DArray t2 = new maxOf2DArray(l[1]);
		maxOf2DArray t3 = new maxOf2DArray(l[2]);
		maxOf2DArray t4 = new maxOf2DArray(l[3]);
		maxOf2DArray t5 = new maxOf2DArray(l[4]);
		
		try{
		t1.t.join();
		max = t1.max;
		t2.t.join();
		if(max<t2.max)max = t2.max;
		
		t3.t.join();
		if(max<t3.max)max = t3.max;
		t4.t.join();
		if(max<t4.max)max = t4.max;
		t5.t.join();
		if(max<t5.max)max = t5.max;
		
		
		}
		catch(InterruptedException e){
			
		}
		long finish = System.currentTimeMillis(); 
		System.out.println("Max sum is "+max+" time taken "+ (finish - start));
		
				
		
	}

}

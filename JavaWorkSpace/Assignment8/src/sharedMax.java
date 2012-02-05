
public class sharedMax implements Runnable {
	long [] array;
	Thread t;
	long max;
	TestMax2 test;
	counter c;

	public sharedMax(long[] arrayin, counter cin ){
		this.array = arrayin;
		t = new Thread(this);
		
		this.c = cin;
		t.start();


	}
	public void run() {
		max = array[0];
		for(long x: array){
			if(max<x){
				synchronized(c){
					max = c.max(x);
				}
				
			}
		}


	}}





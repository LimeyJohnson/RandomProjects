
public class atomicMax implements Runnable{
	long [] array;
	Thread t;
	long max;
	atomicCounter ac;
	
	public atomicMax(long[] arrayin, atomicCounter c){
		this.array = arrayin;
		this.ac = c;
		t = new Thread(this);
		
		t.start();
	
	}
	public void run() {
		max = array[0];
		for(long x: array){
			if(x>max){
				max = ac.max(x);
			}
		}
	}
}

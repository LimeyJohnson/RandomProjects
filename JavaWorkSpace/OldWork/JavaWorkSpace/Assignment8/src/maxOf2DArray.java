
public class maxOf2DArray implements Runnable{
	long [] array;
	Thread t;
	long max;
	
	public maxOf2DArray(long[] arrayin){
		this.array = arrayin;
		t = new Thread(this);
		
		t.start();
	
	}
	public void run() {
		max = array[0];
		for(long x: array){
			if(x>max) max = x;
		}
	}
	

}

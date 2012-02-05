import java.util.* ;
class threeNPlusOne {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		Scanner scanner = new Scanner(System.in);
		
		while(scanner.hasNextInt()){
			int a = scanner.nextInt();
			int b = scanner.nextInt();
			int max = 0;
			for(int c =a; c<= b;c++){
				int temp=rec(c,1);
				if(temp>max) max = temp;
			}
			System.out.println(a+" "+b+" "+max);
		}
	}
	static int rec(int x, int count){//start with count = 1
		if(x==1)return count;
		else if(x%2==0) return rec(x/2,count+1);
		else return rec((3*x)+1,count+1);
	}

}

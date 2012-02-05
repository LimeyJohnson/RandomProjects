import java.util.*;

public class TicTacToe {
	static int[] wins = {0,0,0};
	static boolean print = false;
	static double ties;
	public static void main(String[] args){
		double total = 0;
		double start = System.currentTimeMillis();
		for(int a = 0; a<9; a++){
			//total++;
			LinkedList<Integer> aList = getList(a,10,10,10,10,10,10,10,10);

			int [] aBoard = {0,0,0,0,0,0,0,0,0};
			aBoard[a] = 1;
			if(print) System.out.println("a = "+a);
			while(!aList.isEmpty()){
				//total+=2;
				int b=aList.remove();
				LinkedList<Integer> bList = getList(a,b,10,10,10,10,10,10,10);
				int[] bBoard= aBoard.clone();
				bBoard[b]=2;

				if(print) System.out.println("b = "+b);
				while(!bList.isEmpty()){
					//total+=3;
					int c = bList.remove();
					LinkedList<Integer> cList = getList(a,b,c,10,10,10,10,10,10);
					int[] cBoard = bBoard.clone();
					cBoard[c]=1;
					if(print) System.out.println("c = "+c);

					while(!cList.isEmpty()){
						//total+=4;
						int d = cList.remove();
						LinkedList<Integer> dList = getList(a,b,c,d,10,10,10,10,10);
						int[] dBoard = cBoard.clone();
						dBoard[d]= 2;
						if(print) System.out.println("d = "+d);
						count(isWin(dBoard));

						while(!dList.isEmpty()){
							//total+=5;
							int e = dList.remove();
							LinkedList<Integer> eList = getList(a,b,c,d,e,10,10,10,10);
							int[] eBoard = dBoard.clone();
							eBoard[e]=1;
							if(print) System.out.println("e = "+e);
							count(isWin(eBoard));
							if(isWin(eBoard)!=0)total+=5;
							while(!eList.isEmpty()&&isWin(eBoard)==0){
								//total+=6;

								int f = eList.remove();
								LinkedList<Integer> fList = getList(a,b,c,d,e,f,10,10,10);
								int[] fBoard = eBoard.clone();
								fBoard[f]=2;
								if(print) System.out.println("f = "+f);
								count(isWin(fBoard));
								if(isWin(fBoard)!=0)total+=6;
								while(!fList.isEmpty()&&isWin(fBoard)==0){
									//total+=7;
									int g = fList.remove();
									LinkedList<Integer> gList = getList(a,b,c,d,e,f,g,10,10);
									int[] gBoard = fBoard.clone();
									gBoard[g]=1;

									if(print) System.out.println("g = "+g);
									count(isWin(gBoard));
									if(isWin(gBoard)!=0)total+=7;
									while(!gList.isEmpty()&&isWin(gBoard)==0){
										//total+=8;
										int h = gList.remove();
										LinkedList<Integer> hList = getList(a,b,c,d,e,f,g,h,10);
										int[] hBoard = gBoard.clone();
										hBoard[h]=2;
										if(print) System.out.println("h = "+h);
										count(isWin(hBoard));
										if(isWin(hBoard)!=0)total+=8;
										while(!hList.isEmpty()&&isWin(hBoard)==0){
											//total+=9;
											int i = hList.remove();
											int[] iBoard = hBoard.clone();
											iBoard[i]=1;
											if(print) System.out.println("i = "+i);
											if(isWin(iBoard)==0){
												ties++;
												total+=9;
											}
											if(isWin(iBoard)!=0)total+=9;
											count(isWin(iBoard));
											if(print) System.out.println("i = "+i);
										}
									}
								}
							}

						}
					}



				}

			}
		}
		double finish = System.currentTimeMillis();

		System.out.println("x wins: "+wins[1]+" O wins: "+wins[2]+" ties "+ties);
		System.out.println("Total: "+total); 
		System.out.println("Total Time "+(finish-start)/1000);
	}

	public static LinkedList<Integer> getList(int a, int b, int c, int d, int e, int f, int g, int h, int i){
		LinkedList<Integer> temp = new LinkedList<Integer>();
		for(int x= 0; x<9; x++){
			temp.add(x);
		}
		if(a!=10)temp.remove(temp.indexOf(a));
		if(b!=10)temp.remove(temp.indexOf(b));
		if(c!=10)temp.remove(temp.indexOf(c));
		if(d!=10)temp.remove(temp.indexOf(d));
		if(e!=10)temp.remove(temp.indexOf(e));
		if(f!=10)temp.remove(temp.indexOf(f));
		if(g!=10)temp.remove(temp.indexOf(g));
		if(h!=10)temp.remove(temp.indexOf(h));
		if(i!=10)temp.remove(temp.indexOf(i));





		return temp;

	}
	public static int isWin(int a[]){
		if(((a[0]==a[3])&&(a[3]==a[6]))&&a[0]!=0){


			if(print) printWin(a); 
			return a[0];
		}
		if(((a[1]==a[4])&&(a[4]==a[7]))&&a[1]!=0){

			if(print) printWin(a); 
			return a[1];
		}
		if((a[2]==a[5])&&(a[5]==a[8])&&a[2]!=0){

			if(print) printWin(a); 
			return a[2];
		}
		if(((a[0]==a[1])&&(a[1]==a[2]))&&a[0]!=0){

			if(print) printWin(a); 
			return a[0];
		}
		if(((a[3]==a[4])&&(a[4]==a[5]))&&a[3]!=0){

			if(print) printWin(a); 
			return a[3];
		}
		if(((a[6]==a[7])&&(a[7]==a[8]))&&a[6]!=0){

			if(print) printWin(a); 
			return a[6];
		}
		if(((a[0]==a[4])&&(a[4]==a[8]))&&a[0]!=0){

			if(print) printWin(a); 
			return a[0];
		}
		if(((a[2]==a[4])&&(a[4]==a[6]))&&a[2]!=0){

			if(print) printWin(a); 
			return a[2];
		}
		return 0;






	}
	public static void printWin(int a[]){
		System.out.print("Win! : ");
		for(int x=0; x<9; x++){
			System.out.print(x+" = "+a[x]+",");
		}
		System.out.println("");
	}
	public static void count(int x){
		if(x==1){
			wins[1]++;

		}
		if(x==2){
			wins[2]++;
		}
	}

}

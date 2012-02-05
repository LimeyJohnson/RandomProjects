
public class SafeAccount {
	int balance;
	int n;
	public SafeAccount(int initn){
		n = initn;
	}
	public synchronized boolean credit(int amount){//returns true if the credit worked, false if it failed
		if(balance>n) return false;
		balance += amount;
		return true;
	}
	public synchronized void debit(int amount){
		while(balance<amount);
		balance -= amount;
	}
}

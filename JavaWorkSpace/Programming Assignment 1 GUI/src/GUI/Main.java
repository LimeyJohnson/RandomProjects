package GUI;

import javax.swing.JFrame;

public class Main {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		GameFrame gf = new GameFrame();
		gf.setSize(400, 600);
		gf.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		gf.setVisible(true);
		gf.setTitle("Andrew's Game Tracker");
		

	}

}

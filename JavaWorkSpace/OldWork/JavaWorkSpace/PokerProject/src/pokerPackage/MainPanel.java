package pokerPackage;
import java.awt.*;
import java.awt.event.*;

import javax.swing.*;

public class MainPanel extends JPanel implements ActionListener {
	static final long serialVersionUID = 12;
	JPanel top = new JPanel();
	JPanel bottom = new JPanel();

	PokerFrame frame;
	JButton[] buttons = new JButton[4];

	JButton board = new JButton("Board");

	JButton eval = new JButton("Evaluate");
	JTextField results = new JTextField();
	public MainPanel(PokerFrame framex){
		frame = framex;
		for(int a = 0; a<4;a++){
			JButton temp = new JButton("Hand "+(a+1));
			temp.addActionListener(this);
			temp.setActionCommand(String.valueOf(a));
			top.add(temp);
			buttons[a] = temp;
		}

		board.addActionListener(this);
		eval.addActionListener(this);
		top.setLayout(new GridLayout(6,1));

		top.add(board);
		top.add(eval);
		
		setLayout(new GridLayout(2,1));
		add(top);
		add(results);
	}
	public void actionPerformed(ActionEvent evt){
		String ac = evt.getActionCommand();
		if(ac.equals("0")||ac.equals("1")||ac.equals("2")||ac.equals("3")){
			frame.addHand(Integer.parseInt(ac), 2);
		}
		if(evt.getSource()==board){
			frame.addBoard();
		}
	}
	public void setEval(String s){
		eval.setText(s);
	}
	public void setHand(String s,int hand){


	}

}

package pokerPackage;

import java.awt.*;
import java.awt.event.*;
import java.util.*;
import javax.swing.*;


public class InputPanel extends JPanel implements ActionListener{
	final static long serialVersionUID = 12;
	PokerFrame frame;
	JLabel label = new JLabel("Select Cards");
	JPanel buttons = new JPanel();
	JButton done = new JButton("Done");
	JPanel bottom = new JPanel();
	Color gray = new Color(200,200,200);
	Color white = new Color(255,255,255);
	JButton[] cArray = new JButton[52];

	int hand;
	int numCards;
	LinkedList<Integer> selected = new LinkedList<Integer>();
	public InputPanel(PokerFrame framex, int handx, int numCardsx){
		frame = framex;
		hand = handx;
		numCards = numCardsx;
		buttons.setLayout(new GridLayout(13,4));
		for(int a = 0; a<52; a++){
			JButton temp = new JButton();
			//temp.setText(frame.comp.getCard(a)+frame.comp.getSuit(a));
			cArray[a] = temp;
			temp.addActionListener(this);
			temp.setActionCommand(String.valueOf(a));
			temp.setBackground(gray);
			buttons.add(temp);

		}
		done.addActionListener(this);
		bottom.setLayout(new GridLayout(2,1));
		bottom.add(done);
		bottom.add(label);

		setLayout(new BorderLayout());
		add(buttons,BorderLayout.CENTER);
		add(bottom,BorderLayout.SOUTH);
	}

	public void actionPerformed(ActionEvent evt){
		if(evt.getSource() == done){
			//if(selected.size()>numCards)label.setText("Please select up to"+numCards+" Cards");
			/*else*/ 
			if(hand<4)frame.handDone(selected, hand);
			else if(hand==4)frame.boardDone(selected);


		}
		else{ 
			int ID = Integer.parseInt(evt.getActionCommand());
			if(selected.indexOf(ID)>=0)cArray[selected.remove(selected.indexOf(ID))].setBackground(gray);
			else if(selected.size()<numCards){
				cArray[ID].setBackground(white);
				selected.add(ID);
//				while(selected.size()>numCards){
//					cArray[selected.remove()].setBackground(gray);
//				}


			}
			label.setText(selected.size()+" card(s) selected");

		}
		System.out.println(selected.size());
	}


}

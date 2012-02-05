package pokerPackage;
import javax.swing.*;


import java.util.*;

public class PokerFrame extends JFrame implements SwingConstants{

	final static long serialVersionUID = 12;
	MainPanel main = new MainPanel(this);
	InputPanel[] input= new InputPanel[4];
	InputPanel board; 
	int[][] hands = {{100,100},{100,100},{100,100},{100,100}};
	Vector<Integer> boardID;
	Hand comp = new Hand();


	public PokerFrame(){
		board = new InputPanel(this,4,5);
		for(int a=0;a<4;a++){
			InputPanel temp = new InputPanel(this,a,2);
			input[a] = temp;
		}
		boardID = new Vector<Integer>();
		add(main);


	}


	public void addHand(int x, int numCards){
		input[x].setVisible(false);
		if(hands[x][0]>=0&&hands[x][0]<=51){
			while(!input[x].selected.isEmpty()){
				input[x].selected.remove();
			}
			input[x].selected.add(hands[x][0]);
			input[x].selected.add(hands[x][1]);
		}
		main.setVisible(false);

		remove(main);
		add(input[x]);
		input[x].setVisible(true);
		validate();

	}
	public void addBoard(){
		if(!boardID.isEmpty()){
			while(!board.selected.isEmpty()){
				board.selected.remove();
			}
			for(int a=0;a<boardID.size();a++)board.selected.add(boardID.get(a));
		}
		board.setVisible(false);
		main.setVisible(false);
		remove(main);
		add(board);
		board.setVisible(true);
		validate();
	}
	public void handDone(LinkedList<Integer> selected, int hand){

		if(!selected.isEmpty()){
			hands[hand][0]=selected.remove();
			hands[hand][1]=selected.remove();
		//	main.buttons[hand].setText("Hand "+hand+" = "+comp.getCard(hands[hand][0])+comp.getSuit(hands[hand][0])+","+comp.getCard(hands[hand][1])+comp.getSuit(hands[hand][1]));
		}
		else main.buttons[hand].setText("Hand "+(hand+1));


		input[hand].setVisible(false);
		remove(input[hand]);
		add(main);
		main.setVisible(true);
		validate();


	}
	public void boardDone(LinkedList<Integer> selected){
		String s = "change method";
//		if(hand==4){
		if(!selected.isEmpty()){
			boardID.removeAllElements();
			int c = selected.remove();
			boardID.add(c);
		//	s = "Board = "+comp.getCard(c)+comp.getSuit(c);

			

			while(!selected.isEmpty()){
				c = selected.remove();
			//	s = s+","+comp.getCard(c)+comp.getSuit(c);
				boardID.add(c);
			}
			main.board.setText(s);
			Hand cTemp = new Hand(boardID);
			cTemp.calculateHand();
		}
		else main.board.setText("Board");
		board.setVisible(false);
		main.setVisible(false);
		remove(board);
		add(main);
		main.setVisible(true);
		validate();
	}



}





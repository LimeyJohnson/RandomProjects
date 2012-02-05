package GUI;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.*;
import javax.swing.border.LineBorder;

public class GameFrame extends JFrame implements ActionListener{ //JFrame for GUI with actionlistener for all action buttons;
	//Global Variables for ActionEvents
	DataManager dataManager;
	JButton userReportButton, gameReportButton, userList, stats, fileButton;
	JComboBox userReportBox, gameReportBox;
	JTextArea resultsTextArea;
	JTextField fileTextField;
	Boolean DataImported;
	
	public GameFrame(){//Initialize GUI Components overall Layout will be GridLayout 2,1
	
		setLayout(new GridLayout(2,1));
		
		DataImported = false;
		
		//Upper Half of Frame
		JPanel upperPanel = new JPanel();
		upperPanel.setLayout(new BorderLayout());

		//Panel at top of GUI for file input information
		JPanel filePanel = new JPanel();
		filePanel.setLayout(new BorderLayout());
		
		fileTextField = new JTextField();
		fileTextField.addActionListener(this);
		filePanel.add(fileTextField, BorderLayout.CENTER);
		
		fileButton = new JButton("Add File");
		fileButton.addActionListener(this);
		filePanel.add(fileButton,BorderLayout.EAST);

		//Panel just above middle for inputting query data
		JPanel queryPanel = new JPanel();
		queryPanel.setLayout(new GridLayout(1,3));
		
		//Panel in QueryPanel for User Data
		JPanel upperLeft = new JPanel();
		upperLeft.setLayout(new GridLayout(2,1));
		upperLeft.setBorder(new LineBorder(Color.BLACK,2));
		userReportBox = new JComboBox();
		userReportButton = new JButton("Run Report");
		userReportButton.addActionListener(this);
		upperLeft.add(userReportBox);
		upperLeft.add(userReportButton);

		//Panel in QueryPanel for Game Data
		JPanel upperMid = new JPanel();
		upperMid.setLayout(new GridLayout(2,1));
		upperMid.setBorder(new LineBorder(Color.BLACK,2));
		gameReportBox = new JComboBox();
		gameReportButton = new JButton("Run Report");
		gameReportButton.addActionListener(this);
		upperMid.add(gameReportBox);
		upperMid.add(gameReportButton);

		//Panel in QueryPanel for Other Stats Boxes
		JPanel upperRight = new JPanel();
		upperRight.setLayout(new GridLayout(2,1));
		userList = new JButton("All Users");
		userList.setBorder(new LineBorder(Color.BLACK,2));
		userList.addActionListener(this);

		stats = new JButton("Stats");
		stats.setBorder(new LineBorder(Color.BLACK,2));
		stats.addActionListener(this);

		upperRight.add(userList);
		upperRight.add(stats);

		queryPanel.add(upperLeft);
		queryPanel.add(upperMid);
		queryPanel.add(upperRight);


		upperPanel.add(queryPanel,BorderLayout.CENTER);
		upperPanel.add(filePanel,BorderLayout.NORTH);

		resultsTextArea = new JTextArea();
		resultsTextArea.setText("Specify your file in the textbox above");
		//add main panels to JFrame
		add(upperPanel);
		add(resultsTextArea);
	}
	public void actionPerformed(ActionEvent evt) { //Display selected data based on the button pressed
		
		if(evt.getSource() == fileButton||evt.getSource()==fileTextField){//input file information into Datamanager and display result
				dataManager = new DataManager(fileTextField.getText());
				gameReportBox.removeAllItems();
				for(Game g: dataManager.vGames)gameReportBox.addItem(g);
				userReportBox.removeAllItems();
				for(User u: dataManager.vUsers)userReportBox.addItem(u);
				resultsTextArea.setText(dataManager.completeMessage);
				DataImported = dataManager.complete;
			
		}
		else if(DataImported){
			if(evt.getSource()==userReportButton){//show user report based on user selected
				User u = (User) userReportBox.getSelectedItem();
				resultsTextArea.setText(dataManager.userReport(u));
			}
			else if(evt.getSource()==gameReportButton){//show game report based on game selected
				Game g = (Game) gameReportBox.getSelectedItem();
				resultsTextArea.setText(dataManager.gameReport(g));
			}
			else if(evt.getSource() == userList){ // show all users and games
				resultsTextArea.setText(dataManager.smallReport());
			}
			else if(evt.getSource() == stats){//show games statistics
				resultsTextArea.setText(dataManager.fullReport());
			}
		}
		else{
			resultsTextArea.setText("Please Import Data First");
		}
		
	}

}

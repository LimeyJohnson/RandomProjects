package App;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;
import javax.swing.JPanel;
import javax.swing.JTextArea;

import java.util.ArrayList;

public class DisplayScreen implements ActionListener,  Screen
{
	private ArrayList<String> singleEntries;
	private ArrayList<String> list;
	FlightApp flightApp;
	private JPanel pane;
	private int pageNumber = 1; //of the list
	int formatWidth = 100; //length of textbox
	//buttons for the entries in the list
	JButton listButton1;
	JButton listButton2;
	JButton listButton3;
	JButton listButton4;
	JButton listButton5;
	//Scroll forward or backward in list
	JButton nextButton;
	JButton previousButton;
	//What type of list this is
	String listType;
	int screenWidth = 200;
	int screenHeight = 100;
	//amount to increase the screen height for each single entry
	int singleEntryHeight = 20;
	//amount to increase the screen height for each list entry
	int listEntryHeight = 30;
	/**
	 * displays information
	 * @param flightApp
	 * @param singleEntries Single text strings that print out before the list 
	 * @param list list of some object (the key ex Type "Passsenger" in list "tomr13" where tomr13 is a passenger ID 
	 * @param listType type of the objects in the list
	 */
	public DisplayScreen(FlightApp flightApp, ArrayList<String> singleEntries, 
			 ArrayList<String> list, String listType)
	{
		this.listType = listType;
		pane = new JPanel();
		nextButton = new JButton("Next");
		nextButton.setActionCommand("Next");
		nextButton.addActionListener(this);
			
		previousButton = new JButton("Previous");
			
		previousButton.setActionCommand("Previous");
		previousButton.addActionListener(this);
		JButton ReturnB;
		if(!((listType.equals("CityIn") || listType.equals("CityOut"))))
		{	
			ReturnB = new JButton("Return to Main Menu");
			ReturnB.setActionCommand("Return to Main Menu");
		}
		else
		{
			ReturnB = new JButton("Return to City Menu");
			ReturnB.setActionCommand("Return to City Menu");
		}
		ReturnB.addActionListener(this);
		pane.add(ReturnB);
		
		this.flightApp = flightApp;
		this.singleEntries = singleEntries;
		this.list = list;
		if(singleEntries != null)
		{
			for(String s : singleEntries)
			{
				pane.add(new JTextArea(makeLength(s,formatWidth)));
				screenHeight+=singleEntryHeight;
			}
		}
		pane.add(previousButton);
		pane.add(nextButton);
		listButton1 = new JButton("");
		listButton2 = new JButton("");
		listButton3 = new JButton("");
		listButton4 = new JButton("");
		listButton5 = new JButton("");
		
		
		if(list != null)
		{
			screenHeight+=listEntryHeight * 5;
			pane.add(listButton1);
			pane.add(listButton2);
			pane.add(listButton3);
			pane.add(listButton4);
			pane.add(listButton5);
			updateList();
		}
		flightApp.frame.setSize(screenWidth,screenHeight);
		flightApp.frame.add(pane);
		flightApp.frame.setResizable(false);
		flightApp.frame.setLocationRelativeTo(null);
		flightApp.frame.setVisible(true);
	}
	/**
	 * makes string s; l length
	 */
	public String makeLength(String s, int l)
	{
		String s2 = s;
		while(s2.length()<l)
		{ s2 = " " + s2+" "; }
		s2.substring(0,l);
		return s2;
	}
	public void dispose()
	{
		flightApp.frame.remove(pane);
	}
	/**
	 * refresh the list
	 */
	public void updateList()
	{
		//update the next/previous button
		if(list.size() <= pageNumber * 5)
			nextButton.setEnabled(false);
		else
			nextButton.setEnabled(true);
		if(pageNumber==1)
			previousButton.setEnabled(false);
		else
			previousButton.setEnabled(true);
		//only display if it exists
		if(list.size() > (pageNumber*5-5))
		{
			listButton1.setVisible(true);
			listButton1.setText(makeLength(list.get(pageNumber*5-5),formatWidth));
			listButton1.setActionCommand(list.get(pageNumber*5-5)+"Q");
			listButton1.addActionListener(this);
		}
		else
			listButton1.setVisible(false);
		if(list.size() > (pageNumber*5-4))
		{
			listButton2.setVisible(true);
			listButton2.setText(makeLength(list.get(pageNumber*5-4),formatWidth));
			listButton2.setActionCommand(list.get(pageNumber*5-4)+"Q");
			listButton2.addActionListener(this);
		}
		else
			listButton2.setVisible(false);
		if(list.size() > (pageNumber*5-3))
		{
			listButton3.setVisible(true);
			listButton3.setText(makeLength(list.get(pageNumber*5-3),formatWidth));
			listButton3.setActionCommand(list.get(pageNumber*5-3)+"Q");
			listButton3.addActionListener(this);
		}
		else
			listButton3.setVisible(false);
		if(list.size() > (pageNumber*5-2))
		{
			listButton4.setVisible(true);
			listButton4.setText(makeLength(list.get(pageNumber*5-2),formatWidth));
			listButton4.setActionCommand(list.get(pageNumber*5-2)+"Q");
			listButton4.addActionListener(this);
		}
		else
			listButton4.setVisible(false);
		if(list.size() > (pageNumber*5-1))
		{
			listButton5.setVisible(true);
			listButton5.setText(makeLength(list.get(pageNumber*5-1),formatWidth));
			listButton5.setActionCommand(list.get(pageNumber*5-1)+"Q");
			listButton5.addActionListener(this);
		}
		else
			listButton5.setVisible(false);
	}
	@Override
	public void actionPerformed(ActionEvent e) {
		boolean buttonSelected = false;
		if(list!= null)
		{
			for(String s : list)
			{
				//"Q" added so doesn't conflict with other action commands (no passenger named "Next")
				String b = s+"Q";
				if(b.equals(e.getActionCommand()))
				{
					buttonSelected = true;
					System.out.println(s+ " Selected");
					if(listType.equals("Passenger"))
						flightApp.changeScreen(new DisplayScreen(flightApp,flightApp.ShowPassenger(s),
										flightApp.getPassenger(s).Flights(),"Flight"));
					else if(listType.equals("Flight"))
						flightApp.changeScreen(new DisplayScreen(flightApp,
								flightApp.ShowFlight(s),
								flightApp.getFlight(s).Passengers(),"Passenger"));
					else if(listType.equals("City"))
						flightApp.changeScreen(new ShowCityScreen(flightApp,s));
					else if(listType.equals("CityOut"))
						flightApp.changeScreen(new DisplayScreen(flightApp,
								flightApp.ShowCity(s),
								flightApp.getCity(s).Out(),"City"));
					else if(listType.equals("CityInto"))
						flightApp.changeScreen(new DisplayScreen(flightApp,
								flightApp.ShowCity(s),
								flightApp.getCity(s).Into(),"City"));
					else
						System.out.println(listType+": invalid listType");
				}
			}
		}
		if ("Return to Main Menu".equals(e.getActionCommand())) {
			flightApp.changeScreen(new MenuScreen(flightApp));
			System.out.println("Returning to Main Menu...");	
	    }
		if ("Return to City Menu".equals(e.getActionCommand())) {
			flightApp.changeScreen(new ShowCityScreen(flightApp,singleEntries.get(0)));
			System.out.println("Returning to City Menu...");	
	    }
		else if("Previous".equals(e.getActionCommand())){
			System.out.println("Scrolling backward in game list..");
			pageNumber--;
			updateList();
	    }
		else if("Next".equals(e.getActionCommand())){
			System.out.println("Scrolling foreward in game list..");
			pageNumber++;
			updateList();
	    }
		else if(!buttonSelected)
		{System.out.println("Error: Button Action Not Defined");}
	}
}
package App;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;
import javax.swing.JTextField;

import java.util.ArrayList;
import javax.swing.*;
/**
 * the main menu
 */
public class MenuScreen implements ActionListener, Screen
{
	private JTextField text1;
	private JButton button1; //search for flight
	private JButton button2; //search for passenger
	private JButton button3; //search for flights into city
	private JButton button4; //search for flights out of city
	private JButton button5; //list all flights
	private JButton button6; //list all passengers
	private JButton button7; //list all cities
	private JButton button8; //add passenger
	private JButton button9; //add flight
	private JButton button10; //add ticket
	private JButton button11; //search for city
	JPanel pane;
	FlightApp flightApp;
	int screenWidth = 290;
	int screenHeight = 310;
	public MenuScreen(FlightApp flightApp){
		this.flightApp = flightApp;
		pane = new JPanel();
		button1 = new JButton("Search for Flight");
		button1.setActionCommand("Search for Flight");
		
		button2 = new JButton("Search for Passenger");
		button2.setActionCommand("Search for Passenger");
		
		button3 = new JButton("Search Flights Into City");
		button3.setActionCommand("Search Flights Into City");
		
		button4 = new JButton("Search Flights Out Of City");
		button4.setActionCommand("Search Flights Out Of City");
		
		button5 = new JButton("List All Flights");
		button5.setActionCommand("List all Flights");
		
		button6 = new JButton("List All Passengers");
		button6.setActionCommand("List all Passengers");
		
		button7 = new JButton("List All Cities");
		button7.setActionCommand("List all Cities");

		button8 = new JButton("Add Passenger");
		button8.setActionCommand("Add Passenger");
		
		button9 = new JButton("Add Flight");
		button9.setActionCommand("Add Flight");
		
		button10 = new JButton("Add Ticket");
		button10.setActionCommand("Add Ticket");
		
		button11 = new JButton("Search for City");
		button11.setActionCommand("Search for City");
		
		text1 = new JTextField(20);
		
		pane.add(text1);
		
		pane.add(button1); 
		pane.add(button2);
		pane.add(button11);
		pane.add(button3);
		pane.add(button4);
		pane.add(button5);
		pane.add(button6);
		pane.add(button7);
		pane.add(button8);
		pane.add(button9);
		pane.add(button10);
		

		button1.addActionListener(this);
		button2.addActionListener(this);
		button3.addActionListener(this);
		button4.addActionListener(this);
		button5.addActionListener(this);
		button6.addActionListener(this);
		button7.addActionListener(this);
		button8.addActionListener(this);
		button9.addActionListener(this);
		button10.addActionListener(this);
		button11.addActionListener(this);
		
		text1.addActionListener(this);
		flightApp.frame.setSize(screenWidth,screenHeight);
		flightApp.frame.setResizable(false);
		flightApp.frame.setLocationRelativeTo(null);
		flightApp.frame.add(pane);
	}
	public void dispose()
	{
		flightApp.frame.remove(pane);
	}
	@Override
	public void actionPerformed(ActionEvent e) {
		if ("Search for Passenger".equals(e.getActionCommand())) {
			try{
				System.out.println("Searching for Passenger...");
				if(flightApp.getPassenger(text1.getText())!=null)
					flightApp.changeScreen(new DisplayScreen(flightApp,
							flightApp.ShowPassenger(text1.getText()),
							flightApp.getPassenger(text1.getText()).Flights(),
			    			"Flight"));
			}
			catch(Exception except)
			{
				flightApp.changeScreen(new DisplayScreen(flightApp,null,null,""));
			}
	    } 
		else if("Search for Flight".equals(e.getActionCommand())){
			try{
				if(flightApp.getFlight(text1.getText())!=null)
					flightApp.changeScreen(new DisplayScreen(flightApp,
							flightApp.ShowFlight(text1.getText()),
							flightApp.getFlight(text1.getText()).Passengers(),
		    				"Passenger"));
			}
			catch(Exception except)
			{
				flightApp.changeScreen(new DisplayScreen(flightApp, null,null,""));
			}
			System.out.println("Searching for Flights...");
			//flightApp.changeScreen(new ShowFlightScreen(flightApp,text1.getText()));
	    }
		else if("Search for City".equals(e.getActionCommand())){
			if(flightApp.getCity(text1.getText())!=null)
				flightApp.changeScreen(new ShowCityScreen(flightApp,
						text1.getText()));
			System.out.println("Searching for Flights...");
			//flightApp.changeScreen(new ShowFlightScreen(flightApp,text1.getText()));
	    }
		else if("Search Flights Into City".equals(e.getActionCommand())){
			System.out.println("Searching Flights Into City...");
			ArrayList<String> cityName = new ArrayList<String>();
			cityName.add(text1.getText());
			flightApp.changeScreen(new DisplayScreen(flightApp,
					cityName,
					flightApp.citiesIntoList(text1.getText()),
	    			""));
	    }
		else if("Search Flights Out Of City".equals(e.getActionCommand())){
			System.out.println("Searching Flights Out Of City...");
			ArrayList<String> cityName = new ArrayList<String>();
			cityName.add(text1.getText());
			flightApp.changeScreen(new DisplayScreen(flightApp,
					cityName,
					flightApp.citiesOutOfList(text1.getText()),
	    			""));
	    }
		else if("List all Passengers".equals(e.getActionCommand())){
			System.out.println("Listing All Passengers...");
			flightApp.changeScreen(new DisplayScreen(flightApp,
					null,
					flightApp.passengerList(),
	    			"Passenger"));
			//flightApp.changeScreen(new ListAllPassengersScreen(flightApp));
	    }
		else if("List all Flights".equals(e.getActionCommand())){
			System.out.println("Listing All Flights...");
			flightApp.changeScreen(new DisplayScreen(flightApp,
					null,
					flightApp.flightList(),
	    			"Flight"));
	    }
		else if("List all Cities".equals(e.getActionCommand())){
			System.out.println("Listing All Cities...");
			flightApp.changeScreen(new DisplayScreen(flightApp,
					null,
					flightApp.cityList(),
	    			"City"));
	    }
		else if("Add Passenger".equals(e.getActionCommand())){
			System.out.println("Adding Passenger");
			flightApp.changeScreen(new AddDataScreen(flightApp, flightApp.addingPassengerText(),"Passenger"));
		}
		else if("Add Flight".equals(e.getActionCommand())){
			System.out.println("Adding Flight");
			flightApp.changeScreen(new AddDataScreen(flightApp, flightApp.addingFlightText(),"Flight"));
		}
		else if("Add Ticket".equals(e.getActionCommand())){
			System.out.println("Adding Ticket");
			flightApp.changeScreen(new AddDataScreen(flightApp, flightApp.addingTicketText(),"Ticket"));
		}
		else
		{System.out.println("Error: Button Action Not Defined");}
	}
}
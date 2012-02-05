package App;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JButton;

import java.util.ArrayList;
import javax.swing.*;
/**
 * Screen that allows you to pick if you want to see flights going into or going out of
 */
public class ShowCityScreen implements ActionListener,  Screen
{
	private JTextArea textArea1; //name of city
	
	private JButton button1; //return button
	private JButton button2; //into
	private JButton button3; //out of

	private FlightApp flightApp;
	private JPanel pane;
	//length of display text boxes
	int formatLength = 100;
	String name; // of this city
	int screenWidth = 400;
	int screenHeight = 120;
	public ShowCityScreen(FlightApp gp, String CityName){
		flightApp = gp;
		name = CityName;
		textArea1 = new JTextArea(makeLength(name,formatLength));
		
		button1 = new JButton("Return to Main Menu");
		button1.setActionCommand("Return to Main Menu");
		button1.addActionListener(this);
		pane = new JPanel();
		pane.add(button1);
		if(flightApp.getCity(name)!= null)
		{
			button2 = new JButton("List Cities to City");
			button2.setActionCommand("List Flights Into City");
			button2.addActionListener(this);
				
			button3 = new JButton("List Cities from City");
			
			button3.setActionCommand("List Flights Out Of City");
			button3.addActionListener(this);
			pane.add(textArea1);
			pane.add(button3);
			pane.add(button2);
				
			flightApp.frame.setSize(screenWidth,screenHeight);
		}
		else
		{
			System.out.println("City Not Found");
			textArea1 = new JTextArea("City Not Found");
			pane.add(textArea1);
			flightApp.frame.setSize(170,90);
		}
		flightApp.frame.add(pane);
		flightApp.frame.setResizable(false);
		flightApp.frame.setLocationRelativeTo(null);
		flightApp.frame.setVisible(true);
	}
	/**
	 * make string s, l length
	 */
	public String makeLength(String s, int l)
	{
		String s2 = s;
		while(s2.length()<l)
		{ s2 = " " + s2+" "; }
		s2.substring(0,l);
		return s2;
	}
	/**
	 * clear the screen
	 */
	public void dispose()
	{
		flightApp.frame.remove(pane);
	}
	@Override
	public void actionPerformed(ActionEvent e) {
		if ("Return to Main Menu".equals(e.getActionCommand())) {
			flightApp.changeScreen(new MenuScreen(flightApp));
			System.out.println("Returning to Main Menu...");	
	    } 
		else if("List Flights Into City".equals(e.getActionCommand())){
			ArrayList<String> cityName = new ArrayList<String>();
			cityName.add(name);
			flightApp.changeScreen(new DisplayScreen(flightApp,
					cityName,
					flightApp.citiesIntoList(name),
	    			"City"));
			System.out.println("Showing flights into city...");
	    }
		else if("List Flights Out Of City".equals(e.getActionCommand())){
			ArrayList<String> cityName = new ArrayList<String>();
			cityName.add(name);
			flightApp.changeScreen(new DisplayScreen(flightApp,
					cityName,
					flightApp.citiesOutOfList(name),
	    			"City"));
			System.out.println("Showing flights out of city...");
	    }
		else
		{System.out.println("Error: Button Action Not Defined");}
	}
}
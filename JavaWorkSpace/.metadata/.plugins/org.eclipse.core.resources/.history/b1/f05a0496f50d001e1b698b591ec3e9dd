package App;
import java.awt.event.*;
import java.util.ArrayList;
import javax.swing.*;

public class FlightApp implements ActionListener
{
	private Screen currentScreen;
	public JFrame frame;
	private Airline airline;
	public ArrayList<String> citiesIntoList(String city)
	{
		return airline.citiesIntoList(city);
	}
	public City getCity(String name)
	{
		return airline.getCity(name);
	}
	public ArrayList<String> citiesOutOfList(String city)
	{
		return airline.citiesOutOfList(city);
	}
	public Passenger getPassenger(String passengerName)
	{
		return airline.getPassenger(passengerName);
	}
	public ArrayList<String> cityList()
	{
		return airline.cityList();
	}
    public Flight getFlight(String flightName)
	{
    	return airline.getFlight(flightName);
	}
	public ArrayList<String> flightList()
	{
		return airline.flightList();
	}
	public ArrayList<String> passengerList(String flightName)
	{
		return airline.passengerList(flightName);
	}
	public ArrayList<String> passengerList()
	{
		return airline.passengerList();
	}
	public void addTicket(ArrayList<String> entries)
	{
		airline.addTicket(entries);
	}
	public void addPassenger(ArrayList<String> entries)
	{
		airline.addPassenger(entries);
	}
	public void addFlight(ArrayList<String> entries)
	{
		airline.addFlight(entries);
	}
	public ArrayList<String> addingTicketText()
	{
		ArrayList<String> text = new ArrayList<String>();
		text.add("Flight Number");
		text.add("Passenger ID");
		return text;
	}
	public ArrayList<String> addingPassengerText()
	{
		//{"FirstName", "LastName", "Miles", "Address", "PassengerID"}
		ArrayList<String> text = new ArrayList<String>();
		text.add("First Name");
		text.add("Last Name");
		text.add("Flyer Miles");
		text.add("Address");
		text.add("Passenger ID");	
		return text;
	}
	public ArrayList<String> addingFlightText()
	{
		//{"FlightNum", "Date", "Plane","MaxPassenger","StartCity","EndCity"};
		ArrayList<String> text = new ArrayList<String>();
		text.add("Flight Number");
		text.add("Date");
		text.add("Plane");
		text.add("Max Passengers");
		text.add("Start City");
		text.add("Ending City");
		return text;
	}
	public ArrayList<String> addFlight()
	{
		ArrayList<String> Ara = new ArrayList<String>();
		return Ara;
	}
	public ArrayList<String> ShowPassenger(String id)
	{
		ArrayList<String> list = new ArrayList<String>();
		Passenger p = airline.getPassenger(id);
		list.add(p.FirstName()+ " " + p.LastName());
		list.add("Address: " + p.Address());
		list.add("Flyer Miles: " + p.FlyerMiles());
		return list;
	}
	public ArrayList<String> ShowCity(String name)
	{
		ArrayList<String> list = new ArrayList<String>();
		City c = airline.getCity(name);
		return list;
	}
	public ArrayList<String> ShowFlight(String flightNum)
	{
		ArrayList<String> list = new ArrayList<String>();
		Flight f = airline.getFlight(flightNum);
		System.out.println(flightNum);
		list.add("Flight " + f.FlightNum());
		list.add("Plane  " + f.PlaneNumber());
		list.add("Take Off:  " + f.Date());
		list.add("From " + f.StartCity() + " To "+f.EndCity());
		list.add(f.CurrentNumPassengers()+"/"+f.MaxNumPassengers()
				+" passengers");
		return list;
	}
	public Screen currentScreen()
	{	
		return currentScreen; 
	}
	public FlightApp(Airline airline)
	{
		this.airline = airline;
		currentScreen = null;
		startApp();
	}
	public void startApp()
	{
		frame = new JFrame("The Epic Flight Application(TEFA)");
		frame.setLocationRelativeTo(null);
		frame.setSize(500,500);
		frame.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
		frame.setVisible(true);
		changeScreen(new MenuScreen(this));
	}
	public void changeScreen(Screen s)
	{
		if(currentScreen != null)
			currentScreen.dispose();
		frame.setVisible(true);
		currentScreen = s;
	}
	@Override
	public void actionPerformed(ActionEvent e) {
		JOptionPane.showMessageDialog(null,"Press the button!");
	}
}
package App;
import java.util.*;

import TupleObjects.TupleObject;
import DatabaseObjects.Database;
import DatabaseObjects.Table;
import DatabaseObjects.Tuple;
public class Airline {
	Flight flight;
	Passenger passenger;
	City city;
	public Database database;
	public Airline()
	{
		database = new Database("Airline");
		createTables();
		testDatabase();	
	}
	/**
	 * creates the tables for the flight application
	 */
	public void createTables()
	{
		String[] flightTblNames = {"FlightNum", "Date", "Plane","MaxPassenger","StartCity","EndCity"};
		database.createTable("Flights", flightTblNames);
		String[] passengerTblNames = {"FirstName", "LastName", "Miles", "Address", "PassengerID"};
		database.createTable("Passengers", passengerTblNames);
		String[] ticketTblNames = {"FlightNum", "PassengerID"};
		database.createTable("Tickets", ticketTblNames);
	}
	/**
	 * creates starting data for the application
	 */
	private void testDatabase(){
		String[] data = {"234123f", "1-1-01", "32421p","42","San Antonio","New York"};
		String[] data1 = {"141123f", "5-5-51", "331421p","2","New York","Little Rock"};
		String[] data2 = {"654123f", "2-5-01", "3244521p","61","San Antonio","Little Rock"};
		String[] data3 = {"23123f", "5-3-01", "32411p","32","Little Rock","San Antonio"};
		String[] data4 = {"2341123f", "5-2-01", "32411p","32","Little Rock","San Antonio"};
		String[] data5 = {"2312433f", "5-1-01", "321411p","32","Austin","San Antonio"};
		String[] data6 = {"23123411f", "5-5-01", "32211p","32","Little Rock","Austin"};
		String[] data7 = {"23f", "1-2-01", "321p","425","A Farm","A City"};
		database.insertQuery("Flights", data);
		database.insertQuery("Flights", data1);
		database.insertQuery("Flights", data2);
		database.insertQuery("Flights", data3);
		database.insertQuery("Flights", data4);
		database.insertQuery("Flights", data5);
		database.insertQuery("Flights", data6);
		database.insertQuery("Flights", data7);
		String[] datap = {"Tom","Robbins","34","Oak Woods","tomr13"};
		String[] datap1 = {"James","Bond","141411","N/A","007"};
		String[] datap2 = {"John","Doe","0","No Where","Nobody"};
		String[] datap3 = {"Bob","Builder","134","Cartoon","TheBuilder"};
		String[] datap4 = {"Jim","Carrey","0","Heaven","God"};
		String[] datap5 = {"Grim","Reaper","0","Hell","reaper"};
		database.insertQuery("Passengers", datap);
		database.insertQuery("Passengers", datap1);
		database.insertQuery("Passengers", datap2);
		database.insertQuery("Passengers", datap3);
		database.insertQuery("Passengers", datap4);
		database.insertQuery("Passengers", datap5);
		String[] datat3 = {"234123f","tomr13"};
		database.insertQuery("Tickets", datat3);

		database.finalize();
	}
	/**
	 * returns the names of the cities that have flights into this city
	 */
	public ArrayList<String> citiesIntoList(String city)
	{
		return (new City(database, city)).Into();
	}
	/**
	 * adds a ticket to the database
	 * @param entries must be in correct order
	 */
	public void addTicket(ArrayList<String> entries)
	{
		String[] data = {entries.get(0), entries.get(1)};
		database.insertQuery("Tickets", data);
		database.finalize();
	}
	/**
	 * adds a passenger to the database
	 * @param entries must be in correct order
	 */
	public void addPassenger(ArrayList<String> entries)
	{
		for(String ss : entries)
		{
			System.out.println(ss);
		}
		String[] data = {entries.get(0),
				entries.get(1),
				entries.get(2),
				entries.get(3),
				entries.get(4)};
		database.insertQuery("Passengers", data);
		database.finalize();
	}
	/**
	 * adds a flight to the database
	 * @param entries must be in correct order
	 */
	public void addFlight(ArrayList<String> entries)
	{
		String[] data = {entries.get(0),
				entries.get(1),
				entries.get(2),
				entries.get(3),
				entries.get(4),
				entries.get(5)};
		database.insertQuery("Flights", data);
		database.finalize();
	}
	/**
	 * makes a City class from the database and the name
	 */
	public City getCity(String name)
	{
		City c = null;
		c = new City(database,name);
		return c;
	}
	/**
	 * creates a list of cities that this city goes out of
	 */
	public ArrayList<String> citiesOutOfList(String city)
	{
		return (new City(database, city)).Out();
	}
	/**
	 * creates a Passenger class from an ID and the database
	 */
	public Passenger getPassenger(String passengerID)
	{
		Passenger p = null;
		p = new Passenger(database, passengerID);
		return p;
	}
	/**
	 * creates a list of all the cities that have flights (in and out)
	 */
	public ArrayList<String> cityList()
	{
		ArrayList<String> cities = new ArrayList<String>();
		Table table = database.selectQuery(null, "Flights", null);
		ArrayList<String> citiesOutList = new ArrayList<String>();
		ArrayList<String> citiesInList = new ArrayList<String>();
		//{"FlightNum", "Date", "Plane","MaxPassenger","StartCity","EndCity"};
		for(Tuple t : table)
		{
			Iterator<TupleObject> iter = t.iterator();
			iter.next();
			iter.next();
			iter.next();
			iter.next();
			citiesOutList.add(iter.next().toString());
			citiesInList.add(iter.next().toString());
		}
		//look in both from City and to City
		for(String s : citiesOutList)
		{
			boolean inList = false;
			for(String s2 : cities)
			{
				if(s2.equals(s))
					inList = true;
			}
			//only add new Strings
			if(!inList)
				cities.add(s);
		}
		for(String s : citiesInList)
		{
			boolean inList = false;
			for(String s2 : cities)
			{
				if(s2.equals(s))
					inList = true;
			}
			//only add new strings
			if(!inList)
				cities.add(s);
		}
		return cities;
	}
	/**
	 * creates a Flight class from the flight number and the database 
	 */
    public Flight getFlight(String flightName)
	{
    	Flight f = null;
		f = new Flight(database, flightName);
		return f;
	}
    /**
     * creates a list of all the flights in this database
     * @return
     */
	public ArrayList<String> flightList()
	{
		ArrayList<String> flights = new ArrayList<String>();
		Table table = database.selectQuery(null, "Flights", null);
		for(Tuple t : table)
		{
			for(TupleObject TO : t){
				flights.add(TO.toString());
				break;
			}
		}
		return flights;
	}
	/**
	 * creates a list of all of the ID's of passengers in this flight from the database
	 */
	public ArrayList<String> passengerList(String flightNum)
	{
		Flight f = getFlight(flightNum);
		return f.Passengers();
	}
	/**
	 * creates a list of all the passengers in this database (by ID)
	 */
	public ArrayList<String> passengerList()
	{
		ArrayList<String> passengers = new ArrayList<String>();
		Table table = database.selectQuery(null, "Passengers", null);
		for(Tuple t : table)
		{
			//{"FirstName", "LastName", "Miles", "Address", "PassengerID"};
			Iterator<TupleObject> iter = t.iterator();
			iter.next();
			iter.next();
			iter.next();
			iter.next();
			passengers.add(iter.next().toString());
		}
		return passengers;
	}
}


package App;
import java.util.*;

import TupleObjects.TupleColumn;
import TupleObjects.TupleObject;
import TupleObjects.TupleString;
import Where.WhereEqual;

import DatabaseObjects.Database;
import DatabaseObjects.Table;
import DatabaseObjects.Tuple;
/**
 * Takes a passengers information from the database and puts in into an easy-to-use class. 
 * @author Tom Robbins
 *
 */
public class Passenger {
	//list of the flight numbers this passenger has tickets for
	ArrayList<String> flights;
	String firstName, lastName;
	//flyer miles
	int miles;
	String address, passengerID;
	/**
	 * Takes a passengers information from the database and puts in into an easy-to-use class for the Application.
	 */
	public Passenger(Database database, String name)
	{
		//Passenger table
		WhereEqual p = new WhereEqual(new TupleColumn("PassengerID"), new TupleString(name));
		Table tablePassenger = database.selectQuery(null, "Passengers", p);
		//{"FirstName", "LastName", "Miles", "Address", "PassengerID"};
		//(The first Tuple in the Table)'s iterator
		//passenger information
		Iterator<TupleObject> iter = tablePassenger.iterator().next().iterator();
		firstName = iter.next().toString();
		lastName = iter.next().toString();
		try{
			miles = Integer.parseInt(iter.next().toString());
		}
		catch(Exception e)
		{
		}
		address = iter.next().toString();
		passengerID = iter.next().toString();
		
		//Ticket information
		//Ticket Table
		WhereEqual t = new WhereEqual(new TupleColumn("PassengerID"),new TupleString(passengerID));
		Table tableTickets = database.selectQuery(null, "Tickets", t);
		flights = new ArrayList<String>();
		for(Tuple tuple : tableTickets)
		{
			Iterator<TupleObject> iter2 = tuple.iterator();
			//FlightNum
			flights.add(iter2.next().toString());
		}
	}
	//simple accessor methods
	public String FirstName() { return firstName; }
	public String LastName() { return lastName; }
	public String Address(){ return address; }
	public int FlyerMiles(){ return miles; }
	public ArrayList<String> Flights() { return flights; }
}


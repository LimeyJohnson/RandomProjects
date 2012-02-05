package App;
import java.util.*;

import TupleObjects.TupleColumn;
import TupleObjects.TupleObject;
import TupleObjects.TupleString;
import Where.WhereEqual;

import DatabaseObjects.Database;
import DatabaseObjects.Table;
import DatabaseObjects.Tuple;
public class Flight {
	ArrayList<String> passengers;
	String flightNum, date, planeNum;
	int maxPassenger;
	String startCity, endCity;
	public Flight(Database database, String flightNum)
	{
		//Flight table
		WhereEqual f = new WhereEqual(new TupleColumn("FlightNum"), new TupleString(flightNum));
		Table tableFlight = database.selectQuery(null, "Flights", f);
		//{"FlightNum", "Date", "Plane","MaxPassenger","StartCity","EndCity"};
		//(The first Tuple in the Table)'s iterator
		//passenger information
		Iterator<TupleObject> iter = tableFlight.iterator().next().iterator();
		this.flightNum = iter.next().toString();
		date = iter.next().toString();
		planeNum = iter.next().toString();
		try{
			maxPassenger = Integer.parseInt(iter.next().toString());
		}
		catch(Exception e)
		{
		}
		startCity = iter.next().toString();
		endCity = iter.next().toString();
		
		//Ticket information
		//Ticket Table
		WhereEqual t = new WhereEqual(new TupleColumn("FlightNum"),new TupleString(flightNum));
		Table tableTickets = database.selectQuery(null, "Tickets", t);
		passengers = new ArrayList<String>();
		for(Tuple tuple : tableTickets)
		{
			Iterator<TupleObject> iter2 = tuple.iterator();
			iter2.next();
			//PassengerID
			passengers.add(iter2.next().toString());
		}
	}
	//accessor methods
	public String StartCity() { return startCity; }
	public String EndCity() {return endCity; }
	public int CurrentNumPassengers() { return passengers.size(); }
	public int MaxNumPassengers() { return maxPassenger; }
	public ArrayList<String> Passengers() { return passengers; }
	public String PlaneNumber() { return planeNum; }
	public String FlightNum() { return flightNum; }
	public String Date() { return date; }
}

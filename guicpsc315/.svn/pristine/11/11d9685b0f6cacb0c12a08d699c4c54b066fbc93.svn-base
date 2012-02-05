package App;
import java.util.*;

import TupleObjects.TupleColumn;
import TupleObjects.TupleObject;
import TupleObjects.TupleString;
import Where.WhereEqual;

import DatabaseObjects.Database;
import DatabaseObjects.Table;
import DatabaseObjects.Tuple;
public class City {
	//name of city
	String name;
	//list of cities that have flights going into this city
	ArrayList<String> into;
	//list of cities that have flights going out of this city
	ArrayList<String> out;
	public City(Database database, String name)
	{
		this.name = name;
		//creates into information
		//into Table
		WhereEqual intow = new WhereEqual(new TupleColumn("EndCity"),new TupleString(name));
		Table tableInto = database.selectQuery(null, "Flights", intow);
		into = new ArrayList<String>();
		for(Tuple tuple : tableInto)
		{
			Iterator<TupleObject> iter2 = tuple.iterator();
			//{"FlightNum", "Date", "Plane","MaxPassenger","StartCity","EndCity"};
			iter2.next(); //FlightNum
			iter2.next(); //Date
			iter2.next(); //Plane
			iter2.next(); //MaxPassengers
			into.add(iter2.next().toString());
		}
		//creates out of informations
		WhereEqual outw = new WhereEqual(new TupleColumn("StartCity"),new TupleString(name));
		Table tableOut = database.selectQuery(null, "Flights", outw);
		out = new ArrayList<String>();
		for(Tuple tuple : tableOut)
		{
			Iterator<TupleObject> iter2 = tuple.iterator();
			//{"FlightNum", "Date", "Plane","MaxPassenger","StartCity","EndCity"};
			iter2.next(); //FlightNum
			iter2.next(); //Date
			iter2.next(); //Plane
			iter2.next(); //MaxPassengers
			iter2.next(); //StatCity
			into.add(iter2.next().toString());
		}
	}
	/**
	 * returns the name of this city
	 */
	public String Name() { return name; }
	/**
	 * returns all of the cities that have flights going to this city
	 */
	public ArrayList<String> Into() { return into; }
	/**
	 * returns all of the cities that have flights going from this city
	 */
	public ArrayList<String> Out() { return out; }
}
package App;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import javax.swing.JButton;
import javax.swing.JPanel;
import javax.swing.JTextArea;
import javax.swing.JTextField;
import java.util.ArrayList;

public class AddDataScreen implements ActionListener,  Screen
{
	private ArrayList<String> list;
	FlightApp flightApp;
	private JPanel pane;
	//width of output text boxes
	int formatWidth = 100;
	//width of input text boxes
	int textFieldWidth = 17;
	//Which table to add the data to
	String type;
	//list of input text boxes
	ArrayList<JTextField> textAreas;
	ArrayList<String> text;
	int screenWidth = 200; 
	int screenHeight = 100;
	//amount each entry adds to the screenHeight
	int entriesHeight = 45;
	/**
	 * Screen that adds an entry to the database
	 * @param flightApp
	 * @param entries The Strings of the output text boxes; null if none
	 * @param type Which table to add this to in the database
	 */
	public AddDataScreen(FlightApp flightApp, ArrayList<String> entries, String type)
	{
		this.type = type;
		pane = new JPanel();
		JButton ReturnB = new JButton("Return to Main Menu");
		ReturnB.setActionCommand("Return to Main Menu");
		ReturnB.addActionListener(this);
		pane.add(ReturnB);
		this.flightApp = flightApp;
		textAreas = new ArrayList<JTextField>();
		if(entries != null)
		{
			for(String s : entries)
			{
				pane.add(new JTextArea(makeLength(s,formatWidth)));
				JTextField textf = new JTextField(textFieldWidth);
				pane.add(textf);
				textAreas.add(textf);
				screenHeight+= entriesHeight;
			}
		}
		JButton add = new JButton("ADD");
		add.setActionCommand("Add");
		add.addActionListener(this);
		pane.add(add);
		flightApp.frame.setSize(screenWidth,screenHeight);
		flightApp.frame.add(pane);
		flightApp.frame.setResizable(false);
		flightApp.frame.setLocationRelativeTo(null);
		flightApp.frame.setVisible(true);
	}
	/**
	 * makes String s, l length
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
	 * clears the screen
	 */
	public void dispose()
	{
		flightApp.frame.remove(pane);
	}
	@Override
	public void actionPerformed(ActionEvent e) {
		boolean buttonSelected = false;
		if(list!= null)
		{
			for(String s : list)
			{
				String b = s+"Q";
				if(b.equals(e.getActionCommand()))
				{
					buttonSelected = true;
					System.out.println(s+ " Selected");
					text = new ArrayList<String>();
					for(JTextField t : textAreas)
					{
						text.add(t.getText());
					}
					if(type.equals("Passenger"))
						flightApp.addPassenger(text);
					else if(type.equals("Flight"))
						flightApp.addFlight(text);
					else if(type.equals("Ticket"))
						flightApp.addTicket(text);
					else
						System.out.println(type+": invalid listType");
					flightApp.changeScreen(new MenuScreen(flightApp));
				}
			}
		}
		if ("Return to Main Menu".equals(e.getActionCommand())) {
			flightApp.changeScreen(new MenuScreen(flightApp));
			System.out.println("Returning to Main Menu...");	
	    }
		if ("Add".equals(e.getActionCommand())) {
			System.out.println(type+ " Added");
			text = new ArrayList<String>();
			for(JTextField t : textAreas)
			{
				text.add(t.getText());
			}
			boolean missingData = false;
			for(String s : text)
			{
				if(s.equals(""))
					missingData = true;
			}
			if(!missingData)
			{
				if(type.equals("Passenger"))
					flightApp.addPassenger(text);
				else if(type.equals("Flight"))
					flightApp.addFlight(text);
				else if(type.equals("Ticket"))
					flightApp.addTicket(text);
				else
					System.out.println(type+": invalid listType");
				System.out.println("Added...");	
			}
			else
				System.out.println("Missing Data");
			flightApp.changeScreen(new MenuScreen(flightApp));
	    }
		else if(!buttonSelected)
		{System.out.println("Error: Button Action Not Defined");}
	}
}
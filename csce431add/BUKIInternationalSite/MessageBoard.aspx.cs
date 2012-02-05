using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using BUKI;

public partial class MessageBoard : System.Web.UI.Page
{
    //Current Employee Logged In
    public Employee m_currEmployee;
    protected int mbLocationID;
 
    string clickedButtonID;

    //List and new instances of messages
    static System.Collections.Generic.List<Message> list_of_messages;
    Message new_msg;

    //SQL Stuff
    MySqlConnection conn;
    MySqlCommand cmd;

    //Constructor
    public MessageBoard()
    {
        list_of_messages = new System.Collections.Generic.List<Message>();
    }

    protected override void OnInit(EventArgs e)
    {

        if (Session["employee"] != null)
        {
            m_currEmployee = DataAccess.GetEmployeeByID(int.Parse(Session["employee"].ToString()));
        }
        
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Cell spacing for msg board items
        m_tblListofMsgs.CellSpacing = 20;

        //Gather the current Message Board Location
        if (Session["forum_location"] != null)
        {
            mbLocationID = Int32.Parse(Session["forum_location"].ToString());
        }
        else if (m_currEmployee != null) mbLocationID = m_currEmployee.ID;
        else Response.Redirect("Login.aspx");

        //Find whose message board we are at
        FindLocation(mbLocationID);

        //Gather the list of messages
        populateMessageList(mbLocationID);

        //Increment the amount of views for all existing messages
        incrementMessageViews(mbLocationID);

        //Create the messages
        GenerateMessageList();
    }

    protected void addNewMessage(int currSender, int currReciever, string currText)
    {
        //Command setup for adding a new message to the database
        conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
        string gatherMessagesCmd = "INSERT INTO messages (from_id, to_id, message)" +
                                        " VALUES(" + currSender + "," + currReciever + ",'" + currText + "')";
        cmd = new MySqlCommand(gatherMessagesCmd, conn);

        //Database Processes
        cmd.Connection.Open();
        cmd.ExecuteNonQuery();
        cmd.Connection.Close();

        //Refresh the page
        Response.Redirect(Request.Url.ToString());

    }

    protected void incrementMessageViews(int u_id)
    {
        //Command setup for adding a new message to the database
        conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
        string gatherMessagesCmd = "UPDATE messages SET read_count=read_count +1 WHERE to_id= "+u_id;
        cmd = new MySqlCommand(gatherMessagesCmd, conn);

        //Database Processes
        cmd.Connection.Open();
        cmd.ExecuteNonQuery();
        cmd.Connection.Close();

    }

    protected void deleteExistingMessage(int m_id)
    {
        //Command setup for adding a new message to the database
        conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
        string gatherMessagesCmd = "DELETE FROM messages WHERE id= "+m_id;
        cmd = new MySqlCommand(gatherMessagesCmd, conn);

        //Database Processes
        cmd.Connection.Open();
        cmd.ExecuteNonQuery();
        cmd.Connection.Close();

        //Refresh the page
        Response.Redirect(Request.Url.ToString());
    }

    protected void replyToExistingMessage(int m_id)
    {
        //Commmand setupd for adding a new message to the database
        conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
        string gatherAuthorCmd = "SELECT from_id as id FROM messages WHERE id=" + m_id;
        cmd = new MySqlCommand(gatherAuthorCmd, conn);
        conn.Open();
        if (conn.State == ConnectionState.Open)
        {
           
            MySqlDataReader msgAuthor = cmd.ExecuteReader();

            //Database Process
            while (msgAuthor.Read())
            {
                mbLocationID = Int32.Parse(msgAuthor["id"].ToString());
            }
            msgAuthor.Close();
            conn.Close();

            //Save a session variable for the next pass
            Session["forum_location"] = mbLocationID.ToString();

            //Refresh the page
            Response.Redirect(Request.Url.ToString());
        }
    }

    protected void populateMessageList(int uid)
    {
        //Command Setup for Retrieving all Messages by user
        conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
        string gatherMessagesCmd = "SELECT messages.id, CONCAT(f_name,\" \", l_name) AS Name, from_id,sent_date,message,read_count" +
                                     " FROM messages JOIN employee ON from_id=employee.id WHERE to_id =" + uid;
        cmd = new MySqlCommand(gatherMessagesCmd, conn);
        
        //Actual Querying Process
        cmd.Connection.Open();
        MySqlDataReader userMessages = cmd.ExecuteReader();

        //Populate the messages
        while (userMessages.Read())
        {
            new_msg = new Message();
            new_msg.ID = Int32.Parse(userMessages["id"].ToString());
            new_msg.Sender = userMessages["Name"].ToString();
            new_msg.Conception = userMessages["sent_date"].ToString();
            new_msg.Text = userMessages["message"].ToString();
            new_msg.ViewCount = Int32.Parse(userMessages["read_count"].ToString());
            list_of_messages.Add(new_msg);
        }

        //Cleanup objects
        userMessages.Close();
        cmd.Connection.Close();
    }

    protected void FindLocation(int uid)
    {
        string current_locale = null;

        //Command Setup for Retrieving all Messages by user
        conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
        string gatherMessagesCmd = "SELECT CONCAT(f_name,\" \",l_name) AS Name FROM employee WHERE id=" +uid; 
        cmd = new MySqlCommand(gatherMessagesCmd, conn);

        //Actual Querying Process
        cmd.Connection.Open();
        MySqlDataReader currentUser = cmd.ExecuteReader();

        while (currentUser.Read())
        {
            current_locale = currentUser["Name"].ToString();
        }

        currentUser.Close();
        conn.Close();

        currentMsgBrdLocale.Text = current_locale + "'s ";

    }

    private Table BuildMessageTable(Message singleMsg)
    {
        Table newMessageBox = new Table();
        newMessageBox.BorderWidth = 3;
        newMessageBox.CellSpacing = 5;
        newMessageBox.CellPadding = 2;
        newMessageBox.Style.Add(HtmlTextWriterStyle.BorderColor, "Gray");
        newMessageBox.BorderColor = System.Drawing.Color.Black;

        //Author and Message
        TableRow authorAndMessageRow = new TableRow();
        TableCell authorData = new TableCell();
        authorData.Width = 100;
        authorData.Text = "<center><h3>" + singleMsg.Sender + "</h3><i>" + singleMsg.Conception + "</i></center><br/>";
        authorAndMessageRow.Cells.Add(authorData);
        TableCell messageData = new TableCell();
        messageData.Width = 280;
        messageData.Text = "<center>" + singleMsg.Text + "</center>";
        authorAndMessageRow.Cells.Add(messageData);
        newMessageBox.Rows.Add(authorAndMessageRow);

        //Reply and Delete Buttons - Only create if the current employee logged in matches the current
        //Message Board Location
        if (Master.currEmployee.ID == mbLocationID)
        {
            TableRow buttonActionsRow = new TableRow();
            TableCell emptyCell = new TableCell();
            buttonActionsRow.Cells.Add(emptyCell);
            TableCell buttonCell = new TableCell();
            Button replyButton = new Button();
            replyButton.Text = "Reply";
            replyButton.ID = "rply" + singleMsg.ID.ToString();
            replyButton.Click += new System.EventHandler(this.btnReplyMsg_Click);
            Button deleteButton = new Button();
            deleteButton.Text = "Delete";
            deleteButton.ID = singleMsg.ID.ToString();
            deleteButton.Click += new System.EventHandler(this.btnDeleteMsg_Click);
            buttonCell.HorizontalAlign = HorizontalAlign.Center;
            buttonCell.Controls.Add(replyButton);
            buttonCell.Controls.Add(deleteButton);
            buttonActionsRow.Cells.Add(buttonCell);
            newMessageBox.Rows.Add(buttonActionsRow);
        }
        else
        {
            TableRow buttonActionsRow = new TableRow();
            TableCell emptyCell = new TableCell();
            buttonActionsRow.Cells.Add(emptyCell);
            TableCell buttonCell = new TableCell();
            Button goBackButton = new Button();
            goBackButton.Text = "Go Back";
            goBackButton.Click += new System.EventHandler(this.btnGoBack_Click);
            buttonCell.Controls.Add(goBackButton);
            buttonCell.HorizontalAlign = HorizontalAlign.Center;
            buttonActionsRow.Cells.Add(buttonCell);
            newMessageBox.Rows.Add(buttonActionsRow);
        }

        return newMessageBox;
    }

    protected void GenerateMessageList()
    {
        int msgsToShow = list_of_messages.Count;

        if (msgsToShow > 0)
        {
            for (int i = 0; i < msgsToShow; i++)
            {
                TableRow newMsgBox = new TableRow();
                TableCell newMsgCell = new TableCell();
                TableCell PlaceHolderCell = new TableCell();
                newMsgCell.Controls.Add(BuildMessageTable(list_of_messages[i]));

                newMsgBox.Cells.Add(PlaceHolderCell);
                newMsgBox.Cells.Add(newMsgCell);
                m_tblListofMsgs.Rows.Add(newMsgBox);
            }
        }
    }

    protected int gatherDeleteIDFromButton(String bid)
    {
        int recoveredInt;
        String btnID = bid;
        recoveredInt = Int32.Parse(btnID);
        return recoveredInt;
    }

    protected int gatherReplyIDFromButton(String bid)
    {
        int recoveredInt = -1;
        String btnID = bid;
        btnID = btnID.Replace("rply", "");
        if (btnID != null)
        {
            recoveredInt = Int32.Parse((btnID));
        }

        return recoveredInt;
    }

    protected void btnDeleteMsg_Click(Object Source, EventArgs E)
    {
        clickedButtonID = ((Button)Source).ID;
        deleteExistingMessage(gatherDeleteIDFromButton(clickedButtonID));
    }

    protected void btnReplyMsg_Click(Object Source, EventArgs E)
    {
        clickedButtonID = ((Button)Source).ID;
        replyToExistingMessage(gatherReplyIDFromButton(clickedButtonID));
    }

    protected void btnGoBack_Click(Object Source, EventArgs E)
    {
        Session["forum_location"] = Master.currEmployee.ID;
        Response.Redirect(Request.Url.ToString());
    }
}


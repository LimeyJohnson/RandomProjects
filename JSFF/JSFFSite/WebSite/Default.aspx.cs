using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
        scsb.UserID = "sa";
        scsb.Password = "Xi4cuiP3";
        scsb.DataSource = "LimeySrv1";
        scsb.InitialCatalog = "Scripts";
        SqlConnection con = new SqlConnection(scsb.ToString());
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandText = "Select * From Todo";
        con.Open();
        SqlDataReader reader = cmd.ExecuteReader();
        string text = "<table>";
        while (reader.Read())
        {
            text = text + "<tr><td>" + reader[0] + "</td></tr>";
        }
        text = text + "</table>";
        reader.Close();
        
        tablediv.InnerHtml = text;
                  
    }
    protected void inputbox_TextChanged(object sender, EventArgs e)
    {
        SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
        scsb.UserID = "sa";
        scsb.Password = "Xi4cuiP3";
        scsb.DataSource = "LimeySrv1";
        scsb.InitialCatalog = "Scripts";
        SqlConnection con = new SqlConnection(scsb.ToString());
        SqlCommand cmd = con.CreateCommand();
        cmd.CommandText = "Insert INTO Todo Values(@todo)";
        cmd.Parameters.AddWithValue("@todo", inputbox.Text);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        Response.Redirect("http://LimeyHouse.dyndns.org");
    }
}

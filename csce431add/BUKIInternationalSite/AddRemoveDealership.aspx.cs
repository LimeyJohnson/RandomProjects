using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using BUKI;

public partial class AddRemoveDealership : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Master.currEmployee == null || Master.currEmployee.Position != Enums.Position.Admin) // Make sure they are logged in
            Response.Redirect("Login.aspx"); 
        if (!IsPostBack)
        {
            string sqlCommand = "SELECT id, name FROM dealership ORDER BY name";
            DataAccess.PopulateDropDownList(ref RemoveDL, sqlCommand, "name", "id");
        }

    }

    protected void AddNewDealership(object sender, EventArgs e)
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        try
        {
            conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
            cmd = new MySqlCommand("INSERT INTO dealership (name,addr_city,addr_state,addr_zip) VALUES (?NAME,?CITY,?STATE,?ZIP)", conn);

            cmd.Parameters.Add(new MySqlParameter("?NAME", NameBox.Text));
            cmd.Parameters.Add(new MySqlParameter("?CITY", CityBox.Text));
            cmd.Parameters.Add(new MySqlParameter("?STATE", StateBox.Text));
            cmd.Parameters.Add(new MySqlParameter("?ZIP", ZipBox.Text));
            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            clearForm();

            // Reload the remove dealership drop-down

            string sqlCommand = "SELECT id, name FROM dealership ORDER BY name";
            DataAccess.PopulateDropDownList(ref RemoveDL, sqlCommand, "name", "id");

            RemoveDL.SelectedIndex = 0;

            AddDealershipStatus.Text = "Dealership added successfully.";

        }
        catch (MySqlException ex)
        {
            AddDealershipStatus.Text = "Error while adding dealership. Make sure that the data was entered correctly.";
        }
    }

    protected void clearForm()
    {
        NameBox.Text = "";
        CityBox.Text = "";
        StateBox.Text = "";
        ZipBox.Text = "";
        AddDealershipStatus.Text = "";
        RemoveDealershipStatus.Text = "";
    }

    protected void RemoveDealership(object sender, System.EventArgs e)
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        try
        {
            conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
            cmd = new MySqlCommand("DELETE FROM dealership WHERE id=?ID", conn);

            cmd.Parameters.Add(new MySqlParameter("?ID", RemoveDL.SelectedItem.Value));
            try
            {
                cmd.Connection.Open();

                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
            clearForm();

            // Reload the remove dealership drop-down

            string sqlCommand = "SELECT id, name FROM dealership ORDER BY name";
            DataAccess.PopulateDropDownList(ref RemoveDL, sqlCommand, "name", "id");

            RemoveDL.SelectedIndex = 0;

            RemoveDealershipStatus.Text = "Dealership removed successfully.";
        }
        catch (MySqlException ex)
        {
            //throw ex;
            RemoveDealershipStatus.Text = "Error while removing dealership.";
        }
    }
}

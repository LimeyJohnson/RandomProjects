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
using System.IO;
using MySql.Data.MySqlClient;
using BUKI;

public partial class AddRemoveVehicle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Master.currEmployee == null || Master.currEmployee.Position == Enums.Position.Sales_Person) Response.Redirect("Login.aspx");
        if (Master.currLocation == null) Response.Redirect("SetLocation.aspx");
        if (Master.currEmployee.Position == Enums.Position.Admin)
        {
            AddVehicleModelDiv.Visible = true;
        }
        if (!IsPostBack)
        {

            string sqlCommand = "select id, concat(make, \" \", model, \", \", vehicle_year) as visibleText from vehicle_type order by make";
            DataAccess.PopulateDropDownList(ref MakeModelDL, sqlCommand, "visibleText", "id");

            sqlCommand = "select vin, concat(vehicle_year, \" \", make,\", \", vin) as visibleText from vehicle join vehicle_type on vehicle.vehicle_type=vehicle_type.id order by make,vehicle_year";
            DataAccess.PopulateDropDownList(ref RemoveDL, sqlCommand, "visibleText", "vin");

            if (Master.currEmployee.Position != Enums.Position.Admin)
            {
                sqlCommand = "select id, concat(name, \",   \", addr_city, \", \", addr_state) as visibleText from dealership WHERE id = " + Master.currEmployee.Dealership + " order by name";
                DataAccess.PopulateDropDownList(ref LocationDL, sqlCommand, "visibleText", "id");
            }
            else
            {
                sqlCommand = "select id, concat(name, \",   \", addr_city, \", \", addr_state) as visibleText from dealership order by name";
                DataAccess.PopulateDropDownList(ref LocationDL, sqlCommand, "visibleText", "id");
            }

            sqlCommand = "select id, description from vehicle_condition where id > 0 and id < 6";
            DataAccess.PopulateDropDownList(ref ConditionDL, sqlCommand, "description", "id");

            sqlCommand = "select description, hex_color from color group by description order by description";
            DataAccess.PopulateDropDownList(ref ColorDL, sqlCommand, "description", "hex_color");

            MakeModelDL_SelectedIndexChanged(sender, e);
        }
    }

    protected void MakeModelDL_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        MySqlConnection conn = new MySqlConnection("Server=Database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
        MySqlCommand cmd = new MySqlCommand("select vehicle_type,mpg,doors from vehicle_type where id = ?ID", conn);
        cmd.Parameters.Add(new MySqlParameter("?ID", MakeModelDL.SelectedValue));
        try
        {
            cmd.Connection.Open();

            MySqlDataReader results = cmd.ExecuteReader();

            if (results.Read())
            {
                TypeBox.Text = results.GetString("vehicle_type");
                MPGBox.Text = results.GetString("mpg");
                DoorsBox.Text = results.GetString("doors");
            }
            else
            {
                TypeBox.Text = "";
                MPGBox.Text = "";
                DoorsBox.Text = "";
            }
        }
        finally
        {
            cmd.Connection.Close();
            cmd.Connection.Dispose();
        }
    }

    protected void AddNewVehicle(object sender, System.EventArgs e)
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        try
        {
            conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
            cmd = new MySqlCommand("INSERT INTO vehicle (vin,color,vehicle_type,dealership,display_photo,odometer,vehicle_condition) VALUES (?VIN,?COLOR,?VEHICLE_TYPE,?DEALERSHIP,?DISPLAYPHOTO,?ODOMETER,?VEHICLE_CONDITION)", conn);
            if (VINBox.Text == "")
            {
                return;
            }
            String filename = VINBox.Text;
            cmd.Parameters.Add(new MySqlParameter("?VIN", VINBox.Text));
            cmd.Parameters.Add(new MySqlParameter("?COLOR", ColorDL.SelectedItem.Value));
            cmd.Parameters.Add(new MySqlParameter("?VEHICLE_TYPE", MakeModelDL.SelectedItem.Value));
            cmd.Parameters.Add(new MySqlParameter("?DEALERSHIP", LocationDL.SelectedItem.Value));
            cmd.Parameters.Add(new MySqlParameter("?DISPLAYPHOTO", "car-pictures//" + filename+".jpg"));
            cmd.Parameters.Add(new MySqlParameter("?ODOMETER", MileageBox.Text));
            cmd.Parameters.Add(new MySqlParameter("?VEHICLE_CONDITION", ConditionDL.SelectedItem.Value));
            //cmd.Parameters.Add(new MySqlParameter("?DESCRIPTION", DescriptionBox.Text));
            cmd.Connection.Open();

            cmd.ExecuteNonQuery();

            cmd.Connection.Close();
            cmd.Connection.Dispose();

            clearForm();

            // Reload the remove vehicle drop-down




            string sqlCommand = "select vin, concat(vehicle_year, \" \", make,\", \", vin) as visibleText from vehicle join vehicle_type on vehicle.vehicle_type=vehicle_type.id order by make,vehicle_year";
            DataAccess.PopulateDropDownList(ref RemoveDL, sqlCommand, "visibleText", "vin");

            RemoveDL.SelectedIndex = 0;

            AddVehicleStatus.Text = "Vehicle added successfully.";
            try
            {
                ImageBox.PostedFile.SaveAs(Server.MapPath("car-pictures//" + filename+".jpg"));
            }
            catch (Exception exp)
            {

            }

        }
        catch (MySqlException ex)
        {
            //throw ex;
            AddVehicleStatus.Text = "Error while adding vehicle. Make sure VIN is unique.";
        }
    }

    protected void RemoveVehicle(object sender, System.EventArgs e)
    {
        MySqlConnection conn;
        MySqlCommand cmd;
        try
        {
            conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
            cmd = new MySqlCommand("DELETE FROM vehicle WHERE vin=?VIN", conn);

            cmd.Parameters.Add(new MySqlParameter("?VIN", RemoveDL.SelectedItem.Value));
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

            // Reload the remove vehicle drop-down



            string sqlCommand = "select vin, concat(vehicle_year, \" \", make,\", \", vin) as visibleText from vehicle join vehicle_type on vehicle.vehicle_type=vehicle_type.id order by make,vehicle_year";
            DataAccess.PopulateDropDownList(ref RemoveDL, sqlCommand, "visibleText", "vin");

            RemoveDL.SelectedIndex = 0;

            RemoveVehicleStatus.Text = "Vehicle removed successfully.";
        }
        catch (MySqlException ex)
        {
            //throw ex;
            RemoveVehicleStatus.Text = "Error while removing vehicle.";
        }
    }

    protected void clearForm()
    {
        MakeModelDL.SelectedIndex = 0;
        LocationDL.SelectedIndex = 0;
        VINBox.Text = "";
        PriceBox.Text = "";
        ColorDL.SelectedIndex = 0;
        MileageBox.Text = "";
        ConditionDL.SelectedIndex = 0;
        TypeBox.Text = "";
        MPGBox.Text = "";
        DoorsBox.Text = "";
        DescriptionBox.Text = "";
        AddVehicleStatus.Text = "";
        RemoveVehicleStatus.Text = "";
        RemoveDL.SelectedIndex = 0;
    }
    protected void PostModelButton_Click(object sender, EventArgs e)
    {

    }
}


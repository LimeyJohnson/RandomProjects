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
using MySql.Data.MySqlClient;
using BUKI;

public partial class ApprovePendingSale : System.Web.UI.Page
{
    BUKI.Sale currentPendingSale;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Needs A Sale

    }

    protected void GeneratePendingSalePage(BUKI.Sale salesData)
    {
        //Salesperson Data
        m_SalesPerson.Text = salesData.Salesperson.FirstName + " " + salesData.Salesperson.LastName;

        //Vehicle Data
        m_ApprPendingCarColorVal.Text = salesData.Vehicle.Color;
        m_ApprPendingCarMakeVal.Text = salesData.Vehicle.Make;
        m_ApprPendingCarModelVal.Text = salesData.Vehicle.Model;
        m_ApprPendingCarMSRPVal.Text = salesData.Price.ToString();
        m_ApprPendingCarVINVal.Text = salesData.Vehicle.ID.ToString();
        CarImage.ImageUrl = salesData.Vehicle.Display_Photo;
        
        //Customer Data
        m_ApprPendingCustAddressVal.Text = salesData.Customer.Address;
        m_ApprPendingCustAgeVal.Text = salesData.Customer.Age;
        m_ApprPendingCustCityVal.Text = salesData.Customer.City;
        m_ApprPendingCustNameVal.Text = salesData.Customer.FirstName + " " + salesData.Customer.LastName;
        m_ApprPendingCustStateVal.Text = salesData.Customer.State;
        m_ApprPendingCustZipVal.Text = salesData.Customer.Zip;

        //Extra Notes
        m_txtApprPendingAddtlInfo.Text = salesData.Notes;
    }

    void changeSalesStatus(int sale_id, string new_status)
    {
        if (sale_id == 0 || new_status == "")
        {
            return;
        }
        else
        {
            MySqlConnection conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
            MySqlCommand cmd = new MySqlCommand("UPDATE sales SET status '"+new_status+"' WHERE id =" + sale_id.ToString());
            
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }

    protected void approveBtn_Click(Object Source, EventArgs E)
    {
        changeSalesStatus(currentPendingSale.ID, "APPROVED");
        Response.Redirect("ManagerApproved.aspx");
    }

    protected void denyBtn_Click(Object Source, EventArgs E)
    {
        changeSalesStatus(currentPendingSale.ID, "DENIED");
        Response.Redirect("Denied.aspx");
    }
}

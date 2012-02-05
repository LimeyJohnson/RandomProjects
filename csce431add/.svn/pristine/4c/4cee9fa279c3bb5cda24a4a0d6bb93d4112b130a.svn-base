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
using System.Text.RegularExpressions;
using BUKI;
using MySql.Data.MySqlClient;

public partial class BuyNow : System.Web.UI.Page
{
    DataRow m_drCurrentCarInfo = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessPageRequest();
            BuildCarInfo();
        }
        if (Master.currEmployee == null) div_customer.Visible = true;
        else
        {
            div_salesman.Visible = true;

            lbl_salesman_title.Text = "Make sale for " + Master.currEmployee.FirstName + " " + Master.currEmployee.LastName;
          }
    }

    private void ProcessPageRequest()
    {
        string strCarVin = Request.QueryString["car"];

        //Check to make sure our VIN length is what it should be
        if (strCarVin.Length == 17)
        {
            DataSet CarToBuy = DataAccess.GetCarFromVIN(strCarVin);

            if (CarToBuy.Tables.Count > 0)
            {
                DataTable dtCarsInfo = CarToBuy.Tables[0];
                if (dtCarsInfo.Rows.Count > 0)
                {
                    m_drCurrentCarInfo = dtCarsInfo.Rows[0];
                }
            }
        }
    }

    private void BuildCarInfo()
    {
        if (m_drCurrentCarInfo != null)
        {
            m_lblVinValue.Text = m_drCurrentCarInfo["vin"].ToString();
            m_lblMakeValue.Text = m_drCurrentCarInfo["make"].ToString();
            m_lblModelValue.Text = m_drCurrentCarInfo["model"].ToString();
            m_lblColorValue.Style.Add(HtmlTextWriterStyle.Color, m_drCurrentCarInfo["color"].ToString());
            m_lblColorValue.Style.Add(HtmlTextWriterStyle.BackgroundColor, m_drCurrentCarInfo["color"].ToString());

            //This is used to match the length of the VIN to give a nice even background color.
            m_lblColorValue.Text = m_drCurrentCarInfo["vin"].ToString();
            m_lblMSRPValue.Text = "$" + m_drCurrentCarInfo["market_price"].ToString();
            m_imgCarImage.ImageUrl = m_drCurrentCarInfo["display_photo"].ToString();

            m_lblPageTitle.Text = m_drCurrentCarInfo["vehicle_year"] + " " + m_drCurrentCarInfo["make"] + " " + m_drCurrentCarInfo["model"];
        }
    }

    public int AddNewCustomer()
    {
        int newCustomerID = 0;
        int existingCustomerResult = 0; //Used to check if this customer already exists in the database

        MySqlConnection conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
        MySqlCommand findExistingCmd = new MySqlCommand("SELECT MAX(id) from customer where f_name ='" + m_txtCrtPendingCustFirstNameVal.Text + "' and l_name ='" + m_txtCrtPendingCustLastNameVal.Text + "'", conn);
        findExistingCmd.Connection.Open();

        //First check to see if the customer already exists in the database, if he does, return his ID, as there is no need to add him
        MySqlDataReader queryReader = findExistingCmd.ExecuteReader();
        while (queryReader.Read())
        {
            if (queryReader.FieldCount != 0)
            {
                existingCustomerResult = (queryReader.GetInt32(0));
            }
        }

        queryReader.Close();

        if (existingCustomerResult != 0)
        {
            queryReader.Dispose();
            return existingCustomerResult;
        }

        //If the customer doesn't exist, add him into the database, and if its a success, extract his id and return it.
        MySqlCommand addNewCustomerCmd = new MySqlCommand("INSERT INTO customer(f_name, l_name, addr_city, addr_state, addr_zip, phone, email) " +
                                               "VALUES('" + m_txtCrtPendingCustFirstNameVal.Text + "','" + m_txtCrtPendingCustLastNameVal.Text + "','" + m_txtCrtPendingCustCityVal.Text + "','" + m_txtCrtPendingCustStateVal.Text + "','" + m_txtCrtPendingCustZipVal.Text + "','" + m_txtCustomerPhoneNumber.Text + "','" + m_txtCustomerEmail.Text + ")", conn);

        addNewCustomerCmd.ExecuteNonQuery();

        //queryReader.();
        queryReader = findExistingCmd.ExecuteReader();
        while (queryReader.Read())
        {
            newCustomerID = (queryReader.GetInt32(0));
        }

        if (newCustomerID != 0)
        {
            queryReader.Close();
            queryReader.Dispose();
            return newCustomerID;
        }

        else
        {
            queryReader.Close();
            queryReader.Dispose();
            return 0;
        }
    }

    public bool generateNewSale(int customerID)
    {
        //Make a new pending sale
        MySqlConnection conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
        MySqlCommand findExistingCmd = new MySqlCommand("SELECT id FROM sales WHERE sales.vin='" + m_drCurrentCarInfo["vin"].ToString() + "'", conn);

        findExistingCmd.Connection.Open();

        //First check to see if the sale already exists in the database, if it does, return something so that we know
        MySqlDataReader queryReader = findExistingCmd.ExecuteReader();
        int existingID = 0;
        while (queryReader.Read())
        {
            existingID = (queryReader.GetInt32(0));
        }

        queryReader.Close();

        if (existingID != 0)
        {
            //Then we have found an existing sale for this car already.
            queryReader.Dispose();
            return false;
        }


        //NOw insert the sale into the database

        MySqlCommand AddNewSale = new MySqlCommand("INSERT INTO sales(salesman_id, manager_id, customer_id, vin, sale_price, sale_date, status, terms, notes) " +
                                               "VALUES('" + "0" + "','" + "0" + "','" + customerID.ToString() + "','" + m_drCurrentCarInfo["vin"] + "','" + m_drCurrentCarInfo["market_price"] + "','" + DateTime.Now.ToShortDateString() + "','" + "PENDING" + "','" + "" + "','" + "" + ")", conn);

        AddNewSale.ExecuteNonQuery();

        return true;
    }

    protected void btnBuyNow_Click(object sender, EventArgs e)
    {
        int CustomerID = AddNewCustomer();

        if (CustomerID != 0)
        {
            generateNewSale(CustomerID);
        }
    }
}

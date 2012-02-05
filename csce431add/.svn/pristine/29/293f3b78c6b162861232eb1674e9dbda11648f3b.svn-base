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
      
            ProcessPageRequest();
            BuildCarInfo();

            if (Master.currEmployee != null)
            {
                lbl_salesman.Text = "Sell Car as " + Master.currEmployee.FirstName + " " + Master.currEmployee.LastName;
                ManPointing.Visible = false;
                BuyNowBtn.Visible = false;
                btn_salesman.Visible = true;
            }
    }

    private void ProcessPageRequest()
    {
        string strCarVin = Request.QueryString["Car"];

        //Check to make sure our VIN length is what it should be
        if (strCarVin.Length>1)
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

    protected int AddNewCustomer()
    {
        int newCustomerID = 0; //Used to return the return the id of the new customer that is added to db
        int existingCustomerResult = 0; //Used to check if this customer already exists in the database

        MySqlConnection conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
        String findExistingCustomer = "SELECT MAX(id) AS existingID from customer where f_name ='" + m_txtCrtPendingCustFirstNameVal.Text + "' and l_name ='" + m_txtCrtPendingCustLastNameVal.Text + "'";
        MySqlCommand findExistingCmd = new MySqlCommand(findExistingCustomer, conn);

        if (m_txtCrtPendingCustFirstNameVal.Text != null && m_txtCrtPendingCustLastNameVal.Text != null)
        {
            findExistingCmd.Connection.Open();

            //First check to see if the customer already exists in the database, if he does, return his ID, as there is no need to add him
            MySqlDataReader queryReader = findExistingCmd.ExecuteReader();
            while (queryReader.Read())
            {
                if (queryReader["existingID"].ToString() != "")
                {
                    existingCustomerResult = int.Parse(queryReader["existingID"].ToString());
                }
            }

            queryReader.Close();

            //The Customer Already Exists in the database, return his ID
            if (existingCustomerResult != 0)
            {
                findExistingCmd.Connection.Close();
                return existingCustomerResult;
            }
            findExistingCmd.Connection.Close();

            //If the customer doesn't exist, add him into the database, and if its a success, extract his id and return it.
            String addNewCustomer = "INSERT INTO customer(f_name, l_name, addr_city, addr_state, addr_zip, phone, email) " +
                                                   "VALUES('" + m_txtCrtPendingCustFirstNameVal.Text + "','" + m_txtCrtPendingCustLastNameVal.Text + "','" + m_txtCrtPendingCustCityVal.Text + "','" + m_txtCrtPendingCustStateVal.Text + "','" + m_txtCrtPendingCustZipVal.Text + "','" + m_txtCustomerPhoneNumber.Text + "','" + m_txtCustomerEmail.Text + "')";
            MySqlCommand addNewCustomerCmd = new MySqlCommand(addNewCustomer, conn);

            addNewCustomerCmd.Connection.Open();
            addNewCustomerCmd.ExecuteNonQuery();
            addNewCustomerCmd.Connection.Close();

            String retrieveNewCustomer = "SELECT MAX(id) AS newCustomer from customer where f_name ='" + m_txtCrtPendingCustFirstNameVal.Text + "' and l_name ='" + m_txtCrtPendingCustLastNameVal.Text + "'";
            MySqlCommand gatherIDFromCustomerAdded = new MySqlCommand(retrieveNewCustomer, conn);
            gatherIDFromCustomerAdded.Connection.Open();
            MySqlDataReader queryRetriever = gatherIDFromCustomerAdded.ExecuteReader();

            while (queryRetriever.Read())
            {
                if (queryRetriever["newCustomer"].ToString() != "")
                {
                    newCustomerID = Int32.Parse(queryRetriever["newCustomer"].ToString());
                }
                else
                    newCustomerID = 0;
            }

            queryRetriever.Close();
            gatherIDFromCustomerAdded.Connection.Close();

            return newCustomerID;
        }
        
        //Handle if one of the fields is null
        else
            return 0;
    }

    public void generateNewSale(int customerID)
    {
        if (customerID != 0)
        {
            //Make a new pending sale
            MySqlConnection conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
            if (m_drCurrentCarInfo != null)
            {
                String gatherExistingSaleID = "SELECT id AS existingSalesID FROM sales WHERE sales.vin='" + m_drCurrentCarInfo["vin"].ToString() + "'";
                MySqlCommand findExistingCmd = new MySqlCommand(gatherExistingSaleID, conn);

                findExistingCmd.Connection.Open();

                //First check to see if the sale already exists in the database, if it does, return something so that we know
                MySqlDataReader queryReader = findExistingCmd.ExecuteReader();
                int existingID = 0;
                while (queryReader.Read())
                {
                    existingID = Int32.Parse(queryReader["existingSalesID"].ToString());
                }

                queryReader.Close();

                if (existingID != 0)
                {
                    //Then we have found an existing sale for this car already.
                    queryReader.Close();
                    findExistingCmd.Connection.Close();
                    Response.Redirect("CarIsDeniedPending.aspx");
                }

                //Insert the sale into the database
                String addNewSaleQuery;
                String salesman = "null";
                if (Master.currEmployee != null) salesman = "'" + Master.currEmployee.ID.ToString() + "'";
               addNewSaleQuery = "INSERT INTO sales(salesman_id,customer_id, vin, sale_price, sale_date, status, terms, notes) " +
                                                    "VALUES("+salesman+",'" + customerID.ToString() + "','" + m_drCurrentCarInfo["vin"].ToString() + "','" + m_drCurrentCarInfo["market_price"].ToString() + "', CURDATE(), 'Pending' ,'" + "" + "','" + "Customer Website Purchase'" + ")";
            
               
                MySqlCommand AddNewSale = new MySqlCommand(addNewSaleQuery, conn);
                
                AddNewSale.ExecuteNonQuery();
                AddNewSale.Connection.Close();
            }
        }
    }

    protected void btnBuyNow_Click(object sender, EventArgs e)
    {
        int CustomerID = AddNewCustomer();
        if (CustomerID != 0)
        {
            generateNewSale(CustomerID);
            Response.Redirect("CarIsPending.aspx");
        }
        Response.Redirect("CarIsDeniedPending.aspx");
    }
}

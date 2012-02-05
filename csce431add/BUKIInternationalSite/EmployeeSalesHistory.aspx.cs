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

public partial class EmployeeSalesHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PopulateEmployees();
        }
        GridView1.DataBind();
    }

    public void PopulateEmployees()
    {
        MySqlConnection conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
        MySqlCommand cmd = new MySqlCommand("SELECT CONCAT(l_name,', ',f_name) AS 'Name', employee.id AS 'id' from employee join job_categories on employee.job=job_categories.id where title like '%Salesman%'", conn);
        //MySqlCommand cmd = new MySqlCommand("SELECT * from employee join job_categories on employee.job=job_categories.id", conn);
        cmd.Connection.Open();

        MySqlDataReader ddlValues = cmd.ExecuteReader();
        EmployeeDL.DataSource = ddlValues;
        EmployeeDL.DataTextField = "Name";
        EmployeeDL.DataValueField = "id";
        

        EmployeeDL.DataBind();
        EmployeeDL.Items.Insert(0, new ListItem("Everyone", "-1"));
        cmd.Connection.Close();
        cmd.Connection.Dispose();
    }
     public DataSet GetData(String eID)
    {
        if (eID == null || eID == "")
        {
            return null;
        }
        
        int employeeID = System.Convert.ToInt32(eID, 10);
        String whereClause = String.Empty;
         if (employeeID != -1)
        {
            whereClause += "where salesman_id =" + employeeID;
        }
         MySqlConnection conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");

         MySqlDataAdapter dataAdapter = new MySqlDataAdapter("SELECT vin, CONCAT(l_name,', ',f_name) AS 'Name', sale_price, sale_date, status FROM sales JOIN employee ON sales.salesman_id = employee.id " + whereClause, conn);
        DataSet DS = new DataSet();
        dataAdapter.Fill(DS, "table");
        return DS;
    }
     protected void btnGatherSalesHistory_Click(object sender, EventArgs e)
     {
         GridView1.DataSource = GetData(EmployeeDL.SelectedValue);
         GridView1.DataBind();
     }
}

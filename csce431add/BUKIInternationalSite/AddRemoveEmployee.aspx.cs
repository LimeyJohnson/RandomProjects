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

public partial class AddRemoveEmployee : System.Web.UI.Page
{
    Employee addedEmployee;
    Employee m_currEmployee;

    //SQL Stuff
    MySqlConnection conn;
    MySqlCommand cmd;

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
        if (m_currEmployee != null ){
        
            if(m_currEmployee.Position != BUKI.Enums.Position.Sales_Person){
                int dealerID = m_currEmployee.Dealership;

                if (!IsPostBack)
                {
                    //Used in Manager's View
                    string sqlCommand;
                    sqlCommand = "select id, concat(l_name, \", \", f_name) AS visibleText from employee where end_date IS NULL";
                    if (m_currEmployee.Position == BUKI.Enums.Position.Manager)
                    {
                        sqlCommand += " AND dealership=" + dealerID;
                    }

                    DataAccess.PopulateDropDownList(ref RemoveEmployeeDL, sqlCommand, "visibleText", "id");

                    sqlCommand = "select id, title AS visibleText FROM job_categories";
                    DataAccess.PopulateDropDownList(ref addEmpGradeDL, sqlCommand, "visibleText", "id");
                }
            }
            //Security feature to prevent Salespeople from using the address bar as means of access
            else
                Response.Redirect("Default.aspx");
        }

        else
        {
            Response.Redirect("Login.aspx");
        }

    }

    protected void instantiateNewEmployee(string fname, string lname, string cty, string state,
                                            string zip, int job, string phone, string username,
                                            int dealership, string pwd, string email)
    {
        addedEmployee = new Employee();

        addedEmployee.FirstName = fname;
        addedEmployee.LastName = lname;
        addedEmployee.City = cty;
        addedEmployee.State = state;
        addedEmployee.Zip = zip;
        addedEmployee.Position = (BUKI.Enums.Position)job;
        addedEmployee.Phone = phone;
        addedEmployee.Dealership = dealership;
        addedEmployee.Username = username;
        addedEmployee.Password = pwd;
        addedEmployee.Email = email;
    }

    protected void addNewEmployee(Employee e)
    {
        if (e != null){
            conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
            string addEmpSQLQuery = "INSERT INTO employee (f_name, l_name, addr_city, addr_state, addr_zip, job, phone, start_date, dealership, username, pass, email) "
                + "VALUES ('" + e.FirstName + "', '" + e.LastName + "', '" + e.City + "', '" + e.State + "', '" +
                                                                e.Zip + "', " + e.Position + ", '" + e.Phone + "', CURDATE(), " +
                                                                e.Dealership + ", '" + e.Username + "', '" + e.Password + "', '" + e.Email + "')";

            cmd = new MySqlCommand(addEmpSQLQuery, conn);

            //Database Processes
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            //Refresh the page
            Response.Redirect(Request.Url.ToString());
        }
    }

    protected void btnAddEmployee_Click(Object Source, EventArgs E)
    {
        if (m_currEmployee != null)
        {
            instantiateNewEmployee(addEmpFirstName.Text, addEmpLastName.Text, addEmpCity.Text, addEmpState.Text, addEmpZip.Text, Int32.Parse(addEmpGradeDL.Text),
                                   addEmpPhone.Text, addEmpUsername.Text, m_currEmployee.Dealership, addEmpPassword.Text, addEmpEmail.Text);
        }
        addNewEmployee(addedEmployee);
    }

    protected void deleteSelectedEmployee(int id)
    {
        if (id != 0)
        {
            conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
            string addEmpSQLQuery = "UPDATE employee SET end_date =CURDATE() WHERE id=" + id;

            cmd = new MySqlCommand(addEmpSQLQuery, conn);

            //Database Processes
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

            //Refresh the page
            Response.Redirect(Request.Url.ToString());
        }
    }
    protected void btnRemoveEmployee_Click(Object Source, EventArgs E)
    {
        int deletedEmployee = 0;
        if (m_currEmployee != null)
        {
            deletedEmployee = Int32.Parse(RemoveEmployeeDL.Text);
        }
        deleteSelectedEmployee(deletedEmployee);
    }
}

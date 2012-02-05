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

    public partial class DealershipModification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                

                string sqlCommand = "select id, concat(addr_city,\", \",addr_state) as visibleText from dealership order by addr_city";
                DataAccess.PopulateDropDownList(ref DealershipDL, sqlCommand, "visibleText", "id");
            }

            if (IsPostBack)
            {
                MySqlConnection conn;
                MySqlCommand cmd;

                try
                {

                    string selectStatement = "SELECT l_name as `Last Name`,f_name as `First Name`,addr_city as City,addr_state as State,addr_zip as ZIP,phone as Phone,DATE_FORMAT(start_date,'%b %e, %Y') as `Start Date`,DATE_FORMAT(end_date, '%b %e, %Y') as `End Date` ";
                    string fromClause = "FROM employee JOIN job_categories ON employee.job=job_categories.id ";
                    //
                    // Populate salesmen grid
                    //

                    conn = new MySqlConnection("Server=database2.cs.tamu.edu; Database=gcopley-car_dealership; Uid=gcopley ;Pwd=add431;");
                    string sqlCommand = selectStatement;
                    sqlCommand += fromClause;
                    sqlCommand += "WHERE dealership=?DEALERSHIP AND job_categories.title LIKE \"%Salesman%\" order by l_name";
                    cmd = new MySqlCommand(sqlCommand, conn);

                    cmd.Parameters.AddWithValue("?DEALERSHIP", DealershipDL.SelectedItem.Value);

                    cmd.Connection.Open();

                    MySqlDataReader salesmanGridValues = cmd.ExecuteReader();

                    SalesmenGrid.DataSource = salesmanGridValues;
                    SalesmenGrid.DataBind();

                    cmd.Connection.Close();

                    //
                    // Populate managers grid
                    //

                    sqlCommand = selectStatement;
                    sqlCommand += fromClause;
                    sqlCommand += "WHERE dealership=?DEALERSHIP AND job_categories.title LIKE \"Manager%\" order by l_name";
                    cmd = new MySqlCommand(sqlCommand, conn);

                    cmd.Parameters.AddWithValue("?DEALERSHIP", DealershipDL.SelectedItem.Value);

                    cmd.Connection.Open();

                    MySqlDataReader managerGridValues = cmd.ExecuteReader();

                    ManagersGrid.DataSource = managerGridValues;
                    ManagersGrid.DataBind();

                    cmd.Connection.Close();

                    // 
                    // Populate admins grid
                    //

                    sqlCommand = selectStatement;
                    sqlCommand += fromClause;
                    sqlCommand += "WHERE dealership=?DEALERSHIP AND job_categories.title LIKE \"Admin%\" order by l_name";
                    cmd = new MySqlCommand(sqlCommand, conn);

                    cmd.Parameters.AddWithValue("?DEALERSHIP", DealershipDL.SelectedItem.Value);

                    cmd.Connection.Open();

                    MySqlDataReader adminGridValues = cmd.ExecuteReader();

                    AdminsGrid.DataSource = adminGridValues;
                    AdminsGrid.DataBind();

                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }
                catch (MySqlException ex)
                {

                }
            }
        }
    }


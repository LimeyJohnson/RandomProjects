using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Data Access Class for interfacing with Database
/// </summary>
/// 
namespace BUKI
{
    public static class DataAccess
    {
       static MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["MySql"].ConnectionString);
       
        public static String GetDealerShipNameById(int id)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT name FROM dealership WHERE id = ?ID";

            cmd.Parameters.Add(new MySqlParameter("?ID", id));
            String Output = null;
            try
            {
                conn.Open();
                MySqlDataReader DR = cmd.ExecuteReader();
                DR.Read();
                Output = DR[0].ToString();
                conn.Close();
            }
            finally
            {
                conn.Close();
            }

            return Output;
        }
        public static  String GetDealerAddressNameById(int id)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT addr FROM dealership WHERE id = ?ID";

            cmd.Parameters.Add(new MySqlParameter("?ID", id));
            String Output = null;
            try
            {
                conn.Open();
                MySqlDataReader DR = cmd.ExecuteReader();
                DR.Read();
                Output = DR[0].ToString();

            }
            finally
            {
                conn.Close();
            }

            return Output;
        }
        public static Employee GetEmployeeByUsrnameAndPassword(String Username, String Password)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM employee LEFT OUTER JOIN job_categories ON employee.job = job_categories.id WHERE username = ?Username AND pass = ?Password;";

            cmd.Parameters.Add(new MySqlParameter("?Username", Username.ToLower().Trim()));
            cmd.Parameters.Add(new MySqlParameter("?Password", Password));
            Employee emp = null;
            try
            {
                conn.Open();
                MySqlDataReader DR = cmd.ExecuteReader();
                if (!DR.HasRows)
                {
                    conn.Close();
                    return null;
                }
                DR.Read();

                emp = new Employee();

                if (DR["title"].ToString() == "Admin")
                {
                    emp = new Admin();
                }
                if (DR["title"].ToString() == "Manager")
                {
                    emp = new Manager(DR["dealership"].ToString());

                }
                if (DR["title"].ToString() == "Salesman")
                {
                    emp = new Salesperson(DR["dealership"].ToString());

                }
                emp.FirstName = DR["f_name"].ToString();
                emp.LastName = DR["l_name"].ToString();
                emp.Dealership = int.Parse(DR["dealership"].ToString());
                emp.Address = DR["addr_city"].ToString() + " " + DR["addr_state"].ToString();
                emp.Email = DR["email"].ToString();
                emp.EmployedDate = DateTime.Parse(DR["start_date"].ToString());
                // emp.TerminatedDataAccesste = DataAccessteTime.Parse(DR["end_DataAccesste"].ToString());
                emp.Password = DR["pass"].ToString();
                emp.Username = DR["username"].ToString();
                emp.ID = int.Parse(DR["id"].ToString());

            }
            finally
            {
                conn.Close();
            }
            return emp;
        }
        public static Employee GetEmployeeByID(int ID)
        {

            MySqlCommand cmd = conn.CreateCommand();
            //cmd.CommandText = "SELECT * FROM employee LEFT OUTER JOIN job_categories ON employee.job = job_categories.id WHERE employee.id = ?id";

            cmd.CommandText = "SELECT IF(title LIKE '%Salesman%', 'Salesman', IF(title LIKE '%Admin%', 'Admin', IF(title LIKE '%Manager%', 'Manager', 'Unknown'))) " +
                        "AS title,employee.id,addr_city,addr_state,start_date,dealership, email,f_name,l_name,username,pass from employee JOIN job_categories ON job=job_categories.id " +
                        "WHERE end_date IS null AND employee.id=?id";
            cmd.Parameters.Add(new MySqlParameter("?id", ID));
            Employee emp = null;

            try
            {
                conn.Open();
               
                MySqlDataReader DR = cmd.ExecuteReader();
                if (!DR.HasRows)
                {
                    conn.Close();
                    return null;
                }
                DR.Read();

                emp = new Employee();

                if (DR["title"].ToString() == "Admin")
                {
                    emp = new Admin();
                }
                if (DR["title"].ToString() == "Manager")
                {
                    emp = new Manager(DR["dealership"].ToString());

                }
                if (DR["title"].ToString() == "Salesman")
                {
                    emp = new Salesperson(DR["dealership"].ToString());

                }
                emp.FirstName = DR["f_name"].ToString();
                emp.LastName = DR["l_name"].ToString();
                emp.Address = DR["addr_city"].ToString() + " " + DR["addr_state"].ToString();
                emp.Email = DR["email"].ToString();
                emp.EmployedDate = DateTime.Parse(DR["start_date"].ToString());
                // emp.TerminatedDataAccesste = DataAccessteTime.Parse(DR["end_DataAccesste"].ToString());
                emp.Password = DR["pass"].ToString();
                emp.Username = DR["username"].ToString();
                emp.ID = int.Parse(DR["id"].ToString());
                emp.Dealership = int.Parse(DR["dealership"].ToString());
                DR.Close();

                MySqlCommand msgCmd = conn.CreateCommand();
                msgCmd.CommandText = "SELECT sent_date, from_id, to_id, message, messages.id, read_count, empFrom.f_name AS from_f_name , empFrom.l_name AS from_l_name, empTo.f_name AS to_f_name, empTo.l_name AS to_l_name FROM messages, employee as empFrom, employee as empTo WHERE messages.to_id = empTo.id AND messages.from_id = empFrom.id AND (messages.to_id = ?id OR messages.from_id = ?id) ORDER BY sent_date DESC";
                msgCmd.Parameters.Add(new MySqlParameter("?id", emp.ID));


                MySqlDataReader MsgDR = msgCmd.ExecuteReader();

                while (MsgDR.Read())
                {
                    Message msg = new Message();
                    msg.Text = MsgDR["message"].ToString();
                    msg.ViewCount = MsgDR.GetInt32("read_count");
                    msg.Sender = MsgDR["from_f_name"] + " " + MsgDR["from_l_name"];
                    msg.Receiver = MsgDR["to_f_name"] + " " + MsgDR["to_l_name"];
                    msg.ID = MsgDR.GetInt32("id");
                    msg.SentDate = MsgDR["sent_date"].ToString();
                    if (MsgDR.GetInt32("to_id") == emp.ID)
                    {
                        emp.AddRecMessage(msg);
                    }
                    if (MsgDR.GetInt32("from_id") == emp.ID)
                    {
                        emp.AddSentMessage(msg);
                    }
                }
            }
            finally
            {
                conn.Close();
            }

            //Quick Fix to Retrieve Employees Position
            conn.Open();
            emp.Position = getPositionFromEmployeeID(ID);
            conn.Close();

            return emp;

            
        }
        public static DataSet GetCarLocations()
        {
           MySqlCommand cmd = conn.CreateCommand();
           cmd.CommandText = "SELECT * FROM vehicle,vehicle_type WHERE vehicle.vehicle_type = vehicle_type.id";
           MySqlDataAdapter DataAccess = new MySqlDataAdapter(null, conn);
           DataSet Location = new DataSet();
           DataAccess.SelectCommand = cmd;
           DataAccess.Fill(Location, "Location");
          
           return Location;
        }

        public static  DataSet GetAllCars()
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM vehicle, vehicle_type WHERE vehicle.vehicle_type = vehicle_type.id";
            MySqlDataAdapter DataAccess = new MySqlDataAdapter(null, conn);
            DataSet Cars = new DataSet();
            DataAccess.SelectCommand = cmd;
            DataAccess.Fill(Cars, "Cars");

            return Cars;
        }

        public static DataSet GetCarFromVIN(string strVin)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM vehicle, vehicle_type WHERE vehicle.vehicle_type = vehicle_type.id AND vehicle.vin='" + strVin + "'";
            MySqlDataAdapter DataAccess = new MySqlDataAdapter(null, conn);
            DataSet Car = new DataSet();
            DataAccess.SelectCommand = cmd;
            DataAccess.Fill(Car, "Car");

            return Car;
        }

        public static DataSet GetSearchedCars(String[] args, String advancedOptions)
        {
            MySqlCommand cmd = conn.CreateCommand();
            String sqlCommand = "SELECT * FROM vehicle JOIN vehicle_type ON vehicle.vehicle_type=vehicle_type.id JOIN dealership ON vehicle.dealership=dealership.id  WHERE ";
            if (args.Count() > 0)
                sqlCommand += "(";
            foreach (String arg in args)
            {
                sqlCommand += "make LIKE \"%" + arg + "%\" OR model LIKE \"%" + arg + "%\" OR ";
            }
            sqlCommand = sqlCommand.TrimEnd(" OR".ToCharArray());
            if(args.Count() > 0)
                sqlCommand += ")";
            if (args.Count() == 0)
            {
                advancedOptions = advancedOptions.Trim();
                if(advancedOptions.StartsWith("AND"))
                    advancedOptions = advancedOptions.Trim("AND".ToCharArray());
            }
            sqlCommand += advancedOptions;
            MySqlDataAdapter DataAccess = new MySqlDataAdapter(null, conn);
            DataSet Cars = new DataSet();
            cmd.CommandText = sqlCommand;
            DataAccess.SelectCommand = cmd;
            DataAccess.Fill(Cars, "Cars");

            return Cars;
        }

        // Used to populate a drop-down from a SQL query
        public static void PopulateDropDownList(ref DropDownList ddl, string sqlCommand, string textField, string valueField)
        {
            MySqlCommand cmd = new MySqlCommand(sqlCommand, conn);
            try
            {
                cmd.Connection.Open();

                MySqlDataReader ddlValues = cmd.ExecuteReader();

                ddl.DataSource = ddlValues;
                ddl.DataTextField = textField;
                ddl.DataValueField = valueField;
                ddl.DataBind();

                ddl.Items.Insert(0, ""); // Insert blank entry as first item

            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public static void IncrementViewCount(int msgId)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE messages SET read_count = read_count + 1 WHERE id = ?id";
            cmd.Parameters.Add(new MySqlParameter("?id", msgId));
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }
        public static void AddMessage(int sender, int reciever, string text)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO messages(from_id, to_id, sent_date, message, read_count) VALUES(?from_id, ?to_id, CURDATE(), ?message, 0)";
            cmd.Parameters.Add(new MySqlParameter("?from_id", sender));
            cmd.Parameters.Add(new MySqlParameter("?to_id", reciever ));
            cmd.Parameters.Add(new MySqlParameter("?message", text  ));
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


        }
        public static List<Employee> GetAllEmployees()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT CONCAT(f_name,', ',l_name) AS 'Name', employee.id AS 'id' from employee join job_categories on employee.job=job_categories.id ORDER BY Name",conn );
            List<Employee> emps = new List<Employee>();
            conn.Open();
            MySqlDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                Employee emp = new Employee();
                emp.FirstName = DR["Name"].ToString();
                emp.ID = DR.GetInt32("id");
                emps.Add(emp);
            }
            conn.Close();
            return emps;

        }
        public static void DeleteMessage(int id)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM messages WHERE id = ?id";
            cmd.Parameters.Add(new MySqlParameter("?id", id));
           
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();


        }
        private static BUKI.Enums.Position getPositionFromEmployeeID(int id)
        {
            BUKI.Enums.Position determinedPosition = 0;
            String queryReturn = String.Empty;

            String positionCommand = "SELECT IF(title LIKE '%Salesman%', 'Salesman', IF(title LIKE '%Admin%', 'Admin', IF(title LIKE '%Manager%', 'Manager', 'Unknown')))" +
                                        " AS Position FROM employee JOIN job_categories ON job=job_categories.id WHERE end_date IS null AND employee.id="+id;
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = positionCommand;

            MySqlDataReader positionReader = cmd.ExecuteReader();
            while(positionReader.Read()){
                queryReturn = positionReader["Position"].ToString();
            }

            if (queryReturn.Equals("Salesman")){
                determinedPosition = BUKI.Enums.Position.Sales_Person;
                positionReader.Close();
                return determinedPosition;
            }
            else if(queryReturn.Equals("Manager")){
                determinedPosition = BUKI.Enums.Position.Manager;
                positionReader.Close();
                return determinedPosition;
            }
            else if(queryReturn.Equals("Admin")){
                determinedPosition = BUKI.Enums.Position.Admin;
                positionReader.Close();
                return determinedPosition;
            }
            else{
                positionReader.Close();
                return determinedPosition;
            }
        }
        public static DataSet GetRequestsByEmployeeID(int id)
        {
            string sqlcommand = "SELECT vehicle.vin, vehicle_requests.id, dealership.name as Destination, request_date, IF(approved = 0, \"Pending\" , \"Approved\") as approved, approved_by_emp FROM employee inner join vehicle ON employee.dealership = vehicle.dealership INNER JOIN vehicle_requests ON vehicle_requests.vin = vehicle.vin INNER JOIN dealership ON vehicle_requests.to_dealership = dealership.id WHERE employee.id = ?id";

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlcommand;
            cmd.Parameters.Add(new MySqlParameter("?id", id));
           MySqlDataAdapter DA = new MySqlDataAdapter(null, conn);
           DA.SelectCommand = cmd;
           DataSet Requests = new DataSet();
           DA.Fill(Requests, "Location");
          
           return Requests;
        }
        public static DataSet GetPendingSalesByEmployeeID(int id)
        {
            string sqlcommand = "SELECT sales.id, sales.vin, sales.sale_price, sales.sale_date,  sales.terms, sales.status,  sales.notes, SalesEmp.f_name, SalesEmp.l_name FROM employee INNER JOIN vehicle ON employee.dealership = vehicle.dealership INNER JOIN sales ON vehicle.vin = sales.vin LEFT OUTER JOIN employee as SalesEmp ON sales.salesman_id = employee.id WHERE employee.id = ?id ORDER BY sales.status desc, sale_date desc";


            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sqlcommand;
            cmd.Parameters.Add(new MySqlParameter("?id", id));
            MySqlDataAdapter DA = new MySqlDataAdapter(null, conn);
            DA.SelectCommand = cmd;
            DataSet Requests = new DataSet();
            DA.Fill(Requests, "Location");

            return Requests;
        }
        public static void SetSalesStatus(int id, string status)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE sales SET status = ?status WHERE id = ?id";
            cmd.Parameters.Add(new MySqlParameter("?id", id));
            cmd.Parameters.Add(new MySqlParameter("?status", status));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static void SetRequestStatus(int id, int status, int approvedBy)
        {
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE vehicle_requests SET approved = ?status, approved_by_emp = ?approvedBY WHERE id = ?id";
            cmd.Parameters.Add(new MySqlParameter("?id", id));
            cmd.Parameters.Add(new MySqlParameter("?status", status));
            cmd.Parameters.Add(new MySqlParameter("?approvedBy", approvedBy));

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}

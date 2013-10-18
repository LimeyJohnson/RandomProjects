using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace FriendsFlock.Web
{
    /// <summary>
    /// Summary description for Analytics
    /// </summary>
    [WebService(Namespace = "http://friendsflock.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Analytics : System.Web.Services.WebService
    {
        
        [WebMethod]
        public bool UpdateAnalytic(string sessionID, string columnName, string metric)
        {
            try
            {
                using (SqlConnection conn = GetConn())
                {
                    //Run the stored procedure to create a session if it doesn't exist
                    SqlCommand cmdVerify = conn.CreateCommand();
                    cmdVerify.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdVerify.CommandText = "VerifySession";
                    cmdVerify.Parameters.AddWithValue("@SessionID", sessionID);


                    SqlCommand cmd = conn.CreateCommand();
                    //Insert the data
                    cmd.CommandText = "UPDATE UserSessions SET [" + columnName + "] = @Metric WHERE SessionID = @SessionID";
                    cmd.Parameters.AddWithValue("@Metric", metric);
                    cmd.Parameters.AddWithValue("@SessionID", sessionID);
                    conn.Open();
                    cmdVerify.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }
            return true;

        }

        private SqlConnection GetConn()
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            sb.DataSource = "zwcag9dp75.database.windows.net";
            sb.UserID = "FFLogin";
            sb.Password = "Arsenal4Prez!";
            sb.Encrypt = true;
            sb.TrustServerCertificate = false;
            sb.InitialCatalog = "FriendsFlock";

            return new SqlConnection(sb.ToString());
        }
    }
}

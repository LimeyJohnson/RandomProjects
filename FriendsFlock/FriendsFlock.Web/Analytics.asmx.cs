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

        private void StartSession(string guid)
        {
            using (SqlConnection conn = GetConn())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO UserSessions ([SessionID], [Created]) VALUES (@Guid, SYSUTCDATETIME())";
                cmd.Parameters.AddWithValue("@Guid", guid);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        private void VerifySession(string guid)
        {
            bool returnVal;
            using (SqlConnection conn = GetConn())
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT SessionID FROM UserSessions WHERE [SessionID] = @Guid";
                cmd.Parameters.AddWithValue("@Guid", guid);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                returnVal = reader.HasRows;
            }
            if (!returnVal) StartSession(guid);
        }
        
        [WebMethod]
        public bool UpdateAnalytic(string sessionID, string columnName, string metric)
        {
            try
            {
                VerifySession(sessionID);
                using (SqlConnection conn = GetConn())
                {
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "UPDATE UserSessions SET ["+columnName+"] = @Metric WHERE SessionID = @SessionID";
                    cmd.Parameters.AddWithValue("@Metric", metric);
                    cmd.Parameters.AddWithValue("@SessionID", sessionID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                //Create an entry in the database and return the sessionID
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
            sb.DataSource = "zzhvpwvkxm.database.windows.net";
            sb.UserID = "FFLogin";
            sb.Password = "Arsenal4Prez!";
            sb.Encrypt = true;
            sb.TrustServerCertificate = false;
            sb.InitialCatalog = "FFStats";

            return new SqlConnection(sb.ToString());
        }
    }
}

using System;
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

public partial class Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Master.currLocation == null) Response.Redirect("SetLocation.aspx"); 
        //DataAccess DA = new DataAccess();
        //AddressLabel.Text = DA.GetDealerAddressNameById(1);
    }
}

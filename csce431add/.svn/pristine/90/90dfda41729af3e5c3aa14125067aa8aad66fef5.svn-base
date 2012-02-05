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
using BUKI;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sqlCommand = "select id, concat(name, \",   \", addr_city, \", \", addr_state) as visibleText from dealership order by addr_city";
            DataAccess.PopulateDropDownList(ref LocationDL, sqlCommand, "visibleText", "id");
        }
    }
    protected void LocationDL_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["location"] = LocationDL.SelectedValue;
        Response.Redirect("default.aspx");
    }
}

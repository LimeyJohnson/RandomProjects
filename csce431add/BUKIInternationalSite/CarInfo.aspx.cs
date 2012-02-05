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

public partial class CarInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = "BUKI Car Info Page";
        TextBox ChosenVin = (TextBox)Page.PreviousPage.FindControl("ChosenVin");
        Vehicle pickedVehicle = new Vehicle();
        String VIN = ChosenVin.Text;
    }
}

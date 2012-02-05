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

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        if (txt_password.Text != "" && txt_username.Text != "")
        {
            
            Employee emp = DataAccess.GetEmployeeByUsrnameAndPassword(txt_username.Text, txt_password.Text);

            if (emp != null)
            {
                Session["employee"] = emp.ID.ToString();
                Response.Redirect("Default.aspx");
            }
            else
            {
                lbl_error.Text = "Username/Password Incorrect";
            }
        }
        else lbl_error.Text = "Please type in your username and password";
    }
}

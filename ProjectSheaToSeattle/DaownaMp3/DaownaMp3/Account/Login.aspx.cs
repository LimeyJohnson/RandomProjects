using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DaownaMp3Library;

namespace DaownaMp3.Account
{
    public partial class _login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {            
            LoginCredentials currentCredentials = new LoginCredentials(UserName.Text, Password.Text);
            bool login = currentCredentials.Authenticate();

            if (login == true)
            {
                Session["ID"] = currentCredentials.ID;
                Response.Redirect(@"~/Member/Member.aspx");
            }
            else
                Response.Write("<script>alert('Account name and password did not match.');</script>");
        }
        protected void ForgotPasswordButton_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"~/Account/ForgotPassword.aspx");
        }
    }
}
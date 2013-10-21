using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using DaownaMp3Library;

namespace DaownaMp3.Account
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void EmailPassword_Click(object sender, EventArgs e)
        {
            LoginCredentials findPassword = new LoginCredentials(txtNameEmail.Text);
            if (findPassword.Username == "dne")
                Response.Write("<script>alert('No account match was found from input.');</script>");
            else
            {   
                findPassword.EmailLostPassword();

                Response.Redirect(@"~/Account/Login.aspx");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DaownaMp3Library;

namespace DaownaMp3.Account
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            if (AccountName.Text == "" || Password.Text == "" || Email.Text == "")
                Response.Write("<script>alert('A field was left blank. Please try again.');</script>");
            else
            {
                LoginCredentials registeringAccount = new LoginCredentials(AccountName.Text, Password.Text, Email.Text);
                if (registeringAccount.Register())
                {
                    //good for you
                    Response.Redirect(@"~/Default.aspx");
                }
                else
                    Response.Write("<script>alert('Account name or e-mail already in use.');</script>");
            }
        }
    }
}
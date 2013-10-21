using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SampleAspWebForms
{
    public partial class _default : System.Web.UI.Page
    {
       /* protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect(@"http://tubgirl.me");
        }*/

        //button event for click fires this method
        protected void btnMoveDataButton_Click(object sender, EventArgs e)
        {
            //get textbox control's current text
            string input = txtInputTextbox.Text;

            //validate something was inputted
            if (string.IsNullOrEmpty(input))
            {
                Response.Write("<script>alert('Please Input Something First');</script>");
            }
            else
            {
                string output = txtOutputTextbox.Text;
                txtOutputTextbox.Text = input + "\n" + output;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SampleAspWebForms
{
    public partial class page2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void submit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(input.Text))
            {
                result.Text = input.Text + " is a very nice, just all around great person and we are all glad to have known them.";
            }
        }
    }
}
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
public partial class ViewMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["mid"] == null || Master.currEmployee == null) Response.Redirect("Default.aspx");

        Message msg = Master.currEmployee.getMessage(int.Parse(Request.QueryString["mid"]));
        txt_message.Text = msg.Text;
        txt_sender.Text = msg.Sender;
        txt_sent_date.Text = msg.SentDate;
        txt_reciever.Text = msg.Receiver;
    }
}

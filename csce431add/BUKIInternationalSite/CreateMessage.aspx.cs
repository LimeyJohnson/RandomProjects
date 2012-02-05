using System;
using System.Collections;
using System.Collections.Generic;
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

public partial class CreateMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Master.currEmployee == null) Response.Redirect("Login.aspx"); // Make sure they are logged in
        List<Employee> emps = DataAccess.GetAllEmployees();
        foreach (Employee emp in emps) lbx_employee.Items.Add(new ListItem(emp.FirstName, emp.ID.ToString()));

    }
    protected void btn_send_Click(object sender, EventArgs e)
    {
        if (Master.currEmployee == null) Response.Redirect("Login.aspx");
        foreach (ListItem LI in lbx_employee.Items)
        {
            if (LI.Selected)
            {
                Message msg = new Message();
                msg.Text = txt_message.Text;
                Master.currEmployee.SendMessage(int.Parse(LI.Value), msg);
            }
        }
        Response.Redirect("Inbox.aspx");
    }
}

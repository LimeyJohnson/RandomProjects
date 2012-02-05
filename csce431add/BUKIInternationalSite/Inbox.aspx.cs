using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BUKI;
public partial class Inbox : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Master.currEmployee == null) Response.Redirect("Login.aspx"); // Make sure they are logged in
        grid_rec_messages.DataSource = Master.currEmployee.RecMessages;
        grid_rec_messages.DataBind();

        grid_sent_messages.DataSource = Master.currEmployee.SentMessages;
        grid_sent_messages.DataBind();

        if (Master.currEmployee.Position == Enums.Position.Admin || Master.currEmployee.Position == Enums.Position.Manager)
        {
            grid_tranfer_requests.DataSource = DataAccess.GetRequestsByEmployeeID(Master.currEmployee.ID);
            grid_tranfer_requests.DataBind();
            grid_tranfer_requests.Visible = true;

            grid_pending_sales.DataSource = DataAccess.GetPendingSalesByEmployeeID(Master.currEmployee.ID);
            grid_pending_sales.DataBind();
            grid_pending_sales.Visible = true;
            
        }
    }

    protected void btn_create_message_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateMessage.aspx");
    }
    protected void btn_delete_message_Click(object sender, EventArgs e)
    {
        if (Master.currEmployee == null) Response.Redirect("Login.aspx"); // Make sure they are logged in
        Button btn = (Button)sender;
        Master.currEmployee.DeleteMessage(int.Parse(btn.CommandArgument));
        grid_rec_messages.DataBind();
    }
    protected void btn_deny_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        DataAccess.SetSalesStatus(int.Parse(btn.CommandArgument), "DENIED");
        grid_pending_sales.DataSource = DataAccess.GetPendingSalesByEmployeeID(Master.currEmployee.ID);
        grid_pending_sales.DataBind();
    }
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        DataAccess.SetSalesStatus(int.Parse(btn.CommandArgument), "APPROVED");
         grid_pending_sales.DataSource = DataAccess.GetPendingSalesByEmployeeID(Master.currEmployee.ID);
        grid_pending_sales.DataBind();
    }
    protected void btn_request_deny_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        DataAccess.SetRequestStatus(int.Parse(btn.CommandArgument), 0, Master.currEmployee.ID);
        grid_tranfer_requests.DataSource = DataAccess.GetRequestsByEmployeeID(Master.currEmployee.ID);
        grid_tranfer_requests.DataBind();
    
    }
    protected void btn_request_approve_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        DataAccess.SetRequestStatus(int.Parse(btn.CommandArgument), 1, Master.currEmployee.ID);
        grid_tranfer_requests.DataSource = DataAccess.GetRequestsByEmployeeID(Master.currEmployee.ID);
        grid_tranfer_requests.DataBind();
    }
}

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using BUKI;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public Employee m_currEmployee;
    public int m_currLocation = 0;
    protected override void OnInit(EventArgs e)
    {
        if (Session["employee"] != null)
        {
            m_currEmployee = DataAccess.GetEmployeeByID(int.Parse(Session["employee"].ToString()));
        }

        if (Session["location"] != null)
        {
            m_currLocation = System.Convert.ToInt32(Session["location"]);
        }
        
        base.OnInit(e);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (m_currLocation == 0)
        {
            lbl_header.Text = "Buki International";
            Page.Title = "Buki International";

        }
        else
        {
            String dealername = DataAccess.GetDealerShipNameById(m_currLocation);
            lbl_header.Text = dealername;
            Page.Title = dealername;
        }
        if (currEmployee != null)
        {
            if (currEmployee.UnreadMessageCount > 0)
            {
                lnk_messages.Text = "(" + currEmployee.UnreadMessageCount + ") Inbox";

            }
            else lnk_messages.Text = "Inbox";
            lnk_messages.Visible = true;
            lnk_logout.Visible = true;
            lnk_login.Visible = false;
            td_login.Visible = false;
            lnk_mboard.Visible = true;
            lnk_EmpRoster.Visible = true;
            lnk_setLocation.Visible = true;

            if (m_currEmployee.Position == BUKI.Enums.Position.Admin || m_currEmployee.Position == BUKI.Enums.Position.Manager)
            {
                lnk_addRemoveEmp.Visible = true;
                lnk_salesHistory.Visible = true;
                lnk_inventorymanagement.Text = "Inventory Manangement";
                lnk_inventorymanagement.Visible = true;
            }
            if (m_currEmployee.Position == BUKI.Enums.Position.Admin)
            {
                HyperLink5.Visible = true;
            }
        }
    }

    protected void m_btnLogout_Click(object sender, EventArgs e)
    {
    }
    protected void lnk_logout_Click(object sender, EventArgs e)
    {
        Session["employee"] = null;
        Response.Redirect("default.aspx");
    }
    public Employee currEmployee
    {
        get { return m_currEmployee; }
    }
    public int currLocation
    {
        get { return m_currLocation; }
        set { m_currLocation = value; }
    }
}

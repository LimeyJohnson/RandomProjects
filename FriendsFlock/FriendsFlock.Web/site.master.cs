using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace FriendsFlock.Web
{

    public partial class site : System.Web.UI.MasterPage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ServerVariables.Init(HttpContext.Current.Request.ServerVariables["HTTP_HOST"]);
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Menu Support
            string url = Request.Url.Segments.Last<string>();

            if (url == "default.aspx")
                menuHome.Text = "class=\"active\"";
            else if (url == "about.aspx")
                menuAbout.Text = "class=\"active\"";
            else if (url == "support.aspx")
                menuSupport.Text = "class=\"active\"";
            else if (url == "privacy.aspx")
                menuPrivacy.Text = "class=\"active\"";

            HtmlMeta metaTag = new HtmlMeta();
            metaTag.Attributes.Add("Property", "fb:app_id");
            metaTag.Content = ServerVariables.AppID;
            Page.Header.Controls.AddAt(0, metaTag);


            string header = " <script type=\"text/javascript\"> ";

            string fbScript = @"
        function login() { 
            FB.login(function (response) {
            if (response.authResponse) {
                    window.location = " + "\"app.aspx\""+@";
                } else {
                }
            }, { scope: 'friends_birthday,friends_relationships,friends_relationship_details,friends_status' });
        };
        FB.init({
            appId: '" + ServerVariables.AppID + @"',
            status: true, 
            cookie: true, 
            xfbml: true,
            channelUrl: '"+ServerVariables.ChannelURL+@"',
            oauth: true});";

            string footer = " </script>";

            fbScriptLiteral.Text = header + fbScript + footer;

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "FBInit", fbScript, true);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "FBInit", fbScript, true);
        }
    }
}
using System;
using Facebook;
using Facebook.Web;

namespace FriendsFlock.Web
{
    public partial class app : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!FacebookWebContext.Current.IsAuthorized()) //Todo : add perms
            {
                //logged-out
                Response.Redirect("default.aspx");
            }
            else
            {
                //logged-in
                try
                {
                    var client = new FacebookClient(ServerVariables.AppID, ServerVariables.AppSecret);
                    string fbToken = FacebookWebContext.Current.Session.AccessToken;
                    long userUid = FacebookWebContext.Current.Session.UserId;

                    ParamInitParams.Text = String.Format("<param name=\"InitParams\" value=\"{0}, {1}\" />",
                    "fbToken=" + fbToken, "userUid=" + userUid.ToString());
                }
                catch (Exception ex)
                {
                    ParamInitParams.Text = String.Format("<param name=\"InitParams\" value=\"{0}, {1}\" />",
                        "fbToken=" + "Error", "userUid=" + ex.Message);
                }
            }
        }
    }
}
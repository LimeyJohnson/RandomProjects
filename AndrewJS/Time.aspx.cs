using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Time : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String[] names = { "Andrew Eric Johnson", "Ashley Andreas Johnson", "Laura Grace Johnson", "Hannah Kate Johnson", "Stephanie Elizabeth Artificavitch", "Emily Gayle Artificavitch", "Christopher Michael Artificavitch", "Andrew Dennis Artificavitch", "David Vincent Artificavitch", "Henry Alan Johnson", "Lorraine Landers Johnson", "Matthew Edwin Johnson", "James Alan Johnson", "Craig John Andreas", "Tracey Denise Urban", "Alicia Kathleen Bogel", "Michael Jarred Urban", "Krysten Nichole Urban" };
        string sub = Request.QueryString["sub"].ToLower();
        if (!String.IsNullOrEmpty(sub))
        {
            
            if (sub == "all") sub = "";
            int max = 0;
            string maxName = "";
            foreach (string name in names)
            {
                string compareName = name.ToLower();
                int charInCommon = 0;
                if (compareName != sub)
                {
                    foreach (char original in sub.ToCharArray())
                    {
                        if (compareName.Contains(original.ToString()) && original != ' ')
                        {
                            charInCommon++;
                            compareName = compareName.Replace(original, ' ');
                        }
                    }
                    if (charInCommon == max)
                    {
                        maxName += ", " + name;
                    }
                    if (charInCommon > max)
                    {
                        max = charInCommon;
                        maxName = name;
                    }
                }
                
                //
                //if (compareName.Contains(sub))
                //{
                //    Response.Write(name + "<br/>");
                //    x++;
                //}
                
            }
            Response.Write(maxName + "</br>" + max+" in common") ;
        }
    }
}
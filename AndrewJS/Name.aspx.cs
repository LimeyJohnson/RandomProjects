using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Time : System.Web.UI.Page
{
    private enum CompareType
    {
        Distinct, 
        Total
    }
    string[] names = { "Andrew Eric Johnson", "Ashley Andreas Johnson", "Laura Grace Johnson", "Hannah Kate Johnson", "Stephanie Elizabeth Artificavitch", "Emily Gayle Artificavitch", "Christopher Michael Artificavitch", "Andrew Dennis Artificavitch", "David Vincent Artificavitch", "Henry Alan Johnson", "Lorraine Landers Johnson", "Matthew Edwin Johnson", "James Alan Johnson", "Craig John Andreas", "Tracey Denise Urban", "Alicia Kathleen Bogel", "Michael Jarred Urban", "Krysten Nichole Smith", "Megan Alexander Smith" };
    protected void Page_Load(object sender, EventArgs e)
    {
        string type = null; 
        string sub = null;
        if(String.IsNullOrEmpty(Request.QueryString["type"])) type = Request.QueryString["type"].ToLower();
        if(String.IsNullOrEmpty(Request.QueryString["sub"]))   sub = Request.QueryString["sub"].ToLower();
        CompareType compareType = (CompareType) Enum.Parse(typeof(CompareType) , type);
        
        if (!String.IsNullOrEmpty(sub))
        {
            int max = 0;
            string maxName = "";
            foreach (string name in names)
            {
                int charInCommon = CompareName(sub, name,compareType);
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
            Response.Write(maxName + "</br>" + max + " in common");
        }
    }
    private int CompareName(string original, string compare, CompareType compareType)
    {

        original = original.ToLower();
        compare = compare.ToLower();
        int charInCommon = 0;
        if (compare != original)
        {
            foreach (char originalChar in original.ToCharArray())
            {
                int index;
                if ((index = compare.IndexOf(originalChar.ToString()))>=0 && originalChar != ' ')
                {
                    charInCommon++;
                    if(compareType == CompareType.Distinct)  compare = compare.Replace(originalChar, ' ');
                    if (compareType == CompareType.Total) compare.Remove(index, 1);
                }
            }
        }
        return charInCommon;
    }
}

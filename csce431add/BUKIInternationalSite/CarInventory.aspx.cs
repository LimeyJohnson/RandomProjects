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
using System.Text.RegularExpressions;
using System.Net;

public partial class CarInventory : System.Web.UI.Page
{
    
    private int m_nCurrentPage = 1;
    private int m_nCarsPerPage = 25;

    protected void Page_Load(object sender, EventArgs e)
    {
        ProcessPageRequest();
        BuildCarList();

        if (m_nCurrentPage > 1)
        {
            m_lnkbtnPrevPage.Enabled = true;
        }
        else
        {
            m_lnkbtnPrevPage.Enabled = false;
        }
    }

    private void ProcessPageRequest()
    {
        string strRequest = Request.Url.ToString();
        Regex regexpression = new Regex("([/?p]+)0*[1-9][0-9]*");
        Match isMatch = regexpression.Match( strRequest );
        if (isMatch.Success)
        {
            string temp = isMatch.Groups[0].ToString().Replace("?p","");
            m_nCurrentPage = int.Parse(temp);
        }
    }

    protected void BuildCarList()
    {
        int CarsToShow = m_nCarsPerPage;
        DataSet dsCars = DataAccess.GetAllCars();
        DataTable dtCarsInfo = dsCars.Tables[0];
        if (dtCarsInfo.Rows.Count < 25)
        {
            CarsToShow = dtCarsInfo.Rows.Count;            
        }

        int CurrentPosition = m_nCarsPerPage * (m_nCurrentPage - 1);

        if (dtCarsInfo.Rows.Count - CurrentPosition < 25)
        {
            CarsToShow = dtCarsInfo.Rows.Count - CurrentPosition;
        }

        //Check to see if we should enable/disable the Next Page Button
        if (dtCarsInfo.Rows.Count - m_nCurrentPage * m_nCarsPerPage > 0)
        {
            m_lnkbtnNextPage.Enabled = true;
        }
        else
        {
            m_lnkbtnNextPage.Enabled = false;
        }


        if (CarsToShow > 0)
        {
            for (int i = CurrentPosition; i < CurrentPosition + CarsToShow; i++)
            {
                DataRow drSingleCar = dtCarsInfo.Rows[i];
                TableRow newCarInfoRow = new TableRow();
                TableCell newCarInfoCell = new TableCell();
                TableCell PlaceHolderCell = new TableCell();
                TableCell ButtonHolderCell = new TableCell();
                //PlaceHolderCell.Style.Add(HtmlTextWriterStyle.Width, "20%");
                newCarInfoCell.Controls.Add(BuildSingleCarHTMLTable(drSingleCar));

                Button btn_BuyNow = new Button();
                btn_BuyNow.ID = "m_btnBuy" + drSingleCar["vin"].ToString();
                btn_BuyNow.Text = "Buy Now!";
                ButtonHolderCell.Controls.Add(btn_BuyNow);
                btn_BuyNow.Click += new EventHandler(BuyitNowClicked);
                

                newCarInfoRow.Cells.Add(PlaceHolderCell);
                newCarInfoRow.Cells.Add(newCarInfoCell);
                newCarInfoRow.Cells.Add(ButtonHolderCell);
                m_tblListofCars.Rows.Add(newCarInfoRow);
            }
        }
    }

    private Table BuildSingleCarHTMLTable(DataRow drSingleCar)
    {
        Table newCarInfoBox = new Table();
        newCarInfoBox.BorderWidth = 3;
        newCarInfoBox.GridLines = GridLines.Both;
        newCarInfoBox.Style.Add(HtmlTextWriterStyle.BorderColor, "Black");
        newCarInfoBox.BorderColor = System.Drawing.Color.Black;
        newCarInfoBox.CssClass = "CarInfoTable";
        newCarInfoBox.Style.Add(HtmlTextWriterStyle.MarginTop, "15px");
        
        TableRow trFirstRow = new TableRow();
        TableCell tcHeader = new TableCell();
        tcHeader.Text = drSingleCar["vehicle_year"].ToString() + " " + drSingleCar["make"] + " " + drSingleCar["model"];
        tcHeader.ColumnSpan = 3;
        tcHeader.CssClass = "CarName";
        trFirstRow.Cells.Add(tcHeader);
        newCarInfoBox.Rows.Add(trFirstRow);

        TableRow trSecondRow = new TableRow();
        TableCell tcSecondOne = new TableCell();
        tcSecondOne.Text = "Price:";
        trSecondRow.Cells.Add(tcSecondOne);
        TableCell tcSecondTwo = new TableCell();
        tcSecondTwo.Text = "$" + drSingleCar["market_price"].ToString();
        trSecondRow.Cells.Add(tcSecondTwo);
        TableCell tcSecondThree = new TableCell();
        Image CarPic = new Image();
        CarPic.ImageUrl = drSingleCar["display_photo"].ToString();
        CarPic.Width = 350;
        CarPic.Height = 250;
        tcSecondThree.Width = 350;

        tcSecondThree.Controls.Add(CarPic);
        tcSecondThree.RowSpan = 3;
        trSecondRow.Cells.Add(tcSecondThree);
        newCarInfoBox.Rows.Add(trSecondRow);

        TableRow trThirdRow = new TableRow();
        TableCell tcThirdOne = new TableCell();
        tcThirdOne.Text = "MPG:";
        trThirdRow.Cells.Add(tcThirdOne);
        TableCell tcThirdTwo = new TableCell();
        tcThirdTwo.Text = drSingleCar["mpg"].ToString();
        trThirdRow.Cells.Add(tcThirdTwo);
        newCarInfoBox.Rows.Add(trThirdRow);

        TableRow trFourthRow = new TableRow();
        TableCell tcFourthOne = new TableCell();
        tcFourthOne.Text = "Color:";
        trFourthRow.Cells.Add(tcFourthOne);
        TableCell tcFourthTwo = new TableCell();
        tcFourthTwo.Style.Add(HtmlTextWriterStyle.BackgroundColor, drSingleCar["color"].ToString());
        trFourthRow.Cells.Add(tcFourthTwo);
        newCarInfoBox.Rows.Add(trFourthRow);

        return newCarInfoBox;
    }

    protected void m_lnkbtnPrevPageClicked(object sender, EventArgs e)
    {
        //HttpWebRequest newRequest = (HttpWebRequest)WebRequest.Create(Request.Url.AbsolutePath + "?p" + (m_nCurrentPage - 1).ToString());
        //System.Web.HttpContext.Current.RewritePath(Request.Url.AbsolutePath + "?p" + (m_nCurrentPage - 1).ToString());
        HttpContext.Current.Response.Redirect(Request.Url.AbsolutePath + "?p" + (m_nCurrentPage - 1).ToString());
    }

    protected void m_lnkbtnNextPageClicked(object sender, EventArgs e)
    {
        //System.Web.HttpContext.Current.RewritePath(Request.Url.AbsolutePath + "?p" + (m_nCurrentPage + 1).ToString());
        HttpContext.Current.Response.Redirect(Request.Url.AbsolutePath + "?p" + (m_nCurrentPage + 1).ToString());
    }

    protected void BuyitNowClicked(object sender, EventArgs e)
    {
        Button btnSender = (Button)sender;
        string strVin = btnSender.ID.Replace("m_btnBuy", "");
        string strCurrentPage = Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(strCurrentPage); 
        HttpContext.Current.Response.Redirect(Request.Url.AbsolutePath.Replace(oInfo.Name,"BuyNow.aspx") + "?Car=" + strVin);
    }
}

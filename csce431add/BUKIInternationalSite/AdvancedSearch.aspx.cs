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
using MySql.Data.MySqlClient;
using BUKI;

    public partial class AdvancedSearch : System.Web.UI.Page
    {
        private int m_nCurrentPage = 1;
        private int m_nCarsPerPage = 100;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string sqlCommand = "select id, concat(name, \",   \", addr_city, \", \", addr_state) as visibleText from dealership order by addr_city";
                DataAccess.PopulateDropDownList(ref LocationDL, sqlCommand, "visibleText", "id");

                sqlCommand = "select vin from vehicle order by vin";
                DataAccess.PopulateDropDownList(ref VinDL, sqlCommand, "vin", "vin");

                sqlCommand = "select valid, id from valid_vehicle_types";
                DataAccess.PopulateDropDownList(ref VehicleTypeDL, sqlCommand, "valid", "id");

                sqlCommand = "select description, hex_color from color group by description order by description";
                DataAccess.PopulateDropDownList(ref ColorDL, sqlCommand, "description", "hex_color");
            }
        }
        protected void btnAdvSearch_Click(object sender, EventArgs e)
        {
            SearchError.Text = "";
            String[] args = SearchBox.Text.Split(", \t\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            BuildCarListFromQuery(args);
        }
        protected void BuildCarListFromQuery(String[] args)
        {
            int CarsToShow = m_nCarsPerPage;
            String advancedOptions = "";
            if (LocationDL.SelectedItem.Text != "")
                advancedOptions += " AND vehicle.dealership = " + LocationDL.SelectedValue;
            if (VinDL.SelectedItem.Text != "")
                advancedOptions += " AND vehicle.vin = \"" + VinDL.SelectedValue + "\"";
            if (ColorDL.SelectedItem.Text != "")
                advancedOptions += " AND vehicle.color = \"" + ColorDL.SelectedValue + "\"";
            if (VehicleTypeDL.SelectedItem.Text != "")
                advancedOptions += " AND vehicle_type.vehicle_type = \"" + VehicleTypeDL.SelectedValue + "\"";
            if (args.Count() == 0 && advancedOptions.Equals(""))
            {
                SearchError.Text = "Please enter a search term(s) and/or choose filters below";
                return;
            }
            DataSet dsCars = DataAccess.GetSearchedCars(args, advancedOptions);
            DataTable dtCarsInfo = dsCars.Tables[0];
            if (dtCarsInfo.Rows.Count == 0)
            {
                SearchError.Text = "No vehicles found matching the information given";
                return;
            }
            if (dtCarsInfo.Rows.Count < m_nCarsPerPage)
            {
                CarsToShow = dtCarsInfo.Rows.Count;
            }

            int CurrentPosition = m_nCarsPerPage * (m_nCurrentPage - 1);

            if (dtCarsInfo.Rows.Count - CurrentPosition < 25)
            {
                CarsToShow = dtCarsInfo.Rows.Count - CurrentPosition;
            }

            if (CarsToShow > 0)
            {
                for (int i = CurrentPosition; i < CurrentPosition + CarsToShow; i++)
                {
                    DataRow drSingleCar = dtCarsInfo.Rows[i];
                    TableRow newCarInfoRow = new TableRow();
                    TableCell newCarInfoCell = new TableCell();
                    TableCell PlaceHolderCell = new TableCell();
                    //PlaceHolderCell.Style.Add(HtmlTextWriterStyle.Width, "20%");
                    newCarInfoCell.Controls.Add(BuildSingleCarHTMLTable(drSingleCar));

                    newCarInfoRow.Cells.Add(PlaceHolderCell);
                    newCarInfoRow.Cells.Add(newCarInfoCell);
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
            HyperLink titleLink = new HyperLink();
            titleLink.Text  = drSingleCar["vehicle_year"].ToString() + " " + drSingleCar["make"] + " " + drSingleCar["model"];
            titleLink.NavigateUrl = "BuyNow.aspx?Car="+drSingleCar["vin"].ToString();
            tcHeader.Controls.Add(titleLink);
            tcHeader.ColumnSpan = 3;
            tcHeader.CssClass = "CarName";
            trFirstRow.Cells.Add(tcHeader);
            newCarInfoBox.Rows.Add(trFirstRow);

            TableRow trSecondRow = new TableRow();
            TableCell tcSecondOne = new TableCell();
            tcSecondOne.Text = "Price :";
            trSecondRow.Cells.Add(tcSecondOne);
            TableCell tcSecondTwo = new TableCell();
            tcSecondTwo.Text = "$ " + drSingleCar["market_price"].ToString();
            trSecondRow.Cells.Add(tcSecondTwo);

            TableCell tcSecondThree = new TableCell();
            Image CarPic = new Image();
            CarPic.ImageUrl = drSingleCar["display_photo"].ToString();
            CarPic.Height = 200;
            CarPic.Width = 250;
            tcSecondThree.Controls.Add(CarPic);
            tcSecondThree.RowSpan = 3;
            trSecondRow.Cells.Add(tcSecondThree);
            newCarInfoBox.Rows.Add(trSecondRow);

            TableRow trThirdRow = new TableRow();
            TableCell tcThirdOne = new TableCell();
            tcThirdOne.Text = "MPG :";
            trThirdRow.Cells.Add(tcThirdOne);
            TableCell tcThirdTwo = new TableCell();
            tcThirdTwo.Text = drSingleCar["mpg"].ToString();
            trThirdRow.Cells.Add(tcThirdTwo);
            newCarInfoBox.Rows.Add(trThirdRow);

            TableRow trFourthRow = new TableRow();
            TableCell tcFourthOne = new TableCell();
            tcFourthOne.Text = "Color :";
            trFourthRow.Cells.Add(tcFourthOne);
            TableCell tcFourthTwo = new TableCell();
            tcFourthTwo.Style.Add(HtmlTextWriterStyle.BackgroundColor, drSingleCar["color"].ToString());
            trFourthRow.Cells.Add(tcFourthTwo);
            newCarInfoBox.Rows.Add(trFourthRow);

            return newCarInfoBox;
        }
}


<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdvancedSearch.aspx.cs" 
Inherits="AdvancedSearch" Title="BUKI Advanced Search" %>


<asp:Content ID="AdvancedSearch" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class ="Contents">
        <div class="ImageCentering">
        
            <asp:Image ID="AdvancedSearchBanner" runat="server" ImageUrl="advancedSearch.jpg"
                                    Height="300px" ImageAlign="Middle"
            />
            <br />
            <br />
            <center>
                <FONT size = 4><b><u>Search Term:</u></b> &nbsp; &nbsp; </FONT>
                            <asp:TextBox
                            id = "SearchBox"
                            width = "50%"
                            rows = "10"
                            maxlength = "1000"
                            enabled = "true"
                            readonly = "false"
                            toolTip = "Query for Advanced Search"
                            runat = "server"
                  /> &nbsp;
                       <asp:Button
                            id = "AdvSearch"
                            text = "Search"
                            onClick = "btnAdvSearch_Click"
                            enabled = "true"
                            visible = "true"
                            toolTip = "Start Query!"
                            runat = "server"
                />
            </center>
            <br /> 
            <div class ="DBOperationStatus">
                <asp:Label
                     id = "SearchError"
                     width = "100%"
                     runat = "server"
                     text = ""
                     AutoPostBack = "true"
                />
                </div>
            <br />
            <h2><u>Advanced Options:</u></h2>
        </div> 
        <br />
        <br />
        <div class="AdvancedSearchLeft">    
            <FONT size = 3><b>Location:</b> &nbsp; &nbsp; </FONT>
            <asp:DropDownList 
                             id="LocationDL"
                             runat = "server" 
                             width = "60%"
                 >
                     
            </asp:DropDownList>
            <br />
            <br />
            <FONT size = 3><b>Vehicle Type:</b> &nbsp; &nbsp; </FONT>
            <asp:DropDownList 
                             id="VehicleTypeDL"
                             runat = "server"   
                 >
                     <asp:ListItem Selected="True" Value="" Text=""/>
                     <asp:ListItem Value="TestData2">Utility</asp:ListItem>
                     <asp:ListItem Value="TestData3">Boat</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <%--<FONT size = 3><b>Price Range:</b> &nbsp; &nbsp; </FONT>
            <asp:DropDownList 
                             id="PriceRangeDL"
                             runat = "server"   
                 >
                     <asp:ListItem Selected="True" Value="" Text=""/>
                     <asp:ListItem Value="TestData2"><=$4000</asp:ListItem>
                     <asp:ListItem Value="TestData3">>$4000</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <br />--%>
        </div>
        <div class="AdvancedSearchRight">
            <FONT size = 3><b>Color:</b> &nbsp; &nbsp; </FONT>
            <asp:DropDownList 
                             id="ColorDL"
                             runat = "server"
                             width = "40%"   
                 >
                     <asp:ListItem Selected="True" Value="" Text=""/>
                     <asp:ListItem Value="TestData2">Red</asp:ListItem>
                     <asp:ListItem Value="TestData3">Green</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
                <FONT size = 3><b>VIN:</b> &nbsp; &nbsp; </FONT>
            <asp:DropDownList 
                             id="VinDL"
                             runat = "server" 
                             width = "40%"  
                 >
                     <asp:ListItem Selected="True" Value="" Text=""/>
            </asp:DropDownList>
            <br />
            <br />
            <br />
            <br />
        </div>
        <div>
        
            &nbsp;&nbsp;&nbsp;
            <asp:Table ID="m_tblListofCars" runat="server" BorderWidth="0" Height="16px" 
                style="margin-left:20%">
            </asp:Table>
            </div>
  </div>
</asp:Content>

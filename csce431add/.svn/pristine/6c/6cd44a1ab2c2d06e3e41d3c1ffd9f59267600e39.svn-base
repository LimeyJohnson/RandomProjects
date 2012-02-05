<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeSalesHistory.aspx.cs" 
Inherits="EmployeeSalesHistory" Title="BUKI Employee Sales History" %>

<asp:Content ID="AdvancedSearch" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Contents">
        <table border="0">
            <tr>
            <td>
             <asp:Image ID="Image1" runat="server" ImageUrl="money_car_3.jpg"
                            Height="200px" Width="200px" Style="margin-right: 10px" border="1" hspace="20" 
                            /></td>
            <td><h1><u>Employee Sales History</h1></u></td>
            <td>
               <asp:Image ID="MoneyCar" runat="server" ImageUrl="money_car_3.jpg"
                            Height="200px" Width="200px" Style="margin-right: 10px" border="1" hspace="20" 
                            /></td>
            </tr>
        </table>
        <br />
        <br />
        <table border="0">
            <tr>
               <td>
               <asp:DropDownList 
                                     id="EmployeeDL"
                                     runat = "server"   
                         >
                             <asp:ListItem Selected="True" Value="" Text=""/>
                        </asp:DropDownList>
               </td>
               <td>
               <asp:Button
                                id = "GatherSalesHistoryBtn"
                                text = "Get Sales History"
                                onClick = "btnGatherSalesHistory_Click"
                                enabled = "true"
                                visible = "true"
                                toolTip = "Gather Sales History for this Employee"
                                runat = "server"
                    />
               </td> 
            </tr>
        </table>
        <br />
        <br />
        <br />      
         <asp:GridView ID="GridView1" Runat="server" 
               AutoGenerateColumns="False" 
              BorderWidth="1px" BackColor="White" CellPadding="3" BorderStyle="None" 
              BorderColor="#CCCCCC" Font-Names="Arial">
                <FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
                <PagerStyle ForeColor="#000066" HorizontalAlign="Left" 
                  BackColor="White"></PagerStyle>
                <HeaderStyle ForeColor="White" Font-Bold="True" 
                  BackColor="#006699"></HeaderStyle>
                <Columns>
                <asp:BoundField HeaderText = "SalesMan" DataField = "Name"/>
                    <asp:BoundField HeaderText="VIN" DataField="vin"></asp:BoundField>
                    <asp:BoundField HeaderText="Sale Price" DataField="sale_price"></asp:BoundField>
                    <asp:BoundField HeaderText="Sale Date" DataField="sale_date"></asp:BoundField>
                    <asp:BoundField HeaderText="Status" DataField="status"></asp:BoundField>
                </Columns>
                <SelectedRowStyle ForeColor="White" Font-Bold="True" 
                   BackColor="#669999"></SelectedRowStyle>
                <RowStyle ForeColor="#000066"></RowStyle>
           </asp:GridView>
    </div>
</asp:Content>
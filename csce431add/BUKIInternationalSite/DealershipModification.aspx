<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DealershipModification.aspx.cs" 
Inherits="DealershipModification" Title="BUKI Dealership Modification" %>

<script runat= "server">
    void btnViewData_Click(Object Source, EventArgs E){
    }
</script> 

<asp:Content ID="DealershipModification" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="Contents">
    
        <h1>Employee Roster</h1>
                     <asp:DropDownList 
                                     id="DealershipDL"
                                     runat = "server"   
                         >
                        </asp:DropDownList>
                &nbsp; &nbsp;
                     <asp:Button
                                id = "DealershipSearch"
                                text = "Search Dealership"
                                onClick = "btnViewData_Click"
                                enabled = "true"
                                visible = "true"
                                toolTip = "Search"
                                runat = "server"
                    />
                    
           <br />
           <h4>Salesmen: </h4>
           <asp:Datagrid runat="server" id="SalesmenGrid" CellPadding=5>
                <HeaderStyle Font-Bold="true" BackColor="#CCCCCC" />
           </asp:Datagrid>
           <br />
           <h4>Managers: </h4>
           <asp:Datagrid runat="server" id="ManagersGrid" CellPadding=5>
                <HeaderStyle Font-Bold="true" BackColor="#CCCCCC" />
           </asp:Datagrid>    
           <br />
           <h4>Admins: </h4>
           <asp:Datagrid runat="server" id="AdminsGrid" CellPadding=5>
                <HeaderStyle Font-Bold="true" BackColor="#CCCCCC" />
           </asp:Datagrid>    
           <br />  
    
    </div>
    
</asp:Content>
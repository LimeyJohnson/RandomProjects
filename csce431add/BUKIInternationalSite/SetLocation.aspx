<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SetLocation.aspx.cs" Inherits="Default2" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="Div1" runat = "server">
<FONT size = 3><b>Input Your Current Location:</b> &nbsp; &nbsp; </FONT>
            <asp:DropDownList 
                             id="LocationDL"
                             runat = "server" 
                             width = "60%" onselectedindexchanged="LocationDL_SelectedIndexChanged"
                             AutoPostBack = "true"
                             
                 >
                     
            </asp:DropDownList>
</div>

</asp:Content>


<%@ Page Title="Default Page Title" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="SampleAspWebForms._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Default.aspx = Default Webpage</h1>
    <!-- This is a Comment --->
    <br /> <!--- Return Line --->
    <asp:Button ID="btnMoveDataButton" runat="server" Text="Click Me" OnClick="btnMoveDataButton_Click" />
    <br />
    Input Textbox:
    <br />
    <asp:TextBox ID="txtInputTextbox" runat="server" Width="600"></asp:TextBox>
    <br />
    Output Textbox:
    <br />
    <asp:TextBox ID="txtOutputTextbox" runat="server" Width="600" Height="100" TextMode="MultiLine"></asp:TextBox>
    <br />    
    <a href="ASPNameChange.aspx">Name Change via ASP.Net</a>
    <br />
    <a href="HtmlJSNameChange.html">Name Change via HTML/JS</a>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="page2.aspx.cs" Inherits="SampleAspWebForms.page2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>page2</h1>
    <br />
    <h2>Name Change via ASP.Net </h2>
    <div>
        Please Enter your name:
        <br />
        <asp:TextBox ID="input" runat="server" Width="265px"></asp:TextBox>
        <br />
        <asp:Button ID="submit" runat="server" Text="Button" OnClick="submit_Click" />
        <br />
        <asp:TextBox ID="result" runat="server" Width="656px"></asp:TextBox>
    </div> 
</asp:Content>

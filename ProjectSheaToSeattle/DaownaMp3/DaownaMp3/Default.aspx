<%@ Page Title="Hello World" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="DaownaMp3._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>                
            </hgroup>
            <p>
                This color scheme is coming off as 50 Shades of Grey.. with a blue blob in the middle.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Some random stuff here:</h3>
    <p>
        <!--<asp:GridView ID="GridView1" runat="server" AutoGenerateEditButton="True" AutoGenerateColumns="True" DataKeyNames="ID"  DataSourceID="DaownaMp3" AllowSorting="True">
        </asp:GridView>
        <asp:SqlDataSource ID="DaownaMp3" runat="server" ConnectionString="<%$ ConnectionStrings:DaownaMp3ConnectionString %>" SelectCommand="SELECT [ID], [UserName], [DateRegistered], [Password], [Active], [Email], [Admin] FROM [User]" DeleteCommand="DELETE FROM [User] WHERE [ID] = @ID" InsertCommand="INSERT INTO [User] ([ID], [UserName], [DateRegistered], [Password], [Active], [Email], [Admin]) VALUES (@ID, @UserName, @DateRegistered, @Password, @Active, @Email, @Admin)" UpdateCommand="UPDATE [User] SET [UserName] = @UserName, [DateRegistered] = @DateRegistered, [Password] = @Password, [Active] = @Active, [Email] = @Email, [Admin] = @Admin WHERE [ID] = @ID">
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int32"></asp:Parameter>
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="ID" Type="Int32"></asp:Parameter>
                <asp:Parameter Name="UserName" Type="String"></asp:Parameter>
                <asp:Parameter DbType="Date" Name="DateRegistered"></asp:Parameter>
                <asp:Parameter Name="Password" Type="String"></asp:Parameter>
                <asp:Parameter Name="Active" Type="Boolean"></asp:Parameter>
                <asp:Parameter Name="Email" Type="String"></asp:Parameter>
                <asp:Parameter Name="Admin" Type="Boolean"></asp:Parameter>
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="UserName" Type="String"></asp:Parameter>
                <asp:Parameter DbType="Date" Name="DateRegistered"></asp:Parameter>
                <asp:Parameter Name="Password" Type="String"></asp:Parameter>
                <asp:Parameter Name="Active" Type="Boolean"></asp:Parameter>
                <asp:Parameter Name="Email" Type="String"></asp:Parameter>
                <asp:Parameter Name="Admin" Type="Boolean"></asp:Parameter>
                <asp:Parameter Name="ID" Type="Int32"></asp:Parameter>
            </UpdateParameters>
        </asp:SqlDataSource>-->
    </p>
</asp:Content>
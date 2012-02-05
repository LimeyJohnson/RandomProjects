<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    EnableEventValidation="false" CodeFile="Inbox.aspx.cs" Inherits="Inbox" %>

<%@ MasterType VirtualPath="~/MasterPage.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Button ID="btn_create_message" runat="server" Text="Create Message" OnClick="btn_create_message_Click" />
    <h2>
        Recieved Message</h2>
    <asp:GridView ID="grid_rec_messages" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container,"DataItem.Sender") %>
                </ItemTemplate>
                <HeaderTemplate>
                    Sender</HeaderTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Message</HeaderTemplate>
                <ItemTemplate>
                    <a href="ViewMessage.aspx?mid=<%# DataBinder.Eval(Container,"DataItem.ID") %>">
                        <%# DataBinder.Eval(Container,"DataItem.ShortText") %></a></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Sent Date</HeaderTemplate>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container,"DataItem.SentDate") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container,"DataItem.Read") %>
                </ItemTemplate>
                <HeaderTemplate>
                    Read</HeaderTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btn_delete" CommandArgument='<%# DataBinder.Eval(Container,"DataItem.ID") %>'
                        runat="server" Text="delete" OnClick="btn_delete_message_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <h2>
        Sent Message</h2>
    <asp:GridView ID="grid_sent_messages" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container,"DataItem.Sender") %>
                </ItemTemplate>
                <HeaderTemplate>
                    Sender</HeaderTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Message</HeaderTemplate>
                <ItemTemplate>
                    <a href="ViewMessage.aspx?mid=<%# DataBinder.Eval(Container,"DataItem.ID") %>">
                        <%# DataBinder.Eval(Container, "DataItem.ShortText")%></a></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Sent Date</HeaderTemplate>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container,"DataItem.SentDate") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <%# DataBinder.Eval(Container,"DataItem.Read") %>
                </ItemTemplate>
                <HeaderTemplate>
                    Read</HeaderTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <h2>
        Pending Sales</h2>
    <asp:GridView ID="grid_pending_sales" runat="server" AutoGenerateColumns="false"
        Visible="false">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    VIN</HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("vin")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Sales Price</HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("sale_price")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Sales Date</HeaderTemplate>
                <ItemTemplate>
                    <%# DateTime.Parse(Eval("sale_date").ToString()).ToString("d")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Salesman</HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("f_name").ToString()+ Eval("l_name").ToString()%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Status</HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("status") %>
                </ItemTemplate>
            </asp:TemplateField>
           
            <asp:TemplateField>
                <HeaderTemplate>
                    Notes</HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("notes") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Action</HeaderTemplate>
                <ItemTemplate>
                    <asp:Button runat="server" ID="Deny" Text="Deny" CommandArgument='<%# Eval("id") %>'
                        OnClick="btn_deny_Click" />
                    <asp:Button runat="server" ID="Approve" Text="Approve" CommandArgument='<%# Eval("id") %>'
                        OnClick="btn_approve_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <h2>
        Pending Transfer Requests</h2>
    <asp:GridView ID="grid_tranfer_requests" runat="server" AutoGenerateColumns="false"
        Visible="false">
         <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    VIN</HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("vin")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                   Destination</HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("Destination")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Request Date</HeaderTemplate>
                <ItemTemplate>
                    <%# DateTime.Parse(Eval("request_date").ToString()).ToString("d")%></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    Approved</HeaderTemplate>
                <ItemTemplate>
                    <%# Eval("approved") %></ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField>
                <HeaderTemplate>
                    Action</HeaderTemplate>
                <ItemTemplate>
                    <asp:Button runat="server" ID="Deny" Text="Deny" CommandArgument='<%# Eval("id") %>'
                        OnClick="btn_request_deny_Click" />
                    <asp:Button runat="server" ID="Approve" Text="Approve" CommandArgument='<%# Eval("id") %>'
                        OnClick="btn_request_approve_Click" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

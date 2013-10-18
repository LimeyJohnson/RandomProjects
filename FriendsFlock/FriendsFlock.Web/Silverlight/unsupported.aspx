<%@ Page Title="" Language="C#" MasterPageFile="~/Silverlight/silverlight.Master" AutoEventWireup="true" CodeBehind="unsupported.aspx.cs" Inherits="FriendsFlock.Web.Silverlight.unsupported" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <section id="content">
		<div class="wrapper" >
		<article class="col-1 maxheight bg">
		    <div class="col-indent">
                <h2>Silverlight Unsupported</h2>
                <div class="wrapper">
			        <span class="title">Supported Devices</span> 
                    <div>
                        <img src="/images/marker.gif" alt="Alternate Text" />
                        Windows based Computers
                    </div>
                    <div style="margin-top:4px">
                        <img src="/images/marker.gif" alt="Alternate Text" />
                        Mac based Computers
                    </div>  
                </div>
                <br />
                <div class="wrapper">
			        <span class="title">Unsupported Devices</span> 
                    <div>
                        <img src="/images/marker.gif" alt="Alternate Text" />
                        No Cell Phones (iPhone, Android, Windows 7 Phone…)
                    </div>
                    <div style="margin-top:4px">
                        <img src="/images/marker.gif" alt="Alternate Text" />
                        No Tablets (iPad, Android…)
                    </div>    
                </div>
	    </article>
	    <article class="col-2 maxheight bg1">
	        <div class="col-indent1">
		        <h3 class="h3-pad">Silverlight</h3>
		        <div class="wrapper">
			        <span class="title">What is Silverlight?</span> 
			        Silverlight is a browser plugin used by Microsoft applications. You can read more 
                    about Silverlight on <a href="http://www.microsoft.com/silverlight/faq/" target="_blank">Microsoft's website</a> and on 
                    <a href="http://en.wikipedia.org/wiki/Silverlight/" target="_blank">Wikipedia</a>.
		        </div>
		        <div class="wrapper pad">
			        <span class="title">Why do I have to install Silverlight?</span> 
			        Friends Flock uses advanced features in its clustering algorithm and media controls that are not easily accessible without Silverlight.
		        </div>
		        <div class="wrapper pad">
		        </div>
            </div>
        </article>
	    </div>
    </section>
    </div>
</asp:Content>

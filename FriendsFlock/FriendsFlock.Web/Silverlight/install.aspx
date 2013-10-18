<%@ Page Title="" Language="C#" MasterPageFile="~/Silverlight/silverlight.Master" AutoEventWireup="true" CodeBehind="install.aspx.cs" Inherits="FriendsFlock.Web.Silverlight.install" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //INSTALL CLICKED, NEED REFRESH
        function InstallClicked() {
            window.location = "http://go2.microsoft.com/fwlink/?linkid=124807";
        }

        function BackClicked() {
            window.location = "http://www.friendsflock.com/app.aspx";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="content">
		<div class="wrapper" >
		<article class="col-1 maxheight bg">
		    <div class="col-indent">
                <h2>Installing Silverlight</h2>
                <div class="wrapper" onclick="InstallClicked()" style="cursor: pointer">
							<div class="date">
								<span>1</span>Step
                            </div>
                        <img 
                            src="http://go.microsoft.com/fwlink/?LinkId=161376" 
                            alt="Get Microsoft Silverlight" 
                            style="border-style: none"/>
                </div>
                <div class="wrapper">
						<div class="date" onclick="BackClicked()" style="cursor: pointer">
							<span>2</span>Step
						</div>
                        <div class="title">
                        <%--<img src="/images/marker.gif" alt="Alternate Text" />--%>
						After installing, please press back on your browser or click <a href="http://www.friendsflock.com/app.aspx" style="color:#ff0042">here</a>.
                        </div>
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
			        <span class="title">After I install Silverlight?</span> 
                    Please press back on your browser or click <a href="http://www.friendsflock.com/app.aspx">here</a>.
		        </div>
            </div>
        </article>
	    </div>
    </section>
</asp:Content>
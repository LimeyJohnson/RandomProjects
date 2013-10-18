<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="FriendsFlock.Web._default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>friends flock</title>
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-25437372-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <section id="content">
			 <div class="wrapper">
			 	<article class="col-1 maxheight bg">
					<div class="col-indent">
                        <h2>Video Demo</h2>
                        <iframe width="480" height="390" src="http://www.youtube.com/embed/K-wo3paK6xE" frameborder="0" allowfullscreen></iframe>
                        <br /><br /><br />
						<span class="title">Friends Flock</span> 
                        This website builds a map of your friends and clusters them into flocks.
                        See how your friends inter-connect!
                        Friends Flock is still in the beta testing phase. This means things may not work exactly as planned. 
                        Hope you enjoy it and share it with your friends! 
					</div>
				</article>
				<article class="col-2 maxheight bg1">
					<div class="col-indent1">
						<h3 class="h3-pad">Getting Started</h3>
						<div class="wrapper">
							<div class="date">
								<span>1</span>Step
							</div>
							Log in by clicking the Facebook icon in the top right hand corner.
						</div>
						<div class="wrapper pad">
							<div class="date">
								<span>2</span>Step
							</div>
							You will be prompted login to Facebook and to authorize Friends Flock.
                            <br /><br />
                            Friends Flock doesn’t view, save or share any of your information. 
						</div>
						<div class="wrapper pad">
							<div class="date">
								<span>3</span>Step
                            </div>
                            Lastly, if you don’t already have 
                            <a href="http://en.wikipedia.org/wiki/Microsoft_Silverlight" target="_blank">Silverlight</a> you will need to 
                            <a href="http://go.microsoft.com/fwlink/?LinkID=149156" target="_blank">install</a> it.
                            <br /><br />
                            Friends Flock will only work on a Windows or Mac computer (no cell phones or tablets).
						</div>
						<%--<div class="border1"></div>
						<h3 class="h3-pad1">Blah, Blah, Blah...</h3>
						Blah, Blah, Blah...</div>--%>
                        </div>
				</article>
			 </div>
		</section>
    </div>
</asp:Content>

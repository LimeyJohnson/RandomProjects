// Class1.cs
//

using System;
using System.Html;

using jQueryApi;
using System.Collections;
using FreindsLibrary;
using D3Wrapper;
using System.Html.Media.Graphics;
namespace JSFFScript
{

    internal static class FFJS
    {
        public static string UserID;
        public static Dictionary Friends = new Dictionary();
        public static bool debug = false;
        static FFJS()
        {
            jQuery.OnDocumentReady(new Action(Onload));
        }
        public static void ButtonClicked(jQueryEvent e)
        {
            LoginOptions options = new LoginOptions();
            options.scope = "email, user_likes, publish_stream";
            Facebook.login(delegate(LoginResponse response) { }, options);
        }
        public static void GraphFriends(jQueryEvent e)
        {
            Array nodes = new Array();
            Array links = new Array();
            ApiOptions options = new ApiOptions();
            Facebook.api("/me/friends", delegate(ApiResponse apiResponse)
            {
                for (int x = 0; x < apiResponse.data.Length; x++)
                {
                    Friend friend = new Friend(apiResponse.data[x], x);
                    Friends[friend.id] = friend;
                    Node noeNode = new Node();
                    noeNode.Name = friend.name;
                    noeNode.Group = 1;
                    nodes[nodes.Length] = noeNode;

                }
                Queries q = new Queries();
                q.friendsLimit = "SELECT uid1, uid2 from friend WHERE uid1 = " + UserID + " ORDER BY uid2";
                q.friendsAll = "SELECT uid1, uid2 from friend WHERE uid1 = " + UserID;
                q.friendsoffriends = "SELECT uid1, uid2 FROM friend WHERE uid1 IN (SELECT uid2 from #friendsLimit) AND uid2 IN (SELECT uid2 from #friendsAll) AND uid1 < uid2";

                ApiOptions queryOptions = new ApiOptions();
                queryOptions.method = "fql.multiquery";
                queryOptions.queries = q;


                Facebook.api(queryOptions, delegate(QueryResponse[] queryResponse)
                {
                    if (debug) Script.Alert(queryResponse[2].fql_result_set.Length);
                    for (int i = 0; i < queryResponse[2].fql_result_set.Length; i++)
                    {
                        MultiQueryResults results = queryResponse[2].fql_result_set[i];
                        Friend target = ((Friend)Friends[results.uid1]);
                        Friend origin = ((Friend)Friends[results.uid2]);
                        Link newLink = new Link();
                        newLink.Source = origin.index;
                        newLink.Target = target.index;
                        newLink.Value = 1;
                        links[links.Length] = newLink;
                    }
                    int width = 960;
                    int height = 500;
                    ForceObject force = D3.Layout.Force().Charge(-120).LinkDistance(30).Size(new int[] { width, height });
                    SelectObject svg = D3.Select("#canvas").Append("svg").Attr("width", width).Attr("height", height);
                    force.Nodes((Node[])nodes).Links((Link[])links).Start();
                    SelectObject link = svg.SelectAll(".link").Data((Link[])links).Enter().Append("line").Attr("class", "link").Style("stroke-width", delegate(Dictionary d) { return Math.Sqrt((int)d["value"]); });
                    SelectObject node = svg.SelectAll(".node").Data((Node[])nodes).Enter().Append("circle").Attr("class", "node").Attr("r", 5).Call(force.Drag);
                    node.Append("title").Text(delegate(Dictionary D) { return (string)D["name"]; });
                    force.On("tick", delegate()
                    {
                        link.Attr("x1", delegate(Dictionary D) { return (int)((Dictionary)D["source"])["x"]; }).
                            Attr("y1", delegate(Dictionary D) { return (int)((Dictionary)D["source"])["y"]; }).
                            Attr("x2", delegate(Dictionary D) { return (int)((Dictionary)D["target"])["x"]; }).
                            Attr("y2", delegate(Dictionary D) { return (int)((Dictionary)D["target"])["y"]; });
                        node.Attr("cx", delegate(Dictionary D) { return (int)D["x"]; }).
                            Attr("cy", delegate(Dictionary D) { return (int)D["y"]; });
                       

                    });
                    if (debug) Script.Alert(Friends.Count);
                }
                );


            });
        }
        public static void LogOut(jQueryEvent e)
        {
            Facebook.logout(delegate() { });


        }
        public static void Onload()
        {
            jQuery.Select("#login").Click(new jQueryEventHandler(ButtonClicked));
            jQuery.Select("#graph").Click(new jQueryEventHandler(GraphFriends));
            jQuery.Select("#LogoutButton").Click(new jQueryEventHandler(LogOut));
            InitOptions options = new InitOptions();
            options.appId = "459808530803920";
            options.channelUrl = "http://localhost/channel.aspx";
            options.status = true;
            options.cookie = true;
            options.xfbml = false;
            Facebook.init(options);
            Facebook.getLoginStatus(delegate(LoginResponse loginResponse)
          {
              if (loginResponse.status == "connected")
              {
                  UserID = loginResponse.authResponse.userID;
                  ((ImageElement)Document.GetElementById("image")).Src = "http://graph.facebook.com/" + UserID + "/picture";
              }
          });
            Facebook.Event.subscribe("auth.authResponseChange", delegate(LoginResponse response)
            {
                if (debug) Script.Alert("Event Login Fired");
                if (response.status == "connected")
                {
                    UserID = response.authResponse.userID;
                    ((ImageElement)Document.GetElementById("image")).Src = "http://graph.facebook.com/" + UserID + "/picture";
                }
                else
                {
                    ((ImageElement)Document.GetElementById("image")).Src = "";
                }
            });
        }


    }
}

using System;
using System.Diagnostics;
using System.Linq;
using Facebook;
using System.Collections.Generic;

namespace FriendsService
{
    #region DTO
    public struct FriendsInfoStruct
    {
        public long Uid { get; set; }
        public string Name { get; set; }
        public string Pic_Big_Url { get; set; }
        public string Pic_Sqaure_Url { get; set; }
        public string Profile_Url { get; set; }
        public long Profile_Update_Time { get; set; }

        public string Status_Time { get; set; }
        public string Birthday_date { get; set; }
        public string Sex { get; set; }
        public string Relationship_Status { get; set; }
        public long Significant_Other_Id { get; set; }
        public bool Is_App_User { get; set; }
        public int Wall_Count { get; set; }
        public string Status_Message { get; set; }
        public string Online_Presence { get; set; }
        public string Current_Location { get; set; }
        public string Webstie { get; set; }
        public string Third_Party_Id { get; set; }
    }
    #endregion

    #region Event Arguments
    public class GetFriendsInfoEventArgs : FriendsEventArgs
    {
        public List<long> FriendsList;
        public Dictionary<long, FriendsInfoStruct> Result { get; private set; }

        public GetFriendsInfoEventArgs(List<long> FriendsList, Dictionary<long, FriendsInfoStruct> Result, bool Success, Exception Error)
        {
            this.FriendsList = FriendsList;
            this.Result = Result;
            base.Success = Success;
            base.Error = Error;
        }
    }
    #endregion

    #region Api Class
    public class GetFriendsInfo
    {
        string fbToken;
        long userUid;

        public delegate void GetFriendsInfoEventHandler(object sender, GetFriendsInfoEventArgs e);
        public event GetFriendsInfoEventHandler GetFriendsInfo_Complete;

        public GetFriendsInfo(string fbToken, long userUid)
        {
            this.fbToken = fbToken;
            this.userUid = userUid;
        }

        public void Run()
        {
            //1. Prepare API
            string query0 = "SELECT uid2 FROM friend where uid1=me()";
            string query1 =
                "SELECT uid, name, pic_big ,pic_square, profile_url, profile_update_time, is_app_user, wall_count, " +
                "birthday_date, online_presence, relationship_status, significant_other_id, sex, status, website " +
                "FROM user WHERE uid IN (SELECT uid2 FROM #query0)";
            string query2 = "SELECT uid,time,message FROM status WHERE uid IN (SELECT uid2 FROM friend WHERE uid1= me())";
            string[] fqlMultiQuery = { query0, query1, query2 };

            //2. Add Async Event Handler
            FacebookClient client = new FacebookClient(fbToken);
            client.GetCompleted += new EventHandler<FacebookApiEventArgs>(client_GetCompleted);

            //3. Execute
            client.QueryAsync(fqlMultiQuery);
        }

        void client_GetCompleted(object sender, FacebookApiEventArgs e)
        {
            List<long> FriendsList = new List<long>();
            Dictionary<long, FriendsInfoStruct> FriendsInfo = new Dictionary<long,FriendsInfoStruct>();

            if (e.Error == null)
            {
                //Success
                JsonArray ResultsArray = (JsonArray)e.GetResultData();
                FriendsInfo = ParseResults(ResultsArray);

                foreach (KeyValuePair<long, FriendsInfoStruct> kvp in FriendsInfo)
                {
                    FriendsList.Add(kvp.Key);
                }

                //Event
                if (GetFriendsInfo_Complete != null)
                    GetFriendsInfo_Complete.Invoke(this, new GetFriendsInfoEventArgs(FriendsList, FriendsInfo, true, null));
            }
            else
            {
                //Error, Event
                if (GetFriendsInfo_Complete != null)
                    GetFriendsInfo_Complete.Invoke(this, new GetFriendsInfoEventArgs(FriendsList, FriendsInfo, false, e.Error));
            }
        }

        private Dictionary<long, FriendsInfoStruct> ParseResults(JsonArray ResultsArray)
        {
            Dictionary<long, FriendsInfoStruct> FriendsInfo = new Dictionary<long, FriendsInfoStruct>();

            JsonObject FriendsQuery = (JsonObject)ResultsArray.ElementAt(2);

            for (int i = 1; i < FriendsQuery.Count; i++)
            {
                JsonArray Friends = (JsonArray)FriendsQuery.ElementAt(i).Value;

                for (int j = 0; j < Friends.Count; j++)
                {
                    JsonObject FriendJson = (JsonObject)Friends.ElementAt(j);

                    string is_app_user = "false";
                    string name = "";
                    string pic_big = "";
                    string profile_update_time = "0";
                    string sex = "";
                    string uid = "0";
                    string wall_count = "0";
                    string pic_square = "";
                    string profile_url = "";

                    //Extended 1
                    string birthday_date = "";
                    string online_presence = "offline";            //active, idle, offline, or error 
                    string relationship_status = "";
                    string significant_other_id = "0";
                   

                    //Extended 2 
                    string status_message = "";
                    string status_time = "";

                    string current_location = "";
                    string website = "";
                    string third_party_id = "";

                    #region Standard
                    if (FriendJson.ContainsKey("is_app_user"))
                        if (FriendJson["is_app_user"] != null)
                            is_app_user = FriendJson["is_app_user"].ToString();

                    if (FriendJson.ContainsKey("name"))
                        if (FriendJson["name"] != null)
                            name = FriendJson["name"].ToString();

                    if (FriendJson.ContainsKey("pic_big"))
                        if (FriendJson["pic_big"] != null)
                            pic_big = FriendJson["pic_big"].ToString();

                    if (FriendJson.ContainsKey("profile_update_time"))
                        if (FriendJson["profile_update_time"] != null)
                            profile_update_time = FriendJson["profile_update_time"].ToString();

                    if (FriendJson.ContainsKey("sex"))
                        if (FriendJson["sex"] != null)
                            sex = FriendJson["sex"].ToString();

                    if (FriendJson.ContainsKey("uid"))
                        if (FriendJson["uid"] != null)
                            uid = FriendJson["uid"].ToString();

                    if (FriendJson.ContainsKey("wall_count"))
                        if (FriendJson["wall_count"] != null)
                            wall_count = FriendJson["wall_count"].ToString();

                    if (FriendJson.ContainsKey("pic_square"))
                        if (FriendJson["pic_square"] != null)
                            pic_square = FriendJson["pic_square"].ToString();

                    if (FriendJson.ContainsKey("profile_url"))
                        if (FriendJson["profile_url"] != null)
                            profile_url = FriendJson["profile_url"].ToString();
                    #endregion

                    #region Extended
                    if (FriendJson.ContainsKey("birthday_date"))
                        if (FriendJson["birthday_date"] != null)
                            birthday_date = FriendJson["birthday_date"].ToString();

                    if (FriendJson.ContainsKey("online_presence"))
                        if (FriendJson["online_presence"] != null)
                            online_presence = FriendJson["online_presence"].ToString();

                    if (FriendJson.ContainsKey("relationship_status"))
                        if (FriendJson["relationship_status"] != null)
                            relationship_status = FriendJson["relationship_status"].ToString();

                    if (FriendJson.ContainsKey("significant_other_id"))
                        if (FriendJson["significant_other_id"] != null)
                            significant_other_id = FriendJson["significant_other_id"].ToString();

                    if (FriendJson.ContainsKey("status"))
                        if (FriendJson["status"] != null)
                        {
                            var Status = FriendJson["status"] as JsonObject;

                            if (Status[0] != null)
                            {
                                status_message = Status[0].ToString();
                                //string remomve = "[message, ";
                                //status_message = status_message.Substring((remomve.Count() - 1), (status_message.Length - (remomve.Count())));
                            }

                            if (Status[1] != null)
                            {
                                status_time = Status[1].ToString();
                                string remomve = "[time, ";
                                status_time = status_time.Substring((remomve.Count() - 1), (status_time.Length - (remomve.Count())));
                            }
                        }

                    //if (FriendJson.ContainsKey("website"))
                    //    if (FriendJson["website"] != null)
                    //        website = FriendJson["website"].ToString();

                    //if (FriendJson.ContainsKey("third_party_id"))
                    //    if (FriendJson["third_party_id"] != null)
                    //        third_party_id = FriendJson["third_party_id"].ToString();

                    #endregion

                    try
                    {
                        FriendsInfoStruct Friend = new FriendsInfoStruct();
                        Friend.Uid = long.Parse(uid);
                        Friend.Name = name;

                        bool isMale;
                        if (sex == "female")
                            isMale = false;
                        else
                            isMale = true;

                        pic_big = Helpers.DefaultPictureBig(pic_big, isMale);
                        Friend.Pic_Big_Url = Helpers.CleanHttps(pic_big);

                        pic_square = Helpers.DefaultPictureSquare(pic_square, isMale);
                        Friend.Pic_Sqaure_Url = Helpers.CleanHttps(pic_square);
    
                        Friend.Profile_Url = Helpers.CleanHttps(profile_url);

                        Friend.Profile_Update_Time = long.Parse(profile_update_time);
                        Friend.Wall_Count = int.Parse(wall_count);

                        Friend.Status_Time = status_time;
                        Friend.Birthday_date = birthday_date;
                        Friend.Sex = sex;
                        Friend.Relationship_Status = relationship_status;
                        Friend.Significant_Other_Id = long.Parse(significant_other_id);
                        Friend.Is_App_User = bool.Parse(is_app_user);
                        Friend.Status_Message = status_message;
                        Friend.Status_Time = status_time;
                        Friend.Online_Presence = online_presence;
                        Friend.Current_Location = current_location;
                        Friend.Webstie = website;
                        Friend.Third_Party_Id = third_party_id;

                        FriendsInfo.Add(Friend.Uid, Friend);
                    }
                    catch
                    {
                        Debug.WriteLine("Error @ Parse Friends Info");
                    }
                }
            }

            return FriendsInfo;
        }
    }
    #endregion
}
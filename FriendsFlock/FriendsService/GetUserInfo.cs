using System;
using System.Diagnostics;
using System.Linq;
using Facebook;

namespace FriendsService
{
    #region DTO
    public struct UserInfoStruct
    {
        public long Uid { get; set; }
        public string Name { get; set; }
        public string Pic_Big_Url { get; set; }
        public string Pic_Sqaure_Url { get; set; }
        public string Profile_Url { get; set; }
    }
    #endregion

    #region Event Arguments
    public class GetUserInfoEventArgs : FriendsEventArgs
    {
        public UserInfoStruct Result { get; private set; }

        public GetUserInfoEventArgs(UserInfoStruct Result, bool Success, Exception Error)
        {
            this.Result = Result;
            base.Success = Success;
            base.Error = Error;
        }
    }
    #endregion

    #region Api Class
    public class GetUserInfo
    {
        string fbToken;
        long userUid;

        public delegate void GetUserInfoEventHandler(object sender, GetUserInfoEventArgs e);
        public event GetUserInfoEventHandler GetUserInfo_Complete;

        public GetUserInfo(string fbToken, long userUid)
        {
            this.fbToken = fbToken;
            this.userUid = userUid;
        }

        public void Run()
        {
            //1. Prepare API
            string fqlQuery = "SELECT uid, name, pic_big, pic_square, profile_url " +
                "FROM user WHERE uid=" + userUid.ToString();

            //2. Add Async Event Handler
            FacebookClient client = new FacebookClient(fbToken);
            client.GetCompleted += new EventHandler<FacebookApiEventArgs>(client_GetCompleted);

            //3. Execute
            client.QueryAsync(fqlQuery);
        }

        void client_GetCompleted(object sender, FacebookApiEventArgs e)
        {
            UserInfoStruct UserInfo = new UserInfoStruct();

            if (e.Error == null)
            {
                //Success
                JsonArray ResultsArray = (JsonArray)e.GetResultData();
                UserInfo = ParseResults(ResultsArray);

                //Event
                if (GetUserInfo_Complete != null)
                    GetUserInfo_Complete.Invoke(this, new GetUserInfoEventArgs(UserInfo, true, null));
            }
            else
            {
                //Error, Event
                if (GetUserInfo_Complete != null)
                    GetUserInfo_Complete.Invoke(this, new GetUserInfoEventArgs(UserInfo, false, e.Error));
            }
        }

        private UserInfoStruct ParseResults(JsonArray ResultsArray)
        {
            UserInfoStruct UserInfo = new UserInfoStruct();

            //Parse Results
            JsonObject ResultsObj = (JsonObject)ResultsArray.First();
            UserInfo.Uid = long.Parse(ResultsObj["uid"].ToString());
            UserInfo.Name = ResultsObj["name"].ToString();

            UserInfo.Pic_Big_Url = ResultsObj["pic_big"].ToString();
            UserInfo.Pic_Big_Url = Helpers.CleanHttps(UserInfo.Pic_Big_Url);

            UserInfo.Pic_Sqaure_Url = ResultsObj["pic_square"].ToString();
            UserInfo.Pic_Sqaure_Url = Helpers.CleanHttps(UserInfo.Pic_Sqaure_Url);

            UserInfo.Profile_Url = ResultsObj["profile_url"].ToString();
            UserInfo.Profile_Url = Helpers.CleanHttps(UserInfo.Profile_Url);

            return UserInfo;
        }
    }
    #endregion
}
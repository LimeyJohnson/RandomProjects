using System;
using System.Collections.Generic;
using System.Diagnostics;
using FriendsService;

namespace FriendsFlockInterface
{
    public class FriendsInfoAgentEventArgs : EventArgs
    {
        public bool Success { get; private set; }
        public Exception Error { get; private set; }
        public FriendsUserInfo UserInfo { get; private set; }
        public Dictionary<long, FriendsInfo> FriendsInfo { get; private set; } 

        public FriendsInfoAgentEventArgs(bool success, Exception error, FriendsUserInfo userInfo, Dictionary<long, FriendsInfo> friendsInfo)
        {
            Success = success;
            Error = error;
            UserInfo = userInfo;
            FriendsInfo = friendsInfo;
        }
    }

    public class FriendsInfoAgent
    {
        public delegate void FriendsInfoServiceEventHandler(object sender, FriendsInfoAgentEventArgs e);
        public event FriendsInfoServiceEventHandler FriendsInfoAgent_Complete;

        string FbToken;
        long UserUid;
        bool IsTestMode;

        FriendsUserInfo UserInfo;
        Dictionary<long, FriendsInfo> FriendsInfoDict;

        public FriendsInfoAgent(string fbToken, long userUid, bool isTestMode)
        {
            FbToken = fbToken;
            UserUid = userUid;
            IsTestMode = isTestMode;

            UserInfo = new FriendsUserInfo();
            FriendsInfoDict = new Dictionary<long, FriendsInfo>();
        }

        public void RunAsync()
        {
            if (IsTestMode)
            {
                //Test Mode
                //1. Random DTO
                //2. Package
                RunTestMode();
            }
            else
            {
                //Production Mode
                //1. Get User Info
                //2. Get Friends Info
                //3. Package
                RunProductionMode();
            }
        }

        private void RunTestMode()
        {
            Debug.WriteLine("RunTestMode");

            int testNodes = Dashboard.TestVertexes;

            for (int i = 0; i < testNodes; i++)
            {
                FriendsInfo fi = new FriendsInfo();
                fi.Uid = i;
                fi.Name = string.Format("Friend #{0}", i);
                fi.Pic_Big_Url = "Images/Dad_Profile.png";
                fi.Pic_Sqaure_Url = "Images/Dad_Square.png";
                FriendsInfoDict.Add(fi.Uid, fi);
            }

            FinishedSuccessfully();
        }

        #region Run Production Mode
        private void RunProductionMode()
        {
            Debug.WriteLine("RunProductionMode");
            try
            {
                GetUserInfo gui = new GetUserInfo(FbToken, UserUid);
                gui.GetUserInfo_Complete += new GetUserInfo.GetUserInfoEventHandler(gui_GetUserInfo_Complete);
                gui.Run();
            }
            catch (Exception ex)
            {
                FinishedUnsuccessfully(ex);
            }
        }

        void gui_GetUserInfo_Complete(object sender, GetUserInfoEventArgs e)
        {
            Debug.WriteLine("gui_GetUserInfo_Complete");
            if (e.Success)
            {
                UserInfo.Uid = e.Result.Uid;
                UserInfo.Name = e.Result.Name;
                UserInfo.Profile_Url = e.Result.Profile_Url;
                UserInfo.Pic_Square_Url = e.Result.Pic_Sqaure_Url;
                UserInfo.Pic_Big_Url = e.Result.Pic_Big_Url;

                GetFriendsInfo gfi = new GetFriendsInfo(FbToken, UserUid);
                gfi.GetFriendsInfo_Complete += new GetFriendsInfo.GetFriendsInfoEventHandler(gfi_GetFriendsInfo_Complete);
                gfi.Run();
            }
            else
                FinishedUnsuccessfully(e.Error);
        }

        void gfi_GetFriendsInfo_Complete(object sender, GetFriendsInfoEventArgs e)
        {
            if (e.Success)
            {
                foreach (FriendsInfoStruct fis in e.Result.Values)
                {
                    FriendsInfo info = new FriendsInfo();

                    info.Uid = fis.Uid;
                    info.Name = fis.Name;
                    info.Pic_Big_Url = fis.Pic_Big_Url;
                    info.Pic_Sqaure_Url = fis.Pic_Sqaure_Url;
                    info.Profile_Url = fis.Profile_Url;
                    info.Profile_Update_Time = fis.Profile_Update_Time;
                    info.Status_Time = fis.Status_Time;
                    info.Birthday_date = fis.Birthday_date;
                    info.Sex = fis.Sex;
                    info.Relationship_Status = fis.Relationship_Status;
                    info.Significant_Other_Id = fis.Significant_Other_Id;
                    info.Is_App_User = fis.Is_App_User;
                    info.Wall_Count = fis.Wall_Count;
                    info.Status_Message = fis.Status_Message;
                    info.Online_Presence = fis.Online_Presence;
                    info.Current_Location = fis.Current_Location;
                    info.Webstie = fis.Webstie;
                    info.Third_Party_Id = fis.Third_Party_Id;

                    //TODO - REMOVE
                    //info.Name = string.Format("Friend #{0}", i++);
                    //info.Pic_Big_Url = "http://www.friendsflock.com/images/Dad_Profile.png";
                    //info.Pic_Sqaure_Url = "http://www.friendsflock.com/images/Dad_Square.png";
                    //if (!string.IsNullOrEmpty(info.Status_Message))
                    //    info.Status_Message = "Friends's Status...";

                    FriendsInfoDict.Add(info.Uid, info);
                }

                FinishedSuccessfully();
            }
            else
                FinishedUnsuccessfully(e.Error);
        }
        #endregion

        #region Finished
        private void FinishedSuccessfully()
        {
            Debug.WriteLine("FinishedSuccessfully");

            bool isSuccess = true;

            if (FriendsInfoAgent_Complete != null)
                FriendsInfoAgent_Complete.Invoke(this, 
                    new FriendsInfoAgentEventArgs(isSuccess, null, UserInfo, FriendsInfoDict));
        }

        private void FinishedUnsuccessfully(Exception error)
        {
            Debug.WriteLine("FinishedUnsuccessfully: {0}", error.Message);

            bool isSuccess = false;

            if (FriendsInfoAgent_Complete != null)
                FriendsInfoAgent_Complete.Invoke(this,
                    new FriendsInfoAgentEventArgs(isSuccess, error, null, null));
        }
        #endregion
    }
}
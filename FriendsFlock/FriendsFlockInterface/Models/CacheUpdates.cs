using System;
using EpocTimeHelper;


namespace FriendsFlockInterface
{
    public class CacheUpdates
    {
        FriendsGraph friendsGraph;

        public CacheUpdates(FriendsGraph friendsGraph)
        {
            this.friendsGraph = friendsGraph;
        }

        public void Run()
        {
            foreach (FriendsVertex fv in friendsGraph.VertexDictionary.Values)
            {
                SetHighlightProfile(fv);
                SetHighlightStatus(fv);
                SetHighlightBirthday(fv);
                SetHighlightRelationship(fv);
            }
        }
        #region Friends Highlight
        private void SetHighlightProfile(FriendsVertex v)
        {
            long epocTime = v.Info.Profile_Update_Time;

            if (epocTime > 0)
            {
                TimeSpan ts = EpocTime.EpocToTimeSpan(epocTime);
                v.Info.Profile_Update_Time_String = EpocTime.TimeSpanString(ts);

                if (ts.TotalDays < 7)
                    v.State.ProfileUpdated = true;
                else
                    v.State.ProfileUpdated = false;
            }
            else
            {
                v.Info.Profile_Update_Time_String = "No Recent Updates" + ",\r\n" + Words.ProfileDescription;
                v.State.ProfileUpdated = false;
            }
        }

        private void SetHighlightStatus(FriendsVertex v)
        {
                if (!string.IsNullOrEmpty(v.Info.Status_Message))
                    v.State.StatusUpdated = true;
                else
                {
                    v.Info.Status_Message = "No Recent Status" + ",\r\n" + Words.StatusDescription;
                    v.State.StatusUpdated = false;
                }
        }

        private void SetHighlightBirthday(FriendsVertex v)
        {
            DateTime birthday;

            if (DateTime.TryParse(v.Info.Birthday_date, out birthday))
            {
                //12/28/1988 12:00:00 AM
                v.Info.Birthday_date = birthday.ToString("MMMM d");
                birthday = birthday.AddYears(DateTime.Now.Year - birthday.Year);
                TimeSpan ts = birthday - DateTime.Now;

                if ((0 <= ts.TotalDays) && (ts.TotalDays <= 31))
                    v.State.BirthdayUpdated = true;
                else
                    v.State.BirthdayUpdated = false;
            }
            else
            {
                v.Info.Birthday_date = "No Birthday Posted" + ",\r\n" + Words.BirthdayDescription;
                v.State.BirthdayUpdated = false;
            }
        }

        private void SetHighlightRelationship(FriendsVertex v)
        {
            if ((v.Info.Relationship_Status != "Single") && (!string.IsNullOrEmpty(v.Info.Relationship_Status)))
            {
                v.State.Relationship = true;
            }
            else
            {
                if (string.IsNullOrEmpty(v.Info.Relationship_Status))
                    v.Info.Relationship_Status = "No Relationship Posted" + ",\r\n" + Words.RelationshipDescription;
                v.State.Relationship = false;
            }
        }

        #endregion
    }
}

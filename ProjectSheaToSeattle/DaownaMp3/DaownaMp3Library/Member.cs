using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DaownaMp3Library
{
    public class Member
    {
        private int _id;
        private string _username;
        private DateTime _date;
        private bool _active;
        private string _email;
        private List<int> _myTrackIds;
        private List<int> _myPlaylistIds;

        public Member(int id)
        {
            _id = id;            
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string UserName
        {
            get 
            {   
                if (_username == null)
                    _username = DataAccess.Instance.GetAccountName(_id); 
                return _username;
            }
            set { _username = value; }
        }
        public DateTime Date
        {
            get 
            {
                _date = DataAccess.Instance.GetDateRegistered(_id);
                return _date; 
            }
            set { _date = value; }
        }
        public bool Active
        {
            get 
            {
                _active = DataAccess.Instance.GetActive(_id);
                return _active; 
            }
            set { _active = value; }
        }
        public string Email
        {
            get 
            {
                if (_email == null)
                    _email = DataAccess.Instance.GetEmail(_id);
                return _email; 
            }
            set
            {
                if (DataAccess.Instance.SetEmail(_id, value))
                    _email = value;
                else
                {
                    //error setting email
                }
            }
        }
        public List<int> MyPlaylistIds
        {
            get 
            {
                if (_myPlaylistIds == null)
                    _myPlaylistIds = DataAccess.Instance.GetUserPlaylistIds(_id);
                return _myPlaylistIds; 
            }
            set { _myPlaylistIds = value; }
        }
        public List<int> MyTrackIds
        {
            get 
            {
                if (_myTrackIds == null)
                    _myTrackIds = DataAccess.Instance.GetUserTrackIds(_id);
                return _myTrackIds; 
            }
            set { _myTrackIds = value; }
        }

        public bool ValidateEmail(string address)
        {
            try
            {
                MailAddress checkAddress = new MailAddress(address);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public bool ChangePassword(string oldPassword, string newPassword)
        {
            if (DataAccess.Instance.GetPassword(_id) == oldPassword)
            {
                if (DataAccess.Instance.SetPassword(_id, newPassword))
                    return true;
                else
                {
                    //error setting password
                }
            }
            return false;
        }
        public bool RemoveFromPlaylistIds(int remove) 
        {
            DataAccess.Instance.RemovePlayList(remove);
            return _myPlaylistIds.Remove(remove); 
        }
        public bool CreateUserPlaylist(string name, bool isPublic)
        {
            bool duplicateName = false;

            for (int i = 0; i < _myPlaylistIds.Count; i++)
                if (name == DataAccess.Instance.GetPlayListName(_myPlaylistIds[i]))
                    duplicateName = true;

            if (duplicateName == true)
            {
                //duplicate playlist name attempt
                return false;
            }
            else
            {
                List<int> updateMyPlaylistIds;
                PlayList createdPlaylist = new PlayList(name, _id, isPublic);
                updateMyPlaylistIds = _myPlaylistIds;
                updateMyPlaylistIds.Add(createdPlaylist.PlaylistID);

                return true;
            }
        }
    }
}

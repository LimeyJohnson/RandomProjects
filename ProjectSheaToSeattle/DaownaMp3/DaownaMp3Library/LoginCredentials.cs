using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DaownaMp3Library
{
    public class LoginCredentials
    {
        private string _username;
        private string _password;
        private string _email;
        private int _id;

        //Authentication for login Constructor
        public LoginCredentials(string name, string pass)
        {
            _username = name;
            _password = pass;
            _id = -1;
        }
        //Forgotten password retrieval Constructor
        public LoginCredentials(string name)
        {
            if ((_username = DataAccess.Instance.GetEmail(name)) != "dne")
            {
                _password = DataAccess.Instance.GetLostPassword(_username);
                _id = -1;
            }            
        }
        //Registration Constructor
        public LoginCredentials(string name, string pass, string email)
        {
            _username = name;
            _password = pass;
            _email = email;
            _id = -1;
        }

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public bool Register()
        {
            if (DataAccess.Instance.CheckRegistrationName(_username))
                return false;
            if (DataAccess.Instance.CheckRegistrationName(_email))
                return false;
            
            DateTime now = DateTime.Now;
            return DataAccess.Instance.AddAccount(_username, _password, _email, now);
        }
        public bool Authenticate()
        {
            return DataAccess.Instance.Authenticate(this);
        }
        public void EmailLostPassword()
        {
            MailMessage passwordMessage = new MailMessage(ServerInfo.DaownaMp3EmailAddress, _username, "Password Recovery", "Your password is: " + _password);
            SmtpClient objsmtp = new SmtpClient("smtp.gmail.com", 587);
            objsmtp.EnableSsl = true;
            objsmtp.UseDefaultCredentials = false;
            objsmtp.Credentials = new NetworkCredential(ServerInfo.DaownaMp3EmailAddress, ServerInfo.DaownaMp3EmailPassword);
            objsmtp.Send(passwordMessage);
        }
    }
}

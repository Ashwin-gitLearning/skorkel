using System;

/// <summary>
/// Summary description for DO_Login
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_Login
    {
        public DO_Login()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Variable declaration

        private string _username;
        private string _password;
        public Int32 intRegistartionID { get; set; }
        public int UserID { get; set; }
      

        #endregion

        #region Properties

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
        #endregion
    }
}
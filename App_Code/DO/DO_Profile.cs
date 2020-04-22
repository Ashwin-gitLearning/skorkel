using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_Profile
/// </summary>
namespace DA_SKORKEL
{
    public class DO_Profile
    {
        public DO_Profile()
        {

        }
        #region Variable Declaration;
        private int registrationId;
        #endregion
        #region Properties
        public int RegistrationId
        {
            get { return registrationId; }
            set { registrationId = value; }
        }
        public int ConnectRegistrationId { get; set; }
        public int intInvitedUserId { get; set; }
        public int intInstituteUserId { get; set; }
        public string striInvitedUserId { get; set; }
        #endregion

    }
}
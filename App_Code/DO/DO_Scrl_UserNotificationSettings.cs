using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserNotificationSettings
    {
        public DO_Scrl_UserNotificationSettings()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _intNotificationId;
        private int _intRegistrationId;      
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;

        public int intNotificationId { get { return _intNotificationId; } set { _intNotificationId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }       
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }
    }
}
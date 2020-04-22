using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserPrivacySettings
    {
        public DO_Scrl_UserPrivacySettings()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _intPrivacyId;
        private int _intSubPrivacyId;
        private int _intRegistrationId;
        private int _intCustomRegId;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;
        private string _txtSearchText;
        private int _intCustomId;

        public int intPrivacyId { get { return _intPrivacyId; } set { _intPrivacyId = value; } }
        public int intSubPrivacyId { get { return _intSubPrivacyId; } set { _intSubPrivacyId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public int intCustomRegId { get { return _intCustomRegId; } set { _intCustomRegId = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }
        public string txtSearchText { get { return _txtSearchText; } set { _txtSearchText = value; } }
        public int intCustomId { get { return _intCustomId; } set { _intCustomId = value; } }
    }
}
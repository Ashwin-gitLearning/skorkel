using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserHonorsTbl
    {
        public DO_Scrl_UserHonorsTbl()
        { }
        private int _intHonorId;
        private int _intRegistrationId;
        private string _strTitle;
        private string _strIssuer;
        private int _intYear;
        private string _strDescription;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;

        public int intHonorId { get { return _intHonorId; } set { _intHonorId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public string strTitle { get { return _strTitle; } set { _strTitle = value; } }
        public string strIssuer { get { return _strIssuer; } set { _strIssuer = value; } }
        public int intYear { get { return _intYear; } set { _intYear = value; } }
        public string strDescription { get { return _strDescription; } set { _strDescription = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }

    }
}

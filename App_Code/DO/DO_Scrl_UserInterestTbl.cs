using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserInterestTbl
    {
        public DO_Scrl_UserInterestTbl()
        { }
        private int _intInterestId;
        private int _intRegistrationId;
        private string _strInterests;
        private int _intPercent;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;

        public int intInterestId { get { return _intInterestId; } set { _intInterestId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public int intPercent { get { return _intPercent; } set { _intPercent = value; } }
        public string strInterests { get { return _strInterests; } set { _strInterests = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }

        public int intOutId { get; set; }

    }
}

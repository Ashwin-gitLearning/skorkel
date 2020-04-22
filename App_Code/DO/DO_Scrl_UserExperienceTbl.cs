using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserExperienceTbl
    {
        public DO_Scrl_UserExperienceTbl()
        { }
        private int _intExperienceId;
        private int _intRegistrationId;
        private string _strCompanyName;
        private string _strDesignation;

        private DateTime _dtFrom;
        private DateTime _dttTO;

        private int _inFromtMonth;
        private int _intFromYear;
        private int _intToMonth;
        private int _intToYear;
        private string _strLocation;
        private string _strDescription;
        private Boolean _bitAtPresent;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;
        private string _strBarNo;

        public int intExperienceId { get { return _intExperienceId; } set { _intExperienceId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public string strCompanyName { get { return _strCompanyName; } set { _strCompanyName = value; } }
        public string strDesignation { get { return _strDesignation; } set { _strDesignation = value; } }

        public DateTime dtFromDate { get { return _dtFrom; } set { _dtFrom = value; } }
        public DateTime dtToDate { get { return _dttTO; } set { _dttTO = value; } }

        public int inFromtMonth { get { return _inFromtMonth; } set { _inFromtMonth = value; } }
        public int intFromYear { get { return _intFromYear; } set { _intFromYear = value; } }
        public int intToMonth { get { return _intToMonth; } set { _intToMonth = value; } }
        public int intToYear { get { return _intToYear; } set { _intToYear = value; } }
        public string strLocation { get { return _strLocation; } set { _strLocation = value; } }
        public string strDescription { get { return _strDescription; } set { _strDescription = value; } }
        public Boolean bitAtPresent { get { return _bitAtPresent; } set { _bitAtPresent = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }
        public string strBarNo { get { return _strBarNo; } set { _strBarNo = value; } }
        public int intOutId { get; set; }
        public string strUnqId { get; set; }
        public string strSkillName { get; set; }
        public int intUserSkillId { get; set; }

    }
}

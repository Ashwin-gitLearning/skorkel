using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserContactDetailsTbl
    {
        public DO_Scrl_UserContactDetailsTbl()
        { }
        private int _intContactId;
        private int _intRegistrationId;
        private Int64 _intMobileNo;
        private Int64 _intPhoneNo;
        private string _strEmailId;
        private string _strWebSite;
        private string _strAddress;
        private string _strAddress2;
        private int _intCountry;
        private int _intState;
        private int _intCity;
        private int _intPinCode;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;

        public int intContactId { get { return _intContactId; } set { _intContactId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public Int64 intMobileNo { get { return _intMobileNo; } set { _intMobileNo = value; } }
        public Int64 intPhoneNo { get { return _intPhoneNo; } set { _intPhoneNo = value; } }
        public string strEmailId { get { return _strEmailId; } set { _strEmailId = value; } }
        public string strWebSite { get { return _strWebSite; } set { _strWebSite = value; } }
        public string strAddress { get { return _strAddress; } set { _strAddress = value; } }
        public string strAddress2 { get { return _strAddress2; } set { _strAddress2 = value; } }
        public int intCountry { get { return _intCountry; } set { _intCountry = value; } }
        public int intState { get { return _intState; } set { _intState = value; } }
        public int intCity { get { return _intCity; } set { _intCity = value; } }
        public int intPinCode { get { return _intPinCode; } set { _intPinCode = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }

    }
}

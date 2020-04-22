using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserMobileDetailsTbl
    {
        public DO_Scrl_UserMobileDetailsTbl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _intMobileId;
        private int _intRegistrationId;
        private Int64 _intMobileNo;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;

        public int intMobileId { get { return _intMobileId; } set { _intMobileId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public Int64 intMobileNo { get { return _intMobileNo; } set { _intMobileNo = value; } }        
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }
    }
}
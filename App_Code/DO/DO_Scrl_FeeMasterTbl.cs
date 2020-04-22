using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_FeeMasterTbl
    {
        public DO_Scrl_FeeMasterTbl()
        { }
        private int _intFeeId;
        private string _strFeeName;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIPAddress;

        public int intFeeId { get { return _intFeeId; } set { _intFeeId = value; } }
        public string strFeeName { get { return _strFeeName; } set { _strFeeName = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIPAddress { get { return _strIPAddress; } set { _strIPAddress = value; } }

    }
}

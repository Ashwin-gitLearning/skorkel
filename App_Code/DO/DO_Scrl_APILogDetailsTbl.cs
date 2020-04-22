using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_APILogDetailsTbl
    {
        public DO_Scrl_APILogDetailsTbl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _intAPILogId;
        private string _strAPIType;
        private string _strURL;
        private string _strResponse;
        private DateTime _dtEntryDate;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIPAddress;

        public int intAPILogId { get { return _intAPILogId; } set { _intAPILogId = value; } }
        public string strAPIType { get { return _strAPIType; } set { _strAPIType = value; } }        
        public string strURL { get { return _strURL; } set { _strURL = value; } }
        public string strResponse { get { return _strResponse; } set { _strResponse = value; } }
        public DateTime dtEntryDate { get { return _dtEntryDate; } set { _dtEntryDate = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIPAddress { get { return _strIPAddress; } set { _strIPAddress = value; } }
    }
}
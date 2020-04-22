using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserFollowGroup
    {
        public DO_Scrl_UserFollowGroup()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _intFollowId;
        private int _intRegistrationId;
        private int _intFollowStatus;
        private int _intGroupId;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;
        private int _intOrgnisationID;

        public int intFollowId { get { return _intFollowId; } set { _intFollowId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public int intFollowStatus { get { return _intFollowStatus; } set { _intFollowStatus = value; } }
        public int intGroupId { get { return _intGroupId; } set { _intGroupId = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }

        public int intOrgnisationID { get { return _intOrgnisationID; } set { _intOrgnisationID = value; } }
    }
}
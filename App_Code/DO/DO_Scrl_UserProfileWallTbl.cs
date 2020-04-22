using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserProfileWallTbl
    {
        public DO_Scrl_UserProfileWallTbl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _intProfilePostId;
        private int _intRegistrationId;
        private string _StrPost;
        private int _intInvitedUserId;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;

        public int intProfilePostId { get { return _intProfilePostId; } set { _intProfilePostId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public string StrPost { get { return _StrPost; } set { _StrPost = value; } }
        public int intInvitedUserId { get { return _intInvitedUserId; } set { _intInvitedUserId = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }
    }

}
using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserGroupJoin
    {
        public DO_Scrl_UserGroupJoin()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _intRequestJoinId;
        private int _inGroupId;
        private int _isAccepted;
        private int _intRegistrationId;
        private int _intInvitedUserId;
        private string _strGroupName;            
        private string _strDescription;       
        private string _strLogoPath;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;
        private int _intOrgnisationID;
        public int intOrgnisationID { get { return _intOrgnisationID; } set { _intOrgnisationID = value; } }
       
        
        public string PhotoPath { get; set; }
        public int GroupType { get; set; }
        public string GroupTypeName { get; set; }

        public int intInvitedUserId { get { return _intInvitedUserId; } set { _intInvitedUserId = value; } }
        public int isAccepted { get { return _isAccepted; } set { _isAccepted = value; } }
        public int intRequestJoinId { get { return _intRequestJoinId; } set { _intRequestJoinId = value; } }
        public int inGroupId { get { return _inGroupId; } set { _inGroupId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public string strGroupName { get { return _strGroupName; } set { _strGroupName = value; } }
        public string strDescription { get { return _strDescription; } set { _strDescription = value; } }
        
        public string strLogoPath { get { return _strLogoPath; } set { _strLogoPath = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }
        public int PageSize {get;set;}
        public int Currentpage {get;set;}
        public string strSearch { get; set; }
        public int intUserTypeId { get; set; }


    }
}
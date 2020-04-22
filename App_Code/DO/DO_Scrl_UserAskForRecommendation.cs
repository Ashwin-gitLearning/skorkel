using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserAskForRecommendation
    {
        public DO_Scrl_UserAskForRecommendation()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _intRecommendationChildId;
        private string _StrRecommendation;
        private int _intRegistrationId;      
        private int _intInvitedUserId;
        private DateTime _dtAddedOn;
        private DateTime _dtReqAcceptedDate;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;

        public int intRecommendationChildId { get { return _intRecommendationChildId; } set { _intRecommendationChildId = value; } }
        public string StrRecommendation { get { return _StrRecommendation; } set { _StrRecommendation = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }    
        public int intInvitedUserId { get { return _intInvitedUserId; } set { _intInvitedUserId = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public DateTime dtReqAcceptedDate { get { return _dtReqAcceptedDate; } set { _dtReqAcceptedDate = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }
    }
}
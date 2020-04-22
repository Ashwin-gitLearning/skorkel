using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserRecommendation
    {
        public DO_Scrl_UserRecommendation()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private int _intRecommendationId;
        private int _intRegistrationId;
        private string _StrRecommendation;
        private int _intInvitedUserId;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;

        public int intRecommendationId { get { return _intRecommendationId; } set { _intRecommendationId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public string StrRecommendation { get { return _StrRecommendation; } set { _StrRecommendation = value; } }
        public int intInvitedUserId { get { return _intInvitedUserId; } set { _intInvitedUserId = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }

        public int intGroupId { get; set; }
        public string strSubject { get; set; }
        public int OrgId { get; set; }

        public int intSkillId { get; set; }

        public int intOutRecommendationId { get; set; }
        public int intMessageId { get; set; }
        public string striInvitedUserId { get; set; }
        public int CurrentPageSize { get; set; }
        public int CurrentPage { get; set; }
        public string StrEndorseMess { get; set; }
        public int intOrgEndorseId { get; set; }
        //public int intSkillId { get; set; }
        
    }
}
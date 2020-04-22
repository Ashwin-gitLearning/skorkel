using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserGroupDetailTbl
    {
        public DO_Scrl_UserGroupDetailTbl()
        { }
        private int _inGroupId;
        private int _intRegistrationId;
        private string _strGroupName;
        private string _strSummary;
        private int _strGroupType;
        private string _strDescription;
        private string _strAccess;
        private string _strLogoPath;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;
        private bool _bitPrivatePublic;
        private int _intOrgnisationID;

        public int intGrpInvitationMemberId { get; set; }
        public int intModuleAccessRightId { get; set; }
        public string PhotoPath { get; set; }
        public int GroupType { get; set; }
        public string GroupTypeName { get; set; }

        public int inGroupId { get { return _inGroupId; } set { _inGroupId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public string strGroupName { get { return _strGroupName; } set { _strGroupName = value; } }
        public string strSummary { get { return _strSummary; } set { _strSummary = value; } }
        public int strGroupType { get { return _strGroupType; } set { _strGroupType = value; } }
        public string strDescription { get { return _strDescription; } set { _strDescription = value; } }
        public string strAccess { get { return _strAccess; } set { _strAccess = value; } }
        public string strLogoPath { get { return _strLogoPath; } set { _strLogoPath = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }
        public bool bitPrivatePublic { get { return _bitPrivatePublic; } set { _bitPrivatePublic = value; } }
        public int GroupOutId { get; set; }
        public int locationId { get; set; }

        public int intInviteeId { get; set; }
        public int IsAccepted { get; set; }
        public int intUserId { get; set; }
        public string strRoleName { get; set; }
        public int intSubjectCategoryId { get; set; }
        public int DocId { get; set; }
        public string strInvitationMessage { get; set; }
        public string inviteMembers { get; set; }
        public int intRoleId { get; set; }
        public string strUniqueKey { get; set; }
        public int grpInvMemberId { get; set; }
        public int intGrpModuleId { get; set; }
        public int intVisibility { get; set; }
        public int intAddPermission { get; set; }
        public int intEditPermission { get; set; }
        public int intDeletePermission { get; set; }
        public int intOrgId { get; set; }
        public int intGroupTypeCode { get; set; }
        public int intOrgType { get; set; }
        public int intGroupTypeId { get; set; }



        public int intGrpCategoryID { get; set; }
        public int intOfficeId { get; set; }
        public string strMatterTitle { get; set; }
        public string strMatterDetails { get; set; }
        public DateTime? dtOpenDate { get; set; }
        public int intPracticeId { get; set; }
        public string strReferenceNo { get; set; }
        public int intCurrencyId { get; set; }
        public string strIntialFees { get; set; }
        public int intHourlyBlendedRate { get; set; }
        public string strHourlyFees { get; set; }
        public int intCategoryId { get; set; }
        public string IsComplete { get; set; }

        public string strCourseOutline { get; set; }
        public DateTime? dtStartDate { get; set; }
        public string intNoOfClasses { get; set; }
        public string strHall { get; set; }
        public int intNoOfCredit { get; set; }
        public string strMarkingSystem { get; set; }
        public int intMaxStudent { get; set; }
        public DateTime? dtLastDateOfSignUp { get; set; }
        public string isAuditingAllowed { get; set; }
        public string strAlloedFor { get; set; }
        public string strDay { get; set; }
        public string strStartTime { get; set; }
        public string strEndTime { get; set; }
        public int intClassRoomScheduledId { get; set; }
        public int intGrpRoleId { get; set; }
        public int intNewGroupID { get { return _inGroupId; } set { _inGroupId = value; } }
        
        public int intOrgnisationID { get { return _intOrgnisationID; } set { _intOrgnisationID = value; } }
        public string strTotalGrpMemberID { get; set; }
        public string strMemberName { get; set; }
        public int IsAccept { get; set; }
    }
}

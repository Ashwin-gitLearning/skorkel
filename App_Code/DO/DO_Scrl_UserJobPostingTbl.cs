using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserJobPostingTbl
    {
        public DO_Scrl_UserJobPostingTbl()
        { }
        private int _intJobPostingId;
        private int _intRegistrationId;
        private int _intGroupId;
        private string _strTitle;
        private string _strDescription;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;
        private string _strComment;

        private int _intLikeId;
        private int _intLikeDisLike;
        private int _intCommentId;

        public int intJobPostingId { get { return _intJobPostingId; } set { _intJobPostingId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public int intGroupId { get { return _intGroupId; } set { _intGroupId = value; } }
        public string strTitle { get { return _strTitle; } set { _strTitle = value; } }
        public string strDescription { get { return _strDescription; } set { _strDescription = value; } }
        public string strComment { get { return _strComment; } set { _strComment = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }

        public int intLikeDisLike { get { return _intLikeDisLike; } set { _intLikeDisLike = value; } }
        public int intCommentId { get { return _intCommentId; } set { _intCommentId = value; } }
        public int intLikeId { get { return _intLikeId; } set { _intLikeId = value; } }

        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }
        public int intCityId { get; set; }
        public string strOtherCity { get; set; }
        public string strJobType { get; set; }
        public string strExpiry { get; set; }
        public string StrCityId { get; set; }
        public string strOrganization { get; set; }
        public DateTime? expiryDate { get; set; }
        public int OrgId { get; set; }
        public int intJobAppliedID { get; set; }
        public string strName { get; set; }
        public string strEmailId { get; set; }
        public int intJobLocationID { get; set; }
        public string strJobTypeAppliedFor { get; set; }
        public string strUploadResume { get; set; }
        public string StrNotes { get; set; }
        
    }
}

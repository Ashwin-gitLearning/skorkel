using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_GroupEventsTbl
    {
        public DO_Scrl_GroupEventsTbl()
        { }
        private int _intGrpEventId;
        private int _intGroupId;
        private int _intRegistrationId;
        private string _strTitle;
        private int _intMonth;
        private int _intYear;
        private string _strLocation;
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

        public int intLikeDisLike { get { return _intLikeDisLike; } set { _intLikeDisLike = value; } }
        public int intCommentId { get { return _intCommentId; } set { _intCommentId = value; } }
        public int intLikeId { get { return _intLikeId; } set { _intLikeId = value; } }

        public int intGrpEventId { get { return _intGrpEventId; } set { _intGrpEventId = value; } }
        public int intGroupId { get { return _intGroupId; } set { _intGroupId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public string strTitle { get { return _strTitle; } set { _strTitle = value; } }
        public int intMonth { get { return _intMonth; } set { _intMonth = value; } }
        public int intYear { get { return _intYear; } set { _intYear = value; } }
        public string strLocation { get { return _strLocation; } set { _strLocation = value; } }
        public string strDescription { get { return _strDescription; } set { _strDescription = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }
        public string strComment { get { return _strComment; } set { _strComment = value; } }
        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }

        public DateTime? dtFromDate { get; set; }
        public DateTime? dtTodate { get; set; }
        public string strPriority { get; set; }
        public string strColor { get; set; }
        private string _strContactNumber;
        public string strContactNumber { get { return _strContactNumber; } set { _strContactNumber = value; } }
        private string _strContactPerson { get; set; }
        public string strContactPerson { get { return _strContactPerson; } set { _strContactPerson = value; } }

        public int intNewEventID { get; set; }
        public int OrgId { get; set; }
    }
}

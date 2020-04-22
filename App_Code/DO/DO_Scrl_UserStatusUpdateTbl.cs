using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserStatusUpdateTbl
    {
        public DO_Scrl_UserStatusUpdateTbl()
        { }
        private int _intStatusUpdateId;
        private int _intRegistrationId;
        private int _intGroupId;
        private string _strPostDescription;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;

        private int _intLikeId;
        private int _intLikeDisLike;
        private int _intCommentId;
        private string _strComment;

        public int intStatusUpdateId { get { return _intStatusUpdateId; } set { _intStatusUpdateId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public int intGroupId { get { return _intGroupId; } set { _intGroupId = value; } }
        public string strPostDescription { get { return _strPostDescription; } set { _strPostDescription = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }

        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }

        public int intLikeDisLike { get { return _intLikeDisLike; } set { _intLikeDisLike = value; } }
        public int intCommentId { get { return _intCommentId; } set { _intCommentId = value; } }
        public int intLikeId { get { return _intLikeId; } set { _intLikeId = value; } }
        public string strComment { get { return _strComment; } set { _strComment = value; } }

        public string strPhotoPath { get; set; }
        public string strVideoPath { get; set; }

        public string strPostType { get; set; }
        public string strPostLink { get; set; }

        public string strFriendList { get; set; }
        public String strMessage { get; set; }
        public String strLink { get; set; }
        public String strInvitee { get; set; }
        public int intSharePostingId { get; set; }
        public int orgId { get; set; }

    }
}

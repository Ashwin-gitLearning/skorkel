using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserQuestionTbl
    {
        public DO_Scrl_UserQuestionTbl()
        { }
        private int _intQuestionId;
        private int _intRegistrationId;
        private int _intGroupId;
        private string _strQuestion;

        private string _strComment;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;

        private int _intLikeId;
        private int _intLikeDisLike;
        private int _intCommentId;



        public int intQuestionId { get { return _intQuestionId; } set { _intQuestionId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public int intGroupId { get { return _intGroupId; } set { _intGroupId = value; } }
        public string strQuestion { get { return _strQuestion; } set { _strQuestion = value; } }
        public string strComment { get { return _strComment; } set { _strComment = value; } }

        public int intLikeDisLike { get { return _intLikeDisLike; } set { _intLikeDisLike = value; } }
        public int intCommentId { get { return _intCommentId; } set { _intCommentId = value; } }
        public int intLikeId { get { return _intLikeId; } set { _intLikeId = value; } }


        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }

        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }

    }
}

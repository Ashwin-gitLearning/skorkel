using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_ProfessorWiseSubjects
    {
        public DO_Scrl_ProfessorWiseSubjects()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _intProfessorId;
        private int _intSubjectId;
        private int _intProjectId;
        private int _intAddedBy;
        private string _strIpAddress;
        private int _intLikeId;
        private int _intLikeDisLike;
        private int _intCommentId;
        private string _strComment;
        private int _intRating;
        private int _intRatingId;
        

        public int intProfessorId { get { return _intProfessorId; } set { _intProfessorId = value; } }
        public int intSubjectId { get { return _intSubjectId; } set { _intSubjectId = value; } }
        public int intProjectId { get { return _intProjectId; } set { _intProjectId = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }      
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }
        public int intLikeDisLike { get { return _intLikeDisLike; } set { _intLikeDisLike = value; } }
        public int intCommentId { get { return _intCommentId; } set { _intCommentId = value; } }
        public int intLikeId { get { return _intLikeId; } set { _intLikeId = value; } }
        public string strComment { get { return _strComment; } set { _strComment = value; } }
        public int intRating { get { return _intRating; } set { _intRating = value; } }
        public int intRatingId { get { return _intRatingId; } set { _intRatingId = value; } }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
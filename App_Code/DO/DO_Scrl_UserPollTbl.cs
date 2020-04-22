using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserPollTbl
    {
        public DO_Scrl_UserPollTbl()
        { }
        private int _intPollId;
        private int _intPollChildId;
        private int _intRegistrationId;
        private int _intGroupId;
        private string _strPollName;
        private string _stroption1;
        private string _stroption2;
        private string _stroption3;
        private string _stroption4;
        private string _stroption5;
        private string _stroption6;
        private string _stroption7;
        private string _stroption8;
        private string _stroption9;
        private string _stroption10;


        private bool _bitoption1;
        private bool _bitoption2;
        private bool _bitoption3;
        private bool _bitoption4;
        private bool _bitoption5;
        private bool _bitoption6;
        private bool _bitoption7;
        private bool _bitoption8;
        private bool _bitoption9;
        private bool _bitoption10;


        private string _strComment;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;

        private int _intLikeId;
        private int _intLikeDisLike;
        private int _intCommentId;


        public int intPollChildId { get { return _intPollChildId; } set { _intPollChildId = value; } }
        public int intPollId { get { return _intPollId; } set { _intPollId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public int intGroupId { get { return _intGroupId; } set { _intGroupId = value; } }
        public string strPollName { get { return _strPollName; } set { _strPollName = value; } }

        public string Option1 { get { return _stroption1; } set { _stroption1 = value; } }
        public string Option2 { get { return _stroption2; } set { _stroption2 = value; } }
        public string Option3 { get { return _stroption3; } set { _stroption3 = value; } }
        public string Option4 { get { return _stroption4; } set { _stroption4 = value; } }

        public string Option5 { get { return _stroption5; } set { _stroption5 = value; } }
        public string Option6 { get { return _stroption6; } set { _stroption6 = value; } }
        public string Option7 { get { return _stroption7; } set { _stroption7 = value; } }
        public string Option8 { get { return _stroption8; } set { _stroption8 = value; } }
        public string Option9 { get { return _stroption9; } set { _stroption9 = value; } }
        public string Option10 { get { return _stroption10; } set { _stroption10 = value; } }

        public bool bitOption1 { get { return _bitoption1; } set { _bitoption1 = value; } }
        public bool bitOption2 { get { return _bitoption2; } set { _bitoption2 = value; } }
        public bool bitOption3 { get { return _bitoption3; } set { _bitoption3 = value; } }
        public bool bitOption4 { get { return _bitoption4; } set { _bitoption4 = value; } }
        public bool bitOption5 { get { return _bitoption5; } set { _bitoption5 = value; } }
        public bool bitOption6 { get { return _bitoption6; } set { _bitoption6 = value; } }
        public bool bitOption7 { get { return _bitoption7; } set { _bitoption7 = value; } }
        public bool bitOption8 { get { return _bitoption8; } set { _bitoption8 = value; } }
        public bool bitOption9 { get { return _bitoption9; } set { _bitoption9 = value; } }
        public bool bitOption10 { get { return _bitoption10; } set { _bitoption10 = value; } }
        public bool bitShowVoteToOthers { get; set; }

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

        public string strDescription { get; set; }
        public string strVotingPattern { get; set; }
        public string strVotingEnds { get; set; }
        public DateTime? dtPollExpireDate { get; set; }
        public string strPollExpireTime { get; set; }
        public string strVoteType { get; set; }
        public int intPollOutId { get; set; }
        public string strSearch { get; set; }
        public int OrgId { get; set; }
    }
}

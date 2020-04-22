using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserFavouriteQuestionTbl
    {
        public DO_Scrl_UserFavouriteQuestionTbl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _intRegistrationId;
        private int _intGroupId;
        private int _intQuestionId { get; set; }
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;
        private int _intQuestionFavouriteId;
        private int _intMarkFavourit { get; set; }

        public int intMarkFavourite { get { return _intMarkFavourit; } set { _intMarkFavourit = value; } }
        public int intQuestionId { get { return _intQuestionId; } set { _intQuestionId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public int intQuestionFavouriteId { get { return _intQuestionFavouriteId; } set { _intQuestionFavouriteId = value; } }
        public int intGroupId { get { return _intGroupId; } set { _intGroupId = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }      
    }
}
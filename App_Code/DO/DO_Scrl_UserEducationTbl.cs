using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserEducationTbl
    {
        public DO_Scrl_UserEducationTbl()
        { }
        private int _intEducationId;
        private int _intRegistrationId;
        private string _strInstituteName;
        private int _intMonth;
        private int _intYear;
        private string _strDegree;
        private string _strGrade;
        private string _strSpclLibrary;
        private string _strEducationAchievement;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;
        private DateTime _dtDate;
        private byte _strIsScorePublic;

        public int intEducationId { get { return _intEducationId; } set { _intEducationId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public string strInstituteName { get { return _strInstituteName; } set { _strInstituteName = value; } }
        public int intMonth { get { return _intMonth; } set { _intMonth = value; } }
        public int intYear { get { return _intYear; } set { _intYear = value; } }
        public string strDegree { get { return _strDegree; } set { _strDegree = value; } }
        public string strGrade { get { return _strGrade; } set { _strGrade = value; } }
        public string strSpclLibrary { get { return _strSpclLibrary; } set { _strSpclLibrary = value; } }
        public string strEducationAchievement { get { return _strEducationAchievement; } set { _strEducationAchievement = value; } }
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }
        public byte strIsScorePublic { get { return _strIsScorePublic; } set { _strIsScorePublic = value; } }
        public DateTime dtDate { get { return _dtDate; } set { _dtDate = value; } }
        public int intToMonth { get; set; }
        public int intToYear { get; set; }
        public int intAchivmentId { get; set; }
        public int intOutId { get; set; }
        public int intDegreeId { get; set; }
        public int intInstituteId { get; set; }

        public string strCompititionName { get; set; }
        public string strPosition { get; set; }
        public string strAdditionalAward { get; set; }
        public string strAchDescription { get; set; }
        public string strMilestone { get; set; }
        public int intMilestoneId { get; set; }
        public int intPostionId { get; set; }
        public int CompLavel { get; set; }
        public string PositionIdList { get; set; }
        public int CompID { get; set; }
        public decimal stud_percentile { get; set; }
    }
}

using System;

/// <summary>
/// Summary description for DO_Case
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_Case
    {
        public DO_Case()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Variable Declaration

        private int _caseId;
        private string _caseTitle;
        private string _citation;
        private string _enactmentCites;
        private string _jurisdiction;
        private string _provisionofLaw;
        private DateTime _judgementDate;
        private string _cites;
        private string _citedBy;
        private string _partyNames;
        private string _benchStrength;
        private string _judgeNames;
        private string _contentSource;
        private String _description;
        private int _addedBy;
        private DateTime _addedOn;
        private DateTime _modifieddOn;
        private int _modifiedBy;
        private string _iPAddress;
        public int year { get; set; }
        public String Condition { get; set; }
        public Int64 ContentTypeID { get; set; }
        public Int64 CurrentPage { get; set; }
        public Int64 PageSize { get; set; }
        public int CommnetId { get; set; }
        public int CommnetAddedFor { get; set; }
        #endregion


        #region properties
        public int CaseId
        {
            get { return _caseId; }
            set { _caseId = value; }
        }
        public int TagId
        {
            get { return _caseId; }
            set { _caseId = value; }
        }
        public string CaseTitle
        {
            get { return _caseTitle; }
            set { _caseTitle = value; }
        }

        public string Citation
        {
            get { return _citation; }
            set { _citation = value; }
        }

        public string EnactmentCites
        {
            get { return _enactmentCites; }
            set { _enactmentCites = value; }
        }

        public string Jurisdiction
        {
            get { return _jurisdiction; }
            set { _jurisdiction = value; }
        }

        public string ProvisionofLaw
        {
            get { return _provisionofLaw; }
            set { _provisionofLaw = value; }
        }

        public DateTime JudgementDate
        {
            get { return _judgementDate; }
            set { _judgementDate = value; }
        }

        public string Cites
        {
            get { return _cites; }
            set { _cites = value; }
        }

        public string CitedBy
        {
            get { return _citedBy; }
            set { _citedBy = value; }
        }

        public string PartyNames
        {
            get { return _partyNames; }
            set { _partyNames = value; }
        }
        public string BenchStrength
        {
            get { return _benchStrength; }
            set { _benchStrength = value; }
        }


        public string JudgeNames
        {
            get { return _judgeNames; }
            set { _judgeNames = value; }
        }

        public string ContentSource
        {
            get { return _contentSource; }
            set { _contentSource = value; }
        }

        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int AddedBy
        {
            get { return _addedBy; }
            set { _addedBy = value; }
        }

        public DateTime AddedOn
        {
            get { return _addedOn; }
            set { _addedOn = value; }
        }

        public DateTime ModifiedOn
        {
            get { return _modifieddOn; }
            set { _modifieddOn = value; }
        }
        public int ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }


        public string IPAddress
        {
            get { return _iPAddress; }
            set { _iPAddress = value; }
        }

        #endregion

        public string strFilePath { get; set; }
    }
}
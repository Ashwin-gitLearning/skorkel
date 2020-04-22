using System;
namespace DA_SKORKEL
{
    public class DO_UserScrlAdvanceSearch
    {
        public DO_UserScrlAdvanceSearch()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _PageSize;
        private int _Currentpage;
        private string _strInstituteName;
        private int _inSpecializationId;
        private string _strSpecialization;
        private int _intRegistrationId;
        private int _intCityId;
        private int _intCountryId;
        private int _intExperience;
        private string _strsearch;
        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;
        private int _intUserType;

        public int PageSize { get { return _PageSize; } set { _PageSize = value; } }
        public int Currentpage { get { return _Currentpage; } set { _Currentpage = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public string strsearch { get { return _strsearch; } set { _strsearch = value; } }
        public string strInstituteName { get { return _strInstituteName; } set { _strInstituteName = value; } }
        public int inSpecializationId { get { return _inSpecializationId; } set { _inSpecializationId = value; } }
        public string strSpecialization { get { return _strSpecialization; } set { _strSpecialization = value; } }
        public int intCityId { get { return _intCityId; } set { _intCityId = value; } }
        public int intCountryId { get { return _intCountryId; } set { _intCountryId = value; } }
        public int intExperience { get { return _intExperience; } set { _intExperience = value; } }  
        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }
        public int intUserType { get { return _intUserType; } set { _intUserType = value; } }
    }
}
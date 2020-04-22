using System;

/// <summary>
/// Summary description for DO_Registrationdetails
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_Registrationdetails
    {
        public DO_Registrationdetails()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Variable Declaration

        private int _registrationId;
        private string _firstName;
        private string _middleName;
        private string _lastName;
        private string _sex;
        private string _userName;
        private string _password;
        private string _address;        
        private string _country;
        private string _city;
        private string _state;
        private DateTime _addedOn;
        private int _addedBy;
        private string _ipAddress;
        private DateTime _modifiedOn;
        private int _modifiedBy;
        private string _name;
        public string strOthers { get; set; }

        public int intRequestInvitaionId { get; set; }
        public int UserTypeId { get; set; }
        public int InvitedUserId { get; set; }
        public string InstituteName { get; set; }
        public int ParentId { get; set; }
        public int Pin { get; set; }
        public int RollNo { get; set; }
        public string Batch { get; set; }
        public string PoliticalView { get; set; }
        public string Languages { get; set; }
        public string NonAchedemicInterest { get; set; }
        public string OtherPersonal { get; set; }
        public string PhotoPath { get; set; }
        public Int64 Phone { get; set; }
        public Int64 Mobile { get; set; }
        public string Designation { get; set; }
        public string University { get; set; }
        public string Alunmi { get; set; }
        public string CVPath { get; set; }
        public string Specialization { get; set; }
        public string Location { get; set; }
        public string Experience { get; set; }

        public string AkineTostatic { get; set; }
        public string WorkedAt { get; set; }
        public string ProfAchievements { get; set; }
        public string Publishings { get; set; }
        public string SubOfInterest { get; set; }
        public string Goals { get; set; }
        public string ExtraCredit { get; set; }
        public string Leadership { get; set; }
        public string LegalDept { get; set; }
        public string OtherLegalProj { get; set; }
        public string SubOfTought { get; set; }
        public int RegOutId { get; set; }
        public int InstituteId { get; set; }
        public int CourseId { get; set; }
        public int DeptId { get; set; }
        public int LawFirmId { get; set; }
        public int ApproveChildTblId { get; set; }
        public int IsApproved { get; set; }

        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }

        public string vchrRegistartionType { get; set; }
        public int intLawRelated { get; set; }

        public int intFollowStatus { get; set; }
        public int FollowId { get; set; }
        public int ConnectRegistrationId { get; set; }
        public int intInvitedUserId { get; set; }
        public int intOrgJoinId { get; set; }
        public int intOrgJoinStatus { get; set; }
        public int OrgId { get; set; }
        public int _intGroupId { get; set; }
        public int _intOrgnisationID { get; set; }

        public string _strSearch { get; set; }
        #endregion

        #region Properties
        public string strSearch
        {
            get { return _strSearch; }
            set { _strSearch = value; }
        }
        public int intGroupId
        {
            get { return _intGroupId; }
            set { _intGroupId = value; }
        }
        public int intOrgnisationID
        {
            get { return _intOrgnisationID; }
            set { _intOrgnisationID = value; }
        }
        public int RegistrationId
        {
            get { return _registrationId; }
            set { _registrationId = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string MiddleName
        {
            get { return _middleName; }
            set { _middleName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }



        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }        

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        public DateTime AddedOn
        {
            get { return _addedOn; }
            set { _addedOn = value; }
        }

        public int AddedBy
        {
            get { return _addedBy; }
            set { _addedBy = value; }
        }

        public string IpAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; }
        }

        public DateTime ModifiedOn
        {
            get { return _modifiedOn; }
            set { _modifiedOn = value; }
        }

        public int ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int intNotificationCount { get; set; }
        public int Currentpage { get; set; }
        public int PageSize { get; set; }

        #endregion







    }
}
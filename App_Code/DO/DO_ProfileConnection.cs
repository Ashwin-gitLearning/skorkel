using System;

/// <summary>
/// Summary description for DO_ProfileConnection
/// </summary>
namespace DA_SKORKEL
{
    public class DO_ProfileConnection
    {
        public DO_ProfileConnection()
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

        public int UserTypeId { get; set; }
        public int InvitedUserId { get; set; }
        public string InstituteName { get; set; }
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

        public string CompanyName { get; set; }
        public string WorkedAt { get; set; }
        public string ProfAchievements { get; set; }
        public string Publishings { get; set; }
        public string SubOfInterest { get; set; }
        public string Goals { get; set; }
 
        public string Leadership { get; set; }
        public string LegalDept { get; set; }
        public string OtherLegalProj { get; set; }
        public string Degree { get; set; }
        public int RegOutId { get; set; }

        public string Interests { get; set; }     
        #endregion

        #region Properties

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

        #endregion
    }
}
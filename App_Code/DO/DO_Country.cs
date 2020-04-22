using System;

/// <summary>
/// Summary description for DO_Country
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_Country
    {
        public DO_Country()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Variable Declaration

        private int _countryId;
        private string _countryName;
        private string _countryCode;
        private DateTime _addedOn;
        private int _addedBy;
        private string _ipAddress;
        private DateTime _modifiedOn;
        private int _modifiedBy;

        #endregion

        #region Properties

        public int CountryId
        {
            get { return _countryId; }
            set { _countryId = value; }
        }

        public string CountryName
        {
            get { return _countryName; }
            set { _countryName = value; }
        }

        public string CountryCode
        {
            get { return _countryCode; }
            set { _countryCode = value; }
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

        #endregion


    }
}





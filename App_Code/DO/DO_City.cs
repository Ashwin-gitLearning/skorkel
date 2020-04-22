using System;


/// <summary>
/// Summary description for DO_City
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_City
    {
        public DO_City()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Variable Declartion

        private int _cityId;
        private string _cityName;
        private int _stateId;
        private DateTime _addedOn;
        private int _addedBy;
        private string _ipAddress;
        private DateTime _modifiedOn;
        private int _modifiedBy;
        private int _CountryId;

        #endregion

        #region properties

        public int CityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }

        public string CityName
        {
            get { return _cityName; }
            set { _cityName = value; }
        }

        public int StateId
        {
            get { return _stateId; }
            set { _stateId = value; }
        }

        public int CountryId
        {
            get { return _CountryId; }
            set { _CountryId = value; }
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
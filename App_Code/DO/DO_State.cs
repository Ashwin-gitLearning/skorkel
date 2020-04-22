using System;

/// <summary>
/// Summary description for DO_State
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_State
    {
        public DO_State()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Variable Declaretion
      
        private int _stateid;
        private string _statename;
        private int _countryID;
        private DateTime _addedOn;
        private int _addedBy;
        private string _ipAddress;
        private DateTime _modifiedOn;
        private int _modifiedBy;

        #endregion

        #region Properties

        public int StateId
        {
            get { return _stateid; }
            set { _stateid = value; } 
        }
        public string StateName
        {
            get { return _statename; }
            set { _statename = value; }
        }

        public int CountryID
        {
            get { return _countryID; }
            set { _countryID = value; }
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
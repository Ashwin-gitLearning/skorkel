using System;

/// <summary>
/// Summary description for DO_UserType
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_UserType
    {
        public DO_UserType()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Variable Daclaration

        private int _userTypeId;
        private string _userType;
        private DateTime _addedOn;
        private int _addedBy;
        private string _ipAddress;
        private DateTime _modifiedOn;
        private int _modifiedBy;

        #endregion

        #region Properties
        public int UserTypeId
        {
            get { return _userTypeId; }
            set { _userTypeId = value; }
        }

        public string UserType
        {
            get { return _userType; }
            set { _userType = value; }
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
using System;

/// <summary>
/// Summary description for DO_Networks
/// </summary>
namespace DA_SKORKEL
{
    public class DO_Networks
    {
        public DO_Networks()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Variable Declaration
            private int _registrationId;       
            private string _name;      
            private DateTime _addedOn;
            private int _addedBy;
            private string _ipAddress;
            private DateTime _modifiedOn;
            private int _modifiedBy;                   
        #endregion

        #region Properties
            public int InvitedUserId { get; set; }
            public DateTime RequestAcceptedDate { get; set; }
            public string Status { get; set; }  

            public int RegistrationId
            {
                get { return _registrationId; }
                set { _registrationId = value; }
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

            public string NotificationDate { get; set; }
            public string CurrentPage { get; set; }
            public string CurrentPageSize { get; set; }
        #endregion


    }
}
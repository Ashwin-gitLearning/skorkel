using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_UserFavoriteList
    {
        public DO_Scrl_UserFavoriteList()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _intFavoriteId;
        private int _intRegistrationId;
        private int _intAddedBy;
        private string _strIpAddress;
        private int _PageSize;
        private int _Currentpage;

        public int intFavoriteId { get { return _intFavoriteId; } set { _intFavoriteId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }
        public int PageSize { get { return _PageSize; } set { _PageSize = value; } }
        public int Currentpage { get { return _Currentpage; } set { _Currentpage = value; } }
    }
}
using System;

/// <summary>
/// Summary description for DO_Lexipedia
/// </summary>
///

namespace DA_SKORKEL
{
    public class DO_Lexipedia
    {
        public DO_Lexipedia()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        #region proerties

        public String Condition { get; set; } 

        private int _lexipediaId;
        public int LexipediaId
        {
            get { return _lexipediaId; }
            set { _lexipediaId = value; }
        }

        private string lexipediaTitle;
        public string LexipediaTitle
        {
            get { return lexipediaTitle; }
            set { lexipediaTitle = value; }
        }

        private string subtitles;
        public string Subtitles
        {
            get { return subtitles; }
            set { subtitles = value; }
        }


        private string _authors;
        public string Authors
        {
            get { return _authors; }
            set { _authors = value; }
        }


        private string _reference;
        public string Reference
        {
            get { return _reference; }
            set { _reference = value; }
        }


        private DateTime _lexipadiaDate;
        public DateTime LexipadiaDate
        {
            get { return _lexipadiaDate; }
            set { _lexipadiaDate = value; }
        }

        private int _addedBy;
        public int AddedBy
        {
            get { return _addedBy; }
            set { _addedBy = value; }
        }


        private DateTime _addedOn;
        public DateTime AddedOn
        {
            get { return _addedOn; }
            set { _addedOn = value; }
        }

        private DateTime _modifiedOn;
        public DateTime ModifiedOn
        {
            get { return _modifiedOn; }
            set { _modifiedOn = value; }
        }

        private int _modifiedBy;
        public int ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private string _iPAddress;
        public string IPAddress
        {
            get { return _iPAddress; }
            set { _iPAddress = value; }
        }
    

        #endregion
    }
}
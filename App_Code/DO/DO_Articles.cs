using System;

/// <summary>
/// Summary description for DO_Articles
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_Articles
    {
        public DO_Articles()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Properties


        public String Condition { get; set; } 
        private int _articleId;
        public int ArticleId
        {
            get { return _articleId; }
            set { _articleId = value; }
        }


        private string _articleTitle;
        public string ArticleTitle
        {
            get { return _articleTitle; }
            set { _articleTitle = value; }
        }


        private string _citation;
        public string Citation
        {
            get { return _citation; }
            set { _citation = value; }
        }


        private string _enactmentCites;
        public string EnactmentCites
        {
            get { return _enactmentCites; }
            set { _enactmentCites = value; }
        }

        private string _jurisdiction;
        public string Jurisdiction
        {
            get { return _jurisdiction; }
            set { _jurisdiction = value; }
        }


        private string _provisionofLaw;
        public string ProvisionofLaw
        {
            get { return _provisionofLaw; }
            set { _provisionofLaw = value; }
        }

        private string _cites;
        public string Cites
        {
            get { return _cites; }
            set { _cites = value; }
        }

        private string _citedBy;
        public string CitedBy
        {
            get { return _citedBy; }
            set { _citedBy = value; }
        }

        private string _author;
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }


        private string _contentSource;
        public string ContentSource
        {
            get { return _contentSource; }
            set { _contentSource = value; }
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
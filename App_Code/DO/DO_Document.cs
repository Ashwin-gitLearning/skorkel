using System;

/// <summary>
/// Summary description for DO_Document
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_Document
    {
        public DO_Document()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Properties
        
        public int DocId { get; set; }
        public int StudentId { get; set; }
        public string DocTitle { get; set; }
        public string FilePath { get; set; }
        public string DocContent { get; set; }
        public DateTime AddedOn { get; set; }
        public int MicroTag { get; set; }
        public int AddedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string IpAddress { get; set; }
        public int intDocOutId { get; set; }

        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }
        #endregion
    }
}
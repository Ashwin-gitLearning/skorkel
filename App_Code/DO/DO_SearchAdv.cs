using System;

/// <summary>
/// Summary description for DO_Articles
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_AdvSearch
    {
        public DO_AdvSearch()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Properties

        public String Condition { get; set; }
        public int MarkID { get; set; }
        public int SummaryID { get; set; }
        public int FactID { get; set; }
        public int RatioID { get; set; }
        public int KeywordID { get; set; }
        public int CommentID { get; set; }
        public String ContentIDs { get; set; }
        public int Year { get; set; }
        public String Court { get; set; }
        public String YearPassed { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; } 
        public int MacroTag { get; set; }
        #endregion
    }
}
using System;

/// <summary>
/// Summary description for DO_Case
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_SAJournal
    {
        public DO_SAJournal()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int IntUserId { get; set; }
        public string FilePath { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public int JournalID { get; set; }
        public int ArticleID { get; set; }
        public bool ActiveStatus { get; set; }
        public int CommentId { get; set; }
        public string Comment { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }
        public string ArticleIdList { get; set; }
        public int ArticleWordCount { get; set; }
    }
}
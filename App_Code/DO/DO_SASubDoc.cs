using System;

/// <summary>
/// Summary description for DO_Case
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_SASubDoc
    {
        public DO_SASubDoc()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int IntUserId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public int CurrentPage { get; set; }
        public int CurrentPageSize { get; set; }
        public string TxtSearch { get; set; }
    }
}
using System;
namespace DA_SKORKEL
{
    public class DO_DocumentList
    {
        public DO_DocumentList()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int CitationId { get; set; }
        public string ContentTitle { get; set; }
        public int ContentTypeId { get; set; }
        public int Caseid { get; set; }
        public int AddedBy { get; set; }


        public int Juridictionid { get; set; }
        public int CitedById { get; set; }
        public int JudgeNameId { get; set; }
        public int CitesId { get; set; }
    }
}
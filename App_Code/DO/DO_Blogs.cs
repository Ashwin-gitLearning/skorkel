using System;


/// <summary>
/// Summary description for DO_City
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_Blogs
    {
        public DO_Blogs()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Variable Declartion

        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string WhoWrote { get; set; }
        public string SubjectMatter { get; set; }
        public DateTime BlogDate { get; set; }

        public int NoOfFollowers { get; set; }
        public int AddedBy { get; set; }
        public string IPAddress { get; set; }
        public String Condition { get; set; } 

        #endregion



    }


}
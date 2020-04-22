using System;


/// <summary>
/// Summary description for DO_City
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_Questions
    {
        public DO_Questions()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Variable Declartion

        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string WhoWrote { get; set; }
        public string WhoAnswered { get; set; }
        public string Answers { get; set; }
        public DateTime PublishingDate { get; set; }
        public int AddedBy { get; set; }
        public string IPAddress { get; set; }
        public String Condition { get; set; } 
        #endregion



    }


}
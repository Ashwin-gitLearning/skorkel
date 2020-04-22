using System;


/// <summary>
/// Summary description for DO_City
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_ParliamentDebates
    {
        public DO_ParliamentDebates()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Variable Declartion
        public DateTime DebateDate { get; set; }
        public string Session { get; set; }
        public string Member { get; set; }
        public int DebateType { get; set; }
        public string Subject { get; set; }
        public Int64 AddedBy { get; set; }  
       
        public string IPAddress { get; set; }
        public Int64 DebateId { get; set; }
        public Int64 Modifiedby { get; set; }
        public String Condition { get; set; } 

       

        #endregion



    }


}
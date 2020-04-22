using System;


/// <summary>
/// Summary description for DO_City
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_DebateType
    {
        public DO_DebateType()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Variable Declartion
       

        public int DebateTypeID { get; set; }
        public string ParliamentDebateType { get; set; }
        public Int64 AddedBy { get; set; }         
        public string IPAddress { get; set; }
        public Int64 ModifiedBy { get; set; }
       
       

        #endregion



    }


}
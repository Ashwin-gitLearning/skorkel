using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_Class
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_Class
    {
        public DO_Class()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Properties

        public int ClassLinkId{get;set;}
        public string ClassName{get;set;}
        public string DurationInYear{get;set;}
        public int CourseId{get;set;}
        public DateTime AddedOn{get;set;}
        public int AddedBy{get;set;}
        public string IpAddress{get;set;}
        public DateTime ModifiedOn{get;set;}
        public int ModifiedBy{get;set;}

        #endregion

     

    }
}
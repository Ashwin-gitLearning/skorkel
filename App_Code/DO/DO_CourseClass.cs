using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using SqlConn;


/// <summary>
/// Summary description for DO_CourseClass
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_CourseClass
    {
        public DO_CourseClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        #region Properties

        public int CourseDetailId { get; set; }
        public int ClassId { get; set; }
        public int FromClassId { get; set; }
        public string  StudentIds { get; set; }
        public int CourseId { get; set; }
        public string BatchYear { get; set; }
        public string PassingYear { get; set; }
        public int ParentId { get; set; }

        public DateTime AddedOn { get; set; }
        public int AddedBy { get; set; }
        public string IpAddress { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        

        public int RegistartionId { get; set; }
        public int UserTypeId { get; set; }
        public string StudentName { get; set; }
        public int ProfIds { get; set; }
        public string ProfessorName { get; set; }
        

        #endregion
    }
}
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
    public class DO_ProfCourseClass
    {
        public DO_ProfCourseClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        #region Properties

        public int ClassProfDetailsID { get; set; }
        public int ClassId { get; set; }
        public int FromClassId { get; set; }
        public string ProsfIds { get; set; }
        public int CourseId { get; set; }
        public string BatchYear { get; set; }
        public int DeptId { get; set; }
        public byte ActiveFlag { get; set; }
        

        public DateTime AddedOn { get; set; }
        public int AddedBy { get; set; }
        public string IpAddress { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        

        public int RegistartionId { get; set; }
        public int UserTypeId { get; set; }
        public string StudentName { get; set; }
        public string ProfessorName { get; set; }
        public string ClassName { get; set; }
        

        #endregion
    }
}
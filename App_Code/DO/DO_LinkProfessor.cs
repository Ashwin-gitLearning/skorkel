using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_LinkProfessor
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_LinkProfessor
    {
        public DO_LinkProfessor()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Properties

        public int LinkId { get; set; }
        public int ProfessorId { get; set; }
        public int DeptId { get; set; }
        public int CourseId { get; set; }
        public int SubjecId { get; set; }
        public string Semister { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string IPAddress { get; set; }

     
        #endregion

    }
}
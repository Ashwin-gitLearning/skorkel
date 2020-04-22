using System;

/// <summary>
/// Summary description for DO_Scrl_OrgDegree
/// </summary>
namespace DA_SKORKEL
{

    public class DO_Scrl_OrgDegree
    {
        public DO_Scrl_OrgDegree()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int intOrgDegreeId { get; set; }
        public string strDegreeTitle { get; set; }
        public int intNoOfYears { get; set; }
        public int intNoOfCredit { get; set; }
        public int intNoOfCourses { get; set; }
        public int intNoOfSemester { get; set; }
        public int AddedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string IpAddress { get; set; }
        public int orgId { get; set; }
    }
}
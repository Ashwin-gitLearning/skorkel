using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_Subject
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_Subject
    {
        public DO_Subject()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int CourseId { get; set; }
      //  public string DeptName { get; set; }
        public string CourseName { get; set; }
        public string Duration { get; set; }
        public string Pattern { get; set; }
        public string Marks { get; set; }
        public string Semister { get; set; }

        private int _addedBy;
        public int AddedBy
        {
            get { return _addedBy; }
            set { _addedBy = value; }
        }


        private DateTime _addedOn;
        public DateTime AddedOn
        {
            get { return _addedOn; }
            set { _addedOn = value; }
        }

        private DateTime _modifiedOn;
        public DateTime ModifiedOn
        {
            get { return _modifiedOn; }
            set { _modifiedOn = value; }
        }

        private int _modifiedBy;
        public int ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        private string _iPAddress;
        public string IPAddress
        {
            get { return _iPAddress; }
            set { _iPAddress = value; }
        }
    }
}
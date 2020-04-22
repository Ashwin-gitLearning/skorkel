using System;

/// <summary>
/// Summary description for DO_Testimonies
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DO_Testimonies
    {
        public DO_Testimonies()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public String Condition { get; set; } 

        private int _testimonialId;

        public int TestimonialId
        {
            get { return _testimonialId; }
            set { _testimonialId = value; }
        }

        private string _testimonialTitle;

        public string TestimonialTitle
        {
            get { return _testimonialTitle; }
            set { _testimonialTitle = value; }
        }

        private DateTime _publishingDate;

        public DateTime PublishingDate
        {
            get { return _publishingDate; }
            set { _publishingDate = value; }
        }

        private string _summary;

        public string Summary
        {
            get { return _summary; }
            set { _summary = value; }
        }

        private string _subjectMatter;

        public string SubjectMatter
        {
            get { return _subjectMatter; }
            set { _subjectMatter = value; }
        }

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
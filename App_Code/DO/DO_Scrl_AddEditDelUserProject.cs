using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_AddEditDelUserProject
    {
        public DO_Scrl_AddEditDelUserProject()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _intProjectId;
        private int _intProjectAttachId;
        private int _intRegistrationId;
        private string _strProjectTitle;
        private string _strProjectSummary;
        private string _strProjectDetails;
        private int _intSubjectId;

        private string _strFileName1;
        private string _strFileName2;
        private string _strFileName3;
        private string _strFileName4;
        private string _strFileName5;
        private string _strFileName6;
        private string _strFileName7;
        private string _strFileName8;
        private string _strFileName9;
        private string _strFileName10;

        private string _strFileDescription1;
        private string _strFileDescription2;
        private string _strFileDescription3;
        private string _strFileDescription4;
        private string _strFileDescription5;
        private string _strFileDescription6;
        private string _strFileDescription7;
        private string _strFileDescription8;
        private string _strFileDescription9;
        private string _strFileDescription10;

        private DateTime _dtAddedOn;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIpAddress;

        public int intProjectId { get { return _intProjectId; } set { _intProjectId = value; } }
        public int intProjectAttachId { get { return _intProjectAttachId; } set { _intProjectAttachId = value; } }
        public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
        public string strProjectTitle { get { return _strProjectTitle; } set { _strProjectTitle = value; } }
        public string strProjectSummary { get { return _strProjectSummary; } set { _strProjectSummary = value; } }
        public string strProjectDetails { get { return _strProjectDetails; } set { _strProjectDetails = value; } }
        public int intSubjectId { get { return _intSubjectId; } set { _intSubjectId = value; } }

        public string strFileName1 { get { return _strFileName1; } set { _strFileName1 = value; } }
        public string strFileName2 { get { return _strFileName2; } set { _strFileName2 = value; } }
        public string strFileName3 { get { return _strFileName3; } set { _strFileName3 = value; } }
        public string strFileName4 { get { return _strFileName4; } set { _strFileName4 = value; } }
        public string strFileName5 { get { return _strFileName5; } set { _strFileName5 = value; } }
        public string strFileName6 { get { return _strFileName6; } set { _strFileName6 = value; } }
        public string strFileName7 { get { return _strFileName7; } set { _strFileName7 = value; } }
        public string strFileName8 { get { return _strFileName8; } set { _strFileName8 = value; } }
        public string strFileName9 { get { return _strFileName9; } set { _strFileName9 = value; } }
        public string strFileName10 { get { return _strFileName10; } set { _strFileName10 = value; } }

        public string strFileDescription1 { get { return _strFileDescription1; } set { _strFileDescription1 = value; } }
        public string strFileDescription2 { get { return _strFileDescription2; } set { _strFileDescription2 = value; } }
        public string strFileDescription3 { get { return _strFileDescription3; } set { _strFileDescription3 = value; } }
        public string strFileDescription4 { get { return _strFileDescription4; } set { _strFileDescription4 = value; } }
        public string strFileDescription5 { get { return _strFileDescription5; } set { _strFileDescription5 = value; } }
        public string strFileDescription6 { get { return _strFileDescription6; } set { _strFileDescription6 = value; } }
        public string strFileDescription7 { get { return _strFileDescription7; } set { _strFileDescription7 = value; } }
        public string strFileDescription8 { get { return _strFileDescription8; } set { _strFileDescription8 = value; } }
        public string strFileDescription9 { get { return _strFileDescription9; } set { _strFileDescription9 = value; } }
        public string strFileDescription10 { get { return _strFileDescription10; } set { _strFileDescription10 = value; } }

        public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }
        public int PageSize { get; set; }
        public int Currentpage { get; set; }
    }
}
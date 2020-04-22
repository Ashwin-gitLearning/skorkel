using System;
namespace DA_SKORKEL
{
    public class DO_Scrl_MarksTbl
    {
        public DO_Scrl_MarksTbl()
        { }
        private int _intMarksId;
        private int _intClassId;
        private int _intSubjectId;
        private int _intStudentId;
        private int _intProfesorId;
        private int _intMarks;
        private int _intOutOf;
        private int _intAddedBy;
        private DateTime _dtModifiedOn;
        private int _intModifiedBy;
        private string _strIPAddress;

        public int intMarksId { get { return _intMarksId; } set { _intMarksId = value; } }
        public int intClassId { get { return _intClassId; } set { _intClassId = value; } }
        public int intSubjectId { get { return _intSubjectId; } set { _intSubjectId = value; } }
        public int intStudentId { get { return _intStudentId; } set { _intStudentId = value; } }
        public int intProfesorId { get { return _intProfesorId; } set { _intProfesorId = value; } }
        public int intMarks { get { return _intMarks; } set { _intMarks = value; } }
        public int intOutOf { get { return _intOutOf; } set { _intOutOf = value; } }
        public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
        public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
        public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
        public string strIPAddress { get { return _strIPAddress; } set { _strIPAddress = value; } }

    }
}

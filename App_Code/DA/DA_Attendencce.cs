using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_ParliamentDebates
/// </summary>


namespace DA_SKORKEL
{
    public class DA_Attendence
    {
        public DA_Attendence()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public enum Attendence
        {
            GetCourseData = 1, GetClassData = 2, GetSubjectDetails = 3, AllRecord = 4, Add = 5, SearchRecord = 6, AttendenceRecord = 8, ViewAtendee = 9//GetClassForStud = 7,
        };


        SqlCommand cmd = new SqlCommand();

        public void AddEditDel_Attendence(DO_Attendence objAttendence, Attendence flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelAttendence", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@AttendenceId", SqlDbType.BigInt).Value = objAttendence.AttendenceId;
            cmd.Parameters.Add("@CourseId", SqlDbType.BigInt).Value = objAttendence.CourseId;
            cmd.Parameters.Add("@ClassId", SqlDbType.BigInt).Value = objAttendence.ClassId;
            cmd.Parameters.Add("@SubjectId", SqlDbType.BigInt).Value = objAttendence.Subjectid;
            cmd.Parameters.Add("@StudentIds", SqlDbType.VarChar, 8000).Value = objAttendence.StudentIDs;
            cmd.Parameters.Add("@AttendenceDate", SqlDbType.DateTime).Value = objAttendence.AttendenceDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objAttendence.AddedBy;
            cmd.Parameters.Add("@UserTypeId", SqlDbType.BigInt).Value = objAttendence.UserTypeID;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.BigInt).Value = objAttendence.ModifiedBy;
            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 50).Value = objAttendence.IPAddress;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }


        public DataTable GetDatatableAttendence(DO_Attendence objAttendence, Attendence flag)
        {
            //For Retriving Record For Attendence

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelAttendence", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@RegistrationId", SqlDbType.BigInt).Value = objAttendence.RegistrationId;
            da.SelectCommand.Parameters.Add("@CourseId", SqlDbType.BigInt).Value = objAttendence.CourseId;
            da.SelectCommand.Parameters.Add("@ClassId", SqlDbType.BigInt).Value = objAttendence.ClassId;
            da.SelectCommand.Parameters.Add("@SubjectId", SqlDbType.BigInt).Value = objAttendence.Subjectid;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objAttendence.AddedBy;
            da.SelectCommand.Parameters.Add("@UserTypeId", SqlDbType.BigInt).Value = objAttendence.UserTypeID;
            da.SelectCommand.Parameters.Add("@AttendenceDate", SqlDbType.DateTime).Value = objAttendence.AttendenceDate;
            da.SelectCommand.Parameters.Add("@yearPaseed", SqlDbType.BigInt).Value = objAttendence.YearPassed;
            da.SelectCommand.Parameters.Add("@MonthPassed", SqlDbType.BigInt).Value = objAttendence.MonthPassed;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
    }
}
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_ParliamentDebates
/// </summary>


namespace DA_SKORKEL
{
    public class DA_MarksFeeding
    {
        public DA_MarksFeeding()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public enum Marks
        {
            Add = 1, GetMarks = 2, GetStudDetails = 3, AllRecord = 4, GetCourseData = 5, SearchRecord = 6
        };


        SqlCommand cmd = new SqlCommand();

        public void AddEditDel_Marks(DO_MarksFeeding objMarks, Marks flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelStudMarks", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@MarksId", SqlDbType.BigInt).Value = objMarks.MarksId;
            cmd.Parameters.Add("@CourseId", SqlDbType.BigInt).Value = objMarks.CourseId;
            cmd.Parameters.Add("@ClassId", SqlDbType.BigInt).Value = objMarks.ClassId;
            cmd.Parameters.Add("@SubjectId", SqlDbType.BigInt).Value = objMarks.Subjectid;
            cmd.Parameters.Add("@StudentId", SqlDbType.BigInt).Value = objMarks.StudentID;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objMarks.AddedBy;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.BigInt).Value = objMarks.ModifiedBy;
            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 50).Value = objMarks.IPAddress;
            cmd.Parameters.Add("@MarksObtained", SqlDbType.BigInt).Value = objMarks.MarksObtained;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }


        public DataTable GetDatatableMarks(DO_MarksFeeding objMarks, Marks flag)
        {
            //For Retriving Record For Marks

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelStudMarks", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@RegistrationId", SqlDbType.BigInt).Value = objMarks.RegistrationId;
            da.SelectCommand.Parameters.Add("@SubjectId", SqlDbType.BigInt).Value = objMarks.Subjectid;
            da.SelectCommand.Parameters.Add("@ClassId", SqlDbType.BigInt).Value = objMarks.ClassId;
            da.SelectCommand.Parameters.Add("@CourseId", SqlDbType.BigInt).Value = objMarks.CourseId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objMarks.AddedBy;     
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
    }
}
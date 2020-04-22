using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using SqlConn;

/// <summary>
/// Summary description for DA_CourseClass
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DA_Scrl_StudentWiseSubjects
    {
        public DA_Scrl_StudentWiseSubjects()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        SqlCommand cmd = new SqlCommand();
        public enum StudentWiseSubjects
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5
        };

        public DataTable GetDataTable(DO_Scrl_StudentWiseSubjects objSubj, StudentWiseSubjects flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_StudentWiseSubjects", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intStudentId", SqlDbType.BigInt).Value = objSubj.intStudentId;
            da.SelectCommand.Parameters.Add("@intSubjectId", SqlDbType.BigInt).Value = objSubj.intSubjectId;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
    }
}
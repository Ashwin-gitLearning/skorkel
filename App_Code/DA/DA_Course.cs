using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SqlConn;


/// <summary>
/// Summary description for DA_Course
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DA_Course
    {
        public DA_Course()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public enum Course
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5, CheckInSubject = 6, CourseDeptName = 7, Duration = 8, GetCourseId=9
        };

        SqlCommand cmd = new SqlCommand();

        public DataTable GetDataTable(DO_Course objCourse, Course flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelCourse", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@CourseId", SqlDbType.Int).Value = objCourse.CourseId;
            //da.SelectCommand.Parameters.Add("@DeptId", SqlDbType.Int).Value = objCourse.DeptId;
            da.SelectCommand.Parameters.Add("@CourseName", SqlDbType.VarChar,200).Value = objCourse.CourseName;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objCourse.AddedBy ;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_Course(DO_Course  objCourse, DA_Course.Course flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelCourse", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@DeptId", SqlDbType.Int).Value = objCourse.DeptId;
            cmd.Parameters.Add("@CourseId", SqlDbType.Int).Value = objCourse.CourseId;
            cmd.Parameters.Add("@CourseName", SqlDbType.VarChar,200).Value = objCourse.CourseName;
            cmd.Parameters.Add("@DurationYear", SqlDbType.VarChar,200).Value = objCourse.DurationInYear;
            cmd.Parameters.Add("@Pattern", SqlDbType.VarChar,200).Value = objCourse.Pattern;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objCourse.AddedBy;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = objCourse.ModifiedBy;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }


    }
}
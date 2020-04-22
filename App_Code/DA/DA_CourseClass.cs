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
    public class DA_CourseClass
    {
        public DA_CourseClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        SqlCommand cmd = new SqlCommand();
        public enum CourseClass
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5, StudentList = 6,GetStudentIds=7
        };

        public DataTable GetDataTable(DO_CourseClass objClass, CourseClass flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelCourseClassDetail", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@CourseDetailId", SqlDbType.BigInt).Value = objClass.CourseDetailId;
            da.SelectCommand.Parameters.Add("@ClassId", SqlDbType.BigInt).Value = objClass.ClassId;
            da.SelectCommand.Parameters.Add("@CourseId", SqlDbType.BigInt).Value = objClass.CourseId;
           
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_CourseClass(DO_CourseClass objClass, CourseClass flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelCourseClassDetail", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@CourseDetailId", SqlDbType.BigInt).Value = objClass.CourseDetailId;
            cmd.Parameters.Add("@ClassId", SqlDbType.BigInt).Value = objClass.ClassId;
            cmd.Parameters.Add("@FromClassId", SqlDbType.BigInt).Value = objClass.FromClassId;
            cmd.Parameters.Add("@CourseId", SqlDbType.BigInt).Value = objClass.CourseId;
            cmd.Parameters.Add("@StudentIds", SqlDbType.VarChar, 200).Value = objClass.StudentIds;
            cmd.Parameters.Add("@BatchYear", SqlDbType.VarChar, 200).Value = objClass.BatchYear;
            cmd.Parameters.Add("@PassingYear", SqlDbType.VarChar, 200).Value = objClass.PassingYear;
            cmd.Parameters.Add("@ParentId", SqlDbType.BigInt).Value = objClass.ParentId;


            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objClass.AddedBy;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.BigInt).Value = objClass.ModifiedBy;
            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 200).Value = objClass.IpAddress;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }








    }
}
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
    public class DA_ProfCourseClass
    {
        public DA_ProfCourseClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        SqlCommand cmd = new SqlCommand();
        public enum ProfCourseClass
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5, ProfessorList = 6,GetStudentIds=7,DeptName=8,GetClassNames=9,DetachClass=10
        };

        public DataTable GetDataTable(DO_ProfCourseClass objClass, ProfCourseClass flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelProfessorCourseClassDetail", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@ClassProfDetailsID", SqlDbType.BigInt).Value = objClass.ClassProfDetailsID;
            da.SelectCommand.Parameters.Add("@ClassId", SqlDbType.BigInt).Value = objClass.ClassId;
            da.SelectCommand.Parameters.Add("@DeptId", SqlDbType.BigInt).Value = objClass.DeptId;
            da.SelectCommand.Parameters.Add("@CourseId", SqlDbType.BigInt).Value = objClass.CourseId;
            da.SelectCommand.Parameters.Add("@ProsfIds", SqlDbType.VarChar, 200).Value = objClass.ProsfIds;
            da.SelectCommand.Parameters.Add("@ClassName", SqlDbType.VarChar, 200).Value = objClass.ClassName;
            da.SelectCommand.Parameters.Add("@ActiveFlag", SqlDbType.VarChar, 200).Value = objClass.ActiveFlag;

           
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_ProfCourseClass(DO_ProfCourseClass objClass, ProfCourseClass flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelProfessorCourseClassDetail", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@ClassProfDetailsID", SqlDbType.BigInt).Value = objClass.ClassProfDetailsID;
            cmd.Parameters.Add("@ClassId", SqlDbType.BigInt).Value = objClass.ClassId;
            cmd.Parameters.Add("@FromClassId", SqlDbType.BigInt).Value = objClass.FromClassId;
            cmd.Parameters.Add("@CourseId", SqlDbType.BigInt).Value = objClass.CourseId;
            cmd.Parameters.Add("@ProsfIds", SqlDbType.VarChar, 200).Value = objClass.ProsfIds;
            cmd.Parameters.Add("@BatchYear", SqlDbType.VarChar, 200).Value = objClass.BatchYear;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objClass.AddedBy;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.BigInt).Value = objClass.ModifiedBy;
            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 200).Value = objClass.IpAddress;
            cmd.Parameters.Add("@DeptId", SqlDbType.BigInt).Value = objClass.DeptId;
            cmd.Parameters.Add("@ClassName", SqlDbType.VarChar, 200).Value = objClass.ClassName;
            cmd.Parameters.Add("@ActiveFlag", SqlDbType.VarChar, 200).Value = objClass.ActiveFlag;

            
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }








    }
}
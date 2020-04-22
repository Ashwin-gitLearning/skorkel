using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using SqlConn;


/// <summary>
/// Summary description for DA_Class
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DA_Class
    {

        private SqlCommand cmd;
        public DA_Class()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    public enum Class
    {
        Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5,ClassName=6,CheckClassInStuProfCourseClass=8
    };

     public DataTable GetDataTable(DO_Class objClass, Class flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_ClassAddEditDel_SP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@ClassLinkId", SqlDbType.BigInt).Value = objClass.ClassLinkId;
            da.SelectCommand.Parameters.Add("@ClassName", SqlDbType.VarChar,200).Value = objClass.ClassName;
            da.SelectCommand.Parameters.Add("@CourseId", SqlDbType.BigInt).Value = objClass.CourseId;
            da.SelectCommand.Parameters.Add("@Year", SqlDbType.VarChar,200).Value = objClass.DurationInYear;
         
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_Class(DO_Class objClass, Class flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("scrl_ClassAddEditDel_SP", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@ClassLinkId", SqlDbType.BigInt).Value = objClass.ClassLinkId;
            cmd.Parameters.Add("@ClassName", SqlDbType.NVarChar).Value = objClass.ClassName;
            cmd.Parameters.Add("@CourseId", SqlDbType.BigInt).Value = objClass.CourseId;
            cmd.Parameters.Add("@Year", SqlDbType.VarChar,200).Value = objClass.DurationInYear;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objClass.AddedBy;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.BigInt).Value = objClass.ModifiedBy;
            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 200).Value = objClass.IpAddress;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }







    }
}
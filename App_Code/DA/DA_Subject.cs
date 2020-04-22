using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using SqlConn;


/// <summary>
/// Summary description for DA_Subject
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DA_Subject
    {
        public DA_Subject()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public enum Subject
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5, CourseDeptwise = 6, CourseSelectionwise = 7, CourseSubjects = 8, SemisterSubjectwise = 9
        };

        SqlCommand cmd = new SqlCommand();
            
        public DataTable GetDataTable(DO_Subject objSubject, DA_Subject.Subject   flag)       
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelSubjectMaster", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@SubjectId", SqlDbType.Int).Value = objSubject.SubjectId;
            da.SelectCommand.Parameters.Add("@CourseId", SqlDbType.Int).Value = objSubject.CourseId;
           //da.SelectCommand.Parameters.Add("@DeptId", SqlDbType.Int).Value = objSubject.DeptId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objSubject.AddedBy;
            da.SelectCommand.Parameters.Add("@Semister", SqlDbType.VarChar, 200).Value = objSubject.Semister;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_Subject(DO_Subject objSubject, DA_Subject.Subject flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelSubjectMaster", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;

            cmd.Parameters.Add("@CourseId", SqlDbType.Int).Value = objSubject.CourseId;
            cmd.Parameters.Add("@CourseName", SqlDbType.VarChar, 200).Value = objSubject.CourseName;
            cmd.Parameters.Add("@SubjectId", SqlDbType.Int).Value = objSubject.SubjectId;
           //cmd.Parameters.Add("@DeptId", SqlDbType.Int).Value = objSubject.DeptId;
            cmd.Parameters.Add("@SubjectName", SqlDbType.VarChar, 200).Value = objSubject.SubjectName;
            cmd.Parameters.Add("@Duration", SqlDbType.VarChar, 200).Value = objSubject.Duration;
            cmd.Parameters.Add("@Marks", SqlDbType.VarChar, 200).Value = objSubject.Marks;
            cmd.Parameters.Add("@Semister", SqlDbType.VarChar, 200).Value = objSubject.Semister;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objSubject.AddedBy;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = objSubject.ModifiedBy;
            
                
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }





    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_LinkProfessor
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DA_LinkProfessor
    {
        public DA_LinkProfessor()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum LinkProfessor
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5,ProfList=6,DeptList=7,GetsubName=8,Detach=9
        };

        SqlCommand cmd = new SqlCommand();

        public DataTable GetDataTable(DO_LinkProfessor objLinkProfessor, DA_LinkProfessor.LinkProfessor flag)
        {
            DataTable dt = new DataTable();
            SqlConnection cn = new SqlConnection();
            SQLManager co = new SQLManager();
            cn=co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelLinkProfessor", cn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@LinkId", SqlDbType.BigInt).Value = objLinkProfessor.LinkId;
            da.SelectCommand.Parameters.Add("@ProfessorId", SqlDbType.BigInt).Value = objLinkProfessor.ProfessorId;
            da.SelectCommand.Parameters.Add("@SubjectId", SqlDbType.BigInt).Value = objLinkProfessor.SubjecId;
            da.SelectCommand.Parameters.Add("@DeptId", SqlDbType.BigInt).Value = objLinkProfessor.DeptId;
            //da.SelectCommand.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = objLinkProfessor.FromDate;
            //da.SelectCommand.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = objLinkProfessor.ToDate;
            da.Fill(dt);
            co.CloseConnection(cn);
            return dt;
        }

        public void AddEditDelLinkProfessor(DO_LinkProfessor objLinkProfessor, DA_LinkProfessor.LinkProfessor  flag)
        {
            SqlConnection cn = new SqlConnection();
            SQLManager co = new SQLManager();
            cn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelLinkProfessor", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@LinkId", SqlDbType.BigInt).Value = objLinkProfessor.LinkId;
            cmd.Parameters.Add("@ProfessorId", SqlDbType.BigInt).Value = objLinkProfessor.ProfessorId;
            cmd.Parameters.Add("@SubjectId", SqlDbType.BigInt).Value = objLinkProfessor.SubjecId;
            cmd.Parameters.Add("@DeptId", SqlDbType.BigInt).Value = objLinkProfessor.DeptId;
            cmd.Parameters.Add("@CourseId", SqlDbType.BigInt).Value = objLinkProfessor.CourseId;
            cmd.Parameters.Add("@Semister", SqlDbType.VarChar,200).Value = objLinkProfessor.Semister;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objLinkProfessor.AddedBy;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = objLinkProfessor.ModifiedBy;
            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar,200).Value = objLinkProfessor.Semister;
            cmd.ExecuteNonQuery();
            co.CloseConnection(cn);

        }


        //public int checkDelLinkProfessor(DO_LinkProfessor objLinkProfessor, DA_LinkProfessor.LinkProfessor flag)
        //{
        //    SqlConnection cn = new SqlConnection();
        //    SQLManager co = new SQLManager();
        //    cn = co.GetConnection();

        //    cmd = new SqlCommand("Scrl_AddEditDelLinkProfessor", cn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        //    cmd.Parameters.Add("@LinkId", SqlDbType.BigInt).Value = objLinkProfessor.LinkId;
        //    cmd.Parameters.Add("@ProfessorId", SqlDbType.BigInt).Value = objLinkProfessor.ProfessorId;
        //    cmd.Parameters.Add("@SubjectId", SqlDbType.BigInt).Value = objLinkProfessor.SubjecId;
        //    cmd.Parameters.Add("@DeptId", SqlDbType.BigInt).Value = objLinkProfessor.DeptId;
        //    cmd.Parameters.Add("@CourseId", SqlDbType.BigInt).Value = objLinkProfessor.CourseId;
        //    cmd.Parameters.Add("@Semister", SqlDbType.VarChar, 200).Value = objLinkProfessor.Semister;
        //    cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objLinkProfessor.AddedBy;
        //    cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = objLinkProfessor.ModifiedBy;
        //    cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 200).Value = objLinkProfessor.Semister;
        //    cmd.ExecuteNonQuery();
        //    co.CloseConnection(cn);


        //}

        


    }
}
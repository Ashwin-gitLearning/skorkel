using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_Scrl_OrgDegree
/// </summary>
namespace DA_SKORKEL
{

    public class DA_Scrl_OrgDegree
    {
        private SqlCommand cmd;
        public enum Scrl_OrgDegree
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
        };
        public DA_Scrl_OrgDegree()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public void AddEditDel_Scrl_OrgDegree(DO_Scrl_OrgDegree ObjScrl_OrgDegree, Scrl_OrgDegree Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_OrgDegree", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intOrgDegreeId", SqlDbType.Int).Value = ObjScrl_OrgDegree.intOrgDegreeId;
            cmd.Parameters.Add("@strDegreeTitle", SqlDbType.VarChar,200).Value = ObjScrl_OrgDegree.strDegreeTitle;
            cmd.Parameters.Add("@intNoOfCredit", SqlDbType.Int).Value = ObjScrl_OrgDegree.intNoOfCredit;
            cmd.Parameters.Add("@intNoOfYears", SqlDbType.Int).Value = ObjScrl_OrgDegree.intNoOfYears;
            cmd.Parameters.Add("@intNoOfCourses", SqlDbType.Int).Value = ObjScrl_OrgDegree.intNoOfCourses;
            cmd.Parameters.Add("@intNoOfSemester", SqlDbType.Int).Value = ObjScrl_OrgDegree.intNoOfSemester;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = ObjScrl_OrgDegree.ModifiedBy;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = ObjScrl_OrgDegree.AddedBy;
            cmd.Parameters.Add("@IpAddress", SqlDbType.VarChar,20).Value = ObjScrl_OrgDegree.IpAddress;
            cmd.Parameters.Add("@orgId", SqlDbType.VarChar, 20).Value = ObjScrl_OrgDegree.orgId;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetDataTable(DO_Scrl_OrgDegree ObjScrl_OrgDegree, Scrl_OrgDegree Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_OrgDegree", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intOrgDegreeId", SqlDbType.Int).Value = ObjScrl_OrgDegree.intOrgDegreeId;
            da.SelectCommand.Parameters.Add("@orgId", SqlDbType.Int).Value = ObjScrl_OrgDegree.orgId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.Int).Value = ObjScrl_OrgDegree.AddedBy;
            da.SelectCommand.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = ObjScrl_OrgDegree.ModifiedBy;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

    }
}
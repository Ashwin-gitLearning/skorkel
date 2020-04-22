using SqlConn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DA_Scrl_JobCandidate
/// </summary>
public class DA_Scrl_JobCandidate
{
    private SqlCommand cmd;

    public DA_Scrl_JobCandidate()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public enum JobCandidate
    {
        AddJobCandidate = 9, AppliedStatus = 10, AppliedCountStatus = 11, GetCandidateDetails = 12
    }

    public int AddEditJobCandidate(DO_Scrl_JobCandidate objcategory, JobCandidate flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();

        cmd = new SqlCommand("Scrl_JobsListing", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@Job_ID", SqlDbType.Int).Value = objcategory.Job_ID;
        cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = objcategory.User_ID;        
        cmd.Parameters.Add("@Resume_path", SqlDbType.VarChar, 200).Value = objcategory.Resume_path;
        cmd.Parameters.Add("@Resume_title", SqlDbType.VarChar, 200).Value = objcategory.Resume_file_title;
        
        objcategory.intJobsOutId = Convert.ToInt32(cmd.ExecuteScalar());
        co.CloseConnection(conn);
        return objcategory.intJobsOutId;
    }

    public int GetAppliedStatus(DO_Scrl_JobCandidate objcategory, JobCandidate flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();

        cmd = new SqlCommand("Scrl_JobsListing", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@Job_ID", SqlDbType.Int).Value = objcategory.Job_ID;
        if (Convert.ToString(objcategory.User_ID) != "")
        {
            cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = objcategory.User_ID;
        }
        //cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = objcategory.User_ID;

        objcategory.intJobsOutId = Convert.ToInt32(cmd.ExecuteScalar());
        co.CloseConnection(conn);
        return objcategory.intJobsOutId;
    }

    public DataTable GetDataTable(DO_Scrl_JobCandidate objcategory, JobCandidate flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_JobsListing", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;

        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@CurrentPage", SqlDbType.Int).Value = objcategory.CurrentPage;
        da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = objcategory.CurrentPageSize;
        
        if (Convert.ToString(objcategory.Job_ID) != "")
        {
            da.SelectCommand.Parameters.Add("@Job_ID", SqlDbType.Int).Value = objcategory.Job_ID;
        }

        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }
}
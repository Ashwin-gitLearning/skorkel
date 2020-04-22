using SqlConn;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DA_Scrl_JobsListing
/// </summary>
public class DA_Scrl_JobsListing
{
    private SqlCommand cmd;

    public DA_Scrl_JobsListing()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public enum JobsListing
    {
        GetJobsListing = 1, AddJobs = 2, DeleteJobsByID = 3, FilteredJobs = 4, SingleRecord = 5, UpdateJobStatus = 6, UpdateJobByID = 7, GetJobsListingCU = 8
    }

    public DataTable GetDataTable(DO_Scrl_JobsListing objcategory, JobsListing flag)
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
        if (Convert.ToString(objcategory.Status) != "")
        {
            da.SelectCommand.Parameters.Add("@ActiveStatus", SqlDbType.VarChar, 2).Value = objcategory.Status;
        }
        if (Convert.ToString(objcategory.ID) != "")
        {
            da.SelectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = objcategory.ID;
        }

        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }

    public int Add_JobsListing(DO_Scrl_JobsListing objcategory, JobsListing flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_JobsListing", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 100).Value = objcategory.Title;
        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100000000).Value = objcategory.Description;
        cmd.Parameters.Add("@Location", SqlDbType.VarChar, 200).Value = objcategory.Location;
        cmd.Parameters.Add("@JobType", SqlDbType.VarChar, 200).Value = objcategory.JobType;
        cmd.Parameters.Add("@StartingSalary", SqlDbType.VarChar, 20).Value = objcategory.StartingSalary;
        cmd.Parameters.Add("@EndingSalary", SqlDbType.VarChar, 20).Value = objcategory.EndingSalary;
        cmd.Parameters.Add("@StartDuration", SqlDbType.VarChar, 200).Value = objcategory.StartDuration;
        cmd.Parameters.Add("@EndDuration", SqlDbType.VarChar, 200).Value = objcategory.EndDuration;

        objcategory.intJobsOutId = Convert.ToInt32(cmd.ExecuteScalar());
        co.CloseConnection(conn);
        return objcategory.intJobsOutId;
    }

    public void Edit_JobsListing(DO_Scrl_JobsListing objcategory, JobsListing flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_JobsListing", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = objcategory.ID;
        cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 100).Value = objcategory.Title;
        cmd.Parameters.Add("@Description", SqlDbType.VarChar, 100000000).Value = objcategory.Description;
        cmd.Parameters.Add("@Location", SqlDbType.VarChar, 200).Value = objcategory.Location;
        cmd.Parameters.Add("@JobType", SqlDbType.VarChar, 200).Value = objcategory.JobType;
        cmd.Parameters.Add("@StartingSalary", SqlDbType.VarChar, 20).Value = objcategory.StartingSalary;
        cmd.Parameters.Add("@EndingSalary", SqlDbType.VarChar, 20).Value = objcategory.EndingSalary;
        cmd.Parameters.Add("@StartDuration", SqlDbType.VarChar, 200).Value = objcategory.StartDuration;
        cmd.Parameters.Add("@EndDuration", SqlDbType.VarChar, 200).Value = objcategory.EndDuration;

        cmd.ExecuteReader();
        co.CloseConnection(conn);
    }

    public void chk_JobListing(DO_Scrl_JobsListing objcategory, JobsListing flag, Boolean chkTgl)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_JobsListing", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@checkJobs", SqlDbType.Bit).Value = chkTgl;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = objcategory.ID;

        cmd.ExecuteReader();
        co.CloseConnection(conn);
    }
    //public void DelByID_JobsListing(DO_Scrl_JobsListing objcategory, JobsListing flag)
    //{
    //    SqlConnection conn = new SqlConnection();
    //    SQLManager co = new SQLManager();
    //    conn = co.GetConnection();
    //    cmd = new SqlCommand("Scrl_JobsListing", conn);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
    //    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = objcategory.ID;

    //    cmd.ExecuteReader();
    //    co.CloseConnection(conn);
    //}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SqlConn;
using System.Data;

/// <summary>
/// Summary description for DA_Logdetails
/// </summary>
public class DA_Logdetails
{
    SqlConnection conn = new SqlConnection();
    SQLManager co = new SQLManager();
    private SqlCommand cmd;
	public DA_Logdetails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public enum LogDetails
    {
        Insert = 1, GetLog=2,GetLogBydate=3
    };

    public void AddEditDel_Scrl_LogDetailsMaster(DO_LogDetails objLog, LogDetails flag)
    {
        conn = co.GetConnection();
        cmd = new SqlCommand("SP_Scrl_LogDetails", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@intActionId", SqlDbType.Int).Value = objLog.intActionId;
        cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objLog.intAddedBy;
        cmd.Parameters.Add("@intDeletedBy", SqlDbType.VarChar, 500).Value = objLog.intDeletedBy;
        cmd.Parameters.Add("@SectionId", SqlDbType.VarChar, 500).Value = objLog.SectionId;
        cmd.Parameters.Add("@strAction", SqlDbType.VarChar, 100).Value = objLog.strAction;
        cmd.Parameters.Add("@strIPAddress", SqlDbType.VarChar, 20).Value = objLog.strIPAddress;
        cmd.Parameters.Add("@dtDeletedOn", SqlDbType.VarChar, 20).Value = objLog.dtDeletedOn;
        cmd.Parameters.Add("@strActionName", SqlDbType.VarChar, 500000).Value = objLog.strActionName;

        cmd.ExecuteNonQuery();
        co.CloseConnection(conn);
    }

    public DataTable GetDatatable(DO_LogDetails objLog, LogDetails flag)
    {
        DataTable dt = new DataTable();
        conn = co.GetConnection();

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("SP_Scrl_LogDetails", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;

        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@intActionId", SqlDbType.Int).Value = objLog.intActionId;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objLog.intAddedBy;
        da.SelectCommand.Parameters.Add("@intDeletedBy", SqlDbType.VarChar, 500).Value = objLog.intDeletedBy;
        da.SelectCommand.Parameters.Add("@SectionId", SqlDbType.VarChar, 500).Value = objLog.SectionId;
        da.SelectCommand.Parameters.Add("@strAction", SqlDbType.VarChar, 100).Value = objLog.strAction;
        da.SelectCommand.Parameters.Add("@strIPAddress", SqlDbType.VarChar, 20).Value = objLog.strIPAddress;
        da.SelectCommand.Parameters.Add("@dtDeletedOn", SqlDbType.VarChar, 20).Value = objLog.dtDeletedOn;

        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }
}
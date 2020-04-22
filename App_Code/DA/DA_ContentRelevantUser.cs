using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_ContentRelevantUser
/// </summary>
public class DA_ContentRelevantUser
{
	public DA_ContentRelevantUser()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public enum RelevantUser
    {
        GetRelevantUser = 1, GetRelevantUserDetails = 2, InsertDocViewDetails = 3,GetUserName=4,
    };


    public DataTable GetDataTable(DO_ContentRelevantUser objDORelevntU, RelevantUser flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelRelevantUser", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objDORelevntU.CaseId;
        da.SelectCommand.Parameters.Add("@ContentTypeId", SqlDbType.BigInt).Value = objDORelevntU.ContentTypeID;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objDORelevntU.AddedBy;
        da.SelectCommand.Parameters.Add("@intViewId", SqlDbType.Int).Value = objDORelevntU.intViewId;
        da.SelectCommand.Parameters.Add("@intDocAddedBy", SqlDbType.Int).Value = objDORelevntU.intDocAddedBy;

        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using SqlConn;
using System.Data;

/// <summary>
/// Summary description for DA_ResearchResult
/// </summary>
public class DA_ResearchResult
{
    SqlConnection conn = new SqlConnection();
    SQLManager co = new SQLManager();

	public DA_ResearchResult()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public enum Research
    {
        GetYear = 1, GetCourt = 2, GetJudes = 3, GetYearCitation = 4, GetCourtCi = 5, GetReportCi = 6, GetVolumeCi = 7, GetPagenoCi = 8,
        GetReportyearCi = 9, GetPagenoyearCi = 10, GetReportyearsCi = 11,
    }



    public DataTable GetDataTable(DO_ResearchResult DOobjResearch, Research flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelGetReserch", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@SerchKey", SqlDbType.VarChar,1000).Value = DOobjResearch.SerchKey;
        //da.SelectCommand.Parameters.Add("@CommentId", SqlDbType.BigInt).Value = objComment.CommentId;
        //da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objComment.CaseId;
        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }
}
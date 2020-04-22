using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_DocumentMark
/// </summary>
public class DA_DocumentMark
{
    private SqlCommand cmd; 
	public DA_DocumentMark()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public enum DocumentMark
    {
        Add = 1, GetCaseMark = 2, DeleteMark = 3
    }

    public void AddEditDel_Case(DO_DocumentMark objMark, DocumentMark flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();

        cmd = new SqlCommand("Scrl_AddEditDelMarkDocument", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@MarkId", SqlDbType.BigInt).Value = objMark.Markid;
        cmd.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objMark.CaseId;
        cmd.Parameters.Add("@MarkContent", SqlDbType.VarChar, 5000).Value = objMark.Markcontent;
        cmd.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objMark.addedby;
        cmd.Parameters.Add("@StartIndex", SqlDbType.BigInt).Value = objMark.StartIndex;
        cmd.Parameters.Add("@EndIndex", SqlDbType.BigInt).Value = objMark.EndIndex;
        cmd.ExecuteNonQuery();

        co.CloseConnection(conn);
    }

    public DataTable GetDataTable(DO_DocumentMark objMark, DocumentMark flag)
    {
        DataTable dt = new DataTable();

        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelMarkDocument", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@MarkId", SqlDbType.BigInt).Value = objMark.Markid;
        da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objMark.CaseId;
        da.SelectCommand.Parameters.Add("@MarkContent", SqlDbType.VarChar, 5000).Value = objMark.Markcontent;
        da.SelectCommand.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objMark.addedby;
        da.SelectCommand.Parameters.Add("@ContentTypeId", SqlDbType.BigInt).Value = objMark.ContentTypeID;
        da.SelectCommand.Parameters.Add("@StartIndex", SqlDbType.BigInt).Value = objMark.StartIndex;
        da.SelectCommand.Parameters.Add("@EndIndex", SqlDbType.BigInt).Value = objMark.EndIndex;
        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }
}
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_Mark
/// </summary>
public class DA_Mark
{
    private SqlCommand cmd; 
	public DA_Mark()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public enum Mark
    {
        Add = 1, GetCaseMark = 2, DeleteMark = 3, AddTempSubCat = 4, GetDocumentSubCat = 5, GetCatNameByCatID = 6, ADDSelectedSubCatId = 7
    }

    public void AddEditDel_Case( DO_Mark objMark, Mark flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();

        cmd = new SqlCommand("Scrl_AddEditDelMark", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@MarkId", SqlDbType.BigInt).Value = objMark.Markid;
        cmd.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objMark.CaseId;
        cmd.Parameters.Add("@MarkContent", SqlDbType.VarChar, 5000).Value = objMark.Markcontent;
        cmd.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objMark.addedby;
        cmd.Parameters.Add("@StartIndex", SqlDbType.BigInt).Value = objMark.StartIndex;
        cmd.Parameters.Add("@EndIndex", SqlDbType.BigInt).Value = objMark.EndIndex;

        cmd.Parameters.Add("@DocId", SqlDbType.Int).Value = objMark.DocId;
        cmd.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objMark.intSubjectCategoryId;        
        cmd.Parameters.Add("@intModifiedBy", SqlDbType.VarChar, 200).Value = objMark.ModifiedBy;
        cmd.ExecuteNonQuery();

        co.CloseConnection(conn);
    }

    public DataTable GetDataTable(DO_Mark objMark, Mark flag)
    {
        DataTable dt = new DataTable();

        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelMark", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@MarkId", SqlDbType.BigInt).Value = objMark.Markid;
        da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objMark.CaseId;
        da.SelectCommand.Parameters.Add("@MarkContent", SqlDbType.VarChar, 5000).Value = objMark.Markcontent;
        da.SelectCommand.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objMark.addedby;
        da.SelectCommand.Parameters.Add("@ContentTypeId", SqlDbType.BigInt).Value = objMark.ContentTypeID;
        da.SelectCommand.Parameters.Add("@StartIndex", SqlDbType.BigInt).Value = objMark.StartIndex;
        da.SelectCommand.Parameters.Add("@EndIndex", SqlDbType.BigInt).Value = objMark.EndIndex;
        da.SelectCommand.Parameters.Add("@DocId", SqlDbType.Int).Value = objMark.DocId;
        da.SelectCommand.Parameters.Add("@intSubjectCategoryId", SqlDbType.BigInt).Value = objMark.intSubjectCategoryId;
        da.SelectCommand.Parameters.Add("@strImpParagraph", SqlDbType.VarChar, 5000).Value = objMark.strImpParagraph;
        da.SelectCommand.Parameters.Add("@intTagType", SqlDbType.Int).Value = objMark.intTagType;
        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }

}
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_DocumentComment
/// </summary>
public class DA_DocumentComment
{
    private SqlCommand cmd; 
	public DA_DocumentComment()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public enum DocumentComment
    {
        Add = 1, GetDocComment = 2, DeleteComment = 3, GetCommentTitle = 4, UpdateComment = 5
    }

    public void AddEditDel_Case(DO_DocumentComment objComment, DocumentComment flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();

        cmd = new SqlCommand("Scrl_AddEditDelDocComment", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;

        cmd.Parameters.Add("@CommentId", SqlDbType.BigInt).Value = objComment.CommentId;
        cmd.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objComment.CaseId;
        cmd.Parameters.Add("@Comment", SqlDbType.Text).Value = objComment.Comment;
        cmd.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objComment.addedby;
        cmd.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objComment.ContentTypeID;
        cmd.ExecuteNonQuery();

        co.CloseConnection(conn);
    }

    public DataTable GetDataTable(DO_DocumentComment objComment, DocumentComment flag)
    {
        DataTable dt = new DataTable();

        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelDocComment", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@CommentId", SqlDbType.BigInt).Value = objComment.CommentId;
        da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objComment.CaseId;
        da.SelectCommand.Parameters.Add("@Comment", SqlDbType.Text).Value = objComment.Comment;
        da.SelectCommand.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objComment.addedby;
        da.SelectCommand.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objComment.ContentTypeID;
        da.SelectCommand.Parameters.Add("@strCommentedText", SqlDbType.VarChar, 500).Value = objComment.CommentedText;
        da.SelectCommand.Parameters.Add("@ParentID", SqlDbType.VarChar, 500).Value = objComment.ParentID;
        //da.SelectCommand.Parameters.Add("@OldParentID", SqlDbType.VarChar, 500).Value = objComment.OldParentID;
        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }
}
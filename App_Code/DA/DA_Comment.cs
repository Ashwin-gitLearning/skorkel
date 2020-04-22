using System.Data;
using System.Data.SqlClient;
using SqlConn;
/// <summary>
/// Summary description for DA_Comment
/// </summary>
public class DA_Comment
{
    private SqlCommand cmd; 
	public DA_Comment()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public enum Comment
    {
        Add = 1, GetCaseComment = 2,DeleteComment=3,GetCommentTitle=4,UpdateComment=5
    }

    public void AddEditDel_Case(DO_Comment objComment, Comment flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();

        cmd = new SqlCommand("Scrl_AddEditDelComment", conn);
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

    public DataTable GetDataTable(DO_Comment objComment, Comment flag)
    {
        DataTable dt = new DataTable();

        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelComment", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@CommentId", SqlDbType.BigInt).Value = objComment.CommentId;
        da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objComment.CaseId;
        da.SelectCommand.Parameters.Add("@Comment", SqlDbType.Text).Value = objComment.Comment;
        da.SelectCommand.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objComment.addedby;
        da.SelectCommand.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objComment.ContentTypeID;
        da.SelectCommand.Parameters.Add("@strCommentedText", SqlDbType.VarChar,500).Value = objComment.CommentedText;
        da.SelectCommand.Parameters.Add("@ParentID", SqlDbType.VarChar, 500).Value = objComment.ParentID;
        da.SelectCommand.Parameters.Add("@intCommentAddedFor", SqlDbType.VarChar, 500).Value = objComment.intCommentAddedFor;
        da.SelectCommand.Parameters.Add("@strOrigenalText", SqlDbType.VarChar, 500).Value = objComment.strOrigenalText;
        //da.SelectCommand.Parameters.Add("@OldParentID", SqlDbType.VarChar, 500).Value = objComment.OldParentID;
        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }

}
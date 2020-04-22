using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_DocumentUploaderComment
/// </summary>

namespace DA_SKORKEL
{
    public class DA_DocumentUploaderComment
    {
        public DA_DocumentUploaderComment()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum DocumentUploaderComment
        {
            GetRecord = 1, Update = 2, GetComments = 3, GetChildComment = 4
        };

        SqlCommand cmd = new SqlCommand();

        public DataTable GetDataTable(DO_DocumentUploaderComment objcase, DA_DocumentUploaderComment.DocumentUploaderComment flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelDocumentUploaderComment", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objcase.CaseId;
            da.SelectCommand.Parameters.Add("@ContentTypeId", SqlDbType.BigInt).Value = objcase.ContentTypeID;
            da.SelectCommand.Parameters.Add("@CommentID", SqlDbType.Int).Value = objcase.CommnetId;


            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_Case(DO_DocumentUploaderComment objcase, DA_DocumentUploaderComment.DocumentUploaderComment flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelDocumentUploaderComment", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objcase.CaseId;
            cmd.Parameters.Add("@Description", SqlDbType.Text).Value = objcase.Description;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }

        public DataTable GetDataTableContentDetail(int prefixText)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_CommonFunctions", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 6;
            da.SelectCommand.Parameters.Add("@intuserid", SqlDbType.Int).Value = prefixText;


            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
    }
}
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_CaseInsert
/// </summary>
/// 
namespace DA_SKORKEL
{

    public class DA_CaseComment
    {
        public DA_CaseComment()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum CaseComment
        {
            GetRecord = 1, Update = 2, GetComments = 3, GetChildComment = 4, GetTotalComment = 5, GetDocsDetails = 6, AddCaseDocument = 7, GETLikeUser = 8, GetCommentsOrg = 9,
        };

        SqlCommand cmd = new SqlCommand();

        public DataTable GetDataTable(DO_Case objcase, DA_CaseComment.CaseComment flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelCaseComment", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objcase.CaseId;
            da.SelectCommand.Parameters.Add("@ContentTypeId", SqlDbType.BigInt).Value = objcase.ContentTypeID;
            da.SelectCommand.Parameters.Add("@CommentID", SqlDbType.Int).Value = objcase.CommnetId;   
              da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objcase.AddedBy;
              da.SelectCommand.Parameters.Add("@TagId", SqlDbType.Int).Value = objcase.TagId;
              da.SelectCommand.Parameters.Add("@CommnetAddedFor", SqlDbType.Int).Value = objcase.CommnetAddedFor;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_Case(DO_Case objcase, DA_CaseComment.CaseComment flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelCaseComment", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objcase.CaseId;
            cmd.Parameters.Add("@Description", SqlDbType.Text).Value = objcase.Description;
            cmd.Parameters.Add("@strCaseTitle", SqlDbType.VarChar,1000).Value = objcase.CaseTitle;
            cmd.Parameters.Add("@strFilePath", SqlDbType.VarChar,100).Value = objcase.strFilePath;
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
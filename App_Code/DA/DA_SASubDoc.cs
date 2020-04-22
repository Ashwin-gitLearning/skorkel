using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_CaseInsert
/// </summary>
/// 
namespace DA_SKORKEL
{

    public class DA_SASubDoc
    {

        public DA_SASubDoc()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum Case
        {
            GetArticles = 1,
            AddArticle = 2
        }

        SqlCommand cmd = new SqlCommand();

        public DataTable GetDataTable(DO_SASubDoc objDOSubDoc, Case flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_sp_SubDocs", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            //da.SelectCommand.Parameters.Add("@SubDocId", SqlDbType.Int).Value = objDOSubDoc.SubDocId;
            da.SelectCommand.Parameters.Add("@CurrentPage", SqlDbType.Int).Value = objDOSubDoc.CurrentPage;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = objDOSubDoc.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@TxtSearch", SqlDbType.VarChar).Value = objDOSubDoc.TxtSearch;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
        public void AddEditSubDocs(DO_SASubDoc objDOSubDoc, Case flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_sp_EditSubDocs", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@FilePath", SqlDbType.VarChar).Value = objDOSubDoc.FilePath;
            cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = objDOSubDoc.FileName;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objDOSubDoc.IntUserId;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
    }


}
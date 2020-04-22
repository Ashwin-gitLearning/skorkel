using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_CaseInsert
/// </summary>
/// 
namespace DA_SKORKEL
{

    public class DA_DocumentSummary
    {
        public DA_DocumentSummary()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum DocumentSummary
        {
            Add = 1, GetDocuementSummary = 2, Update = 3, Delete = 4, SingleRecord = 5
        };

        SqlCommand cmd = new SqlCommand();

        public void AddEditDel_Summary(DO_DocumentSummary objSummary, DA_DocumentSummary.DocumentSummary flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelDocumentSummary", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@SummaryId", SqlDbType.BigInt).Value = objSummary.SummaryId;
            cmd.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objSummary.ContentId;
            cmd.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objSummary.addedby;
            cmd.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objSummary.ContentTypeID;
            cmd.Parameters.Add("@SummaryText", SqlDbType.VarChar, 8000).Value = objSummary.SummaryText;
            cmd.ExecuteNonQuery();

            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_DocumentSummary objSummary, DA_DocumentSummary.DocumentSummary flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelDocumentSummary", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@SummaryId", SqlDbType.BigInt).Value = objSummary.SummaryId;
            da.SelectCommand.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objSummary.ContentId;
            da.SelectCommand.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objSummary.addedby;
            da.SelectCommand.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objSummary.ContentTypeID;
            da.SelectCommand.Parameters.Add("@SummaryText", SqlDbType.VarChar, 8000).Value = objSummary.SummaryText;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
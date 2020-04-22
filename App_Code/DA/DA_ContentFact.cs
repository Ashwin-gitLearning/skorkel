using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_CaseInsert
/// </summary>
/// 
namespace DA_SKORKEL
{

    public class DA_ContentFact
    {
        public DA_ContentFact()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum ContentFact
        {
            Add = 1, GetContentFact = 2, DeleteFact = 3
        };


        SqlCommand cmd = new SqlCommand();

        public void AddEditDel_Fact(DO_ContentFact objFact, DA_ContentFact.ContentFact flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelContentFact", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@FactId", SqlDbType.BigInt).Value = objFact.FactId;
            cmd.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objFact.ContentId;
            cmd.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objFact.addedby;
            cmd.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objFact.ContentTypeID;
            cmd.Parameters.Add("@FactText", SqlDbType.VarChar, 8000).Value = objFact.FactText;
            cmd.Parameters.Add("@StartIndex", SqlDbType.BigInt).Value = objFact.StartIndex;
            cmd.Parameters.Add("@EndIndex", SqlDbType.BigInt).Value = objFact.EndIndex;
            cmd.ExecuteNonQuery();

            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_ContentFact objFact, DA_ContentFact.ContentFact flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelContentFact", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@FactId", SqlDbType.BigInt).Value = objFact.FactId;
            da.SelectCommand.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objFact.ContentId;
            da.SelectCommand.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objFact.addedby;
            da.SelectCommand.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objFact.ContentTypeID;
            da.SelectCommand.Parameters.Add("@FactText", SqlDbType.VarChar, 8000).Value = objFact.FactText;
            da.SelectCommand.Parameters.Add("@StartIndex", SqlDbType.BigInt).Value = objFact.StartIndex;
            da.SelectCommand.Parameters.Add("@EndIndex", SqlDbType.BigInt).Value = objFact.EndIndex;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_CaseInsert
/// </summary>
/// 
namespace DA_SKORKEL
{

    public class DA_ContentRatio
    {
        public DA_ContentRatio()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum ContentRatio
        {
            Add = 1, GetContentRatio = 2, DeleteRatio = 3
        };


        SqlCommand cmd = new SqlCommand();

        public void AddEditDel_Ratio(DO_ContentRatio objRatio, DA_ContentRatio.ContentRatio flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelContentRatio", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@RatioId", SqlDbType.BigInt).Value = objRatio.RatioId;
            cmd.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objRatio.ContentId;
            cmd.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objRatio.addedby;
            cmd.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objRatio.ContentTypeID;
            cmd.Parameters.Add("@RatioText", SqlDbType.VarChar, 8000).Value = objRatio.RatioText;
            cmd.Parameters.Add("@StartIndex", SqlDbType.BigInt).Value = objRatio.StartIndex;
            cmd.Parameters.Add("@EndIndex", SqlDbType.BigInt).Value = objRatio.EndIndex;
            cmd.ExecuteNonQuery();

            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_ContentRatio objRatio, DA_ContentRatio.ContentRatio flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelContentRatio", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@RatioId", SqlDbType.BigInt).Value = objRatio.RatioId;
            da.SelectCommand.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objRatio.ContentId;
            da.SelectCommand.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objRatio.addedby;
            da.SelectCommand.Parameters.Add("@ContentTypeID", SqlDbType.BigInt).Value = objRatio.ContentTypeID;
            da.SelectCommand.Parameters.Add("@RatioText", SqlDbType.VarChar, 8000).Value = objRatio.RatioText;
            da.SelectCommand.Parameters.Add("@StartIndex", SqlDbType.BigInt).Value = objRatio.StartIndex;
            da.SelectCommand.Parameters.Add("@EndIndex", SqlDbType.BigInt).Value = objRatio.EndIndex;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
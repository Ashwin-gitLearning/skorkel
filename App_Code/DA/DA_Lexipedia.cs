using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_Lexipedia
/// </summary>

namespace DA_SKORKEL
{
    public class DA_Lexipedia
    {
        public DA_Lexipedia()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum LexiPedia
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, SingleRecord = 5,SearchRecord=6
        };

        SqlCommand cmd = new SqlCommand();
        public DataTable GetDataTable(DO_Lexipedia objdoLexiPedia, LexiPedia flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_AddEditDeleteLexiPedia_SP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@LexipediaId", SqlDbType.BigInt).Value = objdoLexiPedia.LexipediaId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objdoLexiPedia.AddedBy;
            da.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 500).Value = objdoLexiPedia.Condition;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }


        public void AddEditDel_Lexipedia(DO_Lexipedia objdoLexiPedia, LexiPedia flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("scrl_AddEditDeleteLexiPedia_SP", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@LexipediaId", SqlDbType.BigInt).Value = objdoLexiPedia.LexipediaId;
            cmd.Parameters.Add("@LexipediaTitle", SqlDbType.VarChar).Value = objdoLexiPedia.LexipediaTitle;
            cmd.Parameters.Add("@Subtitles", SqlDbType.VarChar).Value = objdoLexiPedia.Subtitles;
            cmd.Parameters.Add("@Authors", SqlDbType.VarChar).Value = objdoLexiPedia.Authors;
            cmd.Parameters.Add("@Reference", SqlDbType.VarChar).Value = objdoLexiPedia.Reference;
            cmd.Parameters.Add("@LexipadiaDate", SqlDbType.VarChar).Value = objdoLexiPedia.LexipadiaDate;
            //cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objdoLexiPedia.AddedBy;
            //cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = objdoLexiPedia.ModifiedBy;
            
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
            
        }


    }
}

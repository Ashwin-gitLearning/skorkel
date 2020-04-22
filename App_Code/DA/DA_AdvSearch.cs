using System.Data;
using System.Data.SqlClient;
using SqlConn;


/// <summary>
/// Summary description for DA_Advsearch
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DA_AdvSearch
    {
        public DA_AdvSearch()
        {
            //
            // TODO: Add constructor logic here
            //
        }

         public enum Search
        {
            Allrecord = 1, GetYears = 2,GetCourt=3
        };

        SqlCommand cmd = new SqlCommand();

        public DataTable GetDataTable(DO_AdvSearch  objAdvsearch, DA_AdvSearch.Search  flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_SkorkelAdvanceSearch", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@SummaryID", SqlDbType.BigInt).Value = objAdvsearch.SummaryID;
            da.SelectCommand.Parameters.Add("@MarkID", SqlDbType.BigInt).Value = objAdvsearch.MarkID;
            da.SelectCommand.Parameters.Add("@KeywordID", SqlDbType.BigInt).Value = objAdvsearch.KeywordID;
            da.SelectCommand.Parameters.Add("@CommentID", SqlDbType.BigInt).Value = objAdvsearch.CommentID;
            da.SelectCommand.Parameters.Add("@RatioID", SqlDbType.BigInt).Value = objAdvsearch.RatioID;
            da.SelectCommand.Parameters.Add("@FactID", SqlDbType.BigInt).Value = objAdvsearch.FactID;
            da.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 500).Value = objAdvsearch.Condition;
            da.SelectCommand.Parameters.Add("@ContentIds", SqlDbType.VarChar, 500).Value = objAdvsearch.ContentIDs;
            da.SelectCommand.Parameters.Add("@year", SqlDbType.BigInt).Value = objAdvsearch.Year;
            da.SelectCommand.Parameters.Add("@AllYear", SqlDbType.VarChar,1000).Value = objAdvsearch.YearPassed;
            da.SelectCommand.Parameters.Add("@Jurisdiction", SqlDbType.VarChar, 500).Value = objAdvsearch.Court;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = objAdvsearch.CurrentPage;
            da.SelectCommand.Parameters.Add("@pagesize", SqlDbType.Int).Value = objAdvsearch.PageSize;
            da.SelectCommand.Parameters.Add("@MacroTag", SqlDbType.Int).Value = objAdvsearch.MacroTag;
            da.SelectCommand.CommandTimeout = 0;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        } 


    }


}

 
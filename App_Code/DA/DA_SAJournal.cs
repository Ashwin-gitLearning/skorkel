using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_CaseInsert
/// </summary>
/// 
namespace DA_SKORKEL
{

    public class DA_SAJournal
    {

        public DA_SAJournal()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum Case
        {
            AllJournals = 1,
            FilteredJournals = 2,
            UpdateStatus = 3,
            AddJournal = 4,
            GetJournal = 5,
            GetArticles = 6,
            ArticleDetails = 7,
            AddArticle = 8,
            AllArticles = 9,
            PublishJournal = 10,
            JournalsOfArticles = 11,
            RemoveArt = 12,
            GetComments = 13,
            AddComment = 14,
            EditComment = 15,
            DeleteComment = 16,
            LikeUnlike = 17,
            CurrentJournal=18,
            EditJournal = 19,
            GetLikeUnlike = 20,
            GetArticlesUS = 21
        };

        SqlCommand cmd = new SqlCommand();

        public DataTable GetDataTable(DO_SAJournal objSAJournal, Case flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_sp_superjournallist", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@ActiveStatus", SqlDbType.Bit).Value = objSAJournal.ActiveStatus;
            da.SelectCommand.Parameters.Add("@ArticleId", SqlDbType.Int).Value = objSAJournal.ArticleID;
            da.SelectCommand.Parameters.Add("@JournalId", SqlDbType.Int).Value = objSAJournal.JournalID;
            da.SelectCommand.Parameters.Add("@CurrentPage", SqlDbType.Int).Value = objSAJournal.CurrentPage;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = objSAJournal.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@IntUserId", SqlDbType.Int).Value = objSAJournal.IntUserId;
            da.SelectCommand.Parameters.Add("@ArticleIdList", SqlDbType.VarChar, 1000).Value = objSAJournal.ArticleIdList;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
        public void AddEditJournals(DO_SAJournal objSAJournal, Case flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_sp_Editjournallist", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@ActiveStatus", SqlDbType.Bit).Value = objSAJournal.ActiveStatus;
            cmd.Parameters.Add("@ArticleId", SqlDbType.Int).Value = objSAJournal.ArticleID;
            cmd.Parameters.Add("@JournalId", SqlDbType.Int).Value = objSAJournal.JournalID;
            cmd.Parameters.Add("@Month", SqlDbType.Int).Value = objSAJournal.Month;
            cmd.Parameters.Add("@Year", SqlDbType.Int).Value = objSAJournal.Year;
            cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = objSAJournal.Title;
            cmd.Parameters.Add("@FilePath", SqlDbType.VarChar).Value = objSAJournal.FilePath;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objSAJournal.IntUserId;
            cmd.Parameters.Add("@CommentID", SqlDbType.Int).Value = objSAJournal.CommentId;
            cmd.Parameters.Add("@Comment", SqlDbType.VarChar).Value = objSAJournal.Comment;
            cmd.Parameters.Add("@ArticleWordCount", SqlDbType.Int).Value = objSAJournal.ArticleWordCount;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
    }


}
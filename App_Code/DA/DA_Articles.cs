using System.Data;
using System.Data.SqlClient;
using SqlConn;


/// <summary>
/// Summary description for DA_Articles
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DA_Articles
    {
        public DA_Articles()
        {
            //
            // TODO: Add constructor logic here
            //
        }

         public enum Articles
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5,SearchRecord=6
        };

        SqlCommand cmd = new SqlCommand();

        public DataTable GetDataTable(DO_Articles  objarticles, DA_Articles.Articles  flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_AddEditDeleteArticles_SP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@ArticleId", SqlDbType.BigInt).Value = objarticles.ArticleId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objarticles.AddedBy;
            da.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 500).Value = objarticles.Condition;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_Article(DO_Articles objarticles, DA_Articles.Articles  flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("scrl_AddEditDeleteArticles_SP", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@ArticleId", SqlDbType.BigInt).Value = objarticles.ArticleId;
            cmd.Parameters.Add("@ArticleTitle", SqlDbType.VarChar, 200).Value = objarticles.ArticleTitle;
            cmd.Parameters.Add("@Citation", SqlDbType.VarChar,200).Value = objarticles.Citation;
            cmd.Parameters.Add("@EnactmentCites", SqlDbType.VarChar,200).Value = objarticles.EnactmentCites;
            cmd.Parameters.Add("@Jurisdiction", SqlDbType.VarChar, 200).Value = objarticles.Jurisdiction;
            cmd.Parameters.Add("@ProvisionofLaw", SqlDbType.VarChar,200).Value = objarticles.ProvisionofLaw;
            cmd.Parameters.Add("@Cites", SqlDbType.VarChar, 200).Value = objarticles.Cites;
            cmd.Parameters.Add("@CitedBy", SqlDbType.VarChar,200).Value = objarticles.CitedBy;
            cmd.Parameters.Add("@Author", SqlDbType.VarChar, 200).Value = objarticles.Author;
            cmd.Parameters.Add("@ContentSource", SqlDbType.VarChar, 200).Value = objarticles.ContentSource;
            
            
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }





    }


}

 
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_City
/// </summary>
/// 
namespace DA_SKORKEL
{



    public class DA_Blogs
    {

        public DA_Blogs()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum Blogs
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5,SearchRecord=6
        };

        SqlCommand cmd = new SqlCommand();

        public void AddEditDel_Blogs(DO_Blogs objBlogs, Blogs flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelBlogs", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@BlogId", SqlDbType.BigInt).Value = objBlogs.BlogId;
            cmd.Parameters.Add("@BlogTitle", SqlDbType.VarChar, 1000).Value = objBlogs.BlogTitle;
            cmd.Parameters.Add("@WhoWrote", SqlDbType.VarChar, 1000).Value = objBlogs.WhoWrote;
            cmd.Parameters.Add("@SubjectMatter", SqlDbType.VarChar, 1000).Value = objBlogs.SubjectMatter;
            cmd.Parameters.Add("@BlogDate", SqlDbType.DateTime).Value = objBlogs.BlogDate;
            cmd.Parameters.Add("@NoOfFollowers", SqlDbType.Int).Value = objBlogs.NoOfFollowers;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objBlogs.AddedBy;
            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 50).Value = objBlogs.IPAddress;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }

        public DataTable GetDataTableBlogs(DO_Blogs objBlogs, Blogs flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelBlogs", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@BlogId", SqlDbType.BigInt).Value = objBlogs.BlogId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objBlogs.AddedBy;
            da.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 500).Value = objBlogs.Condition;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
    }
}
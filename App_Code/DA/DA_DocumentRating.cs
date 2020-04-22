using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_DocumentRating
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DA_DocumentRating
    {
        public DA_DocumentRating()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum DocumentRating
        {
            Add = 1, GetContentRating = 2, DeleteRating = 3
        };


        SqlCommand cmd = new SqlCommand();

        public void AddEditDel_Rating(DO_DocumentRating objRate, DA_DocumentRating.DocumentRating flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelLikeDisLikeRatingDocument", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@RatingId", SqlDbType.BigInt).Value = objRate.RatingId;
            cmd.Parameters.Add("@Rating", SqlDbType.BigInt).Value = objRate.Rating;
            cmd.Parameters.Add("@TagId", SqlDbType.BigInt).Value = objRate.TagId;
            cmd.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objRate.ContentId;
            cmd.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objRate.addedby;
            cmd.Parameters.Add("@ContentTypeId", SqlDbType.BigInt).Value = objRate.ContentTypeID;
            cmd.Parameters.Add("@TagType", SqlDbType.VarChar, 5).Value = objRate.TagType;

            cmd.ExecuteNonQuery();
            //@Rating,@,@,@ContentId,@ContentTypeId,@addedby
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_DocumentRating objRate, DA_DocumentRating.DocumentRating flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelLikeDisLikeRatingDocument", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@RatingId", SqlDbType.BigInt).Value = objRate.RatingId;
            da.SelectCommand.Parameters.Add("@Rating", SqlDbType.BigInt).Value = objRate.Rating;
            da.SelectCommand.Parameters.Add("@TagId", SqlDbType.BigInt).Value = objRate.TagId;
            da.SelectCommand.Parameters.Add("@ContentId", SqlDbType.BigInt).Value = objRate.ContentId;
            da.SelectCommand.Parameters.Add("@addedby", SqlDbType.BigInt).Value = objRate.addedby;
            da.SelectCommand.Parameters.Add("@ContentTypeId", SqlDbType.BigInt).Value = objRate.ContentTypeID;
            da.SelectCommand.Parameters.Add("@TagType", SqlDbType.VarChar, 5).Value = objRate.TagType;


            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
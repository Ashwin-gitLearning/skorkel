using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_ParliamentDebates
/// </summary>


namespace DA_SKORKEL
{
    public class DA_Testimonies
    {
        public DA_Testimonies()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public enum Testimonies
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5, SearchRecord = 6
        };


        SqlCommand cmd = new SqlCommand();

        public void AddEditDel_Testimonies(DO_Testimonies objTestimonies, Testimonies flag)
        {
            //@IPAddress @ModifiedBy @AddedBy @SubjectMatter @Summary @PublishingDate @TestimonialTitle @TestimonialId @FlagNo
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelTestimonies", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@TestimonialId", SqlDbType.BigInt).Value = objTestimonies.TestimonialId;
            cmd.Parameters.Add("@TestimonialTitle", SqlDbType.VarChar, 1000).Value = objTestimonies.TestimonialTitle;
            cmd.Parameters.Add("@PublishingDate", SqlDbType.DateTime).Value = objTestimonies.PublishingDate;
            cmd.Parameters.Add("@SubjectMatter", SqlDbType.VarChar, 1000).Value = objTestimonies.SubjectMatter;
            cmd.Parameters.Add("@Summary", SqlDbType.VarChar, 2000).Value = objTestimonies.Summary;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objTestimonies.AddedBy;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.BigInt).Value = objTestimonies.ModifiedBy;
            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 50).Value = objTestimonies.IPAddress;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }



        public DataTable GetDatatableTestimonies(DO_Testimonies objTestimonies, Testimonies flag)
        {  
            //For Retriving Record For Testimonies

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelTestimonies", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@TestimonialId", SqlDbType.BigInt).Value = objTestimonies.TestimonialId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objTestimonies.AddedBy;
            da.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 500).Value = objTestimonies.Condition;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
    }
}
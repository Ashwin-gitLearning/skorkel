using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_City
/// </summary>
/// 
namespace DA_SKORKEL
{



    public class DA_Questions
    {

        public DA_Questions()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum Questions
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5,SearchRecord=6
        };

        SqlCommand cmd = new SqlCommand();

        public void AddEditDel_Questions(DO_Questions objQuestions, Questions flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            //@,@,@,@,@,@,@,@
            cmd = new SqlCommand("Scrl_AddEditDelQuestions", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@QuestionId", SqlDbType.BigInt).Value = objQuestions.QuestionId;
            cmd.Parameters.Add("@Question", SqlDbType.VarChar,1000).Value = objQuestions.Question;
            cmd.Parameters.Add("@WhoWrote", SqlDbType.VarChar,1000).Value = objQuestions.WhoWrote;
            cmd.Parameters.Add("@WhoAnswered", SqlDbType.VarChar,1000).Value = objQuestions.WhoAnswered;
            cmd.Parameters.Add("@Answers", SqlDbType.Text).Value = objQuestions.Answers;
            cmd.Parameters.Add("@PublishingDate", SqlDbType.DateTime).Value = objQuestions.PublishingDate;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objQuestions.AddedBy;
            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar,50).Value = objQuestions.IPAddress;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }

        public DataTable GetDataTableQuestions(DO_Questions objQuestions, Questions flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelQuestions", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@QuestionId", SqlDbType.BigInt).Value = objQuestions.QuestionId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objQuestions.AddedBy;
            da.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 500).Value = objQuestions.Condition;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
    }
}
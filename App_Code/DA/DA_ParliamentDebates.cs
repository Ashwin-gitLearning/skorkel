using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_ParliamentDebates
/// </summary>


namespace DA_SKORKEL
{
    public class DA_ParliamentDebates
    {
        public DA_ParliamentDebates()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum Parliament
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5, SearchRecord = 6
        };


        SqlCommand cmd = new SqlCommand();



        public void AddEditDel_Parliments(DO_ParliamentDebates objParliments, Parliament flag)
        {           
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();


            cmd = new SqlCommand("Scrl_AddEditDelParliamentDebates", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@DebateId", SqlDbType.BigInt).Value = objParliments.DebateId;
            cmd.Parameters.Add("@DebateDate", SqlDbType.DateTime).Value = objParliments.DebateDate;
            cmd.Parameters.Add("@Session", SqlDbType.VarChar, 500).Value = objParliments.Session;
            cmd.Parameters.Add("@Member", SqlDbType.VarChar, 500).Value = objParliments.Member;
            cmd.Parameters.Add("@DebateType", SqlDbType.Int).Value = objParliments.DebateType;
            cmd.Parameters.Add("@Subject", SqlDbType.Text).Value = objParliments.Subject;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objParliments.AddedBy;
            cmd.Parameters.Add("@Modifiedby", SqlDbType.BigInt).Value = objParliments.Modifiedby;            
            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 50).Value = objParliments.IPAddress;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }


        public DataTable GetDataTableParliament(DO_ParliamentDebates objParliments, Parliament flag)
        {
            //For Retriving Record For Parliament Debate

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelParliamentDebates", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@DebateId", SqlDbType.BigInt).Value = objParliments.DebateId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objParliments.AddedBy;
            da.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 500).Value = objParliments.Condition;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
    }
}
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_ParliamentDebates
/// </summary>


namespace DA_SKORKEL
{
    public class DA_Status
    {
        public DA_Status()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public enum Status
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5,SearchRecord=6
        };


        SqlCommand cmd = new SqlCommand();

        public void AddEditDel_Status(DO_Status objStatus, Status flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelStatus", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@StatusId", SqlDbType.BigInt).Value = objStatus.StatusId;
            cmd.Parameters.Add("@Year ", SqlDbType.Int).Value = objStatus.Year;
            cmd.Parameters.Add("@Title", SqlDbType.VarChar, 1000).Value = objStatus.Title;
            cmd.Parameters.Add("@Ammendments", SqlDbType.Int).Value = objStatus.Ammendments;
            cmd.Parameters.Add("@SubjectMatter", SqlDbType.VarChar, 1000).Value = objStatus.SubjectMatter;
            cmd.Parameters.Add("@ObjectofAct", SqlDbType.VarChar, 1000).Value = objStatus.ObjectofAct;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objStatus.AddedBy;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.BigInt).Value = objStatus.ModifiedBy;
            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 50).Value = objStatus.IPAddress;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }


        public DataTable GetDatatableStatus(DO_Status objStatus, Status flag)
        {
            //For Retriving Record For Status

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelStatus", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@StatusId", SqlDbType.BigInt).Value = objStatus.StatusId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objStatus.AddedBy;
            da.SelectCommand.Parameters.Add("@Condition", SqlDbType.VarChar, 500).Value = objStatus.Condition;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
    }
}
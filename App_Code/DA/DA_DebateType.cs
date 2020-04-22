using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_DebatreType
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DA_DebateType
    {
        public DA_DebateType()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        SqlCommand cmd = new SqlCommand();

        public enum DebateType
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5
        };



        public void AddEditDel_DebateType(DO_DebateType objDebateType, DebateType flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();


            cmd = new SqlCommand("Scrl_AddEditDel_DebateType", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@DebateTypeId", SqlDbType.BigInt).Value = objDebateType.DebateTypeID;
            cmd.Parameters.Add("@DebateType", SqlDbType.VarChar, 200).Value = objDebateType.ParliamentDebateType;
           cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objDebateType.AddedBy;
           cmd.Parameters.Add("@ModifiedBy", SqlDbType.BigInt).Value = objDebateType.ModifiedBy;
           cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 50).Value = objDebateType.IPAddress;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }


        public DataTable GetDataTableDebate(DO_DebateType objDebateType, DebateType flag)
        {
            //For Retriving Record For DebateType

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDel_DebateType", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@DebateTypeId", SqlDbType.BigInt).Value = objDebateType.DebateTypeID;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objDebateType.AddedBy;
                      
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
    }
}
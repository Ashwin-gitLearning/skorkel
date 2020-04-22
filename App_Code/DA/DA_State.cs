using System.Data;
using System.Data.SqlClient;
using SqlConn;


/// <summary>
/// Summary description for DA_State
/// </summary>
/// 
namespace DA_SKORKEL
{
    public enum State
    {
        Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5, CountryWise = 6, GetStateID = 7
    };

    public class DA_State
    {

        private SqlCommand cmd;
        public DA_State()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public DataTable GetDataTable(DO_State objstate,State flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_StateAddEditDel_SP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@StateId", SqlDbType.BigInt).Value = objstate.StateId;
            da.SelectCommand.Parameters.Add("@StateName", SqlDbType.NVarChar).Value = objstate.StateName;
            da.SelectCommand.Parameters.Add("@intCountryId", SqlDbType.BigInt).Value = objstate.CountryID;
            
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_State(DO_State objstate, State flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("scrl_StateAddEditDel_SP", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@StateId", SqlDbType.BigInt).Value = objstate.StateId;
            cmd.Parameters.Add("@StateName", SqlDbType.VarChar).Value = objstate.StateName;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objstate.AddedBy;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = objstate.ModifiedBy;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }







    }
}
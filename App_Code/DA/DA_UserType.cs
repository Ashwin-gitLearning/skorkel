using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_UserType
/// </summary>
/// 
namespace DA_SKORKEL
{
    public enum UserType
    {
        InternalUsers = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5
    };
    public class DA_UserType
    {
        public DA_UserType()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        SqlCommand cmd = new SqlCommand();

        public DataTable GetDataTable(DO_UserType objusertype, UserType flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_UserTypeAddEditDel_SP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@UserTypeId", SqlDbType.BigInt).Value = objusertype.UserTypeId;
            da.SelectCommand.Parameters.Add("@UserType", SqlDbType.NVarChar).Value = objusertype.UserType;
           
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_State(DO_UserType objUserType, UserType flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("scrl_UserTypeAddEditDel_SP", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@UserTypeId", SqlDbType.BigInt).Value = objUserType.UserTypeId;
            cmd.Parameters.Add("@UserType", SqlDbType.VarChar).Value = objUserType.UserType;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }



    }
}
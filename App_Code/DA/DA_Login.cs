using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_Login
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DA_Login
    {
        private SqlCommand cmd;
        //private SqlCommand cmd;
        public enum Login_1
        {
            UserLogin = 1, SelectForUser = 2, InternalUser = 3, UserDetails = 4, ForgotPassword = 6, GmailFBLogin = 7, InActiveUser = 8,Login=9,Logout=10,GetLoginLogout=11,ChangePassword=12,
            UserLoginMD5 = 13, GetMD5=14, GetUserMailCheck=15
        };

        public DA_Login()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetDataSet(DO_Login objLogin, Login_1 flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("scrl_LoginUserSP", conn);
            da.SelectCommand.CommandType  = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@UserName", SqlDbType.VarChar).Value = objLogin.Username;
            da.SelectCommand.Parameters.Add("@Password", SqlDbType.VarChar).Value = objLogin.Password;
            da.SelectCommand.Parameters.Add("@RegistrationId", SqlDbType.BigInt).Value = objLogin.intRegistartionID;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }


        public DataTable GetDataTable(DO_Login objLogin, Login_1 flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("scrl_LoginUserSP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@RegistrationId", SqlDbType.Int).Value = objLogin.intRegistartionID;      

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_RegistrationDetails(DO_Registrationdetails objRegistration, Login_1 flag)
        {
         SqlConnection conn = new SqlConnection();
         SQLManager co = new SQLManager();
       
            conn = co.GetConnection();
            //conn.Open();
            cmd = new SqlCommand("scrl_LoginUserSP", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@RegistrationId", SqlDbType.BigInt).Value = objRegistration.RegistrationId;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 200).Value = objRegistration.Password;     
   
              cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
       
        }



        public void AddAndGetLoginDetails(DO_Login objLogin, Login_1 flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            //conn.Open();
            cmd = new SqlCommand("scrl_LoginUserSP", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@RegistrationId", SqlDbType.BigInt).Value = objLogin.intRegistartionID;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
    }
}
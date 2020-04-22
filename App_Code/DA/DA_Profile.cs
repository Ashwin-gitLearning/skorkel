using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


/// <summary>
/// Summary description for DA_Profile
/// </summary>
namespace DA_SKORKEL
{
    public class DA_Profile
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        public enum Myprofile
        {
            GetAllProfileUSerDetails = 1, GetConnCount = 2, GetEndorseCount = 3, GetMessageCount = 4, GetFollowCount = 5, GetFollowCountForSameLogin = 6,GetOrgStatusByRegId=7
        }
        public DA_Profile()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable GetMyProfileDetails(DO_Profile objProfile, Myprofile flag)
        {
            DataTable dt = new DataTable();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_GetProfileDetailsByregID", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objProfile.RegistrationId;
            da.SelectCommand.Parameters.Add("@ConnectRegistrationId", SqlDbType.Int).Value = objProfile.ConnectRegistrationId;
            da.SelectCommand.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = objProfile.intInvitedUserId;
            da.SelectCommand.Parameters.Add("@striInvitedUserId", SqlDbType.VarChar,100).Value = objProfile.striInvitedUserId;
           // da.SelectCommand.Parameters.Add("@striInvitedUserId", SqlDbType.VarChar,100).Value = objProfile.striInvitedUserId;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
        public DataSet GetMyOrgProfileDetails(DO_Profile objProfile, Myprofile flag)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_GetProfileDetailsByregID", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objProfile.RegistrationId;
            da.SelectCommand.Parameters.Add("@ConnectRegistrationId", SqlDbType.Int).Value = objProfile.ConnectRegistrationId;
            da.SelectCommand.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = objProfile.intInvitedUserId;
            da.SelectCommand.Parameters.Add("@intInstituteUserId", SqlDbType.Int).Value = objProfile.intInstituteUserId;
            da.Fill(ds);
            co.CloseConnection(conn);
            return ds;
        }
    }
}
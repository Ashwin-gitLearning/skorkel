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
/// Summary description for DA_Networks
/// </summary>
namespace DA_SKORKEL
{
    public class DA_Networks
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        public enum NetworkDetails
        {
            Add = 1, Update = 2, Delete = 3, AllRec = 4, SingleRecord = 5, ConnectedUsers = 6, GetTopNotifications = 7, GetNotificationDate = 8, GetAllNotificationByDate = 9, GetgroupShareInvitee = 10, GetTopFriendList = 11, GetTopNotificationsOrg=12, GetSANotifications=14, GetNotificationCount=15,
            GetAllFriendList = 16
        };

        public DA_Networks()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataTable GetUserSearch(int CurrentPage, int CurrentPageSize, int RegId, NetworkDetails flag)
        {
            DataTable dt = new DataTable();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelMyNetworks", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = RegId;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = CurrentPage;
            da.SelectCommand.Parameters.Add("@pagesize", SqlDbType.Int).Value = CurrentPageSize; 
           

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }


        public DataTable GetNotificationCount(int CurrentPage, int CurrentPageSize, int RegId, NetworkDetails flag, DateTime NotificationDateTime)
        {
            DataTable dt = new DataTable();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelMyNetworks", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = RegId;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = CurrentPage;
            da.SelectCommand.Parameters.Add("@pagesize", SqlDbType.Int).Value = CurrentPageSize;
            da.SelectCommand.Parameters.Add("@NotificationDate", SqlDbType.DateTime).Value = NotificationDateTime;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }



        public DataTable GetSANotifications(int CurrentPage, int CurrentPageSize, NetworkDetails flag)
        {
            DataTable dt = new DataTable();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelMyNetworks", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = CurrentPage;
            da.SelectCommand.Parameters.Add("@pagesize", SqlDbType.Int).Value = CurrentPageSize;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }



        public DataTable GetUserConnections(DO_Networks objNetwork, NetworkDetails flag)
        {
            DataTable dt = new DataTable();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelMyNetworks", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objNetwork.RegistrationId;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public DataTable GetDatatable(DO_Networks objNetwork, NetworkDetails flag)
        {
            DataTable dt = new DataTable();
            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelMyNetworks", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objNetwork.RegistrationId;           
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = objNetwork.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = objNetwork.CurrentPage;
            da.SelectCommand.Parameters.Add("@NotificationDate", SqlDbType.VarChar,20).Value = objNetwork.NotificationDate;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }


    }
}
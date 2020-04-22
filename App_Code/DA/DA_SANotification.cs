using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_CaseInsert
/// </summary>
/// 
namespace DA_SKORKEL
{

    public class DA_SANotification
    {

        public DA_SANotification()
        {
            //
            // TODO: Add constructor logic here
            //
        }

       

        public enum Case
        {
            AddNotification = 1,
            AllNotifications =2,
            DeleteNotification=3

        };

        

        SqlCommand cmd = new SqlCommand();

        public DataTable GetDataTable(DO_SANotification objSANotification, Case flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddSANotification", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@strNotificationDetail", SqlDbType.VarChar).Value = objSANotification.NotificationDetail;
            da.SelectCommand.Parameters.Add("@CurrentPage", SqlDbType.Int).Value = objSANotification.CurrentPage;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = objSANotification.CurrentPageSize;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }



        public void AddNotifications(DO_SANotification objSANotification, Case flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddSANotification", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@strNotificationDetail", SqlDbType.VarChar).Value = objSANotification.NotificationDetail;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }


        public void DeleteNotifications(DO_SANotification objSANotification, Case flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddSANotification", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@Notification_ID", SqlDbType.VarChar).Value = objSANotification.intNotificationId;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
    }


}
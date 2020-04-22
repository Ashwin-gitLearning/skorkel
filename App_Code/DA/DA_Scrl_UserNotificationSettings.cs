using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;



namespace DA_SKORKEL
{
    public class DA_Scrl_UserNotificationSettings
    {
        private SqlCommand cmd;
        public enum Scrl_UserNotificationSettings
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetPostUpdates = 6
        };
        public DA_Scrl_UserNotificationSettings()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Scrl_AddEditDelNotificationSettings(DO_Scrl_UserNotificationSettings ObjScrl_UserNotificationSettings, Scrl_UserNotificationSettings Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelNotificationSettings", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intNotificationId", SqlDbType.Int).Value = ObjScrl_UserNotificationSettings.intNotificationId;
            //cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserNotificationSettings.intRegistrationId;           
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserNotificationSettings.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserNotificationSettings.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserNotificationSettings ObjScrl_UserNotificationSettings, Scrl_UserNotificationSettings Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("Scrl_AddEditDelNotificationSettings", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intNotificationId", SqlDbType.Int).Value = ObjScrl_UserNotificationSettings.intNotificationId;
            //cmd.Parameters.Add("@intNotificationId", SqlDbType.Int).Value = ObjScrl_UserNotificationSettings.intNotificationId;
            //da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserNotificationSettings.intRegistrationId;            
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserNotificationSettings.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserNotificationSettings.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
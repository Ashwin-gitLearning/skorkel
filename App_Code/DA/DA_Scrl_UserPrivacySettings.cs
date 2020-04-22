using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;



namespace DA_SKORKEL
{
    public class DA_Scrl_UserPrivacySettings
    {
        private SqlCommand cmd;
        public enum Scrl_UsePrivacySettings
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetPostUpdates = 6, AddCustom = 7, GetCustom = 8, DeleteCustomUser = 9, DeleteType = 10
        };

        public DA_Scrl_UserPrivacySettings()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Scrl_AddEditDelPrivacySettings(DO_Scrl_UserPrivacySettings ObjScrl_UserPrivacySettings, Scrl_UsePrivacySettings Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelPrivacySettings", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intPrivacyId", SqlDbType.Int).Value = ObjScrl_UserPrivacySettings.intPrivacyId;
            cmd.Parameters.Add("@intSubPrivacyId", SqlDbType.Int).Value = ObjScrl_UserPrivacySettings.intSubPrivacyId;
            cmd.Parameters.Add("@intCustomRegId", SqlDbType.Int).Value = ObjScrl_UserPrivacySettings.intCustomRegId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserPrivacySettings.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserPrivacySettings.strIpAddress;
            cmd.Parameters.Add("@txtSearchText", SqlDbType.VarChar, 50).Value = ObjScrl_UserPrivacySettings.txtSearchText;
            cmd.Parameters.Add("@inCustomId", SqlDbType.Int).Value = ObjScrl_UserPrivacySettings.intCustomId;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserPrivacySettings ObjScrl_UserPrivacySettings, Scrl_UsePrivacySettings Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("Scrl_AddEditDelPrivacySettings", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intPrivacyId", SqlDbType.Int).Value = ObjScrl_UserPrivacySettings.intPrivacyId;
            da.SelectCommand.Parameters.Add("@intSubPrivacyId", SqlDbType.Int).Value = ObjScrl_UserPrivacySettings.intSubPrivacyId;
            da.SelectCommand.Parameters.Add("@intCustomRegId", SqlDbType.Int).Value = ObjScrl_UserPrivacySettings.intCustomRegId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserPrivacySettings.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserPrivacySettings.strIpAddress;
            da.SelectCommand.Parameters.Add("@txtSearchText", SqlDbType.VarChar, 50).Value = ObjScrl_UserPrivacySettings.txtSearchText;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
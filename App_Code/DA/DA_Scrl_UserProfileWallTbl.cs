using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{
    public class DA_Scrl_UserProfileWallTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserProfileWallTbl
        {
            Add = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, AllRecommendations = 6, UpdateAskRecommendation = 7
        };

        public DA_Scrl_UserProfileWallTbl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Scrl_AddEditDelUserProfileWall(DO_Scrl_UserProfileWallTbl objDA, Scrl_UserProfileWallTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelUserProfileWall", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = objDA.intInvitedUserId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objDA.intRegistrationId;
            cmd.Parameters.Add("@intProfilePostId", SqlDbType.Int).Value = objDA.intProfilePostId;
            cmd.Parameters.Add("@StrPost", SqlDbType.VarChar, 200).Value = objDA.StrPost;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objDA.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = objDA.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserProfileWallTbl objDA, Scrl_UserProfileWallTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("Scrl_AddEditDelUserProfileWall", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = objDA.intInvitedUserId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objDA.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intProfilePostId", SqlDbType.Int).Value = objDA.intProfilePostId;
            da.SelectCommand.Parameters.Add("@StrPost", SqlDbType.VarChar, 1000).Value = objDA.StrPost;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objDA.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = objDA.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
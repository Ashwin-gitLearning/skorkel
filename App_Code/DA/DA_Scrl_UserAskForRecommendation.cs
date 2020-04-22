using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{
    public class DA_Scrl_UserAskForRecommendation
    {
        private SqlCommand cmd;
        public enum Scrl_UserAskForRecommendation
        {
            Add = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
        };

        public DA_Scrl_UserAskForRecommendation()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Scrl_AddEditDelAskForRecommendations(DO_Scrl_UserAskForRecommendation ObjScrl_UserHonorsTbl, Scrl_UserAskForRecommendation Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelAskForRecommendations", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intInvitedUserId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRegistrationId;
            cmd.Parameters.Add("@StrRecommendation", SqlDbType.VarChar, 200).Value = ObjScrl_UserHonorsTbl.StrRecommendation;
            cmd.Parameters.Add("@intRecommendationChildId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRecommendationChildId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intAddedBy;            
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserHonorsTbl.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserAskForRecommendation ObjScrl_UserHonorsTbl, Scrl_UserAskForRecommendation Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("Scrl_AddEditDelAskForRecommendations", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intInvitedUserId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@StrRecommendation", SqlDbType.VarChar, 1000).Value = ObjScrl_UserHonorsTbl.StrRecommendation;
            da.SelectCommand.Parameters.Add("@intRecommendationChildId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRecommendationChildId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserHonorsTbl.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
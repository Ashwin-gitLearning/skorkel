using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{
    public class DA_Scrl_UserRecommendation
    {
        private SqlCommand cmd;
        public enum Scrl_UserRecommendation
        {
            Add = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, AllRecommendations = 6, UpdateAskRecommendation = 7, AddTempSubCat = 8, GetDocumentSubCat = 9, RemoveTempSubCat = 10, DeleteContext = 11,
            GetMyEndorse = 12, AddChildRecomendation = 13, GetInboxDetails = 14, GetTopinbox = 15, GetTotalOutBox = 16, GetOutBoxDetails = 17, GetMessageDetaisByMessId = 18, UpdateIsRead = 19, GetMessNotification = 20, GetOutBoxCount=21,
            UpdateIsReadSent = 22, GetSentMessageDetaisByMessId=23
        };
        public enum Scrl_OrgMessage
        {
            Add = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, AllRecommendations = 6, UpdateAskRecommendation = 7, InsertEnodorseMessage = 8, UpdateEnodorsement=9,GetEndorseMess=10
        };
      
        public DA_Scrl_UserRecommendation()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Scrl_AddEditDelRecommendations(DO_Scrl_UserRecommendation ObjScrl_UserHonorsTbl, Scrl_UserRecommendation Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelRecommendations", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intInvitedUserId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRegistrationId;
            cmd.Parameters.Add("@intRecommendationId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRecommendationId;
            cmd.Parameters.Add("@StrRecommendation", SqlDbType.VarChar, 200).Value = ObjScrl_UserHonorsTbl.StrRecommendation;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intGroupId;
            cmd.Parameters.Add("@strSubject", SqlDbType.VarChar, 200).Value = ObjScrl_UserHonorsTbl.strSubject;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intAddedBy;                        
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserHonorsTbl.strIpAddress;
            cmd.Parameters.Add("@intSkillId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intSkillId;
            cmd.Parameters.Add("@intMessageId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intMessageId;
            cmd.Parameters.Add("@striInvitedUserId", SqlDbType.VarChar,100).Value = ObjScrl_UserHonorsTbl.striInvitedUserId;
        

            ObjScrl_UserHonorsTbl.intOutRecommendationId = Convert.ToInt32(cmd.ExecuteScalar());            
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserRecommendation ObjScrl_UserHonorsTbl, Scrl_UserRecommendation Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("Scrl_AddEditDelRecommendations", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intInvitedUserId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intRecommendationId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRecommendationId;
            da.SelectCommand.Parameters.Add("@StrRecommendation", SqlDbType.VarChar, 1000).Value = ObjScrl_UserHonorsTbl.StrRecommendation;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserHonorsTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@intSkillId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intSkillId;
            da.SelectCommand.Parameters.Add("@intMessageId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intMessageId;
            da.SelectCommand.Parameters.Add("@striInvitedUserId", SqlDbType.VarChar,100).Value = ObjScrl_UserHonorsTbl.striInvitedUserId;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.CurrentPage;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void Scrl_AddEditDelOrganization(DO_Scrl_UserRecommendation ObjScrl_UserHonorsTbl, Scrl_OrgMessage Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelSendMessage", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intInvitedUserId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRegistrationId;
            cmd.Parameters.Add("@intRecommendationId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRecommendationId;
            cmd.Parameters.Add("@StrRecommendation", SqlDbType.VarChar, 200).Value = ObjScrl_UserHonorsTbl.StrRecommendation;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intGroupId;
            cmd.Parameters.Add("@strSubject", SqlDbType.VarChar, 200).Value = ObjScrl_UserHonorsTbl.strSubject;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserHonorsTbl.strIpAddress;
            cmd.Parameters.Add("@OrgId", SqlDbType.VarChar, 50).Value = ObjScrl_UserHonorsTbl.OrgId;
            cmd.Parameters.Add("@StrEndorseMess", SqlDbType.VarChar, 200).Value = ObjScrl_UserHonorsTbl.StrEndorseMess;
            cmd.Parameters.Add("@intOrgEndorseId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intOrgEndorseId;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetOrgDataTable(DO_Scrl_UserRecommendation ObjScrl_UserHonorsTbl, Scrl_OrgMessage Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("Scrl_AddEditDelSendMessage", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intInvitedUserId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intRecommendationId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRecommendationId;
            da.SelectCommand.Parameters.Add("@StrRecommendation", SqlDbType.VarChar, 1000).Value = ObjScrl_UserHonorsTbl.StrRecommendation;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserHonorsTbl.strIpAddress;
          

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

       
    }
}
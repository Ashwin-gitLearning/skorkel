using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_UserGroupJoin
    {
        private SqlCommand cmd;
        public enum Scrl_UserGroupJoin
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GroupMembers = 6, 
            UpdateJoiningStatus = 7, GetPopupGroupMemberDtls = 8, GetTotalGrpJoinId = 9, SearchGroupMembers = 11,
            GroupMembersCount = 12, GetConnected = 13, UpdateGroupMember = 14, DeleteGroupMember = 15, UpdateGroupMembers = 16,
            UpdateGroupMembersReq = 17,GetDataFrom=18, GetSingleGroupRecord = 19

        };

        public enum Scrl_OrgnisationGroupJoin
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GroupMembers = 6, UpdateJoiningStatus = 7, GetPopupGroupMemberDtls = 8, GetJoinedOrgGroups=9,
            GetJoinStatus=10,

        };


        public DA_Scrl_UserGroupJoin()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void AddEditDel_Scrl_UserGroupJoin(DO_Scrl_UserGroupJoin ObjScrl_UserGroupJoin, Scrl_UserGroupJoin Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelGroupJoin", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@inGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.inGroupId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intRegistrationId;
            cmd.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intInvitedUserId;
            cmd.Parameters.Add("@intRequestJoinId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intRequestJoinId;
            cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.PageSize;
            cmd.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.Currentpage;
            cmd.Parameters.Add("@IsAccepted", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.isAccepted;                        
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intAddedBy;                        
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupJoin.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserGroupJoin ObjScrl_UserGroupJoin, Scrl_UserGroupJoin Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("Scrl_AddEditDelGroupJoin", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@inGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.inGroupId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intInvitedUserId;
            da.SelectCommand.Parameters.Add("@intRequestJoinId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intRequestJoinId;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.PageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.Currentpage;                                      
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intAddedBy;
            da.SelectCommand.Parameters.Add("@IsAccepted", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.isAccepted;                    
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupJoin.strIpAddress;
            da.SelectCommand.Parameters.Add("@txtSearch", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupJoin.strSearch;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }


        //Mohsin Faras  (10 Feb 2014)
        public void AddEditDel_Scrl_OrgnisationGroupJoin(DO_Scrl_UserGroupJoin ObjScrl_UserGroupJoin, Scrl_OrgnisationGroupJoin Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelOrgnisationGroupJoin", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@inGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.inGroupId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intRegistrationId;
            cmd.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intInvitedUserId;
            cmd.Parameters.Add("@intRequestJoinId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intRequestJoinId;
            cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.PageSize;
            cmd.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.Currentpage;
            cmd.Parameters.Add("@IsAccepted", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.isAccepted;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupJoin.strIpAddress;
            cmd.Parameters.Add("@intOrgnisationID", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intOrgnisationID;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserGroupJoin ObjScrl_UserGroupJoin, Scrl_OrgnisationGroupJoin Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("Scrl_AddEditDelOrgnisationGroupJoin", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@inGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.inGroupId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intInvitedUserId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intInvitedUserId;
            da.SelectCommand.Parameters.Add("@intRequestJoinId", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intRequestJoinId;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.PageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.Currentpage;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intAddedBy;
            da.SelectCommand.Parameters.Add("@IsAccepted", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.isAccepted;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupJoin.strIpAddress;
            da.SelectCommand.Parameters.Add("@txtSearch", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupJoin.strSearch;
            da.SelectCommand.Parameters.Add("@intOrgnisationID", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intOrgnisationID;
            da.SelectCommand.Parameters.Add("@intOrgType", SqlDbType.Int).Value = ObjScrl_UserGroupJoin.intUserTypeId;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

    }
}
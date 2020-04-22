using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_UserGroupDetailTbl
    {
        private SqlCommand cmd;
        public enum Scrl_GroupModulePermissionTbl
        {
            GetWallDetails = 1, UdateModuleAccPermission = 2, DeleteModuleAccPermission = 3, GetUploadDetails = 4, GetEventDetails = 5, GetForumDetails = 6, GetPollDetails = 7, GetJobDetails = 8, GetMemberDetails = 9, GetinviteMember = 10, GetorginviteMember=11
        };

        public enum Scrl_OrgGroupModulePermissionTbl
        {
            GetWallDetails = 1, UdateModuleAccPermission = 2, DeleteModuleAccPermission = 3, GetUploadDetails = 4, GetEventDetails = 5, GetForumDetails = 6, GetPollDetails = 7, GetJobDetails = 8, GetMemberDetails = 9, GetinviteMember = 10, GetorginviteMember = 11
        };

        public enum Scrl_UserGroupDetailTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetGrpupDetails = 6, GetgroupType = 7, GetOtherGroup = 8, GetOtherGroupDetailsByGroupId = 9, AllMyGroupList = 10, AllJoinGroupList = 11, GetJoinedGroup = 12, GetConnectedGroupList=13,
            InserRoleByUserId = 14, AddTempContext = 15, GetCatNameByCatID = 16, EditSubJectCatID = 17, GetDocumentSubCat=18,GetRoleNameByUserId=19,DeleteTempTable=20,InsertGroupInvitation=21,GetMemberNameByUkeyId=22,
            DeleteInvMemberRecord = 23, GetModuleAccessDetails = 24, InsertModuleAccPermission = 25, GetMailId = 26, GetTempRoleGrid = 27, DeleteTempRoleGrid=28,ViewGrpAssignUser = 29, InserCotextOrgGroupDocument = 30, GetContextByGroupID = 31,
            GetAllSelectedContextID = 32, GetGeneralGroupDetaiils = 33, UpdateCotextGroupDocument = 34, DeleteContextGroupDocument = 35, BindRoleByGrpID = 36, GetGrpModuleDetailsAcces = 37, DeleteGrpInvities = 38, ChkGroupOption=39,GetGroupRoleName=40,GetGroupRole=41,
            GetMemberRole = 42, GetGroupMemeberRole = 43, GetMemeberGroup = 44, GetMemberRolefill = 45, UpdateRoleByRoleId = 46, UpdateGroupInvitation = 47, GetGrpRoleRequestPermission = 48, GetRoleDetailById = 49, deleteMemeberRole = 50, InsertGroupInvitationEdit=51,
            UpGrpRoleRequestPermission = 52, GetGroupJoinUser = 53, GetAllGrpupDetails = 54

        };
        public enum Scrl_OrgGroupDetailTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetGrpupDetails = 6, GetgroupType = 7, GetOtherGroup = 8, GetOtherGroupDetailsByGroupId = 9, AllMyGroupList = 10, AllJoinGroupList = 11, GetJoinedGroup = 12, GetConnectedGroupList = 13,
            InserRoleByUserId = 14, AddTempContext = 15, GetCatNameByCatID = 16, EditSubJectCatID = 17, GetDocumentSubCat = 18, GetRoleNameByUserId = 19, DeleteTempTable = 20, InsertGroupInvitation = 21, GetMemberNameByUkeyId = 22,
            DeleteInvMemberRecord = 23, GetModuleAccessDetails = 24, InsertModuleAccPermission = 25, GetMailId = 26,GetGroupCategory=29,GetMatterOffice=30,GetBillingCurrency=31,GetHourlyRate=32,GetPracticeArea=33,InserMatter=34,GetAllOrganizationGroup=35,
            InsertClassRoom = 36, InsertClassTime = 37, GetClassTime = 38, DeleteClassRoomScheById = 39, GetTempRoleGrid = 40, DeleteTempRoleGrid=41,GetOnlyMatterCatgory=42,GetGroupCategoryForPanelShow=43,GetClassRoomGroupDetails=44,
            GetClassTimeDetailsByGrpId = 45, UpdateClassRoom = 46, UpdateClassTime = 47, GetMatterGroupDetails = 48, UpdateMatterDetails = 49, GetGeneralGroupDetais = 50, InserCotextOrgGroupDocument = 51, GetContextByGroupID = 52, UpdateCotextOrgGroupDocument=53,
            GetLastCategoryId = 54, GetUserTypeId = 55, GetAllSelectedContextID = 56, BindRoleByGrpID = 57, GetGrpModuleDetailsAcces = 58, DeleteGrpInvities = 59,GetBindRole=60,GetBindMember=61,GetBindRoleBYId=62,
            UpdateRoleByUserId = 63, DeleteModulePermission = 64, GetMemberRolefillid=65,UpdateOrgGroupInvitation=66,GetOrgGrRole=67
        

        };
        public enum GroupMessage
        {
            Insert = 1
        };
        public enum OrgGroupMessage
        {
            Insert = 1
        };

        public DA_Scrl_UserGroupDetailTbl()
        { }

        public void AddEditDel_Scrl_UserGroupDetailTbl(DO_Scrl_UserGroupDetailTbl ObjScrl_UserGroupDetailTbl, Scrl_UserGroupDetailTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserGroupDetailTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@inGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.inGroupId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRegistrationId;
            cmd.Parameters.Add("@strGroupName", SqlDbType.VarChar, 5000).Value = ObjScrl_UserGroupDetailTbl.strGroupName;
            cmd.Parameters.Add("@strSummary", SqlDbType.VarChar, 500000).Value = ObjScrl_UserGroupDetailTbl.strSummary;
            cmd.Parameters.Add("@strGroupType", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.strGroupType;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 10000).Value = ObjScrl_UserGroupDetailTbl.strDescription;
            cmd.Parameters.Add("@strAccess", SqlDbType.VarChar, 2).Value = ObjScrl_UserGroupDetailTbl.strAccess;
            cmd.Parameters.Add("@strLogoPath", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strLogoPath;
            //cmd.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtAddedOn;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddedBy;
            // cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 5000).Value = ObjScrl_UserGroupDetailTbl.strIpAddress;
            cmd.Parameters.Add("@bitPrivatePublic", SqlDbType.VarChar, 5000).Value = ObjScrl_UserGroupDetailTbl.bitPrivatePublic;
            cmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.locationId;
            cmd.Parameters.Add("@intUserId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intUserId;
            cmd.Parameters.Add("@strRoleName", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strRoleName;
            cmd.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intSubjectCategoryId;
            cmd.Parameters.Add("@DocId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.DocId;
            cmd.Parameters.Add("@strInvitationMessage", SqlDbType.VarChar, 500000).Value = ObjScrl_UserGroupDetailTbl.strInvitationMessage;
            cmd.Parameters.Add("@inviteMembers", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.inviteMembers;
            cmd.Parameters.Add("@strUniqueKey", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strUniqueKey;
            cmd.Parameters.Add("@GrpInvitationMemberId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.grpInvMemberId;
            cmd.Parameters.Add("@intGrpModuleId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGrpModuleId;
            cmd.Parameters.Add("@intVisibility", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intVisibility;
            cmd.Parameters.Add("@intAddPermission", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddPermission;
            cmd.Parameters.Add("@intEditPermission", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intEditPermission;
            cmd.Parameters.Add("@intDeletePermission", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intDeletePermission;
            cmd.Parameters.Add("@intGrpRoleId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGrpRoleId;
            cmd.Parameters.Add("@intRoleId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRoleId;
            cmd.Parameters.Add("@intGrpInvitationMemberId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGrpInvitationMemberId;

            ObjScrl_UserGroupDetailTbl.GroupOutId = Convert.ToInt32(cmd.ExecuteScalar());          
            //cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserGroupDetailTbl ObjScrl_UserGroupDetailTbl, Scrl_UserGroupDetailTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserGroupDetailTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@inGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.inGroupId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strGroupName", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strGroupName;
            da.SelectCommand.Parameters.Add("@strSummary", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strSummary;
            da.SelectCommand.Parameters.Add("@strGroupType", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.strGroupType;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserGroupDetailTbl.strDescription;
            da.SelectCommand.Parameters.Add("@strAccess", SqlDbType.VarChar, 2).Value = ObjScrl_UserGroupDetailTbl.strAccess;
            da.SelectCommand.Parameters.Add("@strLogoPath", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strLogoPath;
            // da.SelectCommand.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtAddedOn;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddedBy;
            // da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@bitPrivatePublic", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.bitPrivatePublic;
            da.SelectCommand.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intSubjectCategoryId;
            da.SelectCommand.Parameters.Add("@strUniqueKey", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.strUniqueKey;
            da.SelectCommand.Parameters.Add("@intUserId ", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intUserId;
            da.SelectCommand.Parameters.Add("@intGrpInvitationMemberId ", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGrpInvitationMemberId;
            da.SelectCommand.Parameters.Add("@GrpInvitationMemberId ", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.grpInvMemberId;
            da.SelectCommand.Parameters.Add("@strMemberName", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strMemberName;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public DataTable GetDataTableGroupType(DO_Scrl_UserGroupDetailTbl ObjScrl_UserGroupDetailTbl, Scrl_UserGroupDetailTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserGroupDetailTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intGroupTypeId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.GroupType;
            da.SelectCommand.Parameters.Add("@strGroupTypeName", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.GroupTypeName;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public DataSet GetDataSet(DO_Scrl_UserGroupDetailTbl ObjScrl_UserGroupDetailTbl, Scrl_UserGroupDetailTbl Flag)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserGroupDetailTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@inGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.inGroupId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strGroupName", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strGroupName;
            da.SelectCommand.Parameters.Add("@strSummary", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strSummary;
            da.SelectCommand.Parameters.Add("@strGroupType", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.strGroupType;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserGroupDetailTbl.strDescription;
            da.SelectCommand.Parameters.Add("@strAccess", SqlDbType.VarChar, 2).Value = ObjScrl_UserGroupDetailTbl.strAccess;
            da.SelectCommand.Parameters.Add("@strLogoPath", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strLogoPath;
            // da.SelectCommand.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtAddedOn;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddedBy;
            // da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@intGrpInvitationMemberId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGrpInvitationMemberId;

            da.Fill(ds);
            co.CloseConnection(conn);
            return ds;
        }

        public void AddEditDel_Scrl_UserRoleGroupDetailTbl(DO_Scrl_UserGroupDetailTbl ObjScrl_UserGroupDetailTbl, Scrl_UserGroupDetailTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserGroupDetailTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@inGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.inGroupId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRegistrationId;
            cmd.Parameters.Add("@strGroupName", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strGroupName;
            cmd.Parameters.Add("@strSummary", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strSummary;
            cmd.Parameters.Add("@strGroupType", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.strGroupType;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserGroupDetailTbl.strDescription;
            cmd.Parameters.Add("@strAccess", SqlDbType.VarChar, 2).Value = ObjScrl_UserGroupDetailTbl.strAccess;
            cmd.Parameters.Add("@strLogoPath", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strLogoPath;
            //cmd.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtAddedOn;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddedBy;
            // cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strIpAddress;
            cmd.Parameters.Add("@bitPrivatePublic", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.bitPrivatePublic;
            cmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.locationId;
            cmd.Parameters.Add("@intUserId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intUserId;
            cmd.Parameters.Add("@strRoleName", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.strRoleName;
            cmd.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intSubjectCategoryId;
            cmd.Parameters.Add("@DocId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.DocId;
            cmd.Parameters.Add("@strInvitationMessage", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.strInvitationMessage;
            cmd.Parameters.Add("@inviteMembers", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.inviteMembers;
            cmd.Parameters.Add("@intRoleId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRoleId;
            cmd.Parameters.Add("@strUniqueKey", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.strUniqueKey;
            cmd.Parameters.Add("@GrpInvitationMemberId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.grpInvMemberId;
            cmd.Parameters.Add("@intGrpModuleId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGrpModuleId;
            cmd.Parameters.Add("@intVisibility", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intVisibility;
            cmd.Parameters.Add("@intAddPermission", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddPermission;
            cmd.Parameters.Add("@intEditPermission", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intEditPermission;
            cmd.Parameters.Add("@intDeletePermission", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intDeletePermission;
            cmd.Parameters.Add("@intGrpRoleId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGrpRoleId;
            cmd.Parameters.Add("@intGrpInvitationMemberId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGrpInvitationMemberId;



            ObjScrl_UserGroupDetailTbl.intRoleId = Convert.ToInt32(cmd.ExecuteScalar());
            //cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetEditDel_Scrl_UserRoleGroupDetailTbl(DO_Scrl_UserGroupDetailTbl ObjScrl_UserGroupDetailTbl, Scrl_UserGroupDetailTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserGroupDetailTbl", conn);
            //cmd = new SqlCommand("SP_Scrl_UserGroupDetailTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intGrpRoleId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGrpRoleId;

            da.SelectCommand.Parameters.Add("@inGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.inGroupId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strGroupName", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strGroupName;
            da.SelectCommand.Parameters.Add("@strSummary", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strSummary;
            da.SelectCommand.Parameters.Add("@strGroupType", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.strGroupType;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserGroupDetailTbl.strDescription;
            da.SelectCommand.Parameters.Add("@strAccess", SqlDbType.VarChar, 2).Value = ObjScrl_UserGroupDetailTbl.strAccess;
            da.SelectCommand.Parameters.Add("@strLogoPath", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strLogoPath;
            // da.SelectCommand.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtAddedOn;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddedBy;
            // da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@intGrpInvitationMemberId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGrpInvitationMemberId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        #region Group Message
        public void AddEditDelGroupMessage(DO_Scrl_UserGroupDetailTbl ObjScrl_UserGroupDetailTbl, GroupMessage Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelGroupMessage", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intInviteeId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intInviteeId;
            cmd.Parameters.Add("@strMessage", SqlDbType.VarChar, 1000).Value = ObjScrl_UserGroupDetailTbl.strDescription;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.inGroupId;           
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = ObjScrl_UserGroupDetailTbl.strIpAddress;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intModifiedBy;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        #endregion


        //organization group

        public void AddEditDel_Scrl_OrgGroupDetailTbl(DO_Scrl_UserGroupDetailTbl ObjScrl_UserGroupDetailTbl, Scrl_OrgGroupDetailTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_OrgGroupDetailTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@inGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.inGroupId;
            cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intOrgId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRegistrationId;
            cmd.Parameters.Add("@strGroupName", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strGroupName;
            cmd.Parameters.Add("@strSummary", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strSummary;
            cmd.Parameters.Add("@strGroupType", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.strGroupType;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserGroupDetailTbl.strDescription;
            cmd.Parameters.Add("@strAccess", SqlDbType.VarChar, 2).Value = ObjScrl_UserGroupDetailTbl.strAccess;
            cmd.Parameters.Add("@strLogoPath", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strLogoPath;
            //cmd.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtAddedOn;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddedBy;
            // cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strIpAddress;
            cmd.Parameters.Add("@bitPrivatePublic", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.bitPrivatePublic;
            cmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.locationId;
            cmd.Parameters.Add("@intUserId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intUserId;
            cmd.Parameters.Add("@strRoleName", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.strRoleName;
            cmd.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intSubjectCategoryId;
            cmd.Parameters.Add("@DocId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.DocId;
            cmd.Parameters.Add("@strInvitationMessage", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.strInvitationMessage;
            cmd.Parameters.Add("@inviteMembers", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.inviteMembers;
            cmd.Parameters.Add("@strUniqueKey", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.strUniqueKey;
            cmd.Parameters.Add("@GrpInvitationMemberId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.grpInvMemberId;
            cmd.Parameters.Add("@intGrpModuleId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGrpModuleId;
            cmd.Parameters.Add("@intVisibility", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intVisibility;
            cmd.Parameters.Add("@intAddPermission", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddPermission;
            cmd.Parameters.Add("@intEditPermission", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intEditPermission;
            cmd.Parameters.Add("@intDeletePermission", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intDeletePermission;
            cmd.Parameters.Add("@intGroupTypeCode", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGroupTypeCode;
            cmd.Parameters.Add("@intOrgType", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intOrgType;
            cmd.Parameters.Add("@intGroupTypeId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGroupTypeId;

            cmd.Parameters.Add("@intOfficeId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intOfficeId;
            cmd.Parameters.Add("@strMatterTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strMatterTitle;
            cmd.Parameters.Add("@strReferenceNo", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strReferenceNo;
            cmd.Parameters.Add("@dtOpenDate", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtOpenDate;
            cmd.Parameters.Add("@intPracticeId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intCategoryId;
            cmd.Parameters.Add("@intCurrencyId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intCurrencyId;
            cmd.Parameters.Add("@strIntialFees", SqlDbType.VarChar,50).Value = ObjScrl_UserGroupDetailTbl.strIntialFees;
            cmd.Parameters.Add("@intHourlyBlendedRate", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intHourlyBlendedRate;
            cmd.Parameters.Add("@strHourlyFees", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.strHourlyFees;


            cmd.Parameters.Add("@strCourseOutline", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strCourseOutline;
            cmd.Parameters.Add("@dtStartDate", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtStartDate;
            cmd.Parameters.Add("@intNoOfClasses", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intNoOfClasses;
            cmd.Parameters.Add("@strHall", SqlDbType.VarChar, 100).Value = ObjScrl_UserGroupDetailTbl.strHall;
            cmd.Parameters.Add("@intNoOfCredit", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intNoOfCredit;
            cmd.Parameters.Add("@strMarkingSystem", SqlDbType.VarChar, 100).Value = ObjScrl_UserGroupDetailTbl.strMarkingSystem;
            cmd.Parameters.Add("@intMaxStudent", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intMaxStudent;
            cmd.Parameters.Add("@dtLastDateOfSignUp", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtLastDateOfSignUp;
            cmd.Parameters.Add("@isAuditingAllowed", SqlDbType.VarChar, 2).Value = ObjScrl_UserGroupDetailTbl.isAuditingAllowed;
            cmd.Parameters.Add("@strAlloedFor", SqlDbType.VarChar, 100).Value = ObjScrl_UserGroupDetailTbl.strAlloedFor;
            cmd.Parameters.Add("@Day", SqlDbType.VarChar, 20).Value = ObjScrl_UserGroupDetailTbl.strDay;
            cmd.Parameters.Add("@strStartTime", SqlDbType.VarChar, 10).Value = ObjScrl_UserGroupDetailTbl.strStartTime;
            cmd.Parameters.Add("@strEndTime", SqlDbType.VarChar, 10).Value = ObjScrl_UserGroupDetailTbl.strEndTime;
            cmd.Parameters.Add("@intClassRoomScheduledId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intClassRoomScheduledId;
            cmd.Parameters.Add("@intGrpRoleId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGrpRoleId;
            cmd.Parameters.Add("@intRoleId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRoleId;
            cmd.Parameters.Add("@IsComplete", SqlDbType.VarChar, 2).Value = ObjScrl_UserGroupDetailTbl.IsComplete;
           // cmd.Parameters.Add("@intNewGroupID ", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intNewGroupID;
            ObjScrl_UserGroupDetailTbl.inGroupId = Convert.ToInt32(cmd.ExecuteScalar());
            //cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public void AddEditDel_Scrl_RoleDetailTbl(DO_Scrl_UserGroupDetailTbl ObjScrl_UserGroupDetailTbl, Scrl_OrgGroupDetailTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_OrgGroupDetailTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@inGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.inGroupId;
            cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intOrgId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRegistrationId;
            cmd.Parameters.Add("@strGroupName", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strGroupName;
            cmd.Parameters.Add("@strSummary", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strSummary;
            cmd.Parameters.Add("@strGroupType", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.strGroupType;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserGroupDetailTbl.strDescription;
            cmd.Parameters.Add("@strAccess", SqlDbType.VarChar, 2).Value = ObjScrl_UserGroupDetailTbl.strAccess;
            cmd.Parameters.Add("@strLogoPath", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strLogoPath;
            //cmd.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtAddedOn;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddedBy;
            // cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strIpAddress;
            cmd.Parameters.Add("@bitPrivatePublic", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.bitPrivatePublic;
            cmd.Parameters.Add("@LocationId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.locationId;
            cmd.Parameters.Add("@intUserId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intUserId;
            cmd.Parameters.Add("@strRoleName", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.strRoleName;
            cmd.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intSubjectCategoryId;
            cmd.Parameters.Add("@DocId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.DocId;
            cmd.Parameters.Add("@strInvitationMessage", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.strInvitationMessage;
            cmd.Parameters.Add("@inviteMembers", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.inviteMembers;
            cmd.Parameters.Add("@intRoleId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRoleId;
            cmd.Parameters.Add("@strUniqueKey", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.strUniqueKey;
            cmd.Parameters.Add("@GrpInvitationMemberId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.grpInvMemberId;
            cmd.Parameters.Add("@intGrpModuleId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGrpModuleId;
            cmd.Parameters.Add("@intVisibility", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intVisibility;
            cmd.Parameters.Add("@intAddPermission", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddPermission;
            cmd.Parameters.Add("@intEditPermission", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intEditPermission;
            cmd.Parameters.Add("@intDeletePermission", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intDeletePermission;
            cmd.Parameters.Add("@intGroupTypeCode", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGroupTypeCode;
            cmd.Parameters.Add("@intOrgType", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intOrgType;
            cmd.Parameters.Add("@intGroupTypeId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGroupTypeId;

            cmd.Parameters.Add("@intOfficeId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intOfficeId;
            cmd.Parameters.Add("@strMatterTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strMatterTitle;
            cmd.Parameters.Add("@strReferenceNo", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strReferenceNo;
            cmd.Parameters.Add("@dtOpenDate", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtOpenDate;
            cmd.Parameters.Add("@intPracticeId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intPracticeId;
            cmd.Parameters.Add("@intCurrencyId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intCurrencyId;
            cmd.Parameters.Add("@strIntialFees", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.strIntialFees;
            cmd.Parameters.Add("@intHourlyBlendedRate", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intHourlyBlendedRate;
            cmd.Parameters.Add("@strHourlyFees", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.strHourlyFees;


            cmd.Parameters.Add("@strCourseOutline", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strCourseOutline;
            cmd.Parameters.Add("@dtStartDate", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtStartDate;
            cmd.Parameters.Add("@intNoOfClasses", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intNoOfClasses;
            cmd.Parameters.Add("@strHall", SqlDbType.VarChar, 100).Value = ObjScrl_UserGroupDetailTbl.strHall;
            cmd.Parameters.Add("@intNoOfCredit", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intNoOfCredit;
            cmd.Parameters.Add("@strMarkingSystem", SqlDbType.VarChar, 100).Value = ObjScrl_UserGroupDetailTbl.strMarkingSystem;
            cmd.Parameters.Add("@intMaxStudent", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intMaxStudent;
            cmd.Parameters.Add("@dtLastDateOfSignUp", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtLastDateOfSignUp;
            cmd.Parameters.Add("@isAuditingAllowed", SqlDbType.VarChar, 2).Value = ObjScrl_UserGroupDetailTbl.isAuditingAllowed;
            cmd.Parameters.Add("@strAlloedFor", SqlDbType.VarChar, 100).Value = ObjScrl_UserGroupDetailTbl.strAlloedFor;
            cmd.Parameters.Add("@Day", SqlDbType.VarChar, 20).Value = ObjScrl_UserGroupDetailTbl.strDay;
            cmd.Parameters.Add("@strStartTime", SqlDbType.VarChar, 10).Value = ObjScrl_UserGroupDetailTbl.strStartTime;
            cmd.Parameters.Add("@strEndTime", SqlDbType.VarChar, 10).Value = ObjScrl_UserGroupDetailTbl.strEndTime;
            cmd.Parameters.Add("@intClassRoomScheduledId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intClassRoomScheduledId;
            cmd.Parameters.Add("@intGrpRoleId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGrpRoleId;
        //    cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRoleId;
            ObjScrl_UserGroupDetailTbl.intRoleId = Convert.ToInt32(cmd.ExecuteScalar());
            //cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetOrgDataTable(DO_Scrl_UserGroupDetailTbl ObjScrl_UserGroupDetailTbl, Scrl_OrgGroupDetailTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_OrgGroupDetailTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@inGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.inGroupId;
            da.SelectCommand.Parameters.Add("@OrgId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intOrgId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strGroupName", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strGroupName;
            da.SelectCommand.Parameters.Add("@strSummary", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strSummary;
            da.SelectCommand.Parameters.Add("@strGroupType", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.strGroupType;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserGroupDetailTbl.strDescription;
            da.SelectCommand.Parameters.Add("@strAccess", SqlDbType.VarChar, 2).Value = ObjScrl_UserGroupDetailTbl.strAccess;
            da.SelectCommand.Parameters.Add("@strLogoPath", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strLogoPath;
            // da.SelectCommand.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtAddedOn;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddedBy;
            // da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@bitPrivatePublic", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.bitPrivatePublic;
            da.SelectCommand.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intSubjectCategoryId;
            da.SelectCommand.Parameters.Add("@strUniqueKey", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.strUniqueKey;
            da.SelectCommand.Parameters.Add("@intGroupTypeCode", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGroupTypeCode;
            da.SelectCommand.Parameters.Add("@intOrgType", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intOrgType;
            da.SelectCommand.Parameters.Add("@intGroupTypeId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGroupTypeId;
            da.SelectCommand.Parameters.Add("@intUserId ", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intUserId;
            da.SelectCommand.Parameters.Add("@GrpInvitationMemberId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.grpInvMemberId;
            da.SelectCommand.Parameters.Add("@intGrpRoleId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intGrpRoleId;
           
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }


        public DataSet GetOrgGroupDataSet(DO_Scrl_UserGroupDetailTbl ObjScrl_UserGroupDetailTbl, Scrl_OrgGroupDetailTbl Flag)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_OrgGroupDetailTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@inGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.inGroupId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strGroupName", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strGroupName;
            da.SelectCommand.Parameters.Add("@strSummary", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strSummary;
            da.SelectCommand.Parameters.Add("@strGroupType", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.strGroupType;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserGroupDetailTbl.strDescription;
            da.SelectCommand.Parameters.Add("@strAccess", SqlDbType.VarChar, 2).Value = ObjScrl_UserGroupDetailTbl.strAccess;
            da.SelectCommand.Parameters.Add("@strLogoPath", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strLogoPath;
            // da.SelectCommand.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtAddedOn;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddedBy;
            // da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strIpAddress;

            da.Fill(ds);
            co.CloseConnection(conn);
            return ds;
        }

        //Mohsin Faras(10 Feb 2014)
        public DataSet GetDataSet(DO_Scrl_UserGroupDetailTbl ObjScrl_UserGroupDetailTbl, Scrl_OrgGroupDetailTbl Flag)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserGroupDetailTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@inGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.inGroupId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strGroupName", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strGroupName;
            da.SelectCommand.Parameters.Add("@strSummary", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strSummary;
            da.SelectCommand.Parameters.Add("@strGroupType", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.strGroupType;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserGroupDetailTbl.strDescription;
            da.SelectCommand.Parameters.Add("@strAccess", SqlDbType.VarChar, 2).Value = ObjScrl_UserGroupDetailTbl.strAccess;
            da.SelectCommand.Parameters.Add("@strLogoPath", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strLogoPath;
            // da.SelectCommand.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtAddedOn;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddedBy;
            // da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@intGrpInvitationMemberId ", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.grpInvMemberId;

            da.Fill(ds);
            co.CloseConnection(conn);
            return ds;
        }

        public void AddEditDelOrgGroupMessage(DO_Scrl_UserGroupDetailTbl ObjScrl_UserGroupDetailTbl, OrgGroupMessage Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelOrgnisationGroupMessage", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intInviteeId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intInviteeId;
            cmd.Parameters.Add("@strMessage", SqlDbType.VarChar, 1000).Value = ObjScrl_UserGroupDetailTbl.strDescription;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.inGroupId;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = ObjScrl_UserGroupDetailTbl.strIpAddress;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intModifiedBy;
            cmd.Parameters.Add("@intOrgnisationID", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intOrgnisationID;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetRolePermissionDataTable1(DO_Scrl_UserGroupDetailTbl ObjScrl_UserGroupDetailTbl, Scrl_UserGroupDetailTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserGroupDetailTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@inGroupId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.inGroupId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strGroupName", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strGroupName;
            da.SelectCommand.Parameters.Add("@strSummary", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strSummary;
            da.SelectCommand.Parameters.Add("@strGroupType", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.strGroupType;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserGroupDetailTbl.strDescription;
            da.SelectCommand.Parameters.Add("@strAccess", SqlDbType.VarChar, 2).Value = ObjScrl_UserGroupDetailTbl.strAccess;
            da.SelectCommand.Parameters.Add("@strLogoPath", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strLogoPath;
            // da.SelectCommand.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtAddedOn;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intAddedBy;
            // da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserGroupDetailTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@bitPrivatePublic", SqlDbType.VarChar, 500).Value = ObjScrl_UserGroupDetailTbl.bitPrivatePublic;
            da.SelectCommand.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intSubjectCategoryId;
            da.SelectCommand.Parameters.Add("@strUniqueKey", SqlDbType.VarChar, 50).Value = ObjScrl_UserGroupDetailTbl.strUniqueKey;
            da.SelectCommand.Parameters.Add("@intUserId ", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.intUserId;
            da.SelectCommand.Parameters.Add("@GrpInvitationMemberId ", SqlDbType.Int).Value = ObjScrl_UserGroupDetailTbl.grpInvMemberId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }


        public DataTable GetRolePermissionDataTable(DO_Scrl_UserGroupDetailTbl objDO, Scrl_GroupModulePermissionTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_GroupModulePermissionTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intRoleId", SqlDbType.Int).Value = objDO.intRoleId;
            da.SelectCommand.Parameters.Add("@intModuleAccessRightId", SqlDbType.Int).Value = objDO.intModuleAccessRightId;
            da.SelectCommand.Parameters.Add("@intVisibility", SqlDbType.Int).Value = objDO.intVisibility;
            da.SelectCommand.Parameters.Add("@intAddPermission", SqlDbType.VarChar, 500).Value = objDO.intAddPermission;
            da.SelectCommand.Parameters.Add("@intEditPermission", SqlDbType.VarChar, 500).Value = objDO.intEditPermission;
            da.SelectCommand.Parameters.Add("@intDeletePermission", SqlDbType.VarChar, 500).Value = objDO.intDeletePermission;
            da.SelectCommand.Parameters.Add("@inviteMembers", SqlDbType.VarChar, 500).Value = objDO.inviteMembers;
            da.SelectCommand.Parameters.Add("@inGroupId", SqlDbType.Int).Value = objDO.inGroupId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;           
        }

        public DataTable GetOrgRolePermissionDataTable(DO_Scrl_UserGroupDetailTbl objDO, Scrl_OrgGroupModulePermissionTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_OrgGroupModulePermissionTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intRoleId", SqlDbType.Int).Value = objDO.intRoleId;
            da.SelectCommand.Parameters.Add("@intModuleAccessRightId", SqlDbType.Int).Value = objDO.intModuleAccessRightId;
            da.SelectCommand.Parameters.Add("@intVisibility", SqlDbType.Int).Value = objDO.intVisibility;
            da.SelectCommand.Parameters.Add("@intAddPermission", SqlDbType.VarChar, 500).Value = objDO.intAddPermission;
            da.SelectCommand.Parameters.Add("@intEditPermission", SqlDbType.VarChar, 500).Value = objDO.intEditPermission;
            da.SelectCommand.Parameters.Add("@intDeletePermission", SqlDbType.VarChar, 500).Value = objDO.intDeletePermission;
            da.SelectCommand.Parameters.Add("@inviteMembers", SqlDbType.VarChar, 500).Value = objDO.inviteMembers;
            da.SelectCommand.Parameters.Add("@inGroupId", SqlDbType.Int).Value = objDO.inGroupId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void Udate_Scrl_GroupModulePermissionTbl(DO_Scrl_UserGroupDetailTbl objDO, Scrl_GroupModulePermissionTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_GroupModulePermissionTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intModuleAccessRightId", SqlDbType.Int).Value = objDO.intModuleAccessRightId;
            cmd.Parameters.Add("@intVisibility", SqlDbType.Int).Value = objDO.intVisibility;
            cmd.Parameters.Add("@intAddPermission", SqlDbType.VarChar, 500).Value = objDO.intAddPermission;
            cmd.Parameters.Add("@intEditPermission", SqlDbType.VarChar, 500).Value = objDO.intEditPermission;
            cmd.Parameters.Add("@intDeletePermission", SqlDbType.VarChar, 500).Value = objDO.intDeletePermission;
            cmd.Parameters.Add("@intRoleId", SqlDbType.Int).Value = objDO.intRoleId;

            cmd.Parameters.Add("@inviteMembers", SqlDbType.Int).Value = objDO.inviteMembers;
            cmd.Parameters.Add("@inGroupId", SqlDbType.Int).Value = objDO.inGroupId;


            objDO.intModuleAccessRightId = Convert.ToInt32(cmd.ExecuteScalar());
            co.CloseConnection(conn);

        }

        public void Udate_Scrl_OrgGroupModulePermissionTbl(DO_Scrl_UserGroupDetailTbl objDO, Scrl_OrgGroupModulePermissionTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_OrgGroupModulePermissionTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intModuleAccessRightId", SqlDbType.Int).Value = objDO.intModuleAccessRightId;
            cmd.Parameters.Add("@intVisibility", SqlDbType.Int).Value = objDO.intVisibility;
            cmd.Parameters.Add("@intAddPermission", SqlDbType.VarChar, 500).Value = objDO.intAddPermission;
            cmd.Parameters.Add("@intEditPermission", SqlDbType.VarChar, 500).Value = objDO.intEditPermission;
            cmd.Parameters.Add("@intDeletePermission", SqlDbType.VarChar, 500).Value = objDO.intDeletePermission;
            cmd.Parameters.Add("@intRoleId", SqlDbType.Int).Value = objDO.intRoleId;

            cmd.Parameters.Add("@inviteMembers", SqlDbType.Int).Value = objDO.inviteMembers;
            cmd.Parameters.Add("@inGroupId", SqlDbType.Int).Value = objDO.inGroupId;


            objDO.intModuleAccessRightId = Convert.ToInt32(cmd.ExecuteScalar());
            co.CloseConnection(conn);

        }

        public DataTable GetMember_Scrl_UserGroupDetailTbl(DO_Scrl_UserGroupDetailTbl objDO, Scrl_GroupModulePermissionTbl scrl_GroupModulePermissionTbl)
        {
            throw new NotImplementedException();
        }

        public string GetUniqueKeyPermissionDataTable(DO_Scrl_UserGroupDetailTbl objDO,int RoleId)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_GroupModuleUniquePermissionTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intRoleId", SqlDbType.Int).Value = RoleId;

            da.Fill(dt);
            return objDO.strUniqueKey = dt.Rows[0]["strUniqueKey"].ToString();
           //return objDO.strUniqueKey = cmd.ExecuteScalar().ToString();
           // throw new NotImplementedException();
        }

        public string GetOrgUniqueKeyPermissionDataTable(DO_Scrl_UserGroupDetailTbl objDO, int RoleId)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_OrgGroupModuleUniquePermissionTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intRoleId", SqlDbType.Int).Value = RoleId;

            da.Fill(dt);
            return objDO.strUniqueKey = dt.Rows[0]["strUniqueKey"].ToString();
            //return objDO.strUniqueKey = cmd.ExecuteScalar().ToString();
            // throw new NotImplementedException();
        }
    }
}

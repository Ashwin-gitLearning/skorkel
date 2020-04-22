using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{
    public class DA_Scrl_UserStatusUpdateTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserStatusUpdateTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetSelfWall = 6, AddComment = 11, BindChildList = 12, AddLike = 13, LikeStatus = 14, Share = 15, GetPostLikeUserLists = 16, GetCommentLikeUserLists = 17,
            GetSharePopupDetails = 18, GetFriendAndNotfrndWall = 19, DeleteComment = 20, GetComment = 21, UpdateComment = 22, UnLikeStatus = 23, UnAddLike = 24
        };
        public enum Scrl_OrgStatusUpdateTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetSelfWall = 6, AddComment = 11, BindChildList = 12, AddLike = 13, LikeStatus = 14, Share = 15, GetPostLikeUserLists = 16, GetCommentLikeUserLists = 17,
            GetSharePopupDetails = 18, GetFriendAndNotfrndWall = 19, DeleteComment = 20, GetComment = 21, UpdateComment = 22, GetOrgGrpSelfWall = 23, GetOrgGrpFriendAndNotfrndWall = 24, UnAddLike = 25, GetOrgSelfWall = 26
        };

        #region user Wall
        public DA_Scrl_UserStatusUpdateTbl()
        { }

        public void AddEditDel_Scrl_UserStatusUpdateTbl(DO_Scrl_UserStatusUpdateTbl ObjScrl_UserStatusUpdateTbl, Scrl_UserStatusUpdateTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserStatusUpdateTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intStatusUpdateId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intStatusUpdateId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intRegistrationId;           
            cmd.Parameters.Add("@strPostDescription", SqlDbType.VarChar, 500000000).Value = ObjScrl_UserStatusUpdateTbl.strPostDescription;
            cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 500000000).Value = ObjScrl_UserStatusUpdateTbl.strComment;
            cmd.Parameters.Add("@strPhotoPath", SqlDbType.VarChar, 1000).Value = ObjScrl_UserStatusUpdateTbl.strPhotoPath;
            cmd.Parameters.Add("@strVideoPath", SqlDbType.VarChar, 1000).Value = ObjScrl_UserStatusUpdateTbl.strVideoPath;

            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intAddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = ObjScrl_UserStatusUpdateTbl.strIpAddress;
            cmd.Parameters.Add("@strPostType", SqlDbType.VarChar, 50).Value = ObjScrl_UserStatusUpdateTbl.strPostType;
            cmd.Parameters.Add("@strPostLink", SqlDbType.VarChar, 50).Value = ObjScrl_UserStatusUpdateTbl.strPostLink;

            cmd.Parameters.Add("@strMessage", SqlDbType.VarChar).Value = ObjScrl_UserStatusUpdateTbl.strMessage;
            cmd.Parameters.Add("@strLink", SqlDbType.VarChar).Value = ObjScrl_UserStatusUpdateTbl.strLink;
            cmd.Parameters.Add("@strInvitee", SqlDbType.VarChar).Value = ObjScrl_UserStatusUpdateTbl.strInvitee;

            cmd.Parameters.Add("@intCommentId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intCommentId;

            cmd.CommandTimeout = 120;// Setting command timeout to 2 minutes
            //cmd.ExecuteNonQuery();
            ObjScrl_UserStatusUpdateTbl.intCommentId = Convert.ToInt32(cmd.ExecuteScalar());

            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserStatusUpdateTbl ObjScrl_UserStatusUpdateTbl, Scrl_UserStatusUpdateTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserStatusUpdateTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.CurrentPage;
            da.SelectCommand.Parameters.Add("@intStatusUpdateId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intStatusUpdateId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intRegistrationId;            
            da.SelectCommand.Parameters.Add("@strPostDescription", SqlDbType.VarChar, 500).Value = ObjScrl_UserStatusUpdateTbl.strPostDescription;
            da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserStatusUpdateTbl.strComment;
            da.SelectCommand.Parameters.Add("@strPhotoPath", SqlDbType.VarChar, 100).Value = ObjScrl_UserStatusUpdateTbl.strPhotoPath;
            da.SelectCommand.Parameters.Add("@strVideoPath", SqlDbType.VarChar, 100).Value = ObjScrl_UserStatusUpdateTbl.strVideoPath;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = ObjScrl_UserStatusUpdateTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@FriendIds", SqlDbType.VarChar, 500).Value = ObjScrl_UserStatusUpdateTbl.strFriendList;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void AddEditDel_Like(DO_Scrl_UserStatusUpdateTbl objLike, Scrl_UserStatusUpdateTbl flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserStatusUpdateTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            cmd.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            cmd.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objLike.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetLikeDataTable(DO_Scrl_UserStatusUpdateTbl objLike, Scrl_UserStatusUpdateTbl flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_UserStatusUpdateTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            da.SelectCommand.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            da.SelectCommand.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            da.SelectCommand.Parameters.Add("@intStatusUpdateId", SqlDbType.BigInt).Value = objLike.intStatusUpdateId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objLike.strIpAddress;
            da.SelectCommand.CommandTimeout = 120;// Setting command timeout to 2 minutes
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        #endregion

        #region Organization wall

        public void AddEditDel_Scrl_OrgStatusUpdateTbl(DO_Scrl_UserStatusUpdateTbl ObjScrl_UserStatusUpdateTbl, Scrl_OrgStatusUpdateTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_OrganizationStatusUpdateTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intStatusUpdateId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intStatusUpdateId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intRegistrationId;
            cmd.Parameters.Add("@strPostDescription", SqlDbType.VarChar, 500).Value = ObjScrl_UserStatusUpdateTbl.strPostDescription;
            cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserStatusUpdateTbl.strComment;
            cmd.Parameters.Add("@strPhotoPath", SqlDbType.VarChar, 100).Value = ObjScrl_UserStatusUpdateTbl.strPhotoPath;
            cmd.Parameters.Add("@strVideoPath", SqlDbType.VarChar, 100).Value = ObjScrl_UserStatusUpdateTbl.strVideoPath;

            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intAddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = ObjScrl_UserStatusUpdateTbl.strIpAddress;
            cmd.Parameters.Add("@strPostType", SqlDbType.VarChar, 50).Value = ObjScrl_UserStatusUpdateTbl.strPostType;
            cmd.Parameters.Add("@strPostLink", SqlDbType.VarChar, 50).Value = ObjScrl_UserStatusUpdateTbl.strPostLink;

            cmd.Parameters.Add("@strMessage", SqlDbType.VarChar).Value = ObjScrl_UserStatusUpdateTbl.strMessage;
            cmd.Parameters.Add("@strLink", SqlDbType.VarChar).Value = ObjScrl_UserStatusUpdateTbl.strLink;
            cmd.Parameters.Add("@strInvitee", SqlDbType.VarChar).Value = ObjScrl_UserStatusUpdateTbl.strInvitee;
            cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.orgId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intGroupId;
            cmd.Parameters.Add("@intCommentId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intCommentId;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetOrgDataTable(DO_Scrl_UserStatusUpdateTbl ObjScrl_UserStatusUpdateTbl, Scrl_OrgStatusUpdateTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_OrganizationStatusUpdateTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.CurrentPage;
            da.SelectCommand.Parameters.Add("@intStatusUpdateId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intStatusUpdateId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strPostDescription", SqlDbType.VarChar, 500).Value = ObjScrl_UserStatusUpdateTbl.strPostDescription;
            da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserStatusUpdateTbl.strComment;
            da.SelectCommand.Parameters.Add("@strPhotoPath", SqlDbType.VarChar, 100).Value = ObjScrl_UserStatusUpdateTbl.strPhotoPath;
            da.SelectCommand.Parameters.Add("@strVideoPath", SqlDbType.VarChar, 100).Value = ObjScrl_UserStatusUpdateTbl.strVideoPath;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = ObjScrl_UserStatusUpdateTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@FriendIds", SqlDbType.VarChar, 500).Value = ObjScrl_UserStatusUpdateTbl.strFriendList;
            da.SelectCommand.Parameters.Add("@OrgId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.orgId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intGroupId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void AddEditDel_OrgLike(DO_Scrl_UserStatusUpdateTbl objLike, Scrl_OrgStatusUpdateTbl flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_OrganizationStatusUpdateTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            cmd.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            cmd.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objLike.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetOrgLikeDataTable(DO_Scrl_UserStatusUpdateTbl objLike, Scrl_OrgStatusUpdateTbl flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_OrganizationStatusUpdateTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            da.SelectCommand.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            da.SelectCommand.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            da.SelectCommand.Parameters.Add("@intStatusUpdateId", SqlDbType.BigInt).Value = objLike.intStatusUpdateId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objLike.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
        #endregion

    }
}

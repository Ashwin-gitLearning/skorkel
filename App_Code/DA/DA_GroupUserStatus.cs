using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;
/// <summary>
/// Summary description for DA_GroupUserStatus
/// </summary>

namespace DA_SKORKEL
{
    public class DA_GroupUserStatus
    {
        private SqlCommand cmd;
        public DA_GroupUserStatus()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum GropUserStatusUpdate
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetStatusUpdates= 6, AddComment = 11, BindChildList = 12, AddLike = 13, LikeStatus = 14, Share = 15, GetPostLikeUserLists = 16, GetCommentLikeUserLists = 17,
            GetSharePopupDetails = 18, GetFriendAndNotfrndWall = 19, DeleteComment = 20, GetComment = 21, UpdateComment = 22, GetSelfWall = 23, UnLikeStatus = 24, UnAddLike = 25
        };

        public void AddEditDel_Scrl_UserStatusUpdateTbl(DO_GroupUserStatus ObjScrl_UserStatusUpdateTbl, GropUserStatusUpdate Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_GroupUserStatusUpdateTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            //cmd.Parameters.Add("@intStatusUpdateId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intStatusUpdateId;
            cmd.Parameters.Add("@intGrpStatusUpdateId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intStatusUpdateId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intRegistrationId;
            cmd.Parameters.Add("@strPostDescription", SqlDbType.VarChar, 5000).Value = ObjScrl_UserStatusUpdateTbl.strPostDescription;
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
            cmd.Parameters.Add("@intGroupId", SqlDbType.VarChar).Value = ObjScrl_UserStatusUpdateTbl.intGroupId;

            cmd.Parameters.Add("@intCommentId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intCommentId;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_GroupUserStatus ObjScrl_UserStatusUpdateTbl, GropUserStatusUpdate Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_GroupUserStatusUpdateTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.CurrentPage;
            da.SelectCommand.Parameters.Add("@intGrpStatusUpdateId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intStatusUpdateId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strPostDescription", SqlDbType.VarChar, 5000).Value = ObjScrl_UserStatusUpdateTbl.strPostDescription;
            da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserStatusUpdateTbl.strComment;
            da.SelectCommand.Parameters.Add("@strPhotoPath", SqlDbType.VarChar, 100).Value = ObjScrl_UserStatusUpdateTbl.strPhotoPath;
            da.SelectCommand.Parameters.Add("@strVideoPath", SqlDbType.VarChar, 100).Value = ObjScrl_UserStatusUpdateTbl.strVideoPath;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = ObjScrl_UserStatusUpdateTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@FriendIds", SqlDbType.VarChar, 500).Value = ObjScrl_UserStatusUpdateTbl.strFriendList;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserStatusUpdateTbl.intGroupId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void AddEditDel_Like(DO_GroupUserStatus objLike, GropUserStatusUpdate flag)
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

        public DataTable GetLikeDataTable(DO_GroupUserStatus objLike, GropUserStatusUpdate flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_GroupUserStatusUpdateTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            da.SelectCommand.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            da.SelectCommand.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            da.SelectCommand.Parameters.Add("@intGrpStatusUpdateId", SqlDbType.BigInt).Value = objLike.intStatusUpdateId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objLike.strIpAddress;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.BigInt).Value = objLike.intGroupId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
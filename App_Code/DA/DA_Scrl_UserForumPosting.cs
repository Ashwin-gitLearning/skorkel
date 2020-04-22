using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;
/// <summary>
/// Summary description for DA_Scrl_UserForumPosting
/// </summary>

namespace DA_SKORKEL
{
    public class DA_Scrl_UserForumPosting
    {
        private SqlCommand cmd;
        public enum Scrl_UseForumPosting
        {
            Insert = 1, GetForumDetails = 2, GetForumDetailsById = 4, InsertPostReply = 5, GetReplyForumComment=6,GetTotalLikeByById=7,InsertLike=8,InsertChildLike=9,GetChildLikeCount=10,GetInviteMemberByGroupId=11,
            InsertShare = 12, UpdateForum = 13, DeleteForum = 14, EditForumDetails = 15, GetReplyForumComments = 16, UpdatePostReply = 17, DeletePostReply = 18,
        }
        public enum Scrl_OrgForumPosting
        {
            Insert = 1, GetForumDetails = 2, GetForumDetailsById = 4, InsertPostReply = 5, GetReplyForumComment = 6, GetTotalLikeByById = 7, InsertLike = 8, InsertChildLike = 9, GetChildLikeCount = 10, GetInviteMemberByGroupId = 11,

            InsertShare = 12, EditForum = 13, UpdateForum = 14, Delete = 15, GetOrgForum = 16, GetLikeFOrum = 17, GetChildLikeUser = 18
        }
        public DA_Scrl_UserForumPosting()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public void AddEditDel_Scrl_UserForumPostingTbl(DO_Scrl_UserForumPosting ObjScrl_UserForumPostingTbl, Scrl_UseForumPosting Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserForumPostingTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@strRepLiShStatus", SqlDbType.VarChar, 2).Value = ObjScrl_UserForumPostingTbl.strRepLiShStatus;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intRegistrationId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intGroupId;
            cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.strTitle;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000000000).Value = ObjScrl_UserForumPostingTbl.strDescription;
            cmd.Parameters.Add("@chkPrivateForum", SqlDbType.VarChar, 2).Value = ObjScrl_UserForumPostingTbl.chkPrivateForum;
            cmd.Parameters.Add("@inviteMembers", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.inviteMembers;
            cmd.Parameters.Add("@chkNotify", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.chkNotify;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar,200).Value = ObjScrl_UserForumPostingTbl.strIpAddress;
            cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 1000).Value = ObjScrl_UserForumPostingTbl.strComment;
            cmd.Parameters.Add("@intForumPostingId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intForumPostingId;
            cmd.Parameters.Add("@ForumReplyLikeShareId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.ForumReplyLikeShareId;
            cmd.Parameters.Add("@intSharedPostingId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intSharedPostingId;
            cmd.Parameters.Add("@strCommentUserId", SqlDbType.VarChar, 50).Value = ObjScrl_UserForumPostingTbl.strCommentUserId;

            cmd.Parameters.Add("@strMessage", SqlDbType.VarChar).Value = ObjScrl_UserForumPostingTbl.strMessage;
            cmd.Parameters.Add("@strLink", SqlDbType.VarChar).Value = ObjScrl_UserForumPostingTbl.strLink;
            cmd.Parameters.Add("@strInviteeShare", SqlDbType.VarChar).Value = ObjScrl_UserForumPostingTbl.strInvitee;

            ObjScrl_UserForumPostingTbl.intForumReplyLikeShareId = Convert.ToInt32(cmd.ExecuteScalar());
           // cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetDataTable(DO_Scrl_UserForumPosting ObjScrl_UserForumPostingTbl, Scrl_UseForumPosting Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserForumPostingTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intForumPostingId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intForumPostingId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intGroupId;
            da.SelectCommand.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.strTitle;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.strDescription;
            da.SelectCommand.Parameters.Add("@chkPrivateForum", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.chkPrivateForum;
            da.SelectCommand.Parameters.Add("@chkNotify", SqlDbType.VarChar, 2).Value = ObjScrl_UserForumPostingTbl.chkPrivateForum;
            da.SelectCommand.Parameters.Add("@inviteMembers", SqlDbType.VarChar, 2).Value = ObjScrl_UserForumPostingTbl.chkNotify;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.CurrentPage;
            da.SelectCommand.Parameters.Add("@ForumReplyLikeShareId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.ForumReplyLikeShareId;
            da.SelectCommand.Parameters.Add("@FriendIds", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.strFriendList;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }



        public void AddEditDel_Scrl_OrgForumPostingTbl(DO_Scrl_UserForumPosting ObjScrl_UserForumPostingTbl, Scrl_OrgForumPosting Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_OrgForumPostingTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@strRepLiShStatus", SqlDbType.VarChar, 2).Value = ObjScrl_UserForumPostingTbl.strRepLiShStatus;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intRegistrationId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intGroupId;
            cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.strTitle;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserForumPostingTbl.strDescription;
            cmd.Parameters.Add("@chkPrivateForum", SqlDbType.VarChar, 2).Value = ObjScrl_UserForumPostingTbl.chkPrivateForum;
            cmd.Parameters.Add("@inviteMembers", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.inviteMembers;
            cmd.Parameters.Add("@chkNotify", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.chkNotify;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserForumPostingTbl.strIpAddress;
            cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 1000).Value = ObjScrl_UserForumPostingTbl.strComment;
            cmd.Parameters.Add("@intForumPostingId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intForumPostingId;
            cmd.Parameters.Add("@ForumReplyLikeShareId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.ForumReplyLikeShareId;
            cmd.Parameters.Add("@intSharedPostingId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intSharedPostingId;
            cmd.Parameters.Add("@strCommentUserId", SqlDbType.VarChar, 50).Value = ObjScrl_UserForumPostingTbl.strCommentUserId;

            cmd.Parameters.Add("@strMessage", SqlDbType.VarChar).Value = ObjScrl_UserForumPostingTbl.strMessage;
            cmd.Parameters.Add("@strLink", SqlDbType.VarChar).Value = ObjScrl_UserForumPostingTbl.strLink;
            cmd.Parameters.Add("@strInviteeShare", SqlDbType.VarChar).Value = ObjScrl_UserForumPostingTbl.strInvitee;
            cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.OrgId;
            cmd.Parameters.Add("@strPhotoPath", SqlDbType.VarChar).Value = ObjScrl_UserForumPostingTbl.strPhotoPath;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.VarChar).Value = ObjScrl_UserForumPostingTbl.intModifiedBy;
            ObjScrl_UserForumPostingTbl.intForumReplyLikeShareId = Convert.ToInt32(cmd.ExecuteScalar());
            // cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetOrgDataTable(DO_Scrl_UserForumPosting ObjScrl_UserForumPostingTbl, Scrl_OrgForumPosting Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_OrgForumPostingTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intForumPostingId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intForumPostingId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intGroupId;
            da.SelectCommand.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.strTitle;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.strDescription;
            da.SelectCommand.Parameters.Add("@chkPrivateForum", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.chkPrivateForum;
            da.SelectCommand.Parameters.Add("@chkNotify", SqlDbType.VarChar, 2).Value = ObjScrl_UserForumPostingTbl.chkPrivateForum;
            da.SelectCommand.Parameters.Add("@inviteMembers", SqlDbType.VarChar, 2).Value = ObjScrl_UserForumPostingTbl.chkNotify;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.CurrentPage;
            da.SelectCommand.Parameters.Add("@ForumReplyLikeShareId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.ForumReplyLikeShareId;
            da.SelectCommand.Parameters.Add("@FriendIds", SqlDbType.VarChar, 500).Value = ObjScrl_UserForumPostingTbl.strFriendList;
            da.SelectCommand.Parameters.Add("@OrgId", SqlDbType.Int).Value = ObjScrl_UserForumPostingTbl.OrgId;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
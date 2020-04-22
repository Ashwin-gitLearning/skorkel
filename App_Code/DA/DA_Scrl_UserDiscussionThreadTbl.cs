using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{
    public class DA_Scrl_UserDiscussionThreadTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserDiscussionThreadTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetDiscussionThread = 6, AddComment = 7, BindChildList = 8, AddLike = 9, GetCommentLikeUserLists=10
        };
        public DA_Scrl_UserDiscussionThreadTbl()
        { }
        public void AddEditDel_Scrl_UserDiscussionThreadTbl(DO_Scrl_UserDiscussionThreadTbl ObjScrl_UserDiscussionThreadTbl, Scrl_UserDiscussionThreadTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserDiscussionThreadTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intDiscussionThreadId", SqlDbType.Int).Value = ObjScrl_UserDiscussionThreadTbl.intDiscussionThreadId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserDiscussionThreadTbl.intRegistrationId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserDiscussionThreadTbl.intGroupId;
            cmd.Parameters.Add("@strdiscussionTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserDiscussionThreadTbl.strdiscussionTitle;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 500).Value = ObjScrl_UserDiscussionThreadTbl.strdiscussionDescription;
            cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserDiscussionThreadTbl.strComment;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserDiscussionThreadTbl.intAddedBy;

            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserDiscussionThreadTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserDiscussionThreadTbl.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetDataTable(DO_Scrl_UserDiscussionThreadTbl ObjScrl_UserDiscussionThreadTbl, Scrl_UserDiscussionThreadTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserDiscussionThreadTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intDiscussionThreadId", SqlDbType.Int).Value = ObjScrl_UserDiscussionThreadTbl.intDiscussionThreadId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserDiscussionThreadTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserDiscussionThreadTbl.intGroupId;
            da.SelectCommand.Parameters.Add("@strdiscussionTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserDiscussionThreadTbl.strdiscussionTitle;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 500).Value = ObjScrl_UserDiscussionThreadTbl.strdiscussionDescription;
            da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserDiscussionThreadTbl.strComment;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserDiscussionThreadTbl.intAddedBy;

            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserDiscussionThreadTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserDiscussionThreadTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserDiscussionThreadTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserDiscussionThreadTbl.CurrentPage;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void AddEditDel_Like(DO_Scrl_UserDiscussionThreadTbl objLike, Scrl_UserDiscussionThreadTbl flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("SP_Scrl_UserDiscussionThreadTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            cmd.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            cmd.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = objLike.strIpAddress;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }



        public DataTable GetLikeDataTable(DO_Scrl_UserDiscussionThreadTbl objLike, Scrl_UserDiscussionThreadTbl flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_UserDiscussionThreadTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            da.SelectCommand.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            da.SelectCommand.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = objLike.strIpAddress;


            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}

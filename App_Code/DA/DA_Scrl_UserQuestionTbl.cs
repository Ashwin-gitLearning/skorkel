using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_UserQuestionTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserQuestionTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetQuestion = 6, AddComment = 7, BindChildList = 8, AddLike = 9, FavoriteQuestion = 10, FollowQuestion = 11, GetCommentLikeUserLists=12
        };

        public DA_Scrl_UserQuestionTbl()
        { }

        public void AddEditDel_Scrl_UserQuestionTbl(DO_Scrl_UserQuestionTbl ObjScrl_UserQuestionTbl, Scrl_UserQuestionTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserQuestionTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intQuestionId", SqlDbType.Int).Value = ObjScrl_UserQuestionTbl.intQuestionId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserQuestionTbl.intRegistrationId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserQuestionTbl.intGroupId;
            cmd.Parameters.Add("@strQuestion", SqlDbType.VarChar, 500).Value = ObjScrl_UserQuestionTbl.strQuestion;
            cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserQuestionTbl.strComment;

            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserQuestionTbl.intAddedBy;

            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserQuestionTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserQuestionTbl.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserQuestionTbl ObjScrl_UserQuestionTbl, Scrl_UserQuestionTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserQuestionTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intQuestionId", SqlDbType.Int).Value = ObjScrl_UserQuestionTbl.intQuestionId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserQuestionTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserQuestionTbl.intGroupId;
            da.SelectCommand.Parameters.Add("@strQuestion", SqlDbType.VarChar, 500).Value = ObjScrl_UserQuestionTbl.strQuestion;
            da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserQuestionTbl.strComment;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserQuestionTbl.intAddedBy;

            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserQuestionTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserQuestionTbl.strIpAddress;

            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserQuestionTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserQuestionTbl.CurrentPage;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void AddEditDel_Like(DO_Scrl_UserQuestionTbl objLike, Scrl_UserQuestionTbl flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("SP_Scrl_UserQuestionTbl", conn);
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

        public DataTable GetLikeDataTable(DO_Scrl_UserQuestionTbl objLike, Scrl_UserQuestionTbl flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_UserQuestionTbl", conn);
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

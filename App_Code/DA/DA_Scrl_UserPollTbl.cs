using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_UserPollTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserPollTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetPoll = 6, InsertOptions = 7, BindChildList = 8, AddLike = 9, InsertComment = 10, GetPostedPolls = 11, GetCommentLikeUserLists = 12, GetRecentPolls = 13, GetActivePolls = 14, PollGroupMemberOnly = 15, BindChart = 16, BindChartY = 17, BindChartX = 18, BindChartall = 19, GetUserMember = 20,GetVoteComent=21,DeleteComment=22,GetCommentById=23,UpdateComment=24,
        };
        public enum Scrl_OrgPollTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetPoll = 6, InsertOptions = 7, BindChildList = 8, AddLike = 9, InsertComment = 10, GetPostedPolls = 11, GetCommentLikeUserLists = 12, GetRecentPolls = 13, GetActivePolls = 14, PollGroupMemberOnly = 15, PollOrgGroupMemberOnly = 16, SingleRecordbyPollId = 17, UpdateByPollId = 18, OrgPollDetails = 19, BindChartY = 20, BindChartX = 21, BindChartGrY = 22, BindChartGrX = 23, GerOrggGrPoll = 24, SingleRecordOrgGrp = 25, BindChartall = 26
        };
        public DA_Scrl_UserPollTbl()
        { }

        public void AddEditDel_Scrl_UserPollTbl(DO_Scrl_UserPollTbl ObjScrl_UserPollTbl, Scrl_UserPollTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserPollTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intPollId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intPollId;
            cmd.Parameters.Add("@intPollChildId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intPollChildId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intRegistrationId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intGroupId;
            cmd.Parameters.Add("@strPollName", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strPollName;
            cmd.Parameters.Add("@intCommentId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intCommentId;

            cmd.Parameters.Add("@stroption1", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option1;
            cmd.Parameters.Add("@stroption2", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option2;
            cmd.Parameters.Add("@stroption3", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option3;
            cmd.Parameters.Add("@stroption4", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option4;
            cmd.Parameters.Add("@stroption5", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option5;
            cmd.Parameters.Add("@stroption6", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option6;
            cmd.Parameters.Add("@stroption7", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option7;
            cmd.Parameters.Add("@stroption8", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option8;
            cmd.Parameters.Add("@stroption9", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option9;
            cmd.Parameters.Add("@stroption10", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option10;

            cmd.Parameters.Add("@bitOption1", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption1;
            cmd.Parameters.Add("@bitOption2", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption2;
            cmd.Parameters.Add("@bitOption3", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption3;
            cmd.Parameters.Add("@bitOption4", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption4;
            cmd.Parameters.Add("@bitOption5", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption5;
            cmd.Parameters.Add("@bitOption6", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption6;
            cmd.Parameters.Add("@bitOption7", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption7;
            cmd.Parameters.Add("@bitOption8", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption8;
            cmd.Parameters.Add("@bitOption9", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption9;
            cmd.Parameters.Add("@bitOption10", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption10;

            cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strComment;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intAddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = ObjScrl_UserPollTbl.strIpAddress;

            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strDescription;
            cmd.Parameters.Add("@strVotingPattern", SqlDbType.VarChar, 50).Value = ObjScrl_UserPollTbl.strVotingPattern;
            cmd.Parameters.Add("@strVotingEnds", SqlDbType.VarChar, 50).Value = ObjScrl_UserPollTbl.strVotingEnds;
            cmd.Parameters.Add("@dtPollExpireDate", SqlDbType.DateTime).Value = ObjScrl_UserPollTbl.dtPollExpireDate;
            cmd.Parameters.Add("@strPollExpireTime", SqlDbType.VarChar, 50).Value = ObjScrl_UserPollTbl.strPollExpireTime;
            cmd.Parameters.Add("@strVoteType", SqlDbType.VarChar, 50).Value = ObjScrl_UserPollTbl.strVoteType;
            cmd.Parameters.Add("@bitShowVoteToOthers", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitShowVoteToOthers;

            ObjScrl_UserPollTbl.intPollOutId = Convert.ToInt32(cmd.ExecuteScalar());
            //cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetDataTable(DO_Scrl_UserPollTbl ObjScrl_UserPollTbl, Scrl_UserPollTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserPollTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intPollId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intPollId;
            da.SelectCommand.Parameters.Add("@intPollChildId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intPollChildId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intGroupId;
            da.SelectCommand.Parameters.Add("@strPollName", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strPollName;

            da.SelectCommand.Parameters.Add("@stroption1", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option1;
            da.SelectCommand.Parameters.Add("@stroption2", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option2;
            da.SelectCommand.Parameters.Add("@stroption3", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option3;
            da.SelectCommand.Parameters.Add("@stroption4", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option4;

            da.SelectCommand.Parameters.Add("@stroption5", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option5;
            da.SelectCommand.Parameters.Add("@stroption6", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option6;
            da.SelectCommand.Parameters.Add("@stroption7", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option7;
            da.SelectCommand.Parameters.Add("@stroption8", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option8;
            da.SelectCommand.Parameters.Add("@stroption9", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option9;
            da.SelectCommand.Parameters.Add("@stroption10", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option10;

            da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strComment;
            da.SelectCommand.Parameters.Add("@bitOption1", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption1;
            da.SelectCommand.Parameters.Add("@bitOption2", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption2;
            da.SelectCommand.Parameters.Add("@bitOption3", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption3;
            da.SelectCommand.Parameters.Add("@bitOption4", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption4;

            da.SelectCommand.Parameters.Add("@bitOption5", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption5;
            da.SelectCommand.Parameters.Add("@bitOption6", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption6;
            da.SelectCommand.Parameters.Add("@bitOption7", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption7;
            da.SelectCommand.Parameters.Add("@bitOption8", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption8;
            da.SelectCommand.Parameters.Add("@bitOption9", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption9;
            da.SelectCommand.Parameters.Add("@bitOption10", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption10;


            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserPollTbl.strIpAddress;

            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserPollTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserPollTbl.CurrentPage;
            da.SelectCommand.Parameters.Add("@txtSearch", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strSearch;

            da.SelectCommand.Parameters.Add("@intCommentId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intCommentId;
            

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public DataSet GetDataTableDS(DO_Scrl_UserPollTbl ObjScrl_UserPollTbl, Scrl_UserPollTbl Flag)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserPollTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intPollId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intPollId;
            da.SelectCommand.Parameters.Add("@intPollChildId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intPollChildId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intGroupId;
            da.SelectCommand.Parameters.Add("@strPollName", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strPollName;

            da.SelectCommand.Parameters.Add("@stroption1", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option1;
            da.SelectCommand.Parameters.Add("@stroption2", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option2;
            da.SelectCommand.Parameters.Add("@stroption3", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option3;
            da.SelectCommand.Parameters.Add("@stroption4", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option4;

            da.SelectCommand.Parameters.Add("@stroption5", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option5;
            da.SelectCommand.Parameters.Add("@stroption6", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option6;
            da.SelectCommand.Parameters.Add("@stroption7", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option7;
            da.SelectCommand.Parameters.Add("@stroption8", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option8;
            da.SelectCommand.Parameters.Add("@stroption9", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option9;
            da.SelectCommand.Parameters.Add("@stroption10", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option10;

            da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strComment;
            da.SelectCommand.Parameters.Add("@bitOption1", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption1;
            da.SelectCommand.Parameters.Add("@bitOption2", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption2;
            da.SelectCommand.Parameters.Add("@bitOption3", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption3;
            da.SelectCommand.Parameters.Add("@bitOption4", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption4;

            da.SelectCommand.Parameters.Add("@bitOption5", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption5;
            da.SelectCommand.Parameters.Add("@bitOption6", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption6;
            da.SelectCommand.Parameters.Add("@bitOption7", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption7;
            da.SelectCommand.Parameters.Add("@bitOption8", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption8;
            da.SelectCommand.Parameters.Add("@bitOption9", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption9;
            da.SelectCommand.Parameters.Add("@bitOption10", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption10;


            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserPollTbl.strIpAddress;

            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserPollTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserPollTbl.CurrentPage;
            da.SelectCommand.Parameters.Add("@txtSearch", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strSearch;


            da.Fill(ds);
            co.CloseConnection(conn);
            return ds;
        }

        public void AddEditDel_Like(DO_Scrl_UserPollTbl objLike, Scrl_UserPollTbl flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("SP_Scrl_UserPollTbl", conn);
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

        public DataTable GetLikeDataTable(DO_Scrl_UserPollTbl objLike, Scrl_UserPollTbl flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_UserPollTbl", conn);
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


        //Organization
        public void AddEditDel_Scrl_OrgPollTbl(DO_Scrl_UserPollTbl ObjScrl_UserPollTbl, Scrl_OrgPollTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_OrgPollTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intPollId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intPollId;
            cmd.Parameters.Add("@intPollChildId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intPollChildId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intRegistrationId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intGroupId;
            cmd.Parameters.Add("@strPollName", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strPollName;

            cmd.Parameters.Add("@stroption1", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option1;
            cmd.Parameters.Add("@stroption2", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option2;
            cmd.Parameters.Add("@stroption3", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option3;
            cmd.Parameters.Add("@stroption4", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option4;
            cmd.Parameters.Add("@stroption5", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option5;
            cmd.Parameters.Add("@stroption6", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option6;
            cmd.Parameters.Add("@stroption7", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option7;
            cmd.Parameters.Add("@stroption8", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option8;
            cmd.Parameters.Add("@stroption9", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option9;
            cmd.Parameters.Add("@stroption10", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option10;

            cmd.Parameters.Add("@bitOption1", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption1;
            cmd.Parameters.Add("@bitOption2", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption2;
            cmd.Parameters.Add("@bitOption3", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption3;
            cmd.Parameters.Add("@bitOption4", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption4;
            cmd.Parameters.Add("@bitOption5", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption5;
            cmd.Parameters.Add("@bitOption6", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption6;
            cmd.Parameters.Add("@bitOption7", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption7;
            cmd.Parameters.Add("@bitOption8", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption8;
            cmd.Parameters.Add("@bitOption9", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption9;
            cmd.Parameters.Add("@bitOption10", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption10;

            cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strComment;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intAddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = ObjScrl_UserPollTbl.strIpAddress;

            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strDescription;
            cmd.Parameters.Add("@strVotingPattern", SqlDbType.VarChar, 50).Value = ObjScrl_UserPollTbl.strVotingPattern;
            cmd.Parameters.Add("@strVotingEnds", SqlDbType.VarChar, 50).Value = ObjScrl_UserPollTbl.strVotingEnds;
            cmd.Parameters.Add("@dtPollExpireDate", SqlDbType.DateTime).Value = ObjScrl_UserPollTbl.dtPollExpireDate;
            cmd.Parameters.Add("@strPollExpireTime", SqlDbType.VarChar, 50).Value = ObjScrl_UserPollTbl.strPollExpireTime;
            cmd.Parameters.Add("@strVoteType", SqlDbType.VarChar, 50).Value = ObjScrl_UserPollTbl.strVoteType;
            cmd.Parameters.Add("@bitShowVoteToOthers", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitShowVoteToOthers;
            cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.OrgId;

            ObjScrl_UserPollTbl.intPollOutId = Convert.ToInt32(cmd.ExecuteScalar());
            //cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetorgDataTable(DO_Scrl_UserPollTbl ObjScrl_UserPollTbl, Scrl_OrgPollTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_OrgPollTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intPollId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intPollId;
            da.SelectCommand.Parameters.Add("@intPollChildId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intPollChildId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intGroupId;
            da.SelectCommand.Parameters.Add("@strPollName", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strPollName;

            da.SelectCommand.Parameters.Add("@stroption1", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option1;
            da.SelectCommand.Parameters.Add("@stroption2", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option2;
            da.SelectCommand.Parameters.Add("@stroption3", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option3;
            da.SelectCommand.Parameters.Add("@stroption4", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option4;

            da.SelectCommand.Parameters.Add("@stroption5", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option5;
            da.SelectCommand.Parameters.Add("@stroption6", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option6;
            da.SelectCommand.Parameters.Add("@stroption7", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option7;
            da.SelectCommand.Parameters.Add("@stroption8", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option8;
            da.SelectCommand.Parameters.Add("@stroption9", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option9;
            da.SelectCommand.Parameters.Add("@stroption10", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option10;

            da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strComment;
            da.SelectCommand.Parameters.Add("@bitOption1", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption1;
            da.SelectCommand.Parameters.Add("@bitOption2", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption2;
            da.SelectCommand.Parameters.Add("@bitOption3", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption3;
            da.SelectCommand.Parameters.Add("@bitOption4", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption4;

            da.SelectCommand.Parameters.Add("@bitOption5", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption5;
            da.SelectCommand.Parameters.Add("@bitOption6", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption6;
            da.SelectCommand.Parameters.Add("@bitOption7", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption7;
            da.SelectCommand.Parameters.Add("@bitOption8", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption8;
            da.SelectCommand.Parameters.Add("@bitOption9", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption9;
            da.SelectCommand.Parameters.Add("@bitOption10", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption10;


            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserPollTbl.strIpAddress;

            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserPollTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserPollTbl.CurrentPage;
            da.SelectCommand.Parameters.Add("@txtSearch", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strSearch;
            da.SelectCommand.Parameters.Add("@OrgId", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.OrgId;


            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public DataSet GetorgDataTableDS(DO_Scrl_UserPollTbl ObjScrl_UserPollTbl, Scrl_OrgPollTbl Flag)
        {
            DataSet dt = new DataSet();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_OrgPollTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intPollId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intPollId;
            da.SelectCommand.Parameters.Add("@intPollChildId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intPollChildId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intGroupId;
            da.SelectCommand.Parameters.Add("@strPollName", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strPollName;

            da.SelectCommand.Parameters.Add("@stroption1", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option1;
            da.SelectCommand.Parameters.Add("@stroption2", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option2;
            da.SelectCommand.Parameters.Add("@stroption3", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option3;
            da.SelectCommand.Parameters.Add("@stroption4", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option4;

            da.SelectCommand.Parameters.Add("@stroption5", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option5;
            da.SelectCommand.Parameters.Add("@stroption6", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option6;
            da.SelectCommand.Parameters.Add("@stroption7", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option7;
            da.SelectCommand.Parameters.Add("@stroption8", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option8;
            da.SelectCommand.Parameters.Add("@stroption9", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option9;
            da.SelectCommand.Parameters.Add("@stroption10", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.Option10;

            da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strComment;
            da.SelectCommand.Parameters.Add("@bitOption1", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption1;
            da.SelectCommand.Parameters.Add("@bitOption2", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption2;
            da.SelectCommand.Parameters.Add("@bitOption3", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption3;
            da.SelectCommand.Parameters.Add("@bitOption4", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption4;

            da.SelectCommand.Parameters.Add("@bitOption5", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption5;
            da.SelectCommand.Parameters.Add("@bitOption6", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption6;
            da.SelectCommand.Parameters.Add("@bitOption7", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption7;
            da.SelectCommand.Parameters.Add("@bitOption8", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption8;
            da.SelectCommand.Parameters.Add("@bitOption9", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption9;
            da.SelectCommand.Parameters.Add("@bitOption10", SqlDbType.Bit).Value = ObjScrl_UserPollTbl.bitOption10;


            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserPollTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserPollTbl.strIpAddress;

            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserPollTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserPollTbl.CurrentPage;
            da.SelectCommand.Parameters.Add("@txtSearch", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.strSearch;
            da.SelectCommand.Parameters.Add("@OrgId", SqlDbType.VarChar, 500).Value = ObjScrl_UserPollTbl.OrgId;


            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void AddEditDel_OrgLike(DO_Scrl_UserPollTbl objLike, Scrl_OrgPollTbl flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("SP_Scrl_OrgPollTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            cmd.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            cmd.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = objLike.strIpAddress;
            cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = objLike.OrgId;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetOrgLikeDataTable(DO_Scrl_UserPollTbl objLike, Scrl_OrgPollTbl flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_OrgPollTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            da.SelectCommand.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            da.SelectCommand.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = objLike.strIpAddress;
            da.SelectCommand.Parameters.Add("@OrgId", SqlDbType.VarChar, 200).Value = objLike.OrgId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}

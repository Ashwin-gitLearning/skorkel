using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{
    public class DA_Scrl_UserPostUpdateTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserPostUpdateTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetPostUpdates = 6, GetUserDetails = 7, GetInstituteDetails = 8, GetLawyersDetails = 9,
            GetGroupDetails = 10, AddComment = 11, BindChildList = 12, AddLike = 13, LikePost = 14, Share = 15, GetPostLikeUserLists = 16, GetCommentLikeUserLists = 17,
            GetAllStudentDetails = 18, GetLawFirmAndInstituteDetails = 19, GetUnionallSearchDetails = 20, GetGroupSearchDetails = 21, GetSinglePeople = 22,
            GetSingleOrganization = 23, GetSingleGroup = 24, GetConnectionList = 25, GetMyGroupList = 26, GetMyGroupListkk = 27
        };

        public DA_Scrl_UserPostUpdateTbl()
        { }

        public void AddEditDel_Scrl_UserPostUpdateTbl(DO_Scrl_UserPostUpdateTbl ObjScrl_UserPostUpdateTbl, Scrl_UserPostUpdateTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserPostUpdateTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intPostUpdateId", SqlDbType.Int).Value = ObjScrl_UserPostUpdateTbl.intPostUpdateId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserPostUpdateTbl.intRegistrationId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserPostUpdateTbl.intGroupId;
            cmd.Parameters.Add("@strPostDescription", SqlDbType.VarChar, 500).Value = ObjScrl_UserPostUpdateTbl.strPostDescription;

            cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserPostUpdateTbl.strComment;

            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserPostUpdateTbl.intAddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserPostUpdateTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserPostUpdateTbl.strIpAddress;
            cmd.Parameters.Add("@txtSearch", SqlDbType.VarChar, 2000).Value = ObjScrl_UserPostUpdateTbl.strSearch;

            cmd.Parameters.Add("@strPhotoPath", SqlDbType.VarChar, 100).Value = ObjScrl_UserPostUpdateTbl.strPhotoPath;
            cmd.Parameters.Add("@strDocumentPath", SqlDbType.VarChar, 100).Value = ObjScrl_UserPostUpdateTbl.strDocumentPath;
            cmd.Parameters.Add("@strFileDescription", SqlDbType.VarChar, 100).Value = ObjScrl_UserPostUpdateTbl.strFileDescription;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserPostUpdateTbl ObjScrl_UserPostUpdateTbl, Scrl_UserPostUpdateTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserPostUpdateTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserPostUpdateTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserPostUpdateTbl.CurrentPage;
            da.SelectCommand.Parameters.Add("@intPostUpdateId", SqlDbType.Int).Value = ObjScrl_UserPostUpdateTbl.intPostUpdateId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserPostUpdateTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserPostUpdateTbl.intGroupId;
            da.SelectCommand.Parameters.Add("@strPostDescription", SqlDbType.VarChar, 500).Value = ObjScrl_UserPostUpdateTbl.strPostDescription;

            da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserPostUpdateTbl.strComment;

            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserPostUpdateTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserPostUpdateTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl_UserPostUpdateTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@txtSearch", SqlDbType.VarChar, 2000).Value = ObjScrl_UserPostUpdateTbl.strSearch;

            da.SelectCommand.Parameters.Add("@strPhotoPath", SqlDbType.VarChar, 100).Value = ObjScrl_UserPostUpdateTbl.strPhotoPath;
            da.SelectCommand.Parameters.Add("@strDocumentPath", SqlDbType.VarChar, 100).Value = ObjScrl_UserPostUpdateTbl.strDocumentPath;
            da.SelectCommand.Parameters.Add("@intUserTypeID", SqlDbType.VarChar, 20).Value = ObjScrl_UserPostUpdateTbl.intUserType;
            da.SelectCommand.Parameters.Add("@strCityIds", SqlDbType.VarChar, 50).Value = ObjScrl_UserPostUpdateTbl.strCityType;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void AddEditDel_Like(DO_Scrl_UserPostUpdateTbl objLike, Scrl_UserPostUpdateTbl flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("SP_Scrl_UserPostUpdateTbl", conn);
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

        public DataTable GetLikeDataTable(DO_Scrl_UserPostUpdateTbl objLike, Scrl_UserPostUpdateTbl flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_UserPostUpdateTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            da.SelectCommand.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            da.SelectCommand.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            da.SelectCommand.Parameters.Add("@intPostUpdateId", SqlDbType.BigInt).Value = objLike.intPostUpdateId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = objLike.strIpAddress;


            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

    }
}

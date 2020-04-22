using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_UserJobPostingTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserJobPostingTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetJobPosting = 6, AddComment = 7, BindChildList = 8, AddLike = 9, GetCommentLikeUserLists=10,GetAllCity=11,GetPostJobDetails=12,
            GetAllCompanyName = 13, GetAllComp = 14, GetCount = 15, GetCityNameByCityID = 16, GetLocByJobPostId = 17, EditJobPost = 18, DeleteJobPost = 19,InsertApplyJob=20,GetGrpUserNameByGrpId=21,GetAppledJob=22
        };
        public enum Scrl_OrgJobPostingTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetJobPosting = 6, AddComment = 7, BindChildList = 8, AddLike = 9, GetCommentLikeUserLists = 10, GetAllCity = 11, GetPostJobDetails = 12,
            GetAllCompanyName = 13, GetAllComp = 14, GetCount = 15, GetCityNameByCityID = 16, GetLocByJobPostId = 17, EditJobDetailsByJobPostId = 18, InsertApplyJob = 19, GetGrpUserNameByGrpId = 20, GetUserNameByorgID=21
        };
        public DA_Scrl_UserJobPostingTbl()
        { }

        public void AddEditDel_Scrl_UserJobPostingTbl(DO_Scrl_UserJobPostingTbl ObjScrl_UserJobPostingTbl, Scrl_UserJobPostingTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserJobPostingTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intJobPostingId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intJobPostingId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intRegistrationId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intGroupId;
            cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strTitle;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strDescription;
            cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strComment;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intAddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strIpAddress;
            cmd.Parameters.Add("@StrCityId", SqlDbType.VarChar, 200).Value = ObjScrl_UserJobPostingTbl.StrCityId;
            cmd.Parameters.Add("@strOtherCity", SqlDbType.VarChar, 200).Value = ObjScrl_UserJobPostingTbl.strOtherCity;
            cmd.Parameters.Add("@strJobType", SqlDbType.VarChar, 200).Value = ObjScrl_UserJobPostingTbl.strJobType;
            cmd.Parameters.Add("@strExpiry", SqlDbType.VarChar, 200).Value = ObjScrl_UserJobPostingTbl.strExpiry;
            cmd.Parameters.Add("@strOrganization", SqlDbType.VarChar, 200).Value = ObjScrl_UserJobPostingTbl.strOrganization;
            //if(ObjScrl_UserJobPostingTbl.expiryDate =="Y")
            cmd.Parameters.Add("@expiryDate", SqlDbType.DateTime).Value = ObjScrl_UserJobPostingTbl.expiryDate;
            cmd.Parameters.Add("@intJobAppliedID", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intJobAppliedID;
            cmd.Parameters.Add("@strName", SqlDbType.VarChar, 50).Value = ObjScrl_UserJobPostingTbl.strName;
            cmd.Parameters.Add("@strEmailId", SqlDbType.VarChar, 50).Value = ObjScrl_UserJobPostingTbl.strEmailId;
           cmd.Parameters.Add("@strUploadResume", SqlDbType.VarChar, 250).Value = ObjScrl_UserJobPostingTbl.strUploadResume;
            cmd.Parameters.Add("@StrNotes", SqlDbType.VarChar, 200).Value = ObjScrl_UserJobPostingTbl.StrNotes;
            ObjScrl_UserJobPostingTbl.intJobPostingId = Convert.ToInt32(cmd.ExecuteScalar());
            //cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetDataTable(DO_Scrl_UserJobPostingTbl ObjScrl_UserJobPostingTbl, Scrl_UserJobPostingTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserJobPostingTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intJobPostingId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intJobPostingId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intGroupId;
            da.SelectCommand.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strTitle;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strDescription;
            da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strComment;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.CurrentPage;
            da.SelectCommand.Parameters.Add("@intCityId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intCityId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
        public void AddEditDel_Like(DO_Scrl_UserJobPostingTbl objLike, Scrl_UserJobPostingTbl flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("SP_Scrl_UserJobPostingTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            cmd.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            cmd.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = objLike.strIpAddress;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetLikeDataTable(DO_Scrl_UserJobPostingTbl objLike, Scrl_UserJobPostingTbl flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_UserJobPostingTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            da.SelectCommand.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            da.SelectCommand.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = objLike.strIpAddress;


            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        //organization
        public void AddEditDel_Scrl_OrgJobPostingTbl(DO_Scrl_UserJobPostingTbl ObjScrl_UserJobPostingTbl, Scrl_OrgJobPostingTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_OrgJobPostingTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intJobPostingId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intJobPostingId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intRegistrationId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intGroupId;
            cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strTitle;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strDescription;
            cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strComment;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intAddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strIpAddress;
            cmd.Parameters.Add("@StrCityId", SqlDbType.VarChar, 200).Value = ObjScrl_UserJobPostingTbl.StrCityId;
            cmd.Parameters.Add("@strOtherCity", SqlDbType.VarChar, 200).Value = ObjScrl_UserJobPostingTbl.strOtherCity;
            cmd.Parameters.Add("@strJobType", SqlDbType.VarChar, 200).Value = ObjScrl_UserJobPostingTbl.strJobType;
            cmd.Parameters.Add("@strExpiry", SqlDbType.VarChar, 200).Value = ObjScrl_UserJobPostingTbl.strExpiry;
            cmd.Parameters.Add("@strOrganization", SqlDbType.VarChar, 200).Value = ObjScrl_UserJobPostingTbl.strOrganization;
            //if(ObjScrl_UserJobPostingTbl.expiryDate =="Y")
            cmd.Parameters.Add("@expiryDate", SqlDbType.DateTime).Value = ObjScrl_UserJobPostingTbl.expiryDate;
            cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.OrgId;
            cmd.Parameters.Add("@intJobAppliedID", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intJobAppliedID;
            cmd.Parameters.Add("@strName", SqlDbType.VarChar, 50).Value = ObjScrl_UserJobPostingTbl.strName;
            cmd.Parameters.Add("@strEmailId", SqlDbType.VarChar, 50).Value = ObjScrl_UserJobPostingTbl.strEmailId;
            cmd.Parameters.Add("@strUploadResume", SqlDbType.VarChar, 250).Value = ObjScrl_UserJobPostingTbl.strUploadResume;
            cmd.Parameters.Add("@StrNotes", SqlDbType.VarChar, 200).Value = ObjScrl_UserJobPostingTbl.StrNotes;
            //else
            //    cmd.Parameters.Add("@expiryDate", SqlDbType.DateTime).Value = DBNull.Value;

            ObjScrl_UserJobPostingTbl.intJobPostingId = Convert.ToInt32(cmd.ExecuteScalar());
            //cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetOrgDataTable(DO_Scrl_UserJobPostingTbl ObjScrl_UserJobPostingTbl, Scrl_OrgJobPostingTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_OrgJobPostingTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intJobPostingId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intJobPostingId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intGroupId;
            da.SelectCommand.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strTitle;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strDescription;
            da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strComment;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserJobPostingTbl.strIpAddress;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.CurrentPage;
            da.SelectCommand.Parameters.Add("@intCityId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.intCityId;
            da.SelectCommand.Parameters.Add("@OrgId", SqlDbType.Int).Value = ObjScrl_UserJobPostingTbl.OrgId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
        public void AddEditDel_OrgLike(DO_Scrl_UserJobPostingTbl objLike, Scrl_OrgJobPostingTbl flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("SP_Scrl_OrgJobPostingTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            cmd.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            cmd.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = objLike.strIpAddress;
            cmd.Parameters.Add("@OrgId", SqlDbType.Int).Value = objLike.OrgId;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetOrgLikeDataTable(DO_Scrl_UserJobPostingTbl objLike, Scrl_OrgJobPostingTbl flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_OrgJobPostingTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            da.SelectCommand.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            da.SelectCommand.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = objLike.strIpAddress;
            da.SelectCommand.Parameters.Add("@OrgId", SqlDbType.BigInt).Value = objLike.OrgId;


            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_CaseList
/// </summary>

namespace DA_SKORKEL
{
    public class DA_BlogLikeShare
    {
        public DA_BlogLikeShare()
        {
            //
            // TODO: Add constructor logic here
            //
        }
      
        public enum BlogShareLikeFlag
        {
            BlogLike = 1, GetTotalLikeByDocId = 2, BlogShare = 3, GetTotalShareByDocId = 4, GetTotalReseachBy = 5, DownloadDoc = 6, GetTotalDownloadByDocId = 7, GetMyFreeTop4Downloads = 8, GetMyFreeDownloads = 9, InsertFavrt = 10, GetMyBookMarks = 11, GetTotalFav = 12, GetfavrtDoc = 13, InsertDeleteFavrt = 14
        };

        public enum BlogHeadingShareLikeFlag
        {
            BlogHeadingLike = 1, GetTotalLikeByBlogId = 2, BlogHeadingShare = 3, GetTotalShareByBlogId = 4
        }

        SqlCommand cmd = new SqlCommand();

        #region Blogheadingsharelike
        public void AddEditDel_BlogHeadingLikeShareTbl(DO_BlogLikeShare objblog, DA_BlogLikeShare.BlogHeadingShareLikeFlag flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("SP_BlogHeadingLikeShareTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intBlogHeadingLikeId", SqlDbType.BigInt).Value = objblog.intBlogHeadingLikeId;
            cmd.Parameters.Add("@strRepLiShStatus", SqlDbType.VarChar, 2).Value = objblog.strRepLiShStatus;
            cmd.Parameters.Add("@intBlogId", SqlDbType.Int).Value = objblog.intBlogId;            
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objblog.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objblog.strIpAddress;
            cmd.Parameters.Add("@strInviteeShare", SqlDbType.VarChar, 20).Value = objblog.strInviteeShare;
            cmd.Parameters.Add("@strLink", SqlDbType.VarChar, 200).Value = objblog.strLink;
            cmd.Parameters.Add("@strMessage", SqlDbType.VarChar, 900000000).Value = objblog.strMessage;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.VarChar, 200).Value = objblog.intAddedBy;
            cmd.Parameters.Add("@strBlogTitle", SqlDbType.VarChar, 20000).Value = objblog.strBlogTitle;
            //cmd.ExecuteNonQuery();
            objblog.intBlogHeadingLikeId = Convert.ToInt32(cmd.ExecuteScalar());

            co.CloseConnection(conn);
        }

        public DataTable GetBlogDataTable(DO_BlogLikeShare objblog, BlogHeadingShareLikeFlag flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_BlogHeadingLikeShareTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intBlogId", SqlDbType.Int).Value = objblog.intBlogId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objblog.intAddedBy;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
        #endregion

        //public void AddEditDel_CaseList(DO_BlogLikeShare objcase, DA_BlogLikeShare.BlogShareLikeFlag flag)
        //{

        //    SqlConnection conn = new SqlConnection();
        //    SQLManager co = new SQLManager();
        //    conn = co.GetConnection();

        //    cmd = new SqlCommand("SP_Scrl_BlogLikeShareTbl", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        //    cmd.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objcase.Caseid;
        //    cmd.Parameters.Add("@StrContentTitle", SqlDbType.VarChar, 200).Value = objcase.ContentTitle;
        //    cmd.Parameters.Add("@Citationid", SqlDbType.Int).Value = objcase.CitationId;
        //    cmd.Parameters.Add("@ContentTypeId", SqlDbType.Int).Value = objcase.ContentTypeId;
        //    cmd.Parameters.Add("@intuserid", SqlDbType.Int).Value = objcase.AddedBy;           

        //    cmd.ExecuteNonQuery();
        //    co.CloseConnection(conn);

        //}

        //public DataSet GetDataSet(DO_BlogLikeShare objDoCaseList, CaseList flag)
        //{
        //    DataSet ds = new DataSet();
        //    SqlConnection conn = new SqlConnection();
        //    SQLManager co = new SQLManager();

        //    conn = co.GetConnection();
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = new SqlCommand("scrl_AddEditDeleteCaseList_SP", conn);
        //    da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        //    da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objDoCaseList.Caseid;
        //   // da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objDoCaseList.AddedBy;
        //    da.SelectCommand.Parameters.Add("@ContentTypeId", SqlDbType.Int).Value = objDoCaseList.ContentTypeId;
        //    da.Fill(ds);
        //    co.CloseConnection(conn);
        //    return ds;
        //}

        //public DataTable GetDataTable(DO_BlogLikeShare objDoCaseList, CaseList flag)
        //{
            
        //    DataTable dt = new DataTable();
        //    SqlConnection conn = new SqlConnection();
        //    SQLManager co = new SQLManager();

        //    conn = co.GetConnection();
        //    SqlDataAdapter da = new SqlDataAdapter();
        //    da.SelectCommand = new SqlCommand("scrl_AddEditDeleteCaseList_SP", conn);
        //    da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        //    da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objDoCaseList.Caseid;
        //    da.SelectCommand.Parameters.Add("@StrContentTitle", SqlDbType.VarChar, 200).Value = objDoCaseList.ContentTitle;
        //    da.SelectCommand.Parameters.Add("@intAddedby", SqlDbType.BigInt).Value = objDoCaseList.AddedBy;
        //    da.SelectCommand.Parameters.Add("@ContentTypeId", SqlDbType.Int).Value = objDoCaseList.ContentTypeId;
        //    da.SelectCommand.Parameters.Add("@intTagType", SqlDbType.VarChar, 20).Value = objDoCaseList.intTagType;
        //    da.Fill(dt);
        //    co.CloseConnection(conn);
        //    return dt;
        //}

        public void AddEditDel_BlogCommentLikeShare(DO_BlogLikeShare objcase, DA_BlogLikeShare.BlogShareLikeFlag flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("SP_Scrl_BlogLikeShareTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intBlogLikeShareId", SqlDbType.BigInt).Value = objcase.intBlogLikeShareId;
            cmd.Parameters.Add("@strRepLiShStatus", SqlDbType.VarChar, 2).Value = objcase.strRepLiShStatus;
            cmd.Parameters.Add("@intCommentID", SqlDbType.Int).Value = objcase.intCommentID;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objcase.intRegistrationId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objcase.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objcase.strIpAddress;
            cmd.Parameters.Add("@strInviteeShare", SqlDbType.VarChar, 20).Value = objcase.strInviteeShare;
            cmd.Parameters.Add("@strLink", SqlDbType.VarChar, 20).Value = objcase.strLink;
            cmd.Parameters.Add("@strMessage", SqlDbType.VarChar, 20).Value = objcase.strMessage;
            objcase.intBlogLikeShareId = Convert.ToInt32(cmd.ExecuteScalar());
            co.CloseConnection(conn);

        }
        public DataTable GetBlogDataTable(DO_BlogLikeShare objDoCaseList, BlogShareLikeFlag flag)
        {

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_BlogLikeShareTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intCommentID", SqlDbType.BigInt).Value = objDoCaseList.intCommentID;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objDoCaseList.intAddedBy;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
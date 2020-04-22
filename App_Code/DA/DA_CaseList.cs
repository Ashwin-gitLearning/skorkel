using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_CaseList
/// </summary>

namespace DA_SKORKEL
{
    public class DA_CaseList
    {
        public DA_CaseList()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public enum CaseList
        {
            AddCitation = 1, AddJuridiction = 2, AddCites = 3, AddCitedBy = 4, AddJudgeName = 5, GetCaseRefList = 6, GetAllCites = 7, GetTagsAll = 10,DeleteTagMRF=11,
            GetAllorIndividualTags = 12, GetOnlyMineTags = 13, GetCasedetails = 14, GetMySkorkelTags = 15, GetMySkorkelNewTag = 16, GetMySkorkelTagss = 17,GetCaseDetailsById=18,
        };
        public enum MicroTagLikeShare
        {
            MicroTagLike = 1, GetTotalLikeByDocId = 2, MicrotagShare = 3, GetTotalShareByDocId = 4, GetTotalReseachBy = 5, DownloadDoc = 6, GetTotalDownloadByDocId = 7, GetMyFreeTop4Downloads = 8, GetMyFreeDownloads = 9, InsertFavrt = 10, GetMyBookMarks = 11, GetTotalFav = 12, GetfavrtDoc = 13, InsertDeleteFavrt = 14, DeleteBookmark = 15, DeleteDownloadedDoc = 16,
            GetTotalConsumedByUserId=17,GETUSERID=18, UpdateLikes=19 //added by sourabh for Like Unlike
        };
        SqlCommand cmd = new SqlCommand();


        public void AddEditDel_CaseList(DO_CaseList objcase, DA_CaseList.CaseList flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("scrl_AddEditDeleteCaseList_SP", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objcase.Caseid;
            cmd.Parameters.Add("@StrContentTitle", SqlDbType.VarChar, 200).Value = objcase.ContentTitle;
            cmd.Parameters.Add("@Citationid", SqlDbType.Int).Value = objcase.CitationId;
            cmd.Parameters.Add("@ContentTypeId", SqlDbType.Int).Value = objcase.ContentTypeId;
            cmd.Parameters.Add("@intuserid", SqlDbType.Int).Value = objcase.AddedBy;           

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }

        public DataSet GetDataSet(DO_CaseList objDoCaseList, CaseList flag)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_AddEditDeleteCaseList_SP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objDoCaseList.Caseid;
           // da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objDoCaseList.AddedBy;
            da.SelectCommand.Parameters.Add("@ContentTypeId", SqlDbType.Int).Value = objDoCaseList.ContentTypeId;
            da.Fill(ds);
            co.CloseConnection(conn);
            return ds;
        }

        public DataTable GetDataTable(DO_CaseList objDoCaseList, CaseList flag)
        {
            
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_AddEditDeleteCaseList_SP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objDoCaseList.Caseid;
            da.SelectCommand.Parameters.Add("@StrContentTitle", SqlDbType.VarChar, 200).Value = objDoCaseList.ContentTitle;
            da.SelectCommand.Parameters.Add("@intAddedby", SqlDbType.BigInt).Value = objDoCaseList.AddedBy;
            da.SelectCommand.Parameters.Add("@ContentTypeId", SqlDbType.Int).Value = objDoCaseList.ContentTypeId;
            da.SelectCommand.Parameters.Add("@intTagType", SqlDbType.VarChar, 20).Value = objDoCaseList.intTagType;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = objDoCaseList.PageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = objDoCaseList.Currentpage;                                      
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public void AddEditDel_MicroTagLikeShare(DO_CaseList objcase, DA_CaseList.MicroTagLikeShare flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("SP_Scrl_MicroTagLikeShareTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@intMicroLikeShareId", SqlDbType.BigInt).Value = objcase.intMicroLikeShareId;
            cmd.Parameters.Add("@strRepLiShStatus", SqlDbType.VarChar, 3).Value = objcase.strRepLiShStatus;
            cmd.Parameters.Add("@intDocId", SqlDbType.Int).Value = objcase.intDocId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objcase.intRegistrationId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objcase.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objcase.strIpAddress;
            cmd.Parameters.Add("@strInviteeShare", SqlDbType.VarChar, 100).Value = objcase.strInviteeShare;
            cmd.Parameters.Add("@strLink", SqlDbType.VarChar, 100).Value = objcase.strLink;
            cmd.Parameters.Add("@strMessage", SqlDbType.VarChar, 200).Value = objcase.strMessage;
            cmd.Parameters.Add("@strCaseTitle", SqlDbType.VarChar, 110).Value = objcase.ContentTitle;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 110).Value = objcase.strDescription;
            cmd.Parameters.Add("@Author", SqlDbType.VarChar, 500).Value = objcase.strAuthor;
            cmd.Parameters.Add("@Publisher", SqlDbType.VarChar, 500).Value = objcase.strPublisher;           
            cmd.Parameters.Add("@dtDate", SqlDbType.VarChar, 50).Value = objcase.dtdate;

            objcase.intMicroLikeShareId =Convert.ToInt32(cmd.ExecuteScalar());
            co.CloseConnection(conn);

        }
        public DataTable GetMicroTagDataTable(DO_CaseList objDoCaseList, MicroTagLikeShare flag)
        {

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_MicroTagLikeShareTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intDocId", SqlDbType.BigInt).Value = objDoCaseList.intDocId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objDoCaseList.intAddedBy;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = objDoCaseList.PageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = objDoCaseList.Currentpage;                                      
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
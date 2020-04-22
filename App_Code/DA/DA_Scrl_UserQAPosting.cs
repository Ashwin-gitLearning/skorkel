using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_Scrl_UserQAPosting
/// </summary>
namespace DA_SKORKEL
{
   
    public class DA_Scrl_UserQAPosting
    {
        private SqlCommand cmd;
        public DA_Scrl_UserQAPosting()
        {
           
        }
        public enum QuetionAns
        {
            Add = 1, AddTempSubCat = 2, GetCatNameByCatID = 3, EditSubJectCatID = 4, GetDocumentSubCat = 5, ADDSelectedSubCatId=6,GetRelatedAllQuestion=7,GetTotalQuestionCount=8,
            GetQueAnsDetails = 9, LikeQAInsert = 10, GetTotalLikeByById = 11, InsertShare = 13, GetSingleQueAnsDetails = 14, InsertComment=15,GetMostLike=16,GetAllReplies=17,
            GetTotalReplies = 18, GetSearchResult = 19, DeleteContext = 20, GetAllDetailsOfReplies = 21, GetViewMoreDetails = 22, DeleteQA = 28, GetSearchResults = 29,GetQueAnsDetailss = 30,
            GetLikeUser = 31, GetQA = 32, GetSubjectListQA = 33, GetAllSubjectListQA = 34, Update = 35, DeleteSelectedSubCatId = 36, GetCommentByID = 37, UpdateComment = 38, DeleteAnsCmnts = 39,
            GetQuestionListing = 48
            
        }
        public void AddEditDel_Scrl_UserQueAnsPostingTbl(DO_Scrl_UserQAPosting objQueAns, QuetionAns Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelQuetionAns", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intPostQuestionId", SqlDbType.Int).Value = objQueAns.intPostQuestionId;
            cmd.Parameters.Add("@strQuestionDescription", SqlDbType.VarChar,500).Value = objQueAns.strQuestionDescription;
            cmd.Parameters.Add("@intContextId", SqlDbType.Int).Value = objQueAns.intContextId;
            cmd.Parameters.Add("@strFileName", SqlDbType.VarChar, 50).Value = objQueAns.strFileName;
            cmd.Parameters.Add("@FilePath", SqlDbType.VarChar, 50).Value = objQueAns.strFilePath;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objQueAns.intAddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objQueAns.intModifiedBy;
            cmd.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objQueAns.intSubjectCategoryId;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = objQueAns.strIPAddress;
            cmd.Parameters.Add("@strRepLiShStatus", SqlDbType.VarChar, 2).Value = objQueAns.strRepLiShStatus;
            cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 500000000).Value = objQueAns.strComment;
            cmd.Parameters.Add("@PrvateMessage", SqlDbType.Int).Value = objQueAns.PrvateMessage;
            cmd.Parameters.Add("@intQAReplyLikeShareId", SqlDbType.Int).Value = objQueAns.intQAReplyLikeShareId;
            cmd.Parameters.Add("@strInvitee", SqlDbType.VarChar, 500).Value = objQueAns.strInvitee;
            cmd.Parameters.Add("@strSharelink", SqlDbType.VarChar, 500).Value = objQueAns.strSharelink;
            objQueAns.intPostQuestionId = Convert.ToInt32(cmd.ExecuteScalar());
            co.CloseConnection(conn);
        }
        public void AddEditDel_Scrl_UserQueAnswerTbl(DO_Scrl_UserQAPosting objQueAns, QuetionAns Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
        }
        public DataTable GetDataTable(DO_Scrl_UserQAPosting objcategory, DA_Scrl_UserQAPosting.QuetionAns flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelQuetionAns", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@DocId", SqlDbType.Int).Value = objcategory.DocId;
            da.SelectCommand.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objcategory.intSubjectCategoryId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objcategory.intAddedBy;
            da.SelectCommand.Parameters.Add("@intPostQuestionId", SqlDbType.Int).Value = objcategory.intPostQuestionId;
            da.SelectCommand.Parameters.Add("@txtSearch", SqlDbType.VarChar, 500).Value = objcategory.strSearch;
            da.SelectCommand.Parameters.Add("@Ids", SqlDbType.VarChar, 50).Value = objcategory.ID;
            da.SelectCommand.Parameters.Add("@intQAReplyLikeShareId", SqlDbType.VarChar, 50).Value = objcategory.intQAReplyLikeShareId;
            da.SelectCommand.Parameters.Add("@CurrentPage", SqlDbType.Int).Value = objcategory.CurrentPage;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = objcategory.CurrentPageSize;
            da.SelectCommand.Parameters.Add("@MP", SqlDbType.VarChar, 10).Value = objcategory.MP;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public DataTable GetDataTableQuestion(DO_Scrl_UserQAPosting objcategory, DA_Scrl_UserQAPosting.QuetionAns flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelQuetionAns", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@QuesIds", SqlDbType.VarChar, 200).Value = objcategory.QuestionIdList;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{
    public class DA_Scrl_ProfessorWiseSubjects
    {
        private SqlCommand cmd;
        public enum ProfessorWiseSubjects
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, GetProjects = 6, GetDocuments = 7, AddComment = 8, GetComment = 9, LikeComment = 10, AddUpdateRating = 11,
            GetRating = 12, LikeProject = 13, GetProjectLikeUserLists=14, GetCommentLikeUserLists = 15
        };

        public DA_Scrl_ProfessorWiseSubjects()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void AddEditDel_Scrl_ProfessorWiseSubjects(DO_Scrl_ProfessorWiseSubjects objProfSub, ProfessorWiseSubjects Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_ProfessorWiseSubjects", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intProfessorId", SqlDbType.Int).Value = objProfSub.intProfessorId;
            cmd.Parameters.Add("@intSubjectId", SqlDbType.Int).Value = objProfSub.intSubjectId;
            cmd.Parameters.Add("@intProjectId", SqlDbType.Int).Value = objProfSub.intProjectId;
            cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = objProfSub.strComment;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objProfSub.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objProfSub.strIpAddress;
            cmd.Parameters.Add("@intRating", SqlDbType.VarChar, 20).Value = objProfSub.intRating;

            //cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objProfSub.intRegistrationId;
            //cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = objProfSub.strTitle;
            //cmd.Parameters.Add("@intMonth", SqlDbType.Int).Value = objProfSub.intMonth;
            //cmd.Parameters.Add("@intYear", SqlDbType.Int).Value = objProfSub.intYear;
            //cmd.Parameters.Add("@strLocation", SqlDbType.VarChar, 500).Value = objProfSub.strLocation;
            //cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = objProfSub.strDescription;
            //cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 500).Value = objProfSub.strComment;          
            //cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objProfSub.intModifiedBy;           
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_ProfessorWiseSubjects objProfSub, ProfessorWiseSubjects Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_ProfessorWiseSubjects", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intProfessorId", SqlDbType.Int).Value = objProfSub.intProfessorId;
            da.SelectCommand.Parameters.Add("@intSubjectId", SqlDbType.Int).Value = objProfSub.intSubjectId;
            da.SelectCommand.Parameters.Add("@intProjectId", SqlDbType.Int).Value = objProfSub.intProjectId;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = objProfSub.PageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = objProfSub.CurrentPage;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public DataTable GetLikeDataTable(DO_Scrl_ProfessorWiseSubjects objLike, ProfessorWiseSubjects Flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("SP_Scrl_ProfessorWiseSubjects", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            //da.SelectCommand.Parameters.Add("@intLikeId", SqlDbType.BigInt).Value = objLike.intLikeId;
            da.SelectCommand.Parameters.Add("@intLikeDisLike", SqlDbType.BigInt).Value = objLike.intLikeDisLike;
            da.SelectCommand.Parameters.Add("@intCommentId", SqlDbType.BigInt).Value = objLike.intCommentId;
            da.SelectCommand.Parameters.Add("@intProjectId", SqlDbType.BigInt).Value = objLike.intProjectId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.BigInt).Value = objLike.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objLike.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
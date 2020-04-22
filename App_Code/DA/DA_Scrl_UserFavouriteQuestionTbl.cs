using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{

    public class DA_Scrl_UserFavouriteQuestionTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserFavouriteQuestionTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5
        };

        public DA_Scrl_UserFavouriteQuestionTbl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void AddEditDel_Scrl_UserFavouriteQuestionTbl(DO_Scrl_UserFavouriteQuestionTbl objFavrt, Scrl_UserFavouriteQuestionTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserFavouriteQuestionTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intQuestionFavouriteId", SqlDbType.Int).Value = objFavrt.intQuestionFavouriteId;
            cmd.Parameters.Add("@intQuestionId", SqlDbType.Int).Value = objFavrt.intQuestionId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objFavrt.intRegistrationId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = objFavrt.intGroupId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objFavrt.intAddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objFavrt.intModifiedBy;
            cmd.Parameters.Add("@intMarkFavourite", SqlDbType.Int).Value = objFavrt.intMarkFavourite;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserFavouriteQuestionTbl objFavrt, Scrl_UserFavouriteQuestionTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserFavouriteQuestionTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intQuestionFavouriteId", SqlDbType.Int).Value = objFavrt.intQuestionFavouriteId;
            da.SelectCommand.Parameters.Add("@intQuestionId", SqlDbType.Int).Value = objFavrt.intQuestionId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objFavrt.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = objFavrt.intGroupId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objFavrt.intAddedBy;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objFavrt.intModifiedBy;
            da.SelectCommand.Parameters.Add("@intMarkFavourite", SqlDbType.Int).Value = objFavrt.intMarkFavourite;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
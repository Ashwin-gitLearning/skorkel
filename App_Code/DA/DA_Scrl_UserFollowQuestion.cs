using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{
    public class DA_Scrl_UserFollowQuestion
    {
        private SqlCommand cmd;
        public enum Scrl_UserFollowQuestion
        {
            Add = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
        };

        public DA_Scrl_UserFollowQuestion()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public void Scrl_AddEditDelFollowQuestion(DO_Scrl_UserFollowQuestion ObjScrl, Scrl_UserFollowQuestion Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelFollowQuestion", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intQuestionId", SqlDbType.Int).Value = ObjScrl.intQuestionId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl.intRegistrationId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl.intGroupId;
            cmd.Parameters.Add("@intFollowId", SqlDbType.Int).Value = ObjScrl.intFollowId;
            cmd.Parameters.Add("@intFollowStatus", SqlDbType.Int).Value = ObjScrl.intFollowStatus;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserFollowQuestion ObjScrl, Scrl_UserFollowQuestion Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("Scrl_AddEditDelFollowQuestion", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = ObjScrl.intGroupId;
            da.SelectCommand.Parameters.Add("@intQuestionId", SqlDbType.Int).Value = ObjScrl.intQuestionId;
            da.SelectCommand.Parameters.Add("@intFollowId", SqlDbType.Int).Value = ObjScrl.intFollowId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
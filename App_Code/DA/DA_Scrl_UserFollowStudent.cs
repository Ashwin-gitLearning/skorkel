using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{
    public class DA_Scrl_UserFollowStudent
    {
        private SqlCommand cmd;
        public enum Scrl_UserFollowStudent
        {
            Add = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
        };

        public DA_Scrl_UserFollowStudent()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void Scrl_AddEditDelFollowStudent(DO_Scrl_UserFollowStudent ObjScrl, Scrl_UserFollowStudent Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_AddEditDelFollowStudent", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl.intRegistrationId;    
            cmd.Parameters.Add("@intStudentUserId", SqlDbType.Int).Value = ObjScrl.intStudentUserId;
            cmd.Parameters.Add("@intFollowId", SqlDbType.Int).Value = ObjScrl.intFollowId;                  
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl.intAddedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserFollowStudent ObjScrl, Scrl_UserFollowStudent Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("Scrl_AddEditDelFollowStudent", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intStudentUserId", SqlDbType.Int).Value = ObjScrl.intStudentUserId;
            da.SelectCommand.Parameters.Add("@intFollowId", SqlDbType.Int).Value = ObjScrl.intFollowId;            
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_UserEmailDetailsTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserEmailDetailsTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
        };

        public DA_Scrl_UserEmailDetailsTbl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void AddEditDel_Scrl_UserEmailDetailsTbl(DO_Scrl_UserEmailDetailsTbl ObjScrl_UserEmailDetailsTbl, Scrl_UserEmailDetailsTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserEmailDetailsTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intEmailId", SqlDbType.Int).Value = ObjScrl_UserEmailDetailsTbl.intEmailId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserEmailDetailsTbl.intRegistrationId;
            cmd.Parameters.Add("@strEmailId", SqlDbType.VarChar,50).Value = ObjScrl_UserEmailDetailsTbl.strEmailId;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserEmailDetailsTbl.intAddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserEmailDetailsTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = ObjScrl_UserEmailDetailsTbl.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserEmailDetailsTbl ObjScrl_UserEmailDetailsTbl, Scrl_UserEmailDetailsTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserEmailDetailsTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intEmailId", SqlDbType.Int).Value = ObjScrl_UserEmailDetailsTbl.intEmailId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserEmailDetailsTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strEmailId", SqlDbType.VarChar,50).Value = ObjScrl_UserEmailDetailsTbl.strEmailId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserEmailDetailsTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserEmailDetailsTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = ObjScrl_UserEmailDetailsTbl.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

    }
}
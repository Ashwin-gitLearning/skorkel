using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_UserInterestTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserInterestTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
        };
        public enum KeyPeople
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
        };      

        public DA_Scrl_UserInterestTbl()
        { }

        public void AddEditDel_Scrl_UserInterestTbl(DO_Scrl_UserInterestTbl ObjScrl_UserInterestTbl, Scrl_UserInterestTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserInterestTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intInterestId", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intInterestId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intRegistrationId;
            cmd.Parameters.Add("@strInterests", SqlDbType.VarChar, 500).Value = ObjScrl_UserInterestTbl.strInterests;
            cmd.Parameters.Add("@intPercent", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intPercent;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intAddedBy;
            // cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserInterestTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserInterestTbl.strIpAddress;

            ObjScrl_UserInterestTbl.intOutId = Convert.ToInt32(cmd.ExecuteScalar());
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserInterestTbl ObjScrl_UserInterestTbl, Scrl_UserInterestTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserInterestTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intInterestId", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intInterestId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strInterests", SqlDbType.VarChar, 500).Value = ObjScrl_UserInterestTbl.strInterests;
            da.SelectCommand.Parameters.Add("@intPercent", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intPercent;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intAddedBy;
            // da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserInterestTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserInterestTbl.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }


        public void AddEditDel_Scrl_OrgKeyPeople(DO_Scrl_UserInterestTbl ObjScrl_UserInterestTbl, KeyPeople Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_OrgKeyPeople", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intInterestId", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intInterestId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intRegistrationId;
            cmd.Parameters.Add("@strInterests", SqlDbType.VarChar, 500).Value = ObjScrl_UserInterestTbl.strInterests;
            cmd.Parameters.Add("@intPercent", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intPercent;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intAddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserInterestTbl.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetOrgDataTable(DO_Scrl_UserInterestTbl ObjScrl_UserInterestTbl, KeyPeople Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_OrgKeyPeople", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intInterestId", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intInterestId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strInterests", SqlDbType.VarChar, 500).Value = ObjScrl_UserInterestTbl.strInterests;
            da.SelectCommand.Parameters.Add("@intPercent", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intPercent;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intAddedBy;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserInterestTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserInterestTbl.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_UserEventsTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserEventsTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
        };
        public DA_Scrl_UserEventsTbl()
        { }
        public void AddEditDel_Scrl_UserEventsTbl(DO_Scrl_UserEventsTbl ObjScrl_UserEventsTbl, Scrl_UserEventsTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserEventsTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intEventstId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intEventstId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intRegistrationId;
            cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strTitle;
            cmd.Parameters.Add("@intMonth", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intMonth;
            cmd.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intYear;
            cmd.Parameters.Add("@strLocation", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strLocation;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserEventsTbl.strDescription;
            //  cmd.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserEventsTbl.dtAddedOn;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intAddedBy;
            //  cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserEventsTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetDataTable(DO_Scrl_UserEventsTbl ObjScrl_UserEventsTbl, Scrl_UserEventsTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserEventsTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intEventstId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intEventstId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strTitle;
            da.SelectCommand.Parameters.Add("@intMonth", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intMonth;
            da.SelectCommand.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intYear;
            da.SelectCommand.Parameters.Add("@strLocation", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strLocation;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserEventsTbl.strDescription;
            // da.SelectCommand.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserEventsTbl.dtAddedOn;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intAddedBy;
            // da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserEventsTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserEventsTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserEventsTbl.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}

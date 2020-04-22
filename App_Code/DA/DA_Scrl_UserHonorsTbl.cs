using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_UserHonorsTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserHonorsTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
        };
        public DA_Scrl_UserHonorsTbl()
        { }
        public void AddEditDel_Scrl_UserHonorsTbl(DO_Scrl_UserHonorsTbl ObjScrl_UserHonorsTbl, Scrl_UserHonorsTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserHonorsTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intHonorId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intHonorId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRegistrationId;
            cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 200).Value = ObjScrl_UserHonorsTbl.strTitle;
            cmd.Parameters.Add("@strIssuer", SqlDbType.VarChar, 50).Value = ObjScrl_UserHonorsTbl.strIssuer;
            cmd.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intYear;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserHonorsTbl.strDescription;
            // cmd.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserHonorsTbl.dtAddedOn;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intAddedBy;
            //  cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserHonorsTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserHonorsTbl.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetDataTable(DO_Scrl_UserHonorsTbl ObjScrl_UserHonorsTbl, Scrl_UserHonorsTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserHonorsTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intHonorId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intHonorId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strTitle", SqlDbType.VarChar, 200).Value = ObjScrl_UserHonorsTbl.strTitle;
            da.SelectCommand.Parameters.Add("@strIssuer", SqlDbType.VarChar, 50).Value = ObjScrl_UserHonorsTbl.strIssuer;
            da.SelectCommand.Parameters.Add("@intYear", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intYear;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserHonorsTbl.strDescription;
            //da.SelectCommand.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserHonorsTbl.dtAddedOn;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intAddedBy;
            //da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserHonorsTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserHonorsTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 50).Value = ObjScrl_UserHonorsTbl.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}

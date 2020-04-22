using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_APILogDetailsTbl
    {
        public DA_Scrl_APILogDetailsTbl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private SqlCommand cmd;
        public enum Scrl_APILogDetailsTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
        };

        public void AddEditDel_Scrl_APILogDetailsTbl(DO_Scrl_APILogDetailsTbl ObjScrl, Scrl_APILogDetailsTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_APILogDetailsTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intAPILogId", SqlDbType.Int).Value = ObjScrl.intAPILogId;
            cmd.Parameters.Add("@strAPIType", SqlDbType.VarChar,50).Value = ObjScrl.strAPIType;
            cmd.Parameters.Add("@strURL", SqlDbType.VarChar, 500).Value = ObjScrl.strURL;
            cmd.Parameters.Add("@strResponse", SqlDbType.VarChar, 8000).Value = ObjScrl.strResponse;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl.intAddedBy;            
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl.intModifiedBy;
            cmd.Parameters.Add("@strIPAddress", SqlDbType.VarChar, 20).Value = ObjScrl.strIPAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_APILogDetailsTbl ObjScrl, Scrl_APILogDetailsTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_APILogDetailsTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intAPILogId", SqlDbType.Int).Value = ObjScrl.intAPILogId;
            da.SelectCommand.Parameters.Add("@intCollegeId", SqlDbType.VarChar,50).Value = ObjScrl.strAPIType;
            da.SelectCommand.Parameters.Add("@strURL", SqlDbType.VarChar, 150).Value = ObjScrl.strURL;
            da.SelectCommand.Parameters.Add("@strResponse", SqlDbType.VarChar, 8000).Value = ObjScrl.strResponse;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl.intAddedBy;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIPAddress", SqlDbType.VarChar, 20).Value = ObjScrl.strIPAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_FeeMasterTbl
    {
        private SqlCommand cmd;
        public enum Scrl_FeeMasterTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
        };
        public DA_Scrl_FeeMasterTbl()
        { }
        public void AddEditDel_Scrl_FeeMasterTbl(DO_Scrl_FeeMasterTbl ObjScrl_FeeMasterTbl, Scrl_FeeMasterTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_FeeMasterTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intFeeId", SqlDbType.Int).Value = ObjScrl_FeeMasterTbl.intFeeId;
            cmd.Parameters.Add("@strFeeName", SqlDbType.VarChar, 50).Value = ObjScrl_FeeMasterTbl.strFeeName;            
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_FeeMasterTbl.intAddedBy;            
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_FeeMasterTbl.intModifiedBy;
            cmd.Parameters.Add("@strIPAddress", SqlDbType.VarChar, 20).Value = ObjScrl_FeeMasterTbl.strIPAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetDataTable(DO_Scrl_FeeMasterTbl ObjScrl_FeeMasterTbl, Scrl_FeeMasterTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_FeeMasterTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intFeeId", SqlDbType.Int).Value = ObjScrl_FeeMasterTbl.intFeeId;
            da.SelectCommand.Parameters.Add("@strFeeName", SqlDbType.VarChar, 50).Value = ObjScrl_FeeMasterTbl.strFeeName;            
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_FeeMasterTbl.intAddedBy;            
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_FeeMasterTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIPAddress", SqlDbType.VarChar, 20).Value = ObjScrl_FeeMasterTbl.strIPAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}

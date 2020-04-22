using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_UserMobileDetailsTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserMobileDetailsTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
        };

        public DA_Scrl_UserMobileDetailsTbl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void AddEditDel_Scrl_UserMobileDetailsTbl(DO_Scrl_UserMobileDetailsTbl ObjScrl_UserMobileDetailsTbl, Scrl_UserMobileDetailsTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserMobileDetailsTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intMobileId", SqlDbType.Int).Value = ObjScrl_UserMobileDetailsTbl.intMobileId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserMobileDetailsTbl.intRegistrationId;
            cmd.Parameters.Add("@intMobileNo", SqlDbType.BigInt).Value = ObjScrl_UserMobileDetailsTbl.intMobileNo;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserMobileDetailsTbl.intAddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserMobileDetailsTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = ObjScrl_UserMobileDetailsTbl.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserMobileDetailsTbl ObjScrl_UserMobileDetailsTbl, Scrl_UserMobileDetailsTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserMobileDetailsTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intMobileId", SqlDbType.Int).Value = ObjScrl_UserMobileDetailsTbl.intMobileId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserMobileDetailsTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intMobileNo", SqlDbType.BigInt).Value = ObjScrl_UserMobileDetailsTbl.intMobileNo;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserMobileDetailsTbl.intAddedBy;            
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserMobileDetailsTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = ObjScrl_UserMobileDetailsTbl.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_UserContactDetailsTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserContactDetailsTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
        };
        public DA_Scrl_UserContactDetailsTbl()
        { }
        public void AddEditDel_Scrl_UserContactDetailsTbl(DO_Scrl_UserContactDetailsTbl ObjScrl_UserContactDetailsTbl, Scrl_UserContactDetailsTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserContactDetailsTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intContactId", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intContactId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intRegistrationId;
            cmd.Parameters.Add("@intMobileNo", SqlDbType.BigInt).Value = ObjScrl_UserContactDetailsTbl.intMobileNo;
            cmd.Parameters.Add("@intPhoneNo", SqlDbType.BigInt).Value = ObjScrl_UserContactDetailsTbl.intPhoneNo;
            cmd.Parameters.Add("@strEmailId", SqlDbType.VarChar, 500).Value = ObjScrl_UserContactDetailsTbl.strEmailId;
            cmd.Parameters.Add("@strWebSite", SqlDbType.VarChar, 500).Value = ObjScrl_UserContactDetailsTbl.strWebSite;
            cmd.Parameters.Add("@strAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserContactDetailsTbl.strAddress;
            cmd.Parameters.Add("@strAddress2", SqlDbType.VarChar, 500).Value = ObjScrl_UserContactDetailsTbl.strAddress2;
            cmd.Parameters.Add("@intCountry", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intCountry;
            cmd.Parameters.Add("@intState", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intState;
            cmd.Parameters.Add("@intCity", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intCity;
            cmd.Parameters.Add("@intPinCode", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intPinCode;
            //  cmd.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserContactDetailsTbl.dtAddedOn;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intAddedBy;
            // cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserContactDetailsTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 100).Value = ObjScrl_UserContactDetailsTbl.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetDataTable(DO_Scrl_UserContactDetailsTbl ObjScrl_UserContactDetailsTbl, Scrl_UserContactDetailsTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserContactDetailsTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intContactId", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intContactId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intMobileNo", SqlDbType.BigInt).Value = ObjScrl_UserContactDetailsTbl.intMobileNo;
            da.SelectCommand.Parameters.Add("@intPhoneNo", SqlDbType.BigInt).Value = ObjScrl_UserContactDetailsTbl.intPhoneNo;
            da.SelectCommand.Parameters.Add("@strEmailId", SqlDbType.VarChar, 500).Value = ObjScrl_UserContactDetailsTbl.strEmailId;
            da.SelectCommand.Parameters.Add("@strWebSite", SqlDbType.VarChar, 500).Value = ObjScrl_UserContactDetailsTbl.strWebSite;
            da.SelectCommand.Parameters.Add("@strAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserContactDetailsTbl.strAddress;
            da.SelectCommand.Parameters.Add("@strAddress2", SqlDbType.VarChar, 500).Value = ObjScrl_UserContactDetailsTbl.strAddress2;
            da.SelectCommand.Parameters.Add("@intCountry", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intCountry;
            da.SelectCommand.Parameters.Add("@intState", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intState;
            da.SelectCommand.Parameters.Add("@intCity", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intCity;
            da.SelectCommand.Parameters.Add("@intPinCode", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intPinCode;
            //  da.SelectCommand.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserContactDetailsTbl.dtAddedOn;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intAddedBy;
            // da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserContactDetailsTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserContactDetailsTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 100).Value = ObjScrl_UserContactDetailsTbl.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}

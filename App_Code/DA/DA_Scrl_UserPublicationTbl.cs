using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_UserPublicationTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserPublicationTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCount = 6
        };
        public DA_Scrl_UserPublicationTbl()
        { }
        public void AddEditDel_Scrl_UserPublicationTbl(DO_Scrl_UserPublicationTbl ObjScrl_UserPublicationTbl, Scrl_UserPublicationTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserPublicationTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intPublicationId", SqlDbType.Int).Value = ObjScrl_UserPublicationTbl.intPublicationId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserPublicationTbl.intRegistrationId;
            cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserPublicationTbl.strTitle;
            cmd.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserPublicationTbl.strDescription;
            // cmd.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserPublicationTbl.dtAddedOn;
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserPublicationTbl.intAddedBy;
            //   cmd.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserPublicationTbl.dtModifiedOn;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserPublicationTbl.intModifiedBy;
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserPublicationTbl.strIpAddress;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }
        public DataTable GetDataTable(DO_Scrl_UserPublicationTbl ObjScrl_UserPublicationTbl, Scrl_UserPublicationTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserPublicationTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intPublicationId", SqlDbType.Int).Value = ObjScrl_UserPublicationTbl.intPublicationId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl_UserPublicationTbl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@strTitle", SqlDbType.VarChar, 500).Value = ObjScrl_UserPublicationTbl.strTitle;
            da.SelectCommand.Parameters.Add("@strDescription", SqlDbType.VarChar, 1000).Value = ObjScrl_UserPublicationTbl.strDescription;
            // da.SelectCommand.Parameters.Add("@dtAddedOn", SqlDbType.DateTime).Value = ObjScrl_UserPublicationTbl.dtAddedOn;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl_UserPublicationTbl.intAddedBy;
            //da.SelectCommand.Parameters.Add("@dtModifiedOn", SqlDbType.DateTime).Value = ObjScrl_UserPublicationTbl.dtModifiedOn;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = ObjScrl_UserPublicationTbl.intModifiedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 500).Value = ObjScrl_UserPublicationTbl.strIpAddress;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}

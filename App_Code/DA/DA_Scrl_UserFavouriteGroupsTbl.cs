using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{
    public class DA_Scrl_UserFavouriteGroupsTbl
    {
        private SqlCommand cmd;
        public enum Scrl_UserFavouriteGroupsTbl
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5
        };

        public DA_Scrl_UserFavouriteGroupsTbl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void AddEditDel_Scrl_UserPostUpdateTbl(DO_Scrl_UserFavouriteGroupsTbl objFavrt, Scrl_UserFavouriteGroupsTbl Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("SP_Scrl_UserFavouriteGroupsTbl", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intGroupFavouriteId", SqlDbType.Int).Value = objFavrt.intGroupFavouriteId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objFavrt.intRegistrationId;
            cmd.Parameters.Add("@intGroupId", SqlDbType.Int).Value = objFavrt.intGroupId;                        
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objFavrt.intAddedBy;
            cmd.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objFavrt.intModifiedBy;
            cmd.Parameters.Add("@intMarkFavourite", SqlDbType.Int).Value = objFavrt.intMarkFavourite;      

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserFavouriteGroupsTbl objFavrt, Scrl_UserFavouriteGroupsTbl Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SP_Scrl_UserFavouriteGroupsTbl", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intGroupFavouriteId", SqlDbType.Int).Value = objFavrt.intGroupFavouriteId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objFavrt.intRegistrationId;
            da.SelectCommand.Parameters.Add("@intGroupId", SqlDbType.Int).Value = objFavrt.intGroupId;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objFavrt.intAddedBy;
            da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.Int).Value = objFavrt.intModifiedBy;
            da.SelectCommand.Parameters.Add("@intMarkFavourite", SqlDbType.Int).Value = objFavrt.intMarkFavourite;   
          
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
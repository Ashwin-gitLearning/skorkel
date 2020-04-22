using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;


namespace DA_SKORKEL
{
    public class DA_Scrl_UserFavoriteList
    {
        private SqlCommand cmd;
        public enum Scrl_UserFavoriteList
        {
            GetFavoriteGroup = 1, GetFavoriteQuestion = 2, UpdateFavoriteGroup = 3, UpdateFavoriteQuestion = 4

        };

        public DA_Scrl_UserFavoriteList()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void AddEditDel_Scrl_UserFavoriteList(DO_Scrl_UserFavoriteList ObjFavorite, Scrl_UserFavoriteList Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_UserFavoriteList", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@intFavoriteId", SqlDbType.Int).Value = ObjFavorite.intFavoriteId;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjFavorite.intRegistrationId; 
            //cmd.Parameters.Add("@strLogoPath", SqlDbType.VarChar, 500).Value = ObjFavorite.strLogoPath;            
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjFavorite.intAddedBy;                   
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 10).Value = ObjFavorite.strIpAddress;
            
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_Scrl_UserFavoriteList ObjFavorite, Scrl_UserFavoriteList Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("Scrl_UserFavoriteList", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@intFavoriteId", SqlDbType.Int).Value = ObjFavorite.intFavoriteId;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjFavorite.intRegistrationId;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjFavorite.Currentpage;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjFavorite.PageSize; 
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
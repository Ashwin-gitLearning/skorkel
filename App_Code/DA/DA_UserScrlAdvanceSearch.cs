using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

namespace DA_SKORKEL
{
    public class DA_UserScrlAdvanceSearch
    {
        private SqlCommand cmd;
        public enum ScrlAdvanceSearch
        {
            Insert = 1, Update = 2, Delete = 3, SingleRecord = 4, AllRecords = 5, SelectCity = 6, SelectSpecialization = 7, LawFirms = 8, Institutes = 9, Groups = 10
        };

        public DA_UserScrlAdvanceSearch()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void AddEditDel_Scrl_UserAdvanceSearch(DO_UserScrlAdvanceSearch ObjScrl, ScrlAdvanceSearch Flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            cmd = new SqlCommand("Scrl_UserAdvanceSearch", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl.PageSize;
            cmd.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl.Currentpage;
            //cmd.Parameters.Add("@strInstituteName", SqlDbType.VarChar, 500).Value = ObjScrl.strInstituteName;
            cmd.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl.intRegistrationId;
            cmd.Parameters.Add("@inSpecializationId", SqlDbType.Int).Value = ObjScrl.inSpecializationId;
            cmd.Parameters.Add("@strSpecialization", SqlDbType.VarChar, 100).Value = ObjScrl.strSpecialization;
            cmd.Parameters.Add("@intCityId", SqlDbType.Int).Value = ObjScrl.intCityId;
            cmd.Parameters.Add("@intCountryId", SqlDbType.Int).Value = ObjScrl.intCountryId;
            cmd.Parameters.Add("@txtSearch", SqlDbType.VarChar, 500).Value = ObjScrl.strsearch;                      
            cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl.intAddedBy;                  
            cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl.strIpAddress;
            cmd.Parameters.Add("@intUserType", SqlDbType.VarChar, 200).Value = ObjScrl.intUserType;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);
        }

        public DataTable GetDataTable(DO_UserScrlAdvanceSearch ObjScrl, ScrlAdvanceSearch Flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("Scrl_UserAdvanceSearch", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = Flag;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = ObjScrl.PageSize;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = ObjScrl.Currentpage;
            //da.SelectCommand.Parameters.Add("@strInstituteName", SqlDbType.VarChar, 500).Value = ObjScrl.strInstituteName;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = ObjScrl.intRegistrationId;
            da.SelectCommand.Parameters.Add("@inSpecializationId", SqlDbType.Int).Value = ObjScrl.inSpecializationId;
            da.SelectCommand.Parameters.Add("@strSpecialization", SqlDbType.VarChar, 100).Value = ObjScrl.strSpecialization;            
            da.SelectCommand.Parameters.Add("@intCityId", SqlDbType.Int).Value = ObjScrl.intCityId;
            da.SelectCommand.Parameters.Add("@intCountryId", SqlDbType.Int).Value = ObjScrl.intCountryId;
            da.SelectCommand.Parameters.Add("@txtSearch", SqlDbType.VarChar, 500).Value = ObjScrl.strsearch;
            da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = ObjScrl.intAddedBy;
            da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = ObjScrl.strIpAddress;
            da.SelectCommand.Parameters.Add("@intUserType", SqlDbType.VarChar, 200).Value = ObjScrl.intUserType;
            da.SelectCommand.Parameters.Add("@intExperience", SqlDbType.Int).Value = ObjScrl.intExperience;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
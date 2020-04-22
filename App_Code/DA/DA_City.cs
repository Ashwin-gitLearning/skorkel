using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_City
/// </summary>
/// 
namespace DA_SKORKEL
{

    

    public class DA_City
    {


        public DA_City()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum City
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5, StateWise = 6, GetCityId = 7
        };

        SqlCommand cmd = new SqlCommand();

        public DataTable GetDataTable(DO_City objcity, City flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_CityAddEditDel_SP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@CityId", SqlDbType.BigInt).Value = objcity.CityId;
            da.SelectCommand.Parameters.Add("@CityName", SqlDbType.NVarChar).Value = objcity.CityName;
            da.SelectCommand.Parameters.Add("@StateId", SqlDbType.BigInt).Value = objcity.StateId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_City(DO_City objcity, City flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("scrl_CityAddEditDel_SP", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@CityId", SqlDbType.BigInt).Value = objcity.CityId;
            cmd.Parameters.Add("@CityName", SqlDbType.VarChar).Value = objcity.CityName;
            cmd.Parameters.Add("@StateId", SqlDbType.VarChar).Value = objcity.StateId;
            cmd.Parameters.Add("@CountryId", SqlDbType.VarChar).Value = objcity.CountryId;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objcity.AddedBy;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = objcity.ModifiedBy;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }

    }


}
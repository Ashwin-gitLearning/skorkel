using System.Data;
using System.Data.SqlClient;
using SqlConn;


/// <summary>
/// Summary description for DA_Address
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DA_Address
    {
        public DA_Address()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public enum Address
        {
            Country = 1, state = 2, city = 3, code = 4, RegistrationCountry = 5, RegistrationCity = 6, GetCurrency = 7, getUserCurrency = 8,
            getcurrencyname = 9, getInvoicecurrency = 10, getPrebillcurrency = 11, GetAdminCurrency = 12
        }

        public DataTable GetCountry(int Countryid, int stateid, int CityId, Address flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("PL_AddEditDelAddres_SP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@CountryId", SqlDbType.Int).Value = Countryid;
            da.SelectCommand.Parameters.Add("@StateId", SqlDbType.Int).Value = stateid;
            da.SelectCommand.Parameters.Add("@CityId ", SqlDbType.Int).Value = CityId;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public DataTable GetInvoiceCurrency(string invoiceno, Address flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("PL_AddEditDelAddres_SP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@invoiceno", SqlDbType.VarChar, 100).Value = invoiceno;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

        public DataSet GetCode(int Countryid, int CityId, int stateid, Address flag)
        {
            DataSet dt = new DataSet();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("PL_AddEditDelAddres_SP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@CountryId", SqlDbType.Int).Value = Countryid;
            da.SelectCommand.Parameters.Add("@StateId", SqlDbType.Int).Value = stateid;
            da.SelectCommand.Parameters.Add("@CityId ", SqlDbType.Int).Value = CityId;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }

    }
}
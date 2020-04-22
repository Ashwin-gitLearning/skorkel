using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_Country
/// </summary>
/// 
namespace DA_SKORKEL
{
    public enum Country
    {
        add = 1, Update = 2, Delete = 3, AllRecord = 4, SingleRecord = 5,GetcountryID=6

    };
    
    public class DA_Country
    {

        private SqlCommand cmd;  

        public DA_Country()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        

        public void AddEditDel_Country(DO_Country objCountry, Country flag)
        {
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            cmd = new SqlCommand("scrl_CountryAddEditDel_SP", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@CountryId", SqlDbType.BigInt).Value = objCountry.CountryId;
            cmd.Parameters.Add("@CountryName", SqlDbType.VarChar, 100).Value = objCountry.CountryName;
            cmd.Parameters.Add("@CountryCode", SqlDbType.VarChar, 100).Value = objCountry.CountryCode;
            cmd.Parameters.Add("@AddedBy", SqlDbType.Int).Value = objCountry.AddedBy;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.Int).Value = objCountry.ModifiedBy;
            cmd.ExecuteNonQuery();

            co.CloseConnection(conn);
        }


        public DataTable GetDataTable(DO_Country objCountry, Country flag)
        {
            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_CountryAddEditDel_SP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@CountryId", SqlDbType.BigInt).Value = objCountry.CountryId;
            da.SelectCommand.Parameters.Add("@CountryName", SqlDbType.VarChar, 100).Value = objCountry.CountryName;
           
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

    

    }
}

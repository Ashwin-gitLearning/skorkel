using System;
using System.Data;
using System.Data.SqlClient;
using SqlConn;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for DO_ProfileConnection
/// </summary>
namespace DA_SKORKEL
{
    public class DA_ProfileConnection
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        public enum ProfileConnection
        {
            Add = 1, Update = 2, Delete = 3, AllReg = 4, SingleRecord = 5, DisableUser = 6, SearchText = 7, EmailVerification = 8
        };

        public DA_ProfileConnection()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet GetDataSet(DO_ProfileConnection objRegistration)
        {
            DataSet ds = new DataSet();
            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_UserProfleConnection", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            //da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@intRegistrationId", SqlDbType.Int).Value = objRegistration.RegistrationId;          
            da.Fill(ds);
            co.CloseConnection(conn);
            return ds;
        }
    }
}
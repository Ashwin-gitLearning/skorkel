using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_City
/// </summary>
/// 
namespace DA_SKORKEL
{



    public class DA_InternalUser
    {

        public DA_InternalUser()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum InternalUser
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5,changePass=7,userprofile=8
        };

        SqlCommand cmd = new SqlCommand();

        public void AddEditDel_InternalUser(DO_InternalUser objInernalUser, InternalUser flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelInternalUser", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = objInernalUser.UserId;
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 200).Value = objInernalUser.FirstName;
            cmd.Parameters.Add("@MiddleName", SqlDbType.VarChar, 200).Value = objInernalUser.MiddleName;
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 200).Value = objInernalUser.LastName;
            cmd.Parameters.Add("@Sex", SqlDbType.Bit).Value = objInernalUser.Sex;
            cmd.Parameters.Add("@EmailId", SqlDbType.VarChar,200).Value = objInernalUser.EmailId;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 200).Value = objInernalUser.Password;
            cmd.Parameters.Add("@Address", SqlDbType.VarChar, 500).Value = objInernalUser.Address;
            cmd.Parameters.Add("@City", SqlDbType.BigInt).Value = objInernalUser.City;
            cmd.Parameters.Add("@State", SqlDbType.BigInt).Value = objInernalUser.State;
            cmd.Parameters.Add("@Country", SqlDbType.BigInt).Value = objInernalUser.Country;
            cmd.Parameters.Add("@ParentId", SqlDbType.BigInt).Value = objInernalUser.ParentId;
            cmd.Parameters.Add("@UserType", SqlDbType.BigInt).Value = objInernalUser.UserType;
            cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar,200).Value = objInernalUser.CompanyName;
            cmd.Parameters.Add("@CompannyId", SqlDbType.BigInt).Value = objInernalUser.CompannyId;
            cmd.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objInernalUser.AddedBy;
            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 50).Value = objInernalUser.IPAddress;
            cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar, 50).Value = objInernalUser.ZipCode;
            cmd.Parameters.Add("@PhoneNo", SqlDbType.VarChar, 50).Value = objInernalUser.PhoneNo;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }

        public DataTable GetDataTableInternalUser(DO_InternalUser objInernalUser, InternalUser flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelInternalUser", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@UserId", SqlDbType.BigInt).Value = objInernalUser.UserId;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objInernalUser.AddedBy;
            da.SelectCommand.Parameters.Add("@ParentId", SqlDbType.BigInt).Value = objInernalUser.ParentId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }
    }
}
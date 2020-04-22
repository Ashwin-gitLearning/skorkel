using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_Scrl_AddEditDelDocCategoryDetails
/// </summary>
namespace DA_SKORKEL
{
    public class DA_Scrl_AddEditDelDocCategoryDetails
    {
        public DA_Scrl_AddEditDelDocCategoryDetails()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum DocCategoryDetails
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5, GetByCategoryId = 6
        };

        SqlCommand cmd = new SqlCommand();

        #region Document Category Details Section

        public DataTable GetDataTable(DO_Scrl_AddEditDelDocCategoryDetails objcategory, DA_Scrl_AddEditDelDocCategoryDetails.DocCategoryDetails flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelDocCategoryDetails", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = objcategory.CategoryID;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objcategory.AddedBy;
            da.SelectCommand.Parameters.Add("@RegistrationId", SqlDbType.BigInt).Value = objcategory.RegistrationId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_SubjCategoryDetails(DO_Scrl_AddEditDelDocCategoryDetails objcategory, DA_Scrl_AddEditDelDocCategoryDetails.DocCategoryDetails flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelDocCategoryDetails", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = objcategory.CategoryID;
            cmd.Parameters.Add("@RegistrationId", SqlDbType.VarChar, 200).Value = objcategory.RegistrationId;
            cmd.Parameters.Add("@AddedBy", SqlDbType.VarChar, 200).Value = objcategory.AddedBy;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 200).Value = objcategory.ModifiedBy;
            cmd.Parameters.Add("@IPAddress", SqlDbType.VarChar, 20).Value = objcategory.IPAddress;
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }

        #endregion
    }
}
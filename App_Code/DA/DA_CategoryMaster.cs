using System.Data;
using System.Data.SqlClient;
using SqlConn;


/// <summary>
/// Summary description for DA_Community
/// </summary>
/// 
namespace DA_SKORKEL
{
    public class DA_CategoryMaster
    {
        public DA_CategoryMaster()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public enum CategoryMaster
        {
            Add = 1, Update = 2, Delete = 3, AllRecord = 4, singleRecord = 5,
            AddDocument = 6, UpdateDocument = 7, DeleteDocument = 8, AllRecordDocument = 9, singleRecordDocument = 10,
            AddOthers = 11, UpdateOthers = 12, DeleteOthers = 13, AllRecordOthers = 14, singleRecordOthers = 15, GetTopRecords = 16,
            GetTopDocument = 17, GetTopOthers = 18,GetCatNameByPostQuestionId=19,GetEditDocSubjectList=20,
        };

        SqlCommand cmd = new SqlCommand();

        #region Subject Category Section

        public DataTable GetDataTable(DO_CategoryMaster objcategory, DA_CategoryMaster.CategoryMaster flag)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelCategoryMaster", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@CategoryId", SqlDbType.Int).Value = objcategory.CategoryID;
            da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objcategory.AddedBy;
            da.SelectCommand.Parameters.Add("@intPostQuestionId", SqlDbType.BigInt).Value = objcategory.intPostQuestionId;

            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;

        }

        public void AddEditDel_Category(DO_CategoryMaster objcategory, DA_CategoryMaster.CategoryMaster flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("Scrl_AddEditDelCategoryMaster", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@CategoryId", SqlDbType.Int).Value = objcategory.CategoryID;
            cmd.Parameters.Add("@CategoryName", SqlDbType.VarChar, 200).Value = objcategory.CategoryName;
            cmd.Parameters.Add("@AddedBy", SqlDbType.VarChar, 200).Value = objcategory.AddedBy;
            cmd.Parameters.Add("@ModifiedBy", SqlDbType.VarChar, 200).Value = objcategory.ModifiedBy;  
            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }

        #endregion


      }
}
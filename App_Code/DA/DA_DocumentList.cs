using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_DocumentList
/// </summary>
namespace DA_SKORKEL
{
    public class DA_DocumentList
    {
        public DA_DocumentList()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public enum DocumentList
        {
            AddCitation = 1, AddJuridiction = 2, AddCites = 3, AddCitedBy = 4, AddJudgeName = 5, GetCaseRefList = 6, GetAllCites = 7, GetTagsAll = 10, DeleteTagMRF = 11
        };

        SqlCommand cmd = new SqlCommand();


        public void AddEditDel_CaseList(DO_DocumentList objcase, DA_DocumentList.DocumentList flag)
        {

            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();
            conn = co.GetConnection();

            cmd = new SqlCommand("scrl_AddEditDeleteCaseListDocument_SP", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            cmd.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objcase.Caseid;
            cmd.Parameters.Add("@StrContentTitle", SqlDbType.VarChar, 200).Value = objcase.ContentTitle;
            cmd.Parameters.Add("@Citationid", SqlDbType.Int).Value = objcase.CitationId;
            cmd.Parameters.Add("@ContentTypeId", SqlDbType.Int).Value = objcase.ContentTypeId;
            cmd.Parameters.Add("@intuserid", SqlDbType.Int).Value = objcase.AddedBy;

            cmd.ExecuteNonQuery();
            co.CloseConnection(conn);

        }

        public DataSet GetDataSet(DO_DocumentList objDoCaseList, DocumentList flag)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_AddEditDeleteCaseListDocument_SP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objDoCaseList.Caseid;
            // da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objDoCaseList.AddedBy;
            da.SelectCommand.Parameters.Add("@ContentTypeId", SqlDbType.Int).Value = objDoCaseList.ContentTypeId;
            da.Fill(ds);
            co.CloseConnection(conn);
            return ds;
        }

        public DataTable GetDataTable(DO_DocumentList objDoCaseList, DocumentList flag)
        {

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            SQLManager co = new SQLManager();

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = new SqlCommand("scrl_AddEditDeleteCaseListDocument_SP", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
            da.SelectCommand.Parameters.Add("@CaseId", SqlDbType.BigInt).Value = objDoCaseList.Caseid;
            da.SelectCommand.Parameters.Add("@StrContentTitle", SqlDbType.VarChar, 200).Value = objDoCaseList.ContentTitle;
            // da.SelectCommand.Parameters.Add("@AddedBy", SqlDbType.BigInt).Value = objDoCaseList.AddedBy;
            da.SelectCommand.Parameters.Add("@ContentTypeId", SqlDbType.Int).Value = objDoCaseList.ContentTypeId;
            da.Fill(dt);
            co.CloseConnection(conn);
            return dt;
        }
    }
}
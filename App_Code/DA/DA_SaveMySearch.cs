using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_SaveMySearch
/// </summary>
public class DA_SaveMySearch
{
	public DA_SaveMySearch()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public enum MySearch
    {
        Add = 1, AddTempSubCat = 2, GetDocumentSubCat = 3, GetCatNameByCatID = 4, DeleteContext=5,GetJuridiction=6,GetMySavedSearches=7,GetMySavedContext=8,GetAllSavedContext=9
    };

    SqlCommand cmd = new SqlCommand();

    public void AddEditDel_Searchs(DO_SaveMySearch objSave, MySearch flag)
    {

        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();

        cmd = new SqlCommand("Scrl_AddEditDelSavedMySearch", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@intMySearchId", SqlDbType.Int).Value = objSave.intMySaveSearchId;
        cmd.Parameters.Add("@intDocId", SqlDbType.Int).Value = objSave.intDocId;
        cmd.Parameters.Add("@intDocTypeId", SqlDbType.Int).Value = objSave.intDocTypeId;
        cmd.Parameters.Add("@strSavedMyTitle", SqlDbType.VarChar, 100).Value = objSave.strSavedMyTitle;
        cmd.Parameters.Add("@strSearchQuery", SqlDbType.VarChar, 1000).Value = objSave.strSearchQuery;

        cmd.Parameters.Add("@strSearchFor", SqlDbType.VarChar, 500).Value = objSave.strSearchFor;
        cmd.Parameters.Add("@strPreSearchedIn", SqlDbType.VarChar, 200).Value = objSave.strPreSearchedIn;
        cmd.Parameters.Add("@strDocumentType", SqlDbType.VarChar, 150).Value = objSave.strDocumentType;
        cmd.Parameters.Add("@strLookInto", SqlDbType.VarChar, 50).Value = objSave.strLookInto;
        cmd.Parameters.Add("@strAdvSearchTitle", SqlDbType.VarChar, 500).Value = objSave.strAdvSearchTitle;
        cmd.Parameters.Add("@strAdvJuridiction", SqlDbType.VarChar, 100).Value = objSave.strAdvJuridiction;
        cmd.Parameters.Add("@strAdvCitation", SqlDbType.VarChar, 100).Value = objSave.strAdvCitation;
        cmd.Parameters.Add("@strAdvDateFrom", SqlDbType.VarChar, 10).Value = objSave.strAdvDateFrom;
        cmd.Parameters.Add("@strAdvDateTo", SqlDbType.VarChar, 10).Value = objSave.strAdvDateTo;
        cmd.Parameters.Add("@strAdvProvision", SqlDbType.VarChar, 100).Value = objSave.strAdvProvision;
        cmd.Parameters.Add("@strAdvPartyName", SqlDbType.VarChar, 100).Value = objSave.strAdvPartyName;
        cmd.Parameters.Add("@strAdvBench", SqlDbType.VarChar, 100).Value = objSave.strAdvBench;
        cmd.Parameters.Add("@strAdvJudgeName", SqlDbType.VarChar, 150).Value = objSave.strAdvJudgeName;
        cmd.Parameters.Add("@strContextId", SqlDbType.VarChar, 150).Value = objSave.strContextId;

        cmd.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 20).Value = objSave.strIpAddress;
        cmd.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objSave.intSubjectCategoryId;
        cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objSave.intAddedBy;
        cmd.Parameters.Add("@intSearchResultCount", SqlDbType.Int).Value = objSave.intSearchResultCount;

        cmd.ExecuteNonQuery();
        co.CloseConnection(conn);

    }

    public DataTable GetDataTable(DO_SaveMySearch objSave, MySearch flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelSavedMySearch", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@intMySearchId", SqlDbType.Int).Value = objSave.intMySaveSearchId;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objSave.intAddedBy;
        da.SelectCommand.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objSave.intSubjectCategoryId;
        da.SelectCommand.Parameters.Add("@Ids", SqlDbType.VarChar, 150).Value = objSave.Ids;
        da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = objSave.PageSize;
        da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = objSave.Currentpage;                                      
        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }
}
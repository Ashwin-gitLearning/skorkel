using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Services;
using SqlConn;

/// <summary>
/// Summary description for autoComplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class autoCompleteSkorkelSearch : System.Web.Services.WebService
{

    public autoCompleteSkorkelSearch()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    public enum Search
    {
        All = 1, Allfilter = 2, CaseList = 3, ArticleList = 4, BlogList = 5, QuestionList = 6,
        LexipediaList = 7, ParlimentList = 8, StausList = 9, TestimoniesList = 10
    };


    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod(EnableSession = true)]
    public string[] GetSkorkelSearchList(string prefixText, int count)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();

        da.SelectCommand = new SqlCommand("Scrl_SkorkelSearch", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        // da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 1;
        da.SelectCommand.Parameters.Add("@SearchTitle", SqlDbType.VarChar, 200).Value = prefixText;
        // da.SelectCommand.Parameters.Add("@intuserid", SqlDbType.Int).Value = Convert.ToInt32(Session["CompanyID"]);
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["SearchTitle"].ToString(), i);
            i++;
        }
        Application["SearchConetentList"] = dt;
        return items.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public DataTable GetSkorkelSearchData(int CurrentPage, int CurrentPageSize, string prefixText, int Year, Search flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        int CTId = 0;
        if (Session["SessionSearchCTId"] != null)
        {
            CTId = Convert.ToInt32(Session["SessionSearchCTId"]);
        }
        else
        {
            CTId = 0;
        }
        string Court = "";
        if (Session["CourtName"] != null)
        {
            Court = Convert.ToString(Session["CourtName"]);
        }
        else
        {
            Court = "";
        }
        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();

        da.SelectCommand = new SqlCommand("Scrl_Search", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@SearchTitle", SqlDbType.VarChar, 200).Value = prefixText;
        da.SelectCommand.Parameters.Add("@Jurisdiction", SqlDbType.VarChar, 200).Value = Court;
        da.SelectCommand.Parameters.Add("@ContentTypeID", SqlDbType.Int).Value = CTId;
        da.SelectCommand.Parameters.Add("@year", SqlDbType.Int).Value = Year;
        da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = CurrentPage;
        da.SelectCommand.Parameters.Add("@pagesize", SqlDbType.Int).Value = CurrentPageSize;
        da.SelectCommand.CommandTimeout = 0;
        da.Fill(dt);
        return dt;
    }
}

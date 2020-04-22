using System.Linq;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using SqlConn;
using System;

/// <summary>
/// Summary description for autoComplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class autoComplete : System.Web.Services.WebService
{

    public autoComplete()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod(EnableSession = true)]
    public string[] GetContentTitleList(string prefixText, int count)
    {

        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();

        da.SelectCommand = new SqlCommand("Scrl_CommonFunctions", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 1;
        da.SelectCommand.Parameters.Add("@StrContentTitle", SqlDbType.VarChar, 200).Value = prefixText;
        // da.SelectCommand.Parameters.Add("@intuserid", SqlDbType.Int).Value = Convert.ToInt32(Session["CompanyID"]);
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["Title"].ToString(), i);
            i++;
        }
        Application["ContentTitleList"] = dt;
        return items.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public string[] GetDocNameList(string prefixText, int count)
    {

        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();

        da.SelectCommand = new SqlCommand("Scrl_CommonFunctions", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 8;
        da.SelectCommand.Parameters.Add("@StrContentTitle", SqlDbType.VarChar, 200).Value = prefixText;
        // da.SelectCommand.Parameters.Add("@intuserid", SqlDbType.Int).Value = Convert.ToInt32(Session["CompanyID"]);
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["strDocTitle"].ToString(), i);
            i++;
        }
        Application["ContentDocList"] = dt;
        return items.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public string[] GetJudgeNameList(string prefixText, int count)
    {

        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();

        da.SelectCommand = new SqlCommand("Scrl_CommonFunctions", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 2;
        da.SelectCommand.Parameters.Add("@StrContentTitle", SqlDbType.VarChar, 200).Value = prefixText;
        // da.SelectCommand.Parameters.Add("@intuserid", SqlDbType.Int).Value = Convert.ToInt32(Session["CompanyID"]);
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["JudgeName"].ToString(), i);
            i++;
        }
        Application["JudgeNameList"] = dt;
        return items.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public string[] GetCourtNameList(string prefixText, int count)
    {

        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();

        da.SelectCommand = new SqlCommand("Scrl_CommonFunctions", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 3;
        da.SelectCommand.Parameters.Add("@StrContentTitle", SqlDbType.VarChar, 200).Value = prefixText;
        // da.SelectCommand.Parameters.Add("@intuserid", SqlDbType.Int).Value = Convert.ToInt32(Session["CompanyID"]);
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["CourtName"].ToString(), i);
            i++;
        }
        Application["CourtNameList"] = dt;
        return items.ToArray();
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
    public string[] GetTagNameList(string prefixText, int count)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();

        da.SelectCommand = new SqlCommand("Scrl_AddEditDelContentTagDefinition", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 5;
        da.SelectCommand.Parameters.Add("@TagDefinition", SqlDbType.VarChar, 200).Value = prefixText;
        // da.SelectCommand.Parameters.Add("@intuserid", SqlDbType.Int).Value = Convert.ToInt32(Session["CompanyID"]);
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["TagDefinition"].ToString(), i);
            i++;
        }
        Application["TagNameList"] = dt;
        return items.ToArray();
    }


    [WebMethod(EnableSession = true)]
    public string[] GetPrivacySettings(string prefixText, int count)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        //if (prefixText.Contains("@"))
        //{

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();

        da.SelectCommand = new SqlCommand("Scrl_AddEditDelPrivacySettings", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //da.SelectCommand.Parameters.Add("@intPrivacyId", SqlDbType.Int).Value = 0;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 4;
        //da.SelectCommand.Parameters.Add("@intSubPrivacyId", SqlDbType.Int).Value = 0;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = Convert.ToInt32(Session["ExternalUserId"]);
        //da.SelectCommand.Parameters.Add("@strIpAddress", SqlDbType.VarChar, 200).Value = "";
        da.SelectCommand.Parameters.Add("@txtSearchText", SqlDbType.VarChar, 500).Value = prefixText;
        // da.SelectCommand.Parameters.Add("@intuserid", SqlDbType.Int).Value = Convert.ToInt32(Session["CompanyID"]);
        da.Fill(dt);
        // }
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["Name"].ToString(), i);
            i++;
        }
        Application["ConnectedUserList"] = dt;

        //items[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Data(s) Found.", "0");
        return items.ToArray();

    }

    [WebMethod(EnableSession = true)]
    public string[] GetInstituteName(string prefixText, int count)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();

        da.SelectCommand = new SqlCommand("Scrl_UserAdvanceSearch", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        //da.SelectCommand.Parameters.Add("@intPrivacyId", SqlDbType.Int).Value = 0;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 8;
        //da.SelectCommand.Parameters.Add("@intSubPrivacyId", SqlDbType.Int).Value = 0;
        //da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = Convert.ToInt32(Session["ExternalUserId"]);
        //da.SelectCommand.Parameters.Add("@strIpAddres", SqlDbType.VarChar, 200).Value = "";
        da.SelectCommand.Parameters.Add("@strInstituteName", SqlDbType.VarChar, 500).Value = prefixText;
        // da.SelectCommand.Parameters.Add("@intuserid", SqlDbType.Int).Value = Convert.ToInt32(Session["CompanyID"]);
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["strsearch"].ToString(), i);
            i++;
        }
        Application["InstituteList"] = dt;
        co.CloseConnection(conn);
        return items.ToArray();

    }

    [WebMethod(EnableSession = true)]
    public string[] GetSubjectNameList(string prefixText, int count)
    {

        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();

        da.SelectCommand = new SqlCommand("Scrl_CommonFunctions", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 7;
        da.SelectCommand.Parameters.Add("@StrContentTitle", SqlDbType.VarChar, 200).Value = prefixText;

        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["SubjectName"].ToString(), i);
            i++;
        }
        Application["SubjectNameList"] = dt;
        return items.ToArray();
    }

    [WebMethod(EnableSession = true)]
    public string[] GetUserListOnComment(string prefixText, int count)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        if (prefixText.Contains("@"))
        {
            int j = prefixText.IndexOf(@"@");

            string newString = prefixText.Substring(j + 1);

            conn = co.GetConnection();
            SqlDataAdapter da = new SqlDataAdapter();

            da.SelectCommand = new SqlCommand("Scrl_AddEditDelGroupJoin", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = 200;
            da.SelectCommand.Parameters.Add("@Currentpage", SqlDbType.Int).Value = 1;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 6;
            da.SelectCommand.Parameters.Add("@inGroupId", SqlDbType.Int).Value = Convert.ToInt32(Session["intGroupId"]);
            da.SelectCommand.Parameters.Add("@txtSearch", SqlDbType.VarChar, 500).Value = newString;
            da.Fill(dt);
            Application["ConnectedUserListOnComment"] = dt;
        }

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["Name"].ToString(), i);
            i++;
        }

        // Session["ConnectedUserListOnComment"] = dt;

        //items[i] = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem("No Data(s) Found.", "0");
        return items.ToArray();

    }

    [WebMethod(EnableSession = true)]
    public string[] GetRatioNameList(string prefixText, int count, string contextKey)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();

        //string words = "This is a list of words, with: a bit of punctuation" +
        //               "\tand a tab character.";
        string caseid = "", contenttypeid = "", tagid = "";
        string[] items = new string[0];
        try
        {
            string words = contextKey;
            if (!string.IsNullOrEmpty(contextKey))
            {
                string[] split = words.Split(new Char[] { ' ', '#' });

                caseid = split[0];
                contenttypeid = split[1];
                tagid = split[2];

            }
            da.SelectCommand = new SqlCommand("Scrl_AddEditDelRatio", conn);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 5;
            da.SelectCommand.Parameters.Add("@intDocId", SqlDbType.BigInt).Value = caseid;
            da.SelectCommand.Parameters.Add("@intDocTypeId", SqlDbType.Int).Value = contenttypeid;
            da.SelectCommand.Parameters.Add("@intTagType", SqlDbType.Int).Value = tagid;
            da.Fill(dt);
            items = new string[dt.Rows.Count];
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                items.SetValue(dr["strRatioTitle"].ToString(), i);
                i++;
            }

            Application["RatioTitle"] = dt;
        }
        catch
        {

        }
        return items.ToArray();

    }


    [WebMethod(EnableSession = true)]
    public string[] GetJuridictionList(string prefixText, int count)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();

        da.SelectCommand = new SqlCommand("Scrl_AddEditDelSavedMySearch", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = 6;
        da.SelectCommand.Parameters.Add("@strSavedMyTitle", SqlDbType.VarChar, 200).Value = prefixText;
        // da.SelectCommand.Parameters.Add("@intuserid", SqlDbType.Int).Value = Convert.ToInt32(Session["CompanyID"]);
        da.Fill(dt);
        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["ContentTitle"].ToString(), i);
            i++;
        }
        Application["JuridictionName"] = dt;
        return items.ToArray();
    }
}

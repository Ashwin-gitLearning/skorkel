using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SqlConn;

/// <summary>
/// Summary description for DA_Scrl_UserNewsListing
/// </summary>
public class DA_Scrl_UserNewsListing
{
    private SqlCommand cmd;
    public DA_Scrl_UserNewsListing()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public enum NewsListing
    {
        GetNewsListing = 1, AddNews = 2, RssNews = 3, GetRssListing = 4, DeleteRssByID = 5, SingleRecord = 6, UpdateRSSByID = 7, SingleNewsRecord = 8,
        UpdateRSSUser = 9, UpdateNews = 10, GetNewsListingUser = 11, GetNewsDtls = 12, GetTop5NewsListingUser = 13, DeleteNewsByID = 15, RSSCount = 16,
        CheckingRSSEdit = 17, GetNewsListingUS = 18
    }

    public DataTable GetDataTable(DO_Scrl_UserNewsListing objcategory, NewsListing flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_NewsListing", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;

        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@CurrentPage", SqlDbType.Int).Value = objcategory.CurrentPage;
        da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = objcategory.CurrentPageSize;
        if (Convert.ToString(objcategory.ID) != "")
        {
            da.SelectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = objcategory.ID;
        }        

        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }

    public DataTable GetNewsDataTable(DO_Scrl_UserNewsListing objcategory, NewsListing flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_NewsListing", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;

        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        //da.SelectCommand.Parameters.Add("@CurrentPage", SqlDbType.Int).Value = objcategory.CurrentPage;
        //da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = objcategory.CurrentPageSize;
        //if (Convert.ToString(objcategory.ID) != "")
        //{
        //    da.SelectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = objcategory.ID;
        //}

        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }

    public int AddEditDel_NewsListing(DO_Scrl_UserNewsListing objcategory, NewsListing flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_NewsListing", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        //cmd.Parameters.Add("@intBlogId", SqlDbType.Int).Value = objBlog.intBlogId;
        cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 200).Value = objcategory.Title;
        cmd.Parameters.Add("@strType", SqlDbType.VarChar, 100).Value = objcategory.Type;
        cmd.Parameters.Add("@strLink", SqlDbType.VarChar, 200).Value = objcategory.Source_link;
        cmd.Parameters.Add("@strContent", SqlDbType.VarChar, 100000000).Value = objcategory.Content;
        //cmd.Parameters.Add("@strBlogPicture", SqlDbType.VarChar, 100).Value = objBlog.strBlogPicture;
        //cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objBlog.intAddedBy;
        //cmd.Parameters.Add("@strIPAddress", SqlDbType.VarChar, 20).Value = objBlog.strIPAddress;

        //cmd.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objBlog.intSubjectCategoryId;
        //cmd.Parameters.Add("@intModifiedBy", SqlDbType.VarChar, 200).Value = objBlog.intModifiedBy;
        //cmd.ExecuteReader();
        objcategory.intNewsOutId = Convert.ToInt32(cmd.ExecuteScalar());
        co.CloseConnection(conn);
        return (objcategory.intNewsOutId);
    }

    public int Add_RssListing(DO_Scrl_UserNewsListing objcategory, NewsListing flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_NewsListing", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 2000).Value = objcategory.Title;
        cmd.Parameters.Add("@strLink", SqlDbType.VarChar, 200).Value = objcategory.Source_link;
        //cmd.Parameters.Add("@strContent", SqlDbType.VarChar, 100000000).Value = objcategory.Content;
        //cmd.Parameters.Add("@publishTimestamp", SqlDbType.DateTime).Value = objcategory.Published_Timestamp;

        objcategory.intNewsOutId = Convert.ToInt32(cmd.ExecuteScalar());
        co.CloseConnection(conn);
        return objcategory.intNewsOutId;
    }

    public int Checking_RssListing(DO_Scrl_UserNewsListing objcategory, NewsListing flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_NewsListing", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        //cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 2000).Value = objcategory.Title;
        cmd.Parameters.Add("@strLink", SqlDbType.VarChar, 200).Value = objcategory.Source_link;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = objcategory.ID;
        //cmd.Parameters.Add("@strContent", SqlDbType.VarChar, 100000000).Value = objcategory.Content;
        //cmd.Parameters.Add("@publishTimestamp", SqlDbType.DateTime).Value = objcategory.Published_Timestamp;

        objcategory.intNewsOutId = Convert.ToInt32(cmd.ExecuteScalar());
        co.CloseConnection(conn);
        return objcategory.intNewsOutId;
    }

    public void Edit_RssListing(DO_Scrl_UserNewsListing objcategory, NewsListing flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_NewsListing", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = objcategory.ID;
        cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 2000).Value = objcategory.Title;
        cmd.Parameters.Add("@strLink", SqlDbType.VarChar, 200).Value = objcategory.Source_link;
        
        cmd.ExecuteReader();
        co.CloseConnection(conn);
    }
    public void DelByID_RssListing(DO_Scrl_UserNewsListing objcategory, NewsListing flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_NewsListing", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = objcategory.ID;
       
        cmd.ExecuteReader();
        co.CloseConnection(conn);
    }

    public void Edit_RssUserListing(DO_Scrl_UserNewsListing objcategory, NewsListing flag, Boolean chkTgl)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_NewsListing", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@checkNews", SqlDbType.Bit).Value = chkTgl;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = objcategory.ID;

        cmd.ExecuteReader();
        co.CloseConnection(conn);
    }
    public void Edit_NewsListing(DO_Scrl_UserNewsListing objcategory, NewsListing flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_NewsListing", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        //cmd.Parameters.Add("@intBlogId", SqlDbType.Int).Value = objBlog.intBlogId;
        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = objcategory.ID;
        cmd.Parameters.Add("@strTitle", SqlDbType.VarChar, 20000).Value = objcategory.Title;
        cmd.Parameters.Add("@strType", SqlDbType.VarChar, 100).Value = objcategory.Type;
        cmd.Parameters.Add("@strLink", SqlDbType.VarChar, 200).Value = objcategory.Source_link;
        cmd.Parameters.Add("@strContent", SqlDbType.VarChar, 100000000).Value = objcategory.Content;
        
        cmd.ExecuteReader();
        co.CloseConnection(conn);
    }

    public DataTable GetDataTableNews(DO_Scrl_UserNewsListing objcategory, DA_Scrl_UserNewsListing.NewsListing flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_NewsListing", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@NewsIds", SqlDbType.VarChar, 200).Value = objcategory.NewsIdList;
        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }
}
using System.Data;
using System.Data.SqlClient;
using SqlConn;
using System;

/// <summary>
/// Summary description for DA_NewBlogs
/// </summary>
public class DA_NewBlogs
{
    private SqlCommand cmd; 
	public DA_NewBlogs()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public enum Blog
    {
        Add = 1, AddTempSubCat = 2, GetDocumentSubCat = 3, GetCatNameByCatID = 4, ADDSelectedSubCatId = 5, GetAllBlogs = 6, RemoveTempSubCat = 7, GetArchives = 8, GetAllBlogsPost = 9, GetAllBlogsDetails = 10,
        DeleteContext = 11, GetSearchResult = 12, GetBlogsById = 13, GetProducedBlog = 14, AddConsumedBlog = 15, GetConsumedBlogs = 16, GetTotalBlogCount = 17, GetAllBlogsDate=18,DeleteBlog=19,GetAllBlogss=20,
        GetLikeUsers = 21, GetBlogss = 22, GetRelatedSubject = 23, GetRelatedSubjects = 24, Update = 25, DeleteSelectedSubCatId = 26,GetSubjectListById=27,GetBlogsEdit=28, GetBlogListing = 29
    }
    public enum BlogCommnet
    {
        Add = 1, GetByAddedby = 2, GetByBlog = 3, GetTop4ByBlog = 4, GetLikeUsers=5,GetCommentById=6,Update=7,Delete=8,
    }

    public void AddEditDel_NewBlog(DO_NewBlogs objBlog, Blog flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_AddEditDelNewBlog", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@intBlogId", SqlDbType.Int).Value = objBlog.intBlogId;
        cmd.Parameters.Add("@strBlogHeading", SqlDbType.VarChar, 20000).Value = objBlog.strBlogHeading;
        cmd.Parameters.Add("@strBlogTitle", SqlDbType.VarChar,100000000).Value = objBlog.strBlogTitle;
        cmd.Parameters.Add("@strBlogPicture", SqlDbType.VarChar, 100).Value = objBlog.strBlogPicture;
        cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objBlog.intAddedBy;
        cmd.Parameters.Add("@strIPAddress", SqlDbType.VarChar,20).Value = objBlog.strIPAddress;
              
        cmd.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objBlog.intSubjectCategoryId;
        cmd.Parameters.Add("@intModifiedBy", SqlDbType.VarChar, 200).Value = objBlog.intModifiedBy;
        objBlog.intOutId = Convert.ToInt32(cmd.ExecuteScalar());
        co.CloseConnection(conn);

       
    }

    public DataTable GetDataTable(DO_NewBlogs objBlog, Blog flag)
    {
        DataTable dt = new DataTable();

        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelNewBlog", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@intBlogId", SqlDbType.Int).Value = objBlog.intBlogId;
        da.SelectCommand.Parameters.Add("@strBlogTitle", SqlDbType.VarChar, 1000).Value = objBlog.strBlogTitle;
        da.SelectCommand.Parameters.Add("@strBlogPicture", SqlDbType.VarChar, 100).Value = objBlog.strBlogPicture;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objBlog.intAddedBy;
        da.SelectCommand.Parameters.Add("@strIPAddress", SqlDbType.VarChar, 20).Value = objBlog.strIPAddress;
        da.SelectCommand.Parameters.Add("@intSubjectCategoryId", SqlDbType.Int).Value = objBlog.intSubjectCategoryId;
        da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.VarChar, 200).Value = objBlog.intModifiedBy;
        da.SelectCommand.Parameters.Add("@Ids", SqlDbType.VarChar, 50).Value = objBlog.ID;
        da.SelectCommand.Parameters.Add("@txtSearch", SqlDbType.VarChar, 500).Value = objBlog.strSearch;
        da.SelectCommand.Parameters.Add("@CurrentPage", SqlDbType.Int).Value = objBlog.CurrentPage;
        da.SelectCommand.Parameters.Add("@PageSize", SqlDbType.Int).Value = objBlog.CurrentPageSize;
      
        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }



    //Mohsin faras(04/01/2014)
    public void AddEditDel_NewBlogComment(DO_NewBlogs objBlog, BlogCommnet flag)
    {
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();
        conn = co.GetConnection();
        cmd = new SqlCommand("Scrl_AddEditDelNewBlogComments", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        cmd.Parameters.Add("@intCommentId", SqlDbType.Int).Value = objBlog.intCommentId;
        cmd.Parameters.Add("@intBlogId", SqlDbType.Int).Value = objBlog.intBlogId;
        cmd.Parameters.Add("@strComment", SqlDbType.VarChar, 1000).Value = objBlog.strComment;
        cmd.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objBlog.intAddedBy;
        cmd.Parameters.Add("@strIPAddress", SqlDbType.VarChar, 20).Value = objBlog.strIPAddress;
        cmd.Parameters.Add("@intModifiedBy", SqlDbType.VarChar, 200).Value = objBlog.intModifiedBy;
        objBlog.intOutId = Convert.ToInt32(cmd.ExecuteScalar());
        co.CloseConnection(conn);


    }

    public DataTable GetDataTableComment(DO_NewBlogs objBlog, BlogCommnet flag)
    {
        DataTable dt = new DataTable();

        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelNewBlogComments", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@intCommentId", SqlDbType.Int).Value = objBlog.intCommentId;
        da.SelectCommand.Parameters.Add("@intBlogId", SqlDbType.Int).Value = objBlog.intBlogId;
        da.SelectCommand.Parameters.Add("@strComment", SqlDbType.VarChar, 1000).Value = objBlog.strComment;
        da.SelectCommand.Parameters.Add("@intAddedBy", SqlDbType.Int).Value = objBlog.intAddedBy;
        da.SelectCommand.Parameters.Add("@strIPAddress", SqlDbType.VarChar, 20).Value = objBlog.strIPAddress;
        da.SelectCommand.Parameters.Add("@intModifiedBy", SqlDbType.VarChar, 200).Value = objBlog.intModifiedBy;
        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }

    public DataTable GetDataTableBlogs(DO_NewBlogs objBlog, DA_NewBlogs.Blog flag)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection();
        SQLManager co = new SQLManager();

        conn = co.GetConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = new SqlCommand("Scrl_AddEditDelNewBlog", conn);
        da.SelectCommand.CommandType = CommandType.StoredProcedure;
        da.SelectCommand.Parameters.Add("@FlagNo", SqlDbType.Int).Value = flag;
        da.SelectCommand.Parameters.Add("@BlogsIds", SqlDbType.VarChar, 200).Value = objBlog.BlogsIdList;
        da.Fill(dt);
        co.CloseConnection(conn);
        return dt;
    }
}
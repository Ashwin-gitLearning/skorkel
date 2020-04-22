using System;

/// <summary>
/// Summary description for DO_NewBlogs
/// </summary>
public class DO_NewBlogs
{
	public DO_NewBlogs()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Int32 intBlogId { get; set; } 
    public string strBlogTitle { get; set; }
    public string strBlogPicture { get; set; }
    public Int32 intAddedBy { get; set; }
    public string strIPAddress { get; set; }  
    public Int32 intSubjectCategoryId { get; set; }
    public Int32 intModifiedBy { get; set; }
    public Int32 intOutId { get; set; }
    public int DocId { get; set; }
    public string strSearch { get; set; }
    public string ID { get; set; }
    public string strBlogHeading { get; set; }
    public int CurrentPage { get; set; }
    public int CurrentPageSize { get; set; }
    //Mohsin Faras(4/1/2014)
    public Int32 intCommentId { get; set; }
    public string strComment { get; set; }
    public string BlogsIdList { get; set; }
}
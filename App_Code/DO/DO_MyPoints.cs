using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_MyPoints
/// </summary>
public class DO_MyPoints
{
	public DO_MyPoints()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Variable Decleration

    public Int64 intDocRecentActId { get; set; }
    public Int64 intContentId { get; set; }
    public string strDescrption { get; set; }
    public Int64 addedby { get; set; }
    public Int64 intContentTypeId { get; set; }
    public Int64 intTagType { get; set; }
    public int UserID { get; set; }
    #endregion

}
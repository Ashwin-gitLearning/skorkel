using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_ResearchResult
/// </summary>
public class DO_ResearchResult
{
	public DO_ResearchResult()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Int64 RatingId { get; set; }
    public Int64 ContentId { get; set; }
    public Int64 addedby { get; set; }
    public Int64 ContentTypeID { get; set; }
    public string SerchKey { get; set; }

}
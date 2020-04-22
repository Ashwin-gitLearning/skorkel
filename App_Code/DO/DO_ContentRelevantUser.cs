using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_ContentRelevantUser
/// </summary>
public class DO_ContentRelevantUser
{
	public DO_ContentRelevantUser()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Int64 LinkId { get; set; }
    public Int64 ContentId { get; set; }
    public string LinkUrl { get; set; }
    public Int64 AddedBy { get; set; }
    public Int64 ContentTypeID { get; set; }
    public Int64 CaseId { get; set; }
    public int intViewId { get; set; }
    public int intDocAddedBy { get; set; }
}
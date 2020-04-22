using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_ViewActivity
/// </summary>
public class DO_ViewActivity
{
	public DO_ViewActivity()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int RegistrationId { get; set; }
    public string ActivityDate { get; set; }   
    public string CurrentPage { get; set; }
    public string CurrentPageSize { get; set; }

    public int intActivityId { get; set; }
     public int intID { get; set; }
    public string strRepLiShStatus { get; set; }
    public string strTitle { get; set; }
    public string strMessage { get; set; }
     public string intAddedBy { get; set; }
     public string strIpAddress { get; set; }
    public string strTableName { get; set; }



}
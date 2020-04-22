using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_WallShare
/// </summary>
public class DO_WallShare
{
	public DO_WallShare()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public String strMessage { get; set; }
    public String strLink { get; set; }
    public String strInvitee { get; set; }
    public int intStatusUpdateId { get; set; }
    public int intAddedBy { get; set; }
    public String strIPAddress { get; set; }
}
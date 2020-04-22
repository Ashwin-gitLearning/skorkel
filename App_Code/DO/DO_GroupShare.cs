using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_GroupShare
/// </summary>
public class DO_GroupShare
{
	public DO_GroupShare()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public String strMessage { get; set; }
    public String strLink { get; set; }
    public String strInvitee { get; set; }
    public int intGroupId { get; set; }
    public int intAddedBy { get; set; }  
    public String strIPAddress { get; set; }

    //Orgnisation Group Share
    public int intOrgnisationID { get; set; }
       
}
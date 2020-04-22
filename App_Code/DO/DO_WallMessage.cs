using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_WallMessage
/// </summary>
public class DO_WallMessage
{
	public DO_WallMessage()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    private int _intMessageId;
    private int _intRegistrationId;
    private string _StrRecommendation;
    private int _intInvitedUserId;
    private DateTime _dtAddedOn;
    private int _intAddedBy;
    private DateTime _dtModifiedOn;
    private int _intModifiedBy;
    private string _strIpAddress;

    public int intMessageId { get { return _intMessageId; } set { _intMessageId = value; } }
    public int intRegistrationId { get { return _intRegistrationId; } set { _intRegistrationId = value; } }
    public string StrRecommendation { get { return _StrRecommendation; } set { _StrRecommendation = value; } }
    public int intInvitedUserId { get { return _intInvitedUserId; } set { _intInvitedUserId = value; } }
    public DateTime dtAddedOn { get { return _dtAddedOn; } set { _dtAddedOn = value; } }
    public int intAddedBy { get { return _intAddedBy; } set { _intAddedBy = value; } }
    public DateTime dtModifiedOn { get { return _dtModifiedOn; } set { _dtModifiedOn = value; } }
    public int intModifiedBy { get { return _intModifiedBy; } set { _intModifiedBy = value; } }
    public string strIpAddress { get { return _strIpAddress; } set { _strIpAddress = value; } }

    public int intGroupId { get; set; }
    public string strSubject { get; set; }
    public int OrgId { get; set; }

    public int intSkillId { get; set; }

    public int intOutWallMessageId { get; set; }
    public string strTotalGrpMemberID { get; set; }
    public string striInvitedUserId { get; set; }

    
}
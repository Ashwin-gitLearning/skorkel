using System;

/// <summary>
/// Summary description for DO_Comment
/// </summary>
public class DO_ContentSummary
{
    public DO_ContentSummary()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Variable Decleration
    
    public Int64 SummaryId { get; set; }
    public Int64 ContentId { get; set; }
    //public string LinkUrl { get; set; }
    public Int64 addedby { get; set; }
    public Int64 ContentTypeID { get; set; }
    public String SummaryText { get; set; }

    public int intDocRecentActId { get; set; }
    public String strDescrption { get; set; }
    public String strIPAddress { get; set; }
    public int intTagType { get; set; }
    public int intUserTypeId { get; set; }
    public int PointId { get; set; }

    #endregion
}
using System;

/// <summary>
/// Summary description for DO_DocumentSummary
/// </summary>
public class DO_DocumentSummary
{
	public DO_DocumentSummary()
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

    #endregion
}
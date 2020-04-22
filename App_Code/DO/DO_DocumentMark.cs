using System;

/// <summary>
/// Summary description for DO_DocumentMark
/// </summary>
public class DO_DocumentMark
{
	public DO_DocumentMark()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Variable Decleration
    public Int64 Markid { get; set; }
    public Int64 CaseId { get; set; }
    public string Markcontent { get; set; }
    public Int64 addedby { get; set; }
    public Int32 ContentTypeID { get; set; }
    public Int64 StartIndex { get; set; }
    public Int64 EndIndex { get; set; }

    #endregion
}
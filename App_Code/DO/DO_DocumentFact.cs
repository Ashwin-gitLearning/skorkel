using System;

/// <summary>
/// Summary description for DO_DocumentFact
/// </summary>
public class DO_DocumentFact
{
	public DO_DocumentFact()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Variable Decleration

    public Int64 FactId { get; set; }
    public Int64 ContentId { get; set; }
    //public string LinkUrl { get; set; }
    public Int64 addedby { get; set; }
    public Int64 ContentTypeID { get; set; }
    public String FactText { get; set; }
    public Int64 StartIndex { get; set; }
    public Int64 EndIndex { get; set; }

    #endregion
}
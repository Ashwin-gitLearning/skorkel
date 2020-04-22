using System;

/// <summary>
/// Summary description for DO_Comment
/// </summary>
public class DO_ContentTagDef
{
    public DO_ContentTagDef()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Variable Decleration
    
    public Int64 TagDefId { get; set; }
    public Int64 ContentId { get; set; }
    //public string LinkUrl { get; set; }
    public Int64 addedby { get; set; }
    public Int64 ContentTypeID { get; set; }
    public String TaggedText { get; set; }
    public String TagDef { get; set; }
    public Int64 TagDefOutId { get; set; }
    public int intTagType { get; set; }
    public Int64 StartIndex { get; set; }
    public Int64 EndIndex { get; set; }
    #endregion
}
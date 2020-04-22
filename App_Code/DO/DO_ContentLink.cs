using System;

/// <summary>
/// Summary description for DO_Comment
/// </summary>
public class DO_ContentLink
{
    public DO_ContentLink()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Variable Decleration
    
    public Int64 LinkId { get; set; }
    public Int64 ContentId { get; set; }
    public string LinkUrl { get; set; }
    public Int64 addedby { get; set; }
    public Int64 ContentTypeID { get; set; }
    public string LinkedText { get; set; }
    public string strLinkedText { get; set; }
    public Int64 LinkedContentId { get; set; }
    public Int64 LinkedContentTypeId { get; set; }
    public Int64 StartIndex { get; set; }
    public Int64 EndIndex { get; set; }
    public int intTagType { get; set; }
    #endregion
}
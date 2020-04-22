using System;

/// <summary>
/// Summary description for DO_Comment
/// </summary>
public class DO_ContentRatio
{
    public DO_ContentRatio()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Variable Decleration
    
    public Int64 RatioId { get; set; }
    public Int64 ContentId { get; set; }
    //public string LinkUrl { get; set; }
    public Int64 addedby { get; set; }
    public Int64 ContentTypeID { get; set; }
    public String RatioText { get; set; }
    public Int64 StartIndex { get; set; }
    public Int64 EndIndex { get; set; }

    #endregion
}
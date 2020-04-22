using System;

/// <summary>
/// Summary description for DO_Mark
/// </summary>
public class DO_Mark
{
	public DO_Mark()
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
    public Int32 intSubjectCategoryId { get; set; }
    public Int32 ModifiedBy { get; set; }
    public Int32 DocId { get; set; }
    public string strImpParagraph { get; set; }
    public int intTagType { get; set; }
    
    #endregion
}
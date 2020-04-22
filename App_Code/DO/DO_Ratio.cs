using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_Ratio
/// </summary>
public class DO_Ratio
{
	public DO_Ratio()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    #region Variable Decleration

    public Int32 intRatioId { get; set; }
    public Int64 CaseId { get; set; }
    public Int64 ContentTypeID { get; set; }
    public string strRatioTitle { get; set; }
    public Int64 intTagType { get; set; }
    public string strTagDescription { get; set; }
    public Int64 StartIndex { get; set; }
    public Int64 EndIndex { get; set; }
    public Int32 intAddedBy { get; set; }
    public Int32 intModifiedBy { get; set; }
    public string strIpAddress { get; set; }
    public Int32 intParentId { get; set; }
    public string strNewRatioTitle { get; set; }

    #endregion
}
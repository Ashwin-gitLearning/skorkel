using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_LogDetails
/// </summary>
public class DO_LogDetails
{
    public DO_LogDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int LogId { get; set; }
    public int SectionId { get; set; }
    public int intActionId { get; set; }
    public string strAction { get; set; }
    public int intAddedBy { get; set; }
    public string dtAddedOn { get; set; }
    public int intDeletedBy { get; set; }
    public string dtDeletedOn { get; set; }
    public string strIPAddress { get; set; }
    public string SectionName { get; set; }
    public string CurrentPage { get; set; }
    public string CurrentPageSize { get; set; }
    public string strActionName { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_Scrl_JobsListing
/// </summary>
public class DO_Scrl_JobsListing
{
    public DO_Scrl_JobsListing()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public string JobType { get; set; }
    public string StartingSalary { get; set; }
    public string EndingSalary { get; set; }
    public string StartDuration { get; set; }
    public string EndDuration { get; set; }
    public string Status { get; set; }
    public int CurrentPage { get; set; }
    public int CurrentPageSize { get; set; }
    public int intJobsOutId { get; set; }

}
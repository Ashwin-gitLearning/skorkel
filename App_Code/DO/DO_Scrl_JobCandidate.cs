using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_Scrl_JobCandidate
/// </summary>
public class DO_Scrl_JobCandidate
{
    public DO_Scrl_JobCandidate()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int ID { get; set; }
    public int Job_ID { get; set; }
    public int User_ID { get; set; }
    public string Resume_file_title { get; set; }
    public string Resume_path { get; set; }
    public int intJobsOutId { get; set; }
    public int CurrentPage { get; set; }
    public int CurrentPageSize { get; set; }
}
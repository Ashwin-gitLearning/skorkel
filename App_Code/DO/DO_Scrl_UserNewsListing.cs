using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_Scrl_UserNewsListing
/// </summary>
public class DO_Scrl_UserNewsListing
{
    public DO_Scrl_UserNewsListing()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public int ID { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Source_link { get; set; }
    public string Photo_path { get; set; }
    public string Content { get; set; }
    public string Status { get; set; }
    public int CurrentPage { get; set; }
    public int CurrentPageSize { get; set; }
    public DateTime Published_Timestamp { get; set; }
    public int intNewsOutId { get; set; }
    public string NewsIdList { get; set; }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DO_Scrl_UserQAPostingAnswer
/// </summary>
public class DO_Scrl_UserQAPostingAnswer
{
    public DO_Scrl_UserQAPostingAnswer()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    
    public int ID { get; set; }
    public int intAnswerId { get; set; }
    public string strAnsLiStatus { get; set; }
    public string strComment { get; set; }
    public int intAnsAddedBy { get; set; }
    public int ResultId { get; set; }
}
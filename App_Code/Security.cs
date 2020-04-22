using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for Security
/// </summary>
public static class Validations
{

    public static string validateHtmlInput(string input)
    {
        string noHTML = Regex.Replace(input, @"<[^>]+>|&nbsp;", "");
        string outputString= Regex.Replace(noHTML, @"\s{2,}", " ");
        return outputString;
    }

    public static Boolean validateJavaScriptInput(string input)
    {
        string noHTML = Regex.Replace(input, @"<[^>]+>|&nbsp;", "");
        string outputString = Regex.Replace(noHTML, @"\s{2,}", " ");
        if (input==outputString)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
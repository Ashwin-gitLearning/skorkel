using System;


/// <summary>
/// Summary description for DO_SaveMySearch
/// </summary>
public class DO_SaveMySearch
{
	public DO_SaveMySearch()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public int intMySaveSearchId { get; set; }
    public string strSavedMyTitle { get; set; }
    public string strSearchQuery { get; set; }
    public int intAddedBy { get; set; }
    public string strIpAddress { get; set; }
    public Int32 intSubjectCategoryId { get; set; }
    public string Ids { get; set; }

    public string strSearchFor { get; set; }
    public string strPreSearchedIn { get; set; }
    public string strDocumentType { get; set; }
    public string strLookInto { get; set; }
    public string strAdvSearchTitle { get; set; }
    public string strAdvJuridiction { get; set; }
    public string strAdvCitation { get; set; }
    public string strAdvDateFrom { get; set; }
    public string strAdvDateTo { get; set; }
    public string strAdvProvision { get; set; }
    public string strAdvPartyName { get; set; }
    public string strAdvBench { get; set; }
    public string strAdvJudgeName { get; set; }
    public string strContextId { get; set; }
    public int intDocId { get; set; }
    public int intDocTypeId { get; set; }
    public int intSearchResultCount { get; set; }
    public int PageSize { get; set; }
    public int Currentpage { get; set; }

}
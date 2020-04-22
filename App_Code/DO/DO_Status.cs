using System;

/// <summary>
/// Summary description for DO_Status
/// </summary>
/// 
namespace  DA_SKORKEL
{

public class DO_Status

{
	public DO_Status()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    #region properties

    public String Condition { get; set; } 

    private int _statusId;
    public int StatusId
    {
        get { return _statusId; }
        set { _statusId = value; }
    }

    private int _year;
    public int Year
    {
        get { return _year; }
        set { _year = value; }
    }

    private string _title;
    public string Title
    {
        get { return _title; }
        set { _title = value; }
    }

    private int _ammendments;
    public int Ammendments
    {
        get { return _ammendments; }
        set { _ammendments = value; }
    }

    private string _subjectMatter;
    public string SubjectMatter
    {
        get { return _subjectMatter; }
        set { _subjectMatter = value; }
    }

    private string _objectofAct;
    public string ObjectofAct
    {
        get { return _objectofAct; }
        set { _objectofAct = value; }
    }

    private int _addedBy;
    public int AddedBy
    {
        get { return _addedBy; }
        set { _addedBy = value; }
    }


    private DateTime _addedOn;
    public DateTime AddedOn
    {
        get { return _addedOn; }
        set { _addedOn = value; }
    }

    private DateTime _modifiedOn;
    public DateTime ModifiedOn
    {
        get { return _modifiedOn; }
        set { _modifiedOn = value; }
    }

    private int _modifiedBy;
    public int ModifiedBy
    {
        get { return _modifiedBy; }
        set { _modifiedBy = value; }
    }

    private string _iPAddress;
    public string IPAddress
    {
        get { return _iPAddress; }
        set { _iPAddress = value; }
    }

#endregion

}
}

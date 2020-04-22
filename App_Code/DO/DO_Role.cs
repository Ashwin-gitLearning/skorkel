using System;

/// <summary>
/// Summary description for DO_Role
/// </summary>
public class DO_Role
{
	public DO_Role()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    #region Variable Declaration
    public int URPId { get; set; }
    public int UserTypeID { get; set; }
    public string TagName { get; set;}
    public int TagCode { get; set; }
    public bool IsAll { get; set; }
    public bool IsView { get; set; }
    public bool IsAdd { get; set; }
    public bool IsModify { get; set; }
    public bool IsDelete { get; set; }

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
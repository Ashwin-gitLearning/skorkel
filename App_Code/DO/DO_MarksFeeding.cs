using System;

/// <summary>
/// Summary description for DO_Marks
/// </summary>
/// 
namespace  DA_SKORKEL
{

public class DO_MarksFeeding

{
    public DO_MarksFeeding()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region properties

    public int RegistrationId { get; set; }
    public int CourseId { get; set; }
    public String Condition { get; set; }
    public int ClassId { get; set; }
    public int StudentID { get; set; }
    public int MarksObtained { get; set; }
    private int _MarksId;
    public int MarksId
    {
        get { return _MarksId; }
        set { _MarksId = value; }
    }

    private int _year;
    public int Year
    {
        get { return _year; }
        set { _year = value; }
    }   

    public int Subjectid { get; set; }  
   
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

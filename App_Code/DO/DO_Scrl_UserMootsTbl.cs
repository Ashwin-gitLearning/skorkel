using System;
namespace DA_SKORKEL
{
public class DO_Scrl_UserMootsTbl
{
public DO_Scrl_UserMootsTbl()
{  }
private  int	 _intMootId;
private  int	 _intRegistrationId;
private  string	 _strTitle;
private  int	 _intMonth;
private  int	 _intYear;
private  string	 _strLocation;
private  string	 _strJudges;
private  string	 _strDescription;
private  DateTime	 _dtAddedOn;
private  int	 _intAddedBy;
private  DateTime	 _dtModifiedOn;
private  int	 _intModifiedBy;
private  string	 _strIpAddress;

public  int intMootId       {         get { return _intMootId; }         set { _intMootId = value; }       } 
public  int intRegistrationId       {         get { return _intRegistrationId; }         set { _intRegistrationId = value; }       } 
public  string strTitle       {         get { return _strTitle; }         set { _strTitle = value; }       } 
public  int intMonth       {         get { return _intMonth; }         set { _intMonth = value; }       } 
public  int intYear       {         get { return _intYear; }         set { _intYear = value; }       } 
public  string strLocation       {         get { return _strLocation; }         set { _strLocation = value; }       } 
public  string strJudges       {         get { return _strJudges; }         set { _strJudges = value; }       } 
public  string strDescription       {         get { return _strDescription; }         set { _strDescription = value; }       } 
public  DateTime dtAddedOn       {         get { return _dtAddedOn; }         set { _dtAddedOn = value; }       } 
public  int intAddedBy       {         get { return _intAddedBy; }         set { _intAddedBy = value; }       } 
public  DateTime dtModifiedOn       {         get { return _dtModifiedOn; }         set { _dtModifiedOn = value; }       } 
public  int intModifiedBy       {         get { return _intModifiedBy; }         set { _intModifiedBy = value; }       } 
public  string strIpAddress       {         get { return _strIpAddress; }         set { _strIpAddress = value; }       } 

} }

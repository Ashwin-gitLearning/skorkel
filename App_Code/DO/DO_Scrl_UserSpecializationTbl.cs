using System;
namespace DA_SKORKEL
{
public class DO_Scrl_UserSpecializationTbl
{
public DO_Scrl_UserSpecializationTbl()
{  }
private  int	 _inSpecializationId;
private  int	 _intRegistrationId;
private  string	 _strSpecialization;
private  int	 _intYear;
private  string	 _strDescription;
private  DateTime	 _dtAddedOn;
private  int	 _intAddedBy;
private  DateTime	 _dtModifiedOn;
private  int	 _intModifiedBy;
private  string	 _strIpAddress;

public  int inSpecializationId       {         get { return _inSpecializationId; }         set { _inSpecializationId = value; }       } 
public  int intRegistrationId       {         get { return _intRegistrationId; }         set { _intRegistrationId = value; }       } 
public  string strSpecialization       {         get { return _strSpecialization; }         set { _strSpecialization = value; }       } 
public  int intYear       {         get { return _intYear; }         set { _intYear = value; }       } 
public  string strDescription       {         get { return _strDescription; }         set { _strDescription = value; }       } 
public  DateTime dtAddedOn       {         get { return _dtAddedOn; }         set { _dtAddedOn = value; }       } 
public  int intAddedBy       {         get { return _intAddedBy; }         set { _intAddedBy = value; }       } 
public  DateTime dtModifiedOn       {         get { return _dtModifiedOn; }         set { _dtModifiedOn = value; }       } 
public  int intModifiedBy       {         get { return _intModifiedBy; }         set { _intModifiedBy = value; }       } 
public  string strIpAddress       {         get { return _strIpAddress; }         set { _strIpAddress = value; }       } 

} }

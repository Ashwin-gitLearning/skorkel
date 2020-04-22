      
      
-- =============================================      
-- Author:  Monali Verulkar      
-- Create date: 20/01/2012      
-- Description: For Storing Data in Registration table      
-- =============================================      
ALTER PROCEDURE [dbo].[scrl_InSertRegistrationSP]      
@FlagNo INT,      
@RegistrationId INT =NULL,      
@FirstName varchar(50)=NULL,      
@MiddleName varchar(50)=NULL,      
@LastName varchar(50)=NULL,      
@Sex char=NULL,      
@UserName varchar(50)=NULL,      
@Password varchar(50)=NULL,      
@Address varchar(200)=NULL,      
@City varchar(50)=NULL,      
@State varchar(50)=NULL,      
@Country varchar(50)=NULL,      
@AddedBy int=NULL,      
@IPAddress varchar(50)=NULL,      
@intPinCode int=NULL,      
@ModifiedBy int=NULL,      
@UserTypeId BIGINT=NULL,      
@InstituteName varchar(50)=NULL,      
@DisableStatus bit=0,      
@Photopath varchar(200)=Null,      
@RollNo int=NULL ,      
@Batch varchar(50)=NULL,      
@PoliticalView varchar(50)=NULL,      
@Languages varchar(50)=NULL,      
@NonAchedemicInterest varchar(50)=NULL,      
@OtherPer varchar(50)=NULL,      
@Phone BIGINT=NULL,      
@Mobile BIGINT=NULL,      
@Designation varchar(50)=NULL,      
@University varchar(50)=NULL,      
@Alumni varchar(50)=NULL,      
@AkineTostatic varchar(50)=NULL,      
@WorkedAt varchar(50)=NULL,      
@ProfAchievements varchar(50)=NULL,      
@Publishings varchar(50)=NULL,      
@SubOfInterest varchar(50)=NULL,      
@Goals varchar(50)=NULL,      
@ExtraCredit varchar(50)=NULL,      
@Leadership varchar(50)=NULL,      
@Legaldept varchar(50)=NULL,      
@OtherLegalProject varchar(50)=NULL,      
@SubjectOfTought varchar(50)=NULL,      
@CVPath varchar(50)=NULL,      
@ParentId BigInt =NULL,      
@CourseId BigInt=NULL,      
@DeptId BigInt=NULL,      
@InstituteId int=NULL,      
@LawFirmId  int=NULL,      
@intLawRelated int=NULL,      
@vchrRegistartionType VARCHAR(2)=NULL,      
@strOthers VARCHAR(50)=NULL,      
@intNotificationCount INT=NULL,     
@NotificationDateTime datetime =null  
      
AS      
BEGIN      
 IF @FlagNo=1      
  BEGIN      
  DECLARE @strSrcklCode VARCHAR(50);      
  Declare @strSrckl_ID varchar(50);      
  DECLARE @strSrcklID BIGINT       
        
        
  IF @UserTypeId=4 OR @UserTypeId=6 OR @UserTypeId=7      
   SET @strSrckl_ID = (Select MAX(SUBSTRING(strSrcklId,3,LEN(strSrcklId))) from scrl_RegistrationTbl  WHERE intUserTypeID=@UserTypeId)      
  ELSE      
   SET @strSrckl_ID = (Select MAX(SUBSTRING(strSrcklId,3,LEN(strSrcklId))) from scrl_RegistrationTbl  WHERE intUserTypeID=@UserTypeId)      
  if (@strSrckl_ID is null)      
        set @strSrckl_ID =0;      
       
  SET @strSrcklID = CONVERT ( BIGINT , @strSrckl_ID)      
  SET @strSrcklID =@strSrcklID +1;        
      
  IF ( LEN(@strSrcklID) =1)      
  SET @strSrcklCode= '00000' + CONVERT ( VARCHAR(10),@strSrcklID)      
  ELSE IF ( LEN(@strSrcklID) =2)      
  SET @strSrcklCode = '0000' + CONVERT ( VARCHAR(10),@strSrcklID)      
  ELSE IF ( LEN(@strSrcklID) =3)      
  SET @strSrcklCode=  '000' + CONVERT ( VARCHAR(10),@strSrcklID)      
  ELSE IF ( LEN(@strSrcklID) =4)      
  SET @strSrcklCode=   '00' + CONVERT ( VARCHAR(10),@strSrcklID)      
  ELSE  IF ( LEN(@strSrcklID) =5)      
  SET @strSrcklCode=   '0' + CONVERT ( VARCHAR(10),@strSrcklID)      
  ELSE IF ( LEN(@strSrcklID) =6)      
  SET @strSrcklCode=    CONVERT ( VARCHAR(10),@strSrcklID)      
      
  IF   @UserTypeId = 1      
  SELECT @strSrcklCode='S' + @strSrcklCode      
      
  ELSE IF   @UserTypeId = 2      
  SELECT @strSrcklCode='P' + @strSrcklCode      
      
  ELSE IF @UserTypeId=3      
  Select @strSrcklCode='L' + @strSrcklCode      
      
  ELSE IF @UserTypeId=4      
  Select @strSrcklCode= 'IL' + @strSrcklCode      
      
  ELSE IF @UserTypeId=5      
  Select @strSrcklCode= 'J' + @strSrcklCode      
      
  ELSE IF @UserTypeId=6      
  Select @strSrcklCode='Th' + @strSrcklCode      
      
  ELSE IF @UserTypeId=7      
  Select @strSrcklCode='LF' + @strSrcklCode      
      
  ELSE IF @UserTypeId=8        Select @strSrcklCode= 'A' + @strSrcklCode      
      
  IF NOT EXISTS (SELECT 1 FROM scrl_RegistrationTbl WHERE vchrUserName=@UserName)      
  BEGIN      
   Insert into scrl_RegistrationTbl(vchrFirstName,vchrMiddleName,vchrLastName,chrSex,vchrUserName,vchrPassword,      
   vchrAddress,intPinCode,vchrCity,vchrState,intRollNo,vchrCountry,intUserTypeID,dtAddedOn,intAddedBy,vchrIPAddress,vchrInstituteName,bitDisableStatus,vchrPhotoPath,      
   vchrBatch,vchrPoliticalView,vchrLanguages,vchrNonAchedemicInterest,vchrOtherPer,intPhone,intMobile,vchrDesignation,vchrUniversity,vchrAlumni,strSrcklID,vchrAkineTostatic,vchrWorkedAt,vhcrProfAchievements,vchrPublishings,vchrSubOfInterest,vchrGoals,vchrExtraCredit,vchrLeadership,vchrCVPath ,vchrLegaldept,vchrOtherLegalProject,vchrSubjectOfTought,intDeptId,intCourseId,intInstituteId,intParentId,intLawFirmId,intLawRelated,vchrRegistartionType,strOthers)--,dtModifiedOn,intModifiedBy)      
   Values(@FirstName,@MiddleName,@LastName,@Sex,@UserName,@Password,@Address,@intPinCode,@City,@State,@RollNo,@Country,@UserTypeID,Getdate(),      
   @AddedBy,@IPAddress,@InstituteName,@DisableStatus,@Photopath,@Batch ,@PoliticalView,@Languages,@NonAchedemicInterest,@OtherPer,@Phone,@Mobile,@Designation,@University,@Alumni,@strSrcklCode,@AkineTostatic,@WorkedAt,@ProfAchievements,@Publishings,@SubOfInterest,@Goals,@ExtraCredit,@Leadership,@CVPath,@Legaldept ,@OtherLegalProject,@SubjectOfTought,@DeptId,@CourseId,@InstituteId,@ParentId,@LawFirmId,@intLawRelated,@vchrRegistartionType,@strOthers)      
   DECLARE @intNewRegID INT      
   SET @intNewRegID = @@IDENTITY;       
   SELECT @intNewRegID;      
   END      
  END      
 ELSE IF @FLagNo=2      
  BEGIN      
         
   UPDATE  scrl_RegistrationTbl       
   SET  vchrFirstName  = @FirstName      
     , vchrMiddleName  = @MiddleName      
     , vchrLastName  = @LastName      
     , chrSex  = @Sex      
     , vchrUserName = @UserName      
     , vchrPassword = @Password      
     , vchrAddress  = @Address      
     ,intPinCode=@intPinCode      
     , vchrCity  = @City      
     , vchrState = @State      
     , intRollNo = @RollNo      
     , vchrCountry=@Country          
     , vchrIPAddress = @IPAddress      
     , dtModifiedOn  = Getdate()      
     , intModifiedBy =@ModifiedBy      
     , vchrInstituteName=@InstituteName      
     , vchrPhotoPath=@Photopath      
     , vchrBatch=@Batch      
     , vchrPoliticalView=@PoliticalView      
     , vchrLanguages=@Languages      
     , vchrNonAchedemicInterest=@NonAchedemicInterest      
     , vchrOtherPer=@OtherPer      
     , intPhone=@Phone      
     , intMobile=@Mobile      
     , vchrDesignation=@Designation      
     , vchrUniversity=@University      
     ,vchrAlumni=@Alumni      
     ,vchrAkineTostatic=@AkineTostatic      
     ,vchrWorkedAt=@WorkedAt      
     ,vhcrProfAchievements=@ProfAchievements      
     ,vchrPublishings=@Publishings      
     ,vchrSubOfInterest=@SubOfInterest      
     ,vchrGoals=@Goals      
     ,vchrExtraCredit=@ExtraCredit      
     ,vchrLeadership=@Leadership      
     ,vchrCVPath=@CVPath      
     ,vchrLegaldept=@Legaldept      
     ,vchrOtherLegalProject=@OtherLegalProject      
     ,vchrSubjectOfTought=@SubjectOfTought      
     ,intCourseId=@CourseId      
     ,intDeptId=@DeptId      
     ,intLawRelated=@intLawRelated      
     ,vchrRegistartionType=@vchrRegistartionType      
     ,strOthers=@strOthers      
    WHERE intRegistrationId=@RegistrationId      
  END       
  ELSE IF @FLagNo=3      
        
  BEGIN      
   DELETE FROM  scrl_RegistrationTbl       
   WHERE intRegistrationId=@RegistrationId      
  END      
        
        
  ELSE IF @FLagNo=4      
        
  BEGIN      
  select intregistrationid,vchrfirstname, vchrmiddlename, vchrlastname,       
  case when chrsex='m' then 'male'      
    when chrsex='f' then 'female'      
    else ''      
  end chrsex,        
  (vchrfirstname +' ' + case when vchrlastname is null then '' else vchrlastname end) username, vchrusername,      
  vchrpassword,vchraddress,scrl_citymastertbl.vchrcityname vchrcity, scrl_statemastertbl.vchrstatename vchrstate,scrl_countrytbl.vchrcountryname vchrcountry,      
  introllno,intlawrelated,vchrregistartiontype,strothers,      
  (select intcountryid from scrl_countrytbl where intcountryid=vchrcountry)intcountryid,       
  (select strcoursename from scrl_coursetbl where intcourseid=scrl_registrationtbl.intcourseid)as coursename,intcourseid,intusertypeid ,      
  (select vchrusertype from scrl_usertypeexttbl  where intusertypeid= scrl_registrationtbl.intusertypeid)usertypename,vchrinstitutename,vchrbatch,vchrpoliticalview,vchrlanguages,vchrnonachedemicinterest,vchrotherper,intphone,intmobile,vchrdesignation,vchruniversity,vchralumni,strsrcklid,      
  (select strdeptname from scrl_departmenttbl where intdeptid=scrl_registrationtbl.intdeptid)as deptname,intdeptid,intlawrelated      
  from  scrl_registrationtbl      
  inner join scrl_citymastertbl on scrl_registrationtbl.vchrcity=scrl_citymastertbl.intcityid      
  inner join scrl_statemastertbl on scrl_registrationtbl.vchrstate=scrl_statemastertbl.intstateid      
  inner join scrl_countrytbl on scrl_registrationtbl.vchrcountry=Scrl_CountryTbl.intCountryId WHERE intUserTypeID=@UserTypeId AND bitDisableStatus=0      
      
      
 -- **** Comment By KIRAN 12 FEB 2015 ****           
  --SELECT intRegistrationId,vchrFirstName, vchrMiddleName, vchrLastName,       
  --CASE WHEN chrSex='M' THEN 'Male'      
  --  WHEN chrSex='F' THEN 'Female'      
  --  ELSE ''      
  --END chrSex,        
  --(vchrFirstName +' ' + CASE WHEN vchrLastName IS NULL THEN '' else vchrLastName END) UserName, vchrUserName,      
  --vchrPassword,vchrAddress,scrl_citymasterTbl.vchrCityName vchrCity, Scrl_StateMasterTbl.vchrStateName vchrState,Scrl_CountryTbl.vchrCountryName vchrCountry,      
  --intRollNo,intLawRelated,vchrRegistartionType,strOthers,      
  --(SELECT intCountryId FROM Scrl_CountryTbl WHERE intCountryId=vchrCountry)intCountryId,       
  --(Select strCourseName from Scrl_CourseTbl where intCourseId=scrl_RegistrationTbl.intCourseId)as CourseName,intCourseId,intUserTypeID ,      
  --(SELECT vchrUserType FROM Scrl_UserTypeExtTbl  WHERE intUserTypeID= scrl_RegistrationTbl.intUserTypeID)UserTypeName,vchrInstituteName,vchrBatch,vchrPoliticalView,vchrLanguages,vchrNonAchedemicInterest,vchrOtherPer,intPhone,intMobile,vchrDesignation,vchrUniversity,vchrAlumni,strSrcklID,      
  --(Select strDeptName from Scrl_DepartmentTbl where intDeptId=scrl_RegistrationTbl.intDeptId)as DeptName,intDeptId,intLawRelated      
  --FROM  scrl_RegistrationTbl      
  --Inner join scrl_citymasterTbl ON scrl_RegistrationTbl.vchrCity=scrl_citymasterTbl.intCityId      
  --Inner join Scrl_StateMasterTbl ON scrl_RegistrationTbl.vchrState=Scrl_StateMasterTbl.intStateId      
  --Inner join Scrl_CountryTbl ON scrl_RegistrationTbl.vchrCountry=Scrl_CountryTbl.intCountryId WHERE intUserTypeID=@UserTypeId AND bitDisableStatus=0      
 -- **** Kiran Comment END *****       
  END      
        
 ELSE IF @FLagNo=5      
  BEGIN         
  --SELECT TOP 1 scrl_RegistrationTbl.intRegistrationId,vchrFirstName, vchrMiddleName, vchrLastName, vchrInstituteName,      
  -- CASE WHEN chrSex='M' THEN 'Male'      
  --   WHEN chrSex='F' THEN 'Female'      
  --   ELSE ''      
  -- END chrSex,       
  -- (vchrFirstName +' ' + CASE WHEN vchrLastName IS NULL THEN '' else vchrLastName END) UserName, vchrUserName,vchrPassword,      
  -- vchrAddress,scrl_citymasterTbl.vchrCityName vchrCity, Scrl_StateMasterTbl.vchrStateName vchrState,Scrl_CountryTbl.vchrCountryName vchrCountry,           
  -- intCourseId,intDeptId,intUserTypeID ,      
  -- (SELECT vchrUserType FROM Scrl_UserTypeExtTbl WHERE intUserTypeID= scrl_RegistrationTbl.intUserTypeID)UserTypeName,      
  -- Scrl_UserContactDetailsTbl.intPinCode,vchrUserName,vchrInstituteName,vchrPhotoPath,      
  -- intRollNo,vchrBatch,vchrPoliticalView,vchrLanguages,vchrNonAchedemicInterest,vchrOtherPer,intPhone,intMobile,vchrDesignation,vchrUniversity,vchrAlumni,      
  -- vchrAkineTostatic,vchrWorkedAt,vhcrProfAchievements,vchrPublishings,vchrSubOfInterest,vchrGoals,vchrExtraCredit,vchrLeadership,vchrLegaldept,vchrOtherLegalProject,      
  -- vchrSubjectOfTought,vchrCVPath,strSrcklID,intLawRelated,vchrRegistartionType,strOthers,intNotificationCount      
  --FROM  scrl_RegistrationTbl       
  -- left JOIN Scrl_UserContactDetailsTbl ON scrl_RegistrationTbl.intRegistrationId = Scrl_UserContactDetailsTbl.intRegistrationId       
  -- left join scrl_citymasterTbl ON Scrl_UserContactDetailsTbl.intCity=scrl_citymasterTbl.intCityId      
  -- left join Scrl_StateMasterTbl ON Scrl_UserContactDetailsTbl.intState=Scrl_StateMasterTbl.intStateId      
  -- left join Scrl_CountryTbl ON Scrl_UserContactDetailsTbl.intCountry=Scrl_CountryTbl.intCountryId       
  --WHERE  scrl_RegistrationTbl.intRegistrationId=@RegistrationId AND bitDisableStatus=0      
      
      
  SELECT TOP 1 scrl_RegistrationTbl.intRegistrationId,vchrFirstName, vchrMiddleName, vchrLastName, vchrInstituteName,      
   CASE WHEN chrSex='M' THEN 'Male'      
     WHEN chrSex='F' THEN 'Female'      
     ELSE ''      
   END chrSex,       
   (vchrFirstName +' ' + CASE WHEN vchrLastName IS NULL THEN '' else vchrLastName END) UserName, vchrUserName,vchrPassword,      
   vchrAddress,           
   intCourseId,intDeptId,intUserTypeID ,      
   (SELECT vchrUserType FROM Scrl_UserTypeExtTbl WHERE intUserTypeID= scrl_RegistrationTbl.intUserTypeID)UserTypeName,      
   vchrUserName,vchrInstituteName,vchrPhotoPath,      
   intRollNo,vchrBatch,vchrPoliticalView,vchrLanguages,vchrNonAchedemicInterest,vchrOtherPer,intPhone,intMobile,vchrDesignation,vchrUniversity,vchrAlumni,      
   vchrAkineTostatic,vchrWorkedAt,vhcrProfAchievements,vchrPublishings,vchrSubOfInterest,vchrGoals,vchrExtraCredit,vchrLeadership,vchrLegaldept,vchrOtherLegalProject,      
   vchrSubjectOfTought,vchrCVPath,strSrcklID,intLawRelated,vchrRegistartionType,strOthers,intNotificationCount,notificationtimestamp      
  FROM  scrl_RegistrationTbl       
  WHERE  scrl_RegistrationTbl.intRegistrationId=@RegistrationId AND (bitDisableStatus=0 OR bitDisableStatus IS NULL)      
         
  END      
 ELSE IF @FLagNo=6        
  BEGIN      
   UPDATE  scrl_RegistrationTbl       
   SET bitDisableStatus=1      
    WHERE  intRegistrationId=@RegistrationId AND bitDisableStatus=0      
  END      
 Else If @FlagNo=7      
     BEGIN      
      SELECT intRegistrationId,vchrFirstName, vchrMiddleName, vchrLastName, chrSex,UserName, vchrUserName,      
   vchrPassword,vchrAddress,vchrCity,vchrState,intCountryId,vchrCountry,intUserTypeID ,UserTypeName,      
   vchrInstituteName      
   FROM (      
    SELECT intRegistrationId,vchrFirstName, vchrMiddleName, vchrLastName,       
    CASE WHEN chrSex='M' THEN 'Male'      
     WHEN chrSex='F' THEN 'Female'      
     ELSE ''      
   END chrSex,       
    (vchrFirstName +' ' + CASE WHEN vchrLastName IS NULL THEN '' else vchrLastName END) UserName, vchrUserName,      
   vchrPassword,vchrAddress,      
         
   (SELECT vchrCityName FROM Scrl_CityMasterTbl WHERE intCityId=vchrCity) vchrCity,       
   (SELECT vchrCountryName FROM Scrl_CountryTbl WHERE intCountryId=vchrCountry) vchrCountry,       
   (SELECT vchrStateName FROM Scrl_StateMasterTbl WHERE intStateId=vchrState) vchrState,       
            
   (SELECT intCountryId FROM Scrl_CountryTbl WHERE intCountryId=vchrCountry)intCountryId,      
   (Select strCourseName from Scrl_CourseTbl where intCourseId=scrl_RegistrationTbl.intCourseId)as CourseName,intCourseId, intUserTypeID ,      
   (SELECT vchrUserType FROM Scrl_UserTypeExtTbl  WHERE intUserTypeID= scrl_RegistrationTbl.intUserTypeID)UserTypeName,vchrInstituteName,intLawRelated,vchrRegistartionType,strOthers      
   FROM  scrl_RegistrationTbl WHERE intUserTypeID=@UserTypeId AND bitDisableStatus=0      
   ) AS T WHERE UserName LIKE '%'+'@FirstName' +'%'       
  END      
        
  ELSE IF @FLagNo=8      
        
  BEGIN      
   SELECT intRegistrationId,vchrFirstName, vchrMiddleName, vchrLastName, intLawRelated,vchrRegistartionType,strOthers,      
   CASE WHEN chrSex='M' THEN 'Male'      
   WHEN chrSex='F' THEN 'Female'      
   ELSE ''      
   END chrSex,       
   (vchrFirstName +' ' + CASE WHEN vchrLastName IS NULL THEN '' else vchrLastName END) UserName, vchrUserName,      
   vchrPassword,vchrAddress,scrl_citymasterTbl.vchrCityName vchrCity, Scrl_StateMasterTbl.vchrStateName vchrState,Scrl_CountryTbl.vchrCountryName vchrCountry,           
   intRollNo,      
   (SELECT vchrCountryName  FROM Scrl_CountryTbl WHERE intCountryId=vchrCountry)intCountryId,      
   (Select strCourseName from Scrl_CourseTbl where intCourseId=scrl_RegistrationTbl.intCourseId)as CourseName,intCourseId,      
   (Select strDeptName from Scrl_DepartmentTbl where intDeptId=scrl_RegistrationTbl.intDeptId)as DeptName,intDeptId,intUserTypeID ,      
   (SELECT vchrUserType FROM Scrl_UserTypeExtTbl  WHERE intUserTypeID= scrl_RegistrationTbl.intUserTypeID)UserTypeName,vchrInstituteName,vchrBatch,vchrPoliticalView,vchrLanguages,vchrNonAchedemicInterest,vchrOtherPer,intPhone,intMobile,vchrDesignation,vchrUniversity,vchrAlumni,intParentId      
   FROM  scrl_RegistrationTbl       
   Inner join scrl_citymasterTbl ON scrl_RegistrationTbl.vchrCity=scrl_citymasterTbl.intCityId     Inner join Scrl_StateMasterTbl ON scrl_RegistrationTbl.vchrState=Scrl_StateMasterTbl.intStateId      
   Inner join Scrl_CountryTbl ON scrl_RegistrationTbl.vchrCountry=Scrl_CountryTbl.intCountryId       
   WHERE intUserTypeID=@UserTypeId AND bitDisableStatus=0 And scrl_RegistrationTbl.intParentId=@ParentId      
  END      
        
 ELSE IF @FLagNo=9        
  BEGIN      
   SELECT intRegistrationId, vchrFirstName +' '+ISNULL(vchrLastName,'') FullName , vchrFirstName,vchrMiddleName,vchrLastName,intUserTypeID,intLawRelated,vchrRegistartionType,strOthers,vchrPhotoPath      
   FROM scrl_RegistrationTbl WHERE intRegistrationId=@AddedBy      
  END      
 Else IF @FLagNo=10      
  BEGIN      
  SET @strSrckl_ID = (Select MAX(SUBSTRING(strSrcklId,2,LEN(strSrcklId))) from scrl_RegistrationTbl  WHERE intUserTypeID=1)      
  if (@strSrckl_ID is null)      
        set @strSrckl_ID =0;      
        
  SET @strSrcklID = CONVERT ( BIGINT , @strSrckl_ID)      
  SET @strSrcklID =@strSrcklID +1;        
      
  IF ( LEN(@strSrcklID) =1)      
  SET @strSrcklCode= '00000' + CONVERT ( VARCHAR(10),@strSrcklID)      
  ELSE IF ( LEN(@strSrcklID) =2)      
  SET @strSrcklCode = '0000' + CONVERT ( VARCHAR(10),@strSrcklID)      
  ELSE IF ( LEN(@strSrcklID) =3)      
  SET @strSrcklCode=  '000' + CONVERT ( VARCHAR(10),@strSrcklID)      
  ELSE IF ( LEN(@strSrcklID) =4)      
  SET @strSrcklCode=   '00' + CONVERT ( VARCHAR(10),@strSrcklID)      
  ELSE  IF ( LEN(@strSrcklID) =5)      
  SET @strSrcklCode=   '0' + CONVERT ( VARCHAR(10),@strSrcklID)      
  ELSE IF ( LEN(@strSrcklID) =6)      
  SET @strSrcklCode=    CONVERT ( VARCHAR(10),@strSrcklID)      
      
  IF   @UserTypeId = 1      
  SELECT @strSrcklCode='S' + @strSrcklCode      
      
  ELSE IF   @UserTypeId = 2      
  SELECT @strSrcklCode='P' + @strSrcklCode      
      
  ELSE IF @UserTypeId=3      
  Select @strSrcklCode='L' + @strSrcklCode      
      
  ELSE IF @UserTypeId=4      
  Select @strSrcklCode= 'IL' + @strSrcklCode      
      
  ELSE IF @UserTypeId=5      
  Select @strSrcklCode= 'J' + @strSrcklCode      
      
  ELSE IF @UserTypeId=6      
  Select @strSrcklCode='Th' + @strSrcklCode      
      
  ELSE IF @UserTypeId=7      
  Select @strSrcklCode='LF' + @strSrcklCode      
      
  ELSE IF @UserTypeId=8      
  Select @strSrcklCode= 'A' + @strSrcklCode      
      
      
   Insert into scrl_RegistrationTbl(vchrFirstName,vchrMiddleName,vchrLastName,chrSex,vchrUserName,vchrPassword,      
   vchrAddress,intPinCode,vchrCity,vchrState,intRollNo,vchrCountry,intUserTypeID,dtAddedOn,intAddedBy,vchrIPAddress,vchrInstituteName,bitDisableStatus,vchrPhotoPath,      
   vchrBatch,vchrPoliticalView,vchrLanguages,vchrNonAchedemicInterest,vchrOtherPer,intPhone,intMobile,vchrDesignation,vchrUniversity,vchrAlumni,strSrcklID,vchrAkineTostatic,vchrWorkedAt,vhcrProfAchievements,vchrPublishings,vchrSubOfInterest,vchrGoals,vchrExtraCredit,vchrLeadership,vchrCVPath ,vchrLegaldept,vchrOtherLegalProject,vchrSubjectOfTought,intParentId,intCourseId,intDeptId,intInstituteId,intLawRelated,vchrRegistartionType,strOthers)--,dtModifiedOn,intModifiedBy)      
   Values(@FirstName,@MiddleName,@LastName,@Sex,@UserName,@Password,@Address,@intPinCode,@City,@State,@RollNo,@Country,@UserTypeID,Getdate(),      
   @AddedBy,@IPAddress,@InstituteName,@DisableStatus,@Photopath,@Batch ,@PoliticalView,@Languages,@NonAchedemicInterest,@OtherPer,@Phone,@Mobile,@Designation,@University,@Alumni,@strSrcklCode,@AkineTostatic,@WorkedAt,@ProfAchievements,@Publishings,@SubOfInterest,@Goals,@ExtraCredit,@Leadership,@CVPath,@Legaldept ,@OtherLegalProject,@SubjectOfTought,@ParentId,@CourseId,@DeptId,@InstituteId,@intLawRelated,@vchrRegistartionType,@strOthers)      
  END      
        
        
 ELSE IF @FLagNo=11      
  BEGIN      
  Select intDeptId,strDeptName from Scrl_DepartmentTbl      
        
  END      
       
  ELSE IF @FLagNo=12 --This flag is for Uploader on Web module added on 3 April 2013      
  BEGIN      
  SELECT intRegistrationId,vchrFirstName, vchrMiddleName, vchrLastName,intLawRelated,vchrRegistartionType,strOthers,      
  CASE WHEN chrSex='M' THEN 'Male'      
  WHEN chrSex='F' THEN 'Female'      
  ELSE ''      
  END chrSex,       
  (vchrFirstName +' ' + CASE WHEN vchrLastName IS NULL THEN '' else vchrLastName END) UserName, vchrUserName,      
  vchrPassword,vchrAddress,scrl_citymasterTbl.vchrCityName vchrCity, Scrl_StateMasterTbl.vchrStateName vchrState,Scrl_CountryTbl.vchrCountryName vchrCountry,      
  intRollNo,      
  (SELECT intCountryId FROM Scrl_CountryTbl WHERE intCountryId=vchrCountry)intCountryId,       
  (Select strCourseName from Scrl_CourseTbl where intCourseId=scrl_RegistrationTbl.intCourseId)as CourseName,intCourseId,intUserTypeID ,      
  (SELECT vchrUserType FROM Scrl_UserTypeExtTbl  WHERE intUserTypeID= scrl_RegistrationTbl.intUserTypeID)UserTypeName,vchrInstituteName,vchrBatch,vchrPoliticalView,vchrLanguages,vchrNonAchedemicInterest,vchrOtherPer,intPhone,intMobile,vchrDesignation,vchrUniversity,vchrAlumni,strSrcklID,      
  (Select strDeptName from Scrl_DepartmentTbl where intDeptId=scrl_RegistrationTbl.intDeptId)as DeptName,intDeptId      
  FROM  scrl_RegistrationTbl      
  Inner join scrl_citymasterTbl ON scrl_RegistrationTbl.vchrCity=scrl_citymasterTbl.intCityId      
  Inner join Scrl_StateMasterTbl ON scrl_RegistrationTbl.vchrState=Scrl_StateMasterTbl.intStateId      
  Inner join Scrl_CountryTbl ON scrl_RegistrationTbl.vchrCountry=Scrl_CountryTbl.intCountryId WHERE intUserTypeID=@UserTypeId AND bitDisableStatus=0  And scrl_RegistrationTbl.intParentId=@ParentId      
END      
        
 Else IF @FLagNo=13 --this flag is user for Admin module      
  BEGIN      
   UPDATE  scrl_RegistrationTbl       
   SET  vchrActive  = 'Y'      
   WHERE intRegistrationId=@RegistrationId       
  END      
       
 Else IF @FLagNo=14      
  BEGIN      
   UPDATE  scrl_RegistrationTbl       
   SET  intNotificationCount  = @intNotificationCount, notificationtimestamp =getdate()      
   WHERE intRegistrationId=@RegistrationId      
  END      
       
 Else IF @FLagNo=15      
  BEGIN      
   SELECT vchrInstituteName FROM scrl_RegistrationTbl      
   WHERE vchrInstituteName=@InstituteName      
  END      
      
END      
      
      
      
      
      
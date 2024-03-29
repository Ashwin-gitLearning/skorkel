
/****** Object:  StoredProcedure [dbo].[SP_Scrl_UserExperienceTbl]    Script Date: 13-02-2019 11:40:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- ==========================================================
-- Author:		Monali Verulkar
-- Create date: 16/03/2013
-- ==========================================================


ALTER PROCEDURE [dbo].[SP_Scrl_UserExperienceTbl]
@FlagNo AS INT,
@intExperienceId	 AS int =NULL ,
@intRegistrationId	 AS int =NULL ,
@strCompanyName	 AS varchar(500)=NULL ,
@strDesignation	 AS varchar(500) =NULL ,
@dtFromDate AS DATETIME=NULL,
@dtToDate AS DATETIME=NULL,
@inFromtMonth	 AS int =NULL ,
@intFromYear	 AS int =NULL ,
@intToMonth	 AS int =NULL ,
@intToYear	 AS int =NULL ,
@strLocation	 AS varchar(500) =NULL ,
@strDescription	 AS varchar(500) =NULL ,
@bitAtPresent AS BIT=NULL,
@dtAddedOn	 AS datetime =NULL ,
@intAddedBy	 AS int =NULL ,
@dtModifiedOn	 AS datetime =NULL ,
@intModifiedBy	 AS int =NULL ,
@strIpAddress	 AS varchar(20) =NULL,
@strBarNo AS varchar(50) =NULL,
@strUnqId AS VARCHAR(500)=NULL,
@strSkillName AS VARCHAR(500)=null,
@intUserSkillId AS INT=NULL
AS
BEGIN

IF @FlagNo=1

BEGIN
IF EXISTS (SELECT 1 FROM [Scrl_UserExperienceTbl] WHERE strCompanyName=@strCompanyName AND strDesignation=@strDesignation AND intAddedBy=@intAddedBy)
BEGIN
SELECT 0;
END
ELSE
BEGIN
	 INSERT INTO [Scrl_UserExperienceTbl]  ( intRegistrationId, strCompanyName, strDesignation,dtFromDate,dtToDate, inFromtMonth, intFromYear, intToMonth, intToYear, strLocation, strDescription,bitAtPresent, dtAddedOn, intAddedBy,  strIpAddress,strBarNo,strUnqId) 
	 VALUES (@intRegistrationId,@strCompanyName,@strDesignation,@dtFromDate,@dtToDate,@inFromtMonth,@intFromYear,@intToMonth,@intToYear,@strLocation,@strDescription,@bitAtPresent,GetDate(),@intAddedBy,@strIpAddress,@strBarNo,@strUnqId)

	 Declare @OutId INT
	 SET @OutId = @@IDENTITY;
	 select @OutId;
	 END

END

ELSE IF @FlagNo=2
BEGIN
IF EXISTS (SELECT 1 FROM [Scrl_UserExperienceTbl] WHERE strCompanyName=@strCompanyName AND strDesignation=@strDesignation AND intAddedBy=@intAddedBy AND intExperienceId<>@intExperienceId)
BEGIN
SELECT 0;
END
ELSE
BEGIN

	UPDATE [Scrl_UserExperienceTbl] 
	 SET 
		intRegistrationId=@intRegistrationId,
		strCompanyName=@strCompanyName,
		strDesignation=@strDesignation,
		dtFromDate=@dtFromDate,
		dtToDate=@dtToDate,
		inFromtMonth=@inFromtMonth,
		intFromYear=@intFromYear,
		intToMonth=@intToMonth,
		intToYear=@intToYear,
		strLocation=@strLocation,
		strDescription=@strDescription,
		bitAtPresent=@bitAtPresent,
		--intAddedBy=@intAddedBy,
		dtModifiedOn= GetDate(),
		intModifiedBy=@intModifiedBy,
		strIpAddress=@strIpAddress,
		strBarNo=@strBarNo	
	 WHERE intExperienceId = @intExperienceId
	 Declare @OutId1 INT
	 SET @OutId1 = @intExperienceId;
	 select @OutId1;
END
END

ELSE IF @FlagNo=3

BEGIN

DELETE FROM  [Scrl_UserExperienceTbl]  WHERE intExperienceId = @intExperienceId

END

ELSE IF @FlagNo=4

BEGIN

SELECT  intExperienceId, intRegistrationId, strCompanyName, strDesignation,REPLACE(CONVERT(VARCHAR(11),dtFromDate,106), ' ','-') dtFromDate,REPLACE(CONVERT(VARCHAR(11),dtToDate,106), ' ','-') dtToDate , inFromtMonth, intFromYear, intToMonth, intToYear, strLocation, strDescription,bitAtPresent, dtAddedOn, intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress ,strBarNo,strUnqId
FROM	[Scrl_UserExperienceTbl] 
WHERE	intExperienceId = @intExperienceId

END

ELSE IF @FlagNo=5

BEGIN 

SELECT  intExperienceId, intRegistrationId, strCompanyName, strDesignation,bitAtPresent ,dtFromDate, 
		REPLACE(CONVERT(VARCHAR(11),dtToDate,106), ' ','-') dtToDate,
		DATEDIFF (DAY , intFromYear , intToYear ) TotalExperience,		
		DateName( month , DateAdd( month ,inFromtMonth  , 0 ) - 1)
		inFromtMonth, intFromYear, 
		DateName( month , DateAdd( month ,intToMonth  , 0 ) - 1)
		intToMonth, intToYear, strLocation, strDescription,bitAtPresent, dtAddedOn, intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress , strBarNo
FROM	[Scrl_UserExperienceTbl]  
WHERE	intRegistrationId=@intAddedBy
ORDER BY dtAddedOn DESC

END
ELSE IF @FlagNo=7
BEGIN
IF NOT EXISTS (SELECT 1 FROM [Scrl_UserExperienceSkillTbl] WHERE strUnqId=@strUnqId AND strSkillName=@strSkillName)
BEGIN
	 INSERT INTO [Scrl_UserExperienceSkillTbl]  (  strUnqId, strSkillName,dtAddedOn,strIpAddress) 
	 VALUES (@strUnqId,@strSkillName,GETDATE(),@strIpAddress)

	 SET @OutId = @@IDENTITY;
	 select @OutId;
	 END
END
ELSE IF @FlagNo=8
BEGIN
	 SELECT intUserSkillId,strUnqId,strSkillName,dtAddedOn  FROM [Scrl_UserExperienceSkillTbl]  WHERE strUnqId=@strUnqId
	 ORDER BY dtAddedOn DESC
END
ELSE IF @FlagNo=9
BEGIN
	DELETE  FROM [Scrl_UserExperienceSkillTbl]  WHERE intUserSkillId=@intUserSkillId
END
ELSE IF @FlagNo=10
BEGIN
		UPDATE [Scrl_UserExperienceSkillTbl] 
		SET 
		intAddedBy=@intAddedBy	
		WHERE strUnqId=@strUnqId
END
ELSE IF @FlagNo=11
BEGIN
	 SELECT intUserSkillId,strUnqId,strSkillName,dtAddedOn  FROM [Scrl_UserExperienceSkillTbl]  WHERE intAddedBy=@intAddedBy AND strUnqId!=''	
	 ORDER BY dtAddedOn DESC
END
ELSE IF @FlagNo=12
BEGIN
	 SELECT intUserSkillId,strUnqId,strSkillName,dtAddedOn  FROM [Scrl_UserExperienceSkillTbl]  WHERE intAddedBy=@intAddedBy AND (strUnqId=null OR strUnqId='')	
	 ORDER BY dtAddedOn DESC
END
ELSE IF @FlagNo=13
BEGIN
	 SELECT DISTINCT strUnqId  FROM [Scrl_UserExperienceSkillTbl]  WHERE intAddedBy=@intAddedBy AND strUnqId!=''		
END
ELSE IF @FlagNo=14
BEGIN
	 Update Scrl_UserExperienceSkillTbl SET strUnqId=@strUnqId  WHERE intAddedBy=@intAddedBy AND(strUnqId=null OR strUnqId='')		
END
ELSE IF @FlagNo=15
BEGIN
	 INSERT INTO [Scrl_UserExperienceSkillTbl]  ( intAddedBy, strUnqId, strSkillName,dtAddedOn,strIpAddress) 
	 VALUES (@intAddedBy,@strUnqId,@strSkillName,GETDATE(),@strIpAddress)

	 SET @OutId = @@IDENTITY;
	 select @OutId;
END

ELSE IF @FlagNo=16
BEGIN
	 DELETE [Scrl_UserExperienceSkillTbl] WHERE intAddedBy=@intAddedBy
END

ELSE IF @FlagNo=17
BEGIN
	  SELECT COUNT(intRecommendationId)Endorse FROM	Scrl_UserRecommendationTbl 
   INNER JOIN scrl_RegistrationTbl ON Scrl_UserRecommendationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId
   WHERE	Scrl_UserRecommendationTbl.intInvitedUserId=@intAddedBy AND IsAccepted=1 AND Scrl_UserRecommendationTbl.intSkillId=@intUserSkillId
END
ELSE IF @FlagNo=18
BEGIN
	DELETE  FROM [Scrl_UserExperienceSkillTbl]  WHERE intAddedBy=@intAddedBy AND strUnqId=''
END
END



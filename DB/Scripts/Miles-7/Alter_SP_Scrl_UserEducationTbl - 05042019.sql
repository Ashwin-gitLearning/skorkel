
/****** Object:  StoredProcedure [dbo].[SP_Scrl_UserEducationTbl]    Script Date: 05-04-2019 19:04:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
ALTER PROCEDURE [dbo].[SP_Scrl_UserEducationTbl]
@FlagNo AS INT,
@intEducationId	 AS int =NULL ,
@intRegistrationId	 AS int =NULL ,
@strInstituteName	 AS varchar(500) =NULL ,
@dtDate AS DATETIME=NULL,
@intMonth	 AS int =NULL ,
@intYear	 AS int =NULL ,
@strDegree	 AS varchar(500) =NULL ,
@strGrade	 AS varchar(500) =NULL ,
@strSpclLibrary	 AS varchar(500)=NULL ,
@strEducationAchievement AS VARCHAR(50)=NULL,
@dtAddedOn	 AS datetime =NULL ,
@intAddedBy	 AS int =NULL ,
@dtModifiedOn	 AS datetime =NULL ,
@intModifiedBy	 AS int =NULL ,
@strIpAddress	 AS varchar(200) =NULL ,
@intToYear AS INT =NULL,
@intToMonth AS INT=NULL,
@intAchivmentId AS INT = NULL,
@strCompititionName	 AS varchar(500) =NULL ,
@strPosition	 AS varchar(500) =NULL ,
@strAdditionalAward	 AS varchar(500) =NULL ,
@strAchDescription	 AS varchar(5000) =NULL ,
@strMilestone	 AS varchar(500) =NULL ,
@intDegreeId AS INT=NULL,
@intInstituteId AS INT=NULL,
@intPostionId AS INT=NULL,
@intMilestoneId AS INT=NULL,
@CompLavel AS INT=NULL,
@PositionIds VARCHAR(200)=NULL,
@CompId AS INT=NULL,
@stud_percentile as Decimal=NULL

AS
BEGIN

IF @FlagNo=1

BEGIN
if EXISTS (SELECT intEducationId FROM [Scrl_UserEducationTbl] WHERE strInstituteName=@strInstituteName AND strDegree=@strDegree AND strEducationAchievement=@strEducationAchievement AND intRegistrationId=@intRegistrationId)
BEGIN
Select 0;
END
ELSE
BEGIN
	 INSERT INTO [Scrl_UserEducationTbl]  ( intRegistrationId, strInstituteName, dtDate,intMonth, intYear, strDegree, strGrade, strSpclLibrary,strEducationAchievement, dtAddedOn, intAddedBy,  strIpAddress,intToMonth,intToYear,intDegreeId,intInstituteId,student_percentile) 
	 VALUES (@intRegistrationId,@strInstituteName,@dtDate,@intMonth,@intYear,@strDegree,@strGrade,@strSpclLibrary,@strEducationAchievement,GetDate(),@intAddedBy,@strIpAddress,@intToMonth,@intToYear,@intDegreeId,@intInstituteId,@stud_percentile)
	 select 1;
	 END
END

ELSE IF @FlagNo=2

BEGIN

UPDATE [Scrl_UserEducationTbl] 
 SET 
intRegistrationId=@intRegistrationId,
strInstituteName=@strInstituteName,
dtDate=@dtDate,
intMonth=@intMonth,
intYear=@intYear,
strDegree=@strDegree,
strGrade=@strGrade,
strSpclLibrary=@strSpclLibrary,
strEducationAchievement=@strEducationAchievement,
--intAddedBy=@intAddedBy,
dtModifiedOn= GetDate(),
intModifiedBy=@intModifiedBy,
strIpAddress=@strIpAddress,
intToMonth=@intToMonth,
intToYear=@intToYear,
intInstituteId=@intInstituteId,
intDegreeId=@intDegreeId,
student_percentile=@stud_percentile
 WHERE intEducationId = @intEducationId
 Select 1
END

ELSE IF @FlagNo=3

BEGIN

DELETE FROM  [Scrl_UserEducationTbl]  WHERE intEducationId = @intEducationId

END

ELSE IF @FlagNo=4

BEGIN

SELECT  intEducationId, intRegistrationId, strInstituteName, REPLACE(CONVERT(VARCHAR(11),dtDate,106), ' ','-') dtDate,intMonth, intYear, strDegree, strGrade, strSpclLibrary,strEducationAchievement, dtAddedOn, intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress,intToYear,intToMonth FROM [Scrl_UserEducationTbl] WHERE intEducationId = @intEducationId

END

ELSE IF @FlagNo=5

BEGIN
SELECT  ROW_NUMBER() OVER (ORDER BY [Scrl_UserEducationTbl].intYear ASC ) AS RowNumber,
		intEducationId, intRegistrationId, strInstituteName, 
		DateName( month , DateAdd( month ,intMonth  , 0 ) - 1)intMonth  , REPLACE(CONVERT(VARCHAR(11),dtDate,106), ' ','-') dtDate,
		--intYear, strDegree, strGrade, replace(strSpclLibrary,substring(strSpclLibrary,100,len(strSpclLibrary)),'...') strSpclLibrary,strEducationAchievement, dtAddedOn, intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress 
		intYear, strDegree, strGrade, strSpclLibrary,strEducationAchievement, dtAddedOn, intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress 
FROM	[Scrl_UserEducationTbl]   
where	intRegistrationId=@intAddedBy
ORDER BY dtAddedOn DESC

END

ELSE IF @FlagNo=6

BEGIN
SELECT  ROW_NUMBER() OVER (ORDER BY [Scrl_UserEducationTbl].intYear ASC ) AS RowNumber,
		intEducationId, intRegistrationId, strInstituteName, 
		DateName( month , DateAdd( month ,intMonth  , 0 ) - 1)intMonth  ,dtDate,
		intYear, strDegree, strGrade, replace(strSpclLibrary,substring(strSpclLibrary,100,len(strSpclLibrary)),'...') strSpclLibrary,strEducationAchievement, dtAddedOn, intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress 
FROM	[Scrl_UserEducationTbl]   
where	intRegistrationId=@intAddedBy AND strEducationAchievement='Education'

END

ELSE IF @FlagNo=7

BEGIN
SELECT  ROW_NUMBER() OVER (ORDER BY [Scrl_UserEducationTbl].intYear ASC ) AS RowNumber,
		intEducationId, intRegistrationId, strInstituteName, 
		DateName( month , DateAdd( month ,intMonth  , 0 ) - 1)intMonth  ,CONVERT(VARCHAR(20),dtDate,106)dtDate,
		intYear, strDegree, strGrade,
		 replace(strSpclLibrary,substring(strSpclLibrary,150,len(strSpclLibrary)),'...') strSpclLibrary
		 ,strEducationAchievement, dtAddedOn, intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress 
FROM	[Scrl_UserEducationTbl]   
where	intRegistrationId=@intAddedBy AND strEducationAchievement='Achievement'

END
ELSE IF @FlagNo=8

BEGIN
SELECT  ROW_NUMBER() OVER (ORDER BY [Scrl_UserEducationTbl].intYear ASC ) AS RowNumber,
		intEducationId, intRegistrationId, strInstituteName, 
		DateName( month , DateAdd( month ,intMonth  , 0 ) - 1)intMonth  , REPLACE(CONVERT(VARCHAR(11),dtDate,106), ' ','-') dtDate,
		--intYear, strDegree, strGrade, replace(strSpclLibrary,substring(strSpclLibrary,100,len(strSpclLibrary)),'...') strSpclLibrary,strEducationAchievement, dtAddedOn, intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress 
		intYear, strDegree, strGrade, strSpclLibrary,strEducationAchievement, dtAddedOn, intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress,
		 DateName( month , DateAdd( month ,intToMonth  , 0 ) - 1)intToMonth,intToYear,intToMonth AS ToMonth
FROM	[Scrl_UserEducationTbl]   
where	intRegistrationId=@intAddedBy AND strEducationAchievement='Education'
ORDER BY dtAddedOn DESC

END
ELSE IF @FlagNo=9
BEGIN
SELECT 
		intAchivmentId, intAddedBy, strCompititionName, 
		--DateName( month , DateAdd( month ,intMonth  , 0 ) - 1)intMonth  , REPLACE(CONVERT(VARCHAR(11),dtDate,106), ' ','-') dtDate,
		--intYear, strDegree, strGrade, replace(strSpclLibrary,substring(strSpclLibrary,100,len(strSpclLibrary)),'...') strSpclLibrary,strEducationAchievement, dtAddedOn, intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress 
		strPosition, strAdditionalAward, strDescription, strMilestone,dtAddedOn,
		DATENAME(MONTH, dtAddedOn) +' ' + CAST(YEAR(dtAddedOn) AS VARCHAR(4)) AS MoYear
FROM	Scrl_UserAchivmentTbl   
where	intAddedBy=@intAddedBy 
ORDER BY dtAddedOn DESC
END
ELSE IF @FlagNo=10
BEGIN
DELETE FROM	Scrl_UserAchivmentTbl   
WHERE	intAchivmentId=@intAchivmentId
END
ELSE IF @FlagNo=11
BEGIN
SELECT 
		intAchivmentId, intAddedBy, strCompititionName, 
		strPosition, strAdditionalAward, strDescription, strMilestone,dtAddedOn,
		DATENAME(MONTH, dtAddedOn) +' ' + CAST(YEAR(dtAddedOn) AS VARCHAR(4)) AS MoYear,
		intMilestoneId,intPostionId,CompLavel
FROM	Scrl_UserAchivmentTbl   
WHERE	intAchivmentId=@intAchivmentId
END
ELSE IF @FlagNo=12
BEGIN
	 INSERT INTO Scrl_UserAchivmentTbl (intAddedBy, strCompititionName, strAdditionalAward, strDescription, strMilestone, dtAddedOn,strIpAddress,strPosition,intMilestoneId,intPostionId,CompLavel,intCompetitonId) 
	 VALUES (@intAddedBy,@strCompititionName,@strAdditionalAward,@strAchDescription,@strMilestone,GETDATE(),@strIpAddress,@strPosition,@intMilestoneId,@intPostionId,@CompLavel,@CompId)
	 SELECT @@IDENTITY
END

ELSE IF @FlagNo=13
BEGIN
			UPDATE Scrl_UserAchivmentTbl  SET 
			strCompititionName=@strCompititionName,
			strAdditionalAward=@strAdditionalAward, 
			strDescription=@strAchDescription,
			strMilestone=@strMilestone,
			strPosition=@strPosition,
			dtModifiedOn=GETDATE(),
			intModifiedBy=@intAddedBy,
			intPostionId=@intPostionId,
			intMilestoneId=@intMilestoneId,
			CompLavel=@CompLavel,
			intCompetitonId=@CompId
			WHERE intAchivmentId=@intAchivmentId
END
ELSE IF @FlagNo=14
BEGIN
		SELECT intYearId,intYear FROM Scrl_YearMasterTbl 
		ORDER BY intYear desc
END
ELSE IF @FlagNo=15
BEGIN
		SELECT intDegreeId FROM Scrl_InstituteDegreeTbl 
	    WHERE strDegreeName LIKE  '%'+@strDegree+'%'
END
ELSE IF @FlagNo=16
BEGIN
		SELECT intInstituteId AS intDegreeId FROM Scrl_InstituteNameTbl 
	    WHERE strInstituteName LIKE  '%'+@strInstituteName+'%'
END
ELSE IF @FlagNo=17
BEGIN
		SELECT strMilestoneName,intMilestoneId FROM Scrl_AchivementMilestoneTbl 	   
END
ELSE IF @FlagNo=18
BEGIN
		SELECT strPositionName,intPostionId FROM Scrl_AchivementPositionTbl 	   
END
ELSE IF @FlagNo=19
BEGIN
		SELECT CompLevel AS intDegreeId FROM Scrl_CompetitionMasterTbl 	
		WHERE  CompName LIKE  '%'+@strCompititionName+'%'  
END
ELSE IF @FlagNo=20
BEGIN

		WITH UserParms AS
				(
					SELECT ItemNumber,Item
					FROM dbo.[DelimitedSplit8K](@PositionIds, ',')
				)
				SELECT * FROM
				(		
					SELECT strPositionName,intPostionId FROM Scrl_AchivementPositionTbl 
				)	AS T JOIN UserParms As U ON intPostionId = U.Item WHERE intPostionId IN (Select Item from UserParms) ORDER BY U.ItemNumber


		--SELECT strPositionName,intPostionId FROM Scrl_AchivementPositionTbl 
		--	where intPostionId IN (SELECT item from dbo.split(CASE WHEN LEN(@PositionIds)>0 THEN @PositionIds ELSE CONVERT(VARCHAR(200),intPostionId) END,','))  
END
ELSE IF @FlagNo=21
BEGIN
		SELECT CompetitionId FROM Scrl_CompetitionMasterTbl 	
		WHERE CompName=@strCompititionName 
END
END


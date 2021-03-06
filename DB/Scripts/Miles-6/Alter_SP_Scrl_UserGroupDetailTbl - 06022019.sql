
/****** Object:  StoredProcedure [dbo].[SP_Scrl_UserGroupDetailTbl]    Script Date: 06-02-2019 15:07:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- ==========================================================
-- Author:		Monali Verulkar
-- Create date: 16/03/2013
-- ==========================================================


ALTER PROCEDURE [dbo].[SP_Scrl_UserGroupDetailTbl]
@FlagNo AS INT,
@inGroupId	 AS int =NULL ,
@intGroupTypeId AS int=NULL,
@intRegistrationId	 AS int =NULL ,
@strGroupName	 AS varchar(500) =NULL ,
@strGroupTypeName	 AS varchar(MAX) =NULL ,
@strSummary	 AS varchar(MAX) =NULL ,
@strGroupType	 AS int =NULL ,
@strDescription	 AS varchar(MAX) =NULL ,
@strAccess	 AS varchar(2) =NULL ,
@strLogoPath	 AS varchar(MAX) =NULL ,
@dtAddedOn	 AS datetime =NULL ,
@intAddedBy	 AS int =NULL ,
@dtModifiedOn	 AS datetime =NULL ,
@intModifiedBy	 AS int =NULL ,
@strIpAddress	 AS varchar(500) =NULL ,
@bitPrivatePublic	 AS bit =NULL ,
@LocationId AS int =NULL,
@strContextId VARCHAR(50)=NULL,
@intUserId INT=NULL,
@strRoleName VARCHAR(50)=NULL,
@intSubjectCategoryId INT=NULL,
@DocId INT=NULL,
@strInvitationMessage VARCHAR(MAX)=NULL,
@inviteMembers VARCHAR(MAX)=NULL,
@intRoleId INT=NULL,
@strUniqueKey VARCHAR(MAX)=NULL,
@GrpInvitationMemberId INT=NULL,
@intGrpModuleId INT=NULL,
@intVisibility INT=NULL,
@intAddPermission INT=NULL,
@intEditPermission INT=NULL,
@intDeletePermission INT=NULL,
@intGrpRoleId INT=NULL,
@intGrpInvitationMemberId as int,
@strMemberName AS VARCHAR(max)=null

AS
BEGIN

IF @FlagNo=1

BEGIN
	 INSERT INTO [Scrl_UserGroupDetailTbl]  ( intRegistrationId, strGroupName, strSummary, strGroupType, strDescription, strAccess, strLogoPath, dtAddedOn, intAddedBy, strIpAddress,bitPrivatePublic,intLocationId,strContextId,strInvitationMessage,strUniqueKey) 
	 VALUES (@intRegistrationId,@strGroupName,@strSummary,@strGroupType,@strDescription,@strAccess,@strLogoPath,GetDate(),@intAddedBy,@strIpAddress,@bitPrivatePublic,@LocationId,@strContextId,@strInvitationMessage,@strUniqueKey)
	 DECLARE @intNewGroupID INT
	 SET @intNewGroupID = @@IDENTITY;	
	 SELECT @intNewGroupID;

	 UPDATE Scrl_GroupMemberDetailsTbl SET inGroupId=  @intNewGroupID WHERE strUniqueKey=@strUniqueKey
	 UPDATE Scrl_GroupRoleDetailTbl SET inGroupId=  @intNewGroupID WHERE strUniqueKey=@strUniqueKey
	 UPDATE Scrl_GroupModulePermissionTbl SET intGroupId=  @intNewGroupID WHERE strUniqueKey=@strUniqueKey
END

ELSE IF @FlagNo=2
BEGIN

	UPDATE [Scrl_UserGroupDetailTbl] 
	 SET 
	intRegistrationId=@intRegistrationId,
	strGroupName=@strGroupName,
	strSummary=@strSummary,
	strGroupType=@strGroupType,
	strDescription=@strDescription,
	strAccess=@strAccess,
	strLogoPath=@strLogoPath,
	dtModifiedOn= GetDate(),
	intModifiedBy=@intModifiedBy,
	strIpAddress=@strIpAddress,
	bitPrivatePublic=@bitPrivatePublic,
	strInvitationMessage=@strInvitationMessage
	WHERE inGroupId = @inGroupId

END

ELSE IF @FlagNo=3
BEGIN
	DELETE FROM  [Scrl_UserGroupDetailTbl]  WHERE inGroupId = @inGroupId
	DELETE Scrl_OtherContextGroupDocument WHERE intGrpId = @inGroupId
	DELETE FROM Scrl_UserForumPostingTbl WHERE intGroupId = @inGroupId
	DELETE FROM  Scrl_UserPostForumReplyTbl WHERE intGroupId = @inGroupId
	DELETE FROM Scrl_UserPostForumChildLikeTbl WHERE intGroupId = @inGroupId
	DELETE FROM Scrl_GroupUploadDouments WHERE intGroupId= @inGroupId
	DELETE FROM Scrl_GroupFolderDetails WHERE intGroupId = @inGroupId
	DELETE FROM Scrl_GroupUploadDocLike WHERE intGroupID = @inGroupId
	DELETE FROM Scrl_GroupUploadDocComment WHERE intGroupId = @inGroupId
	DELETE FROM Scrl_UserPollTbl WHERE intGroupId = @inGroupId
	DELETE FROM Scrl_GroupEventsTbl WHERE intGroupId = @inGroupId
END

ELSE IF @FlagNo=4
BEGIN
	SELECT  inGroupId, intRegistrationId, strGroupName, strSummary, strGroupType, strDescription, strAccess, strLogoPath, dtAddedOn, intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress,bitPrivatePublic FROM [Scrl_UserGroupDetailTbl] WHERE inGroupId = @inGroupId
END

ELSE IF @FlagNo=5
BEGIN
	SELECT  inGroupId, intRegistrationId, strGroupName, strSummary,
	(Select strGroupType FROM Scrl_UserGroupTypeTbl where intGroupTypeId= strGroupType)strGroupType, strDescription, bitPrivatePublic,
	Case when strAccess='Y' then 'Auto Join'
	Else 'Request to Join' END strAccess ,
	strLogoPath, dtAddedOn, intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress FROM [Scrl_UserGroupDetailTbl] WHERE  intRegistrationId=@intAddedBy
END

ELSE IF @FlagNo=6
BEGIN
		SELECT DISTINCT TOP 3 COUNT(*) OVER()  AS Maxcount , * FROM(  SELECT 	inGroupId,strGroupName,strLogoPath,strSummary,dtAddedOn FROM Scrl_UserGroupDetailTbl where intRegistrationId=@intRegistrationId
UNION ALL
	SELECT  Scrl_UserGroupDetailTbl.inGroupId,strGroupName,Scrl_UserGroupDetailTbl.strLogoPath,strSummary, Scrl_UserGroupDetailTbl.dtAddedOn
		FROM   Scrl_UserGroupDetailTbl 
			   LEFT OUTER JOIN 
			   Scrl_UserGroupJoiningTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId		
		WHERE Scrl_UserGroupJoiningTbl.intRegistrationId=@intRegistrationId AND Scrl_UserGroupJoiningTbl.IsAccepted=1
			
			) AS T ORDER BY dtAddedOn DESC	  
END

ELSE IF @FlagNo=7
BEGIN
	SELECT intGroupTypeId,strGroupType FROM Scrl_UserGroupTypeTbl
END

ELSE IF @FlagNo=8
BEGIN

	SELECT TOP 4 COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_UserGroupDetailTbl.dtAddedOn desc ) AS RowNumber,
		inGroupId, intRegistrationId, strGroupName, strSummary, strGroupType, strDescription, strAccess, strLogoPath, dtAddedOn,
		(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=[Scrl_UserGroupDetailTbl].intRegistrationId) Name,		
		(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=[Scrl_UserGroupDetailTbl].intRegistrationId) vchrUserName,							
		intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress FROM [Scrl_UserGroupDetailTbl] 
	WHERE inGroupId 
		  NOT IN
		 (
			SELECT Scrl_UserGroupJoiningTbl.inGroupId
			FROM Scrl_UserGroupJoiningTbl
				 LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId = Scrl_UserGroupJoiningTbl.inGroupId
			WHERE Scrl_UserGroupJoiningTbl.intRegistrationId =@intRegistrationId AND IsAccepted=1 
		 )
		 AND Scrl_UserGroupDetailTbl.intRegistrationId !=@intRegistrationId
END

ELSE IF @FlagNo=9
BEGIN
	SELECT  inGroupId, intRegistrationId, strGroupName, strSummary, strGroupType, strDescription, strAccess, strLogoPath, CONVERT(VARCHAR(12),dtAddedOn,109) dtAddedOn,
	(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=[Scrl_UserGroupDetailTbl].intRegistrationId) Name,		
	(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=[Scrl_UserGroupDetailTbl].intRegistrationId) vchrUserName,							
	intAddedBy, CONVERT(VARCHAR(12),dtModifiedOn,109) dtModifiedOn, intModifiedBy, strIpAddress,strUniqueKey FROM [Scrl_UserGroupDetailTbl] 
	WHERE inGroupId =@inGroupId
	
	SELECT intRequestJoinId,IsAccepted FROM Scrl_UserGroupJoiningTbl WHERE inGroupId=@inGroupId AND intAddedBy=@intAddedBy 

END

ELSE IF @FlagNo=10
BEGIN
	SELECT inGroupId,strGroupName,strLogoPath,CONVERT(VARCHAR(12),dtAddedOn,109) dtAddedOn FROM Scrl_UserGroupDetailTbl where intRegistrationId=@intRegistrationId;
END

ELSE IF @FlagNo=11
BEGIN

	SELECT inGroupId, intRegistrationId, strGroupName, strSummary, strGroupType, strDescription, strAccess, strLogoPath, dtAddedOn,
		(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=[Scrl_UserGroupDetailTbl].intRegistrationId) Name,		
		(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=[Scrl_UserGroupDetailTbl].intRegistrationId) vchrUserName,							
		intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress FROM [Scrl_UserGroupDetailTbl] 
	WHERE inGroupId 
		  NOT IN
		 (
			SELECT Scrl_UserGroupJoiningTbl.inGroupId
			FROM Scrl_UserGroupJoiningTbl
				 LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId = Scrl_UserGroupJoiningTbl.inGroupId
			WHERE Scrl_UserGroupJoiningTbl.intRegistrationId =@intRegistrationId AND IsAccepted=1 
		 )
		 AND Scrl_UserGroupDetailTbl.intRegistrationId !=@intRegistrationId
END

ELSE IF @FlagNo=12
BEGIN
	SELECT TOP 4 COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_UserGroupJoiningTbl.dtAddedOn desc ) AS RowNumber,
		 Scrl_UserGroupJoiningTbl.inGroupId ,strGroupName,strLogoPath,Scrl_UserGroupDetailTbl.dtAddedOn
	FROM Scrl_UserGroupJoiningTbl
		 LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId = Scrl_UserGroupJoiningTbl.inGroupId
	WHERE Scrl_UserGroupJoiningTbl.intRegistrationId =@intRegistrationId AND IsAccepted=1 
END

ELSE IF @FlagNo=13
BEGIN
	SELECT  COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_UserGroupJoiningTbl.dtAddedOn desc ) AS RowNumber,
		 Scrl_UserGroupJoiningTbl.inGroupId ,strGroupName,strLogoPath,Scrl_UserGroupDetailTbl.dtAddedOn
	FROM Scrl_UserGroupJoiningTbl
		 LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId = Scrl_UserGroupJoiningTbl.inGroupId
	WHERE Scrl_UserGroupJoiningTbl.intRegistrationId =@intRegistrationId AND IsAccepted=1 
END
ELSE IF @FlagNo=14
BEGIN
	INSERT INTO Scrl_GroupRoleDetailTbl(intUserId,inGroupId,strRoleName,intAddedBy,dtAddedOn,strIpAddress,strUniqueKey)VALUES(@intUserId,@inGroupId,@strRoleName,@intAddedBy,GetDate(),@strIpAddress,@strUniqueKey)
	 DECLARE @RoleID INT
	 SET @RoleID = @@IDENTITY;	
	 SELECT @RoleID;
END
 ELSE IF @FlagNo=15
	IF NOT EXISTS (SELECT 1 FROM Scrl_TempCotextDocument WHERE intSubjectCategoryId=@intSubjectCategoryId AND intAddedBy=@intAddedBy)
		BEGIN
			INSERT INTO Scrl_TempCotextDocument(intDocId,intSubjectCategoryId,intAddedBy,intModifiedBy,dtAddedOn)
			VALUES(@DocId,@intSubjectCategoryId,@intAddedBy,@intModifiedBy,GETDATE())
			END
	ELSE
		BEGIN
			DELETE FROM  Scrl_TempCotextDocument WHERE intSubjectCategoryId=@intSubjectCategoryId AND intAddedBy=@intAddedBy
		END
 ELSE IF @FlagNo=16
		BEGIN
			SELECT strCategoryName FROM Scrl_Category_SubjectTbl WHERE intCategoryId=@intSubjectCategoryId	
		END
 ELSE IF @FlagNo=17
		BEGIN
			SELECT intCategoryId,intSubjectCategoryId,strCategoryName from Scrl_CotextDocumentCreateGroup 
			INNER JOIN Scrl_Category_SubjectTbl ON Scrl_Category_SubjectTbl.intCategoryId=Scrl_CotextDocumentCreateGroup.intSubjectCategoryId 
			WHERE intGroupId=@inGroupId AND Scrl_CotextDocumentCreateGroup.intAddedBY=@intAddedBY
		END
ELSE IF @FlagNo=18
		BEGIN
			SELECT Scrl_Category_SubjectTbl.intCategoryId,strCategoryName
			FROM  Scrl_TempCotextDocument 
			LEFT JOIN Scrl_Category_SubjectTbl ON Scrl_Category_SubjectTbl.intCategoryId=Scrl_TempCotextDocument.intSubjectCategoryId
			WHERE Scrl_TempCotextDocument.intSubjectCategoryId=@intSubjectCategoryId AND Scrl_TempCotextDocument.intAddedBy=@intAddedBY	
			SELECT intPostQuestionId,replace(strFileName,substring(strFileName,20,len(strFileName)),'...') strFileName,CONVERT(VARCHAR(20),dtAddedOn,106) PublishON  FROM Scrl_QuestionAnsTbl WHERE intAddedBy=@intAddedBY 
		END
ELSE IF @FlagNo=19
		BEGIN
			SELECT intGrpRoleId,strRoleName FROM Scrl_GroupRoleDetailTbl WHERE strUniqueKey=@strUniqueKey
		END
ELSE IF @FlagNo=20
		BEGIN
			TRUNCATE TABLE Scrl_TempCotextDocument
		END
ELSE IF @FlagNo=21
BEGIN
	INSERT INTO Scrl_GroupMemberDetailsTbl(strMemberName,intRoleId,inGroupId,intAddedBy,dtAddedOn,strIpAddress,strUniqueKey,strInvitationMessage)VALUES(@inviteMembers,@intRoleId,@inGroupId,@intAddedBy,GetDate(),@strIpAddress,@strUniqueKey,@strInvitationMessage)
END
ELSE IF @FlagNo=22
		BEGIN
			SELECT strMemberName,intGrpInvitationMemberId,intRoleId,Scrl_GroupMemberDetailsTbl.intAddedBy,Scrl_GroupRoleDetailTbl.strRoleName,
			(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_GroupMemberDetailsTbl.strMemberName) Name
			FROM Scrl_GroupMemberDetailsTbl 
			INNER JOIN scrl_RegistrationTbl ON scrl_RegistrationTbl.intRegistrationId=Scrl_GroupMemberDetailsTbl.strMemberName
			INNER JOIN Scrl_GroupRoleDetailTbl ON Scrl_GroupRoleDetailTbl.intGrpRoleId=Scrl_GroupMemberDetailsTbl.intRoleId
			WHERE Scrl_GroupMemberDetailsTbl.strUniqueKey=@strUniqueKey
		END
ELSE IF @FlagNo=23
BEGIN
	DELETE FROM Scrl_GroupMemberDetailsTbl WHERE intGrpInvitationMemberId=@GrpInvitationMemberId
END
ELSE IF @FlagNo=24
BEGIN
	SELECT intGrpAccessid,strAccessPermission FROM Scrl_GroupModuleAccessDetailsTbl
END
ELSE IF @FlagNo=25
BEGIN
	INSERT INTO Scrl_GroupModulePermissionTbl(intGrpModuleId,intVisibility,intAddPermission,intEditPermission,intDeletePermission,intGroupId,strUniqueKey,intAddedBy,dtAddedOn,strIpAddress,intGrpRoleId)
	VALUES(@intGrpModuleId,@intVisibility,@intAddPermission,@intEditPermission,@intDeletePermission,@inGroupId,@strUniqueKey,@intAddedBy,GETDATE(),@strIpAddress,@intRoleId)
END
ELSE IF @FlagNo=26
BEGIN
	SELECT strMemberName,(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_GroupMemberDetailsTbl.strMemberName) Name
	,(SELECT vchrUserName FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_GroupMemberDetailsTbl.strMemberName)vchrUserName
	 FROM Scrl_GroupMemberDetailsTbl 
	WHERE strUniqueKey=@strUniqueKey
END
ELSE IF @FlagNo=27
BEGIN
	SELECT intGrpRoleId,strRoleName,intAddedBy FROM Scrl_GroupRoleDetailTbl WHERE strUniqueKey=@strUniqueKey
END
ELSE IF @FlagNo=28
BEGIN
	DELETE FROM Scrl_GroupRoleDetailTbl WHERE intGrpRoleId=@intGrpRoleId
	DELETE FROM Scrl_GroupModulePermissionTbl WHERE intGrpRoleId=@intGrpRoleId
END
/*Added By Dilip 28-2-2014*/
ELSE IF @FlagNo=29--Check group user join or not
BEGIN
        --For create User
       select count(intRegistrationId) from Scrl_UserGroupDetailTbl where inGroupId=@inGroupId and dbo.Scrl_UserGroupDetailTbl.intRegistrationId=@intRegistrationId

	   --For Join group user
	   SELECT count(dbo.Scrl_UserGroupJoiningTbl.intRegistrationId) [Count] FROM  dbo.Scrl_UserGroupJoiningTbl RIGHT OUTER JOIN
           dbo.Scrl_UserGroupDetailTbl ON dbo.Scrl_UserGroupJoiningTbl.inGroupId = dbo.Scrl_UserGroupDetailTbl.inGroupId 
		   where dbo.Scrl_UserGroupDetailTbl.inGroupId=@inGroupId and dbo.Scrl_UserGroupJoiningTbl.intRegistrationId=@intRegistrationId

     --For Role assign group user
		   SELECT count(dbo.Scrl_GroupMemberDetailsTbl.strMemberName) [Count] 
           FROM dbo.Scrl_UserGroupDetailTbl LEFT OUTER JOIN
           dbo.Scrl_GroupMemberDetailsTbl ON dbo.Scrl_UserGroupDetailTbl.inGroupId = dbo.Scrl_GroupMemberDetailsTbl.inGroupId
		   where dbo.Scrl_UserGroupDetailTbl.inGroupId=@inGroupId and cast(dbo.Scrl_GroupMemberDetailsTbl.strMemberName  as int)=@intRegistrationId
		   select (intRegistrationId) from Scrl_UserGroupDetailTbl where inGroupId=@inGroupId and dbo.Scrl_UserGroupDetailTbl.intRegistrationId=@intRegistrationId

END


ELSE IF @FlagNo=30
	 BEGIN
		IF NOT EXISTS (SELECT 1 FROM Scrl_OtherContextGroupDocument WHERE intSubjectCategoryId=@intSubjectCategoryId AND intAddedBy=@intAddedBy AND intGrpId=@inGroupId)
			BEGIN
				INSERT INTO Scrl_OtherContextGroupDocument(intGrpId,intSubjectCategoryId,dtAddedOn,intAddedBy)
				VALUES(@inGroupId,@intSubjectCategoryId,GETDATE(),@intAddedBy)
			END
	END

ELSE IF @FlagNo=31
	 BEGIN
                  SELECT Scrl_Category_SubjectTbl.intCategoryId,strCategoryName,0 CountSub 
			       FROM  Scrl_OtherContextGroupDocument 
			       LEFT JOIN Scrl_Category_SubjectTbl ON Scrl_Category_SubjectTbl.intCategoryId=Scrl_OtherContextGroupDocument.intSubjectCategoryId
			       WHERE Scrl_OtherContextGroupDocument.intGrpId=@inGroupId
	END

	ELSE IF @FlagNo=32
	 BEGIN
		 SELECT intCategoryId,strCategoryName,dtAddedOn,intAddedBy,dtModifiedOn,intModifiedBy,strIpAddress,
		(SELECT Count(1) FROM  Scrl_OtherContextGroupDocument 			     
		WHERE Scrl_OtherContextGroupDocument.intSubjectCategoryId=intCategoryId and Scrl_OtherContextGroupDocument.intGrpId=@inGroupId) CountSub
		FROM  Scrl_Category_SubjectTbl
	END

	ELSE IF @FlagNo=33
BEGIN 
	 SELECT inGroupId,strGroupName,strSummary,strAccess,strInvitationMessage,strLogoPath,strUniqueKey   FROM Scrl_UserGroupDetailTbl  WHERE inGroupId=@inGroupID
END

ELSE IF @FlagNo=34
	 BEGIN
	          
		    INSERT INTO Scrl_OtherContextGroupDocument(intGrpId,intSubjectCategoryId,dtAddedOn,intAddedBy)
			VALUES(@inGroupId,@intSubjectCategoryId,GETDATE(),@intAddedBy)

	 END

	 ELSE IF @FlagNo=35
	 BEGIN
       delete from Scrl_OtherContextGroupDocument where intGrpId=@inGroupID
  End


  ELSE IF @FlagNo=36
	 BEGIN
		SELECT intGrpRoleId,strRoleName FROM Scrl_GroupRoleDetailTbl WHERE  inGroupId=@inGroupId
	 END
	ELSE IF @FlagNo=37
	 BEGIN
		DECLARE @UserRoleID INT;
		SELECT @UserRoleID=intRoleId from Scrl_GroupMemberDetailsTbl WHERE  inGroupId=@inGroupId AND strMemberName =@intUserId ---AND intGrpRoleId=5
		select intModuleAccessRightId,intGrpRoleId,
		(select strRoleName FROM  Scrl_GroupRoleDetailTbl WHERE intGrpRoleId=@UserRoleID)RoleName,
		Scrl_GroupModulePermissionTbl.intGrpModuleId,strModuleName
		,intVisibility,intAddPermission,
		intEditPermission,intDeletePermission,intGroupId,strUniqueKey
		from Scrl_GroupModulePermissionTbl
		left outer JOIN Scrl_GroupModuleDetailTbl ON Scrl_GroupModuleDetailTbl.intGrpModuleId=Scrl_GroupModulePermissionTbl.intGrpModuleId
		WHERE intGrpRoleId=@UserRoleID
	 END
/*End*/

 ELSE IF @FlagNo=38
	 BEGIN
		delete from Scrl_GroupMemberDetailsTbl where strMemberName in (@inviteMembers) and  inGroupId=@inGroupId
	 END

 ELSE IF @FlagNo=39
	 BEGIN
		 select * from Scrl_UserGroupDetailTbl WHERE inGroupId=@inGroupId --and intRegistrationId=@intRegistrationId
	 END
ELSE IF @FlagNo=40
BEGIN
	SELECT intGrpRoleId,strRoleName,intAddedBy FROM Scrl_GroupRoleDetailTbl WHERE inGroupId=@inGroupId
END
ELSE IF @FlagNo=41
BEGIN
	SELECT strRoleName, intGrpModuleId,Scrl_GroupModulePermissionTbl.intVisibility,Scrl_GroupModulePermissionTbl.intAddPermission,
	Scrl_GroupModulePermissionTbl.intEditPermission,Scrl_GroupModulePermissionTbl.intDeletePermission,Scrl_GroupModulePermissionTbl.intGroupId,
	Scrl_GroupModulePermissionTbl.strUniqueKey FROM Scrl_GroupRoleDetailTbl
	join Scrl_GroupModulePermissionTbl ON Scrl_GroupRoleDetailTbl.intGrpRoleId=Scrl_GroupModulePermissionTbl.intGrpRoleId
	 WHERE Scrl_GroupRoleDetailTbl.intGrpRoleId=@intGrpRoleId
END
ELSE IF @FlagNo=42
		BEGIN
			SELECT strMemberName,intGrpInvitationMemberId,intRoleId,Scrl_GroupMemberDetailsTbl.intAddedBy,Scrl_GroupRoleDetailTbl.strRoleName,
			(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_GroupMemberDetailsTbl.strMemberName) Name
			FROM Scrl_GroupMemberDetailsTbl 
			INNER JOIN scrl_RegistrationTbl ON scrl_RegistrationTbl.intRegistrationId=Scrl_GroupMemberDetailsTbl.strMemberName
			INNER JOIN Scrl_GroupRoleDetailTbl ON Scrl_GroupRoleDetailTbl.intGrpRoleId=Scrl_GroupMemberDetailsTbl.intRoleId
			WHERE Scrl_GroupMemberDetailsTbl.intGrpInvitationMemberId=@GrpInvitationMemberId
		END
ELSE IF @FlagNo=43
		BEGIN
			SELECT strMemberName,intGrpInvitationMemberId,intRoleId,Scrl_GroupMemberDetailsTbl.intAddedBy,
			(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_GroupMemberDetailsTbl.strMemberName) Name
			FROM Scrl_GroupMemberDetailsTbl 
			INNER JOIN scrl_RegistrationTbl ON scrl_RegistrationTbl.intRegistrationId=Scrl_GroupMemberDetailsTbl.strMemberName
			WHERE Scrl_GroupMemberDetailsTbl.inGroupId=@inGroupId

		END
ELSE IF @FlagNo=44
		BEGIN
			SELECT intGrpRoleId,strRoleName FROM Scrl_GroupRoleDetailTbl WHERE inGroupId=@inGroupId
		END
ELSE IF @FlagNo=45
		BEGIN
			SELECT intGrpRoleId,strMemberName,intGrpInvitationMemberId,intRoleId,Scrl_GroupMemberDetailsTbl.intAddedBy,Scrl_GroupRoleDetailTbl.strRoleName,
			(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_GroupMemberDetailsTbl.strMemberName) Name
			FROM Scrl_GroupMemberDetailsTbl 
			INNER JOIN scrl_RegistrationTbl ON scrl_RegistrationTbl.intRegistrationId=Scrl_GroupMemberDetailsTbl.strMemberName
			INNER JOIN Scrl_GroupRoleDetailTbl ON Scrl_GroupRoleDetailTbl.intGrpRoleId=Scrl_GroupMemberDetailsTbl.intRoleId
			where Scrl_GroupMemberDetailsTbl.intRoleId=(
			SELECT intRoleId FROM Scrl_GroupMemberDetailsTbl			
			WHERE Scrl_GroupMemberDetailsTbl.intGrpInvitationMemberId=@GrpInvitationMemberId)
			
		END
ELSE IF @FlagNo=46
BEGIN

	UPDATE Scrl_GroupRoleDetailTbl 
	 SET 
	intUserId=@intUserId,
	strRoleName=@strRoleName,
	intModifiedBy=@intModifiedBy,
	dtModifiedOn= GetDate()
	WHERE intGrpRoleId = @intGrpRoleId

END
ELSE IF @FlagNo=47
BEGIN

	UPDATE Scrl_GroupMemberDetailsTbl 
	 SET 
	strInvitationMessage=@strInvitationMessage,
	intModifiedBy=@intModifiedBy,
	dtModifiedOn= GetDate(),
	strIpAddress=@strIpAddress,
	intRoleId=@intRoleId
	WHERE intGrpInvitationMemberId = @intGrpInvitationMemberId

END
 ELSE IF @FlagNo=48
	 BEGIN
		select IsAccepted from Scrl_UserGroupJoiningTbl where inGroupId=@inGroupId AND intRegistrationId =@intUserId
	 END
	  ELSE IF @FlagNo=49
	 BEGIN
		select intRoleId from Scrl_GroupMemberDetailsTbl where intRoleId=@inGroupId 
	 END
	  ELSE IF @FlagNo=50
	 BEGIN
				DELETE Scrl_GroupMemberDetailsTbl					
				WHERE strUniqueKey=@strUniqueKey AND inGroupId=@inGroupId

		select 1; 
	 END
	 ELSE IF @FlagNo=51
BEGIN
	IF NOT EXISTS (SELECT strUniqueKey FROM Scrl_GroupMemberDetailsTbl WHERE inGroupId=@inGroupId AND strMemberName=@inviteMembers AND intAddedBy= @intAddedBy)
		BEGIN
		INSERT INTO Scrl_GroupMemberDetailsTbl(strMemberName,intRoleId,inGroupId,intAddedBy,dtAddedOn,strIpAddress,strUniqueKey,strInvitationMessage)
		VALUES(@inviteMembers,@intRoleId,@inGroupId,@intAddedBy,GetDate(),@strIpAddress,@strUniqueKey,@strInvitationMessage)
		END
	END
	 ELSE IF @FlagNo=52
BEGIN
BEGIN
		UPDATE Scrl_GroupMemberDetailsTbl 
		SET 
		dtModifiedOn= GetDate(),
		intIsAccept=2
		WHERE inGroupId=@inGroupId AND strMemberName=@inviteMembers AND intAddedBy= @intAddedBy AND strUniqueKey=@strUniqueKey
		END
END
ELSE IF @FlagNo=53
		BEGIN
					SELECT 	COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_UserGroupJoiningTbl.dtAddedOn desc ) AS RowNumber,
					Scrl_UserGroupDetailTbl.inGroupId , Scrl_UserGroupJoiningTbl.intRegistrationId,Scrl_UserGroupDetailTbl.intRegistrationId GroupOwnerRegId,	
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) Name,
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrUserName,
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId) OwnerName,
					(SELECT TOP 1 COUNT(*) OVER()  AS Maxcount FROM	Scrl_UserGroupJoiningTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					WHERE	Scrl_UserGroupJoiningTbl.inGroupId=Scrl_UserGroupDetailTbl.inGroupId AND IsAccepted=1) GroupMembers,
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserGroupDetailTbl.intRegistrationId) OwnerUserName
			FROM	Scrl_UserGroupJoiningTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId	
			WHERE	Scrl_UserGroupJoiningTbl.inGroupId=@inGroupId  AND IsAccepted <> 2 
		END
ELSE IF @FlagNo=54
		BEGIN
			SELECT DISTINCT COUNT(*) OVER() AS Maxcount,* FROM(SELECT inGroupId,strGroupName,strLogoPath,strSummary,dtAddedOn FROM Scrl_UserGroupDetailTbl where intRegistrationId=@intRegistrationId
			 UNION ALL
			  SELECT Scrl_UserGroupDetailTbl.inGroupId,strGroupName,Scrl_UserGroupDetailTbl.strLogoPath,strSummary,Scrl_UserGroupDetailTbl.dtAddedOn
			   FROM   Scrl_UserGroupDetailTbl 
			    LEFT OUTER JOIN 
			     Scrl_UserGroupJoiningTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId		
				  WHERE Scrl_UserGroupJoiningTbl.intRegistrationId=@intRegistrationId AND Scrl_UserGroupJoiningTbl.IsAccepted=1			
				   ) AS T ORDER BY dtAddedOn DESC	  
		END
END

 
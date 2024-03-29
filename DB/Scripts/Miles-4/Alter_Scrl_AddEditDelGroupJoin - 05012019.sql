
/****** Object:  StoredProcedure [dbo].[Scrl_AddEditDelGroupJoin]    Script Date: 05-01-2019 12:44:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--  Scrl_AddEditDelGroupJoin 10,1,4,10,'',0,0,116,0
ALTER PROCEDURE [dbo].[Scrl_AddEditDelGroupJoin]  

@PageSize INT = NULL,
@Currentpage INT = NULL,
@FlagNo INT,
@inGroupId INT=NULL,
@strIpAddress VARCHAR(50)=NULL,
@IsAccepted INT=NULL,
@intAddedBy INT = NULL,
@intRequestJoinId INT=NULL,
@intRegistrationId INT=NULL,
@intInvitedUserId INT=NULL,
@txtSearch VARCHAR(50)=NULL

AS
BEGIN	
SET NOCOUNT ON
	DECLARE @UpperBand INT, @LowerBand INT
	 
	SET @LowerBand  = (@CurrentPage - 1) * @PageSize
	SET @UpperBand  = (@CurrentPage * @PageSize) + 1
	
IF @FlagNo=1
	BEGIN		
		--DELETE FROM Scrl_UserGroupJoiningTbl WHERE inGroupId=@inGroupId AND intAddedBy=@intAddedBy 
		IF NOT EXISTS(SELECT * FROM Scrl_UserGroupJoiningTbl WHERE inGroupId=@inGroupId AND intAddedBy=@intAddedBy )
			BEGIN
				INSERT INTO Scrl_UserGroupJoiningTbl  (inGroupId, intRegistrationId, intInvitedUserId, IsAccepted, dtAddedOn, intAddedBy, strIpAddress) 
				VALUES (@inGroupId, @intRegistrationId, @intInvitedUserId,@IsAccepted,GetDate(),@intAddedBy,@strIpAddress)
			END	
		ELSE
			BEGIN
				UPDATE Scrl_UserGroupJoiningTbl 
				SET IsAccepted=@IsAccepted, dtReqAcceptedDate=GETDATE(), intModifiedBy=@intAddedBy,dtModifiedOn=GETDATE(), strIpAddress=@strIpAddress,dtAddedOn=GETDATE()
				WHERE inGroupId=@inGroupId AND intAddedBy=@intAddedBy 
				if(@IsAccepted=2)
				BEGIN
					DELETE Scrl_GroupMemberDetailsTbl 
		            WHERE inGroupId=@inGroupId AND strMemberName=@intAddedBy 
				END
			END
	END

ELSE IF @FlagNo=2
	BEGIN		
		UPDATE Scrl_UserGroupJoiningTbl 
		SET IsAccepted=@IsAccepted, dtReqAcceptedDate=GETDATE(), intModifiedBy=@intAddedBy,dtModifiedOn=GETDATE(), strIpAddress=@strIpAddress,dtAddedOn=GETDATE()
		WHERE intRequestJoinId=@intRequestJoinId

		--DECLARE @Title1 VARCHAR(500)
		--SET  @Title1=(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') FROM  scrl_RegistrationTbl WHERE  intRegistrationId=(SELECT intAddedBy from Scrl_GroupMemberDetailsTbl where intGrpInvitationMemberId=@intRequestJoinId ))
		--SET @Title1 = @Title1 + ':' + (SELECT strGroupName FROM Scrl_UserGroupDetailTbl where inGroupId IN ( SELECT inGroupID  FROM Scrl_GroupMemberDetailsTbl WHERE intGrpInvitationMemberId=@intRequestJoinId))

		DELETE Scrl_GroupMemberDetailsTbl 
		WHERE intGrpInvitationMemberId=@intRequestJoinId

				DECLARE @Title1 VARCHAR(500)
		SET  @Title1=(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') FROM  scrl_RegistrationTbl WHERE  intRegistrationId=(SELECT intAddedBy from Scrl_UserGroupJoiningTbl where intRequestJoinId=@intRequestJoinId ))
		SET @Title1 = @Title1 + ':' + (SELECT strGroupName FROM Scrl_UserGroupDetailTbl where inGroupId IN ( SELECT inGroupID  FROM Scrl_UserGroupJoiningTbl WHERE intRequestJoinId=@intRequestJoinId))


		INSERT INTO Scrl_ActivityDetailsMaster(intID,strRepLiShStatus,strInviteeShare,strTitle,strMessage,dtAddedOn,intAddedBy,strIpAddress,strTableName)
		VALUES (@intRequestJoinId,'SH',@intAddedBy,@Title1,' ',getdate(),@intAddedBy,@strIpAddress, 'GroupDecline')

			
		
	END
	
		--This flag's 'Where' has been modified by shoaib jamadar according to new design
ELSE IF @FlagNo=4
	BEGIN	
	---exp: Swapnil TO Shoaib's Group
		SELECT   *    FROM
		(	  		
			SELECT	COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_UserGroupJoiningTbl.dtAddedOn desc ) AS RowNumber,
					
					Scrl_UserGroupJoiningTbl.intAddedby, intRequestJoinId JoinId,strGroupName,intRequestJoinId, Scrl_UserGroupJoiningTbl.intRegistrationId,	
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) Name,
					(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrPhotoPath,												
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrUserName,
					CONVERT(VARCHAR(12),dtReqAcceptedDate,109) dtReqAcceptedDate, 				
					CONVERT(VARCHAR(12),Scrl_UserGroupJoiningTbl.dtAddedOn,109) dtAddedOn,IsAccepted status,
									
					CASE	WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'
							WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>' 
							WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>' 		
					ELSE '' END 
					IsAccepted		   
			FROM	Scrl_UserGroupJoiningTbl 
					LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId
					LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId			
			WHERE	Scrl_UserGroupJoiningTbl.intRegistrationId=@intRegistrationId AND Scrl_UserGroupDetailTbl.inGroupId=@inGroupId	
			
			--SELECT	COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_UserGroupJoiningTbl.dtAddedOn desc ) AS RowNumber,
					
			--		intRequestJoinId Id,Scrl_UserGroupJoiningTbl.intAddedby RequestedUserId,strGroupName,Scrl_UserGroupJoiningTbl.intRegistrationId,intInvitedUserId,
			--		(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserGroupJoiningTbl.intAddedby) Name,		
			--		(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserGroupJoiningTbl.intAddedby) vchrPhotoPath,	
			--		(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserGroupJoiningTbl.intAddedby) vchrUserName,							
			--		CONVERT(VARCHAR(12),dtReqAcceptedDate,109) dtReqAcceptedDate, 				
			--		CONVERT(VARCHAR(12),Scrl_UserGroupJoiningTbl.dtAddedOn,109) dtAddedOn, 
									
			--		CASE	WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'
			--				WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>' 
			--				WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>' 		
			--		ELSE '' END 
			--		IsAccepted		   
			--FROM	Scrl_UserGroupJoiningTbl 
			--		LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId
			--		LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId
			--WHERE	Scrl_UserGroupJoiningTbl.intAddedby=@intAddedby	
				
		)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand  		
	END
	

ELSE IF @FlagNo=5
	BEGIN	
	---exp: Shoaib TO Swapnil
			SELECT   *    FROM
				(	
					SELECT	COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_UserGroupJoiningTbl.dtAddedOn desc ) AS RowNumber,
							
							Scrl_UserGroupJoiningTbl.intAddedby, intRequestJoinId Id,strGroupName, Scrl_UserGroupJoiningTbl.intRegistrationId,intInvitedUserId,	
							(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= intInvitedUserId) Name,
							(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= intInvitedUserId) vchrPhotoPath,												
							(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= intInvitedUserId) vchrUserName,
							CONVERT(VARCHAR(12),dtReqAcceptedDate,109) dtReqAcceptedDate, 				
							CONVERT(VARCHAR(12),Scrl_UserGroupJoiningTbl.dtAddedOn,109) dtAddedOn,IsAccepted status,
											
							CASE	WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'
									WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>' 
									WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>' 		
							ELSE '' END 
							IsAccepted		   
					FROM	Scrl_UserGroupJoiningTbl 
							RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId
							LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId			
					WHERE	Scrl_UserGroupJoiningTbl.intRegistrationId=@intRegistrationId
				)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand  		
	END
	
ELSE IF @FlagNo=6
	BEGIN	
	
		SELECT   *    FROM
		(	
			SELECT (SELECT TOP 1 COUNT(*) OVER() FROM	Scrl_UserGroupJoiningTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					WHERE	Scrl_UserGroupJoiningTbl.inGroupId=Scrl_UserGroupDetailTbl.inGroupId AND IsAccepted=1) Maxcount,
			0 AS RowNumber,					
					inGroupId Id, Scrl_UserGroupDetailTbl.intRegistrationId,Scrl_UserGroupDetailTbl.intRegistrationId GroupOwnerRegId,	
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId) Name,
					(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId) vchrPhotoPath,												
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId) vchrUserName,
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId) OwnerName,
					(SELECT TOP 1  strDesignation +'<br>' + strCompanyName  From [Scrl_UserExperienceTbl] WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId AND bitAtPresent=1 order by 1 desc) vchrInstituteName,					
					(SELECT TOP 1 COUNT(*) OVER()  AS Maxcount FROM	Scrl_UserGroupDetailTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupDetailTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					WHERE	Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupDetailTbl.inGroupId) GroupMembers,
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserGroupDetailTbl.intRegistrationId) OwnerUserName,
					--(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserGroupDetailTbl.intRegistrationId) OwnerUserPhoto,
					vchrCityName+' , '+vchrStateName CityState														   
			FROM	Scrl_UserGroupDetailTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupDetailTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					LEFT OUTER JOIN  Scrl_cityMasterTbl ON scrl_RegistrationTbl.vchrCity=Scrl_cityMasterTbl.intCityId		
					LEFT OUTER JOIN  Scrl_StateMasterTbl ON scrl_RegistrationTbl.vchrState=Scrl_StateMasterTbl.intStateId							
			WHERE	
					Scrl_UserGroupDetailTbl.inGroupId=@inGroupId
		)	AS TT
		
	  	UNION ALL

		SELECT   *    FROM
		(	
			SELECT 	COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_UserGroupJoiningTbl.dtAddedOn desc ) AS RowNumber,
					
					intRequestJoinId Id, Scrl_UserGroupJoiningTbl.intRegistrationId,Scrl_UserGroupDetailTbl.intRegistrationId GroupOwnerRegId,	
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) Name,
					(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrPhotoPath,												
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrUserName,
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId) OwnerName,
					(SELECT TOP 1  strDesignation +'<br>' + strCompanyName  From [Scrl_UserExperienceTbl] WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId AND bitAtPresent=1 order by 1 desc) vchrInstituteName,					
					(SELECT TOP 1 COUNT(*) OVER()  AS Maxcount FROM	Scrl_UserGroupJoiningTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					WHERE	Scrl_UserGroupJoiningTbl.inGroupId=Scrl_UserGroupDetailTbl.inGroupId AND IsAccepted=1) GroupMembers,
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserGroupDetailTbl.intRegistrationId) OwnerUserName,
					vchrCityName+' , '+vchrStateName CityState														   
			FROM	Scrl_UserGroupJoiningTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId	
					LEFT OUTER JOIN  Scrl_cityMasterTbl ON scrl_RegistrationTbl.vchrCity=Scrl_cityMasterTbl.intCityId		
					LEFT OUTER JOIN  Scrl_StateMasterTbl ON scrl_RegistrationTbl.vchrState=Scrl_StateMasterTbl.intStateId							
			WHERE	Scrl_UserGroupJoiningTbl.inGroupId=@inGroupId  AND IsAccepted <> 2  
					--Scrl_UserGroupJoiningTbl.inGroupId=@inGroupId  AND IsAccepted <> 2 
					AND
					(
					ISNULL(scrl_RegistrationTbl.vchrFirstName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(scrl_RegistrationTbl.vchrFirstName,'') ELSE '%' + @txtSearch + '%' END	             	
					OR
					ISNULL(scrl_RegistrationTbl.vchrLastName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(scrl_RegistrationTbl.vchrLastName,'') ELSE '%' + @txtSearch + '%' END					
					OR
					ISNULL(Scrl_cityMasterTbl.vchrCityName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(Scrl_cityMasterTbl.vchrCityName,'') ELSE '%' + @txtSearch + '%' END				
					OR
					ISNULL(Scrl_StateMasterTbl.vchrStateName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(Scrl_StateMasterTbl.vchrStateName,'') ELSE '%' + @txtSearch + '%' END				
					)
		)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand  		
	END
	
	ELSE IF @FlagNo=7
	BEGIN		
		UPDATE Scrl_UserGroupJoiningTbl 
		SET IsAccepted=Null, dtReqAcceptedDate=GETDATE(), intModifiedBy=@intAddedBy,dtModifiedOn=GETDATE(), strIpAddress=@strIpAddress
		WHERE inGroupId=@inGroupId AND intAddedBy=@intAddedBy 
		
	END

	ELSE IF @FlagNo=8
	BEGIN	
			
			SELECT 	COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_UserGroupJoiningTbl.dtAddedOn desc ) AS RowNumber,
					
					intRequestJoinId Id, Scrl_UserGroupJoiningTbl.intRegistrationId,Scrl_UserGroupDetailTbl.intRegistrationId GroupOwnerRegId,	
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) Name,
					(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrPhotoPath,												
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrUserName,
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId) OwnerName,
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserGroupDetailTbl.intRegistrationId) OwnerUserName,
					vchrInstituteName,vchrCityName+' , '+vchrStateName CityState														   
			FROM	Scrl_UserGroupJoiningTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId	
					LEFT OUTER JOIN  Scrl_cityMasterTbl ON scrl_RegistrationTbl.vchrCity=Scrl_cityMasterTbl.intCityId		
					LEFT OUTER JOIN  Scrl_StateMasterTbl ON scrl_RegistrationTbl.vchrState=Scrl_StateMasterTbl.intStateId							
			WHERE	
					Scrl_UserGroupJoiningTbl.inGroupId=@inGroupId  AND IsAccepted <> 2 AND Scrl_UserGroupJoiningTbl.intRegistrationId=@intRegistrationId
					AND
					(
						ISNULL(scrl_RegistrationTbl.vchrFirstName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(scrl_RegistrationTbl.vchrFirstName,'') ELSE '%' + @txtSearch + '%' END	             	
						OR
						ISNULL(scrl_RegistrationTbl.vchrLastName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(scrl_RegistrationTbl.vchrLastName,'') ELSE '%' + @txtSearch + '%' END					
						OR
						ISNULL(Scrl_cityMasterTbl.vchrCityName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(Scrl_cityMasterTbl.vchrCityName,'') ELSE '%' + @txtSearch + '%' END				
						OR
						ISNULL(Scrl_StateMasterTbl.vchrStateName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(Scrl_StateMasterTbl.vchrStateName,'') ELSE '%' + @txtSearch + '%' END				
					)
			
	END
ELSE IF @FlagNo=9
	BEGIN
		SELECT inGroupId,intAddedBy,intRegistrationId,(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) Name FROM Scrl_UserGroupJoiningTbl WHERE IsAccepted=1 AND inGroupId=@inGroupId
	END
ELSE IF @FlagNo =10
	BEGIN
			SELECT 	intRequestJoinId Id, Scrl_UserGroupJoiningTbl.intRegistrationId,Scrl_UserGroupDetailTbl.intRegistrationId GroupOwnerRegId,	
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) Name,
					(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrPhotoPath,												
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrUserName,
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId) OwnerName,
					(SELECT TOP 1 COUNT(*) OVER()  AS Maxcount FROM	Scrl_UserGroupJoiningTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					WHERE	Scrl_UserGroupJoiningTbl.inGroupId=Scrl_UserGroupDetailTbl.inGroupId AND IsAccepted=1) GroupMembers,
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserGroupDetailTbl.intRegistrationId) OwnerUserName,
					vchrInstituteName,vchrCityName+' , '+vchrStateName CityState														   
			FROM	Scrl_UserGroupJoiningTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId	
					LEFT OUTER JOIN  Scrl_cityMasterTbl ON scrl_RegistrationTbl.vchrCity=Scrl_cityMasterTbl.intCityId		
					LEFT OUTER JOIN  Scrl_StateMasterTbl ON scrl_RegistrationTbl.vchrState=Scrl_StateMasterTbl.intStateId							
			WHERE	
					Scrl_UserGroupJoiningTbl.inGroupId=@inGroupId  AND IsAccepted <> 2 
	END
	ELSE IF @FlagNo=11
	BEGIN
	SELECT   *    FROM
		(	
			SELECT 	COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_UserGroupJoiningTbl.dtAddedOn desc ) AS RowNumber,
					
					intRequestJoinId Id, Scrl_UserGroupJoiningTbl.intRegistrationId,Scrl_UserGroupDetailTbl.intRegistrationId GroupOwnerRegId,	
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) Name,
					(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrPhotoPath,												
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrUserName,
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId) OwnerName,
					(SELECT TOP 1  strDesignation +'<br>' + strCompanyName  From [Scrl_UserExperienceTbl] WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId AND bitAtPresent=1 order by 1 desc) vchrInstituteName,					
					(SELECT TOP 1 COUNT(*) OVER()  AS Maxcount FROM	Scrl_UserGroupJoiningTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					WHERE	Scrl_UserGroupJoiningTbl.inGroupId=Scrl_UserGroupDetailTbl.inGroupId AND IsAccepted=1) GroupMembers,
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserGroupDetailTbl.intRegistrationId) OwnerUserName,
					vchrCityName+' , '+vchrStateName CityState														   
			FROM	Scrl_UserGroupJoiningTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId	
					LEFT OUTER JOIN  Scrl_cityMasterTbl ON scrl_RegistrationTbl.vchrCity=Scrl_cityMasterTbl.intCityId		
					LEFT OUTER JOIN  Scrl_StateMasterTbl ON scrl_RegistrationTbl.vchrState=Scrl_StateMasterTbl.intStateId							
			WHERE	
					Scrl_UserGroupJoiningTbl.inGroupId=@inGroupId  AND IsAccepted <> 2 
					AND
					(
					scrl_RegistrationTbl.vchrFirstName+' '+scrl_RegistrationTbl.vchrLastName LIKE '%'+@txtSearch+'%'
					--ISNULL(scrl_RegistrationTbl.vchrFirstName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(scrl_RegistrationTbl.vchrFirstName,'') ELSE '%' + @txtSearch + '%' END	             	
					--OR
					--ISNULL(scrl_RegistrationTbl.vchrLastName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(scrl_RegistrationTbl.vchrLastName,'') ELSE '%' + @txtSearch + '%' END					
					OR
					ISNULL(Scrl_cityMasterTbl.vchrCityName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(Scrl_cityMasterTbl.vchrCityName,'') ELSE '%' + @txtSearch + '%' END				
					OR
					ISNULL(Scrl_StateMasterTbl.vchrStateName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(Scrl_StateMasterTbl.vchrStateName,'') ELSE '%' + @txtSearch + '%' END				
					)
		)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand  
		--UNION ALL

		--SELECT   *    FROM
		--(	
		--	SELECT 	COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_UserGroupJoiningTbl.dtAddedOn desc ) AS RowNumber,
					
		--			intRequestJoinId Id, Scrl_UserGroupJoiningTbl.intRegistrationId,Scrl_UserGroupDetailTbl.intRegistrationId GroupOwnerRegId,	
		--			(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) Name,
		--			(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrPhotoPath,												
		--			(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrUserName,
		--			(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId) OwnerName,
		--			(SELECT TOP 1  strDesignation +'<br>' + strCompanyName  From [Scrl_UserExperienceTbl] WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId AND bitAtPresent=1 order by 1 desc) vchrInstituteName,					
		--			(SELECT TOP 1 COUNT(*) OVER()  AS Maxcount FROM	Scrl_UserGroupJoiningTbl 
		--			RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
		--			WHERE	Scrl_UserGroupJoiningTbl.inGroupId=Scrl_UserGroupDetailTbl.inGroupId AND IsAccepted=1) GroupMembers,
		--			(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserGroupDetailTbl.intRegistrationId) OwnerUserName,
		--			vchrCityName+' , '+vchrStateName CityState														   
		--	FROM	Scrl_UserGroupJoiningTbl 
		--			RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
		--			LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId	
		--			LEFT OUTER JOIN  Scrl_cityMasterTbl ON scrl_RegistrationTbl.vchrCity=Scrl_cityMasterTbl.intCityId		
		--			LEFT OUTER JOIN  Scrl_StateMasterTbl ON scrl_RegistrationTbl.vchrState=Scrl_StateMasterTbl.intStateId							
		--	WHERE	
		--			Scrl_UserGroupJoiningTbl.inGroupId=@inGroupId  AND IsAccepted <> 2 
		--			AND
		--			(
		--			(ISNULL(scrl_RegistrationTbl.vchrFirstName,'')+''+ISNULL(scrl_RegistrationTbl.vchrLastName,''))Name LIKE CASE @txtSearch WHEN '' THEN (ISNULL(scrl_RegistrationTbl.vchrFirstName,'')+''+ISNULL(scrl_RegistrationTbl.vchrLastName,''))Name ELSE '%' + @txtSearch + '%' END	             	
		--			OR
		--			ISNULL(Scrl_cityMasterTbl.vchrCityName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(Scrl_cityMasterTbl.vchrCityName,'') ELSE '%' + @txtSearch + '%' END				
		--			OR
		--			ISNULL(Scrl_StateMasterTbl.vchrStateName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(Scrl_StateMasterTbl.vchrStateName,'') ELSE '%' + @txtSearch + '%' END				
		--			)
		--)	AS TT  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand  

	END


	ELSE IF @FlagNo=12
	BEGIN	
	
		SELECT   *    FROM
		(	
			SELECT (SELECT TOP 1 COUNT(*) OVER() FROM	Scrl_UserGroupJoiningTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					WHERE	Scrl_UserGroupJoiningTbl.inGroupId=Scrl_UserGroupDetailTbl.inGroupId AND IsAccepted=1) Maxcount,
			0 AS RowNumber,					
					inGroupId Id, Scrl_UserGroupDetailTbl.intRegistrationId,Scrl_UserGroupDetailTbl.intRegistrationId GroupOwnerRegId,	
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId) Name,
					(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId) vchrPhotoPath,												
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId) vchrUserName,
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId) OwnerName,
					(SELECT TOP 1  strDesignation +'<br>' + strCompanyName  From [Scrl_UserExperienceTbl] WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId AND bitAtPresent=1 order by 1 desc) vchrInstituteName,					
					(SELECT TOP 1 COUNT(*) OVER()  AS Maxcount FROM	Scrl_UserGroupDetailTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupDetailTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					WHERE	Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupDetailTbl.inGroupId) GroupMembers,
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserGroupDetailTbl.intRegistrationId) OwnerUserName,
					--(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserGroupDetailTbl.intRegistrationId) OwnerUserPhoto,
					vchrCityName+' , '+vchrStateName CityState														   
			FROM	Scrl_UserGroupDetailTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupDetailTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					LEFT OUTER JOIN  Scrl_cityMasterTbl ON scrl_RegistrationTbl.vchrCity=Scrl_cityMasterTbl.intCityId		
					LEFT OUTER JOIN  Scrl_StateMasterTbl ON scrl_RegistrationTbl.vchrState=Scrl_StateMasterTbl.intStateId							
			WHERE	
					Scrl_UserGroupDetailTbl.inGroupId=@inGroupId
		)	AS TT
		
	  	UNION ALL

		SELECT   *    FROM
		(	
			SELECT 	COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_UserGroupJoiningTbl.dtAddedOn desc ) AS RowNumber,
					
					intRequestJoinId Id, Scrl_UserGroupJoiningTbl.intRegistrationId,Scrl_UserGroupDetailTbl.intRegistrationId GroupOwnerRegId,	
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) Name,
					(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrPhotoPath,												
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrUserName,
					(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupDetailTbl.intRegistrationId) OwnerName,
					(SELECT TOP 1  strDesignation +'<br>' + strCompanyName  From [Scrl_UserExperienceTbl] WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId AND bitAtPresent=1 order by 1 desc) vchrInstituteName,					
					(SELECT TOP 1 COUNT(*) OVER()  AS Maxcount FROM	Scrl_UserGroupJoiningTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					WHERE	Scrl_UserGroupJoiningTbl.inGroupId=Scrl_UserGroupDetailTbl.inGroupId AND IsAccepted=1) GroupMembers,
					(SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserGroupDetailTbl.intRegistrationId) OwnerUserName,
					vchrCityName+' , '+vchrStateName CityState														   
			FROM	Scrl_UserGroupJoiningTbl 
					RIGHT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId	
					LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId	
					LEFT OUTER JOIN  Scrl_cityMasterTbl ON scrl_RegistrationTbl.vchrCity=Scrl_cityMasterTbl.intCityId		
					LEFT OUTER JOIN  Scrl_StateMasterTbl ON scrl_RegistrationTbl.vchrState=Scrl_StateMasterTbl.intStateId							
			WHERE	Scrl_UserGroupJoiningTbl.inGroupId=@inGroupId  AND IsAccepted > 0 AND IsAccepted <2 
					--Scrl_UserGroupJoiningTbl.inGroupId=@inGroupId  AND IsAccepted <> 2 
					AND
					(
					ISNULL(scrl_RegistrationTbl.vchrFirstName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(scrl_RegistrationTbl.vchrFirstName,'') ELSE '%' + @txtSearch + '%' END	             	
					OR
					ISNULL(scrl_RegistrationTbl.vchrLastName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(scrl_RegistrationTbl.vchrLastName,'') ELSE '%' + @txtSearch + '%' END					
					OR
					ISNULL(Scrl_cityMasterTbl.vchrCityName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(Scrl_cityMasterTbl.vchrCityName,'') ELSE '%' + @txtSearch + '%' END				
					OR
					ISNULL(Scrl_StateMasterTbl.vchrStateName,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(Scrl_StateMasterTbl.vchrStateName,'') ELSE '%' + @txtSearch + '%' END				
					)
		)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand  		
	END
		ELSE IF @FlagNo=13
	BEGIN	

	IF EXISTS(SELECT * FROM Scrl_UserRequestInvitationTbl WHERE (intRegistrationId=@intAddedBy AND intInvitedUserId=@intRegistrationId) AND IsAccepted=1)
		BEGIN
	SELECT * FROM Scrl_UserRequestInvitationTbl where intInvitedUserId=@intRegistrationId AND intAddedBy=@intAddedBy AND IsAccepted=1
	END
	ELSE
	BEGIN
	SELECT * FROM Scrl_UserRequestInvitationTbl where intInvitedUserId=@intAddedBy AND intAddedBy=@intRegistrationId AND IsAccepted=1
	END
	END
	ELSE IF @FlagNo=14
	BEGIN		
		UPDATE Scrl_GroupMemberDetailsTbl 
		SET intIsAccept=1
		WHERE intGrpInvitationMemberId=@intRequestJoinId

		--UPDATE Scrl_GroupMemberDetailsTbl 
		--SET intIsAccept=2
		--WHERE intGrpInvitationMemberId=@intRequestJoinId
		
	END
	ELSE IF @FlagNo=15
	BEGIN		

	DECLARE @Title VARCHAR(500)
		
		SET  @Title=(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') FROM  scrl_RegistrationTbl WHERE  intRegistrationId=(SELECT intAddedBy from Scrl_GroupMemberDetailsTbl where intGrpInvitationMemberId=@intRequestJoinId ))
		SET @Title = @Title + ':' + (SELECT strGroupName FROM Scrl_UserGroupDetailTbl where inGroupId IN ( SELECT inGroupID  FROM Scrl_GroupMemberDetailsTbl WHERE intGrpInvitationMemberId=@intRequestJoinId))

		DELETE Scrl_GroupMemberDetailsTbl 
		WHERE intGrpInvitationMemberId=@intRequestJoinId

		INSERT INTO Scrl_ActivityDetailsMaster(intID,strRepLiShStatus,strInviteeShare,strTitle,strMessage,dtAddedOn,intAddedBy,strIpAddress,strTableName)
		VALUES (@intRequestJoinId,'SH',@intAddedBy,@Title,' ',getdate(),@intAddedBy,@strIpAddress, 'GroupDecline')
		
	END
	ELSE IF @FlagNo=16
	BEGIN		
		UPDATE Scrl_GroupMemberDetailsTbl 
		SET intIsAccept=1
		WHERE inGroupId=@inGroupId
		AND intAddedBy=@intRegistrationId
		AND strMemberName=@intInvitedUserId
		
	END
	ELSE IF @FlagNo=17
	BEGIN		
		UPDATE Scrl_GroupMemberDetailsTbl 
		SET intIsAccept=2
		WHERE inGroupId=@inGroupId
		AND intAddedBy=@intRegistrationId
		AND strMemberName=@intInvitedUserId
	END
	ELSE IF @FlagNo=18
	BEGIN
	IF EXISTS(SELECT * FROM Scrl_UserGroupJoiningTbl WHERE inGroupId=@inGroupId AND intAddedBy=@intAddedBy )
			BEGIN
			SELECT 1;
			END
	END
	ELSE IF @FlagNo=19
	BEGIN
	 SELECT [intRequestJoinId]
       ,[inGroupId]
       ,[intRegistrationId]
       ,[intInvitedUserId]
       ,[IsAccepted]
       ,[dtReqAcceptedDate]
       ,[dtAddedOn]
       ,[intAddedBy]
       ,[dtModifiedOn]
       ,[intModifiedBy]
       ,[strIpAddress]
       ,[strTableName]
     FROM [Scrl_UserGroupJoiningTbl] where intRequestJoinId=@intRequestJoinId
	END
END



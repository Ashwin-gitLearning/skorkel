
/****** Object:  StoredProcedure [dbo].[SP_Scrl_GroupUserStatusUpdateTbl]    Script Date: 15-02-2019 11:40:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- SP_Scrl_GrpUserStatusUpdateTbl 6,10,1,0,0,1,null,null,0,null,0,null
ALTER PROCEDURE [dbo].[SP_Scrl_GroupUserStatusUpdateTbl]
@FlagNo AS INT,
@PageSize INT = NULL,
@Currentpage INT = NULL,
@intGrpStatusUpdateId	 AS int =NULL ,
@intRegistrationId	 AS int =NULL ,
@strPostDescription	 AS varchar(5000) =NULL ,
@dtAddedOn	 AS datetime =NULL ,
@intAddedBy	 AS int =NULL ,
--@dtModifiedOn	 AS datetime =NULL ,
@intModifiedBy	 AS int =NULL ,
@strIpAddress	 AS varchar(20) =NULL,
@strComment varchar (500)=NULL ,
@strPhotoPath varchar (100)=NULL ,
@strVideoPath varchar (100)=NULL ,
@intLikeId AS int= NULL,
@intLikeDisLike AS INT=NULL,
@intCommentId AS INT  = NULL,
@strPostType AS VARCHAR(50)=NULL,
@strPostLink AS VARCHAR(100)=NULL,
@FriendIds AS VARCHAR(100)=NULL,
@strMessage AS VARCHAR(200)=NULL,
@strLink AS VARCHAR(100)=NULL,
@strInvitee AS VARCHAR(100)=NULL,
@intGroupId	 AS int =NULL

AS
BEGIN

DECLARE @UpperBand INT, @LowerBand INT
SET @LowerBand  = (@CurrentPage - 1) * @PageSize
SET @UpperBand  = (@CurrentPage * @PageSize) + 1
	
IF @FlagNo=1
BEGIN
	 INSERT INTO Scrl_GrpUserStatusUpdateTbl  ( intRegistrationId,  strPostDescription,strPhotoPath,strVideoPath,strPostLink,intShared,strPostType, dtAddedOn, intAddedBy,  strIpAddress,intGroupId) 
	 VALUES (@intRegistrationId,@strPostDescription,@strPhotoPath,@strVideoPath,@strPostLink,0,@strPostType,GetDate(),@intAddedBy,@strIpAddress,@intGroupId)
END

ELSE IF @FlagNo=2
BEGIN
UPDATE Scrl_GrpUserStatusUpdateTbl 
 SET 	
	strPostDescription=@strPostDescription,
	strPhotoPath=@strPhotoPath,
	strVideoPath=@strVideoPath,
	strPostLink=@strPostLink,
	--intAddedBy=@intAddedBy,
	strPostType=@strPostType,
	dtModifiedOn= GetDate(),
	intModifiedBy=@intAddedBy,
	strIpAddress=@strIpAddress
	WHERE intGrpStatusUpdateId = @intGrpStatusUpdateId
END

ELSE IF @FlagNo=3
BEGIN
	DELETE FROM  Scrl_GrpUserStatusUpdateTbl  WHERE intGrpStatusUpdateId = @intGrpStatusUpdateId
END

ELSE IF @FlagNo=4
BEGIN
	SELECT  intGrpStatusUpdateId, intRegistrationId,  strPostDescription,strPhotoPath,strVideoPath,strPostLink,strPostType, dtAddedOn, intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress FROM Scrl_GrpUserStatusUpdateTbl WHERE intGrpStatusUpdateId = @intGrpStatusUpdateId AND intGroupId=@intGroupId
END

ELSE IF @FlagNo=5

BEGIN
	SELECT  intGrpStatusUpdateId, intRegistrationId,  strPostDescription,strPhotoPath,strVideoPath,strPostLink, dtAddedOn, intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress FROM Scrl_GrpUserStatusUpdateTbl 
END

ELSE IF @FlagNo=6
BEGIN
	
	SELECT   *    FROM
	(	  		
	

				SELECT	COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_GrpUserStatusUpdateTbl.dtAddedOn desc ) AS RowNumber,  
				Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId Id, intRegistrationId,
				(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intRegistrationId)UserName,
				(SELECT vchrPhotoPath FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intRegistrationId)vchrPhotoPath,
				(SELECT intUserTypeId FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intRegistrationId)intUserTypeId,				
				(SELECT COUNT(intLikeDisLike) FROM  Scrl_GrpLikeUserStatusTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId  AND intLikeDisLike=1)Likes,
				(SELECT COUNT(intStatusUpdateId) FROM  Scrl_GrpUserStatusUpdateChildTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId )Comments,
				(SELECT COUNT(intStatusUpdateId) FROM  Scrl_GrpShareUserStatusTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId )Share,							
				(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intSharedId)SharedUserName,
				(SELECT intUserTypeId FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intSharedId)intSharedUserTypeId,intShared,Scrl_GrpUserStatusUpdateTbl.intSharedId,
				strPostDescription,strPhotoPath,strVideoPath,strPostLink, null strDescription,
				CONVERT(varchar, Scrl_GrpUserStatusUpdateTbl.dtAddedOn, 106) dtAddedOn,
				DATEDIFF(HOUR, Scrl_GrpUserStatusUpdateTbl.dtAddedOn, GETDATE())dtHrs, 
				Scrl_GrpUserStatusUpdateTbl.intAddedBy, Scrl_GrpUserStatusUpdateTbl.dtModifiedOn, Scrl_GrpUserStatusUpdateTbl.intModifiedBy, Scrl_GrpUserStatusUpdateTbl.strIpAddress,Name,ContentType,Data,		
				strMessage,strLink
				
		FROM	Scrl_GrpUserStatusUpdateTbl 
		
		where
				(( intGroupId=@intGroupId  AND strInvitee IS NULL )	
					
				
				OR

				 (
					Scrl_GrpUserStatusUpdateTbl.intRegistrationId IN (SELECT * FROM dbo.CSVToTable(@FriendIds)) AND strInvitee IS NULL)
				 )
				 OR

				 (
					Scrl_GrpUserStatusUpdateTbl.intRegistrationId= CASE WHEN (SELECT id FROM dbo.CSVToTable(strInvitee) WHERE id=@intRegistrationId AND strInvitee IS NOT NULL) >1
					THEN Scrl_GrpUserStatusUpdateTbl.intRegistrationId  ELSE 0 END				 
				 )
				 AND intGroupId=@intGroupId 
)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand  
		
END


ELSE IF @FlagNo=11
BEGIN
	INSERT INTO Scrl_GrpUserStatusUpdateChildTbl( intStatusUpdateId,strComment,intUserId, dtAddedOn, intAddedBy,  strIpAddress,intGroupId) 
	VALUES (@intGrpStatusUpdateId,@strComment,@intAddedBy,GetDate(),@intAddedBy,@strIpAddress,@intGroupId)
END

ELSE IF @FlagNo=12
BEGIN
	SELECT intID, intStatusUpdateId,  strComment,intUserId,
	(SELECT COUNT(intLikeDisLike) FROM  Scrl_GrpLikeUserStatusUpdateTbl WHERE intCommentId=Scrl_GrpUserStatusUpdateChildTbl.intID  AND intLikeDisLike=1)Likes,

	(SELECT intUserId FROM  Scrl_GrpLikeUserStatusUpdateTbl WHERE intCommentId=Scrl_GrpUserStatusUpdateChildTbl.intID AND intUserId=@intAddedBy  AND intLikeDisLike=1)CommentLikeId,

	(SELECT vchrFirstName +' '+ISNULL(vchrLastName,'') FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateChildTbl.intUserId)UserName,
	(SELECT vchrPhotoPath FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateChildTbl.intUserId)vchrPhotoPath,
	(SELECT intUserTypeId FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateChildTbl.intUserId)intUserTypeId,
	(SELECT intRegistrationId FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateChildTbl.intUserId)intRegistrationId,
	CONVERT(VARCHAR, dtAddedOn, 106) dtAddedOn,
	DATEDIFF(hour, dtAddedOn, GETDATE())dtHrs, intAddedBy,  strIpAddress FROM Scrl_GrpUserStatusUpdateChildTbl where intStatusUpdateId=@intGrpStatusUpdateId AND intGroupId=@intGroupId
END

ELSE IF @FlagNo=13
	BEGIN
		IF (SELECT COUNT(intLikeId) FROM  Scrl_GrpLikeUserStatusUpdateTbl WHERE intUserId=@intAddedBy AND intCommentId=@intCommentId AND intGroupId=@intGroupId) = 0 
		BEGIN
			INSERT INTO Scrl_GrpLikeUserStatusUpdateTbl (intLikeDisLike,intCommentId,intUserId,intAddedBy,dtAddedOn,strIpAddress,intGroupId)
			VALUES(@intLikeDisLike,@intCommentId,@intAddedBy,@intAddedBy,GETDATE(),@strIpAddress,@intGroupId)
		
			SELECT 1 Action;
		END 

		ELSE if(SELECT COUNT(intLikeId) FROM  Scrl_GrpLikeUserStatusUpdateTbl WHERE intUserId=@intAddedBy AND intCommentId=@intCommentId AND intGroupId=@intGroupId and intLikeDisLike=0) = 1
		BEGIN 
		update Scrl_GrpLikeUserStatusUpdateTbl set
		intLikeDisLike=1,
		dtModifiedOn=getdate(),
		intModifiedBy=@intAddedBy,
		strIpAddress=@strIpAddress
		where 
		 intUserId=@intAddedBy AND intCommentId=@intCommentId and intLikeDisLike=0 AND intGroupId=@intGroupId

			SELECT 1 Action;
		END

	ELSE
		BEGIN 
			SELECT 0 Action;
		END
	END 
	
	ELSE IF @FlagNo=14
	BEGIN
		IF (SELECT COUNT(intLikeId) FROM  Scrl_GrpLikeUserStatusTbl WHERE intUserId=@intAddedBy AND intStatusUpdateId=@intGrpStatusUpdateId) = 0 
		BEGIN
			INSERT INTO Scrl_GrpLikeUserStatusTbl (intLikeDisLike,intStatusUpdateId,intUserId,intAddedBy,dtAddedOn,strIpAddress,intGroupId)
			VALUES(@intLikeDisLike,@intGrpStatusUpdateId,@intAddedBy,@intAddedBy,GETDATE(),@strIpAddress,@intGroupId)
		
			SELECT 1 Action;
		END 

	ELSE if(SELECT COUNT(intLikeId) FROM  Scrl_GrpLikeUserStatusTbl WHERE intUserId=@intAddedBy AND intStatusUpdateId=@intGrpStatusUpdateId and intLikeDisLike=0) = 1
		BEGIN 
		update Scrl_GrpLikeUserStatusTbl set
		intLikeDisLike=1,
		dtModifiedOn=getdate(),
		intModifiedBy=@intAddedBy,
		strIpAddress=@strIpAddress
		where 
		 intUserId=@intAddedBy AND intStatusUpdateId=@intGrpStatusUpdateId and intLikeDisLike=0

			SELECT 1 Action;
		END
			ELSE
		BEGIN 
			SELECT 0 Action;
		END
	END 
	
	--FOR SHARE ACTIVITY Where intSharedId is 'whose post you are sharing'
	ELSE IF @FlagNo=15
	BEGIN
		INSERT INTO Scrl_GrpUserStatusUpdateTbl  
			( intRegistrationId, strPostDescription,strPhotoPath,strVideoPath,strPostLink,intShared,intSharedId, dtAddedOn, intAddedBy,  strIpAddress,strMessage,strLink,strInvitee,intGroupId,intSharebyMe) 
		--VALUES (@intRegistrationId,@intGroupId,@strPostDescription,@strPhotoPath,@strDocumentPath,@strFileDescription,GetDate(),@intAddedBy,@strIpAddress)
		SELECT   
			@intRegistrationId,			
			(SELECT strPostDescription FROM Scrl_GrpUserStatusUpdateTbl WHERE intGrpStatusUpdateId = @intGrpStatusUpdateId) ,
			(SELECT strPhotoPath FROM Scrl_GrpUserStatusUpdateTbl WHERE intGrpStatusUpdateId = @intGrpStatusUpdateId) ,
			(SELECT strVideoPath FROM Scrl_GrpUserStatusUpdateTbl WHERE intGrpStatusUpdateId = @intGrpStatusUpdateId) ,	
			(SELECT strPostLink FROM Scrl_GrpUserStatusUpdateTbl WHERE intGrpStatusUpdateId = @intGrpStatusUpdateId) ,			
			1 , 
			(SELECT intRegistrationId FROM Scrl_GrpUserStatusUpdateTbl WHERE intGrpStatusUpdateId = @intGrpStatusUpdateId) ,
			GETDATE(),
			@intRegistrationId ,
			@strIpAddress,
			@strMessage,
			@strLink,
			@strInvitee,
			@intGroupId,
			1
			
		INSERT INTO Scrl_GrpShareUserStatusTbl (intStatusUpdateId,intUserId,intAddedBy,dtAddedOn,strIpAddress,intGroupId)
		VALUES(@intGrpStatusUpdateId,@intRegistrationId,@intRegistrationId,GETDATE(),@strIpAddress,@intGroupId)

		
			
	END
	
	--GET USER LIST OF POSTS LIKE
	ELSE IF @FlagNo=16
		BEGIN
			 SELECT intLikeId, intLikeDisLike,intStatusUpdateId, intUserId,
			(SELECT vchrPhotoPath FROM  scrl_RegistrationTbl WHERE  intRegistrationId= Scrl_GrpLikeUserStatusTbl.intUserId)vchrPhotoPath,
			(SELECT vchrFirstName +' '+ISNULL(vchrLastName,'') FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_GrpLikeUserStatusTbl.intUserId)UserName,
			(SELECT intUserTypeId FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_GrpLikeUserStatusTbl.intUserId)intUserTypeId,
			(SELECT intRegistrationId FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_GrpLikeUserStatusTbl.intUserId)intRegistrationId,
			 CONVERT(VARCHAR, dtAddedOn, 106) dtAddedOn, intAddedBy
			 FROM Scrl_GrpLikeUserStatusTbl 
			 WHERE intStatusUpdateId=@intGrpStatusUpdateId AND intGroupId=@intGroupId
		END

--GET USER LIST OF COMMENTS LIKE
	ELSE IF @FlagNo=17
		BEGIN
			 SELECT intLikeId, intLikeDisLike, intUserId,
			(SELECT vchrPhotoPath FROM  scrl_RegistrationTbl WHERE  intRegistrationId= Scrl_GrpLikeUserStatusUpdateTbl.intUserId)vchrPhotoPath,
			(SELECT vchrFirstName +' '+ISNULL(vchrLastName,'') FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_GrpLikeUserStatusUpdateTbl.intUserId)UserName,
			(SELECT intUserTypeId FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_GrpLikeUserStatusUpdateTbl.intUserId)intUserTypeId,
			(SELECT intRegistrationId FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_GrpLikeUserStatusUpdateTbl.intUserId)intRegistrationId,
			 CONVERT(VARCHAR, dtAddedOn, 106) dtAddedOn, intAddedBy
			 FROM Scrl_GrpLikeUserStatusUpdateTbl 
			 WHERE intCommentId=@intCommentId
		END

	ELSE IF @FlagNo=18
	BEGIN
					
	  		
		SELECT	COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_GrpUserStatusUpdateTbl.dtAddedOn desc ) AS RowNumber,  
				intGrpStatusUpdateId Id, intRegistrationId,
				(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intRegistrationId)UserName,
				(SELECT vchrPhotoPath FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intRegistrationId)vchrPhotoPath,
				(SELECT intUserTypeId FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intRegistrationId)intUserTypeId,				
				(SELECT COUNT(intLikeDisLike) FROM  Scrl_LikeUserStatusTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId  AND intLikeDisLike=1)Likes,
				(SELECT COUNT(intStatusUpdateId) FROM  Scrl_UserStatusUpdateChildTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId )Comments,
				(SELECT COUNT(intStatusUpdateId) FROM  Scrl_ShareUserStatusTbl WHERE intStatusUpdateId=Scrl_ShareUserStatusTbl.intStatusUpdateId )Share,				
				(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intSharedId)SharedUserName,
				(SELECT intUserTypeId FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intSharedId)intSharedUserTypeId,intShared,intSharedId,
				 strPostDescription,strPhotoPath,strVideoPath,strPostLink, null strDescription,
				convert(varchar, dtAddedOn, 106) dtAddedOn,
				DATEDIFF(hour, dtAddedOn, GETDATE())dtHrs, 
				intAddedBy, dtModifiedOn, intModifiedBy, strIpAddress,Name,ContentType,Data
		FROM	Scrl_GrpUserStatusUpdateTbl 
		where	intGrpStatusUpdateId=@intGrpStatusUpdateId
	
		
	END

ELSE IF @FlagNo=19 -- Friends Wall
	BEGIN
	IF(	(SELECT COUNT(*) FROM           Scrl_UserGroupJoiningTbl  where (intRegistrationId=@intAddedby OR intInvitedUserId=@intAddedby) and  IsAccepted=1
		AND  (intRegistrationId=@intRegistrationId OR intInvitedUserId=@intRegistrationId)) >0) -- If count greter means frnds
		BEGIN
		 
			SELECT   *    FROM
			(	  		
		 				
				SELECT	COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_GrpUserStatusUpdateTbl.dtAddedOn desc ) AS RowNumber,  
				Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId Id, intRegistrationId,
				(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intRegistrationId)UserName,
				(SELECT vchrPhotoPath FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intRegistrationId)vchrPhotoPath,
				(SELECT intUserTypeId FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intRegistrationId)intUserTypeId,				
				(SELECT COUNT(intLikeDisLike) FROM  Scrl_GrpLikeUserStatusTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId  AND intLikeDisLike=1)Likes,
								(SELECT intUserId  FROM  Scrl_GrpLikeUserStatusTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId  AND intLikeDisLike=1 AND intUserId=@intAddedBy)LikeUserPostId,

				(SELECT COUNT(intStatusUpdateId) FROM  Scrl_GrpUserStatusUpdateChildTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId )Comments,
				(SELECT COUNT(intStatusUpdateId) FROM  Scrl_GrpShareUserStatusTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId )Share,							
				(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intSharedId)SharedUserName,
				(SELECT intUserTypeId FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intSharedId)intSharedUserTypeId,intShared,Scrl_GrpUserStatusUpdateTbl.intSharedId,
				strPostDescription,strPhotoPath,strVideoPath,strPostLink, null strDescription,
				CONVERT(varchar, Scrl_GrpUserStatusUpdateTbl.dtAddedOn, 106) dtAddedOn,
				DATEDIFF(HOUR, Scrl_GrpUserStatusUpdateTbl.dtAddedOn, GETDATE())dtHrs, 
				Scrl_GrpUserStatusUpdateTbl.intAddedBy, Scrl_GrpUserStatusUpdateTbl.dtModifiedOn, Scrl_GrpUserStatusUpdateTbl.intModifiedBy, Scrl_GrpUserStatusUpdateTbl.strIpAddress,Name,ContentType,Data,		
				strMessage,strLink
				
		FROM	Scrl_GrpUserStatusUpdateTbl 
				WHERE (intAddedBy=@intRegistrationId OR intRegistrationId =@intRegistrationId)  AND intGroupId=@intGroupId

			)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand  
		
		END
ELSE -- Not a Friends Wall
BEGIN 
		SELECT   *    FROM
	(	  		
		 				
				SELECT	COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_GrpUserStatusUpdateTbl.dtAddedOn desc ) AS RowNumber,  
				Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId Id, intRegistrationId,
				(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intRegistrationId)UserName,
				(SELECT vchrPhotoPath FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intRegistrationId)vchrPhotoPath,
				(SELECT intUserTypeId FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intRegistrationId)intUserTypeId,				
				(SELECT COUNT(intLikeDisLike) FROM  Scrl_GrpLikeUserStatusTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId  AND intLikeDisLike=1)Likes,
								(SELECT intUserId  FROM  Scrl_GrpLikeUserStatusTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId  AND intLikeDisLike=1 AND intUserId=@intAddedBy)LikeUserPostId,

				(SELECT COUNT(intStatusUpdateId) FROM  Scrl_GrpUserStatusUpdateChildTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId )Comments,
				(SELECT COUNT(intStatusUpdateId) FROM  Scrl_GrpShareUserStatusTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId )Share,							
				(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intSharedId)SharedUserName,
				(SELECT intUserTypeId FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intSharedId)intSharedUserTypeId,intShared,Scrl_GrpUserStatusUpdateTbl.intSharedId,
				strPostDescription,strPhotoPath,strVideoPath,strPostLink, null strDescription,
				CONVERT(varchar, Scrl_GrpUserStatusUpdateTbl.dtAddedOn, 106) dtAddedOn,
				DATEDIFF(HOUR, Scrl_GrpUserStatusUpdateTbl.dtAddedOn, GETDATE())dtHrs, 
				Scrl_GrpUserStatusUpdateTbl.intAddedBy, Scrl_GrpUserStatusUpdateTbl.dtModifiedOn, Scrl_GrpUserStatusUpdateTbl.intModifiedBy, Scrl_GrpUserStatusUpdateTbl.strIpAddress,Name,ContentType,Data,		
				strMessage,strLink
				
		FROM	Scrl_GrpUserStatusUpdateTbl 
		WHERE (intAddedBy=@intRegistrationId OR intRegistrationId =@intRegistrationId) AND  strPostType='public'  AND intGroupId=@intGroupId

	)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand  
		END
	END

 ELSE IF @FlagNo=20
	BEGIN
		DELETE FROM Scrl_GrpUserStatusUpdateChildTbl WHERE intID=@intCommentId		
	END

 ELSE IF @FlagNo=21
	BEGIN
		SELECT intID,strComment FROM Scrl_GrpUserStatusUpdateChildTbl WHERE intID=@intCommentId
	END

 ELSE IF @FlagNo=22
	BEGIN
		UPDATE Scrl_GrpUserStatusUpdateChildTbl 
		SET strComment=@strComment,
			dtModifiedOn=GetDate(),
			intModifiedBy=@intAddedBy,
			strIpAddress=@strIpAddress
		WHERE intID=@intCommentId
	END

ELSE IF @FlagNo=23 -- Self Wall
BEGIN
	SELECT   *    FROM
	(	  			
		SELECT	COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_GrpUserStatusUpdateTbl.dtAddedOn desc ) AS RowNumber,  
				Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId Id, intRegistrationId,
				(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intRegistrationId)UserName,
				(SELECT vchrPhotoPath FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intRegistrationId)vchrPhotoPath,
				(SELECT intUserTypeId FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intRegistrationId)intUserTypeId,				
				(SELECT COUNT(intLikeDisLike) FROM  Scrl_GrpLikeUserStatusTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId  AND intLikeDisLike=1)Likes,

				(SELECT intUserId  FROM  Scrl_GrpLikeUserStatusTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId  AND intLikeDisLike=1 AND intUserId=@intAddedBy)LikeUserPostId,

				(SELECT COUNT(intStatusUpdateId) FROM  Scrl_GrpUserStatusUpdateChildTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId )Comments,
				(SELECT COUNT(intStatusUpdateId) FROM  Scrl_GrpShareUserStatusTbl WHERE intStatusUpdateId=Scrl_GrpUserStatusUpdateTbl.intGrpStatusUpdateId )Share,							
				(SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intSharedId)SharedUserName,
				(SELECT intUserTypeId FROM  scrl_RegistrationTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intSharedId)intSharedUserTypeId,intShared,Scrl_GrpUserStatusUpdateTbl.intSharedId,
				strPostDescription,strPhotoPath,strVideoPath,strPostLink, null strDescription,
				CONVERT(varchar, Scrl_GrpUserStatusUpdateTbl.dtAddedOn, 106) dtAddedOn,
				DATEDIFF(HOUR, Scrl_GrpUserStatusUpdateTbl.dtAddedOn, GETDATE())dtHrs, 
				Scrl_GrpUserStatusUpdateTbl.intAddedBy, Scrl_GrpUserStatusUpdateTbl.dtModifiedOn, Scrl_GrpUserStatusUpdateTbl.intModifiedBy, Scrl_GrpUserStatusUpdateTbl.strIpAddress,Name,ContentType,Data,		
				strMessage,strLink
				
		FROM	Scrl_GrpUserStatusUpdateTbl 
		
		WHERE	((ISNULL(intSharebyMe,0)=0 AND intAddedBy=@intAddedBy AND intRegistrationId =@intAddedBy ) OR (intRegistrationId =@intAddedBy AND ISNULL(intSharebyMe,0)=0)
				--(intAddedBy=@intAddedBy AND intRegistrationId =@intAddedBy) OR intRegistrationId =@intAddedBy
				--Below code to get if any shared data with you
				OR (1=
				(SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInvitee,',') AS a
				INNER JOIN dbo.Split(@intAddedBy,',') AS b ON a.Item = b.Item)
				THEN 1 ELSE  0 END matchId))
				--Get friends id & check for public post added by friend.   14-Apr-2014 by shoeb
				OR (				
				intAddedBy IN (SELECT intInvitedUserId intRegistrationId FROM	Scrl_UserRequestInvitationTbl WHERE	(Scrl_UserRequestInvitationTbl.intRegistrationId=@intAddedBy) AND IsAccepted=1 							
								UNION ALL 	
								SELECT Scrl_UserRequestInvitationTbl.intRegistrationId FROM Scrl_UserRequestInvitationTbl WHERE (Scrl_UserRequestInvitationTbl.intInvitedUserId=@intAddedBy) AND IsAccepted=1)				
								AND strPostType='public'))  AND intGroupId=@intGroupId

	)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand

END
	
	else if @FlagNo=24
	begin
				update Scrl_GrpLikeUserStatusTbl set
		intLikeDisLike=0,
		dtModifiedOn=getdate(),
		intModifiedBy=@intAddedBy,
		strIpAddress=@strIpAddress
		where 
		 intUserId=@intAddedBy AND intStatusUpdateId=@intGrpStatusUpdateId and intLikeDisLike=1
		
			SELECT 0 Action;


	end

	else if @FlagNo=25
	begin

				update Scrl_GrpLikeUserStatusUpdateTbl set
		intLikeDisLike=0,
		dtModifiedOn=getdate(),
		intModifiedBy=@intAddedBy,
		strIpAddress=@strIpAddress
		where 
		 intUserId=@intAddedBy AND intCommentId=@intCommentId and intLikeDisLike=1
		
			SELECT 0 Action;
		--END 


	end

END


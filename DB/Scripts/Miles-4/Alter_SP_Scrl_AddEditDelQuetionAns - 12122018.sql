
/****** Object:  StoredProcedure [dbo].[Scrl_AddEditDelQuetionAns]    Script Date: 12-12-2018 16:13:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER  PROCEDURE [dbo].[Scrl_AddEditDelQuetionAns]
@FlagNo int,
@PageSize INT = NULL,
@Currentpage INT = NULL,
@DocId INT=NULL,
@intPostQuestionId INT=NULL,
@intSubjectCategoryId INT=NULL,
@intModifiedBy INT=NULL,
@intContextId INT=NULL,
@FilePath VARCHAR(250)=NULL,
@intDocumentTypeID INT=NULL,
@strFileName VARCHAR(5000)=NULL,
@AddedBy INT=NULL,
@intTempContextId INT=NULL,
@strDocName VARCHAR(5000)=NULL,
@strQuestionDescription VARCHAR(MAX)=NULL,
@strIPAddress VARCHAR(500)=NULL,
@strRepLiShStatus  VARCHAR(200)=NULL,
@strComment VARCHAR(MAX)=NULL,
@PrvateMessage int=NUll,
@txtSearch VARCHAR(5000)=NULL,
@Ids VARCHAR(50)=NULL,
@intQAReplyLikeShareId INT=NULL,
@strInvitee VARCHAR(500)=NULL,
@strSharelink VARCHAR(5000)=null,
@intAnswerId INT=NULL,
@ID INT=NULL,
@QuesIds VARCHAR(200)=NULL
--@PageSize INT = NULL,
--@Currentpage INT = NULL

AS
BEGIN
		DECLARE @UpperBand INT, @LowerBand INT

SET @LowerBand  = (@CurrentPage - 1) * @PageSize
SET @UpperBand  = (@CurrentPage * @PageSize) + 1
	
              IF @FlagNo=1
			      BEGIN
					INSERT INTO Scrl_QuestionAnsTbl(strQuestionDescription,intContextId,strFileName,strFilePath,dtAddedOn,intAddedBy,strIPAddress)
					                         VALUES(@strQuestionDescription,@intContextId,@strFileName,@FilePath,GETDATE(),@AddedBy,@strIPAddress)
					DECLARE @intPostQueAnsId INT
					SET @intPostQueAnsId = @@IDENTITY;	
					SELECT @intPostQueAnsId;
					TRUNCATE TABLE Scrl_TempCotextDocument
				  END
			 ELSE IF @FlagNo=2
					IF NOT EXISTS (SELECT 1 FROM Scrl_TempCotextDocument WHERE intSubjectCategoryId=@intSubjectCategoryId AND intAddedBy=@AddedBy)
					BEGIN
					    INSERT INTO Scrl_TempCotextDocument(intDocId,intSubjectCategoryId,intAddedBy,intModifiedBy,dtAddedOn)
						VALUES(@DocId,@intSubjectCategoryId,@AddedBy,@intModifiedBy,GETDATE())
						
				     END
					ELSE
					  BEGIN
					    DELETE FROM  Scrl_TempCotextDocument WHERE intSubjectCategoryId=@intSubjectCategoryId AND intAddedBy=@AddedBy
					  END
             ELSE IF @FlagNo=3
			    BEGIN
					SELECT strCategoryName FROM Scrl_Category_SubjectTbl WHERE intCategoryId=@intSubjectCategoryId	
				END
                ELSE IF @FlagNo=4
			    BEGIN
					BEGIN
					SELECT intCategoryId,intSubjectCategoryId,strCategoryName from Scrl_CotextDocumentQA 
					INNER JOIN Scrl_Category_SubjectTbl ON Scrl_Category_SubjectTbl.intCategoryId=Scrl_CotextDocumentQA.intSubjectCategoryId 
					WHERE intPostQuestionId=@intPostQuestionId AND Scrl_CotextDocumentQA.intAddedBY=@AddedBy
					END
				END
				ELSE IF @FlagNo=5
				BEGIN
				SELECT Scrl_Category_SubjectTbl.intCategoryId,strCategoryName
			       FROM  Scrl_TempCotextDocument 
			       LEFT JOIN Scrl_Category_SubjectTbl ON Scrl_Category_SubjectTbl.intCategoryId=Scrl_TempCotextDocument.intSubjectCategoryId
			       WHERE Scrl_TempCotextDocument.intSubjectCategoryId=@intSubjectCategoryId AND Scrl_TempCotextDocument.intAddedBy=@AddedBy	
				   SELECT intPostQuestionId,replace(strFileName,substring(strFileName,20,len(strFileName)),'...') strFileName,CONVERT(VARCHAR(20),dtAddedOn,106) PublishON  FROM Scrl_QuestionAnsTbl WHERE intAddedBy=@AddedBy 
				END
				ELSE IF @FlagNo=6
				BEGIN
					IF NOT EXISTS (SELECT 1 FROM Scrl_CotextDocumentQA WHERE intSubjectCategoryId=@intSubjectCategoryId AND intAddedBy=@AddedBy AND intPostQuestionId=@intPostQuestionId)
					 BEGIN
					 INSERT INTO Scrl_CotextDocumentQA(intPostQuestionId,intSubjectCategoryId,dtAddedOn,intAddedBy)
					 VALUES(@intPostQuestionId,@intSubjectCategoryId,GETDATE(),@AddedBy)
					END
				END
				ELSE IF @FlagNo=7
					BEGIN
						SELECT TOP 8 intPostQuestionId,
						(SUBSTRING ( strQuestionDescription ,0 , 30 )+CASE WHEN LEN(strQuestionDescription)>30 THEN '...' ELSE '' END)strQuestionDescription
						,strFileName,strFilePath FROM Scrl_QuestionAnsTbl ORDER BY  intPostQuestionId DESC
					--WHERE intDocId=@DocId AND Scrl_CotextDocument.intAddedBY=@AddedBy
					END
              ELSE IF @FlagNo=8
			    BEGIN
					SELECT COUNT(intPostQuestionId)TotalQA FROM Scrl_QuestionAnsTbl 
				END
				ELSE IF @FlagNo=9
			    BEGIN
					SELECT intPostQuestionId,replace(strQuestionDescription,substring(strQuestionDescription,300,len(strQuestionDescription)),'...') strQuestionDescription,strFileName,strFilePath,CONVERT(VARCHAR(20), dtAddedOn,106)dtAddedOn,intAddedBy FROM Scrl_QuestionAnsTbl ORDER BY  intPostQuestionId   DESC
				
				END
				ELSE IF @FlagNo=10
			    BEGIN
					IF NOT EXISTS (SELECT 1 FROM Scrl_UserPostQAReplyTbl WHERE intPostQuestionId=@intPostQuestionId AND  strRepLiShStatus='LI' AND intAddedBy=@AddedBy)
						BEGIN
						if EXISTS (SELECT 1 FROM Scrl_UserPostQAReplyTbl WHERE intPostQuestionId=@intPostQuestionId AND  strRepLiShStatus='UnLI' AND intAddedBy=@AddedBy)
						BEGIN
						UPDATE Scrl_UserPostQAReplyTbl set
						strRepLiShStatus='LI',
						dtModifiedOn=getdate(),
						intModifiedBy=@AddedBy,
						strIpAddress=@strIpAddress
						where intPostQuestionId=@intPostQuestionId AND  strRepLiShStatus='UnLI' AND intAddedBy=@AddedBy
						select @intPostQuestionId
						END
						else 
						BEGIN
							INSERT INTO Scrl_UserPostQAReplyTbl(strRepLiShStatus,intPostQuestionId,strComment,dtAddedOn,intAddedBy,strIpAddress)VALUES(@strRepLiShStatus,@intPostQuestionId,@strComment,GETDATE(),@AddedBy,@strIpAddress)
							DECLARE @intNewForumReplyLikeShareId INT
							SET @intNewForumReplyLikeShareId = @@IDENTITY;	
							SELECT @intNewForumReplyLikeShareId;
						END
						END
						ELSE
						BEGIN
						UPDATE Scrl_UserPostQAReplyTbl set
						strRepLiShStatus='UnLI',
						dtModifiedOn=getdate(),
						intModifiedBy=@AddedBy,
						strIpAddress=@strIpAddress
						where intPostQuestionId=@intPostQuestionId AND  strRepLiShStatus='LI' AND intAddedBy=@AddedBy
						select @intPostQuestionId
						END

				END
				ELSE IF @FlagNo=11
				BEGIN
					DECLARE @TotalLike INT;
					DECLARE @TotalReply INT;
					DECLARE @TotalShare INT;
					DECLARE @LikeUserId INT;
					DECLARE @QuesLiStatus varchar(10);
					SELECT @TotalLike=COUNT(*) FROM Scrl_UserPostQAReplyTbl WHERE strRepLiShStatus='LI' AND intPostQuestionId=@intPostQuestionId
					SELECT @TotalReply=COUNT(*) FROM Scrl_UserPostQAReplyTbl WHERE strRepLiShStatus='CM' AND intPostQuestionId=@intPostQuestionId
   					SELECT @TotalShare=COUNT(*) FROM Scrl_UserPostQAReplyTbl WHERE strRepLiShStatus='SH' AND intPostQuestionId=@intPostQuestionId
					SELECT @LikeUserId=intAddedBy FROM Scrl_UserPostQAReplyTbl WHERE strRepLiShStatus='LI' AND intPostQuestionId=@intPostQuestionId AND intAddedBy=@AddedBy
					Select @QuesLiStatus=strRepLiShStatus FROM Scrl_UserPostQAReplyTbl WHERE strRepLiShStatus <> 'CM' AND intPostQuestionId=@intPostQuestionId AND intAddedBy=@AddedBy

					SELECT @TotalLike TotalLike,@TotalReply TotalReply ,@TotalShare TotalShare,@LikeUserId LikeUserId,@QuesLiStatus QuesLiStatus
				END
				ELSE IF @FlagNo=12
				BEGIN
					SELECT intPostQuestionId,Scrl_QuestionAnsTbl.intAddedBy,(vchrFirstName +' ' + ISNULL(vchrLastName,''))Name,strQuestionDescription,CONVERT(VARCHAR(20), Scrl_QuestionAnsTbl.dtAddedOn,106)dtAddedOn,Scrl_QuestionAnsTbl.intAddedBy 
					,(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_QuestionAnsTbl.intAddedBy) vchrPhotoPath
					FROM Scrl_QuestionAnsTbl 
					INNER JOIN scrl_RegistrationTbl ON Scrl_QuestionAnsTbl.intAddedBy =scrl_RegistrationTbl.intRegistrationId
					WHERE intPostQuestionId=@intPostQuestionId
				
				END
				ELSE IF @FlagNo=13
				BEGIN
						INSERT INTO Scrl_UserPostQAReplyTbl(strRepLiShStatus,intPostQuestionId,strComment,dtAddedOn,intAddedBy,strIpAddress,strInvitee,strTableName,strSharelink,strFileName)
						VALUES(@strRepLiShStatus,@intPostQuestionId,@strComment,GETDATE(),@AddedBy,@strIpAddress,@strInvitee,'Scrl_UserPostQAReplyTbl',@strSharelink,@strFileName)
						DECLARE @intShareId INT
						SET @intShareId = @@IDENTITY;	
						SELECT @intShareId;

						declare @strTitle varchar(1000)
						select @strTitle= strQuestionDescription  FROM Scrl_QuestionAnsTbl WHERE intPostQuestionId=@intPostQuestionId
						INSERT INTO [Scrl_ActivityDetailsMaster] ([intID],[strRepLiShStatus],[strInviteeShare],[strTitle] ,[strMessage],
						[dtAddedOn],[intAddedBy],[strIpAddress],[strTableName]) values(@intPostQuestionId,@strRepLiShStatus,@strInvitee,@strTitle
						 ,' ',GETDATE(),@AddedBy,@strIpAddress,'Scrl_QuestionAnsTbl')
				
				END
				 ELSE IF @FlagNo=14
			    BEGIN
					SELECT intPostQuestionId,strQuestionDescription,strFileName,strFilePath,CONVERT(VARCHAR(20), dtAddedOn,106)dtAddedOn,intAddedBy ,
					(SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName) 
						FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_QuestionAnsTbl.intAddedBy)AuthorName
							FROM Scrl_QuestionAnsTbl WHERE intPostQuestionId=@intPostQuestionId
				END
				ELSE IF @FlagNo=15
				BEGIN
				  INSERT INTO Scrl_UserPostQAReplyTbl(strRepLiShStatus,intPostQuestionId,strComment,dtAddedOn,intAddedBy,strIpAddress,strFileName,strFilePath,intPrivateMessage)VALUES(@strRepLiShStatus,@intPostQuestionId,@strComment,GETDATE(),@AddedBy,@strIpAddress,@strFileName,@FilePath,@PrvateMessage)
						DECLARE @intCommentId INT
						SET @intCommentId = @@IDENTITY;	
						SELECT @intCommentId;
				  
				END
				ELSE IF @FlagNo=16
				BEGIN
		--		 SELECT   *    FROM
		--(		
		--			 	SELECT  Scrl_QuestionAnsTbl.intPostQuestionId,strQuestionDescription,Scrl_QuestionAnsTbl.strFileName,Scrl_QuestionAnsTbl.strFilePath,CONVERT(VARCHAR(20), Scrl_QuestionAnsTbl.dtAddedOn,106)dtAddedOn,Scrl_QuestionAnsTbl.intAddedBy,
		--				COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER ( ORDER BY intQAReplyLikeShareId desc) AS RowNumber
		--				FROM Scrl_QuestionAnsTbl 
		--				INNER JOIN  Scrl_UserPostQAReplyTbl ON Scrl_QuestionAnsTbl.intPostQuestionId=Scrl_UserPostQAReplyTbl.intPostQuestionId
		--				WHERE strRepLiShStatus='LI' --ORDER BY  intQAReplyLikeShareId   DESC
		--				)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand 

		-- SELECT   *    FROM
		--(		
					 --	SELECT DISTINCT TOP 10 Scrl_QuestionAnsTbl.intPostQuestionId,strQuestionDescription,Scrl_QuestionAnsTbl.strFileName,Scrl_QuestionAnsTbl.strFilePath,CONVERT(VARCHAR(20), Scrl_QuestionAnsTbl.dtAddedOn,106)dtAddedOn,Scrl_QuestionAnsTbl.intAddedBy,
						--COUNT(*) OVER()  AS Maxcount --,ROW_NUMBER() OVER () AS RowNumber
						--,(SELECT COUNT(strRepLiShStatus) FROM  Scrl_UserPostQAReplyTbl WHERE intPostQuestionId=Scrl_QuestionAnsTbl.intPostQuestionId  AND strRepLiShStatus='LI')Likes
						--FROM Scrl_QuestionAnsTbl 
						--INNER JOIN  Scrl_UserPostQAReplyTbl ON Scrl_QuestionAnsTbl.intPostQuestionId=Scrl_UserPostQAReplyTbl.intPostQuestionId
						--WHERE strRepLiShStatus='LI' ORDER BY Likes DESC

					SELECT DISTINCT TOP 10 Scrl_QuestionAnsTbl.intPostQuestionId,strQuestionDescription,Scrl_QuestionAnsTbl.strFileName,Scrl_QuestionAnsTbl.strFilePath,CONVERT(VARCHAR(20), Scrl_QuestionAnsTbl.dtAddedOn,106)dtAddedOn,Scrl_QuestionAnsTbl.intAddedBy,
						COUNT(*) OVER()  AS Maxcount --,ROW_NUMBER() OVER () AS RowNumber
						,(SELECT COUNT(strRepLiShStatus) FROM  Scrl_UserPostQAReplyTbl WHERE intPostQuestionId=Scrl_QuestionAnsTbl.intPostQuestionId  AND strRepLiShStatus='CM')Likes
						FROM Scrl_QuestionAnsTbl 
						INNER JOIN  Scrl_UserPostQAReplyTbl ON Scrl_QuestionAnsTbl.intPostQuestionId=Scrl_UserPostQAReplyTbl.intPostQuestionId
						WHERE strRepLiShStatus='CM' ORDER BY Likes DESC


					--	)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand 

				END
            ELSE IF @FlagNo=17
				BEGIN
						--SELECT intQAReplyLikeShareId, replace(strComment,substring(strComment,110,len(strComment)),'....') strComment,Scrl_UserPostQAReplyTbl.intAddedBy,(vchrFirstName +' '+ ISNULL(vchrMiddleName,'') +' ' + ISNULL(vchrLastName,''))Name,intPrivateMessage,Scrl_UserPostQAReplyTbl.intAddedBy
						--,(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) vchrPhotoPath,CONVERT(VARCHAR(20), Scrl_UserPostQAReplyTbl.dtAddedOn,106)dtAddedOn,strFileName,strFilePath
						--FROM Scrl_UserPostQAReplyTbl 
						-- INNER JOIN scrl_RegistrationTbl ON Scrl_UserPostQAReplyTbl.intAddedBy =scrl_RegistrationTbl.intRegistrationId
						-- WHERE strRepLiShStatus='CM' AND intPostQuestionId=@intPostQuestionId
						-- ORDER BY intQAReplyLikeShareId DESC
						SELECT intQAReplyLikeShareId, 
							strComment,Scrl_UserPostQAReplyTbl.intAddedBy,
							(vchrFirstName +' ' + ISNULL(vchrLastName,''))Name,
							intPrivateMessage,
							Scrl_UserPostQAReplyTbl.intAddedBy,
							(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) vchrPhotoPath,
							CONVERT(VARCHAR(20), Scrl_UserPostQAReplyTbl.dtAddedOn,106)dtAddedOn,
							strFileName,
							strFilePath,
							(SELECT COUNT(*) From Scrl_UserPostQAAnsReplyTbl 
								WHERE Scrl_UserPostQAAnsReplyTbl.intAnswerId = Scrl_UserPostQAReplyTbl.intQAReplyLikeShareId AND
									Scrl_UserPostQAAnsReplyTbl.strAnsLiStatus='LI')numLikes
						  FROM Scrl_UserPostQAReplyTbl 
						 INNER JOIN scrl_RegistrationTbl ON Scrl_UserPostQAReplyTbl.intAddedBy =scrl_RegistrationTbl.intRegistrationId
						 WHERE strRepLiShStatus='CM' AND intPostQuestionId=@intPostQuestionId
						 ORDER BY numLikes DESC, intQAReplyLikeShareId ASC

				END
			ELSE IF @FlagNo=18
				BEGIN
				    SELECT COUNT(intQAReplyLikeShareId)TotalReplies FROM Scrl_UserPostQAReplyTbl WHERE strRepLiShStatus='CM' AND intPostQuestionId=@intPostQuestionId
				END

			ELSE IF @FlagNo=19
				BEGIN
					--SELECT intPostQuestionId,strQuestionDescription,strFileName,strFilePath,CONVERT(VARCHAR(20), dtAddedOn,106)dtAddedOn FROM Scrl_QuestionAnsTbl 
					--WHERE ISNULL(strQuestionDescription,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(strQuestionDescription,'') ELSE '%' + @txtSearch + '%' END
					--DECLARE @Ids VARCHAR(50)
					--IF @Ids ='' OR  @Ids ='0'
					SELECT DISTINCT replace(strQuestionDescription,substring(strQuestionDescription,300,len(strQuestionDescription)),'...') strQuestionDescription,strFileName,strFilePath,Scrl_QuestionAnsTbl.intPostQuestionId,CONVERT(VARCHAR(20), Scrl_QuestionAnsTbl.dtAddedOn,106)dtAddedOn FROM Scrl_QuestionAnsTbl 
					LEFT OUTER JOIN Scrl_CotextDocumentQA ON Scrl_CotextDocumentQA.intPostQuestionId =Scrl_QuestionAnsTbl.intPostQuestionId
					WHERE ISNULL(strQuestionDescription,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(strQuestionDescription,'') ELSE '%' + @txtSearch + '%' END
					AND  intSubjectCategoryId IN (SELECT item from dbo.split(CASE WHEN LEN(@Ids)>0 THEN @Ids ELSE CONVERT(VARCHAR(25),intSubjectCategoryId) END,','))
	                --SET @Ids='1,2,4,5'
					--SELECT intSubjectCategoryId,Scrl_QuestionAnsTbl.intPostQuestionId,strQuestionDescription,strFileName,strFilePath,CONVERT(VARCHAR(20), Scrl_QuestionAnsTbl.dtAddedOn,106)dtAddedOn FROM Scrl_QuestionAnsTbl 
					--LEFT OUTER JOIN Scrl_CotextDocumentQA ON Scrl_CotextDocumentQA.intPostQuestionId =Scrl_QuestionAnsTbl.intPostQuestionId
					--WHERE ISNULL(strQuestionDescription,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(strQuestionDescription,'') ELSE '%' + @txtSearch + '%' END
					--OR  (Charindex(cast(intSubjectCategoryId AS VARCHAR), @Ids) > 0 )
				END
           ELSE IF @FlagNo=20
				BEGIN
					TRUNCATE TABLE Scrl_TempCotextDocument
				END  
            ELSE IF @FlagNo=21
				BEGIN  
					SELECT intQAReplyLikeShareId,strComment,Scrl_UserPostQAReplyTbl.intAddedBy,(vchrFirstName +' ' + ISNULL(vchrLastName,''))Name,intPrivateMessage,Scrl_UserPostQAReplyTbl.intAddedBy
					,(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) vchrPhotoPath,CONVERT(VARCHAR(20), Scrl_UserPostQAReplyTbl.dtAddedOn,106)dtAddedOn,strFileName,strFilePath
					FROM Scrl_UserPostQAReplyTbl 
					INNER JOIN scrl_RegistrationTbl ON Scrl_UserPostQAReplyTbl.intAddedBy =scrl_RegistrationTbl.intRegistrationId
					WHERE strRepLiShStatus='CM' AND intPostQuestionId=@intPostQuestionId
					ORDER BY intQAReplyLikeShareId DESC
				END   
			 ELSE IF @FlagNo=22
				BEGIN  
					SELECT intQAReplyLikeShareId,strComment,Scrl_UserPostQAReplyTbl.intAddedBy,(vchrFirstName +' ' + ISNULL(vchrLastName,''))Name,intPrivateMessage,Scrl_UserPostQAReplyTbl.intAddedBy
					,(SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) vchrPhotoPath,CONVERT(VARCHAR(20), Scrl_UserPostQAReplyTbl.dtAddedOn,106)dtAddedOn,strFileName,strFilePath
					FROM Scrl_UserPostQAReplyTbl 
					INNER JOIN scrl_RegistrationTbl ON Scrl_UserPostQAReplyTbl.intAddedBy =scrl_RegistrationTbl.intRegistrationId
					WHERE strRepLiShStatus='CM' AND intQAReplyLikeShareId=@intQAReplyLikeShareId

				END   
			ELSE IF @FlagNo=23
				BEGIN  
					SELECT intDocId,IsDocsSale,replace(strDocName,substring(strDocName,20,len(strDocName)),'...') strDocName,strDocTitle,CONVERT(VARCHAR(20),dtAddedOn,106) PublishON  FROM Scrl_DocumentTbl WHERE intAddedBy=@AddedBy AND IsPublish='Y'
				END   
			ELSE IF @FlagNo=24
				BEGIN  
					SELECT COUNT(intDocId)TotalPublDocs  FROM Scrl_DocumentTbl WHERE intAddedBy=@AddedBy AND IsPublish='Y'

				END  
            ELSE IF @FlagNo=25
				BEGIN  
					SELECT TOP 4 intDocId,strDocTitle,CONVERT(VARCHAR(20), dtModifiedOn,106)dtModifiedOn,CONVERT(VARCHAR(20),dtAddedOn,106)dtAddedOn 
                     FROM Scrl_DocumentTbl WHERE intAddedBy=@AddedBy
				END  
			ELSE IF @FlagNo=26
				BEGIN  
					SELECT COUNT(intDocId)TotalDocsCount  FROM Scrl_DocumentTbl WHERE intAddedBy=@AddedBy 
				END 
			ELSE IF @FlagNo=27
				BEGIN
					TRUNCATE TABLE Scrl_TempCotextDocument
				END
			ELSE IF @FlagNo=28
				BEGIN
				--Newly Added
					DELETE from Scrl_UserPostQAAnsReplyTbl where intAnswerId IN 
					(Select intQAReplyLikeShareId from Scrl_UserPostQAReplyTbl WHERE intPostQuestionId=@intPostQuestionId)
					DELETE from Scrl_UserPostQAReplyTbl WHERE intPostQuestionId=@intPostQuestionId
					DELETE from Scrl_QuestionAnsTbl WHERE intPostQuestionId=@intPostQuestionId					
				END
				
			ELSE IF @FlagNo=29
				 BEGIN	

						IF @Ids = ''
						BEGIN
						SELECT   *    FROM(
							SELECT DISTINCT strQuestionDescription,ROW_NUMBER() OVER ( ORDER BY Scrl_QuestionAnsTbl.dtAddedOn desc) AS RowNumber,
							COUNT(*) OVER()  AS Maxcount ,
							strFileName,strFilePath,Scrl_QuestionAnsTbl.intPostQuestionId,CONVERT(VARCHAR(20), Scrl_QuestionAnsTbl.dtAddedOn,106)dtAddedOn,Scrl_QuestionAnsTbl.intAddedBy FROM Scrl_QuestionAnsTbl 
							LEFT OUTER JOIN Scrl_CotextDocumentQA ON Scrl_CotextDocumentQA.intPostQuestionId =Scrl_QuestionAnsTbl.intPostQuestionId
							WHERE (@txtSearch = '' OR ISNULL(strQuestionDescription,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(strQuestionDescription,'') ELSE '%' + @txtSearch + '%' END)
						)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand 
						END
						ELSE
						BEGIN
						SELECT   *    FROM(
							SELECT DISTINCT strQuestionDescription,ROW_NUMBER() OVER ( ORDER BY Scrl_QuestionAnsTbl.dtAddedOn desc) AS RowNumber,
							COUNT(*) OVER()  AS Maxcount ,
							strFileName,strFilePath,Scrl_QuestionAnsTbl.intPostQuestionId,CONVERT(VARCHAR(20), Scrl_QuestionAnsTbl.dtAddedOn,106)dtAddedOn,Scrl_QuestionAnsTbl.intAddedBy FROM Scrl_QuestionAnsTbl 
							LEFT OUTER JOIN Scrl_CotextDocumentQA ON Scrl_CotextDocumentQA.intPostQuestionId =Scrl_QuestionAnsTbl.intPostQuestionId
							WHERE (@txtSearch = '' OR ISNULL(strQuestionDescription,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(strQuestionDescription,'') ELSE '%' + @txtSearch + '%' END)
							AND  intSubjectCategoryId IN (SELECT item from dbo.split(CASE WHEN LEN(@Ids)>0 THEN @Ids ELSE CONVERT(VARCHAR(25),intSubjectCategoryId) END,','))
						)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand 
						END
				END
				ELSE IF @FlagNo=30
			    BEGIN
			    SELECT   *    FROM
				(		
				SELECT COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER ( ORDER BY Scrl_QuestionAnsTbl.dtAddedOn desc) AS RowNumber,
				intPostQuestionId, strQuestionDescription,strFileName,strFilePath,CONVERT(VARCHAR(20), dtAddedOn,106)dtAddedOn,intAddedBy
				FROM Scrl_QuestionAnsTbl 
				--SELECT COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER ( ORDER BY Scrl_QuestionAnsTbl.dtAddedOn desc) AS RowNumber,
				--intPostQuestionId,replace(strQuestionDescription,substring(strQuestionDescription,300,len(strQuestionDescription)),'') strQuestionDescription,strFileName,strFilePath,CONVERT(VARCHAR(20), dtAddedOn,106)dtAddedOn,intAddedBy
				-- FROM Scrl_QuestionAnsTbl 
				--SELECT COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER ( ORDER BY Scrl_QuestionAnsTbl.dtAddedOn desc) AS RowNumber,
				--intPostQuestionId,(SUBSTRING ( strQuestionDescription ,0 , 300 )+'...<span style="color:#C7C7C7"> read more</span>')strQuestionDescription,strFileName,strFilePath,CONVERT(VARCHAR(20), dtAddedOn,106)dtAddedOn,intAddedBy
				-- FROM Scrl_QuestionAnsTbl 
				)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand 
				END
				ELSE IF @FlagNo=31
				BEGIN
				SELECT intQAReplyLikeShareId, (SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName) 
			 FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserPostQAReplyTbl.intAddedBy)UserName 
			 FROM Scrl_UserPostQAReplyTbl WHERE strRepLiShStatus='LI' AND intPostQuestionId=@intPostQuestionId
			 order by intQAReplyLikeShareId desc

				END				

				ELSE IF @FlagNo=32
				BEGIN
				SELECT intPostQuestionId,strQuestionDescription FROM Scrl_QuestionAnsTbl
			    WHERE intPostQuestionId=@intPostQuestionId

				END		
				ELSE IF @FlagNo=33
				BEGIN
				 SELECT Scrl_Category_SubjectTbl.intCategoryId,strCategoryName
					FROM  Scrl_CotextDocumentQA 
					LEFT JOIN Scrl_Category_SubjectTbl ON Scrl_Category_SubjectTbl.intCategoryId=Scrl_CotextDocumentQA.intSubjectCategoryId
					WHERE Scrl_CotextDocumentQA.intPostQuestionId=@intPostQuestionId
				END	
				ELSE IF @FlagNo=34
			    BEGIN				 
					--SELECT intCategoryId,strCategoryName,dtAddedOn,intAddedBy,dtModifiedOn,intModifiedBy,strIpAddress,
					--(SELECT Count(1) FROM  Scrl_CotextDocumentQA 			     
					--WHERE Scrl_CotextDocumentQA.intSubjectCategoryId=intCategoryId and Scrl_CotextDocumentQA.intPostQuestionId=@intPostQuestionId) CountSub
					--FROM  Scrl_Category_SubjectTbl	
					SELECT intCategoryId,strCategoryName,Scrl_Category_SubjectTbl.dtAddedOn,Scrl_Category_SubjectTbl.intAddedBy,Scrl_Category_SubjectTbl.dtModifiedOn,Scrl_Category_SubjectTbl.intModifiedBy,strIpAddress,
					(SELECT Count(1) FROM  Scrl_CotextDocumentQA 			     
					WHERE Scrl_CotextDocumentQA.intSubjectCategoryId=intCategoryId and Scrl_CotextDocumentQA.intPostQuestionId=@intPostQuestionId) CountSub
					FROM  Scrl_Category_SubjectTbl	
					LEFT JOIN Scrl_CotextDocumentQA ON Scrl_Category_SubjectTbl.intCategoryId=Scrl_CotextDocumentQA.intSubjectCategoryId
					WHERE Scrl_CotextDocumentQA.intPostQuestionId=@intPostQuestionId				  
				END	
				ELSE IF @FlagNo=35
			    BEGIN				 
					UPDATE Scrl_QuestionAnsTbl SET strQuestionDescription=@strQuestionDescription,strIPAddress=@strIPAddress,dtModifiedOn=GETDATE(),intModifiedBy=@AddedBy
					WHERE intPostQuestionId=@intPostQuestionId			  
				END	
				ELSE IF @FlagNo=36
			    BEGIN				 
					DELETE Scrl_CotextDocumentQA 
					WHERE intPostQuestionId=@intPostQuestionId			  
				END	
				ELSE IF @FlagNo=37
			    BEGIN				 
					SELECT intQAReplyLikeShareId,strRepLiShStatus,intPostQuestionId,strComment FROM Scrl_UserPostQAReplyTbl
					WHERE intQAReplyLikeShareId=@intQAReplyLikeShareId			  
				END	
				ELSE IF @FlagNo=38
			    BEGIN				 
					UPDATE  Scrl_UserPostQAReplyTbl SET
					strComment=@strComment,intModifiedBy=@AddedBy,dtModifiedOn=GETDATE()
					WHERE intQAReplyLikeShareId=@intQAReplyLikeShareId	
					--intQAReplyLikeShareId,strRepLiShStatus,intPostQuestionId,strComment		  
				END	
				ELSE IF @FlagNo=39
			    BEGIN			
					--Newly Added
					DELETE Scrl_UserPostQAAnsReplyTbl 
					WHERE intAnswerid=@intQAReplyLikeShareId
					 
					DELETE  Scrl_UserPostQAReplyTbl 
					WHERE intQAReplyLikeShareId=@intQAReplyLikeShareId	
					--intQAReplyLikeShareId,strRepLiShStatus,intPostQuestionId,strComment
						  
				END
				--Newly Added
				ELSE IF @FlagNo=40
			    BEGIN
				INSERT INTO
					Scrl_UserPostQAAnsReplyTbl(intAnswerId,strAnsLiStatus,strComment,dtAddedOn,intAnsAddedBy)
					VALUES(@intAnswerId,@strRepLiShStatus,@strComment,GETDATE(),@AddedBy)
						DECLARE @intCommentId1 INT
						SET @intCommentId1 = @@IDENTITY;	
						SELECT @intCommentId1;
				END
				--Newly Added
				ELSE IF @FlagNo=41
			    BEGIN				 
					IF NOT EXISTS (SELECT 1 FROM Scrl_UserPostQAAnsReplyTbl WHERE intAnswerId=@intAnswerId AND strAnsLiStatus='LI' AND intAnsAddedBy=@AddedBy)
					 BEGIN					
					  IF EXISTS (SELECT 1 FROM Scrl_UserPostQAAnsReplyTbl WHERE intAnswerId=@intAnswerId AND strAnsLiStatus='UnLI' AND intAnsAddedBy=@AddedBy)
					   BEGIN
						UPDATE Scrl_UserPostQAAnsReplyTbl 
							Set
							strAnsLiStatus='LI',
							dtAddedOn=getdate()	
							where intAnswerId=@intAnswerId AND strAnsLiStatus='UnLI' AND intAnsAddedBy=@AddedBy
							select @intAnswerId
						END
						ELSE 
						BEGIN
							INSERT INTO Scrl_UserPostQAAnsReplyTbl(strAnsLiStatus,intAnswerId,strComment,dtAddedOn,intAnsAddedBy)
							VALUES(@strRepLiShStatus,@intAnswerId,@strComment,GETDATE(),@AddedBy)
							DECLARE @intId INT
							SET @intId = @@IDENTITY;	
							SELECT @intId;
						END
						END
						ELSE
						BEGIN
							UPDATE Scrl_UserPostQAAnsReplyTbl 
							SET
							strAnsLiStatus='UnLI',
							dtAddedOn=getdate(),
							intAnsAddedBy=@AddedBy	
							where intAnswerId=@intAnswerId AND strAnsLiStatus='LI' AND intAnsAddedBy=@AddedBy
							select @intAnswerId
						END	  
				END
				ELSE IF @FlagNo=42
				BEGIN
					DECLARE @TotalLikeAns INT;
					DECLARE @LikeUserAnsId INT;
					DECLARE @TotalComment INT;
					DECLARE @Commentdesc varchar(max);
					DECLARE @AnsLiStatus varchar(10);
					
					SELECT @TotalLikeAns=COUNT(*) FROM Scrl_UserPostQAAnsReplyTbl WHERE strAnsLiStatus='LI' AND intAnswerId=@intAnswerId
					SELECT @LikeUserAnsId=intAnsAddedBy FROM Scrl_UserPostQAAnsReplyTbl WHERE strAnsLiStatus='LI' AND intAnswerId=@intAnswerId AND intAnsAddedBy=@AddedBy
					--Modified
					SELECT @TotalComment=COUNT(*) FROM Scrl_UserPostQAAnsReplyTbl WHERE strAnsLiStatus='CM' AND intAnswerId=@intAnswerId
					SELECT @Commentdesc=strComment FROM Scrl_UserPostQAAnsReplyTbl WHERE strAnsLiStatus='CM' AND intAnswerId=@intAnswerId
					Select @AnsLiStatus=strAnsLiStatus FROM Scrl_UserPostQAAnsReplyTbl WHERE strAnsLiStatus <> 'CM' AND intAnswerId=@intAnswerId AND intAnsAddedBy=@AddedBy

					SELECT @TotalLikeAns TotalLike,@LikeUserAnsId LikeUserId,@TotalComment TotalComment,@Commentdesc Commentdesc,@AnsLiStatus AnsLiStatus 
				END
				ELSE IF @FlagNo=43
				BEGIN
				 
				 INSERT INTO Scrl_UserPostQAAnsReplyTbl(strAnsLiStatus,intAnswerId,strComment,dtAddedOn,intAnsAddedBy)
							VALUES(@strRepLiShStatus,@intAnswerId,@strComment,GETDATE(),@AddedBy)
					DECLARE @intCommentId2 INT
					SET @intCommentId2 = @@IDENTITY;	
					SELECT @intCommentId2;								  
				END

				ELSE IF @FlagNo=44
				BEGIN
				  Select QAA.id as ID,QAA.strComment as strComment, CONVERT(VARCHAR, QAA.dtAddedOn, 106) as dtAddedOn,
				   CONCAT(Reg.vchrFirstName, ' ' ,Reg.vchrMiddleName, ' ' ,Reg.vchrLastName) as UserName,
					Reg.vchrPhotoPath as PhotoPath 
				    from Scrl_UserPostQAAnsReplyTbl QAA
				     Inner Join scrl_RegistrationTbl Reg
				      On Reg.intRegistrationId = QAA.intAnsAddedBy 
				       WHERE strAnsLiStatus='CM' AND strComment is Not Null 
					   AND intAnswerId=@intAnswerId
				        ORDER BY QAA.dtAddedOn ASC, intAnswerId DESC				  
				END

				ELSE IF @FlagNo=45
			    BEGIN			
					--Newly Added
					DELETE Scrl_UserPostQAAnsReplyTbl 
					WHERE id=@ID						  
				END

				ELSE IF @FlagNo=46
			    BEGIN			
					--Newly Added					
					Select strComment as strComment, dtAddedOn, intAnsAddedBy, intAnswerId 
				     from Scrl_UserPostQAAnsReplyTbl 
				      WHERE strAnsLiStatus='CM' AND strComment is Not Null 
					   AND Id=@ID						  
				END

				ELSE IF @FlagNo=47
			    BEGIN			
					--Newly Added
					Update Scrl_UserPostQAAnsReplyTbl 
					Set strComment=@strComment,
					dtAddedOn=getdate()
					WHERE Id=@ID
					--DECLARE @intCommentId3 INT
					--SET @intCommentId3 = @@IDENTITY;	
					--SELECT @intCommentId3;	  
				END
				ELSE IF @FlagNo=48
			    BEGIN			
				 --Newly Added
				 WITH UserParms AS
				 (
				 	SELECT ItemNumber,Item
				 	FROM dbo.[DelimitedSplit8K](@QuesIds, ',')
				 )
				 SELECT * FROM
				 (		
				 	SELECT COUNT(*) OVER() AS Maxcount,ROW_NUMBER() OVER(ORDER BY Scrl_QuestionAnsTbl.dtAddedOn desc) AS RowNumber,
				 	intPostQuestionId,strQuestionDescription,strFileName,strFilePath,CONVERT(VARCHAR(20),dtAddedOn,106)dtAddedOn,intAddedBy
				 	FROM Scrl_QuestionAnsTbl 
				 )	AS T JOIN UserParms As U ON intPostQuestionId = U.Item WHERE intPostQuestionId IN (Select Item from UserParms) ORDER BY U.ItemNumber
				   
				END
     END
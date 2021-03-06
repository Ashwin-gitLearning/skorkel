
/****** Object:  StoredProcedure [dbo].[Scrl_AddEditDelGroupDocument]    Script Date: 18-04-2019 16:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[Scrl_AddEditDelGroupDocument]
@FlagNo int,
@PageSize INT = NULL,
@Currentpage INT = NULL,
@intUploadDocId INT=NULL,
@intRegistrationId INT=NULL,
@intGroupId INT=NULL,
@strDocTitle VARCHAR(200)=NULL,
@FilePath VARCHAR(200)=NULL,
@StrDocsDetails VARCHAR(500)=NULL,
@IsDocsSale VARCHAR(2)=NULL,
@Price FLOAT=NULL,
@IsDocsDownload VARCHAR(2)=NULL, 
@AddedBy INT=NULL,
@intModifiedBy INT=NULL,
@strIpAddress VARCHAR(20)=NULL,
@IsFolder VARCHAR(2)=NULL,
@strFolderName VARCHAR(100)=NULL,
@intFolderId INT=NULL,
@intParentId INT=NULL,
@strAuthors VARCHAR(200)=NULL,
@intDocsSee Varchar(20)=NULL,
@intDocumentTypeID  INT=NULL,
@intSubjectCategoryId INT=NULL,
@strIsPublic varchar(20)=NULL,
@strFolderDescription varchar(500)=null,
@strDocName varchar(500)=null
AS
BEGIN
			IF @FlagNo=1
			  BEGIN
			    IF NOT EXISTS ( SELECT 1 FROM Scrl_GroupUploadDouments WHERE intUploadDocId=@intUploadDocId)
				 BEGIN
			      INSERT INTO Scrl_GroupUploadDouments(intRegistrationId,intGroupId,strDocTitle,strAuthors,intDocumentTypeID,strFilePath,StrDocsDetails,isDocsSale,Price,isDownloadable,intDocsSee,dtAddedOn,intAddedBy,strIpAddress,IsFolder,strFolderName,intFolderId,intParentId,strDocName)
				  VALUES(@intRegistrationId,@intGroupId,@strDocTitle,@strAuthors,@intDocumentTypeID,@FilePath,@StrDocsDetails,@IsDocsSale,@Price,@IsDocsDownload,@intDocsSee,GETDATE(),@AddedBy,@strIpAddress,@IsFolder,@strFolderName,@intFolderId,@intParentId,@strDocName)
				  DECLARE @intDocId INT
					SET @intDocId = @@IDENTITY;	
					SELECT @intDocId;
				  --TRUNCATE TABLE Scrl_TempDocUpload
				  END
				  ELSE
				  BEGIN
				   INSERT INTO Scrl_GroupUploadChildDouments(intUploadDocId,intRegistrationId,intGroupId,strDocTitle,strAuthors,intDocumentTypeID,strFilePath,StrDocsDetails,isDocsSale,Price,isDownloadable,intDocsSee,dtAddedOn,intAddedBy,strIpAddress,IsFolder)
				   VALUES(@intUploadDocId,@intRegistrationId,@intGroupId,@strDocTitle,@strAuthors,@intDocumentTypeID,@FilePath,@StrDocsDetails,@IsDocsSale,@Price,@IsDocsDownload,@intDocsSee,GETDATE(),@AddedBy,@strIpAddress,@IsFolder)
				 
					SET @intDocId = @@IDENTITY;	
					SELECT @intDocId;
				  --TRUNCATE TABLE Scrl_TempDocUpload
				  END 
			  END
			  ELSE IF @FlagNo=2
			   BEGIN
			     SELECT intUploadDocId,strFilePath,strDocTitle,StrDocsDetails,intAddedBy,intDocsSee, intDocumentTypeID FROM Scrl_GroupUploadDouments WHERE  IsFolder='N' AND intGroupId=@intGroupId --AND intParentId=0
			   END
			   ELSE IF @FlagNo=3
			   BEGIN
			     SELECT intUploadDocId,strFilePath,strDocTitle,StrDocsDetails,intAddedBy,intDocsSee FROM Scrl_GroupUploadDouments WHERE    IsFolder='Y'
			   END
			    ELSE IF @FlagNo=4
			   BEGIN
					
					 INSERT INTO Scrl_GroupCotextDocument(intGroupDocId,intSubjectCategoryId,dtAddedOn,intAddedBy)
					 VALUES(@intUploadDocId,@intSubjectCategoryId,GETDATE(),@AddedBy)
			  -- SELECT  intUploadDocId,strFilePath,strDocTitle,StrDocsDetails  FROM  Scrl_GroupUploadChildDouments WHERE intUploadDocId=@intUploadDocId
				-- SELECT intUploadDocId,strFilePath,strDocTitle,StrDocsDetails FROM Scrl_GroupUploadDouments WHERE intUploadDocId=@intUploadDocId
				 --SELECT * FROM(
					--		  SELECT intUploadDocId,strFilePath,strDocTitle,StrDocsDetails 
					--		  FROM Scrl_GroupUploadDouments UNION ALL 
					--		  SELECT intUploadDocId,strFilePath,strDocTitle,StrDocsDetails 
					--		  FROM Scrl_GroupUploadChildDouments )T WHERE intUploadDocId=@intUploadDocId
			   END
			   ELSE IF @FlagNo=5
			   BEGIN
			     SELECT intUploadDocId,strFilePath,strDocTitle,StrDocsDetails FROM Scrl_GroupUploadChildDouments WHERE  IsFolder='Y'
			   END
			   ELSE IF @FlagNo=6
			   BEGIN
			     SELECT intUploadDocId,strFilePath,strDocTitle,StrDocsDetails,intChildUploadDocId FROM Scrl_GroupUploadChildDouments WHERE  IsFolder='Y' AND intUploadDocId=@intUploadDocId
			   END
			   ELSE IF @FlagNo=7
			   BEGIN
			      INSERT INTO Scrl_GroupFolderDetails(intRegistrationId,intGroupId,strDocTitle,strFilePath,StrDocsDetails,isDocsSale,Price,isDownloadable,dtAddedOn,intAddedBy,strIpAddress,IsFolder,strFolderName,intParentId,strIsPublic,strFolderDescription)
				  VALUES(@intRegistrationId,@intGroupId,@strDocTitle,@FilePath,@StrDocsDetails,@IsDocsSale,@Price,@IsDocsDownload,GETDATE(),@AddedBy,@strIpAddress,@IsFolder,@strFolderName,@intParentId,@strIsPublic,@strFolderDescription)

				    DECLARE @FolderId INT
					SET @FolderId = @@IDENTITY;	
					SELECT @FolderId;
					
			   END
			   ELSE IF @FlagNo=8
			   BEGIN
			      SELECT  IsFolder,strFolderName FROM Scrl_GroupFolderDetails WHERE intFolderId=@intFolderId
			   END
			   ELSE IF @FlagNo=9
			   BEGIN
			      SELECT  intFolderId,IsFolder,strFolderName,intParentId,intRegistrationId,strIsPublic,strFolderDescription FROM Scrl_GroupFolderDetails WHERE intParentId=0 AND intGroupId=@intGroupId
				  --SELECT  Scrl_GroupFolderDetails.intFolderId,Scrl_GroupFolderDetails.IsFolder,Scrl_GroupFolderDetails.strFolderName,Scrl_GroupFolderDetails.intParentId 
      --            ,Scrl_GroupUploadDouments.strFilePath,Scrl_GroupUploadDouments.strDocTitle,Scrl_GroupUploadDouments.StrDocsDetails FROM Scrl_GroupFolderDetails
      --            INNER JOIN Scrl_GroupUploadDouments ON Scrl_GroupUploadDouments.intGroupId=Scrl_GroupFolderDetails.intGroupId
      --            WHERE Scrl_GroupFolderDetails.intParentId=0
				 
			   END
			   ELSE IF @FlagNo=10
			   BEGIN
			      SELECT  intUploadDocId,intRegistrationId,intGroupId,strDocTitle,strFilePath,StrDocsDetails,isDocsSale,intAddedBy,intDocsSee, intDocumentTypeID FROM Scrl_GroupUploadDouments 
                  WHERE intFolderId=@intFolderId and  intGroupId=@intGroupId
			   END 
			    ELSE IF @FlagNo=11
			   BEGIN
			      SELECT intFolderId,IsFolder,strFolderName,intParentId,intRegistrationId,strIsPublic,strFolderDescription FROM Scrl_GroupFolderDetails WHERE  intFolderId=@intFolderId
			   END
			   ELSE IF @FlagNo=12
			   BEGIN
			      SELECT intFolderId,IsFolder,strFolderName,intParentId,intRegistrationId,strIsPublic,strFolderDescription FROM Scrl_GroupFolderDetails WHERE  intParentId=@intParentId
			   END
			   ELSE IF @FlagNo=13
			   BEGIN
			       SELECT COUNT(*)TotalFolder FROM Scrl_GroupFolderDetails  WHERE intParentId=@intParentId
			    -- SELECT strFilePath,strDocTitle,StrDocsDetails FROM Scrl_GroupUploadDouments WHERE  IsFolder='Y' AND  intFolderId=@intFolderId--intParentId=@intParentId
			   END
			   ELSE IF @FlagNo=14
			   BEGIN
			     SELECT COUNT(*)  AS TotalDocs FROM Scrl_GroupUploadDouments  WHERE intFolderId=@intFolderId
			   END
			   ELSE IF @FlagNo=15
			   BEGIN
			     SELECT strFolderName,intParentId,intFolderId FROM Scrl_GroupUploadDouments WHERE intParentId=5
			   END
			   ELSE IF @FlagNo=16
				BEGIN
				TRUNCATE TABLE Scrl_TempDocUpload
				END
                
				ELSE IF @FlagNo=17--Update  Folder Name
				BEGIN
				update  Scrl_GroupFolderDetails  set
				intRegistrationId=@intRegistrationId,
				intGroupId=@intGroupId,
				strDocTitle=@strDocTitle,
				dtAddedOn=GETDATE(),
				intAddedBy=@AddedBy,
				strFolderName=@strFolderName,
				dtModifiedOn=GETDATE(),
				intModifiedBy=@AddedBy,
				strIsPublic=@strIsPublic,
                strFolderDescription=@strFolderDescription
				where intFolderId=@intFolderId
				
				END

			ELSE IF @FlagNo=18
				BEGIN
					DELETE FROM Scrl_GroupFolderDetails WHERE intFolderId=@intFolderId or intParentId=@intFolderId
				END
			ELSE IF @FlagNo=20
				BEGIN
				SELECT intUploadDocId,intRegistrationId,intGroupId,strDocTitle,strAuthors,intDocumentTypeID,strFilePath,StrDocsDetails,isDocsSale,Price,isDownloadable,intDocsSee,strDocName FROM Scrl_GroupUploadDouments 
                   WHERE intUploadDocId=@intUploadDocId
				END
			ELSE IF  @FlagNo=21
				BEGIN
					SELECT intCategoryId,strCategoryName,dtAddedOn,intAddedBy,dtModifiedOn,intModifiedBy,strIpAddress,
					(SELECT Count(1) FROM  Scrl_GroupCotextDocument 			     
					WHERE Scrl_GroupCotextDocument.intSubjectCategoryId=intCategoryId and Scrl_GroupCotextDocument.intGroupDocId=@intUploadDocId) CountSub
					FROM  Scrl_Category_SubjectTbl 
					--SELECT intCategoryId,strCategoryName,dtAddedOn,intAddedBy,dtModifiedOn,intModifiedBy,strIpAddress,
					--(SELECT Count(1) FROM  Scrl_CotextDocument 			     
					--WHERE Scrl_CotextDocument.intSubjectCategoryId=intCategoryId and Scrl_CotextDocument.intDocId=@intUploadDocId) CountSub
					--FROM  Scrl_Category_SubjectTbl 
				END
			ELSE IF @FlagNo=22
				BEGIN  
					SELECT Scrl_Category_SubjectTbl.intCategoryId,strCategoryName,0 CountSub 
			       FROM  Scrl_GroupCotextDocument 
			       LEFT JOIN Scrl_Category_SubjectTbl ON Scrl_Category_SubjectTbl.intCategoryId=Scrl_GroupCotextDocument.intSubjectCategoryId
			       WHERE intGroupDocId=@intUploadDocId
				END
			ELSE IF @FlagNo=23
				BEGIN
					UPDATE Scrl_GroupUploadDouments SET intRegistrationId=@intRegistrationId,strDocTitle=@strDocTitle,strFilePath=@FilePath,intDocumentTypeID=@intDocumentTypeID,
					strAuthors=@strAuthors,IsDocsSale=@IsDocsSale,isDownloadable=@IsDocsDownload,intDocsSee=@intDocsSee,Price=@Price,dtModifiedOn=GETDATE(),intModifiedBy=@AddedBy,strDocName=@strDocName
					WHERE intUploadDocId=@intUploadDocId
				END
			ELSE IF @FlagNo=24
				BEGIN
					DELETE FROM Scrl_GroupCotextDocument WHERE intGroupDocId=@intUploadDocId
					DELETE FROM Scrl_GroupUploadDouments WHERE intUploadDocId=@intUploadDocId
				END


END
 
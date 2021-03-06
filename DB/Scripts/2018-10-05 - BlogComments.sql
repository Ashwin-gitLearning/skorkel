USE [Skorkel_Old]
GO
/****** Object:  StoredProcedure [dbo].[Scrl_AddEditDelNewBlogComments]    Script Date: 05-10-2018 12:55:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [dbo].[Scrl_AddEditDelNewBlogComments]
@FlagNo as int ,
@intCommentId as int= NULL,
@intBlogId as int= NULL,
@strComment as varchar(1000) =NULL,
@intAddedBy as INT =NULL,
@intModifiedBy AS INT =NULL,
@strIPAddress AS  varchar(20) =NULL
AS
	IF @FlagNo= 1 
		BEGIN
			INSERT INTO Scrl_NewBlogsCommentsTbl( intBlogId ,strComment, intAddedBy, dtAddedOn ,strIPAddress)
			VALUES(@intBlogId, @strComment,@intAddedBy,GETDATE() ,@strIPAddress)
        END
    ELSE IF @FlagNo=2
        BEGIN				 
			 SELECT intBlogId ,strComment, intAddedBy, dtAddedOn  FROM Scrl_NewBlogsCommentsTbl WHERE intAddedBy=@intAddedBy
	    END
	ELSE IF @FlagNo=3
		 BEGIN				 
			SELECT intCommentId,intBlogId ,(SUBSTRING ( strComment ,0 , 100 )+'')strComment,strComment AS strCommentAll,
			(SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName) 
			FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_NewBlogsCommentsTbl.intAddedBy)strAddedBy, 
			(SELECT ISNULL('CroppedPhoto/'+vchrPhotoPath,'images/profile-photo.png')vchrPhotoPath 
			FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_NewBlogsCommentsTbl.intAddedBy)vchrPhotoPath,
			intAddedBy, CONVERT(VARCHAR, dtAddedOn, 106) dtAddedOn,
			ISNULL((SELECT ISNULL(COUNT(intBlogLikeShareId),0)TotalLike from Scrl_BlogLikeShareTbl WHERE intCommentID=Scrl_NewBlogsCommentsTbl.intCommentID
			AND strRepLiShStatus='LI'),0)TotalLike,
			ISNULL((SELECT intAddedBy from Scrl_BlogLikeShareTbl WHERE intCommentID=Scrl_NewBlogsCommentsTbl.intCommentID
			AND strRepLiShStatus='LI' and intAddedBy=@intAddedBy),'')BlogCommentUser,
			ISNULL((SELECT ISNULL(COUNT(intBlogLikeShareId),0)TotalShare from Scrl_BlogLikeShareTbl WHERE intCommentID=Scrl_NewBlogsCommentsTbl.intCommentID
			AND strRepLiShStatus='SH'),0)TotalShare
			FROM Scrl_NewBlogsCommentsTbl WHERE intBlogId=@intBlogId order by intCommentId Desc
		 END
	ELSE IF @FlagNo=4
		 BEGIN				 
			 SELECT TOP (4) intCommentId,intBlogId ,(SUBSTRING ( strComment ,0 , 100 )+'')strComment
			 FROM Scrl_NewBlogsCommentsTbl order by intCommentId Desc
		 END
		ELSE IF @FlagNo=5
		 BEGIN				 
			SELECT intCommentId,
			(SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName) 
			FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_BlogLikeShareTbl.intAddedBy)UserName, 
			ISNULL((SELECT ISNULL(COUNT(intBlogLikeShareId),0)TotalLike from Scrl_BlogLikeShareTbl WHERE intCommentID=@intCommentId
			AND strRepLiShStatus='LI'),0)TotalLike,
			ISNULL((SELECT intAddedBy from Scrl_BlogLikeShareTbl WHERE intCommentID=@intCommentId
			AND strRepLiShStatus='LI' and intAddedBy=@intAddedBy),'')BlogCommentUser
			FROM Scrl_BlogLikeShareTbl WHERE intCommentID=@intCommentId and strRepLiShStatus='LI' order by intCommentId Desc
		 END
		 ELSE IF @FlagNo=6	  
	    BEGIN				 
			 SELECT intCommentId,intBlogId ,strComment  FROM Scrl_NewBlogsCommentsTbl WHERE intCommentId=@intCommentId
	    END
		ELSE IF @FlagNo=7	  
	    BEGIN				 
			 UPDATE Scrl_NewBlogsCommentsTbl SET strComment=@strComment,strIPAddress=@strIPAddress,dtModifiedOn=GETDATE(),intModifiedBy=@intAddedBy
			 WHERE intCommentId=@intCommentId
	    END
		ELSE IF @FlagNo=8	  
	    BEGIN				 
			 DELETE Scrl_NewBlogsCommentsTbl 
			 WHERE intCommentId=@intCommentId
	    END





/****** Object:  StoredProcedure [dbo].[Scrl_AddEditDelNewBlog]    Script Date: 31-07-2018 13:11:00 Author: Anurag Sharma  Comment: Modify to allow for blank text search******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER  PROCEDURE [dbo].[Scrl_AddEditDelNewBlog]
@FlagNo as int ,
@intBlogId as int= NULL,
@strBlogHeading as varchar(max)=Null,
@strBlogTitle as varchar(max) =NULL,
@strBlogPicture AS  varchar(100) =NULL,
@intAddedBy as INT =NULL,
@intModifiedBy AS INT =NULL,
@strIPAddress AS  varchar(20) =NULL,
@intSubjectCategoryId INT=NULL,
@txtSearch VARCHAR(500)=NULL,
@Ids VARCHAR(50)=NULL,
@PageSize INT = NULL,
@Currentpage INT = NULL

as
Begin
DECLARE @UpperBand INT, @LowerBand INT

SET @LowerBand  = (@CurrentPage - 1) * @PageSize
SET @UpperBand  = (@CurrentPage * @PageSize) + 1

	IF @FlagNo= 1 
  		
		BEGIN
			INSERT INTO Scrl_NewBlogsTbl(strBlogHeading, strBlogTitle ,strBlogPicture, intAddedBy, dtAddedOn ,strIPAddress)
			VALUES(@strBlogHeading,@strBlogTitle, @strBlogPicture,@intAddedBy,GETDATE() ,@strIPAddress)
			SELECT @@Identity as intBid							
			TRUNCATE TABLE Scrl_TempBlogCotextTbl
        END
			  
	ELSE IF @FlagNo=2
			IF NOT EXISTS (SELECT 1 FROM Scrl_TempBlogCotextTbl WHERE intSubjectCategoryId=@intSubjectCategoryId AND intAddedBy=@intAddedBy)
				BEGIN
					INSERT INTO Scrl_TempBlogCotextTbl(intSubjectCategoryId,intAddedBy,intModifiedBy,dtAddedOn)
					VALUES(@intSubjectCategoryId,@intAddedBy,@intModifiedBy,GETDATE())									
				END

			ELSE
				BEGIN
					DELETE FROM  Scrl_TempBlogCotextTbl WHERE intSubjectCategoryId=@intSubjectCategoryId AND intAddedBy=@intAddedBy									
				END		
					
	ELSE IF @FlagNo=3
				BEGIN
				  SELECT Scrl_Category_SubjectTbl.intCategoryId,strCategoryName
					FROM  Scrl_TempBlogCotextTbl 
					LEFT JOIN Scrl_Category_SubjectTbl ON Scrl_Category_SubjectTbl.intCategoryId=Scrl_TempBlogCotextTbl.intSubjectCategoryId
					WHERE Scrl_TempBlogCotextTbl.intSubjectCategoryId=@intSubjectCategoryId AND Scrl_TempBlogCotextTbl.intAddedBy=@intAddedBy						  
				END

	ELSE IF @FlagNo=4
			    BEGIN				 
				 SELECT strCategoryName FROM Scrl_Category_SubjectTbl WHERE intCategoryId=@intSubjectCategoryId
				END

	ELSE IF @FlagNo=5
			    BEGIN
				  	IF NOT EXISTS (SELECT 1 FROM Scrl_ChildNewBlogsTbl WHERE intSubjectCategoryId=@intSubjectCategoryId AND intAddedBy=@intAddedBy AND intBlogId=@intBlogId)
					 BEGIN
					 INSERT INTO Scrl_ChildNewBlogsTbl(intBlogId,intSubjectCategoryId,dtAddedOn,intAddedBy,strIPAddress)
					 VALUES(@intBlogId,@intSubjectCategoryId,GETDATE(),@intAddedBy,@strIPAddress)
					END
				END
				
	ELSE IF @FlagNo=6
			    BEGIN				 
				 SELECT intBlogId,strBlogHeading,(SUBSTRING ( strBlogTitle ,0 , 300 )+CASE WHEN LEN(strBlogTitle)>300 THEN '...' ELSE '' END)strBlogTitle,
					 convert(varchar, dtAddedOn, 106)dtAddedOn,intAddedBy,ISNULL('CroppedPhoto/'+strBlogPicture,'')strBlogPicture,
					 strBlogTitle AS strBlogTitleAll ,
					 CONVERT(varchar,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intCommentId),50) FROM Scrl_NewBlogsCommentsTbl where intBlogId=Scrl_NewBlogsTbl.intBlogId),0),50)CommentCount
					,(SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName) FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_NewBlogsTbl.intAddedBy)strAddedBy 
				 FROM Scrl_NewBlogsTbl  order by intBlogId desc --WHERE intAddedBy=@intAddedBy
				END
				
	ELSE IF @FlagNo=7
				BEGIN
					DELETE FROM  Scrl_TempBlogCotextTbl WHERE intAddedBy=@intAddedBy									
				END		
	ELSE IF @FlagNo=8
			    BEGIN			
					SELECT CONVERT(VARCHAR,NameMonth,50)+' ('+CONVERT(VARCHAR,count(NameMonth),50)+') 'NameMonth,NameYear
						FROM(
					SELECT intBlogId,datename(month,dtAddedOn)NameMonth,year(dtAddedOn)NameYear
						FROM Scrl_NewBlogsTbl 
						WHERE month(dtAddedOn)=month(dtAddedOn) and year(dtAddedOn)=year(dtAddedOn)
					GROUP BY dtAddedOn,intBlogId
					)AS t1 GROUP BY NameMonth,NameYear order by NameYear DESC	
				END
	ELSE IF @FlagNo=9
			    BEGIN				 
				 SELECT t.* from (
				 SELECT TOP (5) intBlogId,(SUBSTRING ( strBlogHeading ,0 , 40 )+CASE WHEN LEN(strBlogHeading)>40 THEN '...' ELSE '' END)strBlogHeading,(SUBSTRING ( strBlogTitle ,0 , 300 )+CASE WHEN LEN(strBlogTitle)>300 THEN '...' ELSE '' END)strBlogTitle,convert(varchar, dtAddedOn, 106)dtAddedOn,intAddedBy,ISNULL('CroppedPhoto/'+strBlogPicture,'images/groupPhoto.jpg')strBlogPicture,
				 (SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName)FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_NewBlogsTbl.intAddedBy)strAddedBy ,
				 CONVERT(INT,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intBlogId),50) FROM Scrl_BlogConsumeTbl where intBlogId=Scrl_NewBlogsTbl.intBlogId),0),50)ViewCount
				  FROM Scrl_NewBlogsTbl  order by ViewCount desc)t ORDER by  t.viewcount desc, t.dtAddedOn ASC--WHERE intAddedBy=@intAddedBy
				END	
				
	ELSE IF @FlagNo=10
			    BEGIN				 
					SELECT intBlogId,strBlogHeading,convert(varchar, dtAddedOn, 106)dtAddedOn,intAddedBy,ISNULL('CroppedPhoto/'+strBlogPicture,'')strBlogPicture,
					strBlogTitle,
					CONVERT(varchar,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intCommentId),50) FROM Scrl_NewBlogsCommentsTbl where intBlogId=Scrl_NewBlogsTbl.intBlogId),0),50)CommentCount
					,(SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName)FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_NewBlogsTbl.intAddedBy)strAddedBy ,
					ISNULL((SELECT ISNULL(COUNT(intBlogHeadingLikeId),0)TotalLike from Scrl_BlogHeadingLikeShareTbl WHERE intBlogId=Scrl_NewBlogsTbl.intBlogId
					AND strRepLiShStatus='LI'),0)TotalLike,
					(SELECT intAddedBy  from Scrl_BlogHeadingLikeShareTbl WHERE intBlogId=Scrl_NewBlogsTbl.intBlogId and intAddedBy=@intAddedBy
					AND strRepLiShStatus='LI')BlogLikeUserId,
					ISNULL((SELECT ISNULL(COUNT(intBlogHeadingLikeId),0)TotalShare from Scrl_BlogHeadingLikeShareTbl WHERE intBlogId=Scrl_NewBlogsTbl.intBlogId
					AND strRepLiShStatus='SH'),0)TotalShare
					,CONVERT(varchar,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intBlogId),50) FROM Scrl_BlogConsumeTbl where intBlogId=@intBlogId),0),50)ViewCount
					FROM Scrl_NewBlogsTbl
					WHERE intBlogId=@intBlogId 
					order by intBlogId desc
			    END

		ELSE IF @FlagNo=11
				BEGIN
					TRUNCATE TABLE Scrl_TempBlogCotextTbl
				END  

		ELSE IF @FlagNo=12
				BEGIN
							IF @Ids = ''
							BEGIN
							SELECT   *    FROM(
								SELECT DISTINCT strBlogHeading, COUNT(*) OVER()  AS Maxcount, ROW_NUMBER() OVER ( ORDER BY Scrl_NewBlogsTbl.dtAddedOn DESC) AS RowNumber,
								(SUBSTRING ( strBlogTitle ,0 , 300 )+CASE WHEN LEN(strBlogTitle)>300 THEN '...' ELSE '' END)strBlogTitle,Scrl_NewBlogsTbl.intBlogId,CONVERT(VARCHAR(20), Scrl_NewBlogsTbl.dtAddedOn,106)dtAddedOn,Scrl_NewBlogsTbl.dtAddedOn dtAddedOns,
								CONVERT(varchar,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intCommentId),50) FROM Scrl_NewBlogsCommentsTbl where intBlogId=Scrl_NewBlogsTbl.intBlogId),0),50)CommentCount
								,(SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName) 					
								FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_NewBlogsTbl.intAddedBy)strAddedBy ,Scrl_NewBlogsTbl.intAddedBy
								,CONVERT(varchar,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intBlogId),50) FROM Scrl_BlogConsumeTbl where intBlogId=Scrl_NewBlogsTbl.intBlogId),0),50)ViewCount
								FROM Scrl_NewBlogsTbl 
								WHERE (@txtSearch = '' OR ISNULL(strBlogHeading,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(strBlogHeading,'') ELSE '%' + @txtSearch + '%' END
								OR ISNULL(strBlogTitle,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(strBlogTitle,'') ELSE '%' + @txtSearch + '%' END)
								AND Scrl_NewBlogsTbl.intAddedBy=CASE @intAddedBy WHEN 0 THEN Scrl_NewBlogsTbl.intAddedBy ELSE @intAddedBy END
								--AND Scrl_NewBlogsTbl.intAddedBy= @intAddedBy 
								GROUP BY Scrl_NewBlogsTbl.strBlogHeading ,strBlogTitle,strBlogPicture,Scrl_NewBlogsTbl.intBlogId,Scrl_NewBlogsTbl.dtAddedOn,Scrl_NewBlogsTbl.intAddedBy,Scrl_NewBlogsTbl.dtAddedOn
								)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand 
							END
							ELSE
							BEGIN
							SELECT   *    FROM(
								SELECT DISTINCT strBlogHeading, COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER ( ORDER BY Scrl_NewBlogsTbl.dtAddedOn DESC) AS RowNumber,
								(SUBSTRING ( strBlogTitle ,0 , 300 )+CASE WHEN LEN(strBlogTitle)>300 THEN '...' ELSE '' END)strBlogTitle,Scrl_NewBlogsTbl.intBlogId,CONVERT(VARCHAR(20), Scrl_NewBlogsTbl.dtAddedOn,106)dtAddedOn,Scrl_NewBlogsTbl.dtAddedOn dtAddedOns,
								CONVERT(varchar,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intCommentId),50) FROM Scrl_NewBlogsCommentsTbl where intBlogId=Scrl_NewBlogsTbl.intBlogId),0),50)CommentCount
								,(SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName) 					
								FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_NewBlogsTbl.intAddedBy)strAddedBy ,Scrl_NewBlogsTbl.intAddedBy
								,CONVERT(varchar,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intBlogId),50) FROM Scrl_BlogConsumeTbl where intBlogId=Scrl_NewBlogsTbl.intBlogId),0),50)ViewCount
								FROM Scrl_NewBlogsTbl 
								LEFT OUTER JOIN Scrl_ChildNewBlogsTbl ON Scrl_ChildNewBlogsTbl.intBlogId =Scrl_NewBlogsTbl.intBlogId
								WHERE (@txtSearch = '' OR ISNULL(strBlogHeading,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(strBlogHeading,'') ELSE '%' + @txtSearch + '%' END
								OR ISNULL(strBlogTitle,'') LIKE CASE @txtSearch WHEN '' THEN ISNULL(strBlogTitle,'') ELSE '%' + @txtSearch + '%' END)
								AND  intSubjectCategoryId IN (SELECT item from dbo.split(CASE WHEN LEN(@Ids)>0 THEN @Ids ELSE CONVERT(VARCHAR(25),intSubjectCategoryId) END,','))
								AND Scrl_NewBlogsTbl.intAddedBy=CASE @intAddedBy WHEN 0 THEN Scrl_NewBlogsTbl.intAddedBy ELSE @intAddedBy END
								--AND Scrl_NewBlogsTbl.intAddedBy= @intAddedBy 
								GROUP BY Scrl_NewBlogsTbl.strBlogHeading ,strBlogTitle,strBlogPicture,Scrl_NewBlogsTbl.intBlogId,Scrl_NewBlogsTbl.dtAddedOn,Scrl_NewBlogsTbl.intAddedBy,Scrl_NewBlogsTbl.dtAddedOn
								)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand 
							END
				END

		ELSE IF @FlagNo=13
				BEGIN
					SELECT DISTINCT strBlogHeading,(SUBSTRING ( strBlogTitle ,0 , 300 )+CASE WHEN LEN(strBlogTitle)>300 THEN '...' ELSE '' END)strBlogTitle,ISNULL('CroppedPhoto/'+strBlogPicture,'')strBlogPicture,Scrl_NewBlogsTbl.intBlogId,CONVERT(VARCHAR(20), Scrl_NewBlogsTbl.dtAddedOn,106)dtAddedOn,
					CONVERT(varchar,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intCommentId),50) FROM Scrl_NewBlogsCommentsTbl where intBlogId=Scrl_NewBlogsTbl.intBlogId),0),50)CommentCount
					,(SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName) 
					FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_NewBlogsTbl.intAddedBy)strAddedBy ,Scrl_NewBlogsTbl.intAddedBy
					,CONVERT(varchar,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intBlogId),50) FROM Scrl_BlogConsumeTbl where intBlogId=Scrl_NewBlogsTbl.intBlogId),0),50)ViewCount
					FROM Scrl_NewBlogsTbl 
					LEFT OUTER JOIN Scrl_ChildNewBlogsTbl ON Scrl_ChildNewBlogsTbl.intBlogId =Scrl_NewBlogsTbl.intBlogId
					WHERE Scrl_NewBlogsTbl.intBlogId IN (SELECT item FROm dbo.split(@Ids,','))
				END

		ELSE IF @FlagNo=14
		BEGIN
			SELECT distinct COUNT(*) OVER()  AS Maxcount FROM Scrl_NewBlogsTbl
			WHERE intAddedBy=@intAddedBy
		END

		ELSE IF @FlagNo=15
		BEGIN
			IF (SELECT COUNT(intBlogConsumeId) FROM  Scrl_BlogConsumeTbl WHERE intAddedBy=@intAddedBy AND intBlogId=@intBlogId) = 0 
			BEGIN
				INSERT INTO Scrl_BlogConsumeTbl (intBlogId,intAddedBy,dtAddedOn,strIpAddress)
				VALUES(@intBlogId,@intAddedBy,GETDATE(),@strIpAddress)
		
				SELECT 1 Action;
			END 
		ELSE
			BEGIN 
				SELECT 0 Action;
			END
		END 

		ELSE IF @FlagNo=16
		BEGIN
			SELECT distinct COUNT(*) OVER()  AS Maxcount FROM  Scrl_BlogConsumeTbl WHERE intAddedBy=@intAddedBy
		END
		
		ELSE IF @FlagNo=17
		Begin
		select Count(Scrl_NewBlogsTbl.intBlogId)TotalBlog from Scrl_NewBlogsTbl
		end
ELSE IF @FlagNo=18
					BEGIN
				  SELECT   *    FROM
					(				 
					SELECT COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER ( ORDER BY Scrl_NewBlogsTbl.intBlogId desc) AS RowNumber, intBlogId,strBlogHeading,(SUBSTRING ( strBlogTitle ,0 , 300 )+CASE WHEN LEN(strBlogTitle)>300 THEN '...' ELSE '' END)strBlogTitle,
					convert(varchar, dtAddedOn, 106)dtAddedOn,intAddedBy,ISNULL('CroppedPhoto/'+strBlogPicture,'')strBlogPicture,
					strBlogTitle AS strBlogTitleAll ,
					CONVERT(varchar,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intCommentId),50) FROM Scrl_NewBlogsCommentsTbl where intBlogId=Scrl_NewBlogsTbl.intBlogId),0),50)CommentCount
					,(SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName) FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_NewBlogsTbl.intAddedBy)strAddedBy 
					,CONVERT(varchar,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intBlogId),50) FROM Scrl_BlogConsumeTbl where intBlogId=Scrl_NewBlogsTbl.intBlogId),0),50)ViewCount
					FROM Scrl_NewBlogsTbl 
					)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand 
					END

	ELSE IF @FlagNo=19
				BEGIN
					DELETE FROM  Scrl_NewBlogsTbl WHERE intBlogId=@intBlogId
					DELETE FROM  Scrl_BlogHeadingLikeShareTbl WHERE intBlogId=@intBlogId
					DELETE FROM  Scrl_NewBlogsCommentsTbl WHERE intBlogId=@intBlogId									
				END		
ELSE IF @FlagNo=20
				BEGIN	
				SELECT   *    FROM
				(			 
				SELECT COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER ( ORDER BY Scrl_NewBlogsTbl.intBlogId desc) AS RowNumber, intBlogId,strBlogHeading,(SUBSTRING ( strBlogTitle ,0 , 300 )+CASE WHEN LEN(strBlogTitle)>300 THEN '...' ELSE '' END)strBlogTitle,
				convert(varchar, dtAddedOn, 106)dtAddedOn,intAddedBy,ISNULL('CroppedPhoto/'+strBlogPicture,'')strBlogPicture,
				strBlogTitle AS strBlogTitleAll ,
				CONVERT(varchar,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intCommentId),50) FROM Scrl_NewBlogsCommentsTbl where intBlogId=Scrl_NewBlogsTbl.intBlogId),0),50)CommentCount
				,(SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName) FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_NewBlogsTbl.intAddedBy)strAddedBy 
				,CONVERT(varchar,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intBlogId),50) FROM Scrl_BlogConsumeTbl where intBlogId=Scrl_NewBlogsTbl.intBlogId),0),50)ViewCount
				FROM Scrl_NewBlogsTbl WHERE intAddedBy=@intAddedBy			 
				)	AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand 

				END

	ELSE IF @FlagNo=21
			    BEGIN				 
					SELECT intBlogHeadingLikeId,
					(SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName)FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_BlogHeadingLikeShareTbl.intAddedBy)UserName ,
					ISNULL((SELECT ISNULL(COUNT(intBlogHeadingLikeId),0)TotalLike from Scrl_BlogHeadingLikeShareTbl WHERE intBlogId=@intBlogId
					AND strRepLiShStatus='LI'),0)TotalLike,
					(SELECT intAddedBy  from Scrl_BlogHeadingLikeShareTbl WHERE intBlogId=@intBlogId and intAddedBy=@intAddedBy
					AND strRepLiShStatus='LI')BlogLikeUserId
			
					FROM Scrl_BlogHeadingLikeShareTbl
					WHERE intBlogId=@intBlogId and  strRepLiShStatus='LI'
					order by intBlogHeadingLikeId desc
			    END
					ELSE IF @FlagNo=22
			    BEGIN				 
				 SELECT t.* from (
				 SELECT TOP (15) intBlogId,(SUBSTRING ( strBlogHeading ,0 , 40 )+CASE WHEN LEN(strBlogHeading)>40 THEN '...' ELSE '' END)strBlogHeading,(SUBSTRING ( strBlogTitle ,0 , 300 )+CASE WHEN LEN(strBlogTitle)>300 THEN '...' ELSE '' END)strBlogTitle,convert(varchar, dtAddedOn, 106)dtAddedOn,intAddedBy,ISNULL('CroppedPhoto/'+strBlogPicture,'images/groupPhoto.jpg')strBlogPicture,
				 (SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName)FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_NewBlogsTbl.intAddedBy)strAddedBy ,
				 CONVERT(INT,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intBlogId),50) FROM Scrl_BlogConsumeTbl where intBlogId=Scrl_NewBlogsTbl.intBlogId),0),50)ViewCount
				  FROM Scrl_NewBlogsTbl  order by ViewCount desc)t ORDER by  t.viewcount desc, t.dtAddedOn ASC--WHERE intAddedBy=@intAddedBy
				END	

				ELSE IF @FlagNo=23
			    BEGIN				 
				  SELECT Scrl_Category_SubjectTbl.intCategoryId,strCategoryName
					FROM  Scrl_ChildNewBlogsTbl 
					LEFT JOIN Scrl_Category_SubjectTbl ON Scrl_Category_SubjectTbl.intCategoryId=Scrl_ChildNewBlogsTbl.intSubjectCategoryId
					WHERE Scrl_ChildNewBlogsTbl.intBlogId=@intBlogId						 				  
				END	
				ELSE IF @FlagNo=24
			    BEGIN				 
					SELECT intCategoryId,strCategoryName,Scrl_Category_SubjectTbl.dtAddedOn,Scrl_Category_SubjectTbl.intAddedBy,Scrl_Category_SubjectTbl.dtModifiedOn,Scrl_Category_SubjectTbl.intModifiedBy,Scrl_Category_SubjectTbl.strIpAddress,
					(SELECT Count(1) FROM  Scrl_ChildNewBlogsTbl 			     
					WHERE Scrl_ChildNewBlogsTbl.intSubjectCategoryId=intCategoryId and Scrl_ChildNewBlogsTbl.intBlogId=@intBlogId) CountSub
					FROM  Scrl_Category_SubjectTbl	
					LEFT JOIN Scrl_ChildNewBlogsTbl ON Scrl_Category_SubjectTbl.intCategoryId=Scrl_ChildNewBlogsTbl.intSubjectCategoryId
					WHERE Scrl_ChildNewBlogsTbl.intBlogId=@intBlogId	
							  
				END	

				ELSE IF @FlagNo=25
			    BEGIN				 
					UPDATE Scrl_NewBlogsTbl SET strBlogHeading=@strBlogHeading, strBlogTitle=@strBlogTitle ,strBlogPicture=@strBlogPicture,strIPAddress=@strIPAddress,dtModifiedOn=GETDATE(),intModifiedBy=@intAddedBy
					WHERE intBlogId=@intBlogId
									  
				END	
				ELSE IF @FlagNo=26
			    BEGIN				 
					DELETE FROM  Scrl_ChildNewBlogsTbl WHERE intBlogId=@intBlogId	
									  
				END
				ELSE IF @FlagNo=27
			    BEGIN
				SELECT intCategoryId,strCategoryName FROM Scrl_ChildNewBlogsTbl 
				INNER JOIN Scrl_Category_SubjectTbl ON Scrl_ChildNewBlogsTbl.intSubjectCategoryId=Scrl_Category_SubjectTbl.intCategoryId
				WHERE intBlogId=@intBlogId
				END

				ELSE IF @FlagNo=28
				BEGIN
				SELECT intBlogId,strBlogHeading,convert(varchar, dtAddedOn, 106)dtAddedOn,intAddedBy,ISNULL('CroppedPhoto/'+strBlogPicture,'')strBlogPicture,
				strBlogTitle
				,CONVERT(varchar,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intCommentId),50) FROM Scrl_NewBlogsCommentsTbl where intBlogId=Scrl_NewBlogsTbl.intBlogId),0),50)CommentCount
				,(SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName)FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_NewBlogsTbl.intAddedBy)strAddedBy ,
				ISNULL((SELECT ISNULL(COUNT(intBlogHeadingLikeId),0)TotalLike from Scrl_BlogHeadingLikeShareTbl WHERE intBlogId=Scrl_NewBlogsTbl.intBlogId
				AND strRepLiShStatus='LI'),0)TotalLike
				,ISNULL((SELECT ISNULL(COUNT(intBlogHeadingLikeId),0)TotalShare from Scrl_BlogHeadingLikeShareTbl WHERE intBlogId=Scrl_NewBlogsTbl.intBlogId
				AND strRepLiShStatus='SH'),0)TotalShare
				,CONVERT(INT,ISNULL((SELECT CONVERT(VARCHAR,COUNT(intBlogId),50) FROM Scrl_BlogConsumeTbl where intBlogId=intBlogId),0),50)ViewCount

				FROM Scrl_NewBlogsTbl
				WHERE intBlogId=intBlogId 
				order by intBlogId desc
				END
END
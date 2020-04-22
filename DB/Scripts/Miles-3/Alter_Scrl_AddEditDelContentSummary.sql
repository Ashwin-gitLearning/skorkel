Alter PROCEDURE [dbo].[Scrl_AddEditDelContentSummary]  
@FlagNo AS INT ,  
@SummaryId AS NUMERIC(18,0)= NULL,  
@ContentId AS NUMERIC(18,0)= NULL,  
@addedby AS INT =NULL,  
@ContentTypeID AS INT =NULL,  
@SummaryText AS VARCHAR(MAX)=NULL  
  
  
AS  
BEGIN  
  IF @FlagNo= 1   
    BEGIN  
   IF(@SummaryId > 0 )   
    BEGIN  
     INSERT INTO Scrl_SummaryMaterTbl(intContentId,strSummaryText,intContentTypeId,intModifiedBy,dtModifiedDate,bitVersionFlag)  
     VALUES(@ContentId ,@SummaryText,@ContentTypeID,@addedby, GETDATE(),1)  
     SELECT @@Identity as intContentId  
    END  
   ELSE  
    BEGIN  
     INSERT INTO Scrl_SummaryMaterTbl(intContentId,strSummaryText,intContentTypeId,intAddedby,dtaddedDate,bitVersionFlag)  
     VALUES(@ContentId ,@SummaryText,@ContentTypeID,@addedby, GETDATE(),0)  
     SELECT @@Identity as intContentId   
    END  
    END  
 ELSE IF @FlagNo= 2   
    BEGIN  
   SELECT intSummaryid,intContentId,strSummaryText,intContentTypeId,  
   (SELECT vchrFirstName+' '+vchrLastName FROM scrl_RegistrationTbl WHERE intRegistrationId= Summary.intAddedby)UserName,  
   intAddedby,CONVERT(VARCHAR(11), dtaddedDate ,106)dtaddedDate,  
   (SELECT COUNT(intLikeDisLike) FROM  Scrl_LikeDisLikeRatingTbl WHERE intTagId=intSummaryid AND  vhrTagType='S' AND intLikeDisLike=1)Likes,  
   (SELECT COUNT(intLikeDisLike)FROM  Scrl_LikeDisLikeRatingTbl WHERE intTagId=intSummaryid AND vhrTagType='S' AND intLikeDisLike=2)DisLikes  
   FROM Scrl_SummaryMaterTbl  Summary  
   WHERE intContentId = @ContentId AND intContentTypeId=@ContentTypeID   AND bitVersionFlag =0 AND LEN(CONVERT(VARCHAR(MAX),strSummaryText)) > 0   
   ORDER BY 1 desc  
    END  
  
 ELSE IF @FlagNo= 3   
    BEGIN  
   UPDATE Scrl_SummaryMaterTbl  
   SET intContentId=@ContentId ,  
   strSummaryText=@SummaryText,  
   intContentTypeId=@ContentTypeID,  
   intModifiedBy=@addedby,   
   dtModifiedDate=GETDATE()  
   WHERE intSummaryid = @SummaryId  AND intContentId=@ContentId AND intContentTypeId=@ContentTypeID AND intAddedby=@addedby  
   AND bitVersionFlag =0  
  END  
  
  ELSE IF @FlagNo= 4   
    BEGIN  
   DELETE FROM Scrl_SummaryMaterTbl   
   WHERE intSummaryid = @SummaryId AND intContentId=@ContentId AND intContentTypeId=@ContentTypeID AND intAddedby=@addedby  
  END  
 ELSE IF @FlagNo= 5  
    BEGIN  
   SELECT intSummaryid,intContentId,strSummaryText,intContentTypeId,intAddedby,
    (select count(*) from Scrl_CaseSummaryLikes where summary_id= Scrl_SummaryMaterTbl.intSummaryid)TotalLikes,
   (select count(*) from Scrl_CaseSummaryLikes where summary_id= Scrl_SummaryMaterTbl.intSummaryid and user_id=@addedby)UserLikes 
    FROM Scrl_SummaryMaterTbl   
   WHERE intContentId=@ContentId  AND intAddedby=@addedby  
  END  
    ELSE IF @FlagNo= 6  
  BEGIN  
    IF NOT EXISTS (SELECT 1 FROM Scrl_SummaryMaterTbl WHERE intContentId=@ContentId AND intAddedby=@addedby)  
   BEGIN  
    INSERT INTO Scrl_SummaryMaterTbl(intContentId,strSummaryText,intContentTypeId,intAddedby,dtaddedDate,bitVersionFlag)  
    VALUES(@ContentId ,@SummaryText,@ContentTypeID,@addedby, GETDATE(),0)  
    SELECT @@Identity as intContentId   
  END   
  ELSE  
   BEGIN  
    UPDATE Scrl_SummaryMaterTbl  
    SET intContentId=@ContentId ,  
    strSummaryText=@SummaryText,  
    intContentTypeId=@ContentTypeID,  
    intModifiedBy=@addedby,   
    dtModifiedDate=GETDATE()  
    WHERE  intContentId=@ContentId AND intAddedby=@addedby  
    Select intSummaryid As intContentId FROM Scrl_SummaryMaterTbl WHERE  intContentId=@ContentId AND intAddedby=@addedby  
   END  
  END  
  ELSE IF @FlagNo= 7  
    BEGIN  
   SELECT intSummaryid,intContentId,strSummaryText,intContentTypeId,intAddedby,CONVERT(VARCHAR(11), dtaddedDate ,106)dtAddedDate,(SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName)   
   FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_SummaryMaterTbl.intAddedBy)strAddedBy,
    (SELECT ISNULL('CroppedPhoto/'+vchrPhotoPath,'images/profile-photo.png')vchrPhotoPath   
   FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_SummaryMaterTbl.intAddedBy)vchrPhotoPath,
   (select count(*) from Scrl_CaseSummaryLikes where summary_id= Scrl_SummaryMaterTbl.intSummaryid)TotalLikes,
   (select count(*) from Scrl_CaseSummaryLikes where summary_id= Scrl_SummaryMaterTbl.intSummaryid and user_id=@addedby)UserLikes 
    FROM Scrl_SummaryMaterTbl   
   WHERE intContentId=@ContentId  AND intAddedby<>@addedby order by TotalLikes desc
  END  
  ELSE IF @FlagNo= 8  
    BEGIN  
	IF EXISTS (SELECT summary_id FROM Scrl_CaseSummaryLikes WHERE summary_id=@SummaryId AND user_id = @addedby) 
  BEGIN  
	delete from Scrl_CaseSummaryLikes WHERE summary_id=@SummaryId AND user_id = @addedby
  END
  ELSE
  BEGIN
  insert into Scrl_CaseSummaryLikes(summary_id, user_id, timestamp) values(@SummaryId, @addedby, GETDATE())
   END
	END
END  
   
  
   
  
  
  
  
  
  
  
  
  
  
  
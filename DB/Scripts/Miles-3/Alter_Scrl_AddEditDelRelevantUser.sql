Alter PROCEDURE [dbo].[Scrl_AddEditDelRelevantUser]   
 @FlagNo AS INT ,  
@CaseId AS NUMERIC(18,0)= NULL,  
@ContentTypeId INT=NULL,  
@intAddedBy as INT=NULL,  
@intDocAddedBy AS INT=NULL,  
@intViewId AS INT=NULL  
AS  
BEGIN  
 IF @FlagNo=1  
  BEGIN  
  SELECT DISTINCT * FROM (  
 (SELECT DISTINCT intContentId,intAddedby,  
              (SELECT COUNT(*)TotalLikes FROM scrl_CaseMicroTagLikes WHERE Id=@CaseId AND destination_user_id=Scrl_ContentLinkCaseMasterTbl.intAddedBy)TotalLikes,  
              (SELECT ISNULL(vchrPhotoPath,'img3.jpg')vchrPhotoPath FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_ContentLinkCaseMasterTbl.intAddedBy)vchrPhotoPath,  
    (SELECT (vchrFirstName + ' '+vchrLastName) Addedby FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_ContentLinkCaseMasterTbl.intAddedby) Name,  
    (SELECT strCaseTitle FROM  Scrl_CaseTbl where intCaseId=@CaseId) Title,'Desc..' Descriptions  
    FROM Scrl_ContentLinkCaseMasterTbl WHERE intContentId = @CaseId )  
    UNION ALL  
     (SELECT DISTINCT intContentId,intAddedby,  
    ( SELECT COUNT(*)TotalLikes FROM scrl_CaseMicroTagLikes WHERE ID=@CaseId AND destination_user_id=Scrl_ContentTagDefMasterTbl.intAddedby)TotalLikes,  
     (SELECT ISNULL(vchrPhotoPath,'img3.jpg')vchrPhotoPath FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_ContentTagDefMasterTbl.intAddedBy)vchrPhotoPath,  
    (SELECT (vchrFirstName + ' '+vchrLastName) Addedby FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_ContentTagDefMasterTbl.intAddedby) Name,  
    (SELECT strCaseTitle FROM  Scrl_CaseTbl where intCaseId=@CaseId) Title,'Desc..' Descriptions  
    FROM Scrl_ContentTagDefMasterTbl WHERE intContentId = @CaseId )  
    UNION ALL  
    (SELECT DISTINCT intCaseId intContentId,intAddedby,  
     (SELECT COUNT(*)TotalLikes FROM scrl_CaseMicroTagLikes WHERE ID=@CaseId AND destination_user_id=Scrl_MarkMasterTbl.intAddedby)TotalLikes,  
         (SELECT ISNULL(vchrPhotoPath,'img3.jpg')vchrPhotoPath FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_MarkMasterTbl.intAddedBy)vchrPhotoPath,  
    (SELECT (vchrFirstName + ' '+vchrLastName) Addedby FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_MarkMasterTbl.intAddedby) Name,  
    (SELECT strCaseTitle FROM  Scrl_CaseTbl where intCaseId=@CaseId) Title,'Desc..' Descriptions  
    FROM Scrl_MarkMasterTbl WHERE intCaseId = @CaseId )  
    UNION ALL  
    (SELECT DISTINCT intDocId intContentId,intAddedby,  
     (SELECT COUNT(*)TotalLikes FROM scrl_CaseMicroTagLikes WHERE ID=@CaseId AND destination_user_id=Scrl_RatioTbl.intAddedBy)TotalLikes,  
    (SELECT ISNULL(vchrPhotoPath,'img3.jpg')vchrPhotoPath FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_RatioTbl.intAddedBy)vchrPhotoPath,  
    (SELECT (vchrFirstName + ' '+vchrLastName) Addedby FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_RatioTbl.intAddedby) Name,  
    (SELECT strCaseTitle FROM  Scrl_CaseTbl where intCaseId=@CaseId) Title,'Desc..' Descriptions  
    FROM Scrl_RatioTbl WHERE intDocId = @CaseId )  
    ) as T  
  
  END  
  IF @FlagNo=2  
  BEGIN  
  --SELECT DISTINCT intCaseId,intAddedby,  
  --            (SELECT COUNT(*)TotalComment FROM Scrl_CommentMasterTbl WHERE intCaseId=@CaseId AND intCommentAddedFor=Scrl_ResearchDoc_MarkAsTbl.intAddedBy)TotalComment,  
  --            (SELECT ISNULL(vchrPhotoPath,'img3.jpg')vchrPhotoPath FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_ResearchDoc_MarkAsTbl.intAddedBy)vchrPhotoPath,  
  --  (SELECT (vchrFirstName + ' '+vchrLastName) Addedby FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_ResearchDoc_MarkAsTbl.intAddedby) Name,  
  --  (SELECT strCaseTitle FROM  Scrl_CaseTbl where intCaseId=@CaseId) Title,' ' Descriptions  
  --  FROM Scrl_ResearchDoc_MarkAsTbl WHERE intCaseId=@CaseId  
  
  SELECT DISTINCT * FROM (  
   SELECT DISTINCT CASE WHEN intAddedBy=@intAddedBy THEN -1 ELSE ROW_NUMBER() OVER (PARTITION BY intAddedBy ORDER BY intAddedBy) END as rn,intCaseId,intAddedby,
   (SELECT COUNT(*)LikedByUser FROM scrl_CaseMicroTagLikes WHERE ID=@CaseId AND source_user_id=@intAddedBy AND destination_user_id=Scrl_ResearchDoc_MarkAsTbl.intAddedBy)LikedByUser,    
            (SELECT COUNT(*)TotalLikes FROM scrl_CaseMicroTagLikes WHERE ID=@CaseId AND destination_user_id=Scrl_ResearchDoc_MarkAsTbl.intAddedBy)TotalLikes,  
             (SELECT ISNULL(vchrPhotoPath,'img3.jpg')vchrPhotoPath FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_ResearchDoc_MarkAsTbl.intAddedBy)vchrPhotoPath,  
    (SELECT (vchrFirstName + ' '+vchrLastName) Addedby FROM scrl_RegistrationTbl WHERE intRegistrationId=Scrl_ResearchDoc_MarkAsTbl.intAddedby) Name,  
    (SELECT strCaseTitle FROM  Scrl_CaseTbl where intCaseId=@CaseId) Title,' ' Descriptions,  
    (SELECT COUNT(*)TotalViewCount FROM Scrl_ResearchDoc_View WHERE intCaseId=@CaseId AND intDocAddedBy=Scrl_ResearchDoc_MarkAsTbl.intAddedBy)TotalView  
    FROM Scrl_ResearchDoc_MarkAsTbl WHERE intCaseId=@CaseId  
    ) AS TA WHERE rn<2 Order By rn  
  
  END  
  
  IF @FlagNo=3  
  BEGIN  
    
  IF EXISTS (SELECT intDoc_ViewId FROM Scrl_ResearchDoc_View WHERE intCaseId=@CaseId AND intViewId = @intViewId AND intDocAddedBy=@intDocAddedBy)  
  BEGIN  
  SELECT 1;  
  END  
  ELSE  
  BEGIN  
  INSERT INTO Scrl_ResearchDoc_View (intCaseId,intViewId,intDocAddedBy,intViewCount,dtAddedBy)  
  VALUES (@CaseId,@intViewId,@intDocAddedBy,1,GETDATE())  
  SELECT @@identity intDoc_ViewId  
  END  
  END  
  
  IF @FlagNo=4  
  BEGIN  
  SELECT (ISNULL(vchrFirstName,'')+' '+vchrLastName) AS NAME FROM scrl_RegistrationTbl WHERE intRegistrationId=@intAddedBy  
  END  
END  
  
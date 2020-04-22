--  Scrl_AddEditDelMyNetworks 500,1,9,116,'25 Dec 2013'                                        
Alter  PROCEDURE [dbo].[Scrl_AddEditDelMyNetworks]                                          
                                        
@PageSize INT = NULL,                                        
@Currentpage INT = NULL,                                        
@FlagNo INT,                                         
@intRegistrationId INT = NULL,                                        
@NotificationDate VARCHAR(20)=NULL                                        
                                        
AS                                        
BEGIN                                         
SET NOCOUNT ON                                        
DECLARE @UpperBand INT, @LowerBand INT                                        
                                          
SET @LowerBand  = (@CurrentPage - 1) * @PageSize                                        
SET @UpperBand  = (@CurrentPage * @PageSize) + 1                                        
                                         
IF @FlagNo=4                                        
BEGIN                                          
 SELECT   *    FROM                                        
 (                                             
  SELECT COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY Scrl_UserRequestInvitationTbl.dtAddedOn desc ) AS RowNumber,                                        
                                             
    Scrl_UserRequestInvitationTbl.intRegistrationId,intInvitedUserId,intRequestInvitaionId Id,IsAccepted Accepted,intUserTypeId,                                         
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=intInvitedUserId) Name,                                      
 '' as notification_detail,                                        
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=intInvitedUserId) vchrPhotoPath,                                               
    CONVERT(VARCHAR(12),dtReqAcceptedDate,109) dtReqAcceptedDate,                                             
    CONVERT(VARCHAR(12),Scrl_UserRequestInvitationTbl.dtAddedOn,109) dtAddedOn,                                         
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                                         
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                           
     ELSE '' END                                         
    IsAccepted                                             
  FROM Scrl_UserRequestInvitationTbl                                         
  INNER JOIN scrl_RegistrationTbl ON Scrl_UserRequestInvitationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                                        
  WHERE Scrl_UserRequestInvitationTbl.intRegistrationId=@intRegistrationId                                         
                                            
 ) AS T  WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand                                            
END                                        
                                         
ELSE IF @FlagNo=5                                        
BEGIN                                        
 SELECT   *    FROM                                        
 (                                        
 SELECT *, COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY dtAddedOn desc ) AS RowNumber  FROM                                        
 (                                         
   SELECT                                        
   intRequestInvitaionId Id,Scrl_UserRequestInvitationTbl.intRegistrationId,intInvitedUserId,'' strInvitee ,'' strGroupName,'' StrRecommendation,                                        
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) Name,                                        
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) vchrPhotoPath,                                                    
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) vchrUserName,                               
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserRequestInvitationTbl.intRegistrationId) intUserTypeID,                                        
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId and                                         
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                                        
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                                        
   CONVERT(VARCHAR(12),dtReqAcceptedDate,109) dtReqAcceptedDate,                                             
   CONVERT(VARCHAR(12),Scrl_UserRequestInvitationTbl.dtAddedOn,109) dtAddedOn,strTableName,                                        
                                                
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                               
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                           
    ELSE '' END                                         
    IsAccepted,IsAccepted AS IsAccept,'' AS intIsAccept                                             
  FROM Scrl_UserRequestInvitationTbl                                         
  LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserRequestInvitationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                                        
  WHERE Scrl_UserRequestInvitationTbl.intInvitedUserId=@intRegistrationId AND (IsAccepted='0' OR IsAccepted='1')                                         
                                        
  UNION ALL                                        
                                           
  SELECT                                         
   intRecommendationId Id,Scrl_UserRecommendationTbl.intRegistrationId,intInvitedUserId,'' strInvitee,'' strGroupName,StrRecommendation,                                         
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId) Name,                                        
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId) vchrPhotoPath,                                                    
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId) vchrUserName,                                        
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserRecommendationTbl.intRegistrationId) intUserTypeID,                                        
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId and                                         
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                                        
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                                        
    '' dtReqAcceptedDate,                                             
   CONVERT(VARCHAR(12),Scrl_UserRecommendationTbl.dtAddedOn,109) dtAddedOn,strTableName,                                         
                                                
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                              
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                           
    ELSE '' END                                         
    IsAccepted,IsAccepted AS IsAccept,'' AS intIsAccept                                             
  FROM Scrl_UserRecommendationTbl                          
  LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserRecommendationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                                        
 WHERE Scrl_UserRecommendationTbl.intInvitedUserId=@intRegistrationId AND (IsAccepted='0' OR IsAccepted='2')                                         
  UNION ALL                                        
   SELECT                                           
    intRequestJoinId Id,Scrl_UserGroupJoiningTbl.intRegistrationId,intInvitedUserId,'' strInvitee ,strGroupName, '' StrRecommendation,                      
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) Name,                       
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrPhotoPath,                                                    
    (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrUserName,                                        
    (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) intUserTypeID,                                        
    (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_UserGroupJoiningTbl.intRegistrationId and                                         
    (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                                        
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                                        
    CONVERT(VARCHAR(12),dtReqAcceptedDate,109) dtReqAcceptedDate,                                             
    CONVERT(VARCHAR(12),Scrl_UserGroupJoiningTbl.dtAddedOn,109) dtAddedOn,strTableName,                                        
                                                 
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                                         
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                           
    ELSE '' END                                         
    IsAccepted,IsAccepted AS IsAccept,'' AS intIsAccept                                             
  FROM Scrl_UserGroupJoiningTbl                                         
    LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId                                        
    LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId                                        
  WHERE Scrl_UserGroupJoiningTbl.intInvitedUserId=@intRegistrationId AND (IsAccepted='0' )--OR IsAccepted='2')                                        
                                           
  UNION ALL                                         
     SELECT                                           
    intGrpInvitationMemberId Id,Scrl_GroupMemberDetailsTbl.intAddedBy,strMemberName,CONVERT(VARCHAR(500),Scrl_UserGroupDetailTbl.inGroupId) strInvitee ,strGroupName, '' StrRecommendation,                                       
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) Name,                                        
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) vchrPhotoPath,                                               
    (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) vchrUserName,                                        
    (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) intUserTypeID,                                        
    (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_GroupMemberDetailsTbl.intAddedBy and                                         
    (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)               
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                                        
    '' dtReqAcceptedDate,                                             
    CONVERT(VARCHAR(12),Scrl_GroupMemberDetailsTbl.dtAddedOn,109) dtAddedOn,'Scrl_RequestGroupJoin' strTableName,                                        
    '' intIsAccept,'' AS IsAccept,intIsAccept                                             
  FROM Scrl_GroupMemberDetailsTbl                                         
    LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_GroupMemberDetailsTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId                                        
    LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_GroupMemberDetailsTbl.inGroupId=Scrl_UserGroupDetailTbl.inGroupId                                        
  WHERE Scrl_GroupMemberDetailsTbl.strMemberName=@intRegistrationId AND Scrl_GroupMemberDetailsTbl.intIsAccept IS NULL                                        
  AND Scrl_UserGroupDetailTbl.strAccess='R'                      
                  
  --SA Notifications                   
                  
  --UNION ALL                  
                  
  --  SELECT                                           
  --  '0' as Id ,'0' as intAddedBy ,'SA' as strMemberName,'' as strInvitee,Notification_Detail as strGroupName,                  
  --  '' as StrRecommendation, '' as Name,    '' as vchrPhotoPath,                                                    
  --  '' as vchrUserName,                                        
  --  '0' as intUserTypeID,                                        
  --  '' as Designation,                                        
  --  '' as dtReqAcceptedDate,                                             
  --  CONVERT(VARCHAR(12),SCL_SA_Notifications.dateaddedon,109) dtAddedOn,'SCL_SA_Notifications' strTableName,                                        
  --  '0' as intIsAccept,'' AS IsAccept,'0' as intIsAccept                                             
  --FROM SCL_SA_Notifications                                         
                    
                   
  --SA Notifications                   
                  
                    
--    SELECT @intRegistrationId as Id, @intRegistrationId as intRegistrationId, (select intInvitedUserId from Scrl_UserRecommendationTbl where intInvitedUserId=@intRegistrationId) as intInvitedUserId, '' as strInvitee,   notification_detail as strGroupName,'' StrRecommendation,  '' as Name,'' as vchrPhotoPath,'' as vchrUserName,                   
--(SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= @intRegistrationId)  as intUserTypeID ,'' as Designation, '' as dtReqAcceptedDate,dateAddedOn,'SCL_SA_Notifications' as strTableName,'' as IsAccepted, dateAddedOn as dtAdded,0 as IsAccept,0 AS intIsAccept                                      
--  from SCL_SA_Notifications                    
                                   
                  
                                        
                                       
                                       
  UNION ALL                                        
                                        
  SELECT                  
    intSharedId Id,Scrl_GroupShareTbl.intAddedBy intRegistrationId,intGroupId intInvitedUserId,strInvitee,                                        
    Scrl_UserGroupDetailTbl.strGroupName,                                        
   '' StrRecommendation,                                        
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) Name,                                        
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) vchrPhotoPath,                             
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) vchrUserName,                                        
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) intUserTypeID,                                   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_GroupShareTbl.intAddedBy and                                         
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)              
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                                        
   '' dtReqAcceptedDate,                                        
   CONVERT(VARCHAR(12),Scrl_GroupShareTbl.dtAddedOn,109) dtAddedOn,strTableName,                                            
   '' IsAccepted,'' AS IsAccept,'' AS intIsAccept                                                      
                                                  
  FROM Scrl_GroupShareTbl                                              
    LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_GroupShareTbl.intGroupId                                              
  WHERE 1=                                        
     (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInvitee,',') AS a                                        
     INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                                        
     THEN 1 ELSE  0 END matchId)                                        
UNION ALL                                        
     SELECT                                        
     intQAReplyLikeShareId Id,Scrl_UserPostQAReplyTbl.intAddedBy intRegistrationId,Scrl_UserPostQAReplyTbl.intPostQuestionId strInvitee,strInvitee,                                       
     (SUBSTRING ( Scrl_UserPostQAReplyTbl.strFileName ,0 , 50 )+CASE WHEN LEN(Scrl_UserPostQAReplyTbl.strFileName)>50 THEN '...' ELSE '' END)strGroupName,                                        
     strSharelink AS StrRecommendation,                                        
     (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) Name,                                        
     (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) vchrPhotoPath,                                                    
     (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) vchrUserName,                                        
     (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) intUserTypeID,                                        
     '' Designation,                                        
     '' dtReqAcceptedDate,                                             
     CONVERT(VARCHAR(12),Scrl_UserPostQAReplyTbl.dtAddedOn,109) dtAddedOn,strTableName,                                            
     '' IsAccepted,                                        
     '' AS IsAccept,'' AS intIsAccept                                                      
                                                      
     FROM Scrl_UserPostQAReplyTbl                      
     LEFT OUTER JOIN Scrl_QuestionAnsTbl ON Scrl_QuestionAnsTbl.intPostQuestionId=Scrl_UserPostQAReplyTbl.intPostQuestionId                                                 
     WHERE 1=                                        
     (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInvitee,',') AS a                                        
     INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                                        
     THEN 1 ELSE  0 END matchId) AND strRepLiShStatus='SH'                                        
                                        
     UNION ALL                                        
                                        
     SELECT                                        
     intBlogHeadingLikeId Id,Scrl_BlogHeadingLikeShareTbl.intAddedBy intRegistrationId,Scrl_BlogHeadingLikeShareTbl.intBlogId strInviteeShare,strInviteeShare,                                        
     (SUBSTRING ( Scrl_BlogHeadingLikeShareTbl.strBlogTitle ,0 , 50 )+CASE WHEN LEN(Scrl_BlogHeadingLikeShareTbl.strBlogTitle)>50 THEN '...' ELSE '' END)strGroupName,                                        
     strLink AS StrRecommendation,                 
     (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) Name,                                        
     (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) vchrPhotoPath,                  
     (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) vchrUserName,                                        
     (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) intUserTypeID,                                        
     '' Designation,                                        
     '' dtReqAcceptedDate,                     
     CONVERT(VARCHAR(12),Scrl_BlogHeadingLikeShareTbl.dtAddedOn,109) dtAddedOn,strTableName,                                            
     '' IsAccepted,                                        
     '' AS IsAccept,'' AS intIsAccept                                                      
                                                      
     FROM Scrl_BlogHeadingLikeShareTbl                                              
     LEFT OUTER JOIN Scrl_NewBlogsTbl ON Scrl_NewBlogsTbl.intBlogId=Scrl_BlogHeadingLikeShareTbl.intBlogId                                             
     WHERE 1=                                        
     (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInviteeShare,',') AS a                                        
     INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                                        
     THEN 1 ELSE  0 END matchId) AND strRepLiShStatus='SH'                                        
                       
     UNION ALL                                        
                                        
     SELECT                                        
     intMicroLikeShareId Id,Scrl_MicrolTagLikeShareTbl.intAddedBy intRegistrationId,Scrl_MicrolTagLikeShareTbl.intMicroLikeShareId strInviteeShare,strInviteeShare,                                        
     (SUBSTRING ( Scrl_CaseTbl.strCaseTitle ,0 , 50 )+CASE WHEN LEN(Scrl_CaseTbl.strCaseTitle)>50 THEN '...' ELSE '' END)strGroupName,                                        
     strLink AS StrRecommendation,                                        
     (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) Name,                                        
     (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) vchrPhotoPath,                                                    
     (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) vchrUserName,                                        
     (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) intUserTypeID,                                        
     '' Designation,                                        
     '' dtReqAcceptedDate,                                             
     CONVERT(VARCHAR(12),Scrl_MicrolTagLikeShareTbl.dtAddedOn,109) dtAddedOn,'Scrl_MicrolTagLikeShareTbl' strTableName,                                            
     '' IsAccepted,                                        
     '' AS IsAccept,'' AS intIsAccept                                                      
                                                      
     FROM Scrl_MicrolTagLikeShareTbl                                              
     LEFT OUTER JOIN Scrl_CaseTbl ON Scrl_CaseTbl.intCaseId=Scrl_MicrolTagLikeShareTbl.intDocId                        
     WHERE 1=                                        
     (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInviteeShare,',') AS a                                        
     INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                                        
     THEN 1 ELSE  0 END matchId) AND strRepLiShStatus='SH'                                         
                                              
                                              
 ) AS T                           
 ) AS X WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand                                          
END                                         
                                         
--FOR NEW DESIGN                                        
                                         
ELSE IF @FlagNo=6                                        
BEGIN                                          
                                             
IF EXISTS(SELECT 1 FROM Scrl_UserRequestInvitationTbl WHERE (intRegistrationId=@intRegistrationId OR intInvitedUserId=@intRegistrationId) AND IsAccepted=1)                                        
 BEGIN                                        
                                   
  SELECT *, COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY dtAddedOn desc ) AS RowNumber                                        
  FROM(                                        
                                             
    SELECT Scrl_UserRequestInvitationTbl.intRegistrationId,intInvitedUserId,intRequestInvitaionId Id,IsAccepted Accepted,intUserTypeId,'' as notification_detail,                                         
    (SELECT vchrFirstName  From scrl_RegistrationTbl WHERE intRegistrationId=intInvitedUserId) FirstName,                                          
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=intInvitedUserId) Name,                                          
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=intInvitedUserId) vchrPhotoPath,                   
    CONVERT(VARCHAR(12),dtReqAcceptedDate,106) dtReqAcceptedDate,                                             
    CONVERT(VARCHAR(12),Scrl_UserRequestInvitationTbl.dtAddedOn,106) dtAddedOn,                                         
                                                 
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                                         
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                           
     ELSE '' END                                         
    IsAccepted                                             
  FROM Scrl_UserRequestInvitationTbl                                         
  INNER JOIN scrl_RegistrationTbl ON Scrl_UserRequestInvitationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                                        
  WHERE Scrl_UserRequestInvitationTbl.intRegistrationId=@intRegistrationId AND IsAccepted=1                                        
                                        
 UNION ALL                                        
                                          
  SELECT                                        
                                             
    Scrl_UserRequestInvitationTbl.intRegistrationId,Scrl_UserRequestInvitationTbl.intRegistrationId intInvitedUserId,intRequestInvitaionId Id,IsAccepted Accepted,intUserTypeId, '' as notification_detail,                                       
    (SELECT vchrFirstName  From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) FirstName,                                          
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) Name,                                          
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) vchrPhotoPath,                  
    CONVERT(VARCHAR(12),dtReqAcceptedDate,106) dtReqAcceptedDate,                                             
    CONVERT(VARCHAR(12),Scrl_UserRequestInvitationTbl.dtAddedOn,106) dtAddedOn,                                         
                                                 
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                                         
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                           
     ELSE '' END                                         
    IsAccepted                                             
  FROM Scrl_UserRequestInvitationTbl                                         
  INNER JOIN scrl_RegistrationTbl ON Scrl_UserRequestInvitationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                                        
  WHERE Scrl_UserRequestInvitationTbl.intInvitedUserId=@intRegistrationId AND IsAccepted=1                                        
                        
  )A                                        
 END                                        
                                        
ELSE                          
 BEGIN                                        
 SELECT *, COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY dtAddedOn desc ) AS RowNumber                                        
  FROM(                                        
                                             
    SELECT Scrl_UserRequestInvitationTbl.intRegistrationId,intInvitedUserId,intRequestInvitaionId Id,IsAccepted Accepted,intUserTypeId, '' as notification_detail,                                        
    (SELECT vchrFirstName  From scrl_RegistrationTbl WHERE intRegistrationId=intInvitedUserId) FirstName,                                          
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=intInvitedUserId) Name,                                          
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=intInvitedUserId) vchrPhotoPath,                                               
    CONVERT(VARCHAR(12),dtReqAcceptedDate,106) dtReqAcceptedDate,                                       
    CONVERT(VARCHAR(12),Scrl_UserRequestInvitationTbl.dtAddedOn,106) dtAddedOn,                              
                                                 
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                                         
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                           
     ELSE '' END                                         
    IsAccepted                                             
  FROM Scrl_UserRequestInvitationTbl                                         
  INNER JOIN scrl_RegistrationTbl ON Scrl_UserRequestInvitationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                                        
  WHERE Scrl_UserRequestInvitationTbl.intRegistrationId=@intRegistrationId AND IsAccepted=1                                        
                                        
 UNION ALL                                        
                                          
  SELECT                                         
                                             
    Scrl_UserRequestInvitationTbl.intRegistrationId,Scrl_UserRequestInvitationTbl.intRegistrationId intInvitedUserId,intRequestInvitaionId Id,IsAccepted Accepted,intUserTypeId, '' as notification_detail,                                        
    (SELECT vchrFirstName  From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) FirstName,                                          
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) Name,                                          
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) vchrPhotoPath,                                               
    CONVERT(VARCHAR(12),dtReqAcceptedDate,106) dtReqAcceptedDate,                                             
    CONVERT(VARCHAR(12),Scrl_UserRequestInvitationTbl.dtAddedOn,106) dtAddedOn,                                         
                                                 
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                                         
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                           
     ELSE '' END                                         
IsAccepted                                             
  FROM Scrl_UserRequestInvitationTbl                                         
  INNER JOIN scrl_RegistrationTbl ON Scrl_UserRequestInvitationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                                        
  WHERE Scrl_UserRequestInvitationTbl.intInvitedUserId=@intRegistrationId AND IsAccepted=1                                        
  )B                                        
                                
 END                                        
END                                        
                                        
ELSE IF @FlagNo=7                                        
BEGIN                                        
 SELECT   *    FROM                                        
 (                                        
 SELECT TOP 5 *, COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY dtAddedOn desc ) AS RowNumber  FROM                                        
 (                                         
   SELECT                                         
   intRequestInvitaionId Id,Scrl_UserRequestInvitationTbl.intRegistrationId,intInvitedUserId,'' strInvitee, '' strGroupName,'' StrRecommendation,                                        
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) Name,                                        
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) vchrPhotoPath,                                                    
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) vchrUserName,                                        
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserRequestInvitationTbl.intRegistrationId) intUserTypeID,                        
  (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId and                                         
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                                        
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                                        
   CONVERT(VARCHAR(12),dtReqAcceptedDate,109) dtReqAcceptedDate,                                             
   CONVERT(VARCHAR(12),Scrl_UserRequestInvitationTbl.dtAddedOn,109) dtAddedOn,strTableName,                                        
        
  CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                                         
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                           
    ELSE '' END                                         
    IsAccepted,Scrl_UserRequestInvitationTbl.dtAddedOn AS dtAdded,IsAccepted AS IsAccept,'' AS intIsAccept                                             
  FROM Scrl_UserRequestInvitationTbl                                         
  LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserRequestInvitationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                                        
  WHERE Scrl_UserRequestInvitationTbl.intInvitedUserId=@intRegistrationId AND (IsAccepted='0' OR IsAccepted='1')                                         
                                           
  UNION ALL                                        
                                           
  SELECT                                         
   intRecommendationId Id,Scrl_UserRecommendationTbl.intRegistrationId,intInvitedUserId,'' strInvitee,'' strGroupName,StrRecommendation,                                         
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId) Name,                                        
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId) vchrPhotoPath,                                                    
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId) vchrUserName,                                        
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserRecommendationTbl.intRegistrationId) intUserTypeID,                                        
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId and                                         
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                                        
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                                        
    '' dtReqAcceptedDate,                                             
   CONVERT(VARCHAR(12),Scrl_UserRecommendationTbl.dtAddedOn,109) dtAddedOn,strTableName,                                         
                                                
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                     
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                           
    ELSE '' END                                         
    IsAccepted,Scrl_UserRecommendationTbl.dtAddedOn AS dtAdded,IsAccepted AS IsAccept,'' AS intIsAccept                                             
  FROM Scrl_UserRecommendationTbl                                         
  LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserRecommendationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                                        
  WHERE Scrl_UserRecommendationTbl.intInvitedUserId=@intRegistrationId AND IsAccepted='1'                                   
                                           
  UNION ALL                                        
                                              
   SELECT                                         
    intRequestJoinId Id,Scrl_UserGroupJoiningTbl.intRegistrationId,intInvitedUserId,'' strInvitee, strGroupName, '' StrRecommendation,                                        
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) Name,-- change intregistration to intInvitedid date 20-5-15 kiran                                 
  
     
     
       
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrPhotoPath,                                                    
    (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrUserName,                                        
    (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) intUserTypeID,                                        
    (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_UserGroupJoiningTbl.intRegistrationId and                                         
    (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                                        
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                                        
    CONVERT(VARCHAR(12),dtReqAcceptedDate,109) dtReqAcceptedDate,                                             
    CONVERT(VARCHAR(12),Scrl_UserGroupJoiningTbl.dtAddedOn,109) dtAddedOn,strTableName,                                        
                                                 
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                                         
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                  
    ELSE '' END                                         
    IsAccepted,Scrl_UserGroupJoiningTbl.dtAddedOn AS dtAdded,IsAccepted AS IsAccept ,'' AS intIsAccept                                             
  FROM Scrl_UserGroupJoiningTbl                                         
    LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId                                        
    LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId                                        
  WHERE Scrl_UserGroupJoiningTbl.intInvitedUserId=@intRegistrationId AND (IsAccepted='0'OR IsAccepted='2')-- OR IsAccepted='1')           --change date 20-5-15 kiran                                        
   AND Scrl_UserGroupDetailTbl.strAccess='R'                                        
                                              
  UNION ALL                                        
                                        
  SELECT                                        
   intSharedId Id,Scrl_GroupShareTbl.intAddedBy intRegistrationId,intGroupId intInvitedUserId,strInvitee,                                        
    Scrl_UserGroupDetailTbl.strGroupName,                                        
   '' StrRecommendation,                                        
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) Name,                                        
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) vchrPhotoPath,                                                    
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) vchrUserName,                                        
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) intUserTypeID,                                        
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_GroupShareTbl.intAddedBy and                                         
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                                        
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                                        
   '' dtReqAcceptedDate,                                             
   CONVERT(VARCHAR(12),Scrl_GroupShareTbl.dtAddedOn,109) dtAddedOn,strTableName,                                            
   '' IsAccepted,Scrl_GroupShareTbl.dtAddedOn AS dtAdded,'' AS IsAccept,'' AS intIsAccept                                                      
                                                      
  FROM Scrl_GroupShareTbl                                              
    LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_GroupShareTbl.intGroupId                                                 
  WHERE 1=                                        
    (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInvitee,',') AS a                             
    INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                                        
    THEN 1 ELSE  0 END matchId)                                  
                                        
     UNION ALL                                        
      SELECT                                        
    intQAReplyLikeShareId Id,Scrl_UserPostQAReplyTbl.intAddedBy intRegistrationId,Scrl_UserPostQAReplyTbl.intPostQuestionId strInvitee,strInvitee,                                        
    (SUBSTRING ( Scrl_UserPostQAReplyTbl.strFileName ,0 , 50 )+CASE WHEN LEN(Scrl_UserPostQAReplyTbl.strFileName)>50 THEN '...' ELSE '' END)strGroupName,                                        
    strSharelink AS StrRecommendation,                                        
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) Name,                                        
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) vchrPhotoPath,                                                    
    (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) vchrUserName,                                        
    (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) intUserTypeID,                                        
    '' Designation,                                        
    '' dtReqAcceptedDate,                                             
    CONVERT(VARCHAR(12),Scrl_UserPostQAReplyTbl.dtAddedOn,109) dtAddedOn,strTableName,                                            
    '' IsAccepted,Scrl_UserPostQAReplyTbl.dtAddedOn AS dtAdded,'' AS IsAccept,'' AS intIsAccept                                                      
                                                      
    FROM Scrl_UserPostQAReplyTbl                                              
    LEFT OUTER JOIN Scrl_QuestionAnsTbl ON Scrl_QuestionAnsTbl.intPostQuestionId=Scrl_UserPostQAReplyTbl.intPostQuestionId                                                 
    WHERE 1=                                        
    (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInvitee,',') AS a                                        
    INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                                        
    THEN 1 ELSE  0 END matchId) AND strRepLiShStatus='SH'                                        
                                        
    UNION ALL                                        
                                        
      SELECT                                        
    intBlogHeadingLikeId Id,Scrl_BlogHeadingLikeShareTbl.intAddedBy intRegistrationId,Scrl_BlogHeadingLikeShareTbl.intBlogId strInviteeShare,strInviteeShare,                                        
    (SUBSTRING ( Scrl_BlogHeadingLikeShareTbl.strBlogTitle ,0 , 50 )+CASE WHEN LEN(Scrl_BlogHeadingLikeShareTbl.strBlogTitle)>50 THEN '...' ELSE '' END)strGroupName,                                        
    strLink AS StrRecommendation,                                        
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) Name,                                        
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) vchrPhotoPath,                                                    
    (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) vchrUserName,                                        
    (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) intUserTypeID,                                        
    '' Designation,                                        
    '' dtReqAcceptedDate,                                             
    CONVERT(VARCHAR(12),Scrl_BlogHeadingLikeShareTbl.dtAddedOn,109) dtAddedOn,strTableName,                           
    '' IsAccepted,Scrl_BlogHeadingLikeShareTbl.dtAddedOn AS dtAdded,'' AS IsAccept,'' AS intIsAccept                                                      
                                                      
    FROM Scrl_BlogHeadingLikeShareTbl                                              
    LEFT OUTER JOIN Scrl_NewBlogsTbl ON Scrl_NewBlogsTbl.intBlogId=Scrl_BlogHeadingLikeShareTbl.intBlogId                                     
    WHERE 1=                                        
    (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInviteeShare,',') AS a                        
    INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                                        
    THEN 1 ELSE  0 END matchId) AND strRepLiShStatus='SH'                                        
                                        
                                        
                                        
  UNION ALL                                        
   SELECT intOrgEndorseId Id,intRegistrationId,intInvitedUserId,'' strInvitee,                                        
   '' strGroupName,StrEndorseMess StrRecommendation,                                        
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_OrgEndorsement.intAddedBy) Name,                                        
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_OrgEndorsement.intAddedBy) vchrPhotoPath,                                         
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_OrgEndorsement.intAddedBy) vchrUserName,                                        
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_OrgEndorsement.intAddedBy) intUserTypeID,                                         
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_OrgEndorsement.intAddedBy and                                         
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                                         
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                         
   '' dtReqAcceptedDate,                                             
   CONVERT(VARCHAR(12),Scrl_OrgEndorsement.dtAddedOn,109) dtAddedOn,strTableName,                                            
   '' IsAccepted,Scrl_OrgEndorsement.dtAddedOn AS dtAdded,'' AS IsAccept,'' AS intIsAccept                                         
   FROM Scrl_OrgEndorsement                                          
   WHERE Scrl_OrgEndorsement.intInvitedUserId=@intRegistrationId AND IsAccepted='0'                                         
                                                
     UNION ALL                                        
                                        
      SELECT                                        
    intMicroLikeShareId Id,Scrl_MicrolTagLikeShareTbl.intAddedBy intRegistrationId,Scrl_MicrolTagLikeShareTbl.intMicroLikeShareId strInviteeShare,strInviteeShare,                                        
    (SUBSTRING ( Scrl_CaseTbl.strCaseTitle ,0 , 50 )+CASE WHEN LEN(Scrl_CaseTbl.strCaseTitle)>50 THEN '...' ELSE '' END)strGroupName,                                        
    strLink AS StrRecommendation,                                        
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) Name,                                        
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) vchrPhotoPath,                                                    
    (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) vchrUserName,                                      
    (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) intUserTypeID,                                        
    '' Designation,                
    '' dtReqAcceptedDate,                                             
    CONVERT(VARCHAR(12),Scrl_MicrolTagLikeShareTbl.dtAddedOn,109) dtAddedOn,'Scrl_MicrolTagLikeShareTbl' strTableName,                                            
    '' IsAccepted,Scrl_MicrolTagLikeShareTbl.dtAddedOn AS dtAdded,'' AS IsAccept,'' AS intIsAccept                                                      
                                                      
    FROM Scrl_MicrolTagLikeShareTbl                                              
    LEFT OUTER JOIN Scrl_CaseTbl ON Scrl_CaseTbl.intCaseId=Scrl_MicrolTagLikeShareTbl.intDocId                                                 
    WHERE 1=                                        
    (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInviteeShare,',') AS a                                        
    INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                                        
    THEN 1 ELSE  0 END matchId) AND strRepLiShStatus='SH'                        
                                        
    UNION ALL                                   
                         
  SELECT      intGrpInvitationMemberId Id,Scrl_GroupMemberDetailsTbl.intAddedBy,strMemberName intInvitedUserId,CONVERT(VARCHAR(500),Scrl_UserGroupDetailTbl.inGroupId) strInvitee, strGroupName, '' StrRecommendation,          
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) Name,          
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) vchrPhotoPath,                      
    (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) vchrUserName,          
    (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) intUserTypeID,          
    (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_GroupMemberDetailsTbl.intAddedBy and           
    (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)          
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,          
    '' dtReqAcceptedDate,               
    CONVERT(VARCHAR(12),Scrl_GroupMemberDetailsTbl.dtAddedOn,109) dtAddedOn,'Scrl_RequestGroupJoin' strTableName,          
    '' intIsAccept,Scrl_GroupMemberDetailsTbl.dtAddedOn AS dtAdded,'' AS IsAccept, intIsAccept AS intIsAccept                
  FROM Scrl_GroupMemberDetailsTbl           
    LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_GroupMemberDetailsTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId          
    LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_GroupMemberDetailsTbl.inGroupId=Scrl_UserGroupDetailTbl.inGroupId          
  WHERE Scrl_GroupMemberDetailsTbl.strMemberName=@intRegistrationId AND (Scrl_GroupMemberDetailsTbl.intIsAccept IS NULL OR intIsAccept=1)           
  AND Scrl_UserGroupDetailTbl.strAccess='R'                                     
                                        
  UNION ALL            
                                        
   SELECT                                        
    intGrpStatusUpdateId Id,Scrl_GrpUserStatusUpdateTbl.intAddedBy intRegistrationId,intAddedBy,strInvitee,                                       
    (SUBSTRING ( Scrl_GrpUserStatusUpdateTbl.strPostDescription,0 , 50 )+CASE WHEN LEN(Scrl_GrpUserStatusUpdateTbl.strPostDescription)>50 THEN '...' ELSE '' END)strGroupName,                                        
    strPostLink AS StrRecommendation,                                        
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GrpUserStatusUpdateTbl.intAddedBy) Name,                                        
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GrpUserStatusUpdateTbl.intAddedBy) vchrPhotoPath,                                                    
    (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GrpUserStatusUpdateTbl.intAddedBy) vchrUserName,                                        
    (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GrpUserStatusUpdateTbl.intAddedBy) intUserTypeID,                                        
    '' Designation,                                        
    '' dtReqAcceptedDate,                                             
    CONVERT(VARCHAR(12),Scrl_GrpUserStatusUpdateTbl.dtAddedOn,109) dtAddedOn,'Scrl_GrpShareUserStatusTbl' strTableName,                                            
    '' IsAccepted,Scrl_GrpUserStatusUpdateTbl.dtAddedOn AS dtAdded,'' AS IsAccept,'' AS intIsAccept                              
    FROM Scrl_GrpUserStatusUpdateTbl                                              
    WHERE  1=                                        
    (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInvitee,',') AS a                         
    INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                                        
    THEN 1 ELSE  0 END matchId) AND intShared=1                                     
                                      
  union ALL                  
  SELECT @intRegistrationId as Id, @intRegistrationId as intRegistrationId, '' as intInvitedUserId, '' as strInvitee,(SUBSTRING ( SCL_SA_Notifications.notification_detail ,0 , 50 )+CASE WHEN LEN(SCL_SA_Notifications.notification_detail)>50 THEN '...' ELSE '' END)strGroupName,'' StrRecommendation,  '' as Name,'' as vchrPhotoPath,'' as vchrUserName,                   
''  as intUserTypeID ,'' as Designation, '' as dtReqAcceptedDate,dateAddedOn,'SCL_SA_Notifications' as strTableName,'' as IsAccepted, dateAddedOn as dtAdded,0 as IsAccept,0 AS intIsAccept                                      
  from SCL_SA_Notifications                    
                                   
                                          
                            
   -- Writen by kiran...                                        
   UNION ALL                                        
  select intRequestJoinId Id,intRegistrationId,intInvitedUserId,'' strInvitee,                                        
   (select strGroupName From Scrl_OrganizationGroupDetailTbl WHERE inGroupId= Scrl_OrgnisationGroupJoiningTbl.inGroupId ) strGroupName,'' StrRecommendation,                                        
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_OrgnisationGroupJoiningTbl.intAddedBy) Name,                                        
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_OrgnisationGroupJoiningTbl.intAddedBy) vchrPhotoPath,                                         
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_OrgnisationGroupJoiningTbl.intAddedBy) vchrUserName,                                        
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_OrgnisationGroupJoiningTbl.intAddedBy) intUserTypeID,                                         
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_OrgnisationGroupJoiningTbl.intAddedBy and                                         
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                                         
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                                        
   '' dtReqAcceptedDate,                                             
   CONVERT(VARCHAR(12),Scrl_OrgnisationGroupJoiningTbl.dtAddedOn,109) dtAddedOn,strTableName,                                            
   '' IsAccepted,Scrl_OrgnisationGroupJoiningTbl.dtAddedOn AS dtAdded,'' AS IsAccept,'' AS intIsAccept                                         
   FROM Scrl_OrgnisationGroupJoiningTbl                   WHERE Scrl_OrgnisationGroupJoiningTbl.intInvitedUserId=@intRegistrationId AND IsAccepted='0'                                         
    ) AS T  order by dtAdded desc                                        
 ) AS X WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand       
 and   dtAdded >(SELECT DateAdd(month, -1, Convert(date, (select dtAddedOn from scrl_RegistrationTbl where intRegistrationId=@intRegistrationId))))             
END                                         
                                         
--To Get The Distinct Date for Notification_Details page                                        
ELSE IF @FlagNo=8                                        
                    
                  
BEGIN                    
  SELECT   *    FROM                    
  (                    
  SELECT *, COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY dtAddedOn1 desc ) AS RowNumber  FROM                    
  (                     
   SELECT  DISTINCT CONVERT(VARCHAR(11), Scrl_UserRequestInvitationTbl.dtAddedOn, 13) dtAddedOn, dbo.dateonly(Scrl_UserRequestInvitationTbl.dtAddedOn) dtAddedOn1                            
   FROM Scrl_UserRequestInvitationTbl                     
   LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserRequestInvitationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                    
   WHERE Scrl_UserRequestInvitationTbl.intInvitedUserId=@intRegistrationId AND (IsAccepted='0' OR IsAccepted='1')                     
                          
   UNION                     
                          
   SELECT DISTINCT CONVERT(VARCHAR(11), Scrl_UserRecommendationTbl.dtAddedOn, 13) dtAddedOn , dbo.dateonly(Scrl_UserRecommendationTbl.dtAddedOn) dtAddedOn1                        
   FROM Scrl_UserRecommendationTbl                     
     LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserRecommendationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                     
   WHERE Scrl_UserRecommendationTbl.intInvitedUserId=@intRegistrationId AND (IsAccepted='0' OR IsAccepted='2')                    
                           
   UNION                     
                          
   SELECT DISTINCT CONVERT(VARCHAR(11), Scrl_UserGroupJoiningTbl.dtAddedOn, 13) dtAddedOn , dbo.dateonly(Scrl_UserGroupJoiningTbl.dtAddedOn) dtAddedOn1                        
   FROM Scrl_UserGroupJoiningTbl                     
     LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId                    
     LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId                          
   WHERE Scrl_UserGroupJoiningTbl.intInvitedUserId=@intRegistrationId                     
   AND (IsAccepted='0')                    
   AND Scrl_UserGroupDetailTbl.strAccess='R'                    
   ---- Comment on 18 may 2015 'kiran'                    
    UNION                     
    SELECT DISTINCT CONVERT(VARCHAR(11), Scrl_GroupMemberDetailsTbl.dtAddedOn, 13) dtAddedOn , dbo.dateonly(Scrl_GroupMemberDetailsTbl.dtAddedOn) dtAddedOn1                        
   FROM Scrl_GroupMemberDetailsTbl                     
     LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_GroupMemberDetailsTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId                    
     LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_GroupMemberDetailsTbl.inGroupId                          
   WHERE Scrl_GroupMemberDetailsTbl.strMemberName=@intRegistrationId AND (Scrl_GroupMemberDetailsTbl.intIsAccept IS NULL OR intIsAccept=1)                     
   AND Scrl_UserGroupDetailTbl.strAccess='R'                    
                        
   -- Added By Kiran .....                    
   UNION                     
   SELECT DISTINCT CONVERT(VARCHAR(11), Scrl_OrgnisationGroupJoiningTbl.dtAddedOn, 13) dtAddedOn , dbo.dateonly(Scrl_OrgnisationGroupJoiningTbl.dtAddedOn) dtAddedOn1                        
   FROM Scrl_OrgnisationGroupJoiningTbl                     
     LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_OrgnisationGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId                    
     LEFT OUTER JOIN Scrl_OrganizationGroupDetailTbl ON Scrl_OrganizationGroupDetailTbl.inGroupId=Scrl_OrgnisationGroupJoiningTbl.inGroupId                          
   WHERE Scrl_OrgnisationGroupJoiningTbl.intInvitedUserId=@intRegistrationId AND IsAccepted='0'                    
   --- End Kiran                    
   UNION                     
                    
   SELECT DISTINCT CONVERT(VARCHAR(11), Scrl_GroupShareTbl.dtAddedOn, 13) dtAddedOn, dbo.dateonly(Scrl_GroupShareTbl.dtAddedOn) dtAddedOn1                         
   FROM Scrl_GroupShareTbl                       
     LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_GroupShareTbl.intGroupId                          
   WHERE 1=                    
     (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInvitee,',') AS a             
     INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                    
     THEN 1 ELSE  0 END matchId)                    
                    
     UNION                    
                    
     SELECT DISTINCT CONVERT(VARCHAR(11), Scrl_BlogHeadingLikeShareTbl.dtAddedOn, 13) dtAddedOn, dbo.dateonly(Scrl_BlogHeadingLikeShareTbl.dtAddedOn) dtAddedOn1                         
   FROM Scrl_BlogHeadingLikeShareTbl                       
     LEFT OUTER JOIN Scrl_NewBlogsTbl ON Scrl_NewBlogsTbl.intBlogId=Scrl_BlogHeadingLikeShareTbl.intBlogId                 
   WHERE 1=                    
     (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInviteeShare,',') AS a                    
     INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                    
     THEN 1 ELSE  0 END matchId)                    
                    
     UNION                    
                  
                                     
 ---- --SA Notifications                              
                              
    SELECT DISTINCT CONVERT(VARCHAR(11), SCL_SA_Notifications.dateaddedon, 13)  as dtAddedOn, dbo.dateonly(SCL_SA_Notifications.dateaddedon) dtAddedOn1                                           
  FROM SCL_SA_Notifications                       
 union                            
                                        
                  
                  
                    
     SELECT DISTINCT CONVERT(VARCHAR(11), Scrl_UserPostQAReplyTbl.dtAddedOn, 13) dtAddedOn, dbo.dateonly(Scrl_UserPostQAReplyTbl.dtAddedOn) dtAddedOn1                         
   FROM Scrl_UserPostQAReplyTbl                       
     LEFT OUTER JOIN Scrl_QuestionAnsTbl ON Scrl_QuestionAnsTbl.intPostQuestionId=Scrl_UserPostQAReplyTbl.intPostQuestionId                           
   WHERE 1=                    
     (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInvitee,',') AS a                    
     INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                    
     THEN 1 ELSE  0 END matchId)                    
                    
     UNION                    
                    
SELECT DISTINCT CONVERT(VARCHAR(11), Scrl_MicrolTagLikeShareTbl.dtAddedOn, 13) dtAddedOn, dbo.dateonly(Scrl_MicrolTagLikeShareTbl.dtAddedOn) dtAddedOn1                         
   FROM Scrl_MicrolTagLikeShareTbl                       
     LEFT OUTER JOIN Scrl_CaseTbl ON Scrl_CaseTbl.intCaseId=Scrl_MicrolTagLikeShareTbl.intDocId                          
   WHERE 1=                    
     (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInviteeShare,',') AS a                    
     INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                    
     THEN 1 ELSE  0 END matchId)             
     UNION                    
                    
     SELECT DISTINCT CONVERT(VARCHAR(11), Scrl_GrpUserStatusUpdateTbl.dtAddedOn, 13) dtAddedOn, dbo.dateonly(Scrl_GrpUserStatusUpdateTbl.dtAddedOn) dtAddedOn1                         
   FROM Scrl_GrpUserStatusUpdateTbl                       
   WHERE 1=                    
     (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInvitee,',') AS a                    
     INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                    
     THEN 1 ELSE  0 END matchId)AND intShared=1                    
                    
                    
 ) AS T          
  ) AS X WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand  and   dtAddedOn1 >(SELECT DateAdd(month, -1, Convert(date, (select dtAddedOn from scrl_RegistrationTbl where intRegistrationId=@intRegistrationId))))    
  ORDER BY dtAddedOn1 DESC        
 END                   
                    
                                        
ELSE IF @FlagNo=9                                        
                  
                  
BEGIN                    
 SELECT * FROM (                    
  SELECT                     
   intRequestInvitaionId Id,Scrl_UserRequestInvitationTbl.intRegistrationId,intInvitedUserId,'' strInvitee, '' strGroupName,'' StrRecommendation,                    
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) Name,                    
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) vchrPhotoPath,                                
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) vchrUserName,                    
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserRequestInvitationTbl.intRegistrationId) intUserTypeID,                    
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId and                     
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                    
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                    
   CONVERT(VARCHAR(12),dtReqAcceptedDate,109) dtReqAcceptedDate,                        
   CONVERT(VARCHAR(12),Scrl_UserRequestInvitationTbl.dtAddedOn,109) dtAddedOn,strTableName,                    
   LTRIM(RIGHT(CONVERT(VARCHAR(20), Scrl_UserRequestInvitationTbl.dtAddedOn, 100), 7)) AddedTime,                    
                            
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                    
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                     
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                       
    ELSE '' END                     
    IsAccepted,Scrl_UserRequestInvitationTbl.dtAddedOn as dtAdded,                    
    ' ' AS strMessage, ' ' AS strLink,IsAccepted AS IsAccept,' ' AS intIsAccept                         
  FROM Scrl_UserRequestInvitationTbl                     
  LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserRequestInvitationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                    
  WHERE Scrl_UserRequestInvitationTbl.intInvitedUserId=@intRegistrationId AND (IsAccepted='0' OR IsAccepted='1') AND CONVERT(VARCHAR(11), Scrl_UserRequestInvitationTbl.dtAddedOn, 13)=@NotificationDate                    
                       
                       
  UNION ALL                    
                       
                      
  SELECT intRecommendationId Id,Scrl_UserRecommendationTbl.intRegistrationId,intInvitedUserId,'' strInvitee,'' strGroupName,                    
   --StrRecommendation                    
    (' '+ [dbo].[SubjectList](ISNULL(strSubject,0))) AS StrRecommendation                  
   ,                     
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId) Name,                    
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId) vchrPhotoPath,                                
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId) vchrUserName,                    
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserRecommendationTbl.intRegistrationId) intUserTypeID,                    
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId and                     
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                    
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                    
    '' dtReqAcceptedDate,                         
   CONVERT(VARCHAR(12),Scrl_UserRecommendationTbl.dtAddedOn,109) dtAddedOn,strTableName,                     
   LTRIM(RIGHT(CONVERT(VARCHAR(20), Scrl_UserRecommendationTbl.dtAddedOn, 100), 7)) AddedTime,                    
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                    
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                     
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                       
    ELSE '' END                     
    IsAccepted,Scrl_UserRecommendationTbl.dtAddedOn as dtAdded,                    
    Scrl_UserRecommendationTbl.StrRecommendation AS strMessage, ' ' AS strLink,IsAccepted AS IsAccept,' ' AS intIsAccept                         
  FROM Scrl_UserRecommendationTbl                     
  LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserRecommendationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                    
  WHERE Scrl_UserRecommendationTbl.intInvitedUserId=@intRegistrationId AND IsAccepted='1'  AND CONVERT(VARCHAR(11), Scrl_UserRecommendationTbl.dtAddedOn, 13)=@NotificationDate                      
                    
  UNION ALL                    
                          
  SELECT                       
    intRequestJoinId Id,Scrl_UserGroupJoiningTbl.intRegistrationId,intInvitedUserId,'' strInvitee ,strGroupName, '' StrRecommendation,                    
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) Name,-- chang by kiran 20 May 2015                    
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrPhotoPath,                                
    (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrUserName,                    
    (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) intUserTypeID,                    
    (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_UserGroupJoiningTbl.intRegistrationId and                     
    (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                    
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                    
    CONVERT(VARCHAR(12),dtReqAcceptedDate,109) dtReqAcceptedDate,                         
    CONVERT(VARCHAR(12),Scrl_UserGroupJoiningTbl.dtAddedOn,109) dtAddedOn,strTableName,                    
    LTRIM(RIGHT(CONVERT(VARCHAR(20), Scrl_UserGroupJoiningTbl.dtAddedOn, 100), 7)) AddedTime,                    
                             
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                    
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                     
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                       
    ELSE '' END                     
    IsAccepted,Scrl_UserGroupJoiningTbl.dtAddedOn as dtAdded,                    
    ' ' AS strMessage, ' ' AS strLink,IsAccepted AS IsAccept,' ' AS intIsAccept                         
  FROM Scrl_UserGroupJoiningTbl                     
    LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId                    
    LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId                    
     WHERE Scrl_UserGroupJoiningTbl.intInvitedUserId=@intRegistrationId  AND (IsAccepted='0'OR IsAccepted='1')--OR IsAccepted='2')                    
      AND CONVERT(VARCHAR(11), Scrl_UserGroupJoiningTbl.dtAddedOn, 13)=@NotificationDate                    
      AND Scrl_UserGroupDetailTbl.strAccess='R'-- AND Scrl_UserGroupDetailTbl.intAddedBy!=@intRegistrationId                    
                              
   UNION ALL                    
                       
   SELECT                       
    intGrpInvitationMemberId Id,Scrl_GroupMemberDetailsTbl.intAddedBy,strMemberName,CONVERT(VARCHAR(500),Scrl_UserGroupDetailTbl.inGroupId) strInvitee ,strGroupName, '' StrRecommendation,                    
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) Name,                    
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) vchrPhotoPath,                                
    (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) vchrUserName,                    
    (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) intUserTypeID,                    
    (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_GroupMemberDetailsTbl.intAddedBy and                     
    (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                    
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                    
    '' dtReqAcceptedDate,                         
    CONVERT(VARCHAR(12),Scrl_GroupMemberDetailsTbl.dtAddedOn,109) dtAddedOn,'Scrl_RequestGroupJoin' strTableName,                    
    LTRIM(RIGHT(CONVERT(VARCHAR(20), Scrl_GroupMemberDetailsTbl.dtAddedOn, 100), 7)) AddedTime,                    
  '' IsAccepted,Scrl_GroupMemberDetailsTbl.dtAddedOn as dtAdded,                    
    ' ' AS strMessage, ' ' AS strLink,'' AS IsAccept,Scrl_GroupMemberDetailsTbl.intIsAccept AS intIsAccept                         
                        
   FROM Scrl_GroupMemberDetailsTbl                  LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_GroupMemberDetailsTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId                    
     LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_GroupMemberDetailsTbl.inGroupId                          
   WHERE Scrl_GroupMemberDetailsTbl.strMemberName=@intRegistrationId AND (intIsAccept IS NULL OR intIsAccept=1) AND CONVERT(VARCHAR(11), Scrl_GroupMemberDetailsTbl.dtAddedOn, 13)=@NotificationDate                    
   AND Scrl_UserGroupDetailTbl.strAccess='R'                    
                          
  UNION ALL                    
                    
  SELECT                     
    intSharedId Id,Scrl_GroupShareTbl.intAddedBy intRegistrationId,intGroupId intInvitedUserId,strInvitee,                    
    Scrl_UserGroupDetailTbl.strGroupName,                    
   '' StrRecommendation,                    
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) Name,                    
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) vchrPhotoPath,                                
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) vchrUserName,                    
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) intUserTypeID,               
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_GroupShareTbl.intAddedBy and                     
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                    
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                    
   '' dtReqAcceptedDate,                         
   CONVERT(VARCHAR(12),Scrl_GroupShareTbl.dtAddedOn,109) dtAddedOn,strTableName,                    
   LTRIM(RIGHT(CONVERT(VARCHAR(20), Scrl_GroupShareTbl.dtAddedOn, 100), 7)) AddedTime,                     
   '' IsAccepted,Scrl_GroupShareTbl.dtAddedOn as dtAdded,                    
    Scrl_GroupShareTbl.strMessage AS strMessage, Scrl_GroupShareTbl.strLink AS strLink,'' AS IsAccept,' ' AS intIsAccept                                  
                                  
  FROM Scrl_GroupShareTbl                          
    LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_GroupShareTbl.intGroupId                          
  WHERE                     
    (1=                    
    (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInvitee,',') AS a                    
    INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                    
    THEN 1 ELSE  0 END matchId))                    
    AND CONVERT(VARCHAR(11), Scrl_GroupShareTbl.dtAddedOn, 13)=@NotificationDate                    
                        
                    
                    
     UNION ALL                    
                    
  SELECT                     
    intQAReplyLikeShareId Id,Scrl_UserPostQAReplyTbl.intAddedBy intRegistrationId,Scrl_UserPostQAReplyTbl.intPostQuestionId intInvitedUserId,strInvitee,                    
    Scrl_UserPostQAReplyTbl.strFileName AS strGroupName,                    
   strSharelink AS StrRecommendation,           (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) Name,                    
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) vchrPhotoPath,                                
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) vchrUserName,                    
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) intUserTypeID,                    
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_UserPostQAReplyTbl.intAddedBy and                     
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                    
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                    
   '' dtReqAcceptedDate,                         
   CONVERT(VARCHAR(12),Scrl_UserPostQAReplyTbl.dtAddedOn,109) dtAddedOn,strTableName,                    
   LTRIM(RIGHT(CONVERT(VARCHAR(20), Scrl_UserPostQAReplyTbl.dtAddedOn, 100), 7)) AddedTime,                     
   '' IsAccepted,Scrl_UserPostQAReplyTbl.dtAddedOn as dtAdded,                    
    Scrl_UserPostQAReplyTbl.strComment AS strMessage, Scrl_UserPostQAReplyTbl.strSharelink AS strLink,'' AS IsAccept,' ' AS intIsAccept                                  
                                  
  FROM Scrl_UserPostQAReplyTbl                          
    LEFT OUTER JOIN Scrl_QuestionAnsTbl ON Scrl_QuestionAnsTbl.intPostQuestionId=Scrl_UserPostQAReplyTbl.intPostQuestionId                          
  WHERE                     
    (1=                    
    (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInvitee,',') AS a                    
    INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                    
    THEN 1 ELSE  0 END matchId))                    
    AND CONVERT(VARCHAR(11), Scrl_UserPostQAReplyTbl.dtAddedOn, 13)=@NotificationDate                    
                        
                    
    UNION ALL                    
                    
  SELECT                     
    intBlogHeadingLikeId Id,Scrl_BlogHeadingLikeShareTbl.intAddedBy intRegistrationId,Scrl_BlogHeadingLikeShareTbl.intBlogId intInvitedUserId,strInviteeShare,                    
    Scrl_BlogHeadingLikeShareTbl.strBlogTitle AS strGroupName,                    
   strLink AS StrRecommendation,                    
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) Name,                    
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) vchrPhotoPath,                                
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) vchrUserName,                    
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) intUserTypeID,                    
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_BlogHeadingLikeShareTbl.intAddedBy and                     
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                    
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                    
   '' dtReqAcceptedDate,                         
   CONVERT(VARCHAR(12),Scrl_BlogHeadingLikeShareTbl.dtAddedOn,109) dtAddedOn,strTableName,                    
   LTRIM(RIGHT(CONVERT(VARCHAR(20), Scrl_BlogHeadingLikeShareTbl.dtAddedOn, 100), 7)) AddedTime,                     
   '' IsAccepted,Scrl_BlogHeadingLikeShareTbl.dtAddedOn as dtAdded,                    
Scrl_BlogHeadingLikeShareTbl.strMessage AS strMessage, Scrl_BlogHeadingLikeShareTbl.strLink AS strLink,'' AS IsAccept,' ' AS intIsAccept                                  
                                  
  FROM Scrl_BlogHeadingLikeShareTbl                          
    LEFT OUTER JOIN Scrl_NewBlogsTbl ON Scrl_NewBlogsTbl.intBlogId=Scrl_BlogHeadingLikeShareTbl.intBlogId                          
  WHERE                     
    (1=                    
    (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInviteeShare,',') AS a                    
  INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                    
    THEN 1 ELSE  0 END matchId))                    
    AND CONVERT(VARCHAR(11), Scrl_BlogHeadingLikeShareTbl.dtAddedOn, 13)=@NotificationDate                    
                        
    UNION ALL                    
                    
                    
                                   
  Select Notification_ID as Id, 0 as intRegistrationId, 0 as intInvitedUserId,  '' as strInviteeShare,Notification_Detail as strGroupName,  '' StrRecommendation,  '' as Name,'' as vchrPhotoPath,'' as vchrUserName, '' as intUserTypeID,                    
   '' as Designation, '' as dtReqAcceptedDate,dateAddedOn,'SCL_SA_Notifications' as strTableName, LTRIM(RIGHT(CONVERT(VARCHAR(20), SCL_SA_Notifications.dateaddedon, 100), 7)) AddedTime,'' as IsAccepted,dateaddedon as dtAdded, '' as strMessage,'' as strLink,'' as IsAccept,' ' AS intIsAccept                                      
  from SCL_SA_Notifications    where  CONVERT(VARCHAR(11), SCL_SA_Notifications.dateaddedon, 13)=@NotificationDate                      
                        
    UNION ALL                  
                  
  SELECT                     
    intMicroLikeShareId Id,Scrl_MicrolTagLikeShareTbl.intAddedBy intRegistrationId,Scrl_MicrolTagLikeShareTbl.intMicroLikeShareId intInvitedUserId,strInviteeShare,                    
    Scrl_CaseTbl.strCaseTitle strGroupName,                    
   strLink AS StrRecommendation,                    
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) Name,                    
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) vchrPhotoPath,                               (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) vchrUserName,     
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) intUserTypeID,                    
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_MicrolTagLikeShareTbl.intAddedBy and                     
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                    
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                    
   '' dtReqAcceptedDate,                         
   CONVERT(VARCHAR(12),Scrl_MicrolTagLikeShareTbl.dtAddedOn,109) dtAddedOn,'Scrl_MicrolTagLikeShareTbl' strTableName,                    
   LTRIM(RIGHT(CONVERT(VARCHAR(20), Scrl_MicrolTagLikeShareTbl.dtAddedOn, 100), 7)) AddedTime,                     
   '' IsAccepted,Scrl_MicrolTagLikeShareTbl.dtAddedOn as dtAdded,                    
    Scrl_MicrolTagLikeShareTbl.strMessage AS strMessage, Scrl_MicrolTagLikeShareTbl.strLink AS strLink,'' AS IsAccept,' ' AS intIsAccept                                  
                                  
  FROM Scrl_MicrolTagLikeShareTbl                          
    LEFT OUTER JOIN Scrl_CaseTbl ON Scrl_CaseTbl.intCaseId=Scrl_MicrolTagLikeShareTbl.intDocId                          
  WHERE                     
    (1=                    
    (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInviteeShare,',') AS a                    
    INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                    
    THEN 1 ELSE  0 END matchId))                    
    AND CONVERT(VARCHAR(11), Scrl_MicrolTagLikeShareTbl.dtAddedOn, 13)=@NotificationDate                    
                    
    UNION ALL                    
    SELECT                     
    intGrpStatusUpdateId Id,intAddedBy intRegistrationId,intAddedBy intInvitedUserId,strInvitee,                    
    (SUBSTRING ( Scrl_GrpUserStatusUpdateTbl.strPostDescription,0 , 50 )+CASE WHEN LEN(Scrl_GrpUserStatusUpdateTbl.strPostDescription)>50 THEN '...' ELSE '' END) AS strGroupName,                    
   strPostLink AS StrRecommendation,                    
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GrpUserStatusUpdateTbl.intAddedBy) Name,                    
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GrpUserStatusUpdateTbl.intAddedBy) vchrPhotoPath,                                
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GrpUserStatusUpdateTbl.intAddedBy) vchrUserName,                    
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GrpUserStatusUpdateTbl.intAddedBy) intUserTypeID,                    
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_GrpUserStatusUpdateTbl.intAddedBy and                     
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                    
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                    
   '' dtReqAcceptedDate,                         
   CONVERT(VARCHAR(12),Scrl_GrpUserStatusUpdateTbl.dtAddedOn,109) dtAddedOn,'Scrl_GrpShareUserStatusTbl'strTableName,                    
   LTRIM(RIGHT(CONVERT(VARCHAR(20), Scrl_GrpUserStatusUpdateTbl.dtAddedOn, 100), 7)) AddedTime,                     
   '' IsAccepted,Scrl_GrpUserStatusUpdateTbl.dtAddedOn as dtAdded,                    
    (SUBSTRING ( Scrl_GrpUserStatusUpdateTbl.strPostDescription,0 , 50 )+CASE WHEN LEN(Scrl_GrpUserStatusUpdateTbl.strPostDescription)>50 THEN '...' ELSE '' END) AS strMessage, strPostLink AS strLink,'' AS IsAccept,' ' AS intIsAccept                     
   
    
      
        
          
            
                                  
  FROM Scrl_GrpUserStatusUpdateTbl                          
  WHERE                     
    (1=         
    (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInvitee,',') AS a                    
    INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                    
    THEN 1 ELSE  0 END matchId))                    
    AND CONVERT(VARCHAR(11), Scrl_GrpUserStatusUpdateTbl.dtAddedOn, 13)=@NotificationDate                    
    AND intShared=1                    
    ) AS T ORDER BY dtAdded DESC                    
END                   
                                        
ELSE IF @FlagNo=10                                        
BEGIN                                        
 SELECT  intRegistrationId,vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME, vchrPhotoPath, vchrUserName, intUserTypeID,'' as notification_detail,                 
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=scrl_RegistrationTbl.intRegistrationId and                                         
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                                        
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation                                         
 FROM scrl_RegistrationTbl                                               
 WHERE intRegistrationId=@intRegistrationId                                        
END                                        
                                        
--Get Top 12 friendlist                              
ELSE IF @FlagNo=11                                        
BEGIN                                          
                                             
IF EXISTS(SELECT 1 FROM Scrl_UserRequestInvitationTbl WHERE (intRegistrationId=@intRegistrationId OR intInvitedUserId=@intRegistrationId) AND IsAccepted=1)                                        
 BEGIN                                        
                                          
  SELECT TOP 12 *, COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY dtAddedOn desc ) AS RowNumber                                        
  FROM(                                        
                                             
    SELECT Scrl_UserRequestInvitationTbl.intRegistrationId,intInvitedUserId,intRequestInvitaionId Id,IsAccepted Accepted,intUserTypeId,                                        
 '' as notification_detail,                                     
    (SELECT vchrFirstName  From scrl_RegistrationTbl WHERE intRegistrationId=intInvitedUserId) FirstName,                                          
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=intInvitedUserId) Name,                                          
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=intInvitedUserId) vchrPhotoPath,                                               
    CONVERT(VARCHAR(12),dtReqAcceptedDate,106) dtReqAcceptedDate,                     
    CONVERT(VARCHAR(12),Scrl_UserRequestInvitationTbl.dtAddedOn,106) dtAddedOn,                                         
                                                 
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                                         
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                           
     ELSE '' END                                         
    IsAccepted                                             
  FROM Scrl_UserRequestInvitationTbl                                         
 INNER JOIN scrl_RegistrationTbl ON Scrl_UserRequestInvitationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                                        
  WHERE Scrl_UserRequestInvitationTbl.intRegistrationId=@intRegistrationId AND IsAccepted=1                                        
                            
 UNION ALL                                        
                                          
  SELECT                                        
                                             
    Scrl_UserRequestInvitationTbl.intRegistrationId,Scrl_UserRequestInvitationTbl.intRegistrationId intInvitedUserId,intRequestInvitaionId Id,IsAccepted Accepted,intUserTypeId,                                    
 '' as notification_detail,                                         
    (SELECT vchrFirstName  From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) FirstName,                                          
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) Name,                                          
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) vchrPhotoPath,                                               
    CONVERT(VARCHAR(12),dtReqAcceptedDate,106) dtReqAcceptedDate,                                             
    CONVERT(VARCHAR(12),Scrl_UserRequestInvitationTbl.dtAddedOn,106) dtAddedOn,                                         
                                                 
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                                         
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'               
     ELSE '' END                                         
    IsAccepted                                             
  FROM Scrl_UserRequestInvitationTbl                                         
  INNER JOIN scrl_RegistrationTbl ON Scrl_UserRequestInvitationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                                        
  WHERE Scrl_UserRequestInvitationTbl.intInvitedUserId=@intRegistrationId AND IsAccepted=1                                  
               
 )A                                        
 END                                        
                                        
ELSE                                        
 BEGIN                                        
 SELECT TOP 12 *, COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY dtAddedOn desc ) AS RowNumber                                        
  FROM(                                        
                                             
    SELECT Scrl_UserRequestInvitationTbl.intRegistrationId,intInvitedUserId,intRequestInvitaionId Id,IsAccepted Accepted,intUserTypeId,                                      
 '' as notification_detail,                                       
    (SELECT vchrFirstName  From scrl_RegistrationTbl WHERE intRegistrationId=intInvitedUserId) FirstName,                                          
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=intInvitedUserId) Name,                                          
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=intInvitedUserId) vchrPhotoPath,                                               
    CONVERT(VARCHAR(12),dtReqAcceptedDate,106) dtReqAcceptedDate,                                             
    CONVERT(VARCHAR(12),Scrl_UserRequestInvitationTbl.dtAddedOn,106) dtAddedOn,                                         
                                                 
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                       
    WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                                         
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                           
     ELSE '' END                       
    IsAccepted                                             
  FROM Scrl_UserRequestInvitationTbl                                         
  INNER JOIN scrl_RegistrationTbl ON Scrl_UserRequestInvitationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                                        
  WHERE Scrl_UserRequestInvitationTbl.intRegistrationId=@intRegistrationId AND IsAccepted=1                                        
                                        
 UNION ALL                                        
                                          
  SELECT                                         
                                             
    Scrl_UserRequestInvitationTbl.intRegistrationId,Scrl_UserRequestInvitationTbl.intRegistrationId intInvitedUserId,intRequestInvitaionId Id,IsAccepted Accepted,intUserTypeId,                                         
 '' as notification_detail,                                    
    (SELECT vchrFirstName  From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) FirstName,                                          
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) Name,                                          
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) vchrPhotoPath,                                               
    CONVERT(VARCHAR(12),dtReqAcceptedDate,106) dtReqAcceptedDate,                                             
    CONVERT(VARCHAR(12),Scrl_UserRequestInvitationTbl.dtAddedOn,106) dtAddedOn,                                         
                                                 
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                    
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                           
     ELSE '' END                                         
    IsAccepted                         
  FROM Scrl_UserRequestInvitationTbl                                         
  INNER JOIN scrl_RegistrationTbl ON Scrl_UserRequestInvitationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                                   
  WHERE Scrl_UserRequestInvitationTbl.intInvitedUserId=@intRegistrationId AND IsAccepted=1                                        
  )B                                        
                                          
 END                                        
                                            
END                                         
                                         
 ELSE IF @FlagNo=14                                      
 BEGIN                                      
                
                  
SELECT @intRegistrationId as Id, @intRegistrationId as intRegistrationId, (select intInvitedUserId from Scrl_UserRecommendationTbl where intInvitedUserId=@intRegistrationId) as intInvitedUserId, '' as strInvitee, notification_detail , '' as strGroupName,
  
'' StrRecommendation,  '' as Name,'' as vchrPhotoPath,'' as vchrUserName,                   
(SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= @intRegistrationId)  as intUserTypeID ,'' as Designation, '' as dtReqAcceptedDate,dateAddedOn,'SCL_SA_Notifications' as strTableName,'' as IsAccepted, dateAddedOn as dtAdded,0 as IsAccept,0 AS intIsAccept                                      
  from SCL_SA_Notifications                         
                    
                          
 END                                      
                                      
                                         
                 
 Else if @FlagNo=15                
 Begin                
 SELECT   *    FROM                                        
 (                                        
 SELECT *, COUNT(*) OVER()  AS Maxcount ,ROW_NUMBER() OVER (ORDER BY dtAddedOn desc ) AS RowNumber  FROM                                        
 (                                         
   SELECT                                        
   intRequestInvitaionId Id,Scrl_UserRequestInvitationTbl.intRegistrationId,intInvitedUserId,'' strInvitee ,'' strGroupName,'' StrRecommendation,                                        
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) Name,                                        
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) vchrPhotoPath,                                                    
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId) vchrUserName,                                        
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserRequestInvitationTbl.intRegistrationId) intUserTypeID,                                        
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_UserRequestInvitationTbl.intRegistrationId and                                         
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                                        
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                                        
   CONVERT(VARCHAR(12),dtReqAcceptedDate,109) dtReqAcceptedDate,                                             
   Scrl_UserRequestInvitationTbl.dtAddedOn as dtAddedOn,strTableName,                                   
                                                
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                               
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                               
    ELSE '' END                                         
    IsAccepted,IsAccepted AS IsAccept,'' AS intIsAccept                                             
  FROM Scrl_UserRequestInvitationTbl                                         
  LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserRequestInvitationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                                        
  WHERE Scrl_UserRequestInvitationTbl.intInvitedUserId=@intRegistrationId AND (IsAccepted='0' OR IsAccepted='1')                                         
                                        
  UNION ALL                                        
                                           
  SELECT                                         
   intRecommendationId Id,Scrl_UserRecommendationTbl.intRegistrationId,intInvitedUserId,'' strInvitee,'' strGroupName,StrRecommendation,                                         
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId) Name,                                        
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId) vchrPhotoPath,                                                    
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId) vchrUserName,                                        
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserRecommendationTbl.intRegistrationId) intUserTypeID,                                        
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_UserRecommendationTbl.intRegistrationId and                                   
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                                        
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                                        
    '' dtReqAcceptedDate,                                             
   Scrl_UserRecommendationTbl.dtAddedOn as  dtAddedOn,strTableName,                                         
                                                
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                                         
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                           
    ELSE '' END                                         
    IsAccepted,IsAccepted AS IsAccept,'' AS intIsAccept                                             
  FROM Scrl_UserRecommendationTbl                                         
  LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserRecommendationTbl.intRegistrationId=scrl_RegistrationTbl.intRegistrationId                                        
  WHERE Scrl_UserRecommendationTbl.intInvitedUserId=@intRegistrationId AND (IsAccepted='0' OR IsAccepted='2')                                         
  UNION ALL                                        
   SELECT                                           
    intRequestJoinId Id,Scrl_UserGroupJoiningTbl.intRegistrationId,intInvitedUserId,'' strInvitee ,strGroupName, '' StrRecommendation,                                       
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) Name,                       
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrPhotoPath,                                                    
    (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) vchrUserName,                                        
    (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserGroupJoiningTbl.intRegistrationId) intUserTypeID,                                        
    (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_UserGroupJoiningTbl.intRegistrationId and                                         
    (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                                        
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                                        
    CONVERT(VARCHAR(12),dtReqAcceptedDate,109) dtReqAcceptedDate,                                             
    Scrl_UserGroupJoiningTbl.dtAddedOn as dtAddedOn,strTableName,                                        
                                                 
    CASE WHEN IsAccepted='0' THEN  '<span style="color:orange;">Pending</span>'                                        
      WHEN IsAccepted='1' THEN  '<span style="color:Green;">Accepted</span>'                                         
      WHEN IsAccepted='2' THEN  '<span style="color:Red;">Cancelled</span>'                                           
    ELSE '' END                                         
    IsAccepted,IsAccepted AS IsAccept,'' AS intIsAccept                                             
  FROM Scrl_UserGroupJoiningTbl                                         
    LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_UserGroupJoiningTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId                                        
    LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_UserGroupJoiningTbl.inGroupId                                        
  WHERE Scrl_UserGroupJoiningTbl.intInvitedUserId=@intRegistrationId AND (IsAccepted='0' )--OR IsAccepted='2')                                        
                                           
  UNION ALL                                         
     SELECT                                           
    intGrpInvitationMemberId Id,Scrl_GroupMemberDetailsTbl.intAddedBy,strMemberName,CONVERT(VARCHAR(500),Scrl_UserGroupDetailTbl.inGroupId) strInvitee ,strGroupName, '' StrRecommendation,                                       
    (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) Name,                               
    (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) vchrPhotoPath,                                                    
    (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) vchrUserName,                                        
    (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupMemberDetailsTbl.intAddedBy) intUserTypeID,                                        
    (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_GroupMemberDetailsTbl.intAddedBy and                                         
    (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                                        
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                                        
    '' dtReqAcceptedDate,                                             
    Scrl_GroupMemberDetailsTbl.dtAddedOn as dtAddedOn,'Scrl_RequestGroupJoin' strTableName,                                        
    '' intIsAccept,'' AS IsAccept,intIsAccept                                             
  FROM Scrl_GroupMemberDetailsTbl                                         
    LEFT OUTER JOIN scrl_RegistrationTbl ON Scrl_GroupMemberDetailsTbl.intAddedby=scrl_RegistrationTbl.intRegistrationId                                        
    LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_GroupMemberDetailsTbl.inGroupId=Scrl_UserGroupDetailTbl.inGroupId                            
  WHERE Scrl_GroupMemberDetailsTbl.strMemberName=@intRegistrationId AND Scrl_GroupMemberDetailsTbl.intIsAccept IS NULL                                       
  AND Scrl_UserGroupDetailTbl.strAccess='R'                      
                  
  --SA Notifications                   
                  
  UNION ALL                  
                 
    SELECT                                           
   ('') as Id,  ('') as intAddedBy,                
   ('') strMemberName,                
  ('') strInvitee ,                
   (select  '') as strGroupName,                
   '' StrRecommendation,                 
 ('') Name,                                        
    ('') vchrPhotoPath,                                                    
 ('') vchrUserName,                                        
    ('') as intUserTypeID,                                        
    ('') Designation,                                        
    '' dtReqAcceptedDate,                                             
    dateaddedon as dtAddedOn,'SCL_SA_Notifications' strTableName,                                        
    '' intIsAccept,'' AS IsAccept,('') as intIsAccept                                        
  FROM SCL_SA_Notifications                                         
                    
                 
                                        
                                       
                                       
  UNION ALL                                        
                                        
  SELECT                                       
    intSharedId Id,Scrl_GroupShareTbl.intAddedBy intRegistrationId,intGroupId intInvitedUserId,strInvitee,                                        
    Scrl_UserGroupDetailTbl.strGroupName,                                        
   '' StrRecommendation,                                        
   (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) Name,                                        
   (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) vchrPhotoPath,                                                    
   (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) vchrUserName,                                        
   (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_GroupShareTbl.intAddedBy) intUserTypeID,                                        
   (SELECT TOP 1 strDesignation +' at '+ strCompanyName from Scrl_UserExperienceTbl WHERE  intRegistrationId=Scrl_GroupShareTbl.intAddedBy and                                         
   (Scrl_UserExperienceTbl.bitAtPresent=CASE WHEN Scrl_UserExperienceTbl.bitAtPresent = 1 THEN 1 ELSE 0 END)                                        
   ORDER BY dtAddedOn desc,dtModifiedOn desc) Designation,                                        
   '' dtReqAcceptedDate,                                        
   Scrl_GroupShareTbl.dtAddedOn as dtAddedOn,strTableName,                                            
   '' IsAccepted,'' AS IsAccept,'' AS intIsAccept                                                      
                                                      
  FROM Scrl_GroupShareTbl                                              
    LEFT OUTER JOIN Scrl_UserGroupDetailTbl ON Scrl_UserGroupDetailTbl.inGroupId=Scrl_GroupShareTbl.intGroupId                                              
  WHERE 1=                                        
     (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInvitee,',') AS a                                        
     INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                                        
     THEN 1 ELSE  0 END matchId)                                        
UNION ALL                                        
     SELECT                                        
     intQAReplyLikeShareId Id,Scrl_UserPostQAReplyTbl.intAddedBy intRegistrationId,Scrl_UserPostQAReplyTbl.intPostQuestionId strInvitee,strInvitee,                                       
     (SUBSTRING ( Scrl_UserPostQAReplyTbl.strFileName ,0 , 50 )+CASE WHEN LEN(Scrl_UserPostQAReplyTbl.strFileName)>50 THEN '...' ELSE '' END)strGroupName,                                     
     strSharelink AS StrRecommendation,                                        
     (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) Name,                                        
     (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) vchrPhotoPath,                                                    
     (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) vchrUserName,                                        
     (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_UserPostQAReplyTbl.intAddedBy) intUserTypeID,                                        
     '' Designation,                                        
     '' dtReqAcceptedDate,                                             
    Scrl_UserPostQAReplyTbl.dtAddedOn as dtAddedOn,strTableName,                                            
     '' IsAccepted,                           
     '' AS IsAccept,'' AS intIsAccept                                                      
                                 
     FROM Scrl_UserPostQAReplyTbl                                              
     LEFT OUTER JOIN Scrl_QuestionAnsTbl ON Scrl_QuestionAnsTbl.intPostQuestionId=Scrl_UserPostQAReplyTbl.intPostQuestionId                                                 
     WHERE 1=                                        
     (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInvitee,',') AS a                                        
     INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                                        
     THEN 1 ELSE  0 END matchId) AND strRepLiShStatus='SH'                                        
                                        
     UNION ALL                                        
                                        
     SELECT                                        
     intBlogHeadingLikeId Id,Scrl_BlogHeadingLikeShareTbl.intAddedBy intRegistrationId,Scrl_BlogHeadingLikeShareTbl.intBlogId strInviteeShare,strInviteeShare,                                        
     (SUBSTRING ( Scrl_BlogHeadingLikeShareTbl.strBlogTitle ,0 , 50 )+CASE WHEN LEN(Scrl_BlogHeadingLikeShareTbl.strBlogTitle)>50 THEN '...' ELSE '' END)strGroupName,                                        
     strLink AS StrRecommendation,                          
     (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) Name,                                        
     (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) vchrPhotoPath,                                                    
     (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) vchrUserName,                                        
     (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_BlogHeadingLikeShareTbl.intAddedBy) intUserTypeID,                                        
     '' Designation,                                        
     '' dtReqAcceptedDate,                                             
     Scrl_BlogHeadingLikeShareTbl.dtAddedOn as dtAddedOn,strTableName,                                            
     '' IsAccepted,                                        
     '' AS IsAccept,'' AS intIsAccept                                                      
                                                      
     FROM Scrl_BlogHeadingLikeShareTbl                                              
     LEFT OUTER JOIN Scrl_NewBlogsTbl ON Scrl_NewBlogsTbl.intBlogId=Scrl_BlogHeadingLikeShareTbl.intBlogId                                             
     WHERE 1=                                        
     (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInviteeShare,',') AS a                                        
     INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                                        
     THEN 1 ELSE  0 END matchId) AND strRepLiShStatus='SH'                                        
                       
     UNION ALL                                        
                                        
     SELECT                                        
     intMicroLikeShareId Id,Scrl_MicrolTagLikeShareTbl.intAddedBy intRegistrationId,Scrl_MicrolTagLikeShareTbl.intMicroLikeShareId strInviteeShare,strInviteeShare,                                        
     (SUBSTRING ( Scrl_CaseTbl.strCaseTitle ,0 , 50 )+CASE WHEN LEN(Scrl_CaseTbl.strCaseTitle)>50 THEN '...' ELSE '' END)strGroupName,                                        
     strLink AS StrRecommendation,                                        
     (SELECT vchrFirstName +' '+ ISNULL(vchrLastName,'') NAME From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) Name,                                        
     (SELECT vchrPhotoPath From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) vchrPhotoPath,                                                    
     (SELECT vchrUserName From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) vchrUserName,                                        
     (SELECT intUserTypeID From scrl_RegistrationTbl WHERE intRegistrationId= Scrl_MicrolTagLikeShareTbl.intAddedBy) intUserTypeID,                                        
     '' Designation,                                        
     '' dtReqAcceptedDate,                                             
     Scrl_MicrolTagLikeShareTbl.dtAddedOn as dtAddedOn,'Scrl_MicrolTagLikeShareTbl' strTableName,                                            
     '' IsAccepted,                                        
     '' AS IsAccept,'' AS intIsAccept                                                      
                                                      
     FROM Scrl_MicrolTagLikeShareTbl                                              
     LEFT OUTER JOIN Scrl_CaseTbl ON Scrl_CaseTbl.intCaseId=Scrl_MicrolTagLikeShareTbl.intDocId                                            
     WHERE 1=                                        
     (SELECT CASE WHEN EXISTS (SELECT 1 FROM dbo.Split(strInviteeShare,',') AS a                                        
     INNER JOIN dbo.Split(@intRegistrationId,',') AS b ON a.Item = b.Item)                                        
     THEN 1 ELSE  0 END matchId) AND strRepLiShStatus='SH'                                         
                                             
                                              
 ) AS T                                          
 ) AS X WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand    and     dtAddedOn > @NotificationDate                               
 END                
                
                                        
END 
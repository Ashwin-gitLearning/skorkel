--Scoring Algo                          
                         
Alter  procedure sp_scrl_scoring_algo                              
@intRegistrationId  AS int =NULL,                              
@InstitutePoints  AS numeric(10,2)=0,                              
@MootsPoints As Numeric(10,2) =0,                              
@DebatesPoints As Numeric(10,2) =0,                              
@ADRPoints As Numeric(10,2) =0,                              
@PublicationsPoints as Numeric(10,2) = 0,                              
@PercentailePoints as numeric(10,2)=0,                             
@studentbody as numeric(10,2)=0,                          
@membership as numeric(10,2)=0,                          
@studentbodyPoints as numeric(10,2)=0,                            
@MemberhispPoints as numeric(10,2)=0 ,                          
@ArticlePoints as Numeric(10,2) = 0,                            
@BlogPoints as Numeric(10,2) = 0,                            
@JournelPoints as Numeric(10,2) =0,                          
@QuestionPoints as Numeric(10,2)  =0,                            
@AnswerQuestionPoints  as Numeric (10,2)=0,                           
@AllAnswerQuestionPoints as Numeric (10,2)=0,                          
@TotalLikesOnQAReply as numeric(10,2) =0,                            
@TotalLikesOnQAReplyPoints as Numeric(10,2) = 0,                            
@AskQuestionPoints as Numeric (10,2)=0,                            
@TotalLikesOnQAAnswer as Numeric(10,2) =0,                            
@LikesQAPoints as Numeric(10,2)=0,                            
@SummaryPoints as Numeric(10,2) = 0,                            
@CaseAnnotationPoints as Numeric(10,2) =0,                          
@Totalpoints as Numeric(10,2) =0,                          
@MootsPaticpantPoints as numeric(10,2)=0,                          
@DebateParticipantPoints as Numeric(10,2)=0,                          
@ADRParticipantPoints as Numeric(10,2)=0,                          
@AllWriteAnswerPoints as numeric (10,2) =0,                          
@AllBlogsPoints As Numeric(10,2)=0,                          
@AllMootsPoints as Numeric(10,2)=0,                          
@AllDebatesPoints as Numeric(10,2)=0,                          
@AllADRPoints as Numeric(10,2)=0,                          
@AllPublicationsPoints as Numeric(10,2)=0,                          
@AllSummaryPointsPoints as Numeric(10,2)=0,                          
@AllCaseAnnotationPoints as Numeric(10,2)=0,                          
@CategoryAPoints as Numeric(10,2)=0,                          
@CategoryBPoints as Numeric(10,2)=0,                          
@CategoryCPoints as Numeric(10,2)=0,                          
@CategoryDPoints as   Numeric(10,2)=0,                      
@AllMemberhispPoints as   Numeric(10,2)=0,                      
@AllstudentbodyPoints as Numeric(10,2)=0,            
@CasebasePoints as numeric(10,2)=0                     
                          
As                               
BEGIN                              
                              
-- Institute Points      
   declare @InstituteCount as  int  =0             
   SET @InstitutePoints =0   
   SET @InstituteCount=(select count(intInstituteId) from Scrl_UserEducationTbl where intRegistrationId= @intRegistrationId)  
   SET @InstitutePoints = (select Points from Scrl_InstituteNameTbl where intInstituteId=(select top 1 intInstituteId from Scrl_UserEducationTbl where intRegistrationId= @intRegistrationId order by intToYear desc))                              
    if (@InstitutePoints is null)                  
 begin     
 if(@InstituteCount>0)                
  set @InstitutePoints=(select points from scrl_config_params where CompetitionType='Institute')
 else  
   set @InstitutePoints=0              
 end                   
                  
-- Percentile points                              
    Set @PercentailePoints= (select top 1 (student_percentile) * 0.4 from Scrl_UserEducationTbl where intRegistrationId= @intRegistrationId order by intToYear desc)                              
  if (@PercentailePoints is null)                
 begin                   
 set @PercentailePoints=0                  
 end           
         
           
 -- Moots Points                             
  select intCompetitonId, strPosition                              
  into #Temp_Moots_Scrl_UserAchivmentTbl                              
  from dbo.Scrl_UserAchivmentTbl                              
  where intAddedBy= @intRegistrationId and strMilestone='Moot Court Competition' and intCompetitonId <> 0       
declare @intCompetitonId int, @strPosition Varchar(100)                            
while exists (select * from #Temp_Moots_Scrl_UserAchivmentTbl)                           
  begin                              
  select top 1 @intCompetitonId = intCompetitonId,@strPosition= strPosition               
  from #Temp_Moots_Scrl_UserAchivmentTbl                              
  order by intCompetitonId asc                              
     SET @MootsPaticpantPoints =(select Points from Scrl_CompetitionMasterTbl where CompetitionId=@intCompetitonId  and Competitiontype='Moot')                   
                    
  if(@strPosition ='Winner')                              
   Begin                             
    SET @MootsPoints = (@MootsPaticpantPoints) * 5                     
   end                              
                  
   if(@strPosition ='Runners-up' or @strPosition='Best Memo'  or @strPosition='Best Speaker' or @strPosition='Participant')                   
   Begin                              
    SET @MootsPoints =(@MootsPaticpantPoints)*3                     
   end                                
   SET @AllMootsPoints = @AllMootsPoints + @MootsPoints                          
  delete from #Temp_Moots_Scrl_UserAchivmentTbl  where intCompetitonId = @intCompetitonId  and strPosition=@strPosition                                
 end                              
                         
drop table #Temp_Moots_Scrl_UserAchivmentTbl                              
                              
-- Debates Points                              
  select intCompetitonId,strPosition                             
  into #Temp_Debate_Scrl_UserAchivmentTbl                              
  from dbo.Scrl_UserAchivmentTbl                              
  where intAddedBy= @intRegistrationId and strMilestone='Debate Competition'  and intCompetitonId <> 0                          
                          
while exists (select * from #Temp_Debate_Scrl_UserAchivmentTbl)                              
begin                              
   select top 1 @intCompetitonId = intCompetitonId, @strPosition=strPosition                             
   from #Temp_Debate_Scrl_UserAchivmentTbl                              
   order by intCompetitonId asc                              
                             
   SET @DebateParticipantPoints = (select Points from Scrl_CompetitionMasterTbl where CompetitionId=@intCompetitonId  and Competitiontype='Debate')                              
  if(@strPosition ='Winner')                              
   Begin                              
      SET @DebatesPoints =(@DebateParticipantPoints)*5                            
   end                              
   if(@strPosition ='Runners-up')                              
   Begin                              
      SET @DebatesPoints =(@DebateParticipantPoints)*3                            
   end                              
   if(@strPosition ='Break')                              
   Begin                              
      SET @DebatesPoints =(@DebateParticipantPoints)*2                                 
   end                             
 set @AllDebatesPoints =  @DebatesPoints+@AllDebatesPoints                          
delete from #Temp_Debate_Scrl_UserAchivmentTbl  where intCompetitonId  = @intCompetitonId    and strPosition=@strPosition                           
end                        
                       
drop table #Temp_Debate_Scrl_UserAchivmentTbl                              
                              
-- ADR Points                              
  select intCompetitonId,strPosition                              
  into #Temp_ADR_Scrl_UserAchivmentTbl                              
  from dbo.Scrl_UserAchivmentTbl                              
  where intAddedBy= @intRegistrationId and strMilestone='Alternate Dispute Resolution Competition'    and intCompetitonId <> 0               
                          
while exists (select * from #Temp_ADR_Scrl_UserAchivmentTbl)                              
  begin                              
    select top 1 @intCompetitonId = intCompetitonId,@strPosition=strPosition                              
    from #Temp_ADR_Scrl_UserAchivmentTbl                              
    order by intCompetitonId asc                              
    SET @ADRParticipantPoints = (select Points from Scrl_CompetitionMasterTbl where CompetitionId=@intCompetitonId  and Competitiontype='ADR')                              
     if(@strPosition ='Winner')                              
   Begin                              
   SET @ADRPoints =(@ADRParticipantPoints)*5                 
   end                              
  if(@strPosition ='Runners-up')                              
   Begin                              
   SET @ADRPoints =(@ADRParticipantPoints)*3                             
   end                              
 SET @AllADRPoints = @ADRPoints +@AllADRPoints                          
 delete from #Temp_ADR_Scrl_UserAchivmentTbl  where intCompetitonId  = @intCompetitonId  and strPosition=@strPosition                          
end                         
                        
drop table #Temp_ADR_Scrl_UserAchivmentTbl                              
                 
--Publications Points                              
  select intCompetitonId                               
 into #Temp_Publications_Scrl_UserAchivmentTbl                              
 from dbo.Scrl_UserAchivmentTbl                              
 where intAddedBy= @intRegistrationId and strMilestone='Publications'  and intCompetitonId <> 0                            
while exists (select * from #Temp_Publications_Scrl_UserAchivmentTbl)                              
begin                              
   select top 1 @intCompetitonId = intCompetitonId                              
   from #Temp_Publications_Scrl_UserAchivmentTbl                              
   order by intCompetitonId asc                              
   SET @PublicationsPoints = (select Points from Scrl_CompetitionMasterTbl where CompetitionId=@intCompetitonId and Competitiontype='Publications')                              
   SET @AllPublicationsPoints = @AllPublicationsPoints +@PublicationsPoints                          
delete from #Temp_Publications_Scrl_UserAchivmentTbl  where intCompetitonId  = @intCompetitonId                              
end                           
                     
drop table #Temp_Publications_Scrl_UserAchivmentTbl                              
                       
--MembershipPoints                       
 select intCompetitonId                               
 into #Temp_Membership_Scrl_UserAchivmentTbl                              
 from dbo.Scrl_UserAchivmentTbl                              
 where intAddedBy= @intRegistrationId and strMilestone='Committee Membership'   and intCompetitonId <> 0                    
while exists (select * from #Temp_Membership_Scrl_UserAchivmentTbl)                              
                      
begin                              
   select top 1 @intCompetitonId = intCompetitonId                              
   from #Temp_Membership_Scrl_UserAchivmentTbl                        
   order by intCompetitonId asc                           
   Set @MemberhispPoints = (select Points from Scrl_CompetitionMasterTbl where CompetitionId=@intCompetitonId and Competitiontype='Committee Membership')                          
   SET @AllMemberhispPoints =@AllMemberhispPoints +@MemberhispPoints                          
delete from #Temp_Membership_Scrl_UserAchivmentTbl  where intCompetitonId  = @intCompetitonId                              
end                         
                            
drop table #Temp_Membership_Scrl_UserAchivmentTbl                              
                      
--StudentBodayPoints                          
                     
 select intCompetitonId                               
 into #Temp_Student_body_Scrl_UserAchivmentTbl                              
 from dbo.Scrl_UserAchivmentTbl                              
 where intAddedBy=@intRegistrationId and strMilestone='Student Body'    and intCompetitonId <> 0                
                   
                  
while exists (select * from #Temp_Student_body_Scrl_UserAchivmentTbl)                              
                      
begin                              
   select top 1 @intCompetitonId = intCompetitonId                              
   from #Temp_Student_body_Scrl_UserAchivmentTbl                              
   order by intCompetitonId asc                           
   Set @studentbodyPoints = (select Points from Scrl_CompetitionMasterTbl where CompetitionId=@intCompetitonId and Competitiontype='Student Body')                          
                    
   SET @AllstudentbodyPoints =@AllstudentbodyPoints +@studentbodyPoints                          
delete from #Temp_Student_body_Scrl_UserAchivmentTbl  where intCompetitonId  = @intCompetitonId                              
end                         
                      
drop table #Temp_Student_body_Scrl_UserAchivmentTbl              
           
-- Ask/WriteQuestion                           
declare @intPostQuestionId as int  , @likescount as int  =0                       
 select intPostQuestionId                               
 into #Temp_Scrl_QuestionAnsTbl                             
 from dbo.Scrl_QuestionAnsTbl                              
 where intAddedBy= @intRegistrationId                          
                          
while exists (select * from #Temp_Scrl_QuestionAnsTbl)                              
begin                              
   select top 1 @intPostQuestionId = intPostQuestionId                              
   from #Temp_Scrl_QuestionAnsTbl                              
   order by intPostQuestionId asc                          
                                 
  SET @TotalLikesOnQAReply = (SELECT COUNT(*) FROM Scrl_UserPostQAReplyTbl WHERE strRepLiShStatus='LI'  and intPostQuestionId=@intPostQuestionId)                           
  if (@TotalLikesOnQAReply <20)                        
  Begin                        
   set  @TotalLikesOnQAReplyPoints =(select points from scrl_config_params where CompetitionLevel='basepointAskQuestion')                    
                            
   end                            
 else                         
  begin                              
   set @LikesCount=(select FLOOR( @TotalLikesOnQAReply/20 ))                           
   If (@LikesCount >0)                             
  begin                            
    set  @TotalLikesOnQAReplyPoints = @LikesCount * (select points from scrl_config_params where CompetitionLevel='basepointAskQuestion') *3                            
      end                            
end                     
                  
SET @AllWriteAnswerPoints = @AllWriteAnswerPoints+@TotalLikesOnQAReplyPoints                   
                  
                  
delete from #Temp_Scrl_QuestionAnsTbl  where intPostQuestionId = @intPostQuestionId                         
                            
end                       
                          
drop table #Temp_Scrl_QuestionAnsTbl                              
                          
            -- Answer Question  Likes                          
declare @intAnswerId as int                        
 select distinct intAnswerId                              
 into #Temp_Scrl_UserPostQAAnsReplyTbl                             
 from dbo.Scrl_UserPostQAAnsReplyTbl                              
 where intAnsAddedBy= @intRegistrationId                        
                   
                          
while exists (select * from #Temp_Scrl_UserPostQAAnsReplyTbl)                              
begin                              
   select top 1 @intAnswerId = intAnswerId                            
   from #Temp_Scrl_UserPostQAAnsReplyTbl                              
   order by intAnswerId asc                          
                                 
 SET @TotalLikesOnQAAnswer = (SELECT COUNT(*) FROM Scrl_UserPostQAAnsReplyTbl WHERE strAnsLiStatus='LI' AND intAnswerId=@intAnswerId)                           
                   
 if (@TotalLikesOnQAAnswer <20)                        
 begin                        
  SET  @AnswerQuestionPoints = (select points from scrl_config_params where CompetitionLevel='basepointReplyQuestion')                  
  end                         
                         
 else                         
 begin                        
   Declare @Answerslikescount as int  =0                         
   set @Answerslikescount=(select FLOOR( @TotalLikesOnQAAnswer/20 ))                           
   If (@Answerslikescount >0)                             
  begin                            
    set  @AnswerQuestionPoints = @Answerslikescount *(select points from scrl_config_params where CompetitionLevel='basepointReplyQuestion') *3                    
 end                            
  end           
   SET @AllAnswerQuestionPoints = @AllAnswerQuestionPoints+@AnswerQuestionPoints                          
  delete from #Temp_Scrl_UserPostQAAnsReplyTbl  where intAnswerId = @intAnswerId                     
 end                              
drop table #Temp_Scrl_UserPostQAAnsReplyTbl                          
                  
---- WriteBlog                            
declare @intBlogId as int, @TotalLikesOnBlog as int                          
 select intBlogId                               
 into #Temp_Scrl_NewBlogsTbl                            
 from dbo.Scrl_NewBlogsTbl                       
 where intAddedBy= @intRegistrationId                         
 and dbo.TextLength(strBlogTitle)> 200                        
                          
while exists (select * from #Temp_Scrl_NewBlogsTbl)                              
begin                              
   select top 1 @intBlogId = intBlogId                              
   from #Temp_Scrl_NewBlogsTbl                              
   order by intBlogId asc                          
                                 
 SET @TotalLikesOnBlog = (SELECT COUNT(*) FROM Scrl_BlogHeadingLikeShareTbl WHERE strRepLiShStatus='LI' AND intBlogId=@intBlogId)                           
  Declare @Bloglikescount as int                         
  If( @TotalLikesOnBlog <20)                        
  begin                        
     set  @BlogPoints = (select points from scrl_config_params where CompetitionLevel='basepointblog')                        
  end                         
  Else                        
  Begin                        
   set @Bloglikescount=(select FLOOR( @TotalLikesOnBlog/20 ))                           
   if (@Bloglikescount >0)                             
    begin                            
    set  @BlogPoints = @Bloglikescount * (select points from scrl_config_params where CompetitionLevel='basepointblog') *3                            
   end                            
                         
 end                        
  Set @AllBlogsPoints = @BlogPoints+@AllBlogsPoints                         
  delete from #Temp_Scrl_NewBlogsTbl  where intBlogId = @intBlogId                             
 end                              
 drop table #Temp_Scrl_NewBlogsTbl                        
                          
-- Journel Points                            
declare @articleid as int,      
   @AllJournelPoints as numeric(10,2)=0,      
    @journel_id as int      
  select id                        
  into #Temp_ArticleTbl                              
  from Articles                              
  where USERid= @intRegistrationId       
  while exists (select * from #Temp_ArticleTbl)                           
  begin        
    select top 1 @articleid = id from  #Temp_ArticleTbl  order by id                 
          Set @journel_id =(select journal_id from JournalArticles where article_id = @articleid)      
     SET @JournelPoints =  (select count(*) from Journals where status=1 and id in(@journel_id)) * (select  points from  scrl_config_params where CompetitionType='Journel' )        
         SET @AllJournelPoints = @JournelPoints + @AllJournelPoints       
   Delete from #Temp_ArticleTbl where id=@articleid      
   end       
   drop table #Temp_ArticleTbl                  
                        
-- Submit Article                            
Set @ArticlePoints =((Select count (*) from articles  where userId=@intRegistrationId and ArticleLenght>200) * (select  points from  scrl_config_params where CompetitionType='Article' ) )                          
                          
-- Case Annotations Points                          
 select sc.intCaseId as intCaseId ,sc.intmarkid,ct.caselength as caselength ,sc.inttagtype  as   inttagtype                       
 into #Temp_Scrl_crl_ResearchDoc_MarkAsTbl                          
 from Scrl_ResearchDoc_MarkAsTbl sc, Scrl_CaseTbl ct                          
 where ct.caselength > 2000 and sc.intaddedby=@intRegistrationId                 
 and sc.intCaseId=ct.intCaseId                       
                
 declare @caselength as int, @intcaseid as int, @inttagtype as int,  @intmarkid  as int                    
                          
 while exists(select intCaseId,caselength,inttagtype from #Temp_Scrl_crl_ResearchDoc_MarkAsTbl)                          
 Begin                          
     select top 1 @caselength = caselength,@intcaseid = intcaseid, @inttagtype=inttagtype, @intmarkid=intmarkid  from #Temp_Scrl_crl_ResearchDoc_MarkAsTbl  order by intCaseId asc                          
  if (@caselength >2000 and @caselength<12000)                          
  begin                           
 if (@inttagtype =5)              
 begin             
 SET @CasebasePoints =    (select points from scrl_config_params where CompetitionType = 'Case Annotations' and competitionlevel='Ratio Decidendi')                
 SET @CaseAnnotationPoints= @CasebasePoints            
 end             
            
  Else              
  begin            
   SET @CasebasePoints =    (select points from scrl_config_params where CompetitionType = 'Case Annotations' and competitionlevel='All')                
   SET @CaseAnnotationPoints= @CasebasePoints            
 end             
 End                   
                           
  if (@caselength >12000  and @caselength<24000)                          
  begin                           
   if (@inttagtype =5)             
   begin               
   SET @CasebasePoints =    (select points from scrl_config_params where CompetitionType = 'Case Annotations' and competitionlevel='Ratio Decidendi')                
   SET @CaseAnnotationPoints= @CasebasePoints*1.5                      
  end             
   else              
   begin               
     SET @CasebasePoints =    (select points from scrl_config_params where CompetitionType = 'Case Annotations' and competitionlevel='All')                
     SET @CaseAnnotationPoints= @CasebasePoints*1.5                          
  end            
            
  End                          
                          
  if (@caselength >24000 )                          
  begin                           
  if (@inttagtype =5)                
  Begin            
   SET @CasebasePoints =    (select points from scrl_config_params where CompetitionType = 'Case Annotations' and competitionlevel='Ratio Decidendi')                
 SET @CaseAnnotationPoints= @CasebasePoints*2                   
 end             
  Else               
  Begin            
      SET @CasebasePoints =    (select points from scrl_config_params where CompetitionType = 'Case Annotations' and competitionlevel='All')                
      SET @CaseAnnotationPoints= @CasebasePoints*2                          
 end            
 End                          
                          
  declare @Annotationlikescount as int,  @TotalAnnotationsLikes as int                          
  SET @TotalAnnotationsLikes = (SELECT COUNT(*) FROM scrl_CaseMicroTagLikes WHERE id in (select intCaseId from Scrl_ResearchDoc_MarkAsTbl where intAddedby=@intRegistrationId))                          
  set @Annotationlikescount=(select FLOOR( @TotalAnnotationsLikes/20 ))                           
  if (@Annotationlikescount >0)                    
  begin                 
   set  @CaseAnnotationPoints = @Annotationlikescount * @CaseAnnotationPoints *3                            
  end                            
   SET @AllCaseAnnotationPoints = @AllCaseAnnotationPoints+@CaseAnnotationPoints                          
   delete from  #Temp_Scrl_crl_ResearchDoc_MarkAsTbl  where intCaseId=@intCaseId    and intmarkid = @intmarkid                        
  end                             
  drop table   #Temp_Scrl_crl_ResearchDoc_MarkAsTbl                            
                          
 -- Summary Head Note Points                            
select  CT.caselength as contentlength,SM.intContentId as intContentId ,SM.strSummaryText   as    strSummaryText                      
into #Temp_TScrl_SummaryMaterbl  from dbo.Scrl_SummaryMaterTbl SM, Scrl_CaseTbl CT  where SM.intAddedBy= @intRegistrationId   and ct.intCaseId=SM.intContentId                         
declare @contentlength as int,@intContentId as int, @summarylength as int                            
                          
while exists (select * from #Temp_TScrl_SummaryMaterbl)                            
 Begin                            
     select top 1 @contentlength = contentlength,@intContentId =  intContentId, @summarylength= (Select dbo.TextLength(strSummaryText))                      
     from #Temp_TScrl_SummaryMaterbl  order by contentlength asc                            
                                 
     If (@contentlength > 2000 and @contentlength <12000)  and (@summarylength>100)                            
     Begin                             
   Set  @SummaryPoints =(select points from scrl_config_params where CompetitionLevel='SummaryBasePoints')                            
     end                            
     if (@contentlength > 12000 and @contentlength <24000) and (@summarylength>100)                            
     Begin                             
        Set  @SummaryPoints =(select points from scrl_config_params where CompetitionLevel='SummaryBasePoints')*1.5                            
     end                            
      if (@contentlength > 24000 ) and (@summarylength>100)                            
     Begin                             
        Set  @SummaryPoints =(select points from scrl_config_params where CompetitionLevel='SummaryBasePoints')*2                            
     end                            
  declare @Summarylikescount as int,  @TotalSummaryLikes as int                          
  SET @TotalSummaryLikes = (SELECT COUNT(*) FROM Scrl_CaseSummaryLikes WHERE Summary_id in(select intSummaryId from Scrl_SummaryMaterTbl where intAddedby=@intRegistrationId))                          
  set @Summarylikescount=(select FLOOR( @TotalSummaryLikes/20 ))                           
  if (@Summarylikescount >0)                             
 begin                            
    set  @SummaryPoints =  @Summarylikescount *3 *@SummaryPoints                          
   end                            
                       
  SET @AllSummaryPointsPoints = @AllSummaryPointsPoints+@SummaryPoints                    
                          
   delete from  #Temp_TScrl_SummaryMaterbl  where   intContentId=@intContentId                       
   end                            
   drop table   #Temp_TScrl_SummaryMaterbl                            
         
-- Sum of Points based upon category                     
                        
SET @CategoryAPoints=@InstitutePoints                          
SET @CategoryBPoints=@PercentailePoints                          
SET @CategoryCPoints=@AllMootsPoints+ @AllDebatesPoints+@AllADRPoints+@AllPublicationsPoints+@AllMemberhispPoints+@AllstudentbodyPoints                          
SET @CategoryDPoints=@ArticlePoints+@AllJournelPoints+@AllBlogsPoints+@AllWriteAnswerPoints+@AllAnswerQuestionPoints+@AllSummaryPointsPoints+@AllCaseAnnotationPoints                          
                         
-- Insert Score All Category Score into Score Table                       
if (select count(*) from   Score where userid =@intRegistrationId)>0                      
 begin                      
 select @CategoryAPoints, @CategoryBPoints, @CategoryCPoints                  
  update Score set CategoryA = @CategoryAPoints,CategoryB=@CategoryBPoints,CategoryC=@CategoryCPoints,CategoryD=@CategoryDPoints,Scoredate=GETDATE()                      
  where UserId=@intRegistrationId       
 end                       
If (select count(*) from   Score where userid =@intRegistrationId) =0                  
                      
 begin                      
  Insert into Score (CategoryA,CategoryB,CategoryC,CategoryD,userid,Scoredate)                          
  values (@CategoryAPoints,@CategoryBPoints,@CategoryCPoints,@CategoryDPoints,@intRegistrationId,Getdate())                          
 end                  
                       
END 
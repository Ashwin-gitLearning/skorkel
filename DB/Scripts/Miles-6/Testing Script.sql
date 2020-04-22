--update  Scrl_InstituteNameTbl set points = 10.1 where intinstituteid = 22
--select * from Scrl_UserEducationTbl where intInstituteId =17
--select * from Scrl_InstituteNameTbl where intInstituteId = 9
--select * From Scrl_UserEducationTbl a, Scrl_UserEducationTbl b where a.intInstituteId = b.intInstituteId
--declare @InstitutePoints numeric(10,2)
--SET @InstitutePoints = (select Points from Scrl_InstituteNameTbl where intInstituteId=(select top 1 intInstituteId from Scrl_UserEducationTbl where intRegistrationId= 100785 order by intToYear desc))          
--select @InstitutePoints
--Set @PercentailePoints= (select top 1 (student_percentile) * 0.4 from Scrl_UserEducationTbl where intRegistrationId= 100785 order by intToYear desc)          

--------------------------------------------------------------------------------------------------------------
--select intCompetitonId, strPosition          
--  into #Temp_Moots_Scrl_UserAchivmentTbl          
--  from dbo.Scrl_UserAchivmentTbl          
--  where intAddedBy= 100785 and strMilestone='Moot Court Competition' 

   
-- declare @MootsPaticpantPoints as numeric (10,2),@MootsPoints as numeric (10,2)=0,@strPosition  as varchar(100),@intCompetitonId int,@AllMootsPoints as numeric (10,2)=0
--  while exists (select * from #Temp_Moots_Scrl_UserAchivmentTbl)       
--  begin          
--  select top 1 @intCompetitonId = intCompetitonId,@strPosition= strPosition         
--  from #Temp_Moots_Scrl_UserAchivmentTbl          
--  order by intCompetitonId asc          
--     SET @MootsPaticpantPoints =(select points from Scrl_CompetitionMasterTbl where CompetitionId=101  and Competitiontype='Moot')          
--  if(@strPosition ='Winner')          
--   Begin          
--    SET @MootsPoints = (@MootsPaticpantPoints) * 5 
--   end          
--  else if(@strPosition ='Runners-up' or @strPosition='Best Memo'  or @strPosition='Best Speaker')          
--   Begin          
--    SET @MootsPoints =(@MootsPaticpantPoints)*3 
--   end  
--   select        @MootsPoints 
--   SET @AllMootsPoints = @AllMootsPoints + @MootsPoints 
--  delete from #Temp_Moots_Scrl_UserAchivmentTbl  where intCompetitonId = @intCompetitonId and strPosition=@strPosition    
-- end          
--drop table #Temp_Moots_Scrl_UserAchivmentTbl          
--select   @AllMootsPoints
-----------------------------------------------------------------------------------------------------------------
-- update Scrl_UserAchivmentTbl set intAddedBy=100785, intCompetitonId=180 where intAchivmentId=30
-- select * from Scrl_CompetitionMasterTbl
--Alter table Scrl_CompetitionMasterTbl Alter column points numeric(10,2)
--select * from Scrl_UserAchivmentTbl 

-- declare @DebateParticipantPoints as numeric (10,2),@DebatesPoints as numeric (10,2)=0,@strPosition  as varchar(100),@intCompetitonId int,@AllDebatesPoints as numeric (10,2)=0
-- select intCompetitonId, strPosition          
--  into #Temp_Debate_Scrl_UserAchivmentTbl          
--  from dbo.Scrl_UserAchivmentTbl          
--  where intAddedBy= 100785 and strMilestone='Debate Competition'          
--  select * from  #Temp_Debate_Scrl_UserAchivmentTbl   
--while exists (select * from #Temp_Debate_Scrl_UserAchivmentTbl)          
--begin          
--   select top 1 @intCompetitonId = intCompetitonId , @strPosition=strPosition         
--   from #Temp_Debate_Scrl_UserAchivmentTbl          
--   order by intCompetitonId asc          
         
--   SET @DebateParticipantPoints = (select Points from Scrl_CompetitionMasterTbl where CompetitionId=@intCompetitonId  and Competitiontype='Debate')          
   
  
--  if(@strPosition ='Winner')          
--   Begin          
--      SET @DebatesPoints =(@DebateParticipantPoints)*5        
--   end          
--     else if(@strPosition ='Runners-up')          
--   Begin          
--      SET @DebatesPoints =(@DebateParticipantPoints)*3        
--   end          
--       else if(@strPosition ='Break')          
--   Begin          
--      SET @DebatesPoints =(@DebateParticipantPoints)*2             
--   end         
-- set @AllDebatesPoints =  @DebatesPoints+@AllDebatesPoints      
--delete from #Temp_Debate_Scrl_UserAchivmentTbl  where intCompetitonId  = @intCompetitonId          
--end          
--drop table #Temp_Debate_Scrl_UserAchivmentTbl

--select @AllDebatesPoints

------------------------------------------------------------------------------------------------------------
--select * from  Scrl_CompetitionMasterTbl 
--select * from Scrl_UserAchivmentTbl 
--182,188
--select * from Scrl_UserAchivmentTbl where strMilestone='ADR Competiton' 

--insert into Scrl_UserAchivmentTbl (intAddedBy,strCompititionName,strPosition,strDescription,strMilestone,intCompetitonId)
--values (100785,'ADR -COMP','Winner','Desc','ADR Competiton',188)
--declare @ADRParticipantPoints as numeric (10,2),@ADRPoints as numeric (10,2)=0,@strPosition  as varchar(100),@intCompetitonId int,@AllADRPoints as numeric (10,2)=0

--  select intCompetitonId, strPosition          
--  into #Temp_ADR_Scrl_UserAchivmentTbl           
--  from dbo.Scrl_UserAchivmentTbl          
--  where intAddedBy= 100785 and strMilestone='ADR Competiton'          
--while exists (select * from #Temp_ADR_Scrl_UserAchivmentTbl)          
--  begin          
--    select top 1 @intCompetitonId = intCompetitonId,@strPosition=strPosition          
--    from #Temp_ADR_Scrl_UserAchivmentTbl          
--    order by intCompetitonId asc 
--	SET @ADRParticipantPoints = (select Points from Scrl_CompetitionMasterTbl where CompetitionId=@intCompetitonId  and Competitiontype='ADR')       
--	if(@strPosition ='Winner')          
--  Begin          
--     SET @ADRPoints =(@ADRParticipantPoints)*5     
--  end          
--  else if(@strPosition ='Runners-up')          
--  Begin          
--     SET @ADRPoints =(@ADRParticipantPoints)*3         
--  end          
-- SET @AllADRPoints = @ADRPoints +@AllADRPoints      
--delete from #Temp_ADR_Scrl_UserAchivmentTbl  where intCompetitonId  = @intCompetitonId  and @strPosition=strPosition        
--end          
--drop table #Temp_ADR_Scrl_UserAchivmentTbl
--select @ADRPoints

--------------------------------------------------------------------------------------------------------
--select * from Scrl_UserAchivmentTbl
--select * from Scrl_CompetitionMasterTbl
-- insert into Scrl_CompetitionMasterTbl(CompName,Competitiontype) values('Pub-2', 'Publications')
--195,196
--188
--195
--196
--delete from Scrl_UserAchivmentTbl where intCompetitonId =188
--update Scrl_CompetitionMasterTbl set Tier=2, Points=50 where CompetitionId=196
--insert into Scrl_UserAchivmentTbl

--insert into Scrl_UserAchivmentTbl (intAddedBy,strCompititionName,strPosition,strDescription,strMilestone,intCompetitonId)
--values (100785,'Publications- 1','Winner','Desc','Publications',196)


--delete from  Scrl_UserAchivmentTbl where IntCommitteeMembershipId=188 and strMilestone='Publications'  
--declare @PublicationsPoints as numeric (10,2),@ADRPoints as numeric (10,2)=0,@strPosition  as varchar(100),@intCompetitonId int,@AllPublicationsPoints as numeric (10,2)=0
-- select intCompetitonId          
-- into #Temp_Publications_Scrl_UserAchivmentTbl          
-- from dbo.Scrl_UserAchivmentTbl          
-- where intAddedBy= 100785 and strMilestone='Publications'    
--while exists (select * from #Temp_Publications_Scrl_UserAchivmentTbl)          
--begin          
--   select top 1 @intCompetitonId = intCompetitonId          
--   from #Temp_Publications_Scrl_UserAchivmentTbl          
--   order by intCompetitonId asc          
--   SET @PublicationsPoints = (select Points from Scrl_CompetitionMasterTbl where CompetitionId=@intCompetitonId and Competitiontype='Publications')   
         
--   SET @AllPublicationsPoints =@AllPublicationsPoints +@PublicationsPoints      
--delete from #Temp_Publications_Scrl_UserAchivmentTbl  where intCompetitonId  = @intCompetitonId          
--end          
--drop table #Temp_Publications_Scrl_UserAchivmentTbl   
--select @AllPublicationsPoints
---------------------------------------------------------------------------------------------------------

--MembershipPoints 
--select * from Scrl_UserAchivmentTbl
--select * from Scrl_CompetitionMasterTbl

--insert into Scrl_CompetitionMasterTbl(CompName,Competitiontype,points)
--values('Membership','Committee Membership',10)
--insert into Scrl_CompetitionMasterTbl(CompName,Competitiontype,points)
--values('President','Committee Membership',5)

--197
--198

--insert into Scrl_UserAchivmentTbl (intAddedBy,strCompititionName,strPosition,strDescription,strMilestone,intCompetitonId)
--values (100785,'Membership',' ','Desc','Committee Membership',197)
--insert into Scrl_UserAchivmentTbl (intAddedBy,strCompititionName,strPosition,strDescription,strMilestone,intCompetitonId)
--values (100785,'President',' ','Desc','Committee Membership',198)
--declare @MemberhispPoints as numeric (10,2),@ADRPoints as numeric (10,2)=0,@strPosition  as varchar(100),@intCompetitonId int,@AllMemberhispPoints as numeric (10,2)=0
-- select intCompetitonId           
-- into #Temp_Membership_Scrl_UserAchivmentTbl          
-- from dbo.Scrl_UserAchivmentTbl          
-- where intAddedBy= 100785 and strMilestone='Committee Membership'   
--while exists (select * from #Temp_Membership_Scrl_UserAchivmentTbl)          
  
--begin          
--   select top 1 @intCompetitonId = intCompetitonId          
--   from #Temp_Membership_Scrl_UserAchivmentTbl          
--   order by intCompetitonId asc       
--   Set @MemberhispPoints = (select Points from Scrl_CompetitionMasterTbl where CompetitionId=@intCompetitonId and Competitiontype='Committee Membership')      
--   SET @AllMemberhispPoints =@AllMemberhispPoints +@MemberhispPoints      
--delete from #Temp_Membership_Scrl_UserAchivmentTbl  where intCompetitonId  = @intCompetitonId          
--end     
       
--drop table #Temp_Membership_Scrl_UserAchivmentTbl    
--select @AllMemberhispPoints



------------------------------------------------------------------------------------------

--insert into Scrl_CompetitionMasterTbl(CompName,Competitiontype,points)
--values('President','Student Body',50)
--insert into Scrl_CompetitionMasterTbl(CompName,Competitiontype,points)
--values('Vice President','Sudent Body',50)
--select * from Scrl_CompetitionMasterTbl

--insert into Scrl_UserAchivmentTbl (intAddedBy,strCompititionName,strPosition,strDescription,strMilestone,intCompetitonId)
--values (100785,'President',' ','Desc','Student body',199)
--insert into Scrl_UserAchivmentTbl (intAddedBy,strCompititionName,strPosition,strDescription,strMilestone,intCompetitonId)
--values (100785,'Vice President',' ','Desc','Student body',200)

--declare @AllstudentbodyPoints as numeric (10,2)=0,@ADRPoints as numeric (10,2)=0,@strPosition  as varchar(100),@intCompetitonId int,@studentbodyPoints as numeric (10,2)=0

   
-- select intCompetitonId           
-- into #Temp_Student_body_Scrl_UserAchivmentTbl          
-- from dbo.Scrl_UserAchivmentTbl          
-- where intAddedBy= 100785 and strMilestone='Student body'   
--while exists (select intCompetitonId from #Temp_Student_body_Scrl_UserAchivmentTbl)          
  
--begin          
--   select top 1 @intCompetitonId = intCompetitonId          
--   from #Temp_Student_body_Scrl_UserAchivmentTbl          
--   order by intCompetitonId asc       
--   Set @studentbodyPoints = (select Points from Scrl_CompetitionMasterTbl where CompetitionId=@intCompetitonId and Competitiontype='Student body')      
--   SET @AllstudentbodyPoints =@AllstudentbodyPoints +@studentbodyPoints      
--delete from #Temp_Student_body_Scrl_UserAchivmentTbl  where intCompetitonId  = @intCompetitonId          
--end     
       
--drop table #Temp_Student_body_Scrl_UserAchivmentTbl 
--select @AllstudentbodyPoints
----------------------------------------------------------------------------------------------------
  -- Journel Points      
--  declare @JournelPoints as numeric (10,2) 
--Set @JournelPoints=(select count (*) from JournalArticles where active =1  and article_id in(select id from Articles where userid=100785)) * (select  points from  scrl_config_params where CompetitionType='Journel' )   
--select @JournelPoints
--update articles set userid = 100785 where userid=100799
--select * from articles where userid = 100785
--select * from JournalArticles 
--select * from Articles
--select * from scrl_config_params
--insert into scrl_config_params values ( 'Journel','', 40)
-- update scrl_config_params set points = 200 where competitiontype ='Journel'
-->>Alter Table JournalArticles add Column Active numeric
-------------------------------------------------------------------------------------------------------------             
-- Submit Article        
--declare @ArticlePoints as numeric (10,2)  
--Set @ArticlePoints =((Select count (*) from articles where userId=100785 and ArticleLenght>200) * (select  points from  scrl_config_params where CompetitionType='Article' ) )      
-- select @ArticlePoints    
---->> Alter table   articles add ArticleLenght Numeric
----update   articles set ArticleLenght = 201 where userId = 100785
--select * From scrl_config_params

-------------------------------------------------------------------------------------------------------------
 --select * From    Scrl_QuestionAnsTbl 
 --where intaddedby = 100785
--Question Id
--50105
--50106
--50107

--update Scrl_QuestionAnsTbl set intaddedby=100785 where intpostquestionid in (50105,50106,50107)
--select * From Scrl_UserPostQAReplyTbl where intPostQuestionId in (50105,50106,50107) and strRepLiShStatus='LI'
--update Scrl_UserPostQAReplyTbl set strRepLiShStatus='LI' where intPostQuestionId in (50105,50106,50107)
-- select * from scrl_config_params
--insert into scrl_config_params values('Ask Question', 'basepointAskQuestion', 2.5)
-->>>Alter table scrl_config_params alter column Points numeric (10,2)

--select * from  #Temp_Scrl_QuestionAnsTbl
 -- drop table #Temp_Scrl_QuestionAnsTbl
 --Questions ids
--50105
--50106
--50107

-- declare @TotalLikesOnQAReplyPoints as numeric (10,2)=0,@AllWriteAnswerPoints as numeric (10,2)=0,@TotalLikesOnQAReply as Numeric=0,@intPostQuestionId as int , @likescount as int     
 
-- select intPostQuestionId           
-- into #Temp_Scrl_QuestionAnsTbl         
-- from dbo.Scrl_QuestionAnsTbl          
-- where intAddedBy= 100785 
-- while exists (select * from #Temp_Scrl_QuestionAnsTbl)          
--begin          
--   select top 1 @intPostQuestionId = intPostQuestionId          
--   from #Temp_Scrl_QuestionAnsTbl          
--   order by intPostQuestionId asc      
             
--  SET @TotalLikesOnQAReply = (SELECT COUNT(*) FROM Scrl_UserPostQAReplyTbl WHERE strRepLiShStatus='LI'  and intPostQuestionId=@intPostQuestionId)       
--  if (@TotalLikesOnQAReply <20)    
--  Begin    
--   set  @TotalLikesOnQAReplyPoints =(select points from scrl_config_params where CompetitionLevel='basepointAskQuestion')        
-- end        
-- else     
--  begin         
--  set @LikesCount=(select FLOOR( @TotalLikesOnQAReply/20 ))       
--  If (@LikesCount >0)         
--    begin        
--   set  @TotalLikesOnQAReplyPoints = @LikesCount * (select points from scrl_config_params where CompetitionLevel='basepointAskQuestion') *3        
--   end        
      
--end  
--select @AllWriteAnswerPoints
--SET @AllWriteAnswerPoints = @AllWriteAnswerPoints+@TotalLikesOnQAReplyPoints      
--delete from #Temp_Scrl_QuestionAnsTbl  where intPostQuestionId = @intPostQuestionId     
--end          
--drop table #Temp_Scrl_QuestionAnsTbl          

--select @AllWriteAnswerPoints

______________________________________________________________________________________________________________
-- Case Annotation

--select intCaseId,caselength,inttagtype      
-- into #Temp_Scrl_crl_ResearchDoc_MarkAsTbl      
-- from Scrl_ResearchDoc_MarkAsTbl      
-- where caselength > 2000 and intaddedby=100785 
 

 select intCaseId,caselength,inttagtype from #Temp_Scrl_crl_ResearchDoc_MarkAsTbl
 declare @CaseAnnotationPoints as numeric (10,2) , @AllCaseAnnotationPoints numeric (10,2)
 
      
 declare @caselength as int, @intcaseid as int, @inttagtype as int      
      
 while exists(select intCaseId,caselength,inttagtype from #Temp_Scrl_crl_ResearchDoc_MarkAsTbl)      
 Begin      
     select top 1 @caselength = caselength,@intcaseid = intcaseid, @inttagtype=inttagtype  from #Temp_Scrl_crl_ResearchDoc_MarkAsTbl  order by intCaseId asc      
  if (@caselength >2000 and @caselength<12000)      
  begin       
           
  SET @CaseAnnotationPoints= ((select points from scrl_config_params where CompetitionType = 'Case Annotations' and competitionlevel='All'))      
  if (@inttagtype =5)      
  SET @CaseAnnotationPoints= ((select points from scrl_config_params where CompetitionType = 'Case Annotations' and competitionlevel='Ratio Decidendi'))      
  End      
  if (@caselength >12000  and @caselength<24000)      
  begin       
  SET @CaseAnnotationPoints= ((select points from scrl_config_params where CompetitionType = 'Case Annotations' and competitionlevel='All')*1.5)      
  if (@inttagtype =5)      
  SET @CaseAnnotationPoints= ((select points from scrl_config_params where CompetitionType = 'Case Annotations' and competitionlevel='Ratio Decidendi')*1.5)      
  End      
      
  if (@caselength >24000 )      
  begin       
  SET @CaseAnnotationPoints= ((select points from scrl_config_params where CompetitionType = 'Case Annotations' and competitionlevel='All')*2)      
  if (@inttagtype =5)      
  SET @CaseAnnotationPoints= ((select points from scrl_config_params where CompetitionType = 'Case Annotations' and competitionlevel='Ratio Decidendi')*2)      
  End      
      
  declare @Annotationlikescount as int,  @TotalAnnotationsLikes as int      
  SET @TotalAnnotationsLikes = (SELECT COUNT(*) FROM scrl_CaseMicroTagLikes WHERE id=(select intCaseId from #Temp_Scrl_crl_ResearchDoc_MarkAsTbl where intAddedby=100785))      
  set @Annotationlikescount=(select FLOOR( @TotalAnnotationsLikes/20 ))       
  if (@Annotationlikescount >0)         
  begin        
   set  @CaseAnnotationPoints = @TotalAnnotationsLikes * @CaseAnnotationPoints *3        
   end        
        
  SET @AllCaseAnnotationPoints = @AllCaseAnnotationPoints+@CaseAnnotationPoints      
        
  delete from  #Temp_Scrl_crl_ResearchDoc_MarkAsTbl  where   intCaseId=@intCaseId        
         
  end         
   --drop table   #Temp_Scrl_crl_ResearchDoc_MarkAsTbl        
 --select * into Scrl_crl_ResearchDoc_MarkAsTbl from #Temp_Scrl_crl_ResearchDoc_MarkAsTbl
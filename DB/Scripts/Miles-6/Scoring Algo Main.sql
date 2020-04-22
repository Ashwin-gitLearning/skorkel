Alter procedure sp_scorkel_main
As
BEGIN
-- Run the scoring algo for all users in registartion table
declare @intregistrationid as int=null
DECLARE db_cursor_all_users CURSOR FOR 
SELECT intRegistrationid 
	FROM scrl_RegistrationTbl 
OPEN db_cursor_all_users  
 FETCH NEXT FROM db_cursor_all_users INTO @intregistrationid  

	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		  exec  sp_scrl_scoring_algo @intregistrationid
		  FETCH NEXT FROM db_cursor_all_users INTO @intregistrationid 
	END 
CLOSE db_cursor_all_users  
DEALLOCATE db_cursor_all_users


-- Rank and Percentile of CategoryC
select * from scrl_RegistrationTbl
declare @TotalUser as Numeric,@CategoryCRank as numeric
Set @TotalUser = (select count(*) from scrl_RegistrationTbl where vchrActive='Y')
select userid,categoryC, ROW_NUMBER() OVER(ORDER BY CategoryC asc) As rownum , CPercentileScore  into #temp_score
from score order by CategoryC asc

update #temp_score set CPercentileScore  = (rownum*100)/@TotalUser

update Score 
     set CPercentileScore =(
          select #temp_score.CPercentileScore 
          from #temp_score 
          where #temp_score.UserId = Score.UserId
     )
drop table  #temp_score

--select * from Score
--DECLARE db_cursor CURSOR FOR 
--SELECT userid,categoryC 
--	FROM score order by CategoryC  asc
--OPEN db_cursor_all_users  
-- FETCH NEXT FROM db_cursor INTO @userid, @categoryC

--while (@@FETCH_STATUS =0)
--begin
--	SET @CategoryCRank =(SELECT ROW_NUMBER() OVER(ORDER BY CategoryC ASC) AS Row from Score where UserId=@userid)
--	SET @perecentileCategoryC = ((@CategoryCRank)*100) /(@TotalUser)
--	update score  set CPercentileScore= @perecentileCategoryC *0.38 where UserId=@userid
--	FETCH NEXT FROM db_cursor INTO @userid ,@categoryC
--end 

--CLOSE db_cursor  
--DEALLOCATE db_cursor
-- Rank and Percentile of CategoryD

select userid,categoryD, ROW_NUMBER() OVER(ORDER BY CategoryD asc) As rownum , DPercentileScore  into #temp_score1
from score order by CategoryC asc

update #temp_score1 set DPercentileScore  = (rownum*100)/@TotalUser

update Score 
     set DPercentileScore =(
          select #temp_score1.DPercentileScore 
          from #temp_score1 
          where #temp_score1.UserId = Score.UserId
     )

drop table  #temp_score1

--declare @categoryD as numeric(10,2), @CategoryDRank as int, @perecentileCategoryD as numeric(10,2)
--Set @TotalUser = (select count(*) from scrl_RegistrationTbl where vchrActive='Y')
--DECLARE db_cursor CURSOR FOR 
--SELECT userid,categoryD 
--	FROM Score order by CategoryD  asc
--OPEN db_cursor_all_users  
-- FETCH NEXT FROM db_cursor INTO @userid, @categoryD
--while (@@FETCH_STATUS =0)
--begin
--	SET @CategoryDRank =(SELECT ROW_NUMBER() OVER(ORDER BY CategoryD ASC) AS Row from Score where UserId=@userid)
--	SET @perecentileCategoryD = (((@CategoryDRank)*100) /@TotalUser)
--	update score  set DPercentileScore= @perecentileCategoryD *0.02 where UserId=@userid
--	FETCH NEXT FROM db_cursor INTO @userid ,@categoryD
--end 
--CLOSE db_cursor  
--DEALLOCATE db_cursor

END 

 
  
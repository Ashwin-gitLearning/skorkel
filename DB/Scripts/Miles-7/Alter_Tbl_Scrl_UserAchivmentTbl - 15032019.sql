

ALTER TABLE Scrl_UserAchivmentTbl 
ADD CommitteeName varchar(max) NULL 
DEFAULT NULL

ALTER TABLE Scrl_UserAchivmentTbl 
ADD JournalName varchar(max) NULL 
DEFAULT NULL

Select * from Scrl_UserAchivmentTbl order by intAchivmentId desc

Select CompName,intMilestoneId from Scrl_CompetitionMasterTbl
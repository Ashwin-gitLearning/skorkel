  
Alter PROCEDURE [dbo].[Scrl_AddEditDelKorkelShedular]   
@FlagNo AS INT ,  
@intCaseId AS INT,  
@strCaseTitle varchar(max)=NULL,  
@strCitation AS varchar(max) =NULL,  
@dtJudgementDate AS varchar(max)=NULL,  
@strCites AS varchar(max)=NULL,  
@FilePath AS varchar(max)=NULL,  
@AddedBy varchar(max)=NULL,  
@strPartyNames as varchar(max)=NULL,  
@strBenchStrength as varchar(max)=NULL,  
@strJudgeNames AS varchar(max)=NULL,  
@strContentSource AS varchar(max)=NULL,  
@CaseUID As varchar(max)=null,  
@SplCharFlag As varchar(max)=null,  
@strDescription As varchar(max)=null,  
@strJurisdiction As varchar(max)=null,  
@Case_Number AS VARCHAR(MAX)=NULL,  
@Eq_Citations AS VARCHAR(MAX)=NULL,  
@No_of_Judges AS VARCHAR(MAX)=NULL,  
@Refering_Cases AS VARCHAR(MAX)=NULL,  
@Cases_Refered AS VARCHAR(MAX)=NULL,  
@Respondent AS VARCHAR(MAX)=NULL,  
@Respondent_Adv AS VARCHAR(MAX)=NULL,  
@Appellant_Adv AS VARCHAR(MAX)=NULL,  
@Contaxt AS VARCHAR(MAX)=null,  
@Acts AS VARCHAR(MAX)=null,  
@intYear AS VARCHAR(MAX)=null,  
@strYear AS VARCHAR(MAX)=null,  
@strReporter AS VARCHAR(MAX)=null,  
@strVolume AS VARCHAR(MAX)=null,  
@strPageno AS VARCHAR(MAX)=null,  
@strCourt AS VARCHAR(MAX)=null,  
@strEqCourt AS VARCHAR(MAX)=null,  
@strYearId AS VARCHAR(MAX)=null,  
@strReportName AS VARCHAR(MAX)=null,  
@strEqCourtId AS VARCHAR(MAX)=null,  
@strEqVolume AS VARCHAR(MAX)=null,  
@strReportId AS VARCHAR(MAX)=null,  
@intPageNumber AS VARCHAR(MAX)=null,  
@strVolumeId AS VARCHAR(MAX)=null,  
@intBatchId AS VARCHAR(MAX)=null,  
@strBatch AS VARCHAR(MAX)=null,
@Caselenght As bigint=null 
  
AS  
BEGIN  
    DECLARE @streqCourtIds AS VARCHAR(MAX)  
    DECLARE @streqYearId AS VARCHAR(MAX)  
    DECLARE @streqReportIds AS VARCHAR(MAX)  
    DECLARE @streqVolumIds AS VARCHAR(MAX)  
  
 IF @FlagNo=1  
  BEGIN  
  IF NOT EXISTS ( SELECT 1 FROM Scrl_CaseTbl WHERE intCaseId=@intCaseId)  
  BEGIN  
    INSERT INTO Scrl_CaseTbl (intCaseId,strCaseTitle,strCitation,dtJudgementDate,strCites,strPartyNames,strBenchStrength,strJudgeNames,strContentSource,CaseUID,dtAddedOn,SplCharFlag,strDescription,strJurisdiction,strFilePath,  
    Case_Number,Eq_Citations,No_of_Judges,Cases_Refered,Refering_Cases,Respondent,intYear,Respondent_Adv,Appellant_Adv,Acts,caselength)  
    VALUES (@intCaseId,@strCaseTitle,@strCitation,@dtJudgementDate,@strCites,@strPartyNames,@strBenchStrength,@strJudgeNames,@strContentSource,@CaseUID,GETDATE(),@SplCharFlag,@strDescription,@strJurisdiction,@FilePath,  
    @Case_Number,@Eq_Citations,@No_of_Judges,@Cases_Refered,@Refering_Cases,@Respondent,@intYear,@Respondent_Adv,@Appellant_Adv,@Acts,@Caselenght)  
    SELECT @intCaseId  
   END  
   ELSE  
   BEGIN  
    SELECT 0;  
   END  
   IF NOT EXISTS ( SELECT 1 FROM Scrl_YearMasterTbl WHERE intYear=@intYear)  
   BEGIN  
    INSERT INTO Scrl_YearMasterTbl (intYear,dtAddedOn)  
    VALUES (@intYear,GETDATE())  
   END  
  END  
  ELSE  
  IF @FlagNo=2  
  BEGIN  
   UPDATE Scrl_CaseTbl  
   SET strFilePath=@FilePath  
   WHERE intCaseId=@intCaseId   
  END  
  ELSE  
  IF @FlagNo=3  
  BEGIN  
   IF NOT EXISTS ( SELECT 1 FROM Scrl_CourtMasterTbl WHERE strCourtName LIKE '%'+@strJurisdiction+'%')  
   BEGIN  
   SELECT 1;  
   END  
   ELSE  
   BEGIN  
   SELECT 0;  
   END  
  END  
  ELSE  
  IF @FlagNo=4  
  BEGIN  
   IF NOT EXISTS ( SELECT 1 FROM Scrl_CaseJudgeNameTbl WHERE ContentTitle=@strJudgeNames)  
   BEGIN  
   INSERT INTO Scrl_CaseJudgeNameTbl (ContentTitle,AddedOn)  
   VALUES (@strJudgeNames,GETDATE())  
   SELECT @@identity  
   END  
   ELSE  
   BEGIN  
   SELECT 0;  
   END  
  END  
  ELSE  
  IF @FlagNo=5  
  BEGIN  
   IF NOT EXISTS ( SELECT 1 FROM Scrl_Category_SubjectTbl WHERE strSubjectTemp LIKE '%'+@Contaxt+'%')  
   BEGIN  
   SELECT 1;  
   END  
   ELSE  
   BEGIN  
   SELECT 0;  
   END  
  END  
  ELSE  
  IF @FlagNo=6  
  BEGIN  
   IF NOT EXISTS ( SELECT 1 FROM Scrl_ActMasterTbl WHERE ActName=@Acts)  
   BEGIN  
   INSERT INTO Scrl_ActMasterTbl (ActName,AddedOn)  
   VALUES (@Acts,GETDATE())  
   SELECT @@identity  
   END  
   ELSE  
   BEGIN  
   SELECT 0;  
   END  
  END  
  ELSE  
  IF @FlagNo=7  
  BEGIN  
   IF NOT EXISTS ( SELECT 1 FROM Scrl_AdvocateMasterTbl WHERE strAdvocateName=@Appellant_Adv)  
   BEGIN  
   INSERT INTO Scrl_AdvocateMasterTbl (strAdvocateName)  
   VALUES (@Appellant_Adv)  
   SELECT @@identity  
   END  
   ELSE  
   BEGIN  
   SELECT 0;  
   END  
  END  
  ELSE  
  IF @FlagNo=8  
  BEGIN  
   IF NOT EXISTS ( SELECT 1 FROM Scrl_EqCitationDetails WHERE intCaseId=@intCaseId)  
   BEGIN  
   INSERT INTO Scrl_EqCitationDetails (intCaseId,strEqCitation,strYear,strCourt,strReporter,strVolume,strPageno,dtAddedDateOn)  
   VALUES (@intCaseId,@Eq_Citations,@strYear,@strCourt,@strReporter,@strVolume,@strPageno,GETDATE())  
   SELECT @@identity  
   END  
   ELSE  
   BEGIN  
   SELECT 0;  
   END  
  END  
  ELSE  
  IF @FlagNo=9  
  BEGIN  
   IF NOT EXISTS ( SELECT 1 FROM Scrl_YearMasterTbl WHERE intYear=@intYear)  
   BEGIN  
   INSERT INTO Scrl_YearMasterTbl (intYear,dtAddedOn)  
   VALUES (@intYear,GETDATE())  
   SELECT @@identity  
   END  
   ELSE  
   BEGIN  
   SELECT DISTINCT intYearId FROM Scrl_YearMasterTbl WHERE intYear=@intYear  
   END  
  END  
  ELSE  
  IF @FlagNo=10  
  BEGIN  
   IF NOT EXISTS ( SELECT 1 FROM Scrl_EqReportNameTbl WHERE strReportName=@strReportName)  
   BEGIN  
    INSERT INTO Scrl_EqReportNameTbl (strReportName,strEqCourtId,strYearId,dtAddedOn)  
    VALUES (@strReportName,@strEqCourtId,@strYearId,GETDATE())  
    SELECT @@identity  
   END  
   ELSE  
   BEGIN  
   SET @streqCourtIds=(SELECT strEqCourtId FROM Scrl_EqReportNameTbl WHERE strReportName=@strReportName)  
   SET @streqYearId=(SELECT strYearId FROM Scrl_EqReportNameTbl WHERE strReportName=@strReportName)  
  
      IF NOT EXISTS (SELECT 1 FROM Scrl_EqReportNameTbl WHERE (',' + LTRIM(strYearId) + ',') LIKE '%, ' + Isnull(@strYearId,'0') + ',%' AND strReportName=@strReportName)  
      BEGIN  
      SET @streqYearId= COALESCE(@streqYearId + ', ', '') + (Isnull(@strYearId,'0'))  
      END  
  
       IF NOT EXISTS (SELECT 1 FROM Scrl_EqReportNameTbl WHERE (',' + LTRIM(strEqCourtId) + ',') LIKE '%, ' + Isnull(@strEqCourtId,'0') + ',%' AND strReportName=@strReportName)  
      BEGIN  
      SET @streqCourtIds= COALESCE(@streqCourtIds + ', ', '') + (Isnull(@strEqCourtId,'0'))  
      END  
  
    UPDATE Scrl_EqReportNameTbl  
    SET strEqCourtId=@streqCourtIds,  
    strYearId=@streqYearId  
    WHERE strReportName=@strReportName  
  
    SELECT intReportId FROM Scrl_EqReportNameTbl WHERE strReportName=@strReportName  
  
   END  
  END  
  ELSE  
  IF @FlagNo=11  
  BEGIN  
   IF NOT EXISTS ( SELECT 1 FROM Scrl_EqVolumeTbl WHERE strEqVolume=@strVolume)  
   BEGIN  
    INSERT INTO Scrl_EqVolumeTbl (strEqVolume,strReportId,strYearId,dtAddedOn)  
    VALUES (@strVolume,@strReportId,@strYearId,GETDATE())  
    SELECT @@identity  
   END  
   ELSE  
   BEGIN  
       SET @streqReportIds= (SELECT strReportId FROM Scrl_EqVolumeTbl WHERE strEqVolume=@strVolume)  
    SET @streqYearId= (SELECT strYearId FROM Scrl_EqVolumeTbl WHERE strEqVolume=@strVolume)  
  
      IF NOT EXISTS (SELECT 1 FROM Scrl_EqVolumeTbl WHERE (',' + LTRIM(strYearId) + ',') LIKE '%, ' + @strYearId + ',%' AND strEqVolume=@strVolume)  
      BEGIN  
      SET @streqYearId= COALESCE(@streqYearId + ', ', '') + (@strYearId)  
      END  
      IF NOT EXISTS (SELECT 1 FROM Scrl_EqVolumeTbl WHERE (',' + LTRIM(strReportId) + ',') LIKE '%, ' + @strReportId + ',%' AND strEqVolume=@strVolume)  
      BEGIN  
      SET @streqReportIds= COALESCE(@streqReportIds + ', ', '') + (@strReportId)  
      END  
  
    UPDATE Scrl_EqVolumeTbl  
    SET strReportId=@streqReportIds,  
    strYearId=@streqYearId  
     WHERE strEqVolume=@strVolume  
  
    SELECT intVolumeId FROM Scrl_EqVolumeTbl  WHERE strEqVolume=@strVolume  
  
   END  
  END  
  ELSE  
  IF @FlagNo=12  
  BEGIN  
   IF NOT EXISTS ( SELECT 1 FROM Scrl_EqPageNoTbl WHERE intPageNumber=@strPageno)  
   BEGIN  
    INSERT INTO Scrl_EqPageNoTbl (intPageNumber,strVolumeId,strYearId,dtAddedOn)  
    VALUES (@strPageno,@strVolumeId,@strYearId,GETDATE())  
    SELECT @@identity  
   END  
   ELSE  
   BEGIN  
    SET @streqVolumIds= (SELECT strVolumeId FROM Scrl_EqPageNoTbl WHERE intPageNumber=@strPageno)  
    SET @streqYearId= (SELECT strYearId FROM Scrl_EqPageNoTbl WHERE intPageNumber=@strPageno)  
  
      IF NOT EXISTS (SELECT 1 FROM Scrl_EqPageNoTbl WHERE (',' + LTRIM(strYearId) + ',') LIKE '%, ' + @strYearId + ',%' AND intPageNumber=@strPageno)  
      BEGIN  
      SET @streqYearId= COALESCE(@streqYearId + ', ', '') + (@strYearId)  
      END  
      IF NOT EXISTS (SELECT 1 FROM Scrl_EqPageNoTbl WHERE (',' + LTRIM(strVolumeId) + ',') LIKE '%, ' + Isnull(@strVolumeId,'0') + ',%' AND intPageNumber=@strPageno)  
      BEGIN  
      SET @streqVolumIds= COALESCE(@streqVolumIds + ', ', '') + (Isnull(@strVolumeId,'0'))  
      END  
  
    UPDATE Scrl_EqPageNoTbl  
    SET strVolumeId=@streqVolumIds,  
    strYearId=@streqYearId  
     WHERE intPageNumber=@strPageno  
  
    SELECT intPageNo FROM Scrl_EqPageNoTbl  WHERE intPageNumber=@strPageno  
  
   END  
  END  
  ELSE  
  IF @FlagNo=13  
  BEGIN  
   IF NOT EXISTS ( SELECT 1 FROM Scrl_EqCourtTbl WHERE strEqCourt=@strCourt)  
   BEGIN  
    INSERT INTO Scrl_EqCourtTbl (strEqCourt,strYearId,dtAddedOn)  
    VALUES (@strCourt,@strYearId,GETDATE())  
    SELECT @@identity  
   END  
   ELSE  
   BEGIN  
   SET @streqYearId= (SELECT strYearId FROM Scrl_EqCourtTbl WHERE strEqCourt=@strCourt)  
    IF NOT EXISTS (SELECT 1 FROM Scrl_EqCourtTbl WHERE (',' + LTRIM(strYearId) + ',') LIKE '%, ' + @strYearId + ',%' AND strEqCourt=@strCourt)  
      BEGIN  
      SET @streqYearId= COALESCE(@streqYearId + ', ', '') + (@strYearId)  
      END  
    UPDATE Scrl_EqCourtTbl  
    SET   
    strYearId=@streqYearId  
    WHERE strEqCourt=@strCourt  
    SELECT intEqCourtId FROM Scrl_EqCourtTbl  WHERE strEqCourt=@strCourt  
   END  
  END  
  ELSE  
  IF @FlagNo=14  
  BEGIN  
   DECLARE @Batchs varchar(200)  
   Set @Batchs='B'  
   IF EXISTS (SELECT TOP 1 intBatchId FROM Scrl_EqUtilityBatchTbl ORDER BY intBatchId DESC )  
   BEGIN  
    Set @Batchs='B'+CONVERT(VARCHAR(MAX),((SELECT TOP 1 intBatchId FROM Scrl_EqUtilityBatchTbl ORDER BY intBatchId DESC)+1))  
    INSERT INTO Scrl_EqUtilityBatchTbl (strBatch,dtAddedOn)  
    VALUES (@Batchs,GETDATE())  
    SELECT @@identity  
   END  
   ELSE  
   BEGIN  
   Set @Batchs='B1'  
    INSERT INTO Scrl_EqUtilityBatchTbl (strBatch,dtAddedOn)  
    VALUES (@Batchs,GETDATE())  
    SELECT @@identity  
   END   
  END  
  ELSE  
  IF @FlagNo=15  
  BEGIN  
   SELECT DISTINCT StrEqCitCourtName FROM Scrl_EqCitCourtTbl  
  END  
  ELSE  
  IF @FlagNo=16  
  BEGIN  
   BEGIN tran t1  
   ------------------  Judge Name  
    IF NOT EXISTS ( SELECT 1 FROM Scrl_CaseJudgeNameTbl WHERE ContentTitle IN (SELECT item from dbo.split(@strJudgeNames,';')))  
    BEGIN  
     INSERT INTO Scrl_CaseJudgeNameTbl (ContentTitle,AddedOn)  
     VALUES (@strJudgeNames,GETDATE())  
    END  
   ----------------  Advocates  
    IF NOT EXISTS ( SELECT 1 FROM Scrl_AdvocateMasterTbl WHERE strAdvocateName IN (SELECT item from dbo.split(@Appellant_Adv ,';')))  
    BEGIN  
     INSERT INTO Scrl_AdvocateMasterTbl (strAdvocateName)  
     VALUES (@Appellant_Adv)  
    END  
   ---------------- Acts  
    IF NOT EXISTS ( SELECT 1 FROM Scrl_ActMasterTbl WHERE ActName IN (SELECT item from dbo.split(@Acts ,';')))  
    BEGIN  
     INSERT INTO Scrl_ActMasterTbl (ActName,AddedOn)  
     VALUES (@Acts,GETDATE())  
    END  
  ------------------------- Case Details  
   IF NOT EXISTS ( SELECT 1 FROM Scrl_CaseTbl WHERE intCaseId=@intCaseId)  
   BEGIN  
    INSERT INTO Scrl_CaseTbl (intCaseId,strCaseTitle,strCitation,dtJudgementDate,strCites,strPartyNames,strBenchStrength,strJudgeNames,strContentSource,CaseUID,dtAddedOn,SplCharFlag,strDescription,strJurisdiction,strFilePath,  
    Case_Number,Eq_Citations,No_of_Judges,Cases_Refered,Refering_Cases,Respondent,intYear,Respondent_Adv,Appellant_Adv,Acts)  
    VALUES (@intCaseId,@strCaseTitle,@strCitation,@dtJudgementDate,@strCites,@strPartyNames,@strBenchStrength,@strJudgeNames,@strContentSource,@CaseUID,GETDATE(),@SplCharFlag,@strDescription,@strJurisdiction,@FilePath,  
    @Case_Number,@Eq_Citations,@No_of_Judges,@Cases_Refered,@Refering_Cases,@Respondent,@intYear,@Respondent_Adv,@Appellant_Adv,@Acts)  
    SELECT @intCaseId  
   END  
   ELSE  
   BEGIN  
    SELECT 0;  
   END  
   IF NOT EXISTS ( SELECT 1 FROM Scrl_YearMasterTbl WHERE intYear=@intYear)  
   BEGIN  
    INSERT INTO Scrl_YearMasterTbl (intYear,dtAddedOn)  
    VALUES (@intYear,GETDATE())  
   END  
  
     
  END  
  ELSE  
  IF @FlagNo=17  
  BEGIN   
  DECLARE @YearIds bigint;  
  DECLARE @strCourtIds varchar(20);  
  DECLARE @strReportIds varchar(20);  
  DECLARE @strVolumeIds varchar(20);  
  
  IF(@intYear!='')  
   BEGIN  
   IF NOT EXISTS ( SELECT 1 FROM Scrl_YearMasterTbl WHERE intYear=@intYear)  
   BEGIN  
    INSERT INTO Scrl_YearMasterTbl (intYear,dtAddedOn)  
    VALUES (@intYear,GETDATE())  
    SET @YearIds =(SELECT @@identity)  
   END  
   ELSE  
   BEGIN  
    SET @YearIds=(SELECT DISTINCT intYearId FROM Scrl_YearMasterTbl WHERE intYear=@intYear)  
   END  
   SET @strYearId=@YearIds  
   END  
  
---  Insert or Update court   
   IF(@strCourt!='')  
   BEGIN  
    IF NOT EXISTS ( SELECT 1 FROM Scrl_EqCourtTbl WHERE strEqCourt=@strCourt)  
    BEGIN  
     INSERT INTO Scrl_EqCourtTbl (strEqCourt,strYearId,dtAddedOn)  
     VALUES (@strCourt,@strYearId,GETDATE())  
     SET @strCourtIds= (SELECT @@identity)  
    END  
    ELSE  
    BEGIN  
    SET @streqYearId= (SELECT strYearId FROM Scrl_EqCourtTbl WHERE strEqCourt=@strCourt)  
     IF NOT EXISTS (SELECT 1 FROM Scrl_EqCourtTbl WHERE (',' + RTRIM(LTRIM(strYearId)) + ',') LIKE '%,' + Isnull(@strYearId,'0') + ',%' AND strEqCourt=@strCourt)  
     BEGIN  
      SET @streqYearId= COALESCE(@streqYearId + ',', '') + (Isnull(@strYearId,'0'))  
     END  
     UPDATE Scrl_EqCourtTbl  
     SET   
     strYearId=@streqYearId  
     WHERE strEqCourt=@strCourt  
     SET @strCourtIds= (SELECT intEqCourtId FROM Scrl_EqCourtTbl  WHERE strEqCourt=@strCourt)  
    END  
    SET @strEqCourtId=@strCourtIds;  
   END  
  
---  Insert or Update Reporter   
  
  IF NOT EXISTS ( SELECT 1 FROM Scrl_EqReportNameTbl WHERE strReportName=@strReportName)  
  BEGIN  
   INSERT INTO Scrl_EqReportNameTbl (strReportName,strEqCourtId,strYearId,dtAddedOn)  
   VALUES (@strReportName,@strEqCourtId,@strYearId,GETDATE())  
   SET @strReportIds=(SELECT @@identity);  
  END  
  ELSE  
  BEGIN  
   SET @streqCourtIds=(SELECT strEqCourtId FROM Scrl_EqReportNameTbl WHERE strReportName=@strReportName)  
   SET @streqYearId=(SELECT strYearId FROM Scrl_EqReportNameTbl WHERE strReportName=@strReportName)  
  
   IF NOT EXISTS (SELECT 1 FROM Scrl_EqReportNameTbl WHERE (',' + RTRIM(LTRIM(strYearId)) + ',') LIKE '%,' + Isnull(@strYearId,'0') + ',%' AND strReportName=@strReportName)  
   BEGIN  
    SET @streqYearId= COALESCE(@streqYearId + ',', '') + (Isnull(@strYearId,'0'))  
   END  
  
   IF NOT EXISTS (SELECT 1 FROM Scrl_EqReportNameTbl WHERE (',' + RTRIM(LTRIM(strEqCourtId)) + ',') LIKE '%,' + Isnull(@strEqCourtId,'0') + ',%' AND strReportName=@strReportName)  
   BEGIN  
    SET @streqCourtIds= COALESCE(@streqCourtIds + ',', '') + (Isnull(@strEqCourtId,'0'))  
   END  
  
   UPDATE Scrl_EqReportNameTbl  
   SET strEqCourtId=@streqCourtIds,  
   strYearId=@streqYearId  
   WHERE strReportName=@strReportName  
  
   SET @strReportIds=(SELECT intReportId FROM Scrl_EqReportNameTbl WHERE strReportName=@strReportName);  
  END  
  SET @strReportId=@strReportIds;  
  
---  Insert or Update Volume number   
 IF(@strVolume!='')  
 BEGIN  
  IF NOT EXISTS ( SELECT 1 FROM Scrl_EqVolumeTbl WHERE strEqVolume=@strVolume)  
  BEGIN  
   INSERT INTO Scrl_EqVolumeTbl (strEqVolume,strReportId,strYearId,dtAddedOn)  
   VALUES (@strVolume,@strReportId,@strYearId,GETDATE())  
   SET @strVolumeIds=( SELECT @@identity);  
  END  
  ELSE  
  BEGIN  
   SET @streqReportIds= (SELECT strReportId FROM Scrl_EqVolumeTbl WHERE strEqVolume=@strVolume)  
   SET @streqYearId= (SELECT strYearId FROM Scrl_EqVolumeTbl WHERE strEqVolume=@strVolume)  
  
   IF NOT EXISTS (SELECT 1 FROM Scrl_EqVolumeTbl WHERE (',' + RTRIM(LTRIM(strYearId)) + ',') LIKE '%,' + Isnull(@strYearId,'0') + ',%' AND strEqVolume=@strVolume)  
   BEGIN  
    SET @streqYearId= COALESCE(@streqYearId + ',', '') + (Isnull(@strYearId,'0'))  
   END  
   IF NOT EXISTS (SELECT 1 FROM Scrl_EqVolumeTbl WHERE (',' + RTRIM(LTRIM(strReportId)) + ',') LIKE '%,' + @strReportId + ',%' AND strEqVolume=@strVolume)  
   BEGIN  
    SET @streqReportIds= COALESCE(@streqReportIds + ',', '') + (@strReportId)  
   END  
  
   UPDATE Scrl_EqVolumeTbl  
   SET strReportId=@streqReportIds,  
   strYearId=@streqYearId  
   WHERE strEqVolume=@strVolume  
   SET @strVolumeIds=( SELECT intVolumeId FROM Scrl_EqVolumeTbl  WHERE strEqVolume=@strVolume);  
  END  
  SET @strVolumeId=@strVolumeIds;  
 END  
--------------- Insert or Update Page Number  
  
  IF NOT EXISTS ( SELECT 1 FROM Scrl_EqPageNoTbl WHERE intPageNumber=@strPageno)  
  BEGIN  
   INSERT INTO Scrl_EqPageNoTbl (intPageNumber,strVolumeId,strYearId,dtAddedOn)  
   VALUES (@strPageno,@strVolumeId,@strYearId,GETDATE())  
   SELECT @@identity  
  END  
  ELSE  
  BEGIN  
   SET @streqVolumIds= (SELECT strVolumeId FROM Scrl_EqPageNoTbl WHERE intPageNumber=@strPageno)  
   SET @streqYearId= (SELECT strYearId FROM Scrl_EqPageNoTbl WHERE intPageNumber=@strPageno)  
  
   IF NOT EXISTS (SELECT 1 FROM Scrl_EqPageNoTbl WHERE (',' + RTRIM(LTRIM(strYearId)) + ',') LIKE '%,' +Isnull(@strYearId,'0') + ',%' AND intPageNumber=@strPageno)  
   BEGIN  
    SET @streqYearId= COALESCE(@streqYearId + ',', '') + (Isnull(@strYearId,'0'))  
   END  
   IF NOT EXISTS (SELECT 1 FROM Scrl_EqPageNoTbl WHERE (',' + RTRIM(LTRIM(strVolumeId)) + ',') LIKE '%,' + Isnull(@strVolumeId,'0') + ',%' AND intPageNumber=@strPageno)  
   BEGIN  
    SET @streqVolumIds= COALESCE(@streqVolumIds + ',', '') + (Isnull(@strVolumeId,'0'))  
   END  
  
   UPDATE Scrl_EqPageNoTbl  
   SET strVolumeId=@streqVolumIds,  
   strYearId=@streqYearId  
   WHERE intPageNumber=@strPageno  
  
   SELECT intPageNo FROM Scrl_EqPageNoTbl  WHERE intPageNumber=@strPageno  
  END  
  
  END  
END  

/****** Object:  StoredProcedure [dbo].[Scrl_JobsListing]    Script Date: 21-11-2018 16:46:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Scrl_JobsListing]
@ID INT = NULL,
@FlagNo as int,
@PageSize INT = NULL,
@Currentpage INT = NULL,
@strTitle as varchar(100)=NULL,
@Description as varchar(max)=NULL,
@Location as varchar(100)=NULL,
@JobType as varchar(100)=NULL,
@StartingSalary as varchar(20)=NULL,
@EndingSalary as varchar(20)=NULL,
@StartDuration as varchar(100)=NULL,
@EndDuration as varchar(100)=NULL,
@ActiveStatus AS varchar(2)= NULL,
@checkJobs as bit=NULL

AS

BEGIN
DECLARE @UpperBand INT, @LowerBand INT

SET @LowerBand  = (@CurrentPage - 1) * @PageSize
SET @UpperBand  = (@CurrentPage * @PageSize) + 1

IF @FlagNo=1 
  BEGIN
   SELECT * FROM
    (		
     SELECT COUNT(*) OVER() AS Maxcount,ROW_NUMBER() OVER (ORDER BY Jobs.Created_timestamp desc) AS RowNumber,
     ID,Title,[Description],[Location],JobType,StartingSalary,EndingSalary,StartDuration,EndDuration,[Status],
     CONVERT(VARCHAR(20),Created_timestamp,106)Created_timestamp
     FROM Jobs --where Status='A'
    )	
    AS T WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand
  END
  ELSE IF @FlagNo=2
  BEGIN  
  IF NOT EXISTS(SELECT * FROM Jobs WHERE ID=@ID)
    BEGIN
     INSERT INTO Jobs(Title,Created_timestamp,[Description],[Location],JobType,StartingSalary,EndingSalary,StartDuration,EndDuration,[Status])
	  VALUES(@strTitle,getdate(),@Description,@Location,@JobType,@StartingSalary,@EndingSalary,@StartDuration,@EndDuration,'A')
		SELECT 1
	END
	ELSE
	BEGIN
		SELECT 0
	END
  END
  ELSE IF @FlagNo=3
  BEGIN
   Update Jobs
    Set Status='D',
  	Created_timestamp=getdate()
  	where ID=@ID 
  END
  ELSE IF @FlagNo=4
  BEGIN
   SELECT * FROM
    (		
     SELECT COUNT(*) OVER() AS Maxcount,ROW_NUMBER() OVER (ORDER BY Jobs.Created_timestamp desc) AS RowNumber,
     ID,Title,[Description],[Location],JobType,StartingSalary,EndingSalary,StartDuration,EndDuration,[Status],
     CONVERT(VARCHAR(20),Created_timestamp,106)Created_timestamp
     FROM Jobs where Status=@ActiveStatus
    )	
    AS T WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand
  END
  ELSE IF @FlagNo=5
  BEGIN   		
     SELECT ROW_NUMBER() OVER (ORDER BY Jobs.Created_timestamp desc) AS RowNumber,
     ID,Title,[Description],[Location],JobType,StartingSalary,EndingSalary,StartDuration,EndDuration,[Status],
     CONVERT(VARCHAR(20),Created_timestamp,106)Created_timestamp
     FROM Jobs where ID=@ID     
  END
  ELSE IF @FlagNo=6
	BEGIN   		
	If @checkJobs=1
	Begin
	  Update Jobs
	    Set Status='A'
	    where ID=@ID
	End
	Else
	Begin
	  Update Jobs
	    Set Status='D'
	    where ID=@ID
	End
	END
	ELSE IF @FlagNo=7
    BEGIN
	  Update Jobs
		Set Title=@strTitle,
			[Description]=@Description,
			[Location]=@Location,
			JobType=@JobType,
			StartingSalary=@StartingSalary,
			EndingSalary=@EndingSalary,
			StartDuration=@StartDuration,
			EndDuration=@EndDuration,
			Created_timestamp=getdate()
			where ID=@ID
	END
	ELSE IF @FlagNo=8 
	BEGIN
	 SELECT * FROM
	  (		
	   SELECT COUNT(*) OVER() AS Maxcount,ROW_NUMBER() OVER (ORDER BY Jobs.Created_timestamp desc) AS RowNumber,
	   ID,Title,[Description],[Location],JobType,StartingSalary,EndingSalary,StartDuration,EndDuration,[Status],
	   CONVERT(VARCHAR(20),Created_timestamp,106)Created_timestamp
	   FROM Jobs where Status='A'
	  )	
	  AS T WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand
	END
END
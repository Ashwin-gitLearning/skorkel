
GO
/****** Object:  StoredProcedure [dbo].[Scrl_NewsListing]    Script Date: 1/8/2019 5:12:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[Scrl_NewsListing]
@ID INT = NULL,
@FlagNo as int,
@PageSize INT = NULL,
@Currentpage INT = NULL,
@strTitle as varchar(max)=NULL,
@strContent as varchar(max)=NULL,
@strLink as varchar(200)=NULL,
@strType as varchar(100)=NULL,
@checkNews as bit=NULL,
@publishTimestamp as datetime = NULL,
@SourceID INT = NULL,
@NewsIds VARCHAR(200)=NULL

AS

BEGIN
DECLARE @UpperBand INT, @LowerBand INT

SET @LowerBand  = (@CurrentPage - 1) * @PageSize
SET @UpperBand  = (@CurrentPage * @PageSize) + 1

 IF @FlagNo= 1 
  BEGIN
   SELECT * FROM
    (		
     SELECT COUNT(*) OVER() AS Maxcount,ROW_NUMBER() OVER (ORDER BY NewsArticles.Created_timestamp desc) AS RowNumber,
     ID,[Title],[Type],[Source_link],[Photo_path],[Content],
     CONVERT(VARCHAR(20),Created_timestamp,106)Created_timestamp
     FROM NewsArticles where (Status='U' OR Status='A')
    )	
    AS T WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand
  END
 ELSE IF @FlagNo=2
  BEGIN
   INSERT INTO NewsArticles(Title,[Type],Source_link,Content,Created_timestamp,[Status],Published_Timestamp)
	VALUES(@strTitle,@strType,@strLink,@strContent,getdate(),'U',getdate())
	 --SELECT @@Identity as intBid
	 DECLARE @intNewsId INT
	 SET @intNewsId = @@IDENTITY;	
	 SELECT @intNewsId;	
  END
  ELSE IF @FlagNo=3
  BEGIN
  --Newly updated
  IF NOT EXISTS(SELECT * FROM NewsSources WHERE link=@strLink AND (Status='U' OR Status='A'))
  --IF NOT EXISTS(SELECT * FROM NewsArticles WHERE (Status='A'))
	--Newly updated	
    BEGIN
     INSERT INTO NewsSources(Title,Link,Created_timestamp,[Status])
	  VALUES(@strTitle,@strLink,getdate(),'A')
	   --SELECT @@Identity as intBid
		SELECT 1
	END
	ELSE
		BEGIN
			SELECT 0
		END
  END
 -- ELSE IF @FlagNo=3
 -- BEGIN
 -- --Newly updated
 -- IF NOT EXISTS(SELECT * FROM NewsArticles WHERE Source_link=@strLink AND (Status='U' OR Status='A'))
 -- --IF NOT EXISTS(SELECT * FROM NewsArticles WHERE (Status='A'))
	----Newly updated	
 --   BEGIN
 --    INSERT INTO NewsArticles(Title,[Type],Source_link,Content,Created_timestamp,Published_Timestamp,[Status])
	--  VALUES(@strTitle,'RSS',@strLink,@strContent,getdate(),@publishTimestamp,'A')
	--   --SELECT @@Identity as intBid
	--	SELECT 1
	--END
	--ELSE
	--	BEGIN
	--		SELECT 0
	--	END
 -- END
  ELSE IF @FlagNo=4
  BEGIN
   SELECT * FROM
    (		
     SELECT COUNT(*) OVER() AS Maxcount,ROW_NUMBER() OVER (ORDER BY NewsSources.Created_timestamp desc) AS RowNumber,
     ID,Title,Link,
     CONVERT(VARCHAR(20),Created_timestamp,106)Created_timestamp
     FROM NewsSources where (Status='U' OR Status='A')
    )	
    AS T WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand
  END
  --ELSE IF @FlagNo=4
  --BEGIN
  -- SELECT * FROM
  --  (		
  --   SELECT COUNT(*) OVER() AS Maxcount,ROW_NUMBER() OVER (ORDER BY NewsArticles.Created_timestamp desc) AS RowNumber,
  --   ID,[Title],[Type],[Source_link],[Photo_path],[Content],
  --   CONVERT(VARCHAR(20),Created_timestamp,106)Created_timestamp
  --   FROM NewsArticles where Type='RSS' And (Status='U' OR Status='A')
  --  )	
  --  AS T WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand
  --END
  ELSE IF @FlagNo=5
  BEGIN
   Update NewsSources
    Set Status='D',
	Created_timestamp=getdate()
	where ID=@ID --And Type='RSS'
  END
 -- ELSE IF @FlagNo=5
 -- BEGIN
 --  Update NewsArticles
 --   Set Status='D',
	--Created_timestamp=getdate()
	--where ID=@ID --And Type='RSS'
 -- END
  ELSE IF @FlagNo=6
  BEGIN   		
     SELECT ROW_NUMBER() OVER (ORDER BY NewsSources.Created_timestamp desc) AS RowNumber,
     ID,Title,Link,
     CONVERT(VARCHAR(20),Created_timestamp,106)Created_timestamp
     FROM NewsSources where (Status='U' OR Status='A' OR Status='D') AND ID=@ID     
  END
  --ELSE IF @FlagNo=6
  --BEGIN   		
  --   SELECT ROW_NUMBER() OVER (ORDER BY NewsArticles.Created_timestamp desc) AS RowNumber,
  --   ID,[Title],[Type],[Source_link],[Photo_path],[Content],
  --   CONVERT(VARCHAR(20),Created_timestamp,106)Created_timestamp
  --   FROM NewsArticles where Type='RSS' And (Status='U' OR Status='A') AND ID=@ID     
  --END
   ELSE IF @FlagNo=7
   BEGIN
	  Update NewsSources
		Set Title=@strTitle,
			Link=@strLink,
			Created_timestamp=getdate()
			where ID=@ID
	END
 --  ELSE IF @FlagNo=7
 --  BEGIN
	--  Update NewsArticles
	--	Set Title=@strTitle,
	--		Source_link=@strLink,
	--		Created_timestamp=getdate()
	--		where [Type]='RSS' AND ID=@ID
	--END
	ELSE IF @FlagNo=8
	BEGIN   		
     SELECT ROW_NUMBER() OVER (ORDER BY NewsArticles.Created_timestamp desc) AS RowNumber,
     ID,[Title],[Type],[Source_link],[Photo_path],[Content],[Status],
     CONVERT(VARCHAR(20),Created_timestamp,106)Created_timestamp,SourceID
     FROM NewsArticles where (Status='U' OR Status='A') AND ID=@ID
	END
	ELSE IF @FlagNo=9
	BEGIN   		
	If @checkNews=1
	Begin
	  Update NewsArticles
	    Set Status='U',
	    Published_Timestamp=getdate()
	    where ID=@ID
	End
	Else
	Begin
	  Update NewsArticles
	    Set Status='A',
	    Published_Timestamp=getdate()
	    where ID=@ID
	End
	END
	ELSE IF @FlagNo=10
	BEGIN
	  Update NewsArticles
		Set Title=@strTitle,
			[Type]=@strType,
			Content=@strContent,
			Source_link=@strLink,
			Published_Timestamp=getdate()
			where ID=@ID
	END
	ELSE IF @FlagNo= 11 --Common User
	BEGIN
	 SELECT * FROM
	  (		
	   SELECT COUNT(*) OVER() AS Maxcount,ROW_NUMBER() OVER (ORDER BY NewsArticles.Created_timestamp desc) AS RowNumber,
	   ID,[Title],[Type],[Source_link],[Photo_path],[Content],
	   CONVERT(VARCHAR(20),Created_timestamp,106)Created_timestamp
	   FROM NewsArticles where Status='U'
	  )	
	  AS T WHERE RowNumber > @LowerBand AND RowNumber < @UpperBand
	END
	ELSE IF @FlagNo=12
	BEGIN   		
     SELECT
     ID,[Title],[Type],[Source_link],[Photo_path],[Content],[Status],
     CONVERT(VARCHAR(20),Created_timestamp,106)Created_timestamp,SourceID
     FROM NewsArticles where Status='U' AND ID=@ID
	END
	ELSE IF @FlagNo=13
	BEGIN   		
     SELECT Top 5
     ID, [Title]
     FROM NewsArticles where Status='U'
	 Order By Created_timestamp desc
	END
	ELSE IF @FlagNo=14
	BEGIN	
		IF NOT EXISTS (SELECT * FROM NewsArticles WHERE Title=@strTitle and [Type] = 'RSS' and Source_link = @strLink  and Content = @strContent) 
			BEGIN
				INSERT INTO NewsArticles(Title,[Type],Source_link,Content,Created_timestamp,Published_Timestamp,[Status],SourceID)
				  VALUES(@strTitle,'RSS',@strLink,@strContent,getdate(),@publishTimestamp,'U',@SourceID)
					DECLARE @intRSSNewsId INT
					SET @intRSSNewsId = @@IDENTITY;	
					SELECT @intRSSNewsId;
			END		
		ELSE
  			BEGIN
  				SELECT 0
  			END
	END
	--ELSE IF @FlagNo=14
	--BEGIN	
	--INSERT INTO NewsArticles(Title,[Type],Source_link,Content,Created_timestamp,Published_Timestamp,[Status],SourceID)
	--  VALUES(@strTitle,'RSS',@strLink,@strContent,getdate(),@publishTimestamp,'A',@SourceID)
	--    SELECT @@Identity as intBid		
	--END
	ELSE IF @FlagNo=15
    BEGIN
     Update NewsArticles
      Set Status='D',
  		Created_timestamp=getdate()
  		where ID=@ID --And Type='RSS'
    END
	ELSE IF @FlagNo=16
    BEGIN
     Select count(*) as RssCount from NewsSources where status='A'
    END
	ELSE IF @FlagNo=17
    BEGIN
    --Newly updated
    IF NOT EXISTS(SELECT * FROM NewsSources WHERE link=@strLink AND (Status='U' OR Status='A') AND ID<>@ID)
  	--Newly updated	
      BEGIN
     --  INSERT INTO NewsSources(Title,Link,Created_timestamp,[Status])
  	  --VALUES(@strTitle,@strLink,getdate(),'A')
  	   --SELECT @@Identity as intBid
  		SELECT 1
  	END
  	ELSE
  		BEGIN
  			SELECT 0
  		END
    END
	ELSE IF @FlagNo=18
	BEGIN			
	 --Newly Added
	 WITH UserParms AS
	 (
	 	SELECT ItemNumber,Item
	 	FROM dbo.[DelimitedSplit8K](@NewsIds, ',')
	 )
	 SELECT COUNT(*) OVER() AS Maxcount,* FROM
	 (		
	 	SELECT ROW_NUMBER() OVER (ORDER BY NewsArticles.Created_timestamp desc) AS RowNumber,
	     ID,[Title],[Type],[Source_link],[Photo_path],[Content],CONVERT(VARCHAR(20),Created_timestamp,106)Created_timestamp
	 	   FROM NewsArticles WHERE Status='U' --AND ID IN (Select item from dbo.Split(@NewsIds, ','))
	 )	AS T JOIN UserParms As U ON ID = U.Item WHERE ID IN (Select Item from UserParms) ORDER BY U.ItemNumber
	  
	END
END
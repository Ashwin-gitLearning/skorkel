
/****** Object:  StoredProcedure [dbo].[Scrl_GetScorePoints]    Script Date: 01-04-2019 13:18:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Scrl_GetScorePoints]
@FlagNo int,
@intAddedBy int
AS
BEGIN

IF @FlagNo=1
	BEGIN 		
		SELECT * from score where UserId = @intAddedBy
	END
END




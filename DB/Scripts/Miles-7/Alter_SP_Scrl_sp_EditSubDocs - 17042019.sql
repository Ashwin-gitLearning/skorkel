
/****** Object:  StoredProcedure [dbo].[Scrl_sp_EditSubDocs]    Script Date: 17-04-2019 21:59:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Scrl_sp_EditSubDocs] @FlagNo    AS INT,   
			@intAddedBy As INT=null,
			@FilePath As Varchar(500) =null,
			@FileName As Varchar(500) =null
AS   
  BEGIN   
      IF @FlagNo = 2   
        BEGIN
		Delete from SubDocs where userId = @intAddedBy;
       insert into SubDocs(userId,file_path, [file_name],created_timestamp)
	   values(@intAddedBy, @FilePath, @FileName,GETDATE())
   
        END   
  END 
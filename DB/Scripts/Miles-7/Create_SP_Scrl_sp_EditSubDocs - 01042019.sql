
/****** Object:  StoredProcedure [dbo].[Scrl_sp_Editjournallist]    Script Date: 01-04-2019 17:03:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Scrl_sp_EditSubDocs] @FlagNo    AS INT,   
			@intAddedBy As INT=null,
			@FilePath As Varchar(500) =null,
			@FileName As Varchar(500) =null
AS   
  BEGIN   
      IF @FlagNo = 2   
        BEGIN
       insert into SubDocs(userId,file_path, [file_name],created_timestamp)
	   values(@intAddedBy, @FilePath, @FileName,GETDATE())
   
        END   
  END 
/****** Object:  StoredProcedure [dbo].[Scrl_sp_SubDocs]    Script Date: 17-04-2019 21:45:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Scrl_sp_SubDocs] @FlagNo    AS INT, 
										  @PageSize INT = null,  
										  @Currentpage INT = null,
										  @TxtSearch As Varchar(500) =null
AS 
  BEGIN 
  DECLARE @UpperBand INT, @LowerBand INT  
  SET @LowerBand  = (@CurrentPage - 1) * @PageSize  
  SET @UpperBand  = (@CurrentPage * @PageSize) + 1  

		IF @FlagNo = 1
        BEGIN 
	  select * from( 
            SELECT COUNT(*) OVER()  AS Maxcount,
			ROW_NUMBER() OVER(  ORDER  BY created_timestamp DESC ) rn,
             sd.id SubDocId,sd.created_timestamp, convert(varchar, sd.created_timestamp, 106) as DateAdded, 
			 sd.file_name as FileName, file_path FilePath, CONCAT(reg.vchrFirstName ,' ', ISNULL(reg.vchrLastName,'')) UserName ,
			 vchrPhotoPath
			 from SubDocs sd inner join  scrl_RegistrationTbl reg on sd.userId=reg.intRegistrationId 
			 WHERE
				(ISNULL(reg.vchrFirstName,'') + ' ' + ISNULL(reg.vchrLastName,'')) LIKE 
					CASE @TxtSearch WHEN '' THEN (ISNULL(reg.vchrFirstName,'') + ' ' + ISNULL(reg.vchrLastName,'')) 
					ELSE '%' + @TxtSearch + '%' END	             	
			 	)tb 
				where tb.rn>@LowerBand and tb.rn<@UpperBand order by tb.created_timestamp desc
        END 
		
  END 
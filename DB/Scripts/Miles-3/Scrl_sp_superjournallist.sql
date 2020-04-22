IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'Scrl_sp_superjournallist')
DROP PROCEDURE Scrl_sp_superjournallist
go
create PROCEDURE Scrl_sp_superjournallist @FlagNo    AS INT, 
                                          @JournalId AS NUMERIC(18, 0)= NULL, 
                                          @ArticleId AS NUMERIC(18, 0)= NULL,
										  @ActiveStatus AS bit= NULL,
										  @IntUserId AS INT= NULL,
										  @PageSize INT = null,  
										  @Currentpage INT = null  
AS 
   BEGIN 
  DECLARE @UpperBand INT, @LowerBand INT  
SET @LowerBand  = (@CurrentPage - 1) * @PageSize  
SET @UpperBand  = (@CurrentPage * @PageSize) + 1  

      IF @FlagNo = 1 
        BEGIN 
    select * from( 
            SELECT COUNT(*) OVER()  AS Maxcount,
      ROW_NUMBER() OVER(  ORDER  BY year DESC, 
                      month DESC ) rn, (SELECT Count(1) 
                    FROM   journalarticles 
                    WHERE  journal_id = id)  ArticleCount, 
                   id                        JournalID, 
                   month, 
                   year, 
                   title, 
                   status, 
                   CONCAT(title, ' ', DATENAME(month, DATEADD(month, month, 0) 
                                                      - 1 
                                      ), 
                   ' ' 
                   , 
                   CAST(year AS VARCHAR(4))) JournalTitle 
            FROM   journals  
            ) as Tbl where Tbl.rn>@LowerBand and Tbl.rn<@UpperBand order by Tbl.rn;
        END 
         IF @FlagNo = 2
        BEGIN 
            select * from( 
            SELECT COUNT(*) OVER()  AS Maxcount,
      ROW_NUMBER() OVER(  ORDER  BY year DESC, 
                      month DESC ) rn,  (SELECT Count(1) 
                    FROM   journalarticles 
                    WHERE  journal_id = id)  ArticleCount, 
                   id                        JournalID, 
                   month, 
                   year, 
                   title, 
                   status, 
                   CONCAT(title, ' ', DATENAME(month, DATEADD(month, month, 0) 
                                                      - 1 
                                      ), 
                   ' ' 
                   , 
                   CAST(year AS VARCHAR(4))) JournalTitle 
            FROM   journals where status=@ActiveStatus
      ) tbl where tbl.rn>@LowerBand and tbl.rn <@UpperBand
      order by Tbl.rn;
        END 
      IF @FlagNo = 5
        BEGIN 
            SELECT  
                   id                        JournalID, 
                   month, 
                   year, 
                   title, 
                   status, 
                   CONCAT(title, ' ', DATENAME(month, DATEADD(month, month, 0) 
                                                      - 1 
                                      ), 
                   ' ' 
                   , 
                   CAST(year AS VARCHAR(4))) JournalTitle 
            FROM   journals where id=@JournalId

        END 
      IF @FlagNo = 6
        BEGIN 
    select * from (
          SELECT COUNT(*) OVER()  AS Maxcount,
      ROW_NUMBER() OVER(  ORDER  BY articleid DESC) rn, * from (SELECT  
                 jouart.journal_id JournalID,art.file_path FilePath, art.ID ArticleID,art.userId AddedByID, jouart.published_title ArtTitle, CONCAT(reg.vchrFirstName ,' ', ISNULL(reg.vchrLastName,''))AddedByName
         ,count(DISTINCT artLikes.ID) Likes
         ,count(DISTINCT artCmts.ID) Comments
            FROM   JournalArticles jouart inner Join Articles art 
      on art.ID=jouart.article_id inner join scrl_RegistrationTbl reg on intRegistrationId=art.userId left outer join ArticleLikes artLikes on artLikes.journal_article_id=art.ID 
      left outer join ArticleComments artCmts on artCmts.journal_article_id=art.ID 
      group by art.ID, art.file_path, art.userId, jouart.published_title, jouart.journal_id,reg.vchrFirstName, reg.vchrLastName
       having jouart.journal_id=@JournalId
      ) tbl
      )tb where tb.rn>@LowerBand and tb.rn<@UpperBand
      

        END 
    IF @FlagNo = 7
        BEGIN 
  --  delete from Articles
            SELECT  
                 jouart.journal_id JournalID, art.file_path FilePath,  CONCAT(jou.title, ' ', DATENAME(month, DATEADD(month, jou.month, 0) 
                                                      - 1 
                                      ), 
                   ' ' 
                   , 
                   CAST(jou.year AS VARCHAR(4))) journalTitle, art.ID ArticleID, jouart.published_title ArtTitle, CONCAT(reg.vchrFirstName ,' ', ISNULL(reg.vchrLastName,''))AddedByName,art.userId AddedByID,
         count(DISTINCT artLikes.ID) Likes
         ,count(DISTINCT artCmts.ID) Comments
            FROM   JournalArticles jouart inner Join Articles art 
      on art.ID=jouart.article_id inner join Journals jou on jouart.journal_id=jou.ID inner join scrl_RegistrationTbl reg on intRegistrationId=art.userId left outer join ArticleLikes artLikes on artLikes.journal_article_id=art.ID 
      left outer join ArticleComments artCmts on artCmts.journal_article_id=art.ID 
      group by art.ID, art.userId, art.file_path,jou.title,jou.month,jou.year, jouart.published_title, jouart.journal_id,reg.vchrFirstName, reg.vchrLastName
       having jouart.journal_id=@JournalId and art.ID=@ArticleId 

        END 
    IF @FlagNo = 9
        BEGIN 
    select * from( 
            SELECT COUNT(*) OVER()  AS Maxcount,
      ROW_NUMBER() OVER(  ORDER  BY created_timestamp DESC ) rn,
             art.Id ArticleId,art.created_timestamp, convert(varchar, art.created_timestamp, 106) as DateAdded, art.Title, file_path FilePath, CONCAT(reg.vchrFirstName ,' ', ISNULL(reg.vchrLastName,'')) UserName  from Articles art inner join  scrl_RegistrationTbl reg on art.userId=reg.intRegistrationId 
        )tb where tb.rn>@LowerBand and tb.rn<@UpperBand order by tb.created_timestamp desc
        END 
    IF @FlagNo = 11
        BEGIN 
  --  delete from Articles
            SELECT jourart.journal_id JournalID,     CONCAT(title, ' ', DATENAME(month, DATEADD(month, month, 0) 
                                                      - 1 
                                      ), 
                   ' ' 
                   , 
                   CAST(year AS VARCHAR(4)))  JournalTitle 
        from JournalArticles jourart inner Join Journals jour 
        on jourart.journal_id=jour.ID 
        where jourart.article_id=@ArticleId
        Order by jour.year desc, jour.month desc
        END
    IF @FlagNo =13
    BEGIN
    Select cmnts.ID CommentId, cmnts.journal_article_id ArticleId, 
    cmnts.comment_detail Comment, cmnts.user_id AddedByID, 
    convert(Varchar(12),cmnts.created_timestamp ,106)AddedOn , 
    CONCAT(reg.vchrFirstName ,' ', ISNULL(reg.vchrLastName,'')) AddedBy, 
    reg.vchrPhotoPath PhotoPath  
    from ArticleComments cmnts inner join scrl_RegistrationTbl reg 
    on cmnts.user_id = reg.intRegistrationId 
    where cmnts.journal_article_id= @ArticleId
    order by cmnts.created_timestamp desc
    END
     IF @FlagNo = 18
        BEGIN 
            SELECT  top 1
                   id                        JournalID, 
                   month, 
                   year, 
                   title, 
                   status, 
                   CONCAT(title, ' ', DATENAME(month, DATEADD(month, month, 0) 
                                                      - 1 
                                      ), 
                   ' ' 
                   , 
                   CAST(year AS VARCHAR(4))) JournalTitle 
            FROM   journals where datefromparts(year, month,1)<=getdate()
 and status=1 order by year desc, month desc

        END 
		IF @FlagNo=20
		 BEGIN
			SELECT * FROM ArticleLikes WHERE user_id= @IntUserId and journal_article_id=@ArticleId
		 END
  END 
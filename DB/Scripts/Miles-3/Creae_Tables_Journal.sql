CREATE TABLE Articles (ID INT, Title VARCHAR(100), userId INT, file_path VARCHAR(256), created_timestamp DATETIME)
GO
CREATE TABLE Journals (ID INT, month INT, year INT, title VARCHAR(100), created_timestamp DATETIME, status BIT, published_timestamp DATETIME)
GO
CREATE TABLE JournalArticles (journal_article_id INT, journal_id INT, article_id INT, published_title VARCHAR(100), added_timestamp DATETIME)
GO
CREATE TABLE ArticleComments (ID INT, journal_article_id INT, comment_detail VARCHAR(500), user_id INT, created_timestamp DATETIME)
go
CREATE TABLE ArticleLikes (ID INT, journal_article_id INT, user_id INT, created_timestamp DATETIME)
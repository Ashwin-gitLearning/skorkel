CREATE TABLE Scrl_CaseSummaryLikes
(
ID INT identity(1,1),
summary_id INT,
user_id INT,
timestamp datetime
)
--select * from Scrl_CaseSummaryLikes
--insert into Scrl_CaseSummaryLikes(summary_id, user_id, timestamp) values( 28,  100752, GETDATE())
/****** Object:  Table [dbo].[Scrl_UserPostQAAnsReplyTbl]    Script Date: 10/31/2018 9:25:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Scrl_UserPostQAAnsReplyTbl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[intAnswerId] [int] NULL,
	[strAnsLiStatus] [varchar](20) NULL,
	[strComment] [varchar](max) NULL,
	[dtAddedOn] [datetime] NULL,
	[intAnsAddedBy] [int] NULL,
 CONSTRAINT [PK_Scrl_UserPostQAAnsTbl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Scrl_UserPostQAAnsReplyTbl]  WITH CHECK ADD  CONSTRAINT [FK_Scrl_UserPostQAAnsReplyTbl_Scrl_UserPostQAReplyTbl] FOREIGN KEY([intAnswerId])
REFERENCES [dbo].[Scrl_UserPostQAReplyTbl] ([intQAReplyLikeShareId])
GO

ALTER TABLE [dbo].[Scrl_UserPostQAAnsReplyTbl] CHECK CONSTRAINT [FK_Scrl_UserPostQAAnsReplyTbl_Scrl_UserPostQAReplyTbl]
GO



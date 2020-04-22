
/****** Object:  Table [dbo].[SCL_SA_Notifications]    Script Date: 12/6/2018 12:56:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SCL_SA_Notifications](
	[Notification_ID] [int] IDENTITY(1,1) NOT NULL,
	[Notification_Detail] [varchar](500) NOT NULL,
	[dateaddedon] [datetime] NOT NULL
) ON [PRIMARY]
GO


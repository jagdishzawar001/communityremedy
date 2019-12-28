USE [CommunityRemedyDB]
GO

/****** Object:  Table [dbo].[MaintenanceService]    Script Date: 12/28/2019 11:19:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MaintenanceService](
	[ServiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[ServiceName] [varchar](50) NULL,
	[Details] [varchar](500) NULL,
 CONSTRAINT [PK_MaintenanceService] PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


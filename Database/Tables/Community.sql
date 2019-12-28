USE [CommunityRemedyDB]
GO

/****** Object:  Table [dbo].[Community]    Script Date: 12/28/2019 11:18:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Community](
	[CommunityID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[GovtRegistrationId] [varchar](100) NULL,
	[Address] [varchar](500) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[Zip] [varchar](10) NULL,
	[PhoneContact] [varchar](25) NULL,
	[MobileContact] [varchar](25) NULL,
	[EmailId] [varchar](50) NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_CommunityObject] PRIMARY KEY CLUSTERED 
(
	[CommunityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Community]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Community_dbo.ApplicationUser_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[ApplicationUser] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Community] CHECK CONSTRAINT [FK_dbo.Community_dbo.ApplicationUser_UserId]
GO


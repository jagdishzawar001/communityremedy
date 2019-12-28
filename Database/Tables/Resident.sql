USE [CommunityRemedyDB]
GO

/****** Object:  Table [dbo].[Resident]    Script Date: 12/28/2019 11:19:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Resident](
	[ResidentId] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[DOB] [varchar](25) NULL,
	[CommunityId] [bigint] NOT NULL,
	[ApartmentNumber] [varchar](10) NULL,
	[MobileContact] [varchar](25) NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Resident] PRIMARY KEY CLUSTERED 
(
	[ResidentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Resident]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Resident_dbo.ApplicationUser_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[ApplicationUser] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Resident] CHECK CONSTRAINT [FK_dbo.Resident_dbo.ApplicationUser_UserId]
GO

ALTER TABLE [dbo].[Resident]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Resident_dbo_Community_CommunityId] FOREIGN KEY([CommunityId])
REFERENCES [dbo].[Community] ([CommunityID])
GO

ALTER TABLE [dbo].[Resident] CHECK CONSTRAINT [FK_dbo.Resident_dbo_Community_CommunityId]
GO


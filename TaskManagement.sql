
/****** Object:  StoredProcedure [dbo].[sp_UpdateTask]    Script Date: 13-07-2024 22:13:59 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_UpdateTask]
GO
/****** Object:  StoredProcedure [dbo].[sp_RegisterUser]    Script Date: 13-07-2024 22:13:59 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_RegisterUser]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUser]    Script Date: 13-07-2024 22:13:59 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_GetUser]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTaskStatus]    Script Date: 13-07-2024 22:13:59 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_GetTaskStatus]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTasks]    Script Date: 13-07-2024 22:13:59 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_GetTasks]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTask]    Script Date: 13-07-2024 22:13:59 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_GetTask]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteTask]    Script Date: 13-07-2024 22:13:59 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_DeleteTask]
GO
/****** Object:  StoredProcedure [dbo].[sp_AddTask]    Script Date: 13-07-2024 22:13:59 ******/
DROP PROCEDURE IF EXISTS [dbo].[sp_AddTask]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND type in (N'U'))
ALTER TABLE [dbo].[Task] DROP CONSTRAINT IF EXISTS [FK_Task_User]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND type in (N'U'))
ALTER TABLE [dbo].[Task] DROP CONSTRAINT IF EXISTS [FK_Task_Task]
GO
/****** Object:  Table [dbo].[User]    Script Date: 13-07-2024 22:13:59 ******/
DROP TABLE IF EXISTS [dbo].[User]
GO
/****** Object:  Table [dbo].[TaskStatus]    Script Date: 13-07-2024 22:13:59 ******/
DROP TABLE IF EXISTS [dbo].[TaskStatus]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 13-07-2024 22:13:59 ******/
DROP TABLE IF EXISTS [dbo].[Task]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 13-07-2024 22:13:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Duedate] [datetime] NOT NULL,
	[StatusId] [smallint] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskStatus]    Script Date: 13-07-2024 22:13:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskStatus](
	[Id] [smallint] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_TaskStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 13-07-2024 22:13:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](200) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[TaskStatus] ([Id], [Status]) VALUES (1, N'To do')
INSERT [dbo].[TaskStatus] ([Id], [Status]) VALUES (2, N'In Progress')
INSERT [dbo].[TaskStatus] ([Id], [Status]) VALUES (3, N'Completed')
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Task] FOREIGN KEY([StatusId])
REFERENCES [dbo].[TaskStatus] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Task]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_User]
GO
/****** Object:  StoredProcedure [dbo].[sp_AddTask]    Script Date: 13-07-2024 22:13:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AddTask] (
	@Id UNIQUEIDENTIFIER, 
	@Title NVARCHAR(50),
	@Description NVARCHAR(200),
	@Duedate DateTime,
	@StatusId SMALLINT,
	@UserId NVARCHAR(200)	
	)
AS
BEGIN
	INSERT INTO [Task] Values(@Id, @Title, @Description, @Duedate, @StatusId, @UserId, GetDate());
END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteTask]    Script Date: 13-07-2024 22:13:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteTask] (
	@Id UNIQUEIDENTIFIER )
AS
BEGIN   
   Delete from Task where Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTask]    Script Date: 13-07-2024 22:13:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetTask] (
	@Id UNIQUEIDENTIFIER )
AS
BEGIN   
   
	SELECT a.Id, Title, [Description], [DueDate], a.StatusId, b.Status FROM Task a 
	INNER JOIN TaskStatus b on  a.StatusId = b.Id
	WHERE a.Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTasks]    Script Date: 13-07-2024 22:13:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetTasks] (
	@UserId UNIQUEIDENTIFIER )
AS
BEGIN   
   
	SELECT a.Id, Title, [Description], [DueDate], a.StatusId, b.Status FROM Task a 
	INNER JOIN TaskStatus b on  a.StatusId = b.Id
	WHERE UserId = @UserId
	ORDER BY CreatedDate desc;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetTaskStatus]    Script Date: 13-07-2024 22:13:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetTaskStatus] 
AS
BEGIN   
   
	SELECT Id, [Status] FROM TaskStatus Order by 1
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUser]    Script Date: 13-07-2024 22:13:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetUser] 
	@UserName NVARCHAR(200)
AS
BEGIN
	SELECT Id, UserName, [Password] From [dbo].[User] WHERE UserName = @UserName;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_RegisterUser]    Script Date: 13-07-2024 22:13:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_RegisterUser] (
	@Id UNIQUEIDENTIFIER, 
	@UserName NVARCHAR(200),
	@Password NVARCHAR(200))
AS
BEGIN
	INSERT INTO [User] Values(@Id, @UserName, @Password, GETUTCDATE());
END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateTask]    Script Date: 13-07-2024 22:13:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateTask] (
	@Id UNIQUEIDENTIFIER, 
	@Title NVARCHAR(50),
	@Description NVARCHAR(200),
	@Duedate DATETIME,
	@StatusId SMALLINT
)
AS
BEGIN
	UPDATE [Task] Set Title = @Title, [Description] = @Description, Duedate = @Duedate, StatusId = @StatusId
	WHERE Id = @Id;
END
GO

CREATE DATABASE [Stracker];
GO

USE [Stracker];
GO

CREATE TABLE [dbo].[users](
[userId] int PRIMARY KEY IDENTITY(1, 1),
[firebaseUserId] varchar(28),
[email] varchar(254) NOT NULL,
[firstName] varchar(100) NOT NULL,
[lastName] varchar(100) NOT NULL,
[userTypeId] int NOT NULL,
[isParticipant] bit,
)
GO

CREATE TABLE [dbo].[userType](
[userTypeId] int PRIMARY KEY IDENTITY(1, 1),
[userTypeName]	varchar(32),
)
GO

CREATE TABLE [dbo].[participant](
[participantId] int PRIMARY KEY IDENTITY(1, 1),
[userId] int,
[teamId] int,
[firstName] varchar(100),
[lastName] varchar(100),
[jerseyNumber] int,
[statId] int,
)
GO

CREATE TABLE [dbo].[stats](
[statId] int PRIMARY KEY IDENTITY(1, 1),
[goals] int,
[assist] int,
[saves] int,
[participantId] int,
[teamId] int,
[eventId] int,
)
GO

CREATE TABLE [dbo].[event](
[eventId] int PRIMARY KEY IDENTITY(1, 1),
[teamId] int,
[userId] int,
[participantId] int,
[statId] int,
[eventName] varchar(254),
[startDate] datetime,
[endDate] datetime,
[eventYear] datetime,
[eventTime] time,
[eventTypeId] int,
[oneOfFourSeasonsId] int,
)
GO

CREATE TABLE [dbo].[team](
[teamId] int PRIMARY KEY IDENTITY(1, 1),
[participantId] int,
[eventId] int,
[teamName] varchar(48),
[wins] int,
[losses] int,
)
GO

CREATE TABLE [dbo].[eventType](
[eventTypeId] int PRIMARY KEY IDENTITY(1, 1),
[eventTypeName] varchar(48),
)
GO

CREATE TABLE [dbo].[oneOfFourSeasons](
[oneOfFourSeasonsId] int PRIMARY KEY IDENTITY(1, 1),
[seasonName] varchar(48),
)
GO

INSERT INTO [dbo].[eventType] ([eventTypeName]) VALUES ('League')
INSERT INTO [dbo].[eventType] ([eventTypeName]) VALUES ('Season')
INSERT INTO [dbo].[eventType] ([eventTypeName]) VALUES ('Tournament')
INSERT INTO [dbo].[eventType] ([eventTypeName]) VALUES ('Single Game')
GO

INSERT INTO [dbo].[oneOfFourSeasons] ([seasonName]) VALUES ('Winter')
INSERT INTO [dbo].[oneOfFourSeasons] ([seasonName]) VALUES ('Spring')
INSERT INTO [dbo].[oneOfFourSeasons] ([seasonName]) VALUES ('Summer')
INSERT INTO [dbo].[oneOfFourSeasons] ([seasonName]) VALUES ('Fall')
GO

INSERT INTO [dbo].[userType] ([userTypeName]) VALUES ('USER')
INSERT INTO [dbo].[userType] ([userTypeName]) VALUES ('ADMIN')
INSERT INTO [dbo].[userType] ([userTypeName]) VALUES ('SUPER ADMIN')
GO
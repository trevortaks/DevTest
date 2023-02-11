﻿IF DB_ID('DevTest') IS NULL
BEGIN
	CREATE DATABASE DevTest
END

USE DevTest

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'tblClients' AND TABLE_SCHEMA = 'dbo')
BEGIN
	CREATE TABLE tblClients(
		[ClientID] INT IDENTITY(1,1) PRIMARY KEY,
		[ClientCode] VARCHAR(10) NOT NULL,
		[Name] VARCHAR(50) NOT NULL,
	)
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='tblContacts' AND TABLE_SCHEMA = 'dbo')
BEGIN
	CREATE TABLE tblContacts(
		[ContactID] INT IDENTITY(1,1) PRIMARY KEY,
		[Name] VARCHAR(50) NOT NULL,
		[Surname] VARCHAR(50) NOT NULL,
		[EmailAddress] VARCHAR(100) NOT NULL UNIQUE
	)
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='tblContactClients' AND TABLE_SCHEMA = 'dbo')
BEGIN
	CREATE TABLE tblClientContacts(
		[ClientID] INT NOT NULL,
		[ContactID] INT NOT NULL
	)

	ALTER TABLE tblClientContacts WITH CHECK
		ADD CONSTRAINT FK_ClientID FOREIGN KEY(ClientID)
			REFERENCES tblClients(ClientID)

	ALTER TABLE [dbo].[tblClientContacts]  WITH CHECK
		ADD  CONSTRAINT [FK_ContactID] FOREIGN KEY([ContactID])
			REFERENCES [dbo].[tblContacts] ([ContactID])
END

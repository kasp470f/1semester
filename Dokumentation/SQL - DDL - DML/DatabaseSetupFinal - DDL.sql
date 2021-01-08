DECLARE @dbname nvarchar(128)
SET @dbname = N'ProjectAlpha'

IF (EXISTS (SELECT name 
FROM master.dbo.sysdatabases 
WHERE ('[' + name + ']' = @dbname 
OR name = @dbname)))

PRINT 'Database Already Exists'
GO

CREATE DATABASE ProjectAlpha;
GO

USE [ProjectAlpha]
GO

CREATE TABLE [Municipalities] (
    [M_ID] int  NOT NULL ,
    [M_Name] varchar(200)  NOT NULL ,
    CONSTRAINT [PK_Municipalities] PRIMARY KEY CLUSTERED (
        [M_ID] ASC
    )
)

CREATE TABLE [Restrictions] (
    [R_ID] int  NOT NULL ,
    [R_Text] varchar(200)  NOT NULL ,
    [R_Date] datetime  NOT NULL ,
    CONSTRAINT [PK_Restrictions] PRIMARY KEY CLUSTERED (
        [R_ID] ASC
    )
)

CREATE TABLE [IndustriesRestrictions] (
    [RI_ID] int  NOT NULL ,
    [RI_Text] varchar(200)  NOT NULL ,
    [RI_I_ID] int  NOT NULL ,
    [RI_M_ID] int  NOT NULL ,
    [RI_R_ID] int  NOT NULL ,
    CONSTRAINT [PK_IndustriesRestrictions] PRIMARY KEY CLUSTERED (
        [RI_ID] ASC
    )
)

CREATE TABLE [Industries] (
    [I_ID] int  NOT NULL ,
    [I_Name] varchar(200)  NOT NULL ,
    [I_Code] varchar(30)  NOT NULL ,
    [I_Description] varchar(200)  NOT NULL ,
    CONSTRAINT [PK_Industries] PRIMARY KEY CLUSTERED (
        [I_ID] ASC
    )
)
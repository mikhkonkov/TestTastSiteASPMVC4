CREATE TABLE [dbo].[Roles] (
    [RoleId]       INT          IDENTITY (1, 1) NOT NULL,
    [RoleName] VARCHAR (30) NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

CREATE TABLE [dbo].[Users] (
    [UserId]        INT             IDENTITY (1, 1) NOT NULL,
    [FirstName]     VARCHAR (100)   NOT NULL,
    [LastName]      VARCHAR (100)   NOT NULL,
    [Email]         VARCHAR (100)   NOT NULL,
    [ImageData]     VARBINARY (MAX) NULL,
    [ImageMimeType] VARCHAR (50)    NULL,
    [Cookies]       VARCHAR (100)   NULL,
    [Password]      VARCHAR (100)   NOT NULL,
    [IsActive]      BIT             NOT NULL,
    [RoleId]        INT             NOT NULL,
    [DateBirth]     DATE            NOT NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_User_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId])
);
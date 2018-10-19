CREATE TABLE [dbo].[HttpVisit] (
    [Id] [uniqueidentifier] NOT NULL,
    [IsAuthenticated] [bit] NOT NULL,
    [UserName] [nvarchar](50),
    [AuthenticationType] [nvarchar](50),
    [Url] [nvarchar](500),
    [Time] [datetime] NOT NULL,
    [Method] [nvarchar](10),
    [StatusCode] [nvarchar](10),
    [Expires] [bigint] NOT NULL,
    [Key] [nvarchar](50),
    [Description] [nvarchar](100),
    CONSTRAINT [PK_dbo.SqlHttpVisitTables] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[HttpVisitInfo] (
    [Id] [uniqueidentifier] NOT NULL,
    [HttpVisitId] [uniqueidentifier] NOT NULL,
    [Key] [nvarchar](100),
    [Value] text,
    [Time] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.HttpVisitInfo] PRIMARY KEY ([Id])
)

CREATE INDEX IX_HttpVisit_Time ON HttpVisit ([Time]) INCLUDE ([UserName],[Url],[Method],[StatusCode],[Expires],[Key],[Description])
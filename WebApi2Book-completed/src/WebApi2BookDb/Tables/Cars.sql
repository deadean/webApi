CREATE TABLE [dbo].[Car](
	[id] BIGINT  IDENTITY (1, 1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
    CONSTRAINT [PK_Car] PRIMARY KEY ([id])
);
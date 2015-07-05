CREATE TABLE [dbo].[Departamento]
(
	[Id] INT NOT NULL IDENTITY, 
    [Descricao] NCHAR(40) NOT NULL,
	CONSTRAINT [PK_Departamento] PRIMARY KEY ([Id])
)

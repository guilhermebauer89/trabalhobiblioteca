CREATE TABLE [dbo].[Curso]
(
	[Id] INT NOT NULL IDENTITY, 
    [IdUnidade] INT NOT NULL, 
	[IdDepartamento] INT NOT NULL, 
	[Nome] NCHAR(30) NOT NULL,
    CONSTRAINT [FK_Curso_Unidade]  FOREIGN KEY ([IdUnidade]) REFERENCES [Unidade]([Id]),
	CONSTRAINT [FK_Curso_Departamento]  FOREIGN KEY ([IdDepartamento]) REFERENCES [Departamento]([Id]),
	CONSTRAINT [PK_Curso] PRIMARY KEY ([Id])

)

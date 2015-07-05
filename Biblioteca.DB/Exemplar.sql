CREATE TABLE [dbo].[Exemplar]
(
	[Id] INT NOT NULL IDENTITY, 
    [IdLivro] INT NOT NULL, 
	[IdUnidade] INT NOT NULL, 
	[Edicao] NCHAR(30) NOT NULL, 
	[AnoEdicao] NUMERIC(4,0) NOT NULL, 
	CONSTRAINT [PK_Exemplar] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Exemplar_Livro]  FOREIGN KEY ([IdLivro]) REFERENCES [Livro]([Id]),
	CONSTRAINT [FK_Exemplar_Unidade]  FOREIGN KEY ([IdUnidade]) REFERENCES [Unidade]([Id]),

	
)

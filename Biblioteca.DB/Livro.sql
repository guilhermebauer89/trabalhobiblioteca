CREATE TABLE [dbo].[Livro]
(
	[Id] INT NOT NULL IDENTITY, 
    [IdAutor] INT NOT NULL, 
	[IdEditora] INT NOT NULL, 
	[Titulo] NCHAR(30) NOT NULL,
	
	CONSTRAINT [PK_Livro] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Livro_Autor]  FOREIGN KEY ([IdAutor]) REFERENCES [Autor]([Id]),
	CONSTRAINT [FK_Livro_Editora] FOREIGN KEY ([IdEditora]) REFERENCES [Editora]([Id]),
	CONSTRAINT [UNIQUE_Livro_Titulo] UNIQUE ([Titulo]),
)

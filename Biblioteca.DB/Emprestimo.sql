CREATE TABLE [dbo].[Emprestimo]
(
	[IdEmprestimo] INT IDENTITY NOT NULL, 
	[MatriculaUsuario] INT NOT NULL, 
    [IdExemplar] INT NOT NULL, 
    [IdLivro] INT NOT NULL, 
	[DataPrevista] DateTime NOT NULL, 
	[DataDevolucao] DateTime, 
	[DataEmprestimo] DateTime default current_timestamp, 
	[ValorMulta] money ,
	CONSTRAINT [PK_Emprestimo] PRIMARY KEY ([IdEmprestimo]),
	CONSTRAINT [FK_Emprestimo_Exemplar]  FOREIGN KEY ([IdExemplar]) REFERENCES [Exemplar]([Id]),
	CONSTRAINT [FK_Emprestimo_Usuario]  FOREIGN KEY ([MatriculaUsuario]) REFERENCES [Usuario]([Matricula])
)

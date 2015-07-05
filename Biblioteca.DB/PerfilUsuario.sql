CREATE TABLE [dbo].[PerfilUsuario]
(
	[Id] INT NOT NULL IDENTITY,
	[QuantidadeLocacoes] INT NOT NULL,
	[QuantidadeCursos] INT NOT NULL,
	[ValorMultaDiario] MONEY NOT NULL DEFAULT 0,
	[Descricao] NCHAR(50) NOT NULL DEFAULT 0,
	[PodeSerCoordenador] BIT NOT NULL,
	CONSTRAINT [PK_PerfilUsuario] PRIMARY KEY ([Id])
)

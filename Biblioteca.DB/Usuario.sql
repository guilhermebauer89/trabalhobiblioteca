CREATE TABLE [dbo].[Usuario]
(
	[Matricula] INT NOT NULL, 
    [IdPerfilUsuario] INT NOT NULL, 
	[Nome] NCHAR(50) NOT NULL,
	CONSTRAINT [FK_Usuario_PerfilUsuario]  FOREIGN KEY ([IdPerfilUsuario]) REFERENCES [PerfilUsuario]([Id]),
	CONSTRAINT [PK_Usuario] PRIMARY KEY ([Matricula])
)

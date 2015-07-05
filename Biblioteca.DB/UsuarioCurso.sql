CREATE TABLE [dbo].[UsuarioCurso]
(
	[IdCurso] INT NOT NULL, 
    [MatriculaUsuario] INT NOT NULL, 
	[Coordenador] BIT NOT NULL,
	CONSTRAINT [PK_UsuarioCurso] PRIMARY KEY ([IdCurso], [MatriculaUsuario]),
	CONSTRAINT [FK_UsuarioCurso_Curso]  FOREIGN KEY ([IdCurso]) REFERENCES [Curso]([Id]),
	CONSTRAINT [FK_UsuarioCurso_Usuario]  FOREIGN KEY ([MatriculaUsuario]) REFERENCES [Usuario]([Matricula]),
	
)

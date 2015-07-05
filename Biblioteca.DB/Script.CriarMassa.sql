MERGE INTO Departamento AS Target 
USING (VALUES 
        ('Exatas'), 
        ('Humanas'), 
        ('Biológicas')
) 
AS Source (Descricao) 
ON Target.Descricao = Source.Descricao
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Descricao) 
VALUES (Descricao);

MERGE INTO Autor AS Target 
USING (VALUES 
        (1, 'Yoda'), 
        (2, 'Ryu'), 
        (3, 'Vader Boladão')
) 
AS Source (Id, Nome) 
ON Target.Id = Source.Id
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, Nome) 
VALUES (Id, Nome);

MERGE INTO Editora AS Target 
USING (VALUES 
        (1, 'República'), 
        (2, 'Haduken'), 
        (3, 'Estrela da Morte S.A.')
) 
AS Source (Id, Nome) 
ON Target.Id = Source.Id
WHEN NOT MATCHED BY TARGET THEN 
INSERT (Id, Nome) 
VALUES (Id, Nome);
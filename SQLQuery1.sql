CREATE TABLE Heros (
Id INT PRIMARY KEY IDENTITY,
[Name] VARCHAR(255),
FirstName VARCHAR(255),
LastName VARCHAR(255),
Place VARCHAR(255)
);

INSERT INTO Heros ( [Name], FirstName, LastName, Place)
VALUES ( 'Spider-Man', 'Peter', 'Parker', 'New Yourk City');


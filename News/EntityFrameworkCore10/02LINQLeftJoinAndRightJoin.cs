using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatsNewInDotNET10.EntityFrameworkCore10.LINQLeftJoinAndRightJoin
{
	// Cosa cambia:
	// - Con .NET 10 arrivano operatori LINQ di prima classe LeftJoin e RightJoin.EF Core 10 li riconosce e li traduce rispettivamente in LEFT JOIN e RIGHT JOIN,
	//   evitando i vecchi workaround(GroupJoin + DefaultIfEmpty + SelectMany).

	[Table("Students")]
	public class Student
	{
		public int ID { get; set; }
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;
		public int? DepartmentID { get; set; }
	}

	[Table("Departments")]
	public class Department
	{
		public int ID { get; set; }
		public string Name { get; set; } = default!;
	}

	public class StudentContext : DbContext
	{
		public DbSet<Student> Students { get; set; } = default!;
		public DbSet<Department> Departments { get; set; } = default!;
		protected override void OnConfiguring(DbContextOptionsBuilder options)
			=> options.UseSqlServer("Server=SLIM5-TANOP\\SQL2025;Database=WhatsNewInDotNET10;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");
	}

	// Nota:
	// - la query-syntax di C# (from ... join ...) non ha ancora una sintassi equivalente “nuova” per questi operatori; 
	//   li usi in method-syntax.

	//-- Creazione tabella Departments
	//CREATE TABLE Departments(
	//	ID INT IDENTITY(1,1) NOT NULL,
	//	Name NVARCHAR(100) NOT NULL,
	//	CONSTRAINT PK_Departments PRIMARY KEY(ID)
	//);
	//GO

	//-- Creazione tabella Students
	//CREATE TABLE Students(
	//	ID INT IDENTITY(1,1) NOT NULL,
	//	FirstName NVARCHAR(100) NOT NULL,
	//	LastName NVARCHAR(100) NOT NULL,
	//	DepartmentID INT NULL,
	//	CONSTRAINT PK_Students PRIMARY KEY(ID),
	//    CONSTRAINT FK_Students_Departments
	//		FOREIGN KEY(DepartmentID)
	//		REFERENCES Departments(ID)
	//		ON DELETE SET NULL
	//);
	//GO

	//-- Popolamento tabella Departments
	//INSERT INTO Departments(Name)
	//VALUES
	//	('Computer Science'),
	//    ('Mathematics'),
	//    ('Physics'),
	//    ('Literature');
	//GO

	//-- Popolamento tabella Students(10 record)
	//INSERT INTO Students(FirstName, LastName, DepartmentID)
	//VALUES
	//	('Mario', 'Rossi', 1),
	//    ('Luigi', 'Bianchi', 1),
	//    ('Giulia', 'Verdi', 2),
	//    ('Francesca', 'Neri', 2),
	//    ('Luca', 'Ferrari', 3),
	//    ('Marco', 'Russo', 3),
	//    ('Elena', 'Conti', 4),
	//    ('Sara', 'Gallo', 4),
	//    ('Paolo', 'Costa', NULL),   -- studente senza dipartimento
	//	('Anna', 'Romano', 1);
	//	GO
}

CREATE DATABASE Artsofte

USE Artsofte


/*Create tables*/

CREATE TABLE Departments
(
	Id INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(40) NOT NULL,
	Floor INT DEFAULT 1,
)
CREATE TABLE Languages
(
	Id INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(15) NOT NULL,
)
CREATE TABLE Employees
(
	Id INT IDENTITY PRIMARY KEY,
	Name NVARCHAR(20) NOT NULL,
	Surname NVARCHAR(20) NOT NULL,
	Age INT NOT NULL,
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id),
	LanguageId INT FOREIGN KEY REFERENCES Languages(Id)
)


/*Example data*/

INSERT INTO Departments
VALUES
(1,16), (2,1), (3,2), (4,7)

INSERT INTO Languages
VALUES
('C#'), ('Python'), ('C++'), ('Java'), ('JavaScript'), ('C')


/*Create procedures*/

CREATE PROCEDURE GetEmployees 
AS
BEGIN
SELECT * FROM Employees
END

CREATE PROCEDURE GetEmployeesById @Id INT
AS
BEGIN
IF(@Id IS NOT NULL)
	BEGIN
	SELECT * FROM Employees
	WHERE Employees.Id = Id
	END
END

CREATE PROCEDURE DeleteEmployeeById @Id INT
AS
BEGIN
IF(@Id IS NOT NULL)
	BEGIN
	DELETE FROM Employees
	WHERE Employees.Id = Id
	END
END

CREATE PROCEDURE AddEmployee
@Name NVARCHAR(20), @Surname NVARCHAR(20), @Age INT, @LanguageId INT, @DepartmentId INT
AS
BEGIN
IF(@Name IS NOT NULL AND LEN(@Name) > 2 AND @Surname IS NOT NULL AND LEN(@Surname) > 2 
AND @Age IS NOT NULL AND @Age > 18 AND @Age < 115 AND @LanguageId IS NOT NULL AND @DepartmentId IS NOT NULL 
AND (SELECT COUNT(*) FROM Languages WHERE Languages.Id = @LanguageId) != 0
AND (SELECT COUNT(*) FROM Departments WHERE Departments.Id = @DepartmentId) != 0)
	BEGIN
		INSERT INTO Employees
		VALUES
		(@Name, @Surname, @Age, @DepartmentId, @LanguageId)
		RETURN 1;
	END
ELSE
	RETURN 0;
END

CREATE PROCEDURE EditEmployee
@Id INT,@Name NVARCHAR(20), @Surname NVARCHAR(20), @Age INT, @LanguageId INT, @DepartmentId INT
AS
BEGIN
IF(@Id IS NOT NULL AND @Id > 0 AND @Name IS NOT NULL AND LEN(@Name) > 2 AND @Surname IS NOT NULL AND LEN(@Surname) > 2 
AND @Age IS NOT NULL AND @Age > 18 AND @Age < 115 AND @LanguageId IS NOT NULL AND @DepartmentId IS NOT NULL 
AND (SELECT COUNT(*) FROM Languages WHERE Languages.Id = @LanguageId) != 0
AND (SELECT COUNT(*) FROM Departments WHERE Departments.Id = @DepartmentId) != 0)
	BEGIN
		UPDATE Employees
		SET Name = @Name, Surname = @Surname, Age = @Age, DepartmentId = @DepartmentId, LanguageId = @LanguageId
		WHERE Employees.Id = @Id
		RETURN 1;
	END
ELSE
	RETURN 0;
END


CREATE PROCEDURE GetLanguagesById @LanguageId INT
AS
BEGIN
IF(@LanguageId IS NOT NULL)
	BEGIN
	SELECT * FROM Languages AS l
	WHERE l.Id = @LanguageId
	END
END

CREATE PROCEDURE GetDepartmentsById @DepartmentId INT
AS
BEGIN
IF(@DepartmentId IS NOT NULL)
	BEGIN
	SELECT * FROM Departments AS d
	WHERE d.Id = @DepartmentId
	END
END


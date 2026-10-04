-- created this database for the ISS1201 mini project assignment

CREATE DATABASE HIT_CHP_DB;
GO

USE HIT_CHP_DB;
GO

CREATE TABLE CHPUsers (
	StudentID INT  NOT NULL IDENTITY PRIMARY KEY,
	LastName VARCHAR(30),
	FirstName VARCHAR(25),
	DateOfBirth DATE,
	Gender CHAR(1),
	NationalID NCHAR(13) UNIQUE,
	RegNumber NCHAR(8) UNIQUE,
	Part INT,
	School CHAR(4),
	Course VARCHAR(4),
	Phone NVARCHAR(12),
	Email NVARCHAR(50),
	HITmail NVARCHAR(50),
	Address NVARCHAR(150)
);
GO

SELECT * FROM CHPUsers;
GO


CREATE TABLE PersonalDetails (
	StudentID INT,
	LastName VARCHAR(30),
	FirstName VARCHAR(25),
	DateOfBirth DATE,
	Gender CHAR(1),
	NationalID NCHAR(13) UNIQUE,
	Phone NVARCHAR(12),
	Email NVARCHAR(50),
	Address NVARCHAR(150)
);
GO

ALTER TABLE PersonalDetails
	ADD CONSTRAINT FK_CHPUsers_PersonalDetails FOREIGN KEY (StudentID)
	REFERENCES CHPUsers(StudentID)
	ON DELETE CASCADE
	ON UPDATE CASCADE;
GO

SELECT * FROM PersonalDetails;
GO

-------------------------------------------------------------------------------------------------------------------------

CREATE TABLE Schools (
	id INT  NOT NULL IDENTITY PRIMARY KEY,
	School CHAR(4),
	SchoolSIET CHAR(4),
	SchoolSIST CHAR(4),
	SchoolSAHS CHAR(4),
	SchoolSBMS CHAR(4),
	SchoolSIIT CHAR(4),
	Part INT
);
GO

INSERT INTO Schools (School, SchoolSIET, SchoolSIST, SchoolSAHS, SchoolSBMS, SchoolSIIT, Part)
VALUES ('SIET', 'HBME', 'HISA', 'AHPH', 'HIFE', 'HFPT', 1),
       ('SIST', 'CPSE', 'HIIT', 'AHDR', 'HIEC', 'BioT', 2),
	   ('SAHS', 'HIEE', 'HISE', 'AHTR', 'HFAA', '', 3),
	   ('SBMS', 'HIME', 'HICS', '', 'ISSM', '', 4),
	   ('SIIT', 'HEPT', '', '', '', '', ''),
	   ('', 'HEMT', '', '', '', '', '')
GO

SELECT * FROM Schools;
GO

-------------------------------------------------------------------------------------------------------------------------

CREATE TABLE SIETdepartment (
	StudentID INT,
	LastName VARCHAR(30),
	FirstName VARCHAR(25),
	RegNumber NCHAR(8) UNIQUE,
	Part INT,
	Course VARCHAR(4),
	Phone NVARCHAR(12),
	Email NVARCHAR(50),
	HITmail NVARCHAR(50)
);
GO

ALTER TABLE SIETdepartment
	ADD CONSTRAINT FK_CHPUsers_SIETdepartment FOREIGN KEY (StudentID)
	REFERENCES CHPUsers(StudentID)
	ON DELETE CASCADE
	ON UPDATE CASCADE;
GO

SELECT * FROM SIETdepartment;
GO

CREATE TABLE SISTdepartment (
	StudentID INT,
	LastName VARCHAR(30),
	FirstName VARCHAR(25),
	RegNumber NCHAR(8) UNIQUE,
	Part INT,
	Course VARCHAR(4),
	Phone NVARCHAR(12),
	Email NVARCHAR(50),
	HITmail NVARCHAR(50)
);
GO

ALTER TABLE SISTdepartment
	ADD CONSTRAINT FK_CHPUsers_SISTdepartment FOREIGN KEY (StudentID)
	REFERENCES CHPUsers(StudentID)
	ON DELETE CASCADE
	ON UPDATE CASCADE;
GO

SELECT * FROM SISTdepartment;
GO

CREATE TABLE SAHSdepartment (
	StudentID INT,
	LastName VARCHAR(30),
	FirstName VARCHAR(25),
	RegNumber NCHAR(8) UNIQUE,
	Part INT,
	Course VARCHAR(4),
	Phone NVARCHAR(12),
	Email NVARCHAR(50),
	HITmail NVARCHAR(50)
);
GO

ALTER TABLE SAHSdepartment
	ADD CONSTRAINT FK_CHPUsers_SAHSdepartment FOREIGN KEY (StudentID)
	REFERENCES CHPUsers(StudentID)
	ON DELETE CASCADE
	ON UPDATE CASCADE;
GO

SELECT * FROM SAHSdepartment;
GO

CREATE TABLE SBMSdepartment (
	StudentID INT,
	LastName VARCHAR(30),
	FirstName VARCHAR(25),
	RegNumber NCHAR(8) UNIQUE,
	Part INT,
	Course VARCHAR(4),
	Phone NVARCHAR(12),
	Email NVARCHAR(50),
	HITmail NVARCHAR(50)
);
GO

ALTER TABLE SBMSdepartment
	ADD CONSTRAINT FK_CHPUsers_SBMSdepartment FOREIGN KEY (StudentID)
	REFERENCES CHPUsers(StudentID)
	ON DELETE CASCADE
	ON UPDATE CASCADE;
GO

SELECT * FROM SBMSdepartment;
GO

CREATE TABLE SIITdepartment (
	StudentID INT,
	LastName VARCHAR(30),
	FirstName VARCHAR(25),
	RegNumber NCHAR(8) UNIQUE,
	Part INT,
	Course VARCHAR(4),
	Phone NVARCHAR(12),
	Email NVARCHAR(50),
	HITmail NVARCHAR(50)
);
GO

ALTER TABLE SIITdepartment
	ADD CONSTRAINT FK_CHPUsers_SIITdepartment FOREIGN KEY (StudentID)
	REFERENCES CHPUsers(StudentID)
	ON DELETE CASCADE
	ON UPDATE CASCADE;
GO

SELECT * FROM SIITdepartment;
GO

-------------------------------------------------------------------------------------------------------------------------

CREATE TABLE StudentLogin (
	StudentID INT,
	Username NCHAR(8) UNIQUE,
	Password NVARCHAR(25)
);
GO

ALTER TABLE StudentLogin
	ADD CONSTRAINT FK_CHPUsers_StudentLogin FOREIGN KEY (StudentID)
	REFERENCES CHPUsers(StudentID)
	ON DELETE CASCADE
	ON UPDATE CASCADE;
GO

SELECT * FROM StudentLogin;
GO

CREATE TABLE AdminLogin (
	AdminID INT  NOT NULL IDENTITY PRIMARY KEY,
	Username NCHAR(25) UNIQUE,
	Password NVARCHAR(25)
);
GO

INSERT INTO AdminLogin (Username, Password)
VALUES ('admin', '12345678'),
	   ('k1ngDev!', 'devHackerX@hit'),
	   ('Tadiwaaaa', 'Tasima04'),
	   ('Mavuchi', 'VPGuru#01'),
	   ('Mudawarima', '#01DBTeach'),
	   ('CHPdevs', 'C#%code%'),
	   ('DBadmin', 'SQLhero3s');
GO

SELECT * FROM AdminLogin;
GO

-------------------------------------------------------------------------------------------------------------------------

CREATE TABLE Applications (
	StudentID INT,
	ApplicationID INT NOT NULL IDENTITY PRIMARY KEY,
	LastName VARCHAR(30),
	RegNumber NCHAR(8) UNIQUE,
	Hostel INT,
	Room1st VARCHAR(3),
	Room2nd VARCHAR(3),
	Reason VARCHAR(350),
	Status INT
);
GO

ALTER TABLE Applications
	ADD CONSTRAINT FK_CHPUsers_Applications FOREIGN KEY (StudentID)
	REFERENCES CHPUsers(StudentID)
	ON DELETE CASCADE
	ON UPDATE CASCADE;
GO

SELECT * FROM Applications;
GO

CREATE TABLE Mate (
	StudentID INT,
	ApplicationID INT,
	LastName VARCHAR(30),
	RegNumber NCHAR(8) UNIQUE,
	MateName VARCHAR (60),
	MateRegNum NCHAR(8),
	MateCouse VARCHAR(4),
	Status INT
);
GO

ALTER TABLE Mate
	ADD CONSTRAINT FK_Applications_Mate FOREIGN KEY (ApplicationID)
	REFERENCES Applications(ApplicationID)
	ON DELETE CASCADE
	ON UPDATE CASCADE;
GO

SELECT * FROM Mate;
GO

CREATE TABLE Approved (
	ApplicationID INT,
	LastName VARCHAR(30),
	RegNumber NCHAR(8) UNIQUE,
	Hostel INT,
	Room VARCHAR(3),
	RoomMate VARCHAR(60),
	MateRegNum NCHAR(8)
);
GO

ALTER TABLE Approved
	ADD CONSTRAINT FK_Applications_Approved FOREIGN KEY (ApplicationID)
	REFERENCES Applications(ApplicationID)
	ON DELETE CASCADE
	ON UPDATE CASCADE;
GO

SELECT * FROM Approved;
GO

CREATE TABLE Rejected (
	ApplicationID INT,
	LastName VARCHAR(30),
	RegNumber NCHAR(8) UNIQUE,
	Hostel INT,
	Room1st VARCHAR(3),
	Room2nd VARCHAR(3),
	Reason VARCHAR(350)
);
GO

ALTER TABLE Rejected
	ADD CONSTRAINT FK_Applications_Rejected FOREIGN KEY (ApplicationID)
	REFERENCES Applications(ApplicationID)
	ON DELETE CASCADE
	ON UPDATE CASCADE;
GO

SELECT * FROM Rejected;
GO

-------------------------------------------------------------------------------------------------------------------------

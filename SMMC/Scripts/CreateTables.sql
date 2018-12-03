IF OBJECT_ID('PerformancesStudent') IS NOT NULL DROP TABLE PerformancesStudent
IF OBJECT_ID('Performances') IS NOT NULL DROP TABLE Performances
IF OBJECT_ID('Venues') IS NOT NULL DROP TABLE Venues
IF OBJECT_ID('SheetMusicTutors') IS NOT NULL DROP TABLE SheetMusicTutors
IF OBJECT_ID('SheetMusicInstruments') IS NOT NULL DROP TABLE SheetMusicInstruments
IF OBJECT_ID('SheetMusic') IS NOT NULL DROP TABLE SheetMusic
IF OBJECT_ID('LessonStudents') IS NOT NULL DROP TABLE LessonStudents
IF OBJECT_ID('LessonTutors') IS NOT NULL DROP TABLE LessonTutors
IF OBJECT_ID('Lessons') IS NOT NULL DROP TABLE Lessons
IF OBJECT_ID('Tutors') IS NOT NULL DROP TABLE Tutors
IF OBJECT_ID('TutorType') IS NOT NULL DROP TABLE TutorType
IF OBJECT_ID('Certifications') IS NOT NULL DROP TABLE Certifications
IF OBJECT_ID('StudentInstrument') IS NOT NULL DROP TABLE StudentInstrument
IF OBJECT_ID('Ensembles') IS NOT NULL DROP TABLE Ensembles
IF OBJECT_ID('Parents') IS NOT NULL DROP TABLE Parents
IF OBJECT_ID('Payments') IS NOT NULL DROP TABLE Payments
IF OBJECT_ID('Student') IS NOT NULL DROP TABLE Student
IF OBJECT_ID('PersonAddress') IS NOT NULL DROP TABLE PersonAddress
IF OBJECT_ID('Address') IS NOT NULL DROP TABLE Address
IF OBJECT_ID('Person') IS NOT NULL DROP TABLE Person
IF OBJECT_ID('Instruments') IS NOT NULL DROP TABLE Instruments
GO

CREATE TABLE Instruments(
	Instrument		VARCHAR(100),
	StudentFee		DECIMAL(10,2),
	OpenFee			DECIMAL(10,2),
	HireFee			DECIMAL(10,2),
	Comments		VARCHAR(255),
	CONSTRAINT PK_Instruments PRIMARY KEY(Instrument)
)
GO

CREATE TABLE Person(
	ID				int				IDENTITY,
	FirstName		VARCHAR(50)		NOT NULL,
	LastName		VARCHAR(50)		NOT NULL,
	DateOfBirth		Date			NOT NULL,
	PhoneNo			VARCHAR(20)		NOT NULL,
	CONSTRAINT PK_Person PRIMARY KEY(ID)
)
GO

CREATE TABLE Address(
	ID				int				IDENTITY,
	Street			VARCHAR(255)	NOT NULL,
	Suburb			VARCHAR(255)	NOT NULL,
	City			VARCHAR(255)	NOT NULL,
	Postcode		VARCHAR(8)		NOT NULL,
	CONSTRAINT PK_Address PRIMARY KEY(ID)
)
GO

CREATE TABLE PersonAddress(
	PersonID		int		NOT NULL,
	AddressID		int		NOT NULL,
	FOREIGN KEY (PersonID) REFERENCES Person(ID)
	ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (AddressID) REFERENCES Address(ID)
	ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT PK_PersonAddress PRIMARY KEY(PersonID)
)
GO

CREATE TABLE Student(
	PersonID		int 	NOT NULL,
	OpenDivision	bit		DEFAULT 0,
	FOREIGN KEY (PersonID) REFERENCES Person(ID)
	ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT PK_Student PRIMARY KEY(PersonID)
)
GO

CREATE TABLE Payments(
	ID				int				IDENTITY,
	StudentID		int 			NOT NULL,
	DatePaid		Date			DEFAULT GETDATE(),
	Amount			DECIMAL(10,2)	NOT NULL,
	FOREIGN KEY (StudentID) REFERENCES Student(PersonID)
	ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT PK_Payments PRIMARY KEY(ID)
)
GO

CREATE TABLE Parents(
	StudentID		int				NOT NULL,
	ParentID		int				NOT NULL,
	FOREIGN KEY (StudentID) REFERENCES Student(PersonID)
	ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (ParentID) REFERENCES Person(ID),
	CONSTRAINT PK_StudentParent PRIMARY KEY(StudentID, ParentID)
)
GO

CREATE TABLE Ensembles(
	Level			int 			NOT NULL,
	TypeID			VARCHAR(30)		NOT NULL,
	CONSTRAINT PK_Ensembles PRIMARY KEY(Level)
)
GO

CREATE TABLE StudentInstrument(
	StudentID		int				NOT NULL,
	InstrumentID	VARCHAR(100)	NOT NULL,
	Hire			bit				DEFAULT (0),
	FOREIGN KEY (InstrumentID) REFERENCES Instruments(Instrument)
	ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (StudentID) REFERENCES Student(PersonID)
	ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT PK_StudentInstrument PRIMARY KEY(StudentID, InstrumentID)
)
GO

CREATE TABLE Certifications(
	PersonID			int				NOT NULL,
	InstrumentID		VARCHAR(100)	NOT NULL,
	CertificationLevel	int				DEFAULT (0),
	ATCL				bit				DEFAULT (0),
	FOREIGN KEY (PersonID) REFERENCES Person(ID)
	ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (InstrumentID) REFERENCES Instruments(Instrument)
	ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (CertificationLevel) REFERENCES Ensembles(Level)
	ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT PK_Certifications PRIMARY KEY(PersonID, InstrumentID)
)
GO

CREATE TABLE TutorType(
	ID				VARCHAR(20) 	NOT NULL,
	HourlyRate		DECIMAL(10,2) 	NOT NULL,
	CONSTRAINT PK_TutorType PRIMARY KEY(ID)
)
GO

CREATE TABLE Tutors(
	ID				int				IDENTITY,
	PersonID		int				NOT NULL,
	InstrumentID	VARCHAR(100)	NOT NULL,
	Type			VARCHAR(20)		NOT NULL,
	FOREIGN KEY (PersonID) REFERENCES Person(ID),
	FOREIGN KEY (InstrumentID) REFERENCES Instruments(Instrument),
	FOREIGN KEY (Type) REFERENCES TutorType(ID),
	CONSTRAINT PK_Tutors PRIMARY KEY(ID)
)
GO

CREATE TABLE Lessons(
	ID				int				IDENTITY,
	Level			int				NOT NULL,
	InstrumentID	VARCHAR(100)	NOT NULL,
	Time			time			NOT NULL,
	FOREIGN KEY (InstrumentID) REFERENCES Instruments(Instrument)
	ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT PK_Lessons PRIMARY KEY(ID)
)
GO

CREATE TABLE LessonTutors(
	LessonID	int			NOT NULL,
	TutorID		int			NOT NULL,
	FOREIGN KEY (LessonID) REFERENCES Lessons(ID)
	ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (TutorID) REFERENCES Tutors(ID),
	CONSTRAINT PK_LessonTutors PRIMARY KEY(LessonID)
)
GO

CREATE TABLE LessonStudents(
	LessonID				int			NOT NULL,
	StudentID				int			NOT NULL,
	FOREIGN KEY (LessonID) REFERENCES Lessons(ID)
	ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (StudentID) REFERENCES Student(PersonID)
	ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT PK_LessonStudents PRIMARY KEY(LessonID,StudentID)
)
GO

CREATE TABLE SheetMusic(
	ID						int				IDENTITY,
	Name					VARCHAR(100) 	NOT NULL,
	CopiesAllowed			int				DEFAULT 0,
	DistrubitedCopies		int 			DEFAULT 0,
	Orchestral				bit				DEFAULT (0),
	CONSTRAINT PK_SheetMusic PRIMARY KEY(ID)
)
GO

CREATE TABLE SheetMusicInstruments(
	SheetMusicID			int 			NOT NULL,
	InstrumentID			VARCHAR(100)	NOT NULL,
	FOREIGN KEY (SheetMusicID) REFERENCES SheetMusic(ID)
	ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (InstrumentID) REFERENCES Instruments(Instrument),
	CONSTRAINT PK_SheetMusicInstruments PRIMARY KEY(SheetMusicID, InstrumentID)
)
GO

CREATE TABLE SheetMusicTutors(
	SheetMusicID			int			NOT NULL,
	TutorID					int			NOT NULL,
	GivenCopies				int			NOT NULL,
	GivenToStudents			int			NOT NULL,
	FOREIGN KEY (SheetMusicID) REFERENCES SheetMusic(ID)
	ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (TutorID) REFERENCES Tutors(ID),
	CONSTRAINT PK_SheetMusicTutors PRIMARY KEY(SheetMusicID, TutorID)
)
GO

CREATE TABLE Venues(
	ID						int				IDENTITY,
	Name					VARCHAR(100)	NOT NULL,
	Street					VARCHAR(255)	NOT NULL,
	Suburb					VARCHAR(255)	NOT NULL,
	City					VARCHAR(255)	NOT NULL,
	Postcode				VARCHAR(8)		NOT NULL,
	Phone					VARCHAR(20)		NOT NULL,
	CONSTRAINT PK_Venues PRIMARY KEY(ID)
)
GO

CREATE TABLE Performances(
	ID						int			IDENTITY,
	VenueID					int,
	Date					date,
	Time					time,
	Major					bit,
	FOREIGN KEY (VenueID) REFERENCES Venues(ID)
	ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT PK_Performances PRIMARY KEY(ID)
)
GO

CREATE TABLE PerformancesStudent(
	PerformancesID			int,
	StudentID				int,
	InstrumentID			VARCHAR(100),
	FOREIGN KEY (PerformancesID) REFERENCES Performances(ID)
	ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY (StudentID, InstrumentID) REFERENCES StudentInstrument(StudentID, InstrumentID)
	ON UPDATE CASCADE ON DELETE CASCADE,
	CONSTRAINT PK_PerformancesStudent PRIMARY KEY(PerformancesID, StudentID)
)
GO



CREATE TRIGGER [DELETE_Instruments]
	ON Instruments
	INSTEAD OF DELETE
AS
BEGIN
	DELETE FROM Tutors WHERE InstrumentID IN (SELECT Instrument FROM DELETED)
	DELETE FROM Instruments WHERE Instrument IN (SELECT Instrument FROM DELETED)
END
GO

CREATE TRIGGER [DELETE_TutorTypes]
	ON TutorType
	INSTEAD OF DELETE
AS
BEGIN
	DELETE FROM Tutors WHERE Type IN (SELECT ID FROM DELETED)
	DELETE FROM TutorType WHERE ID IN (SELECT ID FROM DELETED)
END
GO

CREATE TRIGGER [DELETE_Tutors]
	ON Tutors
	INSTEAD OF DELETE
AS
BEGIN
	DELETE lt FROM LessonTutors lt WHERE TutorID IN (SELECT ID FROM DELETED)
	DELETE smt FROM SheetMusicTutors smt WHERE TutorID IN (SELECT ID FROM DELETED)
	DELETE Tutors WHERE ID IN (SELECT ID FROM DELETED)
END
GO

CREATE TRIGGER [DELETE_Person]
	ON Person
	INSTEAD OF DELETE
AS
BEGIN
	DELETE FROM Parents WHERE ParentID IN (SELECT ID FROM DELETED)
	DELETE smt FROM SheetMusicTutors smt INNER JOIN Tutors t ON smt.TutorID=t.ID WHERE PersonID IN (SELECT ID FROM DELETED)
	DELETE lt FROM LessonTutors lt INNER JOIN Tutors t ON lt.TutorID=t.ID WHERE PersonID IN (SELECT ID FROM DELETED)
	DELETE FROM Tutors WHERE PersonID IN (SELECT ID FROM DELETED)
	DELETE FROM Person WHERE ID IN (SELECT ID FROM DELETED)
END
GO

CREATE TRIGGER [INSERT_Tutors]
	ON Tutors
	INSTEAD OF INSERT
AS
DECLARE @person INT
DECLARE @instrument VARCHAR(100)
DECLARE @type VARCHAR(20)
SELECT @person=i.PersonID FROM inserted i
SELECT @instrument=i.InstrumentID FROM inserted i
SELECT @type=i.Type FROM inserted i
BEGIN
	IF EXISTS (SELECT * FROM Certifications WHERE PersonID = @person AND InstrumentID = @instrument AND CertificationLevel = 8)
	IF NOT EXISTS (SELECT * FROM Tutors WHERE PersonID = @person AND InstrumentID = @instrument)
	BEGIN
		IF @type = 'Head'
		BEGIN
			IF NOT EXISTS (SELECT * FROM Tutors WHERE Type='Head' AND InstrumentID = @instrument)
			INSERT INTO Tutors VALUES(@person, @instrument, @type)
		END
		ELSE
		BEGIN
			INSERT INTO Tutors VALUES(@person, @instrument, @type)
		END
	END
END
GO

CREATE TRIGGER [INSERT_LessonTutors]
	ON LessonTutors
	INSTEAD OF INSERT
AS
DECLARE @tutorID INT
DECLARE @lessonID INT
DECLARE @lessonlevel INT
DECLARE @type VARCHAR(20)
DECLARE @instrument VARCHAR(100)
SELECT @tutorID=i.TutorID FROM inserted i
SELECT @lessonID=i.LessonID FROM inserted i
SELECT @lessonlevel=l.Level FROM Lessons l WHERE l.ID=@lessonID
SELECT @type=t.Type FROM Tutors t WHERE t.ID = @tutorID
SELECT @instrument=l.InstrumentID FROM Lessons l WHERE l.ID=@lessonID
BEGIN
	IF EXISTS (SELECT * FROM Tutors t WHERE t.ID = @tutorID AND t.InstrumentID=@instrument)
	IF @lessonlevel = 7 OR @lessonlevel = 8
	BEGIN
		IF @type = 'Senior' OR @type = 'Head'
		BEGIN
			INSERT INTO LessonTutors VALUES(@lessonID, @tutorID)
		END
	END
	ELSE
		INSERT INTO LessonTutors VALUES(@lessonID, @tutorID)
END
GO

CREATE TRIGGER [INSERT_LessonStudents]
	ON LessonStudents
	INSTEAD OF INSERT
AS
DECLARE @studentID INT
DECLARE @lessonID INT
DECLARE @lessonlevel INT
DECLARE @noStudents INT
DECLARE @type VARCHAR(20)
DECLARE @instrument VARCHAR(100)
SELECT @studentID=i.StudentID FROM inserted i
SELECT @lessonID=i.LessonID FROM inserted i
SELECT @lessonlevel=l.Level FROM Lessons l WHERE l.ID=@lessonID
SELECT @instrument=l.InstrumentID FROM Lessons l WHERE l.ID=@lessonID
SELECT @noStudents=COUNT(LessonID) FROM LessonStudents WHERE LessonID = @lessonID
BEGIN
	IF EXISTS (SELECT si.InstrumentID FROM StudentInstrument si WHERE si.StudentID=@studentID AND si.InstrumentID=@instrument)
	IF EXISTS (SELECT * FROM Certifications c WHERE c.PersonID=@studentID AND c.CertificationLevel=@lessonlevel)
	BEGIN
		IF @instrument = 'Recorder'
		BEGIN
			IF @noStudents<=16
			BEGIN
				INSERT INTO LessonStudents VALUES(@lessonID, @studentID)
			END
		END
		ELSE
		BEGIN
			IF @noStudents<=8
			BEGIN
				INSERT INTO LessonStudents VALUES(@lessonID, @studentID)
			END
		END
	END
END
GO

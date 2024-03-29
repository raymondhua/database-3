CREATE TRIGGER [DELETE_Tutors]
	ON Tutors
	INSTEAD OF DELETE
AS
BEGIN
	DELETE FROM LessonTutors WHERE ID IN (SELECT ID FROM DELETED)
	DELETE FROM SheetMusicTutors WHERE ID IN (SELECT ID FROM DELETED)
	DELETE FROM Tutors WHERE ID IN (SELECT ID FROM DELETED)
PRINT 'Deleted from Tutors'
END
GO

CREATE TRIGGER [DELETE_Instruments]
	ON Instruments
	INSTEAD OF DELETE
AS
BEGIN
	DELETE FROM Tutors WHERE InstrumentID IN (SELECT Instrument FROM DELETED)
	DELETE FROM Instruments WHERE Instrument IN (SELECT Instrument FROM DELETED)
PRINT 'Deleted from Instruments'
END
GO

CREATE TRIGGER [DELETE_TutorTypes]
	ON TutorType
	INSTEAD OF DELETE
AS
BEGIN
	DELETE FROM Tutors WHERE Type IN (SELECT ID FROM DELETED)
	DELETE FROM TutorType WHERE ID IN (SELECT ID FROM DELETED)
PRINT 'Deleted from TutorType'
END
GO

CREATE TRIGGER [DELETE_Person]
	ON Person
	INSTEAD OF DELETE
AS
BEGIN
	DELETE FROM Parents WHERE ParentID IN (SELECT ID FROM DELETED)
	DELETE FROM Person WHERE ID IN (SELECT ID FROM DELETED)
PRINT 'Deleted from Person'
END
GO
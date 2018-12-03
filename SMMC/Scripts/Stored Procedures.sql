DROP PROCEDURE dbo.DeleteTutor
GO
DROP PROCEDURE dbo.UpdateTutor
GO

CREATE PROCEDURE dbo.DeleteTutor
	@PersonID INT
AS   
	DELETE lt FROM LessonTutors lt INNER JOIN Tutors t ON lt.TutorID=t.ID WHERE PersonID = @PersonID
	DELETE smt FROM SheetMusicTutors smt INNER JOIN Tutors t ON smt.TutorID=t.ID WHERE PersonID = @PersonID
	DELETE Tutors WHERE PersonID = @PersonID
GO

CREATE PROCEDURE dbo.UpdateTutor
	@TutorID INT,
	@Type VARCHAR(15)
AS   
	DECLARE @personID int
	DECLARE @instrumentID VARCHAR(100)
	SELECT @personID=PersonID, @instrumentID=InstrumentID FROM Tutors WHERE ID = @TutorID
	IF EXISTS (SELECT * FROM Certifications WHERE PersonID = @personID AND InstrumentID = @instrumentID AND CertificationLevel = 8)
	BEGIN
		IF @type = 'Head'
		BEGIN
			IF NOT EXISTS (SELECT * FROM Tutors WHERE Type='Head' AND InstrumentID = @instrumentID)
			BEGIN
				UPDATE Tutors SET Type = @Type WHERE ID = @TutorID
			END
		END
		ELSE
		BEGIN
			UPDATE Tutors SET Type = @Type WHERE ID = @TutorID
		END
	END
GO

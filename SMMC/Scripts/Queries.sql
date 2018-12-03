SELECT distinct p.FirstName, p.LastName,
		CASE 
            WHEN s.OpenDivision = 0
               THEN 'No'
               ELSE 'Yes'
		END as OpenDivision, i.Instrument, 
		CASE 
            WHEN si.Hire = 0
               THEN 'No'
               ELSE 'Yes'
		END as Hire,
		CASE 
            WHEN s.OpenDivision = 0
               THEN i.StudentFee
               ELSE i.OpenFee
		END as StudentFee,
		CASE 
            WHEN si.Hire = 0
               THEN 0
               ELSE i.HireFee
		END as HireFee, c1.CertificationLevel,
		CASE 
			WHEN c1.ATCL = 0
					THEN 'No'
					ELSE 'Yes'
			END as Hire  FROM StudentInstrument si
		INNER JOIN  Student s
			ON si.StudentID = s.PersonID
		INNER JOIN  Person p
			ON s.PersonID = p.ID
		INNER JOIN  Instruments i
			ON si.InstrumentID = i.Instrument
		INNER JOIN Certifications c1
			ON c1.PersonID = p.ID

		WHERE s.PersonID = 1

SELECT p.FirstName, p.LastName,
		CASE 
            WHEN s.OpenDivision = 0
               THEN 'No'
               ELSE 'Yes'
		END as OpenDivision, i.Instrument, 
		CASE 
            WHEN si.Hire = 0
               THEN 'No'
               ELSE 'Yes'
		END as Hire,
		CASE 
            WHEN s.OpenDivision = 0
               THEN i.StudentFee
               ELSE i.OpenFee
		END as StudentFee,
		CASE 
            WHEN si.Hire = 0
               THEN 0
               ELSE i.HireFee
		END as HireFee, c1.CertificationLevel,
		CASE 
			WHEN c1.ATCL = 0
					THEN 'No'
					ELSE 'Yes'
			END as ATCL, e.TypeID FROM StudentInstrument si
		INNER JOIN  Person p
			ON si.StudentID = p.ID
		INNER JOIN  Student s
			ON p.ID = s.PersonID
		INNER JOIN  Instruments i
			ON si.InstrumentID = i.Instrument
		INNER JOIN Certifications c1
			ON i.Instrument = c1.InstrumentID
		INNER JOIN Certifications c2
			ON p.ID = c2.PersonID
		INNER JOIN Ensembles e
			ON c1.CertificationLevel = e.Level
		WHERE c1.CertificationLevel = c2.CertificationLevel AND c1.InstrumentID=c2.InstrumentID

SELECT * FROM StudentInstrument si
		INNER JOIN  Person p
			ON si.StudentID = p.ID
		INNER JOIN  Instruments i
			ON si.InstrumentID = i.Instrument
		INNER JOIN Certifications c1
			ON c1.PersonID = si.StudentID
		INNER JOIN Certifications c2
			ON c2.InstrumentID = si.InstrumentID

SELECT p.FirstName, p.LastName, p.DateOfBirth, p.PhoneNo, a.Street, a.Suburb, a.City, a.Postcode FROM Student s
		INNER JOIN  Person p
			ON s.PersonID = p.ID
		FULL OUTER JOIN  PersonAddress pa
			ON pa.PersonID = p.ID
		FULL OUTER JOIN  Address a
			ON pa.AddressID = p.ID

SELECT * FROM Student s
		INNER JOIN  Person p
			ON s.PersonID = p.ID
		INNER JOIN  PersonAddress pa
			ON pa.PersonID = p.ID
		INNER JOIN  Address a
			ON pa.AddressID = p.ID

SELECT * FROM PersonAddress pa
		INNER JOIN  Student s
			ON pa.PersonID = s.PersonID
		INNER JOIN  Address a
			ON pa.AddressID = a.ID
		INNER JOIN  Person p
			ON pa.PersonID = p.ID

SELECT * FROM Student s INNER JOIN Person p ON s.PersonID = p.ID WHERE p.ID = 1

SELECT p.FirstName, p.LastName, a.Street + ', ' + a.Suburb + ', ' + a.City + ', ' + CONVERT(VARCHAR(12),Postcode) as Address, CASE WHEN s.OpenDivision = 0 THEN 'No' ELSE 'Yes' END as OpenDivision FROM PersonAddress pa INNER JOIN Student s ON pa.PersonID = s.PersonID INNER JOIN  Address a ON pa.AddressID = a.ID INNER JOIN  Person p ON pa.PersonID = p.ID

SELECT SUM(i.HireFee) FROM StudentInstrument si INNER JOIN  Student s ON si.StudentID = s.PersonID INNER JOIN Instruments i ON si.InstrumentID = i.Instrument WHERE s.PersonID = 2

SELECT p.FirstName, p.LastName, a.Street + ', ' + Suburb + ', ' + City + ', ' + CONVERT(VARCHAR(12),Postcode) as Address,
		CASE 
            WHEN s.OpenDivision = 0
               THEN 'No'
               ELSE 'Yes'
		END as OpenDivision FROM Student s
		INNER JOIN  Person p
			ON s.PersonID = p.ID
		INNER JOIN  PersonAddress pa
			ON pa.PersonID = pa.PersonID
		INNER JOIN  Address a
			ON pa.AddressID = a.ID
		WHERE s.PersonID = 1

SELECT p.FirstName, p.LastName, CASE WHEN s.OpenDivision = 0 THEN 'No' ELSE 'Yes' END as OpenDivision, i.Instrument, CASE WHEN si.Hire = 0 THEN 'No' ELSE 'Yes' END as Hire, CASE WHEN s.OpenDivision = 0 THEN i.StudentFee ELSE i.OpenFee END as StudentFee, CASE WHEN si.Hire = 0 THEN 0 ELSE i.HireFee END as HireFee FROM StudentInstrument si INNER JOIN  Student s ON si.StudentID = s.PersonID INNER JOIN  Person p ON s.PersonID = p.ID INNER JOIN  Instruments i ON si.InstrumentID = i.Instrument

SELECT p.FirstName, p.LastName, a.Street + ', ' + a.Suburb + ', ' + a.City + ', ' + CONVERT(VARCHAR(12),Postcode) as Address, CASE WHEN s.OpenDivision = 0 THEN 'No' ELSE 'Yes' END as OpenDivision FROM Student s INNER JOIN  Person p ON s.PersonID = p.ID INNER JOIN  PersonAddress pa ON pa.PersonID = pa.PersonID INNER JOIN  Address a ON pa.AddressID = a.ID WHERE s.PersonID = 2

SELECT p.FirstName, p.LastName, CASE WHEN s.OpenDivision = 0 THEN 'No' ELSE 'Yes' END as OpenDivision, i.Instrument, CASE WHEN si.Hire = 0 THEN 'No' ELSE 'Yes' END as Hire, CASE WHEN s.OpenDivision = 0 THEN i.StudentFee ELSE i.OpenFee END as StudentFee, CASE WHEN si.Hire = 0 THEN 0 ELSE i.HireFee END as HireFee, c1.CertificationLevel, CASE WHEN c1.ATCL = 0 THEN 'No' ELSE 'Yes' END as ATCL FROM StudentInstrument si INNER JOIN  Person p ON si.StudentID = p.ID INNER JOIN  Student s ON p.ID = s.PersonID INNER JOIN  Instruments i ON si.InstrumentID = i.Instrument INNER JOIN Certifications c1 ON i.Instrument = c1.InstrumentID INNER JOIN Certifications c2 ON p.ID = c2.PersonID INNER JOIN Ensembles e ON c1.CertificationLevel = e.Level WHERE c1.CertificationLevel = c2.CertificationLevel

SELECT DISTINCT Instrument FROM Instruments i WHERE Instrument NOT IN ( SELECT ID, i.Instrument FROM StudentInstrument si INNER JOIN  Student s ON si.StudentID = s.PersonID INNER JOIN  Instruments i ON si.InstrumentID = i.Instrument WHERE s.PersonID = 1 )

SELECT DISTINCT CONVERT(i.Instrument,'0001') FROM Instruments i
WHERE Instrument NOT IN (SELECT Instrument FROM StudentInstrument si INNER JOIN  Student s ON si.StudentID = s.PersonID INNER JOIN  Instruments i ON si.InstrumentID = i.Instrument WHERE s.PersonID = 1) 

SELECT * FROM Ensembles

SELECT * FROM Certifications WHERE PersonID = 1 AND (CertificationLevel = 6 OR CertificationLevel = 7)

SELECT ID, FirstName + ' ' + LastName AS Fullname FROM Person WHERE ID IN (SELECT PersonID FROM Certifications WHERE (CertificationLevel = 6 OR CertificationLevel = 7)) AND (NOT IN (SELECT * FROM Tutors))

SELECT * FROM Certifications WHERE PersonID = 2 AND (CertificationLevel = 6 OR CertificationLevel = 7) AND InstrumentID NOT IN(SELECT InstrumentID FROM Tutors WHERE PersonID = 2)



SELECT DISTINCT Instrument FROM Instruments i WHERE Instrument NOT IN (SELECT Instrument FROM Certifications c INNER JOIN  Person p ON c.PersonID = p.ID INNER JOIN  Instruments i ON c.InstrumentID = i.Instrument WHERE PersonID = 1)

SELECT p.FirstName, p.LastName, PhoneNo, InstrumentID FROM PerformancesStudent ps
		INNER JOIN  StudentInstrument si
			ON ps.StudentInstrumentID = si.ID
		INNER JOIN  Student s
			ON si.StudentID = s.PersonID
		INNER JOIN Person p
			ON s.PersonID = p.ID

SELECT p.FirstName, p.FirstName,		
		CASE 
            WHEN s.OpenDivision = 0
               THEN 'No'
               ELSE 'Yes'
		END as OpenDivision, l.InstrumentID, l.Level, l.Time FROM LessonStudents ls
		INNER JOIN  Lessons l
			ON ls.LessonID = l.ID
		INNER JOIN  Student s
			ON ls.StudentID = s.PersonID
		INNER JOIN  Person p
			ON s.PersonID = p.ID

SELECT p.FirstName, p.FirstName, l.InstrumentID, l.Level, l.Time, l.ID FROM LessonTutors lt
		INNER JOIN  Lessons l
			ON lt.LessonID = l.ID
		INNER JOIN  Tutors t
			ON lt.TutorID = t.PersonID
		INNER JOIN  Person p
			ON t.PersonID = p.ID


SELECT p.FirstName, p.LastName, l.InstrumentID, l.Level, l.Time FROM LessonTutors lt INNER JOIN Lessons l ON lt.LessonID = l.ID INNER JOIN  Tutors t ON lt.TutorID = t.PersonID INNER JOIN  Person p ON t.PersonID = p.ID WHERE l.ID = 8

SELECT ID, CONVERT(VARCHAR(5),Time) + ' - Level ' + CONVERT(VARCHAR(3),Level) + ' - ' + InstrumentID AS LevelLabel FROM Lessons

SELECT * FROM Tutors



SELECT t.ID, FirstName + ' ' + LastName + ' - ' + InstrumentID AS Name FROM Tutors t INNER JOIN  Person p ON t.PersonID  = p.ID

SELECT * FROM LessonTutors lt INNER JOIN Lessons l ON lt.LessonID = l.ID FULL oUTER JOIN  Tutors t ON lt.TutorID = t.PersonID

SELECT FirstName + ' ' + LastName + ' - ' + InstrumentID AS Name FROM Tutors t
		INNER JOIN  Person p
			ON t.PersonID  = p.ID

SELECT t.ID, FirstName + ' ' + LastName + ' - ' + InstrumentID AS Name FROM Tutors t INNER JOIN  Person p ON t.PersonID  = p.ID WHERE InstrumentID = 'Tuba'

SELECT * FROM Certifications WHERE CertificationLevel = 1

SELECT * FROM StudentInstrument WHERE StudentID IN (SELECT PersonID FROM Certifications WHERE CertificationLevel = 1)

SELECT si.StudentID, p.FirstName + ' ' + p.LastName FROM StudentInstrument si INNER JOIN  Person p ON si.StudentID  = p.ID

SELECT DISTINCT PersonID, p.FirstName + ' ' + p.LastName AS FullName FROM Tutors t
		INNER JOIN  Person p
			ON t.PersonID = p.ID

SELECT * FROM Tutors
SELECT p.ID, p.FirstName, p.LastName, a.Street + ', ' + a.Suburb + ', ' + a.City + ', ' + CONVERT(VARCHAR(12),Postcode) as Address, p.PhoneNo FROM PersonAddress pa INNER JOIN Tutors t ON pa.PersonID = t.PersonID INNER JOIN  Address a ON pa.AddressID = a.ID INNER JOIN  Person p ON pa.PersonID = p.ID

SELECT l.InstrumentID, l.Level, l.Time, cast(round(tt.HourlyRate*0.5,2) as numeric(36,2)) AS Pay FROM LessonTutors lt INNER JOIN Lessons l ON lt.LessonID = l.ID INNER JOIN  Tutors t ON lt.TutorID = t.PersonID INNER JOIN  TutorType tt  ON t.Type=tt.ID WHERE t.PersonID = 4 

SELECT * FROM Tutors t
		INNER JOIN  TutorType tt
			ON t.Type = tt.ID

SELECT * FROM LessonTutors lt INNER JOIN Lessons l ON lt.LessonID = l.ID INNER JOIN TutorType tt  ON t.Type=tt.ID WHERE lt.TutorID = 2

SELECT * FROM Tutors

DELETE FROM LessonStudents WHERE LessonID=2 AND StudentID=2
SELECT l.InstrumentID, l.Level, l.Time, cast(round(tt.HourlyRate*0.5,2) as numeric(36,2)) AS Pay FROM Lessons l INNER JOIN LessonTutors lt ON l.ID=lt.LessonID INNER JOIN Tutors t ON lt.TutorID=t.PersonID INNER JOIN TutorType tt ON t.Type=tt.ID WHERE l.InstrumentID=t.InstrumentID

SELECT pe.FirstName + ' ' + pe.LastName AS FullName, InstrumentID FROM PerformancesStudent ps
	INNER JOIN Performances p ON ps.PerformancesID=p.ID
	INNER JOIN StudentInstrument si ON ps.StudentInstrumentID=si.ID
	INNER JOIN Student st ON si.StudentID=st.PersonID
	INNER JOIN Person pe ON si.StudentID=pe.ID

SELECT p.ID, Name + ' - ' + CONVERT(VARCHAR(12),p.Date) + ' - ' + CONVERT(VARCHAR(12),p.Time) AS PerformanceInfo FROM Performances p INNER JOIN Venues v ON p.VenueID=v.ID

SELECT si.ID, p.FirstName + ' ' + p.LastName + ' - ' + i.Instrument AS FullName FROM StudentInstrument si INNER JOIN  Student s ON si.StudentID = s.PersonID INNER JOIN  Instruments i ON si.InstrumentID = i.Instrument
INNER JOIN Person p ON s.PersonID=p.ID

SELECT st.PersonID FROM PerformancesStudent ps INNER JOIN Performances p ON ps.PerformancesID=p.ID INNER JOIN StudentInstrument si ON ps.StudentInstrumentID=si.ID INNER JOIN Student st ON si.StudentID=st.PersonID WHERE p.ID = 1


SELECT p.Date, p.Time, v.Name, InstrumentID FROM PerformancesStudent ps INNER JOIN Performances p ON ps.PerformancesID = p.ID INNER JOIN Venues v ON p.VenueID = v.ID INNER JOIN StudentInstrument si ON ps.StudentInstrumentID = si.ID INNER JOIN Student st ON si.StudentID = st.PersonID INNER JOIN Person pe ON si.StudentID = pe.ID

SELECT  CONVERT(date, p.Date),p.Date, p.Time, v.Name, InstrumentID FROM PerformancesStudent ps INNER JOIN Performances p ON ps.PerformancesID = p.ID INNER JOIN Venues v ON p.VenueID = v.ID INNER JOIN StudentInstrument si ON ps.StudentInstrumentID = si.ID INNER JOIN Student st ON si.StudentID = st.PersonID INNER JOIN Person pe ON si.StudentID = pe.ID WHERE st.PersonID = 1


SELECT * FROM SheetMusicInstruments

SELECT * FROM Tutors t INNER JOIN Person p ON t.PersonID = p.ID INNER JOIN PersonAddress pa ON p.ID = pa.PersonID INNER JOIN Address a ON pa.AddressID = a.ID

SELECT * FROM Person p INNER JOIN PersonAddress pa ON p.ID = pa.PersonID INNER JOIN Address a ON pa.AddressID = a.ID WHERE p.ID =3

SELECT * FROM SheetMusic sm
	INNER JOIN SheetMusicTutors smt ON sm.ID=smt.SheetMusicID
	INNER JOIN SheetMusicInstruments smi ON sm.ID=smt.SheetMusicID

SELECT DistrubitedCopies-(SELECT CASE WHEN COUNT(1) > 0 THEN GivenCopies ELSE 1 END FROM SheetMusic sm
	INNER JOIN SheetMusicTutors smt ON sm.ID=smt.SheetMusicID WHERE sm.ID = 2
	GROUP BY GivenCopies) AS CopiesToBeReturned FROM SheetMusic sm
	INNER JOIN SheetMusicTutors smt ON sm.ID=smt.SheetMusicID WHERE sm.ID = 2
	GROUP BY DistrubitedCopies
	
SELECT CASE WHEN COUNT(1) > 0 THEN GivenCopies ELSE 1 END as OpenDiv FROM SheetMusic sm
	INNER JOIN SheetMusicTutors smt ON sm.ID=smt.SheetMusicID WHERE sm.ID = 2
	GROUP BY GivenCopies

(SELECT 0 GivenCopies)
UNION
SELECT CASE WHEN COUNT(1) > 0 THEN GivenCopies ELSE 1 END as OpenDiv FROM SheetMusic sm
	INNER JOIN SheetMusicTutors smt ON sm.ID=smt.SheetMusicID WHERE sm.ID = 1
	GROUP BY GivenCopies



SELECT FirstName + ' ' + LastName AS Name FROM Tutors t
	INNER JOIN Person p ON t.PersonID=p.ID
WHERE InstrumentID IN (SELECT InstrumentID FROM SheetMusic sm
	INNER JOIN SheetMusicTutors smt ON sm.ID=smt.SheetMusicID
	INNER JOIN SheetMusicInstruments smi ON sm.ID=smt.SheetMusicID WHERE sm.ID=1)
AND t.ID NOT IN (SELECT * FROM SheetMusicTutors WHERE SheetMusicID = 1)

SELECT * FROM SheetMusic

SELECT DistrubitedCopies-(SELECT GivenToStudents FROM SheetMusic sm INNER JOIN SheetMusicTutors smt ON sm.ID=smt.SheetMusicID GROUP BY GivenToStudents) AS CopiesToBeReturned FROM SheetMusic sm INNER JOIN SheetMusicTutors smt ON sm.ID=smt.SheetMusicID WHERE sm.ID = 3 GROUP BY DistrubitedCopies

SELECT FirstName + ' ' + LastName AS Name FROM Tutors t INNER JOIN Person p ON t.PersonID=p.ID WHERE InstrumentID IN (SELECT InstrumentID FROM SheetMusic sm INNER JOIN SheetMusicTutors smt ON sm.ID=smt.SheetMusicID INNER JOIN SheetMusicInstruments smi ON sm.ID=smt.SheetMusicID WHERE sm.ID=@smID)

SELECT DistrubitedCopies-(SELECT CASE WHEN COUNT(1) > 0 THEN GivenCopies ELSE 1 END as OpenDiv FROM SheetMusic sm
	INNER JOIN SheetMusicTutors smt ON sm.ID=smt.SheetMusicID WHERE sm.ID = 2
	GROUP BY GivenCopies) AS CopiesToBeReturned FROM SheetMusic sm INNER JOIN SheetMusicTutors smt ON sm.ID=smt.SheetMusicID WHERE sm.ID = 2 GROUP BY DistrubitedCopies

SELECT DistrubitedCopies-(SELECT GivenCopies FROM SheetMusic sm
	INNER JOIN SheetMusicTutors smt ON sm.ID=smt.SheetMusicID WHERE sm.ID = 2
	GROUP BY GivenCopies) AS CopiesToBeReturned FROM SheetMusic sm WHERE sm.ID = 2 GROUP BY DistrubitedCopies

SELECT CASE WHEN COUNT(1) > 0 THEN GivenCopies ELSE 1 END as OpenDiv FROM SheetMusic sm
	INNER JOIN SheetMusicTutors smt ON sm.ID=smt.SheetMusicID WHERE sm.ID = 1
	GROUP BY GivenCopies

SELECT * FROM Tutors WHERE SheetMusicID = 9

SELECT InstrumentID FROM SheetMusic sm INNER JOIN SheetMusicInstruments smi ON sm.ID=smi.SheetMusicID WHERE sm.ID=9

SELECT * FROM SheetMusicTutors WHERE SheetMusicID=8 AND TutorID=1

SELECT p.FirstName, p.LastName, p.PhoneNo, SUM(cast(round(tt.HourlyRate*0.5,2) as numeric(36,2))) AS TotalPayPerWeek, SUM(cast(round(0.5,2) as numeric(36,2))) AS HoursWorked FROM Lessons l 
INNER JOIN LessonTutors lt ON l.ID=lt.LessonID 
INNER JOIN Tutors t ON lt.TutorID=t.ID
INNER JOIN Person p ON t.PersonID=p.ID
INNER JOIN TutorType tt ON t.Type=tt.ID WHERE l.InstrumentID=t.InstrumentID 
GROUP BY p.ID, p.FirstName, p.LastName, p.PhoneNo

SELECT '$' + CONVERT(VARCHAR(12),pa.HoursWorked) FROM Payroll pa
INNER JOIN Person pe ON pa.ID=pe.ID

SELECT * FROM Instruments

SELECT DistrubitedCopies-(SELECT GivenCopies FROM SheetMusic sm  INNER JOIN SheetMusicTutors smt ON sm.ID = smt.SheetMusicID WHERE sm.ID = 1 GROUP BY GivenCopies) AS CopiesToBeReturned FROM SheetMusic sm WHERE sm.ID = 1 GROUP BY DistrubitedCopies

SELECT GivenCopies FROM SheetMusic sm INNER JOIN SheetMusicTutors smt ON sm.ID = smt.SheetMusicID WHERE sm.ID = 1 GROUP BY GivenCopies

SELECT GivenToStudents FROM SheetMusic sm INNER JOIN SheetMusicTutors smt ON sm.ID = smt.SheetMusicID WHERE sm.ID = 1 GROUP BY GivenToStudents

SELECT FirstName + ' ' + LastName AS Fullname, GivenCopies, GivenToStudents, GivenCopies-GivenToStudents AS CopiesRemaining FROM SheetMusicTutors smt
INNER JOIN Tutors t ON smt.TutorID=t.ID
INNER JOIN Person p ON t.PersonID=p.ID

SELECT * FROM SheetMusicInstruments WHERE ID = @SheetMusicID

SELECT COUNT(PersonID) AS TotalTutors FROM Tutors

SELECT COUNT(PersonID) AS TotalTutors FROM Tutors

SELECT COUNT(DISTINCT PersonID) AS TotalStudents FROM Student

SELECT * FROM LessonStudents
INNER JOIN Person p ON s.PersonID=p.ID

SELECT p.FirstName + ' ' + p.LastName + ' - ' + i.Instrument AS FullName FROM StudentInstrument si INNER JOIN  Student s ON si.StudentID = s.PersonID INNER JOIN  Instruments i ON si.InstrumentID = i.Instrument INNER JOIN Person p ON s.PersonID = p.ID WHERE s.PersonID NOT IN (SELECT st.PersonID FROM PerformancesStudent ps INNER JOIN Performances p ON ps.PerformancesID=p.ID INNER JOIN StudentInstrument si ON ps.StudentID=si.ID  INNER JOIN Student st ON si.StudentID=st.PersonID WHERE p.ID = 1)

SELECT si.StudentID FROM PerformancesStudent ps INNER JOIN Performances p ON ps.PerformancesID=p.ID INNER JOIN StudentInstrument si ON ps.StudentID=si.StudentID WHERE ps.InstrumentID=si.InstrumentID AND p.ID = 1

SELECT pe.FirstName + ' ' + pe.LastName AS FullName, si.InstrumentID FROM PerformancesStudent ps INNER JOIN Performances p ON ps.PerformancesID = p.ID INNER JOIN StudentInstrument si ON ps.StudentID = si.StudentID INNER JOIN Student st ON si.StudentID = st.PersonID INNER JOIN Person pe ON si.StudentID = pe.ID WHERE si.InstrumentID=ps.InstrumentID AND p.ID=@PerformanceID

SELECT CONVERT(date, p.Date) AS Date, p.Time, v.Name, si.InstrumentID FROM PerformancesStudent ps INNER JOIN Performances p ON ps.PerformancesID = p.ID INNER JOIN Venues v ON p.VenueID = v.ID INNER JOIN StudentInstrument si ON ps.StudentID = si.StudentID INNER JOIN Student st ON si.StudentID = st.PersonID INNER JOIN Person pe ON si.StudentID = pe.ID WHERE si.InstrumentID=ps.InstrumentID AND st.PersonID = @PersonID

SELECT p.FirstName, p.LastName, CASE WHEN s.OpenDivision = 0 THEN 'No' ELSE 'Yes' END as OpenDivision, l.InstrumentID, l.Level, l.Time FROM LessonStudents ls INNER JOIN  Lessons l ON ls.LessonID = l.ID INNER JOIN  Student s ON ls.StudentID = s.PersonID  INNER JOIN  Person p ON s.PersonID = p.ID WHERE l.ID = @LessonID
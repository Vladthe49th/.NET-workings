use Hospital


-- Main task

CREATE TABLE [Patients] (
 [PatientID] INT PRIMARY KEY,
 [FirstName] VARCHAR(50),
 [LastName] VARCHAR(50),
 [Age] INT,
 [Department] VARCHAR(50),
 [AdmissionDate] DATE,
 [DischargeDate] DATE,
 [Disease] VARCHAR(100),
 [Doctor] VARCHAR(50),
 [MobileOperator] VARCHAR(50)
);

INSERT INTO [Patients] ([PatientID], [FirstName], [LastName], [Age], [Department], [AdmissionDate], [DischargeDate], [Disease], [Doctor], [MobileOperator])
VALUES
(1, 'Ivan', 'Petrov', 34, 'Cardiology', '2025-03-10', NULL, 'Hypertension', 'Dr. Sokolov', 'Vodafone'),
(2, 'Toad', 'Grybovski', 28, 'Neurology', '2025-02-15', '2025-03-20', 'Migraine', 'Dr. Mario', 'Kyivstar'),
(3, 'Bruce', 'Willis', 45, 'Orthopedics', '2024-11-01', '2025-01-15', 'Dementia', 'Dr. Sokolov', 'Lifecell'),
(4, 'Olena', 'Olena', 61, 'Cardiology', '2025-01-05', NULL, 'Arrhythmia', 'Dr. Olena', 'Kyivstar'),
(5, 'Mykola', 'Ravlenko', 30, 'Therapy', '2024-10-10', '2025-04-01', 'Pneumonia', 'Dr. Lysenko', 'Vodafone');

-- 1) Patients in hospital

SELECT * FROM [Patients]
WHERE [DischargeDate] IS NULL;

-- 2) Patients from a certain department
SELECT * FROM Patients
WHERE [Department] = 'Cardiology';

-- 3) Department names

SELECT DISTINCT [Department] FROM [Patients];

-- 4) Long-staying patients

SELECT * FROM [Patients]
WHERE DATEDIFF(DAY, [AdmissionDate], GETDATE()) > 30 AND [DischargeDate] IS NULL
ORDER BY [AdmissionDate] ASC;


-- 5) Patients who went away last month

SELECT * FROM [Patients]
WHERE MONTH([DischargeDate]) = MONTH(GETDATE()) - 1
  AND YEAR([DischargeDate]) = YEAR(GETDATE());

-- 6) From december to november

SELECT * FROM [Patients]
WHERE [Department] = 'Orthopedics' AND
      [AdmissionDate] BETWEEN '2024-10-01' AND '2024-12-31';

-- 7) Youngest patient

SELECT TOP 1 *, [Age] FROM [Patients]
ORDER BY [Age] ASC;

-- 8) The three departments

SELECT * FROM [Patients]
WHERE [Department] IN (
    SELECT DISTINCT [Department] FROM [Patients]
    ORDER BY [Department]
    OFFSET 0 ROWS FETCH NEXT 3 ROWS ONLY
);

-- 9) R Last names

SELECT * FROM [Patients]
WHERE [LastName] LIKE 'R%';

-- 10 Certain disease, certain doctor

SELECT * FROM [Patients]
WHERE [Doctor] = 'Dr. Sokolov'
AND [Disease] IN (
    SELECT [Disease]
    FROM [Patients]
    WHERE [Doctor] = 'Dr. Sokolov'
    GROUP BY [Disease]
    HAVING COUNT(*) > 1
);

-- 11) Certain mobile operator

SELECT * FROM [Patients]
WHERE [MobileOperator] = 'Kyivstar';


--12) Update department

UPDATE [Patients]
SET [Department] = 'General Medicine'
WHERE [Department] = 'Therapy';


-- Sub-task University

CREATE TABLE [Faculties] (

[Id] INT PRIMARY KEY,
[Dean] VARCHAR(50),
[Name] VARCHAR(50)

);

CREATE TABLE [Departments] (

[Id] INT PRIMARY KEY,
[Financing] MONEY,
[Name] VARCHAR(50)

);

CREATE TABLE [Teachers] (

[Id] INT PRIMARY KEY,
[EmploymentDate] DATE,
[IsAssistant] BIT,
[Name] VARCHAR(50),
[Position] VARCHAR(50),
[Premium] MONEY,
[Salary] MONEY,
[Surname] VARCHAR(50)

);

CREATE TABLE [Groups] (

[Id] INT PRIMARY KEY,
[Name] VARCHAR(50),
[Rating] INT,
[Year] DATE

);


-- Faculties
INSERT INTO [Faculties] VALUES (1, 'Dmitro Stalone', 'Computer Science');
INSERT INTO [Faculties] VALUES (2, 'Eugene Antonoven', 'Mathematics');

-- Departments
INSERT INTO [Departments] VALUES (1, 10000, 'Physics');
INSERT INTO [Departments] VALUES (2, 30000, 'Software Development');
INSERT INTO [Departments] VALUES (3, 9000, 'History');

-- Teachers
INSERT INTO [Teachers] VALUES (1, '1999-12-12', 1, 'Alice', 'Assistant', 1, 400, 'Lidell');
INSERT INTO Teachers VALUES (2, '2001-05-01', 0, 'Samara', 'Professor', 1, 1200, 'Morgan');
INSERT INTO Teachers VALUES (3, '1995-07-15', 1, 'Tomimo', 'Assistant', 0, 500, 'Tokoso');

-- Groups
INSERT INTO Groups VALUES (1, 'CS-51', 4, '2020-09-01');
INSERT INTO Groups VALUES (2, 'MATH-42', 3, '2019-09-01');
INSERT INTO Groups VALUES (3, 'SHRIMP-21', 2, '2018-09-01');


-- 1) Reverse Departments

SELECT [Name], [Financing], [Id] FROM [Departments];

-- 2) Group names and ratings with new names

SELECT [Name] AS [Group Name], Rating AS [Group Rating] FROM [Groups];

-- 3) Premium sum

SELECT 
  [Surname],
  (CASE WHEN [Premium] > 0 THEN [Salary] * 100.0 / [Premium] ELSE NULL END) AS [Salary to Premium %],
  ([Salary] * 100.0 / (Salary + Premium)) AS [Salary to Total %]
FROM [Teachers];

--4) Formatted faculties

SELECT 'The dean of faculty ' + [Name] + ' is ' + [Dean] + '.' AS Info FROM [Faculties];

--5) Salary higher than 1050

SELECT [Surname] FROM [Teachers]
WHERE [Position] = 'Professor' AND [Salary] > 1050;

--6) Funding less than 11000 or more than 25000

SELECT [Name] FROM [Departments]
WHERE [Financing] < 11000 OR [Financing] > 25000;

--7) All faculties, except CS

SELECT [Name] FROM [Faculties]
WHERE [Name] <> 'Computer Science';

--8) Teachers, not professors

SELECT [Surname], [Position] FROM [Teachers]
WHERE [Position] <> 'Professor';

--9) 160-550 Premium

SELECT [Surname], [Position], [Salary], [Premium] FROM [Teachers]
WHERE [IsAssistant] = 1 AND [Premium] BETWEEN 160 AND 550;

--10) Assistant salary and surnames

SELECT [Surname], [Salary] FROM [Teachers]
WHERE [IsAssistant] = 1;

--11) Teachers before 2000

SELECT [Surname], [Position] FROM [Teachers]
WHERE [EmploymentDate] < '2000-01-01';

--12) Departs before SD in alphabet

SELECT [Name] AS [Name of Department] FROM [Departments]
WHERE [Name] < 'Software Development';

--13) Assistents with salary <=1200

SELECT [Surname] FROM [Teachers]
WHERE [IsAssistant] = 1 AND ([Salary] + [Premium]) <= 1200;

-- 14) Fifth course groups with rating from 2 to 4

SELECT [Name] FROM [Groups]
WHERE [Year] = 2020 AND [Rating] BETWEEN 2 AND 4;

-- 15) Assistants with salary < 550 or premium < 200

SELECT [Surname] FROM [Teachers]
WHERE [IsAssistant] = 1 AND ([Salary] < 550 OR [Premium] < 200);




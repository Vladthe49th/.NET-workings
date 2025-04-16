CREATE DATABASE [first]

USE [first]


-- Task 1 -- students

CREATE TABLE [Students] (

 [Id] INT PRIMARY KEY,
 [Name] NVARCHAR(100) NOT NULL,
 [Age] INT CHECK ([Age] >= 16),
 [Groupcode] INT,
 FOREIGN KEY ([Groupcode]) REFERENCES [Groups]([Groupcode])


);

CREATE TABLE [Groups] (

[Groupcode] INT PRIMARY KEY,
[Groupname] NVARCHAR(100) NOT NULL
);


SELECT * FROM [Students]

GO

-- Task 2 -- books

CREATE TABLE [Books] (
    [BookID] SERIAL PRIMARY KEY,
    [Title] VARCHAR(200) NOT NULL,
    [Pages] INT CHECK (Pages > 0),
    [AuthorID] INT REFERENCES [Authors]([AuthorID])
);

CREATE TABLE [Authors] (
    [AuthorID] SERIAL PRIMARY KEY,
    [Name] VARCHAR(100) NOT NULL
);

-- Task 3 -- courses

CREATE TABLE [Courses] (

[CourseID] SERIAL PRIMARY KEY,
    [Title] VARCHAR(200) NOT NULL,
    [Hours] INT CHECK (Hours >= 0),
    [TeacherID] INT REFERENCES [Teachers]([TeacherID])

);

CREATE TABLE [Teachers] (
    [TeacherID] SERIAL PRIMARY KEY,
    [Name] VARCHAR(100) NOT NULL
);


-- Task 4 -- orders

CREATE TABLE [Orders] (
    [OrderID] SERIAL PRIMARY KEY,
    [OrderDate] DATE NOT NULL,
    [Amount] DECIMAL(10, 2) CHECK (Amount > 0),
    [CustomerID] INT REFERENCES [Customers]([CustomerID])
);


CREATE TABLE [Customers] (
    [CustomerID] SERIAL PRIMARY KEY,
    [Name] VARCHAR(100) NOT NULL
);


-- Task 5 -- products

CREATE TABLE [Products] (
    [ProductID] SERIAL PRIMARY KEY,
    [Title] VARCHAR(200) NOT NULL,
    [Price] DECIMAL CHECK (Price >= 0),
    [CategoryID] INT REFERENCES [Categories]([CategoryID])
);

CREATE TABLE [Categories] (
    [CategoryID] SERIAL PRIMARY KEY,
    [Name] VARCHAR(100) NOT NULL
);


-- Users

CREATE TABLE [Users] (
    [id] SERIAL PRIMARY KEY,
    [username] VARCHAR(50) UNIQUE NOT NULL,
    [password] VARCHAR(100) NOT NULL,
    [role_id] INTEGER NOT NULL,
    FOREIGN KEY ([role_id]) REFERENCES [Roles]([id])
);


CREATE TABLE [Roles] (
    [id] SERIAL PRIMARY KEY,
    [role_name] VARCHAR(50) NOT NULL
);



-- Employees
CREATE TABLE [Employees] (
    [id] SERIAL PRIMARY KEY,
    [name] VARCHAR(100) NOT NULL,
    [salary] NUMERIC  NOT NULL CHECK (salary >= 5000),
    [department_id] INTEGER NOT NULL,
    FOREIGN KEY ([department_id]) REFERENCES [Departments]([id])
);

CREATE TABLE [Departments] (
    id SERIAL PRIMARY KEY,
    department_name VARCHAR(100) NOT NULL
);



-- Cars
CREATE TABLE [Cars] (
    [id] SERIAL PRIMARY KEY,
    [model] VARCHAR(100) NOT NULL,
    [year] INTEGER NOT NULL CHECK (year >= 1990),
    [owner_id] INTEGER NOT NULL,
    FOREIGN KEY ([owner_id]) REFERENCES [Owners]([id])
);

CREATE TABLE [Owners] (
    [id] SERIAL PRIMARY KEY,
    [name] VARCHAR(100) NOT NULL
);



--  Items
CREATE TABLE [Items] (
    [id] SERIAL PRIMARY KEY,
    [item_name] VARCHAR(100) NOT NULL,
    [quantity] INTEGER NOT NULL CHECK (quantity >= 0),
    [supplier_id] INTEGER NOT NULL,
    FOREIGN KEY ([supplier_id]) REFERENCES [Suppliers]([id])
);


CREATE TABLE [Suppliers] (
    [id] SERIAL PRIMARY KEY,
    [name] VARCHAR(100) NOT NULL
);



--  Marks
CREATE TABLE [Marks] (
    [id] SERIAL PRIMARY KEY,
    [pupil_id] INTEGER NOT NULL,
    [subject] VARCHAR(100) NOT NULL,
    [grade] INTEGER NOT NULL CHECK (grade >= 1 AND grade <= 12),
    FOREIGN KEY ([pupil_id]) REFERENCES [Pupils]([id])
);


CREATE TABLE [Pupils] (
    [id] SERIAL PRIMARY KEY,
    [name] VARCHAR(100) NOT NULL
)

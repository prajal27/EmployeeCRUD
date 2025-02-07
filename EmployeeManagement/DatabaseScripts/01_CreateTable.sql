Create DATABASE EmployeeManageDb
USE EmployeeManageDb
Go

CREATE TABLE Departments (
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    DepartmentID INT NOT NULL,
    Salary DECIMAL(18,2) NOT NULL,
    DateOfJoining DATETIME NOT NULL,
    CONSTRAINT FK_Employee_Department FOREIGN KEY (DepartmentID) 
        REFERENCES Departments(DepartmentID)
);

--department insert
INSERT INTO Departments (DepartmentName) 
VALUES ('IT'),('HR'),('Sales'),('finance');

SELECT * FROM Departments
SELECT * FROM Employees

GO
INSERT INTO Employees (Name, DepartmentID, Salary, DateOfJoining) 
VALUES
('Sarah Johnson', 1, 75000.00, '2020-05-15'),
('Michael Chen', 2, 68000.50, '2019-11-22'),
('Emily Rodriguez', 3, 55000.00, '2022-03-01'),
('David Thompson', 4, 62000.75, '2021-08-10'),
('Olivia Smith', 2, 58000.00, '2023-01-15'),
('James Wilson', 3, 82000.25, '2018-06-05'),
('Sophia Martinez', 4, 48000.00, '2022-09-17'),
('Daniel Kim', 1, 71000.00, '2020-02-28'),
('Emma Davis', 3, 53000.50, '2021-04-12'),
('Liam Brown', 4, 65000.00, '2019-07-30'),
('Ava Miller', 4, 88000.00, '2023-03-10'),
('Noah Garcia', 2, 57000.75, '2022-11-05'),
('Isabella Taylor', 3, 60000.00, '2020-12-01'),
('William Clark', 3, 54000.25, '2021-09-19'),
('Mia Hernandez', 1, 49000.00, '2023-06-15');

GO
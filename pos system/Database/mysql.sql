CREATE DATABASE POS;
GO

USE POS;
GO

CREATE TABLE ProductTable (
    ProductId VARCHAR(5) PRIMARY KEY,    
    ProductName NVARCHAR(100),
	Quantity INT,
    CostPrice DECIMAL(10, 2),
    SellingPrice DECIMAL(10, 2),
    Description NVARCHAR(255),
	Status VARCHAR(20)                    

);
Go

CREATE TABLE EmployeeTable (
    EmployeeId NVARCHAR(10) PRIMARY KEY,
    EmployeeName NVARCHAR(100),
    EmployeeNIC NVARCHAR(12),
    EmployeeTeleNo INT,
    EmployeeAddress NVARCHAR(50),
    EmployeeEmail NVARCHAR(50),
    EmployeeRole NVARCHAR(50),
    EmployeeGender VARCHAR(10),
    EmployeePhoto VARBINARY(MAX),
    EmployeeUserName NVARCHAR(50),
    EmployeePassword NVARCHAR(50),
    EmployeeSalary NVARCHAR(20)
);
GO

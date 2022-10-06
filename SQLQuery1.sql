drop database EmployeeDB
create database EmployeeDB
use EmployeeDB

CREATE TABLE Department(
DepartmentID [int] primary key IDENTITY(1,1) NOT NULL,
DepartmentName [nvarchar](50) NULL
)


CREATE TABLE EmployeeDetail(
EmployeeID int primary key IDENTITY(1,1) NOT NULL,
EmployeeName nvarchar(50) NULL,
DepartmentID int NULL,
Gender varchar(15) NULL,
Age int NULL,
JoiningDate date NULL,
PicPath varchar(250) NULL,
)
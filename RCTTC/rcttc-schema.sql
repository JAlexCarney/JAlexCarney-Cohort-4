USE master;
GO
DROP DATABASE IF EXISTS RCTTC;
GO
CREATE DATABASE RCTTC;
GO
USE RCTTC;

-- Create Tables
CREATE TABLE Customer (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    [Address] NVARCHAR(50) NULL,
    Phone NVARCHAR(50) NULL
);

CREATE TABLE Theater (
    Id INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(50) NOT NULL,
    [Address] NVARCHAR(50) NOT NULL,
    Phone NVARCHAR(50) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
);

CREATE TABLE Show (
    Id INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(50) NOT NULL
);

CREATE TABLE Performance(
    Id INT PRIMARY KEY IDENTITY(1,1),
    TheaterId INT NOT NULL,
    ShowId INT NOT NULL,
    TicketPrice FLOAT NOT NULL,
    [Date] DATE NOT NULL,
    CONSTRAINT fk_Performance_Theater_TheaterId
        FOREIGN KEY (TheaterId)
        REFERENCES Theater(Id),
    CONSTRAINT fk_Performance_Show_ShowId
        FOREIGN KEY (ShowId)
        REFERENCES Show(Id)
);

CREATE TABLE Ticket(
    CustomerId INT NOT NULL,
    PerformanceId INT NOT NULL,
    Seat NVARCHAR(50) NOT NULL,
    CONSTRAINT pk_Ticket
        PRIMARY KEY (CustomerId, PerformanceId, Seat),
    CONSTRAINT fk_Ticket_Customer_CustomerId
        FOREIGN KEY (CustomerID)
        REFERENCES Customer(Id),
    CONSTRAINT fk_Ticket_Performance_PerformanceId
        FOREIGN KEY (PerformanceID)
        REFERENCES Performance(Id),
    CONSTRAINT uq_Ticket_PerformanceId_Seat
        UNIQUE (PerformanceId, Seat)
);
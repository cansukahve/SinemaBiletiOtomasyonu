-- Create the database if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'SinemaDatabase')
BEGIN
    CREATE DATABASE SinemaDatabase;
END
GO

USE SinemaDatabase;
GO

-- Create Users table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE Users (
        UserID INT IDENTITY(1,1) PRIMARY KEY,
        Username NVARCHAR(50) NOT NULL UNIQUE,
        Password NVARCHAR(100) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        FullName NVARCHAR(100) NOT NULL
    );
END

-- Create Movies table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Movies')
BEGIN
    CREATE TABLE Movies (
        MovieID INT IDENTITY(1,1) PRIMARY KEY,
        MovieName NVARCHAR(100) NOT NULL,
        ShowTime DATETIME NOT NULL,
        Price DECIMAL(10,2) NOT NULL,
        PosterPath NVARCHAR(500) NULL
    );
END

-- Create Seats table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Seats')
BEGIN
    CREATE TABLE Seats (
        SeatID INT IDENTITY(1,1) PRIMARY KEY,
        MovieID INT,
        SeatNumber NVARCHAR(10),
        IsOccupied BIT DEFAULT 0,
        IsReserved BIT DEFAULT 0,
        FOREIGN KEY (MovieID) REFERENCES Movies(MovieID)
    );
END
ELSE
BEGIN
    ALTER TABLE Seats
    ADD IsReserved BIT DEFAULT 0;
END

-- Create Bookings table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Bookings')
BEGIN
    CREATE TABLE Bookings (
        BookingID INT IDENTITY(1,1) PRIMARY KEY,
        UserID INT,
        MovieID INT,
        SeatID INT,
        TotalAmount DECIMAL(10,2) NOT NULL,
        BookingDate DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (UserID) REFERENCES Users(UserID),
        FOREIGN KEY (MovieID) REFERENCES Movies(MovieID),
        FOREIGN KEY (SeatID) REFERENCES Seats(SeatID)
    );
END

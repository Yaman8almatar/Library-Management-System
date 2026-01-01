CREATE DATABASE LibraryDB;
USE LibraryDB;

CREATE TABLE Users (
    UserId INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) CHECK (Role IN ('Member','Librarian')) NOT NULL,
    JoinDate DATE NULL,
    EmployeeId NVARCHAR(50) NULL
);

CREATE TABLE Books (
    BookId INT IDENTITY PRIMARY KEY,
    Title NVARCHAR(150) NOT NULL,
    Author NVARCHAR(100) NULL,
    ISBN NVARCHAR(20) NULL,
    TotalCopies INT DEFAULT 1,
    AvailableCopies INT DEFAULT 1
);

CREATE TABLE Loans (
    LoanId INT IDENTITY PRIMARY KEY,
    UserId INT NOT NULL,
    BookId INT NOT NULL,
    StartDate DATE NOT NULL DEFAULT GETDATE(),
    DueDate DATE NULL,
    ReturnDate DATE NULL,
    RenewCount INT DEFAULT 0,
    Status NVARCHAR(20) DEFAULT 'Active',
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (BookId) REFERENCES Books(BookId)
);

CREATE TABLE Reservations (
    ReservationId INT IDENTITY PRIMARY KEY,
    UserId INT NOT NULL,
    BookId INT NOT NULL,
    ReservationDate DATE NOT NULL DEFAULT GETDATE(),
    QueuePosition INT NULL,
    Status NVARCHAR(20) DEFAULT 'Pending',
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (BookId) REFERENCES Books(BookId)
);

CREATE TABLE Fines (
    FineId INT IDENTITY PRIMARY KEY,
    LoanId INT NOT NULL,
    Amount DECIMAL(8,2) NOT NULL DEFAULT 0.00,
    IsPaid BIT DEFAULT 0,
    FOREIGN KEY (LoanId) REFERENCES Loans(LoanId)
);
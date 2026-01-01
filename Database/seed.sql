-- sample users
INSERT INTO Users (FullName, Email, PasswordHash, Role, JoinDate)
VALUES ('Ali Ahmad','ali@test.com','hashed_pw_example','Member','2023-10-01');

INSERT INTO Users (FullName, Email, PasswordHash, Role, EmployeeId)
VALUES ('Fatima Noor','fatima@test.com','hashed_pw_example','Librarian','EMP-001');

-- sample books
INSERT INTO Books (Title, Author, ISBN, TotalCopies, AvailableCopies)
VALUES ('Intro to Algorithms','Cormen', '9780262033848', 3, 3),
       ('Clean Code','Robert C. Martin', '9780132350884', 2, 2);
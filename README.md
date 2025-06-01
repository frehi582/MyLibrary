Firehiwot Tesfaye 1501207
AI tool:chat gpt,deepseek
Library – Desktop Library Management System

MyLibrary  is a Windows Forms (WinForms) desktop application built in C# that allows management of a small library’s books and borrowers. It supports login authentication, book and borrower CRUD operations, book issuing/returning, and basic reporting features. The application uses a SQL-based database and ADO.NET for data access.
**Project Objectives
- Apply event-driven programming in a desktop environment.
- Build responsive UI with WinForms.
- Implement Create, Read, Update, Delete (CRUD) operations with ADO.NET.
- Manage data validation, form events, and exception handling.
- Enable database integration for persistent storage
---
**Technologies Used
 Language : C#                       
 UI Framework: Windows Forms (.NET)    
Database:  SQL Server / SQLite      
Data Access:  ADO.NET (parameterized) 
 IDE: Visual Studio            

---

 ** Features
 
 Login Form
- Username and Password authentication.
- Validates credentials against a `Users` table.
- Displays error messages on failed login.

 Books Management
- View list of books with: BookID, Title, Author, Year, AvailableCopies.
- Add, Edit, and Delete books with validation (empty fields, numeric ranges).
- Uses `DataGrid View` for display.
 Borrowers Management
- Manage borrower records (BorrowerID, Name, Email, Phone).
- Add, Edit, Delete borrower entries with input validation.
Book Issue/Return
- Issue Book:
  - Select a book and a borrower.
  - Decrease `AvailableCopies`.
  - Insert into `Issued Books` table (IssueID, BookID, BorrowerID, IssueDate, DueDate).
- Return Book:
  - Select issued record to return.
  - Increase `AvailableCopies`.
  - Remove or flag record as returned.

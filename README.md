# Book_MVC_Application

## Description

This project is a web application for managing books. It allows users to view a list of books, sort them by different criteria, and perform bulk operations such as inserting books into the database.

## Prerequisites

- .NET Core SDK
- Visual Studio or Visual Studio Code

## Installation

1. Clone the repository.
2. Open the solution file in Visual Studio or the project folder in Visual Studio Code.
3. Replace the connection setting in appsetting.json according to your DB(SQL).
4. Click Tools->Nuget console and run add-mirgration [Any name] and run update-database.
5. Build the solution to restore dependencies.
6. Run the application.

## Usage

### Viewing Books

- Navigate to the home page to view a list of books.
- Books are displayed with details such as title, author, publisher, price, publication year, and medium.

### Sorting Books

- Click on the "Sort by Publisher" link to sort books alphabetically by publisher.
- Click on the "Sort by Author" link to sort books alphabetically by author.
- Click on the "Sort by Publisher (Stored Procedure)" link to sort books using a stored procedure.
- Click on the "Sort by Author (Stored Procedure)" link to sort books by author using a stored procedure.

### Bulk Inserting Books

- Click on the "Save List" link to bulk insert books into the database.

### Total Price of Books

- Click on the "Get Total Price of Books" link to calculate the total price of all books.

## Credits

- Created by Naveenraja Rajakulendran.

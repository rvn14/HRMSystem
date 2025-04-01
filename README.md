# HRM_System

A Windows-based HRM (Human Resource Management) system built using WPF, C#, and MySQL. This project demonstrates a login system with custom animations and a modern UI powered by Material Design XAML.

## Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Database Setup](#database-setup)
  - [Using SQL Scripts](#using-sql-scripts)
  - [Using Docker Compose](#using-docker-compose)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [Contributing](#contributing)
- [License](#license)

## Overview

HRM_System is a WPF application that implements a custom login view, animations for window transitions, and integration with a MySQL database. The project follows MVVM principles and offers a sleek, modern UI using Material Design.

## Features

- **Custom Login UI:** A login screen with custom animations and a non-standard title bar.
- **Window Transitions:** Smooth animations for minimizing, maximizing, and closing the window.
- **MySQL Integration:** Connects to a MySQL database to authenticate users.
- **MVVM Architecture:** Clean separation of concerns with a dedicated view model.
- **Docker Support (Optional):** Easily set up a MySQL instance using Docker Compose.

## Prerequisites

- [.NET Framework](https://dotnet.microsoft.com/download) (or .NET Core/5/6 depending on your project setup)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- [Docker](https://www.docker.com/) (if using the Docker option)
- Visual Studio or your preferred C# IDE

## Installation

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/yourusername/HRM_System.git
   cd HRM_System
Restore NuGet Packages:

Open the solution in Visual Studio and restore NuGet packages if they do not load automatically.

Database Setup
Using SQL Scripts
Schema and Seed Data:

In the /Database folder, you will find schema.sql (and optionally seed.sql). These scripts create the necessary database and tables.

Example schema.sql:

sql
Copy
-- schema.sql
CREATE DATABASE IF NOT EXISTS voltexdb;
USE voltexdb;

CREATE TABLE IF NOT EXISTS users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(100) NOT NULL,
    Password VARCHAR(255) NOT NULL
    -- Add additional fields as needed
);
Import the SQL Scripts:

Use your MySQL client or command line to import the schema:

bash
Copy
mysql -u your_user -p < path/to/schema.sql
Using Docker Compose
For an easier setup, you can use Docker Compose to spin up a MySQL container.

Create a docker-compose.yml file (if not provided):

yaml
Copy
version: "3.8"
services:
  db:
    image: mysql:8.0
    restart: always
    environment:
      MYSQL_ROOT_PASSWORD: DJdas12345
      MYSQL_DATABASE: voltexdb
      MYSQL_USER: your_user
      MYSQL_PASSWORD: your_password
    ports:
      - "3306:3306"
    volumes:
      - db_data:/var/lib/mysql
volumes:
  db_data:
Run Docker Compose:

bash
Copy
docker-compose up -d
This will launch a MySQL server on port 3306 with the specified credentials and database.

Configuration
Connection Strings:
Update your connection string in your configuration file (e.g., appsettings.json or another config file) to match your database settings.

Example configuration:

json
Copy
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=voltexdb;Uid=your_user;Pwd=your_password;"
  }
}
Environment Variables:
Alternatively, store sensitive details (like passwords) in environment variables and reference them in your configuration files.

.gitignore:
Make sure that local configuration files containing sensitive data (e.g., appsettings.local.json) are added to your .gitignore file.

Running the Application
Build the Project:

Open the solution in Visual Studio and build the project.

Run:

Start the application. The login view should appear. After a successful login, the main view will open.

Contributing
Contributions are welcome! Follow these steps:

Fork the Repository

Create a Feature Branch:

bash
Copy
git checkout -b feature/your-feature-name
Commit Your Changes

Push to Your Branch:

bash
Copy
git push origin feature/your-feature-name
Submit a Pull Request

Please ensure your code adheres to the existing style and include tests where applicable.

License
This project is licensed under the MIT License - see the LICENSE file for details.

Happy coding! Let’s keep this project lit and collaborative.

yaml
Copy

---

This **README.md** covers all the necessary details: setting up the database (via SQL scripts or Docker

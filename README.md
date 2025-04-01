# HRM_System 🚀

A modern, WPF-based Human Resource Management System built with C#, MVVM, and MySQL. This project showcases a custom login system with slick animations and a Material Design-inspired UI.

---

## Table of Contents 📚

- [Overview](#overview-)
- [Features ✨](#features-)
- [Prerequisites 🔧](#prerequisites-)
- [Installation 💻](#installation-)
- [Database Setup 🗄️](#database-setup-)
  - [Using SQL Scripts](#using-sql-scripts)
  - [Using Docker Compose](#using-docker-compose)
- [Configuration ⚙️](#configuration-)
- [Running the Application ▶️](#running-the-application-)
- [Contributing 🤝](#contributing-)
- [License 📄](#license-)

---

## Overview 📝

**HRM_System** is a feature-rich desktop application designed to streamline HR processes. Built on WPF and C#, it integrates with a MySQL database to manage user authentication and more—all while providing a sleek, responsive UI with custom animations.

---

## Features ✨

- **Custom Login UI:** Modern login screen with custom animations.
- **Smooth Transitions:** Fade, slide, and drag animations for window actions.
- **MVVM Architecture:** Clean separation of concerns.
- **MySQL Integration:** Robust database connectivity for user authentication.
- **Docker Support:** Easily spin up a MySQL instance using Docker Compose.

---

## Prerequisites 🔧

- [.NET Framework / .NET 5+](https://dotnet.microsoft.com/download)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- [Docker](https://www.docker.com/) (for Docker Compose option)
- [Visual Studio](https://visualstudio.microsoft.com/) or your favorite C# IDE

---

## Installation 💻

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/yourusername/HRM_System.git
   cd HRM_System
Restore NuGet Packages:

Open the solution in Visual Studio and restore the packages if they aren’t loaded automatically.

Database Setup 🗄️
Using SQL Scripts
Schema & Seed Data:

In the /Database folder, you’ll find:

schema.sql – Contains all necessary SQL commands to create the database and tables.

seed.sql (optional) – Contains sample data for testing.

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

Use your MySQL client or CLI to run the script:

bash
Copy
mysql -u your_user -p < path/to/schema.sql
Using Docker Compose
For a hassle-free setup, use Docker Compose to run a MySQL container:

Docker Compose File:

Create a docker-compose.yml file with the following content:

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
This command will launch a MySQL server on port 3306 with the specified credentials and database.

Configuration ⚙️
Connection Strings:
Update your connection string in your configuration file (e.g., appsettings.json or a similar config file) to match your database settings.

Example:

json
Copy
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=voltexdb;Uid=your_user;Pwd=your_password;"
  }
}
Sensitive Files:
Ensure any local configuration files containing secrets (e.g., appsettings.local.json) are added to your .gitignore.

Running the Application ▶️
Build the Project:

Open the solution in Visual Studio and build the project.

Run the App:

Launch the application. The login screen will appear—sign in to access the main interface.

Contributing 🤝
Contributions are welcome! Follow these steps to contribute:

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

Please adhere to the existing coding style and include tests when applicable.

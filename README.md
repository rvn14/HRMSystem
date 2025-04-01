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

# Project Setup Guide

## Restore NuGet Packages
1. Open the solution in **Visual Studio**.
2. Restore packages if they aren’t loaded automatically.

---

## Database Setup 🗄️ 

### Using SQL Scripts (Schema & Seed Data)
- **Files**:
  - `schema.sql`: Creates the database and tables.
  - `seed.sql` (optional): Contains sample test data.

**Example `schema.sql`**:
```sql
CREATE DATABASE IF NOT EXISTS voltexdb;
USE voltexdb;

CREATE TABLE IF NOT EXISTS users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    Username VARCHAR(100) NOT NULL,
    Password VARCHAR(255) NOT NULL
    -- Additional fields here
);

## Contributing 🤝

Contributions are welcome! Follow these steps to contribute:

1. **Fork the Repository**

2. **Create a Feature Branch:**

   ```bash
   git checkout -b feature/your-feature-name
Commit Your Changes

Push to Your Branch:

bash
Copy
git push origin feature/your-feature-name
Submit a Pull Request

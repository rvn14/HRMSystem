# Create the Department table
CREATE TABLE Department (
    DepartmentName VARCHAR(255) PRIMARY KEY,
    Emp_Count INT
);

# Create the User table
CREATE TABLE Users (
    Username VARCHAR(255) PRIMARY KEY,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(50)
);

# Create the Employee table
CREATE TABLE Employee (
    EmployeeID VARCHAR(20) PRIMARY KEY,
    Username VARCHAR(255) NOT NULL,
    DepartmentName VARCHAR(255),
    Name VARCHAR(255) NOT NULL,
    NIC VARCHAR(50) UNIQUE,
    Phone VARCHAR(50),
    Email VARCHAR(255) UNIQUE,
    Position VARCHAR(100),
    BankAccNo VARCHAR(100) UNIQUE,
    RemainingLeaves INT,
    StartDate DATE,
    BasicSalary DECIMAL(10, 2),
    FOREIGN KEY (Username) REFERENCES Users(Username),
    FOREIGN KEY (DepartmentName) REFERENCES Department(DepartmentName)
);

# Create the Attendance table
# Has a composite primary key
CREATE TABLE Attendance (
    EmployeeID VARCHAR(20),
    Date DATE,
    CheckinTime TIME,
    CheckOutTime TIME,
    TotalHours DECIMAL(5, 2),
    PRIMARY KEY (EmployeeID, Date), # Composite primary key
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

# Create the Feedback table
CREATE TABLE Feedback (
    FeedBackID INT PRIMARY KEY AUTO_INCREMENT,
    EmployeeID VARCHAR(20),
    Description TEXT,
    Date DATE,
    Time TIME,
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

# Create the LeaveRequest table
CREATE TABLE LeaveRequest (
    LeaveID INT PRIMARY KEY AUTO_INCREMENT,
    EmployeeID VARCHAR(20),
    LeaveType VARCHAR(50),
    StartDate DATE,
    EndDate DATE,
    Reason TEXT,
    Status VARCHAR(50), # 'Pending', 'Approved', 'Rejected'
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

# Create the Payroll table
# Has a composite primary key (EmployeeID, Month)
CREATE TABLE Payroll (
    EmployeeID VARCHAR(20),
    Month VARCHAR(10), # '2025/05'
    StartDate DATE,
    EndDate DATE,
    Status VARCHAR(50), # 'Processed', 'Paid', 'Pending'
    GrossPay DECIMAL(10, 2),
    Deductions DECIMAL(10, 2),
    NetPay DECIMAL(10, 2),
    PRIMARY KEY (EmployeeID, Month), # Composite primary key
    FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
);

# Create the PayComponents table
CREATE TABLE PayComponents (
    PCID INT PRIMARY KEY AUTO_INCREMENT,
    EmployeeID VARCHAR(20),
    Month VARCHAR(10),
    Type VARCHAR(100),
    Amount DECIMAL(10, 2),
    FOREIGN KEY (EmployeeID, Month) REFERENCES Payroll(EmployeeID, Month)
);


# Ensure database context if necessary (e.g., USE your_database_name;)

# Insert Departments
# Setting Emp_Count initially to 0. Can be updated later.
INSERT INTO Department (DepartmentName, Emp_Count) VALUES
('HR', 0),
('IT', 0),
('Finance', 0);

# Insert Users
# Using placeholder password hashes. Replace with actual secure hashes in a real system.
INSERT INTO Users (Username, PasswordHash, Role) VALUES
('jdoe', 'hashed_password_1', 'employee'),
('asmith', 'hashed_password_2', 'employee'),
('pwilson', 'hashed_password_3', 'employee'),
('mlee', 'hashed_password_4', 'employee'),
('rbrown', 'hashed_password_5', 'employee'),
('schen', 'hashed_password_6', 'employee'),
('davis_hr', 'hashed_password_hr', 'HR manager'), # HR Manager User
('admin_sys', 'hashed_password_admin', 'sysadmin');   # System Admin User

# Insert Employees
# Assigning users and departments, using specified EmployeeID format and salary range.
INSERT INTO Employee (EmployeeID, Username, DepartmentName, Name, NIC, Phone, Email, Position, BankAccNo, RemainingLeaves, StartDate, BasicSalary) VALUES
('E001', 'jdoe', 'IT', 'John Doe', 'NIC123456V', '555-1111', 'john.doe@example.com', 'Software Engineer', 'BANKACC001', 18, '2023-05-15', 95000.00),
('E002', 'asmith', 'IT', 'Alice Smith', 'NIC234567V', '555-2222', 'alice.smith@example.com', 'Senior Software Engineer', 'BANKACC002', 20, '2022-08-20', 120000.00),
('E003', 'pwilson', 'IT', 'Peter Wilson', 'NIC345678V', '555-3333', 'peter.wilson@example.com', 'System Administrator', 'BANKACC003', 22, '2023-11-01', 88000.00),
('E004', 'mlee', 'HR', 'Mei Lee', 'NIC456789V', '555-4444', 'mei.lee@example.com', 'HR Assistant', 'BANKACC004', 15, '2024-01-10', 60000.00),
('E005', 'davis_hr', 'HR', 'David Davis', 'NIC567890V', '555-5555', 'david.davis@example.com', 'HR Manager', 'BANKACC005', 20, '2021-03-12', 110000.00),
('E006', 'rbrown', 'Finance', 'Robert Brown', 'NIC678901V', '555-6666', 'robert.brown@example.com', 'Accountant', 'BANKACC006', 19, '2022-06-25', 85000.00),
('E007', 'schen', 'Finance', 'Susan Chen', 'NIC789012V', '555-7777', 'susan.chen@example.com', 'Financial Analyst', 'BANKACC007', 21, '2023-09-01', 105000.00);

# Update Department Employee Counts (Optional but good practice)
UPDATE Department SET Emp_Count = (SELECT COUNT(*) FROM Employee WHERE DepartmentName = 'HR') WHERE DepartmentName = 'HR';
UPDATE Department SET Emp_Count = (SELECT COUNT(*) FROM Employee WHERE DepartmentName = 'IT') WHERE DepartmentName = 'IT';
UPDATE Department SET Emp_Count = (SELECT COUNT(*) FROM Employee WHERE DepartmentName = 'Finance') WHERE DepartmentName = 'Finance';


# Insert Attendance Records (for April 2025)
INSERT INTO Attendance (EmployeeID, Date, CheckinTime, CheckOutTime, TotalHours) VALUES
('E001', '2025-04-28', '08:55:00', '17:35:00', 8.67),
('E001', '2025-04-29', '09:05:00', '17:30:00', 8.42),
('E002', '2025-04-28', '08:30:00', '17:00:00', 8.50),
('E002', '2025-04-29', '08:40:00', '17:15:00', 8.58),
('E004', '2025-04-28', '09:10:00', '17:05:00', 7.92),
('E006', '2025-04-29', '08:58:00', '17:32:00', 8.57);

# Insert Feedback
INSERT INTO Feedback (EmployeeID, Description, Date, Time) VALUES
('E001', 'Good performance on the recent project deployment.', '2025-04-15', '14:30:00'),
('E004', 'Needs to improve response time on HR queries.', '2025-04-22', '10:00:00'),
('E006', 'Excellent attention to detail in the Q1 financial report.', '2025-04-10', '11:15:00');
# Note: FeedBackID is AUTO_INCREMENT, so it's not specified here.

# Insert Leave Requests
INSERT INTO LeaveRequest (EmployeeID, LeaveType, StartDate, EndDate, Reason, Status) VALUES
('E001', 'Vacation', '2025-05-10', '2025-05-15', 'Family trip', 'Approved'),
('E002', 'Sick', '2025-04-25', '2025-04-25', 'Flu symptoms', 'Approved'),
('E004', 'Personal', '2025-05-20', '2025-05-21', 'Personal appointment', 'Pending'),
('E007', 'Vacation', '2025-06-01', '2025-06-10', 'Annual leave', 'Pending'),
('E001', 'Sick', '2025-03-10', '2025-03-11', 'Cold', 'Approved'); # Past leave
# Note: LeaveID is AUTO_INCREMENT, so it's not specified here.

# Insert Payroll Records (for April 2025)
# Assuming payroll for April 2025 has been processed and paid for most.
# GrossPay, Deductions, NetPay are illustrative. Real calculations would be more complex.
INSERT INTO Payroll (EmployeeID, Month, StartDate, EndDate, Status, GrossPay, Deductions, NetPay) VALUES
('E001', '2025/04', '2025-04-01', '2025-04-30', 'Paid', 97000.00, 15000.00, 82000.00), # Basic + small allowance
('E002', '2025/04', '2025-04-01', '2025-04-30', 'Paid', 125000.00, 25000.00, 100000.00),# Basic + allowance
('E003', '2025/04', '2025-04-01', '2025-04-30', 'Paid', 88000.00, 12000.00, 76000.00), # Basic only
('E004', '2025/04', '2025-04-01', '2025-04-30', 'Paid', 60000.00, 8000.00, 52000.00),  # Basic only
('E005', '2025/04', '2025-04-01', '2025-04-30', 'Paid', 115000.00, 22000.00, 93000.00), # Basic + allowance
('E006', '2025/04', '2025-04-01', '2025-04-30', 'Paid', 85000.00, 11500.00, 73500.00), # Basic only
('E007', '2025/04', '2025-04-01', '2025-04-30', 'Processed', 108000.00, 18000.00, 90000.00); # Basic + small allowance, not yet marked as Paid

# Insert Pay Components (for April 2025 Payroll)
# Matching the Payroll entries above.
# Note: PCID is AUTO_INCREMENT.
# Employee E001
INSERT INTO PayComponents (EmployeeID, Month, Type, Amount) VALUES
('E001', '2025/04', 'Basic Salary', 95000.00),
('E001', '2025/04', 'IT Allowance', 2000.00), # Adds up to Gross Pay
('E001', '2025/04', 'Tax Deduction', 10000.00),
('E001', '2025/04', 'Provident Fund', 5000.00); # Adds up to Deductions

# Employee E002
INSERT INTO PayComponents (EmployeeID, Month, Type, Amount) VALUES
('E002', '2025/04', 'Basic Salary', 120000.00),
('E002', '2025/04', 'Performance Bonus', 5000.00), # Adds up to Gross Pay
('E002', '2025/04', 'Tax Deduction', 18000.00),
('E002', '2025/04', 'Provident Fund', 7000.00); # Adds up to Deductions

# Employee E003
INSERT INTO PayComponents (EmployeeID, Month, Type, Amount) VALUES
('E003', '2025/04', 'Basic Salary', 88000.00), # Adds up to Gross Pay
('E003', '2025/04', 'Tax Deduction', 8000.00),
('E003', '2025/04', 'Provident Fund', 4000.00); # Adds up to Deductions

# Employee E004
INSERT INTO PayComponents (EmployeeID, Month, Type, Amount) VALUES
('E004', '2025/04', 'Basic Salary', 60000.00), # Adds up to Gross Pay
('E004', '2025/04', 'Tax Deduction', 5000.00),
('E004', '2025/04', 'Provident Fund', 3000.00); # Adds up to Deductions

# Employee E005
INSERT INTO PayComponents (EmployeeID, Month, Type, Amount) VALUES
('E005', '2025/04', 'Basic Salary', 110000.00),
('E005', '2025/04', 'Management Allowance', 5000.00), # Adds up to Gross Pay
('E005', '2025/04', 'Tax Deduction', 15000.00),
('E005', '2025/04', 'Provident Fund', 7000.00); # Adds up to Deductions

# Employee E006
INSERT INTO PayComponents (EmployeeID, Month, Type, Amount) VALUES
('E006', '2025/04', 'Basic Salary', 85000.00), # Adds up to Gross Pay
('E006', '2025/04', 'Tax Deduction', 7500.00),
('E006', '2025/04', 'Provident Fund', 4000.00); # Adds up to Deductions

# Employee E007
INSERT INTO PayComponents (EmployeeID, Month, Type, Amount) VALUES
('E007', '2025/04', 'Basic Salary', 105000.00),
('E007', '2025/04', 'Travel Allowance', 3000.00), # Adds up to Gross Pay
('E007', '2025/04', 'Tax Deduction', 12000.00),
('E007', '2025/04', 'Provident Fund', 6000.00); # Adds up to Deductions
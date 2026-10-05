# 🏫 HIT Campus Housing Portal (HIT CHP)

An accommodation application and management portal built for Harare Institute of Technology (HIT) students and residence administrators.

---

## 📌 Project Overview

The **HIT Campus Housing Portal (HIT CHP)** streamlines the on-campus residence application process for students while providing housing administrators with real-time statistics, room allocation tools, and application processing capabilities.

Originally created as a visual programming assignment (*ISS1201 Practical Assignment 1*), the project has been fully overhauled into a modern, production-ready 3-tier architecture backed by **MySQL 8.0** and secure **SHA-256** password hashing.

---

## ✨ Features

### 🎓 Student Features
- **Account Registration & Password Setup**:
  - Full profile creation with Registration Number validation.
  - Dynamic selection of HIT Schools (SIET, SIST, SAHS, SBMS, SIIT) and Departments.
  - Secure password hashing during account creation.
- **Student Dashboard**:
  - Live accommodation statistics (total available rooms, male/female room counts).
  - Real-time application status tracker.
- **Res Accommodation Application**:
  - Apply for on-campus hostels based on gender target.
  - Select 1st and 2nd room preferences and state application reasons.
- **Student Profile & Account Management**:
  - View personal info, academic details, and next of kin / guardian contact information.

### 🛡️ Admin Features
- **Admin Dashboard**:
  - Overview cards displaying total accommodation requests, approved applications, rejected applications, and remaining room inventory.
- **Application Management**:
  - Review pending student applications.
  - Approve applications with automatic room allocation and room count updates.
  - Reject applications with administrative remarks.
- **Student Directory**:
  - View all registered students in a formatted DataGridView with search & filter capability.

---

## 🛠️ Technology Stack

- **Frontend / Presentation Layer**: C# Windows Forms (WinForms) .NET Framework 4.8
- **Backend / Service Layer**: Layered Service & Repository pattern
- **Database Engine**: **MySQL 8.0** (`hit_chp_db`)
- **Data Access Driver**: `MySql.Data` 8.0.33 (ADO.NET with parameterized queries)
- **Security**: SHA-256 password hashing (`PasswordHasher`)
- **Build System**: MSBuild / Visual Studio 2022

---

## 📁 Repository & Project Architecture

```
HIT CHP/                                # Workspace Root Directory
├── README.md                           # Documentation & Setup Guide
├── hit_chp_db.sql                      # MySQL 8.0 DDL Database Schema & Seed Data
├── task.txt                            # Original project assignment requirements
└── HIT Campus Housing Portal/          # Visual Studio C# WinForms Project Root
    ├── App.config                      # Connection strings & runtime configuration
    ├── Program.cs                      # Main application startup entry point
    ├── HIT Campus Housing Portal.csproj # Project build definition
    ├── Data/                           # Database Connectivity & Repositories
    │   ├── DbConnectionFactory.cs      # MySQL connection provider
    │   └── Repositories/
    │       ├── ApplicationRepository.cs
    │       ├── RoomRepository.cs
    │       ├── StudentRepository.cs
    │       └── UserRepository.cs
    ├── Models/                         # Domain Entity Models
    │   ├── Application.cs
    │   ├── Department.cs
    │   ├── Room.cs
    │   ├── Student.cs
    │   └── User.cs
    ├── Services/                       # Business Logic Layer
    │   ├── AdminService.cs             # Application approvals & admin stats
    │   ├── AuthService.cs              # Registration & Login authentication
    │   ├── PasswordHasher.cs          # SHA-256 hashing service
    │   ├── StudentService.cs            # Student application submission
    │   └── UserSession.cs              # Global active user context manager
    └── UI/                             # Presentation Layer
        ├── Forms/                      # Window Forms
        │   ├── AdminDash.cs            # Admin shell container
        │   ├── Home.cs                 # Main landing screen
        │   ├── Login.cs                # Login screen
        │   ├── Password.cs             # Password creation form
        │   ├── SignUp.cs               # Student sign up form
        │   └── StudentDash.cs          # Student shell container
        └── Controls/                   # Modular UserControl Views
            ├── Admin/                  # Admin UI Controls
            │   ├── UC_adminDash.cs     # Live admin stats dashboard
            │   ├── UC_Applications.cs  # Applications queue view
            │   ├── UC_ManageAppli.cs   # Application approval/rejection panel
            │   └── UC_Students.cs      # Registered student grid
            └── Student/                # Student UI Controls
                ├── UC_Apply.cs         # Application info screen
                ├── UC_Main.cs          # Hostel application form
                ├── UC_Status.cs        # Application status tracker
                ├── UC_studAcc.cs       # Account settings
                ├── UC_studDash.cs      # Student live stats dashboard
                ├── UC_studProf.cs      # Student profile card
                └── UC_studSett.cs      # User preferences
```

---

## 🚀 Setup & Installation Instructions

### 1. Prerequisites
- **Operating System**: Windows 10 / 11
- **IDE**: Visual Studio 2022 (with *.NET desktop development* workload installed)
- **Database**: **MySQL Server 8.0** (running locally on port `3306`)

### 2. Clone the Repository
```bash
git clone https://github.com/NigelMupira/hit-campus-housing-portal.git
cd hit-campus-housing-portal
```

### 3. Database Import
1. Open MySQL Command Line Client, MySQL Workbench, or phpMyAdmin.
2. Create and seed the database using [`hit_chp_db.sql`](hit_chp_db.sql):
   - **Via MySQL Command Line**:
     ```sql
     SOURCE hit_chp_db.sql;
     ```
   - **Or via Terminal / Shell**:
     ```bash
     mysql -u root -p < hit_chp_db.sql
     ```
   *This creates the `hit_chp_db` database, table structures, and initial seed data for schools, departments, hostels, rooms, and default admin accounts.*

### 4. Connection String Configuration
Inspect [`HIT Campus Housing Portal/App.config`](HIT%20Campus%20Housing%20Portal/App.config) and update the connection details if your MySQL root password is set:

```xml
<connectionStrings>
  <add name="HIT_CHP_MySQL" 
       connectionString="Server=localhost;Port=3306;Database=hit_chp_db;Uid=root;Pwd=YOUR_MYSQL_PASSWORD;SslMode=Preferred;" 
       providerName="MySql.Data.MySqlClient" />
</connectionStrings>
```

### 5. Build & Run

#### Option A: Visual Studio 2022 (Recommended)
1. Open `HIT Campus Housing Portal.sln` in Visual Studio 2022.
2. Press **F5** (or click **Start**) to build and run the application.

#### Option B: Developer Command Prompt for Visual Studio
```cmd
msbuild "HIT Campus Housing Portal.sln" /t:Rebuild /p:Configuration=Debug
```
Run the executable:
```cmd
"HIT Campus Housing Portal\bin\Debug\HIT Campus Housing Portal.exe"
```

---

## 🔑 Default Login Credentials

| Role | Username | Password | Notes |
| :--- | :--- | :--- | :--- |
| **Admin** | `admin` | `12345678` | System Administrator Account |
| **Admin** | `CHPdevs` | `12345678` | Secondary Admin Account |
| **Student** | *(Reg Number)* | *(Created password)* | Register via Sign Up on landing page |

---

## 🔐 Security & Architecture Best Practices

1. **Password Security**: Passwords are never saved in plain text. Hashing is performed using SHA-256 via `PasswordHasher`.
2. **SQL Injection Prevention**: All queries use parameterized `MySqlParameter` bindings in ADO.NET.
3. **Transaction Safety**: Application approval and room assignments execute within ACID MySQL transactions (`MySqlTransaction`) to prevent double-booking.
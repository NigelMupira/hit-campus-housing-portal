-- =========================================================
-- HIT Campus Housing Portal (HIT CHP)
-- MySQL 8.0 Database Schema & Seed Data Script
-- =========================================================

CREATE DATABASE IF NOT EXISTS hit_chp_db;
USE hit_chp_db;

-- ---------------------------------------------------------
-- 1. USERS & AUTHENTICATION TABLE
-- ---------------------------------------------------------
DROP TABLE IF EXISTS room_assignments;
DROP TABLE IF EXISTS applications;
DROP TABLE IF EXISTS rooms;
DROP TABLE IF EXISTS hostels;
DROP TABLE IF EXISTS students;
DROP TABLE IF EXISTS departments;
DROP TABLE IF EXISTS schools;
DROP TABLE IF EXISTS users;

CREATE TABLE users (
    user_id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE, -- Student RegNumber or Admin Username
    password_hash VARCHAR(255) NOT NULL, -- SHA-256 Hashed Password
    role ENUM('Student', 'Admin') NOT NULL DEFAULT 'Student',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------
-- 2. SCHOOLS TABLE
-- ---------------------------------------------------------
CREATE TABLE schools (
    school_id INT AUTO_INCREMENT PRIMARY KEY,
    school_code VARCHAR(10) NOT NULL UNIQUE, -- SIET, SIST, SAHS, SBMS, SIIT
    school_name VARCHAR(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------
-- 3. DEPARTMENTS TABLE
-- ---------------------------------------------------------
CREATE TABLE departments (
    dept_id INT AUTO_INCREMENT PRIMARY KEY,
    school_id INT NOT NULL,
    dept_code VARCHAR(10) NOT NULL UNIQUE, -- e.g. HIIT, HBME, ISSM
    dept_name VARCHAR(100) NOT NULL,
    FOREIGN KEY (school_id) REFERENCES schools(school_id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------
-- 4. STUDENTS PROFILE TABLE
-- ---------------------------------------------------------
CREATE TABLE students (
    student_id INT PRIMARY KEY, -- Maps 1:1 to users.user_id
    reg_number VARCHAR(15) NOT NULL UNIQUE,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    dob DATE NOT NULL,
    gender CHAR(1) NOT NULL, -- 'M' or 'F'
    national_id VARCHAR(20) NOT NULL UNIQUE,
    part INT NOT NULL DEFAULT 1,
    dept_id INT NOT NULL,
    phone VARCHAR(20) NOT NULL,
    email VARCHAR(100) NOT NULL,
    hit_mail VARCHAR(100) NOT NULL,
    address VARCHAR(200) NOT NULL,
    guardian_name VARCHAR(100) NOT NULL,
    guardian_phone VARCHAR(20) NOT NULL,
    guardian_email VARCHAR(100) NOT NULL,
    guardian_relationship CHAR(1) NOT NULL DEFAULT 'P', -- 'P' (Parent) or 'G' (Guardian)
    FOREIGN KEY (student_id) REFERENCES users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (dept_id) REFERENCES departments(dept_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------
-- 5. HOSTELS TABLE
-- ---------------------------------------------------------
CREATE TABLE hostels (
    hostel_id INT AUTO_INCREMENT PRIMARY KEY,
    hostel_name VARCHAR(50) NOT NULL,
    gender_target ENUM('M', 'F', 'Mixed') NOT NULL,
    capacity INT NOT NULL DEFAULT 100
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------
-- 6. ROOMS TABLE
-- ---------------------------------------------------------
CREATE TABLE rooms (
    room_id INT AUTO_INCREMENT PRIMARY KEY,
    hostel_id INT NOT NULL,
    room_number VARCHAR(10) NOT NULL,
    capacity INT NOT NULL DEFAULT 2,
    occupied_count INT NOT NULL DEFAULT 0,
    is_available BOOLEAN AS (occupied_count < capacity) STORED,
    FOREIGN KEY (hostel_id) REFERENCES hostels(hostel_id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------
-- 7. APPLICATIONS TABLE
-- ---------------------------------------------------------
CREATE TABLE applications (
    application_id INT AUTO_INCREMENT PRIMARY KEY,
    student_id INT NOT NULL,
    hostel_id INT NOT NULL,
    preferred_room_1 VARCHAR(10) NOT NULL,
    preferred_room_2 VARCHAR(10) NULL,
    reason VARCHAR(350) NULL,
    status ENUM('Pending', 'Approved', 'Rejected') NOT NULL DEFAULT 'Pending',
    admin_remarks VARCHAR(250) NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    processed_at TIMESTAMP NULL,
    FOREIGN KEY (student_id) REFERENCES users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (hostel_id) REFERENCES hostels(hostel_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- ---------------------------------------------------------
-- 8. ROOM ASSIGNMENTS TABLE
-- ---------------------------------------------------------
CREATE TABLE room_assignments (
    assignment_id INT AUTO_INCREMENT PRIMARY KEY,
    application_id INT NOT NULL UNIQUE,
    student_id INT NOT NULL,
    room_id INT NOT NULL,
    assigned_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (application_id) REFERENCES applications(application_id) ON DELETE CASCADE,
    FOREIGN KEY (student_id) REFERENCES users(user_id) ON DELETE CASCADE,
    FOREIGN KEY (room_id) REFERENCES rooms(room_id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- =========================================================
-- SEED DATA
-- =========================================================

-- Insert Schools
INSERT INTO schools (school_code, school_name) VALUES
('SIET', 'School of Engineering and Technology'),
('SIST', 'School of Information Science and Technology'),
('SAHS', 'School of Allied Health Sciences'),
('SBMS', 'School of Business and Management Sciences'),
('SIIT', 'School of Industrial Sciences and Technology');

-- Insert Departments
INSERT INTO departments (school_id, dept_code, dept_name) VALUES
(1, 'HBME', 'Biomedical Engineering'),
(1, 'HIEE', 'Electronic Engineering'),
(1, 'HIME', 'Industrial & Manufacturing Engineering'),
(2, 'HIIT', 'Information Technology'),
(2, 'HICS', 'Computer Science'),
(2, 'HISE', 'Software Engineering'),
(2, 'HISA', 'Information Security & Assurance'),
(3, 'AHPH', 'Pharmacy'),
(3, 'AHDR', 'Radiography'),
(4, 'HIFE', 'Financial Engineering'),
(4, 'ISSM', 'Software Engineering Management'),
(5, 'BioT', 'Biotechnology'),
(5, 'HFPT', 'Food Processing Technology');

-- Insert Hostels (Male and Female hostels)
INSERT INTO hostels (hostel_name, gender_target, capacity) VALUES
('Hostel 1 (Male)', 'M', 100),
('Hostel 2 (Female)', 'F', 100),
('Hostel 3 (Male)', 'M', 100),
('Hostel 4 (Female)', 'F', 100);

-- Insert Rooms for Hostel 1 & 2
INSERT INTO rooms (hostel_id, room_number, capacity, occupied_count) VALUES
(1, '101', 2, 0),
(1, '102', 2, 0),
(1, '103', 2, 0),
(1, '104', 2, 0),
(1, '105', 2, 0),
(2, '201', 2, 0),
(2, '202', 2, 0),
(2, '203', 2, 0),
(2, '204', 2, 0),
(2, '205', 2, 0);

-- Insert Default Admin User (Password SHA-256 for '12345678': ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f)
INSERT INTO users (username, password_hash, role) VALUES
('admin', 'ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f', 'Admin'),
('CHPdevs', 'ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f', 'Admin');

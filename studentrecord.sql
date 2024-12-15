-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 15, 2024 at 02:56 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `studentrecord`
--

-- --------------------------------------------------------

--
-- Table structure for table `tblacademic_history`
--

CREATE TABLE `tblacademic_history` (
  `ID` int(11) NOT NULL,
  `studentID` varchar(255) DEFAULT NULL,
  `College` varchar(255) DEFAULT NULL,
  `Course` varchar(255) DEFAULT NULL,
  `Yearlevel` varchar(255) NOT NULL,
  `Status` varchar(255) DEFAULT NULL,
  `Semester` varchar(50) DEFAULT NULL,
  `SchoolYear` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblacademic_history`
--

INSERT INTO `tblacademic_history` (`ID`, `studentID`, `College`, `Course`, `Yearlevel`, `Status`, `Semester`, `SchoolYear`) VALUES
(2, '24-0000-251', 'CCS', 'BSIT', '', 'Regular', '1st Semester', '2024-2025'),
(3, '24-0000-251', 'CCS', 'BSIT', '', 'Regular', '1st Semester', '2025-2026'),
(6, '24-0000-251', 'CCS', 'BSIT', '1st Year', 'Regular', '2nd Semester', '2025-2026'),
(8, '24-0000-251', 'CCS', 'BSIT', '1st Year', 'Ireg', '2nd Semester', '2025-2026'),
(9, '24-0000-252', 'CBA', 'BSME', '2', 'Regular', '2nd', '2024-2025'),
(10, '24-0000-253', 'CCS', 'BSIT', '4', 'Regular', '1st Semester', '2024-2025'),
(11, '24-0000-254', 'CCS', 'BSIT', '4', 'Regular', '1st Semester', '2024-2025');

-- --------------------------------------------------------

--
-- Table structure for table `tblstudents`
--

CREATE TABLE `tblstudents` (
  `ID` int(11) NOT NULL,
  `Idno` varchar(255) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Gender` varchar(255) NOT NULL,
  `Contactno` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tblstudents`
--

INSERT INTO `tblstudents` (`ID`, `Idno`, `Name`, `Gender`, `Contactno`, `Password`) VALUES
(14, '24-0000-251', 'Paul', 'Male', '09283788349', '$2y$10$wz1yipcECNbrD0O.B7nOsulVl16b39HKGr5wHVRKEIduklCzrs58S'),
(15, '24-0000-252', 'Rich', 'Male', '09283788349', '$2y$10$KfY0TQYLenyyLq1U6fxtcOd3hfVX3p4c6/zqnpL8Cy0ynpbaCHXaS'),
(16, '24-0000-253', 'Avien', 'Female', '09283788349', '$2y$10$x3ET5UNQEPnFYu25lH4GaeT5n7nRH1kZmhCKlGQUbQbZMxmTt9pYS'),
(17, '24-0000-254', 'Lhizel', 'Female', '09283788349', '$2y$10$i1MLhPDWQi6p3CXQQNs1seEaGBKvp.uvaIkJ8JN7q4X9k4MScu7Ie');

-- --------------------------------------------------------

--
-- Table structure for table `tbl_attendance`
--

CREATE TABLE `tbl_attendance` (
  `Id` int(11) NOT NULL,
  `Studentidno` varchar(255) NOT NULL,
  `Entrytime` datetime NOT NULL,
  `Exittime` datetime DEFAULT NULL,
  `Status` enum('IN','OUT') DEFAULT 'IN'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tbl_attendance`
--

INSERT INTO `tbl_attendance` (`Id`, `Studentidno`, `Entrytime`, `Exittime`, `Status`) VALUES
(16, '24-0000-251', '2024-12-14 19:08:38', '2024-12-14 19:08:41', 'OUT'),
(17, '24-0000-252', '2024-12-15 21:03:52', '2024-12-15 21:03:54', 'OUT'),
(18, '24-0000-252', '2024-12-15 21:04:03', NULL, 'IN'),
(19, '24-0000-252', '2024-12-15 21:04:05', NULL, 'IN'),
(20, '24-0000-252', '2024-12-15 21:04:08', NULL, 'IN'),
(21, '24-0000-251', '2024-12-15 21:34:09', '2024-12-15 21:34:15', 'OUT');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tblacademic_history`
--
ALTER TABLE `tblacademic_history`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `studentID` (`studentID`);

--
-- Indexes for table `tblstudents`
--
ALTER TABLE `tblstudents`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `Idno` (`Idno`);

--
-- Indexes for table `tbl_attendance`
--
ALTER TABLE `tbl_attendance`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `StudentIdno` (`Studentidno`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tblacademic_history`
--
ALTER TABLE `tblacademic_history`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT for table `tblstudents`
--
ALTER TABLE `tblstudents`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `tbl_attendance`
--
ALTER TABLE `tbl_attendance`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `tblacademic_history`
--
ALTER TABLE `tblacademic_history`
  ADD CONSTRAINT `tblacademic_history_ibfk_1` FOREIGN KEY (`studentID`) REFERENCES `tblstudents` (`Idno`) ON DELETE CASCADE;

--
-- Constraints for table `tbl_attendance`
--
ALTER TABLE `tbl_attendance`
  ADD CONSTRAINT `tbl_attendance_ibfk_1` FOREIGN KEY (`Studentidno`) REFERENCES `tblstudents` (`Idno`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

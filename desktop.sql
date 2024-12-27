-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Dec 19, 2024 at 02:32 AM
-- Server version: 8.0.30
-- PHP Version: 8.3.13

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `desktop`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin`
--

CREATE TABLE `admin` (
  `username` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `admin`
--

INSERT INTO `admin` (`username`, `password`) VALUES
('admin', 'admin');

-- --------------------------------------------------------

--
-- Table structure for table `karyawan`
--

CREATE TABLE `karyawan` (
  `nik` varchar(200) NOT NULL,
  `nama` varchar(200) NOT NULL,
  `jabatan` varchar(200) NOT NULL,
  `alamat` varchar(200) NOT NULL,
  `email` varchar(200) NOT NULL,
  `nohp` varchar(200) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `karyawan`
--

INSERT INTO `karyawan` (`nik`, `nama`, `jabatan`, `alamat`, `email`, `nohp`) VALUES
('12123132', 'Bayu Ardi', 'Bos', 'Suruh Tani', 'bayuardi@gmail.com', '085866748293'),
('11239842', 'Davin Wahyu Amanta', 'Seketaris Bos', 'Jungke, Karanganyar', 'davinwahyu@yahoo.com', '085866734298'),
('21123123', 'Rafif Izzudin', 'Admin', 'Bolong, Lalung, Karanganyar', 'izzudinanjay@gmail.com', '085867839948'),
('12532', 'arfan', 'budak', 'tw', 'tw@gmail.com', '085673457'),
('9990123921', 'sad', 'sadsd', 'ad', 'das', 'asdasd'),
('21313', 'dasfsdfsdf', 'aaaaasfaf', 'aaafsdfsdf', 'aasadasdasd', 'aaadasdasdasd');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

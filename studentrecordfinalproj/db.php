<?php
$servername = "localhost";
$username = "testuser";
$password = "testuser";
$dbname = "studentrecord";

$conn = new mysqli($servername, $username, $password, $dbname);

if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}
?>
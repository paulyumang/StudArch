<?php
include 'db.php';

$data = json_decode(file_get_contents("php://input"));

// Get the current year
$currentYear = date("y");

// Fetch the last inserted ID from the database
$sql = "SELECT Idno FROM tblstudents ORDER BY Id DESC LIMIT 1";
$result = $conn->query($sql);
$lastIdno = $result->num_rows > 0 ? $result->fetch_assoc()['Idno'] : null;

$incrementedPart = $lastIdno ? intval(substr($lastIdno, -3)) + 1 : 0;
$newIdno = sprintf("%s-%04d-%03d", $currentYear, 0, $incrementedPart);

// Set the password to be the same as the ID number but hashed
$newPassword = password_hash($newIdno, PASSWORD_DEFAULT);

$name = $data->name;
$gender = $data->gender;
$contact = $data->contactno;


$sql = "INSERT INTO tblstudents (Idno, Name, Gender, Contactno, Password)
VALUES ('$newIdno', '$name', '$gender', '$contact', '$newPassword')";

if ($conn->query($sql) === TRUE) {
    echo json_encode(["message" => "User added successfully"]);
} else {
    echo json_encode(["message" => "Error: " . $conn->error]);
}

$conn->close();
?>
<?php
include 'db.php';

// Set timezone to Philippines
date_default_timezone_set('Asia/Manila');

$data = json_decode(file_get_contents("php://input"), true);

// Debug incoming data
file_put_contents("log.txt", print_r($data, true), FILE_APPEND);

if (isset($data['Studentidno'])) { // Ensure the key matches
    $studentIdno = $data['Studentidno']; // Use 'Studentidno'
    $currentDateTime = date("Y-m-d H:i:s"); // Database format

    $sql = "INSERT INTO tbl_attendance (Studentidno, Entrytime, Status) VALUES ('$studentIdno', '$currentDateTime', 'IN')";

    if ($conn->query($sql) === TRUE) {
        echo json_encode(["message" => "Entry recorded successfully"]);
    } else {
        echo json_encode(["message" => "Error: " . $conn->error]);
    }
} else {
    echo json_encode(["message" => "Invalid data"]);
}

$conn->close();
?>

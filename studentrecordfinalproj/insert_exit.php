<?php
include 'db.php';

// Set timezone to Philippines
date_default_timezone_set('Asia/Manila');

$data = json_decode(file_get_contents("php://input"), true);

if (isset($data['Studentidno'])) { // Use the correct key
    $studentIdno = $data['Studentidno']; // Match case here
    $currentDateTime = date("Y-m-d H:i:s"); // Database format

    $sql = "UPDATE tbl_attendance SET Exittime = '$currentDateTime', Status = 'OUT' 
            WHERE Studentidno = '$studentIdno' AND Status = 'IN' 
            ORDER BY Entrytime DESC LIMIT 1";

    if ($conn->query($sql) === TRUE) {
        echo json_encode(["message" => "Exit recorded successfully"]);
    } else {
        echo json_encode(["message" => "Error: " . $conn->error]);
    }
} else {
    echo json_encode(["message" => "Invalid data"]);
}

$conn->close();
?>

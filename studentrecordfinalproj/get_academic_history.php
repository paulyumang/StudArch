<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");

include 'db.php';

// Get the studentID from the GET request
$studentID = $_GET['studentID'];  // Get the studentID sent via query parameter

// Check if the studentID is provided
if (empty($studentID)) {
    echo json_encode(["message" => "Student ID is required"]);
    exit;
}

// SQL query to fetch academic history records for the student
$sql = "SELECT * FROM tblacademic_history WHERE studentID = '$studentID'";
$result = $conn->query($sql);

// Check if there are any results
if ($result->num_rows > 0) {
    $history = [];
    while ($row = $result->fetch_assoc()) {
        $history[] = $row;  // Add each record to the result array
    }
    echo json_encode($history);  // Return the academic history as JSON
} else {
    echo json_encode(["message" => "No academic history found for this student"]);
}

$conn->close();
?>

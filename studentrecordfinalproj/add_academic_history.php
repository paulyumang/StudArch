<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");

include 'db.php';

// Get the input data
$data = json_decode(file_get_contents("php://input"));

// Validate input
if (!isset($data->studentID) || !isset($data->college) || !isset($data->course) ||!isset($data->yearlevel) || !isset($data->status) || !isset($data->semester) || !isset($data->schoolYear)) {
    echo json_encode(["message" => "All fields are required."]);
    exit;
}

// Get the data from the request
$studentID = $data->studentID;
$college = $data->college;
$course = $data->course;
$yearlevel = $data->yearlevel;
$status = $data->status;
$semester = $data->semester;
$schoolYear = $data->schoolYear;

// SQL query to insert academic history
$sql = "INSERT INTO tblacademic_history (studentID, College, Course, Yearlevel, Status, Semester, SchoolYear) 
        VALUES ('$studentID', '$college', '$course', '$yearlevel', '$status', '$semester', '$schoolYear')";

// Execute the query
if ($conn->query($sql) === TRUE) {
    echo json_encode(["message" => "Academic history added successfully"]);
} else {
    echo json_encode(["message" => "Error: " . $conn->error]);
}

$conn->close();
?>

<?php
include 'db.php';

// Set timezone to Philippines
date_default_timezone_set('Asia/Manila');

if (isset($_GET['Studentidno'])) {
    $studentIdno = $_GET['Studentidno'];

    // Sanitize the input to prevent SQL injection
    $studentIdno = mysqli_real_escape_string($conn, $studentIdno);

    // Query to retrieve attendance records for the specific 'Studentidno'
    $query = "SELECT * FROM tbl_attendance WHERE Studentidno = '$studentIdno' ORDER BY Entrytime DESC";

    // Execute the query
    $result = mysqli_query($conn, $query);

    if ($result) {
        $attendanceData = array();

        // Fetch the data
        while ($row = mysqli_fetch_assoc($result)) {
            $attendanceData[] = array(
                'Studentidno' => $row['Studentidno'],  // Use exact case
                'Entrytime' => date("F d, Y h:i:s A", strtotime($row['Entrytime'])), // Convert Entrytime
                'Exittime' => $row['Exittime'] ? date("F d, Y h:i:s A", strtotime($row['Exittime'])) : "Not yet exited", // Handle NULL
                'Status' => $row['Status']             // Use exact case
            );            
        }

        // Return the data as JSON
        echo json_encode($attendanceData);
    } else {
        // Query failed
        echo json_encode(["error" => "Query failed: " . mysqli_error($conn)]);
    }
} else {
    // Handle missing 'Studentidno' parameter
    echo json_encode(["error" => "Missing 'Studentidno' parameter"]);
}

$conn->close();
?>

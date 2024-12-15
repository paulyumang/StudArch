<?php
include 'db.php';

// SQL Query to retrieve all students and their current academic history (if available)
$sql = "
    SELECT 
        tblstudents.Idno AS IDNO,
        tblstudents.Name AS Name,
        tblacademic_history.College AS College,
        tblacademic_history.Course AS Course,
        tblacademic_history.Yearlevel AS YearLevel,
        tblacademic_history.Semester AS Semester,
        tblacademic_history.SchoolYear AS SchoolYear
    FROM tblstudents
    LEFT JOIN tblacademic_history
    ON tblacademic_history.studentID = tblstudents.Idno
    AND tblacademic_history.ID = (
        SELECT MAX(ID) 
        FROM tblacademic_history 
        WHERE tblacademic_history.studentID = tblstudents.Idno
    );
";

$result = $conn->query($sql);

$data = [];

if ($result->num_rows > 0) {
    // Fetch rows into an associative array
    while ($row = $result->fetch_assoc()) {
        $data[] = [
            'IDNO' => $row['IDNO'],
            'Name' => $row['Name'],
            'College' => $row['College'] ?? null, // Handle null values
            'Course' => $row['Course'] ?? null,
            'YearLevel' => $row['YearLevel'] ?? null,
            'Semester' => $row['Semester'] ?? null,
            'SchoolYear' => $row['SchoolYear'] ?? null
        ];
    }
}

// Return the data as JSON
header('Content-Type: application/json');
echo json_encode($data);

$conn->close();
?>

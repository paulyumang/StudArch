<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");

include 'db.php';

// Query to join tblstudents and tblacademic_history based on the common ID
$sql = "SELECT 
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
    while ($row = $result->fetch_assoc()) {
        $data[] = $row;
    }
}

// Return the result as JSON
echo json_encode($data);

$conn->close();
?>

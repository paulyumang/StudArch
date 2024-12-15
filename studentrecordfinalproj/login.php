<?php
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");

include 'db.php';

$data = json_decode(file_get_contents("php://input"));

if (isset($data->idno) && isset($data->password)) {
    $idno = $data->idno;  // Check for 'Name' instead of 'Idno'
    $password = $data->password;

    // Query the database to find the student by Name
    $sql = "SELECT * FROM tblstudents WHERE Idno = '$idno'";
    $result = $conn->query($sql);

    if ($result->num_rows > 0) {
        $user = $result->fetch_assoc();

        // Verify the password
        if (password_verify($password, $user['Password'])) {
            // Return student data without the password
            unset($user['Password']);
            echo json_encode(["message" => "Login successful", "user" => $user]);
        } else {
            echo json_encode(["message" => "Invalid password"]);
        }
    } else {
        echo json_encode(["message" => "Student not found"]);
    }
} else {
    echo json_encode(["message" => "Missing Name or Password"]);
}

$conn->close();
?>

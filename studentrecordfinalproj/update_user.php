<?php
include 'db.php';

$data = json_decode(file_get_contents("php://input"));

$id = $data->id;
$idno = $data->idno;
$name = $data->name;
$gender = $data->gender;
$contactno = $data->contactno;
$password = $data->password;

$sql = "UPDATE tblstudents SET Idno='$idno', Name='$name', Gender='$gender', Contactno='$contactno', Password='$password' WHERE ID=$id";

if ($conn->query($sql) === TRUE) {
    echo json_encode(["message" => "User updated successfully"]);
} else {
    echo json_encode(["message" => "Error: " . $conn->error]);
}

$conn->close();
?>
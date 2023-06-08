<?php
$serverName = "DESKTOP-J5SD8TT\\SQLEXPRESS";
$connectionOptions = array(
    "Database" => "gameDB",
    "Uid" => "player",
    "PWD" => "player"
);
$conn = sqlsrv_connect($serverName, $connectionOptions);
if($conn === false) {
    die(print_r(sqlsrv_errors(), true));
}



$idCharacter = $_POST['idCharacter'];

// Realizar la consulta SELECT en la tabla upgrades
$query = "SELECT * FROM weapons WHERE idCharacter = ?";
$params = array($idCharacter);
$stmt = sqlsrv_query($conn, $query, $params);

if($stmt === false) {
    die(print_r(sqlsrv_errors(), true));
}

$rows = array();

while($row = sqlsrv_fetch_array($stmt, SQLSRV_FETCH_ASSOC)) {
    $rows[] = $row;
}

echo json_encode($rows);

sqlsrv_free_stmt($stmt);
sqlsrv_close($conn);
?>

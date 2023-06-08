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

$query = "SELECT * FROM saves";
$result = sqlsrv_query($conn, $query);

if($result === false) {
    die(print_r(sqlsrv_errors(), true));
}

$rows = array();

while($row = sqlsrv_fetch_array($result, SQLSRV_FETCH_ASSOC)) {
    $rows[] = $row;
}

echo json_encode($rows);

sqlsrv_free_stmt($result);
sqlsrv_close($conn);
?>

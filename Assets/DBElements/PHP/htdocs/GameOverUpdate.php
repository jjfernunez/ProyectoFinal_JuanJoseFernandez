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


// Obtener los valores de moneySaved y amountOfKills enviados desde Unity
$moneySaved = $_POST['moneySaved'];
$amountOfKills = $_POST['amountOfKills'];
$time_played = $_POST['time_played'];
$id = $_POST['id'];

// Actualizar los valores en la tabla save
$query = "UPDATE Saves SET moneySaved = ?, amountOfKills = ?, time_played = ? where id = ?";
$params = array($moneySaved, $amountOfKills, $time_played, $id);
$stmt = sqlsrv_query($conn, $query, $params);

if ($stmt === false) {
    die(print_r(sqlsrv_errors(), true));
}

// Devolver una respuesta exitosa a Unity
$response = array("status" => "success");
echo json_encode($response);

sqlsrv_free_stmt($stmt);
sqlsrv_close($conn);
?>

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


// Obtener la cantidad de dinero enviada desde Unity
$moneySaved = $_POST['moneySaved'];

// Actualizar el campo moneySaved en la tabla saves
$sql = "UPDATE saves SET moneySaved = ?";
$params = array($moneySaved);
$stmt = sqlsrv_query($conn, $sql, $params);
if (!$stmt) {
    die("Error al actualizar el campo moneySaved: " . sqlsrv_errors());
}

// Enviar una respuesta al cliente
$response = array('message' => 'El campo moneySaved ha sido actualizado exitosamente');
echo json_encode($response);

// Cerrar la conexión a la base de datos
sqlsrv_free_stmt($stmt);
sqlsrv_close($conn);

?>

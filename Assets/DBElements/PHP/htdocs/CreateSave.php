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

// Obtener los datos enviados desde Unity
$id = $_POST['id'];
$nombre = $_POST['name'];
$time_played = $_POST['time_played'];

// Crear una nueva comuna en la base de datos
$sql = "INSERT INTO saves (id, name, time_played) VALUES (?, ?, ?)";
$params = array($id, $nombre, $time_played);
$stmt = sqlsrv_query($conn, $sql, $params);

if ($stmt === false) {
    // Si ocurrió un error en la consulta, manejarlo aquí
    die( print_r( sqlsrv_errors(), true));
} else {
    // Si la consulta se realizó con éxito, devolver una respuesta al cliente
    $response = array(
        'status' => 'ok',
        'message' => 'Comuna creada con éxito'
    );
    echo json_encode($response);

}
sqlsrv_free_stmt($result);
sqlsrv_close($conn);
?>

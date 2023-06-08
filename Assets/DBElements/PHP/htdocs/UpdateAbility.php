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
$idHab = $_POST['idHab'];
$idPlayer = $_POST['idPlayer'];
$name = $_POST['name'];
$abilityLevel = $_POST['abilityLevel'];
$increaseAmount = $_POST['increaseAmount'];
$upgradeCost = $_POST['upgradeCost'];
$description = $_POST['description'];

// Actualizar la habilidad en la base de datos
$sql = "UPDATE Ability SET
            idPlayer = ?,
            name = ?,
            abilityLevel = ?,
            increaseAmount = ?,
            upgradeCost = ?,
            description = ?
        WHERE idHab = ?";
$params = array($idPlayer, $name, $abilityLevel, $increaseAmount, $upgradeCost, $description, $idHab);
$stmt = sqlsrv_query($conn, $sql, $params);
if (!$stmt) {
    die("Error al actualizar la habilidad: " . sqlsrv_errors());
}

// Enviar una respuesta al cliente
echo "La habilidad con ID $idHab ha sido actualizada exitosamente.";

// Cerrar la conexión a la base de datos
sqlsrv_free_stmt($stmt);
sqlsrv_close($conn);
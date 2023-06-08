<?php
// Conexión a la base de datos
$serverName = "DESKTOP-J5SD8TT\\SQLEXPRESS";
$connectionOptions = array(
    "Database" => "gameDB",
    "Uid" => "player",
    "PWD" => "player"
);
$conn = sqlsrv_connect( $serverName, $connectionOptions);

if( $conn ) {
    
}else{
     echo "Conexión no se pudo establecer.<br />";
     die( print_r( sqlsrv_errors(), true));
}

if(isset($_POST['id'])){
    $id = $_POST['id'];
    $tsql = "SELECT * FROM Ability WHERE idPlayer = $id";
    $stmt = sqlsrv_query( $conn, $tsql);
    if( $stmt === false ) {
        echo "Error en la consulta: ";
        die( print_r( sqlsrv_errors(), true));
    }

    $abilities = array();
    while( $row = sqlsrv_fetch_array( $stmt, SQLSRV_FETCH_ASSOC) ) {
        $abilities[] = $row;
    }

    echo json_encode($abilities);
    sqlsrv_free_stmt( $stmt);
    sqlsrv_close( $conn);
}else{
    echo "No se ha especificado el id a buscar.";
}


?>
<?php

$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_controleestoque');
if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST["apppassword"];
if($appkey != "MoveItem")
{
    echo("Wrong appkey");
    exit();
}

$patrimonio = $_POST["patrimonio"];
$serial = $_POST["serial"];
$usuario = $_POST["usuario"];
$data = $_POST["data"];
$deonde = $_POST["deonde"];
$paraonde = $_POST["paraonde"];

$newmoveitemquery= "INSERT INTO movements(Patrimonio, Serial, Usuario, Data, De_onde, Para_onde) VALUES('". $patrimonio ."', '". $serial ."', '". $usuario ."', '". $data ."', '". utf8_decode($deonde) ."', '". utf8_decode($paraonde) ."');";

if($deonde == "Estoque")
{
    $updatedataquery = "UPDATE Inventario SET Saida_do_estoque = '".$data."', Entrada_no_estoque = '' WHERE Patrimonio = '".$patrimonio."';";  
    mysqli_query($con, $updatedataquery) or die("Date query failed");  
}
if($paraonde == "Estoque")
{
    $updatedataquery = "UPDATE Inventario SET Entrada_no_estoque = '".$data."', Saida_do_estoque = '' WHERE Patrimonio = '".$patrimonio."';";
    mysqli_query($con, $updatedataquery) or die("Date query failed");
}
if($paraonde == "Descarte")
{
    $updateinventarioquery = "UPDATE Inventario SET Local = '".$paraonde."', Status = 'DEFEITO' WHERE Patrimonio = '".$patrimonio."';";
}
else
{
    $updateinventarioquery = "UPDATE Inventario SET Local = '".$paraonde."' WHERE Patrimonio = '".$patrimonio."';";
}


mysqli_query($con, $newmoveitemquery) or die("Movement query failed");

mysqli_query($con, $updateinventarioquery) or die("Location query failed");
echo("Moved");

$con->close();

?>
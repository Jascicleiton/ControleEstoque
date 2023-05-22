<?php

$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_ESF_estoque');
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
$patrimonioclean = filter_var($patrimonio, FILTER_SANITIZE_EMAIL);
$serial = $_POST["serial"];
//$serialClean = filter_var($serial, FILTER_SANITIZE_EMAIL);
$usuario = $_POST["usuario"];
$data = $_POST["data"];
$deonde = $_POST["deonde"];
$paraonde = $_POST["paraonde"];
//$paraondeclean = filter_var($paraonde, FILTER_SANITIZE_EMAIL);

$newmoveitemquery= "INSERT INTO movements(Patrimonio, Serial, Usuario, Data, De_onde, Para_onde) VALUES('". $patrimonioclean ."', '". $serial ."', '". $usuario ."', '". $data ."', '". $deonde ."', '". $paraonde ."');";

if($deonde == "Estoque")
{
    $updatedataquery = "UPDATE Inventario SET Saida_do_estoque = '".$data."', Entrada_no_estoque = '' WHERE Patrimonio = '".$patrimonioclean."';";    
}
if($paraonde == "Estoque")
{
    $updatedataquery = "UPDATE Inventario SET Entrada_no_estoque = '".$data."', Saida_do_estoque = '' WHERE Patrimonio = '".$patrimonioclean."';";
}

$updateinventarioquery = "UPDATE Inventario SET Local = '".$paraonde."' WHERE Patrimonio = '".$patrimonioclean."';";

mysqli_query($con, $newmoveitemquery) or die("Movement query failed");
mysqli_query($con, $updatedataquery) or die("Date query failed");
mysqli_query($con, $updateinventarioquery) or die("Location query failed");
 echo("Item moved");

$con->close();

?>
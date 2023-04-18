<?php

$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Quisto');
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

$itemname = $_POST["itemname"];
$itemQuantity = $_POST["itemQuantity"];
$usuario = $_POST["usuario"];
$data = $_POST["data"];
$deonde = $_POST["deonde"];
$paraonde = $_POST["paraonde"];

$newmoveitemquery= "INSERT INTO NoPaNoSeMovements(NomeDoItem, Quantidade, Usuario, Data, De_onde, Para_onde) VALUES('". utf8_decode($itemname) ."', '". $itemQuantity ."', '". $usuario ."', '". $data ."', '". utf8_decode($deonde) ."', '". utf8_decode($paraonde) ."');";

mysqli_query($con, $newmoveitemquery) or die("Movement query failed");


$con->close();

?>
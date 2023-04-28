<?php
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Testing');

if(mysqli_connect_errno())
{
    echo("Conection error");
    exit();
}

$appkey = $_POST["apppassword"];
if($appkey != "UpdateItem")
{
    echo("Wrong app key");
    exit();
}

$itemname = $_POST["itemname"];
$itemQuantity = $_POST["itemQuantity"];

$updateQuery = "UPDATE NoPaNoSe SET Name = '".utf8_decode($itemname)."', Quantity = '".$itemQuantity."' WHERE Name = '".utf8_decode($itemname)."';";
mysqli_query($con, $updateQuery) or die("Inventario update failed");
echo("Updated");

$con->close();
?>
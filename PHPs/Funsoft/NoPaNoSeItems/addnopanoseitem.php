<?php
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Fumsoft_estoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$itemname = $_POST["itemname"];
$itemQuantity = $_POST["itemQuantity"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$insertuserquery= "INSERT INTO NoPaNoSe(Name, Quantity) VALUES('". utf8_decode($itemname) ."', '". $itemQuantity ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
echo("Item added");

$con->close();
?>
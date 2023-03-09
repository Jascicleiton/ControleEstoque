<?php
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Concert');

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

$aquisicao = $_POST["aquisicao"];
$entrada = $_POST["entrada"];
$patrimonio = $_POST["patrimonio"];
$status = $_POST["status"];
$serial = $_POST["serial"];
$categoria = $_POST["categoria"];
$fabricante = $_POST["fabricante"];
$modelo = $_POST["modelo"];
$local = $_POST["local"];
$Pessoa = $_POST["Pessoa"];
$Centro_de_Custo = $_POST["Centro_de_Custo"];
$saida = $_POST["saida"];

$updateQuery = "UPDATE Inventario SET Aquisicao = '".$aquisicao."',Entrada_no_estoque = '".$entrada."', Patrimonio = '".$patrimonio."', Status  = '".utf8_decode($status)."', Serial = '".utf8_decode($serial)."', Categoria = '".utf8_decode($categoria)."', Fabricante = '".utf8_decode($fabricante)."', Modelo = '".$modelo."', Local = '".utf8_decode($local)."', Pessoa = '".utf8_decode($Pessoa)."', Centro_de_Custo = '".utf8_decode($Centro_de_Custo)."', Saida_do_estoque  = '".$saida."' WHERE Patrimonio = '".$patrimonio."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();
?>
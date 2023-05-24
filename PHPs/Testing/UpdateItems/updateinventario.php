<?php
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Testing');

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
$saida = $_POST["saida"];
$observacao = $_POST["observacao"];

$updateQuery = "UPDATE Inventario SET Entrada_no_estoque = '".$entrada."', Patrimonio = '".$patrimonio."', Status  = '".utf8_decode($status)."', Serial = '".$serial."', Categoria = '".utf8_decode($categoria)."', Fabricante = '".utf8_decode($fabricante)."', Modelo = '".$modelo."', Local = '".utf8_decode($local)."', Saida_do_estoque  = '".$saida."', Observacao = '".utf8_decode($observacao)."' WHERE Patrimonio = '".$patrimonio."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();
?>
<?php
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Estoque_Clientes');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
if($appkey != "RecoverBackup")
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

$patrimoniocheckquery = "SELECT * from Inventario WHERE Patrimonio = '" .$patrimonio. "';";
$patrimoniocheckresult = mysqli_query($con, $patrimoniocheckquery) or die ("Patrimônio query failed");

if($patrimoniocheckresult->num_rows > 0)
{
    $updateQuery = "UPDATE Inventario SET Entrada_no_estoque = '".$entrada."', Patrimonio = '".$patrimonio."', Status  = '".utf8_decode($status)."', Serial = '".$serial."', Categoria = '".utf8_decode($categoria)."', Fabricante = '".utf8_decode($fabricante)."', Modelo = '".$modelo."', Local = '".utf8_decode($local)."', Saida_do_estoque  = '".$saida."', Observacao = '".utf8_decode($observacao)."' WHERE Patrimonio = '".$patrimonio."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");
}
else
{
    $insertuserquery= "INSERT INTO Inventario(Entrada_no_estoque, Patrimonio, Status, Serial, Categoria, Fabricante, Modelo, Local, Saida_do_estoque, Observacao, Aquisicao) VALUES('". $entrada ."', '". $patrimonio ."', '". utf8_decode($status) ."', '". $serial ."', '". utf8_decode($categoria) ."', '". utf8_decode($fabricante) ."', '". $modelo ."', '". utf8_decode($local) ."', '". $saida ."', '". utf8_decode($observacao) ."', '".$aquisicao."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Worked");
}

$con->close();
?>
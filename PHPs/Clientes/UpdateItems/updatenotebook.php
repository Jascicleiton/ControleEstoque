<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Estoque_Clientes');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$patrimonio = $_POST["patrimonio"];
$modelo = $_POST["modelo"];
$fabricante = $_POST["fabricante"];
$hd = $_POST["hd"];
$memoria = $_POST["memoria"];
$EntradaRJ45 = $_POST["EntradaRJ45"];
$bateria = $_POST["bateria"];
$adaptadorac = $_POST["adaptadorac"];
$windows = $_POST["windows"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Notebook SET Modelo = '".$modelo."', Fabricante = '".utf8_decode($fabricante)."', HD = '".utf8_decode($hd)."', Memoria = '".utf8_decode($memoria)."', EntradaRJ45 = '".utf8_decode($EntradaRJ45)."', Bateria = '".utf8_decode($bateria)."', AdaptdadorAC = '".utf8_decode($adaptadorac)."', Windows = '".utf8_decode($windows)."' WHERE Patrimonio = '".$patrimonio."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");
$con->close();

?>
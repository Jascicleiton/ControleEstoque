<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Concert');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$patrimonio = $_POST["patrimonio"];
$modelo = $_POST["modelo"];
$fabricante = $_POST["fabricante"];
$wireless = $_POST["wireless"];
$quantasentradas = $_POST["quantasentradas"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Roteador SET Patrimonio = '".$patrimonio."', Modelo = '".$modelo."', Fabricante = '".utf8_decode($fabricante)."', Wireless = '".utf8_decode($wireless)."', Quantas_entradas  = '".$quantasentradas."' WHERE Patrimonio = '".$patrimonio."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
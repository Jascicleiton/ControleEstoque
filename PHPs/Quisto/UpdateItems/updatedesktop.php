<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Quisto');

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
$processador = $_POST["processador"];
$windows = $_POST["windows"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Desktop SET Patrimonio = '".$patrimonio."', Modelo = '".$modelo."', Fabricante  = '".utf8_decode($fabricante)."', HD = '".utf8_decode($hd)."', Memoria = '".utf8_decode($memoria)."', Processador = '".utf8_decode($processador)."', Windows = '".utf8_decode($windows)."' WHERE Patrimonio = '".$patrimonio."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");
$con->close();

?>
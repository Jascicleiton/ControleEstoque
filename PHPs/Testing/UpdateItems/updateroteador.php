<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Testing');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$wireless = $_POST["wireless"];
$quantasentradas = $_POST["quantasentradas"];
$bandamax = $_POST["bandamax"];
$voltagem = $_POST["voltagem"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Roteador SET Modelo = '".$modelo."', Wireless = '".utf8_decode($wireless)."', Quantas_entradas  = '".$quantasentradas."', Banda_max = '".$bandamax."', Voltagem = '".$voltagem."' WHERE Modelo = '".$modelo."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
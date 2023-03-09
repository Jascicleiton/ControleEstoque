<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Funsoft_estoque');

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

$updateQuery = "UPDATE Roteador SET Modelo = '".$modelo."', Wireless = '".$wireless."', Quantas_entradas  = '".$quantasentradas."', Banda_max = '".$bandamax."', Voltagem = '".$voltagem."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Testing');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$fabricante = $_POST["fabricante"];
$polegadas = $_POST["polegadas"];
$tiposentradas = $_POST["tiposentradas"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Monitor SET Modelo = '".$modelo."', Fabricante = '".utf8_decode($fabricante)."', Polegadas  = '".$polegadas."', Tipos_de_entrada = '".$tiposentradas."' WHERE Modelo = '".$modelo."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
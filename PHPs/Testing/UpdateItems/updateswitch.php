<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Testing');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$quantasquaisportas = $_POST["quantasquaisportas"];
$capacidademaxporta = $_POST["capacidademaxporta"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Switch SET Modelo = '".$modelo."', Quantas_quais_entradas = '".$quantasquaisportas."', Capacidade_max  = '".$capacidademaxporta."' WHERE Modelo = '".$modelo."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();
?>
<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_ESF_estoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$ondefunciona = $_POST["ondefunciona"];
$voltagem = $_POST["voltagem"];
$amperagem = $_POST["amperagem"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Carregador SET Modelo = '".$modelo."', Onde_funciona = '".$ondefunciona."', Voltagem  = '".$voltagem."', Amperagem = '".$amperagem."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_ESF_estoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$watts = $_POST["watts"];
$ondefunciona = $_POST["ondefunciona"];
$conectores = $_POST["conectores"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Fonte SET Modelo = '".$modelo."', Watts = '".$watts."', Onde_funciona  = '".$ondefunciona."', Conectores = '".$conectores."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
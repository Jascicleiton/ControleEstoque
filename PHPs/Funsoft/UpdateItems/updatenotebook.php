<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Funsoft_estoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$fabricante = $_POST["fabricante"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Notebook SET Modelo = '".$modelo."', Fabricante = '".$fabricante."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");
$con->close();

?>
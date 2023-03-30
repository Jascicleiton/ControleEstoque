<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_controleestoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$soquete = $_POST["soquete"];
$nucleosfisicos = $_POST["nucleosfisicos"];
$nucleoslogicos = $_POST["nucleoslogicos"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Processador SET Modelo = '".$modelo."', Soquete = '".$soquete."', Nucleos_fisicos  = '".$nucleosfisicos."', Nucleos_logicos = '".$nucleoslogicos."' WHERE Modelo = '".$modelo."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
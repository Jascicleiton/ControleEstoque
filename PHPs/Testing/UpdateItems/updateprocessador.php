<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Testing');

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
$aceitavirtualizacao = $_POST["aceitavirtualizacao"];
$turboboost = $_POST["turboboost"];
$hyperthreading = $_POST["hyperthreading"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Processador SET Modelo = '".$modelo."', Soquete = '".$soquete."', Nucleos_fisicos  = '".$nucleosfisicos."', Nucleos_logicos = '".$nucleoslogicos."', Aceita_virtualizacao = '".utf8_decode($aceitavirtualizacao)."', Turbo_boost = '".utf8_decode($turboboost)."', Hyper_Threading = '".utf8_decode($hyperthreading)."' WHERE Modelo = '".$modelo."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
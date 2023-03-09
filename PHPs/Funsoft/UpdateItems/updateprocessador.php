<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Funsoft_estoque');

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

$updateQuery = "UPDATE Processador SET Modelo = '".$modelo."', Soquete = '".$fabricante."', Nucleos_fisicos  = '".$tipo."', Nucleos_logicos = '".$capacidade."', Aceita_virtualizacao = '".$velocidade."', Turbo_boost = '".$lowvoltage."', Hyper_Threading = '".$rank."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
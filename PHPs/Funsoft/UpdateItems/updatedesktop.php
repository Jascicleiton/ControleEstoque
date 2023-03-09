<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Funsoft_estoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$patrimonio = $_POST["patrimonio"];
$modeloplacamae = $_POST["modeloplacamae"];
$fonte = $_POST["fonte"];
$memoria = $_POST["memoria"];
$hd = $_POST["hd"];
$placavideo = $_POST["placavideo"];
$placarede = $_POST["placarede"];
$leitordvd = $_POST["leitordvd"];
$processador = $_POST["processador"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Desktop SET Patrimonio = '".$patrimonio."', Modelo_placa_mae = '".$modeloplacamae."', Fonte  = '".$fonte."', Memoria = '".$memoria."', HD = '".$hd."',Placa_de_video = '".$placavideo."',Placa_de_rede = '".$placarede."',Leitor_de_DVD = '".$leitordvd."',Processador = '".$processador."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
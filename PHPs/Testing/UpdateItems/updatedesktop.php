<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Testing');

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

$updateQuery = "UPDATE Desktop SET Patrimonio = '".$patrimonio."', Modelo_placa_mae = '".$modeloplacamae."', Fonte  = '".utf8_decode($fonte)."', Memoria = '".utf8_decode($memoria)."', HD = '".utf8_decode($hd)."',Placa_de_video = '".utf8_decode($placavideo)."',Placa_de_rede = '".utf8_decode($placarede)."',Leitor_de_DVD = '".utf8_decode($leitordvd)."',Processador = '".utf8_decode($processador)."' WHERE Patrimonio = '".$patrimonio."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
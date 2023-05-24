<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Testing');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$quantasentradas = $_POST["quantasentradas"];
$velocidadetransmissao = $_POST["velocidadetransmissao"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Placa_SAS SET Modelo = '".$modelo."', Quantas_entradas = '".$quantasentradas."', Velocidade_de_Transmissao = '".$velocidadetransmissao."' WHERE Modelo = '".$modelo."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();
?>
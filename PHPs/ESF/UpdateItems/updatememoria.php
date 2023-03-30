<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_ESF_estoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$fabricante = $_POST["fabricante"];
$tipo = $_POST["tipo"];
$capacidade = $_POST["capacidade"];
$velocidade = $_POST["velocidade"];
$lowvoltage = $_POST["lowvoltage"];
$rank = $_POST["rank"];
$dimm = $_POST["dimm"];
$taxatransmissao = $_POST["taxatransmissao"];
$simbolo = $_POST["simbolo"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Memoria SET Modelo = '".$modelo."', Fabricante = '".$fabricante."', Tipo  = '".$tipo."', Capacidade = '".$capacidade."', Velocidade = '".$velocidade."', Low_voltage = '".$lowvoltage."', Rank = '".$rank."', DIMM = '".$dimm."', Taxa_de_transmissao = '".$taxatransmissao."', Simbolo = '".$simbolo."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Testing');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$fabricante = $_POST["fabricante"];
$interface = $_POST["interface"];
$quantidadeportas = $_POST["quantidadeportas"];
$quaisconexoes = $_POST["quaisconexoes"];
$suportafibraoptica = $_POST["suportafibraoptica"];
$desempenho = $_POST["desempenho"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Placa_de_rede SET Modelo = '".$modelo."', Fabricante = '".utf8_decode($fabricante)."', Interface  = '".$interface."', Quantidade_de_portas = '".$quantidadeportas."', Quais_portas = '".$quaisconexoes."', Suporta_fibra_optica = '".utf8_decode($suportafibraoptica)."', Desempenho = '".$desempenho."' WHERE Modelo = '".$modelo."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
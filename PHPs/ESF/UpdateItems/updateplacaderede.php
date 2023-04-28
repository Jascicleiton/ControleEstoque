<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_ESF_estoque');

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

$updateQuery = "UPDATE Placa_de_rede SET Modelo = '".$modelo."', Fabricante = '".$fabricante."', Interface  = '".$interface."', Quantidade_de_portas = '".$quantidadeportas."', Quais_portas = '".$quaisconexoes."', Suporta_fibra_optica = '".$suportafibraoptica."', Desempenho = '".$desempenho."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
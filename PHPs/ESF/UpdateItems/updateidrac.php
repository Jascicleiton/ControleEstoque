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
$porta = $_POST["porta"];
$velocidade = $_POST["velocidade"];
$entradasd = $_POST["entradasd"];
$servidoressuportados = $_POST["servidoressuportados"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE iDrac SET Modelo = '".$modelo."', Fabricante = '".$fabricante."', Porta  = '".$porta."', Velocidade = '".$velocidade."',Entrada_SD = '".$entradasd."',Servidores_suportados = '".$servidoressuportados."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
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
$interface = $_POST["interface"];
$tamanho = $_POST["tamanho"];
$formaarmazenamento = $_POST["formaarmazenamento"];
$capacidade = $_POST["capacidade"];
$rpm = $_POST["rpm"];
$velocidade = $_POST["velocidade"];
$enterprise = $_POST["enterprise"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE HD SET Modelo = '".$modelo."', Fabricante = '".$fabricante."', Interface  = '".$interface."', Tamanho = '".$tamanho."', Forma_de_armazenamento = '".$formaarmazenamento."', Capacidade = '".$capacidade."', RPM = '".$rpm."', Velocidade_de_Leitura = '".$velocidade."', Enterprise = '".$enterprise."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
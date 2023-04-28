<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_ESF_estoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$tipoconexao = $_POST["tipoconexao"];
$quantidadeportas = $_POST["quantidadeportas"];
$tiporaid = $_POST["tiporaid"];
$tipohd = $_POST["tipohd"];
$capacidademaxhd = $_POST["capacidademaxhd"];
$quantoshd = $_POST["quantoshd"];
$bateriainclusa = $_POST["bateriainclusa"];
$barramento = $_POST["barramento"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Placa_controladora SET Modelo = '".$modelo."', Tipo_de_conexao = '".$tipoconexao."', Quantidade_de_portas  = '".$quantidadeportas."', Tipos_de_RAID = '".$tiporaid."', Tipos_de_HD = '".$tipohd."', Capacidade_max_HD = '".$capacidademaxhd."', Ate_quantos_HD = '".$quantoshd."', Bateria_inclusa = '".$bateriainclusa."', Barramento = '".$barramento."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_controleestoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$tamanhohd = $_POST["tamanhohd"];
$tiporaid = $_POST["tiporaid"];
$tipohd = $_POST["tipohd"];
$capacidademaxhd = $_POST["capacidademaxhd"];
$quantoshd = $_POST["quantoshd"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$updateQuery = "UPDATE Storage_NAS SET Modelo = '".$modelo."', Tamanho_HD = '".$tamanhohd."', Tipos_de_RAID  = '".$tiporaid."', Tipo_de_HD = '".$tipohd."', Capacidade_max_hd = '".$capacidademaxhd."', Ate_quantos_HDs = '".$quantoshd."' WHERE Modelo = '".$modelo."';";
mysqli_query($con, $updateQuery) or die("Update failed");
echo("Updated");

$con->close();

?>
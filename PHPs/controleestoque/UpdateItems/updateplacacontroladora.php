<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_controleestoque');

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

$modelocheckquery = "SELECT * from Placa_controladora WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Query failed");

if($modelocheckresult->num_rows != 1)
{
    $insertuserquery= "INSERT INTO Placa_controladora(Modelo, Tipo_de_conexao, Quantidade_de_portas, Tipos_de_RAID, Tipo_de_HD, Capacidade_max_HD, Ate_quantos_HD, Bateria_inclusa, Barramento) VALUES('". $modelo ."', '". $tipoconexao ."', '". $quantidadeportas ."', '". $tiporaid ."', '". $tipohd ."', '". $capacidademaxhd ."', '". $quantoshd ."', '". utf8_decode($bateriainclusa) ."', '". $barramento ."');";
    mysqli_query($con, $insertuserquery) or die("insert item failed");
    echo("Worked");
}
else
{
    $updateQuery = "UPDATE Placa_controladora SET Modelo = '".$modelo."', Tipo_de_conexao = '".$tipoconexao."', Quantidade_de_portas  = '".$quantidadeportas."', Tipos_de_RAID = '".$tiporaid."', Tipo_de_HD = '".$tipohd."', Capacidade_max_HD = '".$capacidademaxhd."', Ate_quantos_HD = '".$quantoshd."', Bateria_inclusa = '".utf8_decode($bateriainclusa)."', Barramento = '".$barramento."' WHERE Modelo = '".$modelo."';";
    mysqli_query($con, $updateQuery) or die("Update failed");
    echo("Updated");
}

$con->close();
?>
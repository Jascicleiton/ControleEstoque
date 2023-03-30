<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_ESF_estoque');

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

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Placa_controladora WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Placa_controladora(Modelo, Tipo_de_conexao, Quantidade_de_portas, Tipos_de_RAID, Tipo_de_HD, Capacidade_max_HD, Ate_quantos_HD, Bateria_inclusa, Barramento) VALUES('". $modelo ."', '". $tipoconexao ."', '". $quantidadeportas ."', '". $tiporaid ."', '". $tipohd ."', '". $capacidademaxhd ."', '". $quantoshd ."', '". $bateriainclusa ."', '". $barramento ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Item added");



?>
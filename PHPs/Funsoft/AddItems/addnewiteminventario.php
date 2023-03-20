<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Fumsoft_estoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];

$aquisicao = $_POST["aquisicao"];
$entrada = $_POST["entrada"];
$patrimonio = $_POST["patrimonio"];
$status = $_POST["status"];
$serial = $_POST["serial"];
$categoria = $_POST["categoria"];
$fabricante = $_POST["fabricante"];
$modelo = $_POST["modelo"];
$local = $_POST["local"];
$saida = $_POST["saida"];
$observacao = $_POST["observacao"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$patrimoniocheckquery = "SELECT * from Inventario WHERE Patrimonio = '" .$patrimonio. "';";
$patrimoniocheckresult = mysqli_query($con, $patrimoniocheckquery) or die ("Patrimônio query failed");

if($patrimoniocheckresult->num_rows > 0)
{
    echo("Patrimônio found");
    exit();
}

$serialcheckquery = "SELECT * from Inventario WHERE Serial = '" .$serial. "';";
$serialcheckresult = mysqli_query($con, $serialcheckquery) or die ("Serial query failed");

if($serialcheckresult->num_rows > 0)
{
    echo("Serial found");
    exit();
}

$insertuserquery= "INSERT INTO Inventario(Entrada_no_estoque, Patrimonio, Status, Serial, Categoria, Fabricante, Modelo, Local, Saida_do_estoque, Observacao, Aquisicao) VALUES('". $entrada ."', '". $patrimonio ."', '". $status ."', '". $serial ."', '". utf8_decode($categoria) ."', '". utf8_decode($fabricante) ."', '". $modelo ."', '". utf8_decode($local) ."', '". $saida ."', '". utf8_decode($observacao) ."', '".$aquisicao."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Item added");

$con->close();

//Error codes
// 1 - Database connection error
// 4 - insert item failed
// 5 - wrong appkey
?>
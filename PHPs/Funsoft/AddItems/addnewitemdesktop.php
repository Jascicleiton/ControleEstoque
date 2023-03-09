<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Funsoft_estoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$patrimonio = $_POST["patrimonio"];
$modeloplacamae = $_POST["modeloplacamae"];
$fonte = $_POST["fonte"];
$memoria = $_POST["memoria"];
$hd = $_POST["hd"];
$placavideo = $_POST["placavideo"];
$placarede = $_POST["placarede"];
$leitordvd = $_POST["leitordvd"];
$processador = $_POST["processador"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Desktop WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Desktop(Patrimonio, Modelo_placa_mae, Fonte, Memoria, HD, Placa_de_video, Placa_de_rede, Leitor_de_DVD, Processador) VALUES('". $patrimonio ."', '". $modeloplacamae ."', '". $fonte ."', '". $memoria ."', '". $hd ."', '". $placavideo ."', '". $placarede ."', '". $leitordvd ."', '". $processador ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Item added");


$con->close();

//Error codes
// 1 - Database connection error
// 4 - insert user failed
// 5 - wrong appkey
?>
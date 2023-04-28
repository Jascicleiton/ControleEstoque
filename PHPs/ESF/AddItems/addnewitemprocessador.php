<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_ESF_estoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$soquete = $_POST["soquete"];
$nucleosfisicos = $_POST["nucleosfisicos"];
$nucleoslogicos = $_POST["nucleoslogicos"];
$aceitavirtualizacao = $_POST["aceitavirtualizacao"];
$turboboost = $_POST["turboboost"];
$hyperthreading = $_POST["hyperthreading"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Processador WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Processador(Modelo, Soquete, Nucleos_fisicos, Nucleos_logicos, Aceita_virtualizacao, Turbo_boost, Hyper_Threading) VALUES('". $modelo ."', '". $soquete ."', '". $nucleosfisicos ."', '". $nucleoslogicos ."', '". $aceitavirtualizacao ."', '". $turboboost ."', '". $hyperthreading ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Item added");


$con->close();

//Error codes
// 1 - Database connection error
// 4 - insert user failed
// 5 - wrong appkey
?>
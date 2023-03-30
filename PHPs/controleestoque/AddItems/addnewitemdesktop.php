<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_controleestoque');

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

$modelocheckquery = "SELECT * from Desktop WHERE Patrimonio = '" .$patrimonio. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Desktop Patrimonio query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Desktop patrimonio found");
    exit();
}

$insertuserquery= "INSERT INTO Desktop(Patrimonio, Modelo_placa_mae, Fonte, Memoria, HD, Placa_de_video, Placa_de_rede, Leitor_de_DVD, Processador) VALUES('". $patrimonio ."', '". $modeloplacamae ."', '". utf8_decode($fonte) ."', '". utf8_decode($memoria) ."', '". utf8_decode($hd) ."', '". utf8_decode($placavideo) ."', '". utf8_decode($placarede) ."', '". utf8_decode($leitordvd) ."', '". utf8_decode($processador) ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Worked");


$con->close();

//Error codes
// 1 - Database connection error
// 4 - insert user failed
// 5 - wrong appkey
?>
<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_ESF_estoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$patrimonio = $_POST["patrimonio"];
$modelo = $_POST["modelo"];
$fabricante = $_POST["fabricante"];
$hd = $_POST["hd"];
$memoria = $_POST["memoria"];
$processador = $_POST["processador"];
$windows = $_POST["windows"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Servidor WHERE Patrimonio = '" .$patrimonio. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Servidor(Patrimonio, Modelo, Fabricante, HD, Memoria, Processador, Windows) VALUES('". $patrimonio ."', '". utf8_decode($modelo) ."', '". utf8_decode($fabricante) ."', '". utf8_decode($hd) ."', '". utf8_decode($memoria) ."', '". utf8_decode($processador) ."', '". utf8_decode($windows) ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Worked");

$con->close();
?>
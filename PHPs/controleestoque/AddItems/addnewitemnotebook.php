<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_controleestoque');

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
$EntradaRJ45 = $_POST["EntradaRJ45"];
$bateria = $_POST["bateria"];
$adaptadorac = $_POST["adaptadorac"];
$windows = $_POST["windows"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Notebook WHERE Patrimonio = '" .$patrimonio. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Notebook(Patrimonio, Modelo, Fabricante, HD, Memoria, EntradaRJ45, Bateria, AdaptdadorAC, Windows) VALUES('". $patrimonio ."','". utf8_decode($modelo) ."','". utf8_decode($fabricante) ."','". utf8_decode($hd) ."','". utf8_decode($memoria) ."','". utf8_decode($EntradaRJ45) ."','". utf8_decode($bateria) ."','". utf8_decode($adaptadorac) ."','". utf8_decode($windows) ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Worked");


$con->close();

//Error codes
// 1 - Database connection error
// 4 - insert user failed
// 5 - wrong appkey
?>
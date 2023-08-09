<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@415263#', 'sysnetpro_controleestoque');

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

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$patrimoniocheckquery = "SELECT * from Notebook WHERE Patrimonio = '" .$patrimonio. "';";
$patrimoniocheckresult = mysqli_query($con, $patrimoniocheckquery) or die ("Query failed");

if($patrimoniocheckresult->num_rows != 1)
{
    $insertuserquery= "INSERT INTO Notebook(Patrimonio, Modelo, Fabricante, HD, Memoria, EntradaRJ45, Bateria, AdaptadorAC, Windows) VALUES('". $patrimonio ."','". utf8_decode($modelo) ."','". utf8_decode($fabricante) ."','". utf8_decode($hd) ."','". utf8_decode($memoria) ."','". utf8_decode($EntradaRJ45) ."','". utf8_decode($bateria) ."','". utf8_decode($adaptadorac) ."','". utf8_decode($windows) ."');";
    mysqli_query($con, $insertuserquery) or die("insert item failed");
     echo("Worked");
}
else
{
    $updateQuery = "UPDATE Notebook SET Modelo = '".$modelo."', Fabricante = '".utf8_decode($fabricante)."', HD = '".utf8_decode($hd)."', Memoria = '".utf8_decode($memoria)."', EntradaRJ45 = '".utf8_decode($EntradaRJ45)."', Bateria = '".utf8_decode($bateria)."', AdaptadorAC = '".utf8_decode($adaptadorac)."', Windows = '".utf8_decode($windows)."' WHERE Patrimonio = '".$patrimonio."';";
    mysqli_query($con, $updateQuery) or die("Update failed");
    echo("Updated");
}
$con->close();
?>
<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Concert');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$patrimonio = $_POST["patrimonio"];
$modelo = $_POST["modelo"];
$fabricante = $_POST["fabricante"];
$quantasquaisportas = $_POST["quantasquaisportas"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Switch WHERE Patrimonio = '" .$patrimonio. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Switch(Patrimonio, Modelo, Fabricante, QuantasQuaisPortas) VALUES('". $patrimonio ."', '". $modelo ."', '". $fabricante ."', '". $quantasquaisportas ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Worked");


$con->close();
?>



            
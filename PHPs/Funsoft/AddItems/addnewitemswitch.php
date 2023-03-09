<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Funsoft_estoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$quantasentradas = $_POST["quantasentradas"];
$capacidademaxporta = $_POST["capacidademaxporta"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Switch WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Switch(Modelo, Quantas_entradas, Capacidade_max) VALUES('". $modelo ."', '". $quantasentradas ."', '". $capacidademaxporta ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Item added");


$con->close();

?>
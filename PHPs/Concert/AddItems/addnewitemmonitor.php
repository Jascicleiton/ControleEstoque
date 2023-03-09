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
$polegadas = $_POST["polegadas"];
$tiposentradas = $_POST["tiposentradas"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Monitor WHERE Patrimonio = '" .$patrimonio. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Monitor(Patrimonio, Modelo, Fabricante, Polegadas, Tipos_de_entrada) VALUES('". $patrimonio ."', '". utf8_decode($modelo) ."', '". utf8_decode($fabricante) ."', '". $polegadas ."', '". $tiposentradas ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
echo("Worked");

$con->close();
?>
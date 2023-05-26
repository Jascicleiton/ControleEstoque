<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_controleestoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$fabricante = $_POST["fabricante"];
$polegadas = $_POST["polegadas"];
$tiposentradas = $_POST["tiposentradas"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Monitor WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Monitor(Modelo, Fabricante, Polegadas, Tipos_de_entrada) VALUES('". $modelo ."', '". utf8_decode($fabricante) ."', '". $polegadas ."', '". $tiposentradas ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
echo("Worked");

$con->close();
?>
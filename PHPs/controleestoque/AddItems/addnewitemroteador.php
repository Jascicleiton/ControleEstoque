<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@415263#', 'sysnetpro_controleestoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$wireless = $_POST["wireless"];
$quantasentradas = $_POST["quantasentradas"];
$bandamax = $_POST["bandamax"];
$voltagem = $_POST["voltagem"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Roteador WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Roteador(Modelo, Wireless, Quantas_entradas, Banda_max, Voltagem) VALUES('". $modelo ."', '". utf8_decode($wireless) ."', '". $quantasentradas ."', '". $bandamax ."', '". $voltagem ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
echo("Worked");

$con->close();
?>
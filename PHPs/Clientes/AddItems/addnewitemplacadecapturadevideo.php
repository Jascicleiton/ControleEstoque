<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Estoque_Clientes');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$quantasentradas = $_POST["quantasentradas"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Placa_de_captura_de_video WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Placa_de_captura_de_video(Modelo, Quantas_entradas) VALUES('". $modelo ."', '". $quantasentradas ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Worked");


$con->close();

//Error codes
// 1 - Database connection error
// 4 - insert user failed
// 5 - wrong appkey
?>
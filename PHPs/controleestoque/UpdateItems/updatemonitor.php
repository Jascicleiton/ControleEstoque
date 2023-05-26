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

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Monitor WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Query failed");

if($modelocheckresult->num_rows != 1)
{
    $insertuserquery= "INSERT INTO Monitor(Modelo, Fabricante, Polegadas, Tipos_de_entrada) VALUES('". $modelo ."', '". utf8_decode($fabricante) ."', '". $polegadas ."', '". $tiposentradas ."');";
    mysqli_query($con, $insertuserquery) or die("insert item failed");
    echo("Worked");
}
else
{
    $updateQuery = "UPDATE Monitor SET Modelo = '".$modelo."', Fabricante = '".utf8_decode($fabricante)."', Polegadas  = '".$polegadas."', Tipos_de_entrada = '".$tiposentradas."' WHERE Modelo = '".$modelo."';";
    mysqli_query($con, $updateQuery) or die("Update failed");
    echo("Updated");
}

$con->close();
?>
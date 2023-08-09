<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@415263#', 'sysnetpro_controleestoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$quantoscanais = $_POST["quantoscanais"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Placa_de_som WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Query failed");

if($modelocheckresult->num_rows != 1)
{
    $insertuserquery= "INSERT INTO Placa_de_som(Modelo, Quantos_canais) VALUES('". $modelo ."', '". $quantoscanais ."');";
    mysqli_query($con, $insertuserquery) or die("insert item failed");
    echo("Worked");
}
else
{
    $updateQuery = "UPDATE Placa_de_som SET Modelo = '".$modelo."', Quantos_canais = '".$quantoscanais."' WHERE Modelo = '".$modelo."';";
    mysqli_query($con, $updateQuery) or die("Update failed");
    echo("Updated");
}


$con->close();

?>
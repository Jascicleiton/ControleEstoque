<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@415263#', 'sysnetpro_controleestoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$fabricante = $_POST["fabricante"];
$desempenho = $_POST["desempenho"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from GBIC WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Query failed");

if($modelocheckresult->num_rows != 1)
{
    $insertuserquery= "INSERT INTO GBIC(Modelo, Fabricante, Desempenho_max) VALUES('". $modelo ."', '". utf8_decode($fabricante) ."', '". $desempenho ."');";
    mysqli_query($con, $insertuserquery) or die("insert item failed");
     echo("Worked");
}
else
{
    $updateQuery = "UPDATE GBIC SET Modelo = '".$modelo."', Fabricante = '".utf8_decode($fabricante)."', Desempenho_max  = '".$desempenho."' WHERE Modelo = '".$modelo."';";
    mysqli_query($con, $updateQuery) or die("Update failed");
    echo("Updated");
}
$con->close();
?>
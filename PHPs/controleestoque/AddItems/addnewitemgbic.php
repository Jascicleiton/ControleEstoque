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

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from GBIC WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO GBIC(Modelo, Fabricante, Desempenho_max) VALUES('". $modelo ."', '". utf8_decode($fabricante) ."', '". $desempenho ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Worked");

$con->close();
?>
<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@415263#', 'sysnetpro_controleestoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$ondefunciona = $_POST["ondefunciona"];
$voltagem = $_POST["voltagem"];
$amperagem = $_POST["amperagem"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Adaptador_AC WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Query failed");

if($modelocheckresult->num_rows != 1)
{
    $insertuserquery= "INSERT INTO Adaptador_AC(Modelo, Onde_funciona, Voltagem, Amperagem) VALUES('". $modelo ."', '". utf8_decode($ondefunciona) ."', '". $voltagem ."', '". $amperagem ."');";
    mysqli_query($con, $insertuserquery) or die("insert item failed");
     echo("Worked");
}
else
{
    $updateQuery = "UPDATE Adaptador_AC SET Modelo = '".$modelo."', Onde_funciona = '".$ondefunciona."', Voltagem  = '".$voltagem."', Amperagem = '".$amperagem."' WHERE Modelo = '".$modelo."';";
    mysqli_query($con, $updateQuery) or die("Update failed");
    echo("Updated");
}
$con->close();
?>
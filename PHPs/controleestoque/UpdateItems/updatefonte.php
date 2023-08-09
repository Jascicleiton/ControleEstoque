<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@415263#', 'sysnetpro_controleestoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$watts = $_POST["watts"];
$ondefunciona = $_POST["ondefunciona"];
$conectores = $_POST["conectores"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}
$modelocheckquery = "SELECT * from Fonte WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Query failed");

if($modelocheckresult->num_rows != 1)
{
    $insertuserquery= "INSERT INTO Fonte(Modelo, Watts, Onde_funciona, Conectores) VALUES('". $modelo ."', '". $watts ."', '". utf8_decode($ondefunciona) ."', '". utf8_decode($conectores) ."');";
    mysqli_query($con, $insertuserquery) or die("insert item failed");
     echo("Worked");
}
else
{
    $updateQuery = "UPDATE Fonte SET Modelo = '".$modelo."', Watts = '".$watts."', Onde_funciona  = '".$ondefunciona."', Conectores = '".$conectores."' WHERE Modelo = '".$modelo."';";
    mysqli_query($con, $updateQuery) or die("Update failed");
    echo("Updated");
}

$con->close();
?>
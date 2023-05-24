<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Testing');

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

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Fonte WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Fonte(Modelo, Watts, Onde_funciona, Conectores) VALUES('". $modelo ."', '". $watts ."', '". utf8_decode($ondefunciona) ."', '". utf8_decode($conectores) ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Worked");


$con->close();

//Error codes
// 1 - Database connection error
// 4 - insert user failed
// 5 - wrong appkey
?>
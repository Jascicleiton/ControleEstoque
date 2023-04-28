<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_ESF_estoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$modelo = $_POST["modelo"];
$fabricante = $_POST["fabricante"];
$tipo = $_POST["tipo"];
$capacidade = $_POST["capacidade"];
$velocidade = $_POST["velocidade"];
$lowvoltage = $_POST["lowvoltage"];
$rank = $_POST["rank"];
$dimm = $_POST["dimm"];
$taxatransmissao = $_POST["taxatransmissao"];
$simbolo = $_POST["simbolo"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Memoria WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Memoria(Modelo, Fabricante, Tipo, Capacidade, Velocidade, Low_voltage, Rank, DIMM, Taxa_de_Transmisao, Simbolo) VALUES('". $modelo ."', '". $fabricante ."', '". $tipo ."', '". $capacidade ."', '". $velocidade ."', '". $lowvoltage ."', '". $rank ."', '". $dimm ."', '". $taxatransmissao ."', '". $simbolo ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
echo("Item added");


$con->close();

//Error codes
// 1 - Database connection error
// 4 - insert user failed
// 5 - wrong appkey
?>
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

$insertuserquery= "INSERT INTO Memoria(Modelo, Fabricante, Tipo, Capacidade, Velocidade, Low_voltage, Rank, DIMM, Taxa_de_transmissao, Simbolo) VALUES('". $modelo ."', '". utf8_decode($fabricante) ."', '". $tipo ."', '". $capacidade ."', '". $velocidade ."', '". utf8_decode($lowvoltage) ."', '". $rank ."', '". $dimm ."', '". $taxatransmissao ."', '". $simbolo ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
echo("Worked");

$con->close();
?>
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
$porta = $_POST["porta"];
$velocidade = $_POST["velocidade"];
$entradasd = $_POST["entradasd"];
$servidoressuportados = $_POST["servidoressuportados"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from iDrac WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO iDrac(Modelo, Fabricante, Porta, Velocidade, Entrada_SD, Servidores_suportados) VALUES('". $modelo ."', '". utf8_decode($fabricante) ."', '". $porta ."', '". $velocidade ."', '". $entradasd ."', '". $servidoressuportados ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Worked");


$con->close();
?>
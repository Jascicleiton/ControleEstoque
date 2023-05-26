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
$porta = $_POST["porta"];
$velocidade = $_POST["velocidade"];
$entradasd = $_POST["entradasd"];
$servidoressuportados = $_POST["servidoressuportados"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from iDrac WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Query failed");

if($modelocheckresult->num_rows != 1)
{
    $insertuserquery= "INSERT INTO iDrac(Modelo, Fabricante, Porta, Velocidade, Entrada_SD, Servidores_suportados) VALUES('". $modelo ."', '". utf8_decode($fabricante) ."', '". $porta ."', '". $velocidade ."', '". $entradasd ."', '". $servidoressuportados ."');";
    mysqli_query($con, $insertuserquery) or die("insert item failed");
     echo("Worked");
}
else
{
    $updateQuery = "UPDATE iDrac SET Modelo = '".$modelo."', Fabricante = '".utf8_decode($fabricante)."', Porta  = '".$porta."', Velocidade = '".$velocidade."',Entrada_SD = '".$entradasd."',Servidores_suportados = '".$servidoressuportados."' WHERE Modelo = '".$modelo."';";
    mysqli_query($con, $updateQuery) or die("Update failed");
    echo("Updated");
}
$con->close();
?>
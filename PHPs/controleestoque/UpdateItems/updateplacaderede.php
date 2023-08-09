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
$interface = $_POST["interface"];
$quantidadeportas = $_POST["quantidadeportas"];
$quaisconexoes = $_POST["quaisconexoes"];
$suportafibraoptica = $_POST["suportafibraoptica"];
$desempenho = $_POST["desempenho"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Placa_de_rede WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Query failed");

if($modelocheckresult->num_rows != 1)
{
    $insertuserquery= "INSERT INTO Placa_de_rede(Modelo, Fabricante, Interface, Quantidade_de_portas, Quais_portas, Suporta_fibra_optica, Desempenho) VALUES('". $modelo ."', '". utf8_decode($fabricante) ."', '". $interface ."', '". $quantidadeportas ."', '". $quaisconexoes ."', '". utf8_decode($suportafibraoptica) ."', '". $desempenho ."');";
    mysqli_query($con, $insertuserquery) or die("insert item failed");
    echo("Worked");
}
else
{
    $updateQuery = "UPDATE Placa_de_rede SET Modelo = '".$modelo."', Fabricante = '".utf8_decode($fabricante)."', Interface  = '".$interface."', Quantidade_de_portas = '".$quantidadeportas."', Quais_portas = '".$quaisconexoes."', Suporta_fibra_optica = '".utf8_decode($suportafibraoptica)."', Desempenho = '".$desempenho."' WHERE Modelo = '".$modelo."';";
    mysqli_query($con, $updateQuery) or die("Update failed");
    echo("Updated");
}


$con->close();

?>
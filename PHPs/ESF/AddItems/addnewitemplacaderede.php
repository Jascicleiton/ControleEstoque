<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_ESF_estoque');

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

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Placa_de_rede WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Placa_de_rede(Modelo, Fabricante, Interface, Quantidade_de_portas, Quais_portas, Suporta_fibra_optica, Desempenho) VALUES('". $modelo ."', '". $fabricante ."', '". $interface ."', '". $quantidadeportas ."', '". $quaisconexoes ."', '". $suportafibraoptica ."', '". $desempenho ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Item added");

$con->close();

?>
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
$interface = $_POST["interface"];
$tamanho = $_POST["tamanho"];
$formaarmazenamento = $_POST["formaarmazenamento"];
$capacidade = $_POST["capacidade"];
$rpm = $_POST["rpm"];
$velocidade = $_POST["velocidade"];
$enterprise = $_POST["enterprise"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from HD WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO HD(Modelo, Fabricante, Interface, Tamanho, Forma_de_armazenamento, Capacidade, RPM, Velocidade_de_Leitura, Enterprise) VALUES('". $modelo ."', '". utf8_decode($fabricante) ."', '". $interface ."', '". $tamanho ."', '". $formaarmazenamento ."', '". $capacidade ."', '". $rpm ."', '". $velocidade ."', '". utf8_decode($enterprise) ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Worked");


$con->close();

//Error codes
// 1 - Database connection error
// 4 - insert user failed
// 5 - wrong appkey
?>
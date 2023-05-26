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

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from HD WHERE Modelo = '" .$modelo. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Query failed");

if($modelocheckresult->num_rows != 1)
{
    $insertuserquery= "INSERT INTO HD(Modelo, Fabricante, Interface, Tamanho, Forma_de_armazenamento, Capacidade, RPM, Velocidade_de_Leitura, Enterprise) VALUES('". $modelo ."', '". utf8_decode($fabricante) ."', '". $interface ."', '". $tamanho ."', '". $formaarmazenamento ."', '". $capacidade ."', '". $rpm ."', '". $velocidade ."', '". utf8_decode($enterprise) ."');";
    mysqli_query($con, $insertuserquery) or die("insert item failed");
    echo("Worked");
}
else
{
    $updateQuery = "UPDATE HD SET Modelo = '".$modelo."', Fabricante = '".utf8_decode($fabricante)."', Interface  = '".$interface."', Tamanho = '".$tamanho."', Forma_de_armazenamento = '".$formaarmazenamento."', Capacidade = '".$capacidade."', RPM = '".$rpm."', Velocidade_de_Leitura = '".$velocidade."', Enterprise = '".utf8_decode($enterprise)."' WHERE Modelo = '".$modelo."';";
    mysqli_query($con, $updateQuery) or die("Update failed");
    echo("Updated");
}
$con->close();
?>
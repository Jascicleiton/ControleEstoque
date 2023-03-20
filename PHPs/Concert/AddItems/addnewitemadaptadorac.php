<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Concert');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$patrimonio = $_POST["patrimonio"];
$modelo = $_POST["modelo"];
$voltagem = $_POST["voltagem"];
$amperagem = $_POST["amperagem"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$patrimoniocheckquery = "SELECT * from Adaptador_AC WHERE Patrimonio = '" .$patrimonio. "';";
$patrimoniocheckresult = mysqli_query($con, $patrimoniocheckquery) or die ("Modelo query failed");

if($patrimoniocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Adaptador_AC(Patrimonio, Modelo, Voltagem, Amperagem) VALUES('". $patrimonio ."', '". utf8_decode($modelo) ."', '". $voltagem ."', '". $amperagem ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Worked");

$con->close();
?>
<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Testing');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$patrimonio = $_POST["patrimonio"];
$modelo = $_POST["modelo"];
$fabricante = $_POST["fabricante"];
$modeloplacamae = $_POST["modeloplacamae"];
$fonte = $_POST["fonte"];
$memoria = $_POST["memoria"];
$hd = $_POST["hd"];
$placadevideo = $_POST["placadevideo"];
$placaderede = $_POST["placaderede"];
$processador = $_POST["processador"];
$memoriassuportadas = $_POST["memoriassuportadas"];
$quantasmemorias = $_POST["quantasmemorias"];
$ordemdasmemorias = $_POST["ordemdasmemorias"];
$capacidaderamtotal = $_POST["capacidaderamtotal"];
$soquete = $_POST["soquete"];
$placacontroladora = $_POST["placacontroladora"];
$atequantoshds = $_POST["atequantoshds"];
$tiposdehd = $_POST["tiposdehd"];
$tiposderaid = $_POST["tiposderaid"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$modelocheckquery = "SELECT * from Servidor WHERE Patrimonio = '" .$patrimonio. "';";
$modelocheckresult = mysqli_query($con, $modelocheckquery) or die ("Modelo query failed");

if($modelocheckresult->num_rows > 0)
{
    echo("Modelo found");
    exit();
}

$insertuserquery= "INSERT INTO Servidor(Patrimonio, Modelo, Fabricante, ModeloPlacaMae, Fonte, Memoria, HD, PlacaDeVideo, PlacaDeRede, Procesador, MemoriasSuportadas, AteQuantasMemorias, OrdemDasMemorias, CapacidadeRamTotal, Soquete, PlacaControladora, AteQuantosHds, TiposDeHD, TiposDeRaid) VALUES('". $patrimonio ."', '". utf8_decode($modelo)."', '". utf8_decode($fabricante)."', '". utf8_decode($modeloplacamae)."', '". utf8_decode($fonte)."', '". utf8_decode($memoria)."', '". utf8_decode($hd)."', '". utf8_decode($placadevideo)."', '". utf8_decode($placaderede)."', '". utf8_decode($processador)."', '". utf8_decode($memoriassuportadas)."', '". utf8_decode($quantasmemorias)."', '". utf8_decode($ordemdasmemorias)."', '". utf8_decode($capacidaderamtotal)."', '". utf8_decode($soquete)."', '". utf8_decode($placacontroladora)."', '". utf8_decode($atequantoshds)."', '". utf8_decode($tiposdehd)."', '". utf8_decode($tiposderaid)."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Worked");


$con->close();

//Error codes
// 1 - Database connection error
// 4 - insert user failed
// 5 - wrong appkey
?>
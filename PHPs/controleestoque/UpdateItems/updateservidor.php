<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@415263#', 'sysnetpro_controleestoque');

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

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$patrimoniocheckquery = "SELECT * from Servidor WHERE Patrimonio = '" .$patrimonio. "';";
$patrimoniocheckresult = mysqli_query($con, $patrimoniocheckquery) or die ("Query failed");

if($patrimoniocheckresult->num_rows != 1)
{
    $insertuserquery= "INSERT INTO Servidor(Patrimonio, Modelo, Fabricante, ModeloPlacaMae, Fonte, Memoria, HD, PlacaDeVideo, PlacaDeRede, Procesador, MemoriasSuportadas, AteQuantasMemorias, OrdemDasMemorias, CapacidadeRamTotal, Soquete, PlacaControladora, AteQuantosHds, TiposDeHD, TiposDeRaid) VALUES('". $patrimonio ."', '". utf8_decode($modelo)."', '". utf8_decode($fabricante)."', '". utf8_decode($modeloplacamae)."', '". utf8_decode($fonte)."', '". utf8_decode($memoria)."', '". utf8_decode($hd)."', '". utf8_decode($placadevideo)."', '". utf8_decode($placaderede)."', '". utf8_decode($processador)."', '". utf8_decode($memoriassuportadas)."', '". utf8_decode($quantasmemorias)."', '". utf8_decode($ordemdasmemorias)."', '". utf8_decode($capacidaderamtotal)."', '". utf8_decode($soquete)."', '". utf8_decode($placacontroladora)."', '". utf8_decode($atequantoshds)."', '". utf8_decode($tiposdehd)."', '". utf8_decode($tiposderaid)."');";
    mysqli_query($con, $insertuserquery) or die("insert item failed");
    echo("Worked");
}
else
{
    $updateQuery = "UPDATE Servidor SET Modelo = '".utf8_decode($modelo)."', Fabricante = '".utf8_decode($fabricante)."', ModeloPlacaMae = '".utf8_decode($modeloplacamae)."', Fonte = '".utf8_decode($fonte)."', Memoria = '".utf8_decode($memoria)."', HD = '".utf8_decode($hd)."', PlacaDeVideo = '".utf8_decode($placadevideo)."', PlacaDeRede = '".utf8_decode($placaderede)."', Procesador = '".utf8_decode($processador)."', MemoriasSuportadas = '".utf8_decode($memoriassuportadas)."', AteQuantasMemorias = '".utf8_decode($quantasmemorias)."', OrdemDasMemorias = '".utf8_decode($ordemdasmemorias)."', CapacidadeRamTotal = '".utf8_decode($capacidaderamtotal)."', Soquete = '".utf8_decode($soquete)."', PlacaControladora = '".utf8_decode($placacontroladora)."', AteQuantosHds = '".utf8_decode($atequantoshds)."', TiposDeHD = '".utf8_decode($modelo)."', TiposDeRaid = '".utf8_decode($tiposderaid)."' WHERE Patrimonio = '".$patrimonio."';";
    mysqli_query($con, $updateQuery) or die("Update failed");
    echo("Updated");
}


$con->close();

?>
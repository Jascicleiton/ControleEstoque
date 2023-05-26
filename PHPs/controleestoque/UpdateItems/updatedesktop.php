<?
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_controleestoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];
$patrimonio = $_POST["patrimonio"];
$modeloplacamae = $_POST["modeloplacamae"];
$fonte = $_POST["fonte"];
$memoria = $_POST["memoria"];
$hd = $_POST["hd"];
$placavideo = $_POST["placavideo"];
$placarede = $_POST["placarede"];
$leitordvd = $_POST["leitordvd"];
$processador = $_POST["processador"];

if($appkey != "UpdateItem")
{
    echo("wrong appkey");
    exit();
}

$patrimoniocheckquery = "SELECT * from Desktop WHERE Patrimonio = '" .$patrimonio. "';";
$patrimoniocheckresult = mysqli_query($con, $patrimoniocheckquery) or die ("Query failed");

if($patrimoniocheckresult->num_rows != 1)
{
    $insertuserquery= "INSERT INTO Desktop(Patrimonio, Modelo_placa_mae, Fonte, Memoria, HD, Placa_de_video, Placa_de_rede, Leitor_de_DVD, Processador) VALUES('". $patrimonio ."', '". $modeloplacamae ."', '". utf8_decode($fonte) ."', '". utf8_decode($memoria) ."', '". utf8_decode($hd) ."', '". utf8_decode($placavideo) ."', '". utf8_decode($placarede) ."', '". utf8_decode($leitordvd) ."', '". utf8_decode($processador) ."');";
    mysqli_query($con, $insertuserquery) or die("insert item failed");
     echo("Worked");
}
else
{
    $updateQuery = "UPDATE Desktop SET Patrimonio = '".$patrimonio."', Modelo_placa_mae = '".$modeloplacamae."', Fonte  = '".utf8_decode($fonte)."', Memoria = '".utf8_decode($memoria)."', HD = '".utf8_decode($hd)."',Placa_de_video = '".utf8_decode($placavideo)."',Placa_de_rede = '".utf8_decode($placarede)."',Leitor_de_DVD = '".utf8_decode($leitordvd)."',Processador = '".utf8_decode($processador)."' WHERE Patrimonio = '".$patrimonio."';";
    mysqli_query($con, $updateQuery) or die("Update failed");
    echo("Updated");
}
$con->close();
?>
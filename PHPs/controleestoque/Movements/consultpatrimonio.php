<?php
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@415263#', 'sysnetpro_controleestoque');
if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST["apppassword"];
if($appkey != "ConsultDatabase")
{
    echo("Wrong appkey");
    exit();
}

$patrimonio = $_POST["patrimonio"];
$patrimonioClean = filter_var($patrimonio, FILTER_SANITIZE_EMAIL);

$patrimoniocheckquery = "SELECT * from Inventario WHERE Patrimonio = '" .$patrimonioClean. "';";
$patrimoniocheckresult = mysqli_query($con, $patrimoniocheckquery) or die ("Query failed");

if($patrimoniocheckresult->num_rows != 1)
{
    echo("Not found or found duplicate");
    exit();
}
else
{
    echo("Item found");
}

$con->close();
?>
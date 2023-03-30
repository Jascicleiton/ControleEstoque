<?php
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Testing');
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
// Error codes
// 1 - Database connection error
// 2 - patrimonio query ran into an error
// 3 - patrimonio does not exist or there is more than one in the table
// 4 - patrimonio found


?>
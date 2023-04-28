<?php
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_controleestoque');
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

$serial = $_POST["serial"];
//$serialClean = filter_var($serial, FILTER_SANITIZE_EMAIL);

$serialcheckquery = "SELECT * from Inventario WHERE Serial = '" .$serial. "';";
$serialcheckresult = mysqli_query($con, $serialcheckquery) or die ("Query failed");

if($serialcheckresult->num_rows != 1)
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
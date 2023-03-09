<?php
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_relatorioatividades');

if(mysqli_connect_errno())
{
    echo("1");
    exit();
}

$appkey = $_POST["apppassword"];
$uniqueIdentifier = $_POST["uniqueIdentifier"];
$datatermino = $_POST["datatermino"];

if($appkey != "FinishActivity")
{
    exit();
}

$identifierCheckquery = "SELECT * FROM activities WHERE Unique_identifier = '" . $uniqueIdentifier . "';";
$identifierCheckresult = mysqli_query($con, $identifierCheckquery) or die("2");

if($identifierCheckresult->num_rows != 1)
{
    echo("3");
    exit();
}

$updateactivityquery = "UPDATE activities SET Status = 'Terminada', Data_de_termino = '".$datatermino."' WHERE Unique_Identifier = '".$uniqueIdentifier."';";

mysqli_query($con, $updateactivityquery) or die("4");

echo("0");
$con->close();

//Error codes
// 1 - Database connection error
// 2 - username query ran into an error
// 3 - Username does not exist or there is more than one in the table
// 4 - score update failed

?>
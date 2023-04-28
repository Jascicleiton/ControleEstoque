<?php
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_relatorioatividades');

if(mysqli_connect_errno())
{
    echo("ConectionError");
    exit();
}

$appkey = $_POST ["apppassword"];
$datainicio = $_POST["datainicio"];
$tecnico = $_POST["tecnico"];
$atividade = $_POST["atividade"];
$status = $_POST["status"];
$datatermino = $_POST["datatermino"];

if($appkey != "AddActivity")
{
    echo("WrongAppKey");
    exit();
}

$insertuserquery= "INSERT INTO activities(Data_de_inicio, Tecnico, Atividade, Status, Data_de_termino) VALUES('". $datainicio ."', '". $tecnico ."','".utf8_decode($atividade) ."','". $status ."','". $datatermino ."');";
mysqli_query($con, $insertuserquery) or die("DatabaseQueryFailed");
$last_id = $con->insert_id;
echo($last_id);

$con->close();
?>
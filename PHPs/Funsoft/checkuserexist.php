<?php

$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Fumsoft_estoque');
if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST["apppassword"];
if($appkey != "CheckIfUserExist")
{
    echo("wrong appkey");
    exit();
}

$username = $_POST["username"];
$usernameClean = filter_var($username, FILTER_SANITIZE_EMAIL);

$usernamecheckquery = "SELECT * from users WHERE username = '" .$usernameClean. "';";
$usernamecheckresult = mysqli_query($con, $usernamecheckquery) or die ("username query ran into an error");

if($usernamecheckresult->num_rows != 1)
{
    echo("Username does not exist or there is more than one in the table");
    exit();
}
else
{
    echo("Username already exist");
}

$con->close();
?>
<?php

$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Testing');
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
$usernamecheckresult = mysqli_query($con, $usernamecheckquery) or die ("Username query ran into an error");

if($usernamecheckresult->num_rows < 1)
{
    echo("Can create new user");
    exit();
}
else if($usernamecheckresult->num_rows > 1)
{
    echo("Duplicate username");
    exit();
}
else
{
    echo("Username already exist");
}

$con->close();
?>
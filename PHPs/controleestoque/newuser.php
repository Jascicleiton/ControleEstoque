<?php

$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_controleestoque');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$username = $_POST["username"];
$usernameclean = filter_var($username, FILTER_SANITIZE_EMAIL);
$password = $_POST["password"];
$passwordhash = password_hash($password, PASSWORD_DEFAULT);
$appkey = $_POST ["apppassword"];

if($appkey != "InsertNewUser")
{
    echo("wrong appkey");
    exit();
}

$usernamecheckquery = "SELECT username FROM users WHERE username = '" . $usernameclean . "';";
$usernamecheck = mysqli_query($con, $usernamecheckquery) or die("username query ran into an error");

if(mysqli_num_rows($usernamecheck) > 0)
{
    echo("Username already exists");
    exit();
}


$insertuserquery= "INSERT INTO users(username, password) VALUES('". $usernameclean ."', '". $passwordhash ."');";
mysqli_query($con, $insertuserquery) or die("insert user failed");
 echo("User added");


$con->close();

//Error codes
// 1 - Database connection error
// 2 - username query ran into an error
// 3 - Username already exists
// 4 - insert user failed
// 5 - wrong appkey
?>
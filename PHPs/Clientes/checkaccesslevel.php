<?php

$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Estoque_Clientes');
if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST["apppassword"];
if($appkey != "LoginUser")
{
    echo("wrong appkey");
    exit();
}

$username = $_POST["username"];
$usernameClean = filter_var($username, FILTER_SANITIZE_EMAIL);
$password = $_POST["password"];

$usernamecheckquery = "SELECT * from users WHERE username = '" .$usernameClean. "';";
$usernamecheckresult = mysqli_query($con, $usernamecheckquery) or die ("username query ran into an error");
$userinfo = mysqli_fetch_assoc($usernamecheckresult);

if($usernamecheckresult->num_rows != 1)
{
    echo("Username does not exist or there is more than one in the table");
    exit();
}
else
{
    $fetchedpassword = $userinfo["password"];
    if(password_verify(($password), $fetchedpassword))
    {
        echo($userinfo["AccessLevel"]);
    }
    else
    {
        echo("password was not able to be verified");
    }
}

$con->close();


// Error codes
// 1 - Database connection error
// 2 - username query ran into an error
// 3 - Username does not exist or there is more than one in the table
// 4 - password was not able to be verified
// 5 - playerinfo query failed
?>
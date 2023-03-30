<?php

$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_relatorioatividades');
if(mysqli_connect_errno())
{
    echo("1");
    exit();
}

$appkey = $_POST["apppassword"];
if($appkey != "LoginUser")
{
    exit();
}

$username = $_POST["username"];
$usernameClean = filter_var($username, FILTER_SANITIZE_EMAIL);
$password = $_POST["password"];

$usernamecheckquery = "SELECT * from users WHERE username = '" .$usernameClean. "';";
$usernamecheckresult = mysqli_query($con, $usernamecheckquery) or die ("2");

if($usernamecheckresult->num_rows != 1)
{
    echo("3");
    exit();
}

else
{
    $fetchedpassword = mysqli_fetch_assoc($usernamecheckresult)["password"];
    if(password_verify(($password), $fetchedpassword))
    {
        $userInfo = "SELECT * from users WHERE username = '" .$usernameClean. "';";
        $userInfoResult = mysqli_query($con, $userInfo) or die ("5");
        $existingUserInfo = mysqli_fetch_assoc($userInfoResult);
        $Username = $existingUserInfo["username"];
        echo($Username);
    }
    else
    {
        echo("4");
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
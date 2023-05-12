<?php

$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_controleestoque');
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

if($usernamecheckresult->num_rows == 0)
{
    echo("Username does not exist");
    exit();
}
else if($usernamecheckresult->num_rows > 1)
{
    echo("Duplicate username");
    exit();
}
else
{   
    $fetchedpassword = $userinfo["password"];
    
    if(password_verify(($password), $fetchedpassword))
    {         
        $accesslevel = $userinfo["AccessLevel"];
        echo $accesslevel;
    }
    else
    {
        echo("Password was not able to be verified");
    }
}

$con->close();
?>
<?php
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Concert');
if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST["apppassword"];
if($appkey != "ImportDatabase")
{
    echo("wrong appkey");
    exit();
}

function utf8ize($d)
{
    if (is_array($d)) {
        foreach ($d as $k => $v) {
            $d[$k] = utf8ize($v);
        }
    } else if (is_string ($d)) {
        return utf8_encode($d);
    }
    return $d;
}

$tablequery = "SELECT * FROM NoPaNoSe;";
$result = $con->query($tablequery) or die("Query failed");
if($result->num_rows > 0)
{
    $json_array = array();
    while($row = mysqli_fetch_assoc($result))
    {
        $json_array[] = $row;
    }
    echo json_encode(utf8ize($json_array));
}
else
{
    echo("Result came empty");
}

$con->close();
?>
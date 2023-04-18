<?php
$con = mysqli_connect('localhost', 'sysnetpro', '*SnpCpanel@741963#', 'sysnetpro_Quisto');

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

$patrimonio = $_POST["patrimonio"];
$patrimonioClean = filter_var($patrimonio, FILTER_SANITIZE_EMAIL);

$patrimoniocheckquery = "SELECT * from Inventario WHERE Patrimonio = '" .$patrimonioClean. "';";
$patrimoniocheckresult = mysqli_query($con, $patrimoniocheckquery) or die ("Check query failed");

if($patrimoniocheckresult->num_rows > 0)
{
    $json_array = array();
    while($row = mysqli_fetch_assoc($patrimoniocheckresult))
    {
        $json_array[] = $row;
    }
    echo json_encode(utf8ize($json_array));
}
else
{
    echo("Item not found");
    exit();
}

$con->close();

?>
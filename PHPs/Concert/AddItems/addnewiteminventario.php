<?
$con = mysqli_connect('localhost', 'sysnetpro', 'Sysnetpr0@741963', 'sysnetpro_Concert');

if(mysqli_connect_errno())
{
    echo("Database connection error");
    exit();
}

$appkey = $_POST ["apppassword"];

$aquisicao = $_POST["aquisicao"];
$entrada = $_POST["entrada"];
$patrimonio = $_POST["patrimonio"];
$status = $_POST["status"];
$serial = $_POST["serial"];
$categoria = $_POST["categoria"];
$fabricante = $_POST["fabricante"];
$modelo = $_POST["modelo"];
$local = $_POST["local"];
$Pessoa = $_POST["Pessoa"];
$Centro_de_Custo = $_POST["Centro_de_Custo"];
$saida = $_POST["saida"];

if($appkey != "AddNewItem")
{
    echo("wrong appkey");
    exit();
}

$patrimoniocheckquery = "SELECT * from Inventario WHERE Patrimonio = '" .$patrimonio. "';";
$patrimoniocheckresult = mysqli_query($con, $patrimoniocheckquery) or die ("Patrimônio query failed");

if($patrimoniocheckresult->num_rows > 0)
{
    echo("Patrimônio found");
    exit();
}

$serialcheckquery = "SELECT * from Inventario WHERE Serial = '" .$serial. "';";
$serialcheckresult = mysqli_query($con, $serialcheckquery) or die ("Serial query failed");

if($serialcheckresult->num_rows > 0)
{
    echo("Serial found");
    exit();
}

$insertuserquery= "INSERT INTO Inventario(Aquisicao, Entrada_no_estoque, Patrimonio, Status, Serial, Categoria, Fabricante, Modelo, Local, Pessoa, Centro_de_Custo, Saida_do_estoque) VALUES('". $aquisicao ."','". $entrada ."', '". $patrimonio ."', '". utf8_decode($status) ."', '". $serial ."', '". utf8_decode($categoria) ."', '". utf8_decode($fabricante) ."', '". utf8_decode($modelo) ."', '". utf8_decode($local) ."', '". utf8_decode($Pessoa) ."', '". utf8_decode($Centro_de_Custo) ."','". $saida ."');";
mysqli_query($con, $insertuserquery) or die("insert item failed");
 echo("Worked");

$con->close();
?>


switch (InternalDatabase.Instance.currentEstoque) 
{ 
        case CurrentEstoque.Concert: 

        break; 
    default: 

        break; 
} 

switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.Concert:
                        valuesList.Add(parameterValues[2].text);
                        valuesList.Add(parameterValues[7].text);
                        valuesList.Add(parameterValues[6].text);
                        valuesList.Add(parameterValues[12].text);
                        valuesList.Add(parameterValues[13].text);
                        valuesList.Add(parameterValues[14].text);
                        valuesList.Add(parameterValues[15].text);
                        valuesList.Add(parameterValues[16].text);
                        valuesList.Add(parameterValues[17].text);
                        break;
                    default:
     
                        break;
                }
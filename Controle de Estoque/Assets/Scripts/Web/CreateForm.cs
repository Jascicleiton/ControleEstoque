using UnityEngine;
using UnityEngine.Networking;

public class CreateForm
{
    public static WWWForm GetInventarioForm(string appPassword, string aquisicao, string entrada, string patrimonio, string status, string serial, 
        string categoria, string fabricante, string modelo, string local, string saida, string observacao)
    {

        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("aquisicao", aquisicao);
        inventario.AddField("entrada", entrada);
        inventario.AddField("patrimonio", patrimonio);
        inventario.AddField("status", status);
        inventario.AddField("serial", serial);
        inventario.AddField("categoria", categoria);
        inventario.AddField("fabricante", fabricante);
        inventario.AddField("modelo", modelo);
        inventario.AddField("local", local);
        inventario.AddField("saida", saida);
        inventario.AddField("observacao", observacao);

        return inventario;
    }
    #region Categories Forms
    public static WWWForm GetAdaptadorACForm(string appPassword, string modelo, string ondefunciona,
        string voltagem, string amperagem)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("ondefunciona", ondefunciona);
        inventario.AddField("voltagem", voltagem);
        inventario.AddField("amperagem", amperagem);

        return inventario;
    }

    public static WWWForm GetCarregadorForm(string appPassword, string modelo, string ondefunciona,
       string voltagem, string amperagem)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("ondefunciona", ondefunciona);
        inventario.AddField("voltagem", voltagem);
        inventario.AddField("amperagem", amperagem);

        return inventario;
    }

    public static WWWForm GetDesktopForm(string appPassword, string patrimonio, string modeloplacamae, string fonte, string memoria,
   string hd, string placavideo, string placarede, string leitordvd, string processador)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("patrimonio", patrimonio);
        inventario.AddField("modeloplacamae", modeloplacamae);
        inventario.AddField("fonte", fonte);
        inventario.AddField("memoria", memoria);
        inventario.AddField("hd", hd);
        inventario.AddField("placavideo", placavideo);
        inventario.AddField("placarede", placarede);
        inventario.AddField("leitordvd", leitordvd);
        inventario.AddField("processador", processador);

        return inventario;
    }

    public static WWWForm GetFonteForm(string appPassword, string modelo, string watts, string ondefunciona, string conectores)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("watts", watts);
        inventario.AddField("ondefunciona", ondefunciona);
        inventario.AddField("conectores", conectores);

        return inventario;
    }

    public static WWWForm GetGBICForm(string appPassword, string modelo, string fabricante, string desempenho)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("fabricante", fabricante);
        inventario.AddField("desempenho", desempenho);

        return inventario;
    }

    public static WWWForm GetHDForm(string appPassword, string modelo, string fabricante, string Interface, string tamanho, string formaDeArmazenamento,
       string Capacidade, string RPM, string VelocidadeLeitura, string Enterprise)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("fabricante", fabricante);
        inventario.AddField("interface", Interface);
        inventario.AddField("tamanho", tamanho);
        inventario.AddField("formaarmazenamento", formaDeArmazenamento);
        inventario.AddField("capacidade", Capacidade);
        inventario.AddField("rpm", RPM);
        inventario.AddField("velocidade", VelocidadeLeitura);
        inventario.AddField("enterprise", Enterprise);

        return inventario;
    }

    public static WWWForm GetiDracForm(string appPassword, string modelo, string fabricante, string porta, string velocidade,
     string entradasd, string servidoressuportados)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("fabricante", fabricante);
        inventario.AddField("porta", porta);
        inventario.AddField("velocidade", velocidade);
        inventario.AddField("entradasd", entradasd);
        inventario.AddField("servidoressuportados", servidoressuportados);

        return inventario;
    }

    public static WWWForm GetMemoriaForm(string appPassword, string modelo, string fabricante, string tipo, string capacidade, string velocidade,
       string lowvoltage, string rank, string dimm, string taxatransmissao, string simbolo)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("fabricante", fabricante);
        inventario.AddField("tipo", tipo);
        inventario.AddField("capacidade", capacidade);
        inventario.AddField("velocidade", velocidade);
        inventario.AddField("lowvoltage", lowvoltage);
        inventario.AddField("rank", rank);
        inventario.AddField("dimm", dimm);
        inventario.AddField("taxatransmissao", taxatransmissao);
        inventario.AddField("simbolo", simbolo);

        return inventario;
    }

    public static WWWForm GetMonitorForm(string appPassword, string modelo, string fabricante, string polegadas, string tiposentradas)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("fabricante", fabricante);
        inventario.AddField("polegadas", polegadas);
        inventario.AddField("tiposentradas", tiposentradas);

        return inventario;
    }

    public static WWWForm GetNotebookForm(string appPassword, string patrimonio, string modelo, string fabricante, string hd, string memoria, string entradarj49, 
        string bateria, string adaptadorac, string windows)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("patrimonio", patrimonio);
        inventario.AddField("modelo", modelo);
        inventario.AddField("fabricante", fabricante);
        inventario.AddField("hd", hd);
        inventario.AddField("memoria", memoria);
        inventario.AddField("entradarj49", entradarj49);
        inventario.AddField("bateria", bateria);
        inventario.AddField("adaptadorac", adaptadorac);
        inventario.AddField("windows", windows);
        return inventario;
    }

    public static WWWForm GetPlacaControladoraForm(string appPassword, string modelo, string tipoconexao, string quantidadeportas,
    string tiporaid, string tipohd, string capacidademaxhd, string quantoshd, string bateriainclusa, string barramento)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("tipoconexao", tipoconexao);
        inventario.AddField("quantidadeportas", quantidadeportas);
        inventario.AddField("tiporaid", tiporaid);
        inventario.AddField("tipohd", tipohd);
        inventario.AddField("capacidademaxhd", capacidademaxhd);
        inventario.AddField("quantoshd", quantoshd);
        inventario.AddField("bateriainclusa", bateriainclusa);
        inventario.AddField("barramento", barramento);

        return inventario;
    }

    public static WWWForm GetPlacaDeCapturaDeVideoForm(string appPassword, string modelo, string quantasentradas)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("quantasentradas", quantasentradas);

        return inventario;
    }

    public static WWWForm GetPlacaDeRedeForm(string appPassword, string modelo, string fabricante, string Interface, string quantidadeportas,
string quaisconexoes, string suportafibraoptica, string desempenho)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("fabricante", fabricante);
        inventario.AddField("interface", Interface);
        inventario.AddField("quantidadeportas", quantidadeportas);
        inventario.AddField("quaisconexoes", quaisconexoes);
        inventario.AddField("suportafibraoptica", suportafibraoptica);
        inventario.AddField("desempenho", desempenho);

        return inventario;
    }

    public static WWWForm GetPlacaSomForm(string appPassword, string modelo, string quantoscanais)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("quantoscanais", quantoscanais);

        return inventario;
    }

    public static WWWForm GetPlacaVideoForm(string appPassword, string modelo, string quantasentradas, string quaisentradas)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("quantasentradas", quantasentradas);
        inventario.AddField("quaisentradas", quaisentradas);

        return inventario;
    }

    public static WWWForm GetProcessadorForm(string appPassword, string modelo, string soquete, string nucleosfisicos, string nucleoslogicos,
       string aceitavirtualizacao, string turboboost, string hyperthreading)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("soquete", soquete);
        inventario.AddField("nucleosfisicos", nucleosfisicos);
        inventario.AddField("nucleoslogicos", nucleoslogicos);
        inventario.AddField("aceitavirtualizacao", aceitavirtualizacao);
        inventario.AddField("turboboost", turboboost);
        inventario.AddField("hyperthreading", hyperthreading);

        return inventario;
    }

    public static WWWForm GetRoteadorForm(string appPassword, string modelo, string wireless, string quantasentradas,
         string bandamax, string voltagem)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("wireless", wireless);
        inventario.AddField("quantasentradas", quantasentradas);
        inventario.AddField("bandamax", bandamax);
        inventario.AddField("voltagem", voltagem);

        return inventario;
    }

    public static WWWForm GetServidorForm(string appPassword, string patrimonio, string modelo, string fabricante, string modeloplacamae, string fonte, string memoria,
        string hd, string placadevideo, string placaderede, string processador, string memoriassuportadas, string quantasmemorias, 
        string ordemdasmemorias, string capacidaderamtotal, string soquete, string placacontroladora, string atequantoshds, string tiposdehd,
        string tiposderaid)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("patrimonio", patrimonio);
        inventario.AddField("modelo", modelo);
        inventario.AddField("fabricante", fabricante);
        inventario.AddField("modeloplacamae", modeloplacamae);
        inventario.AddField("fonte", fonte);
        inventario.AddField("memoria", memoria);
        inventario.AddField("hd", hd);
        inventario.AddField("placadevideo", placadevideo);
        inventario.AddField("placaderede", placaderede);
        inventario.AddField("processador", processador);
        inventario.AddField("memoriassuportadas", memoriassuportadas);
        inventario.AddField("quantasmemorias", quantasmemorias);
        inventario.AddField("ordemdasmemorias", ordemdasmemorias);
        inventario.AddField("capacidaderamtotal", capacidaderamtotal);
        inventario.AddField("soquete", soquete);
        inventario.AddField("placacontroladora", placacontroladora);
        inventario.AddField("atequantoshds", atequantoshds);
        inventario.AddField("tiposdehd", tiposdehd);
        inventario.AddField("tiposderaid", tiposderaid);
        return inventario;
    }

    public static WWWForm GetStorageNASForm(string appPassword, string modelo, string tamanhohd, string tiporaid,
       string tipohd, string capacidademaxhd, string quantoshd)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("tamanhohd", tamanhohd);
        inventario.AddField("tiporaid", tiporaid);
        inventario.AddField("tipohd", tipohd);
        inventario.AddField("capacidademaxhd", capacidademaxhd);
        inventario.AddField("quantoshd", quantoshd);

        return inventario;
    }

    public static WWWForm GetSwitchForm(string appPassword, string modelo, string quantasentradas, string capacidademaxporta)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("modelo", modelo);
        inventario.AddField("quantasentradas", quantasentradas);
        inventario.AddField("capacidademaxporta", capacidademaxporta);
       
        return inventario;
    }
#endregion

    public static WWWForm GetConsultPatrimonioForm(string appPassword, string patrimonio)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("patrimonio", patrimonio);
        
        return inventario;
    }

    public static WWWForm GetConsultSerialForm(string appPassword, string serial)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("serial", serial);

        return inventario;
    }

    public static WWWForm GetMoveItemForm(string appPassword, string patrimonio, string serial, string usuario, string data, 
        string deOnde, string paraOnde)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("patrimonio", patrimonio);
        inventario.AddField("serial", serial);
        inventario.AddField("usuario", usuario);
        inventario.AddField("data", data);
        inventario.AddField("deonde", deOnde);
        inventario.AddField("paraonde", paraOnde);
        return inventario;
    }

    public static WWWForm GetNoPaNoSeForm(string appPassword, string itemName, int itemQuantity)
    {
        WWWForm item = new WWWForm();
        item.AddField("apppassword", appPassword);
        item.AddField("itemname", itemName);
        item.AddField("itemQuantity", itemQuantity);
        return item;
    }
}

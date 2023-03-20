using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Create all the necessary WWWForms
/// </summary>
public class CreateForm
{
    public static WWWForm GetInventarioForm(string appPassword, List<string> parameters)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.Concert:
                inventario.AddField("aquisicao", parameters[0]);
                inventario.AddField("entrada", parameters[1]);
                inventario.AddField("patrimonio", parameters[2]);
                inventario.AddField("status", parameters[3]);
                inventario.AddField("serial", parameters[4]);
                inventario.AddField("categoria", parameters[5]);
                inventario.AddField("fabricante", parameters[6]);
                inventario.AddField("modelo", parameters[7]);
                inventario.AddField("local", parameters[8]);
                inventario.AddField("Pessoa", parameters[9]);
                inventario.AddField("Centro_de_Custo", parameters[10]);
                inventario.AddField("saida", parameters[11]);
                break;
            default:
                inventario.AddField("aquisicao", parameters[0]);
                inventario.AddField("entrada", parameters[1]);
                inventario.AddField("patrimonio", parameters[2]);
                inventario.AddField("status", parameters[3]);
                inventario.AddField("serial", parameters[4]);
                inventario.AddField("categoria", parameters[5]);
                inventario.AddField("fabricante", parameters[6]);
                inventario.AddField("modelo", parameters[7]);
                inventario.AddField("local", parameters[8]);
                inventario.AddField("saida", parameters[9]);
                inventario.AddField("observacao", parameters[10]);
                break;
        }
        return inventario;
    }

    public static WWWForm GetExportInventoryForm()
    {
        WWWForm exportForm = new WWWForm();
        //exportForm.AddField("", Intern)
        return exportForm;
    }

    #region Categories Forms

    public static WWWForm GetAdaptadorACForm(string appPassword, List<string> parameters)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                inventario.AddField("modelo", parameters[0]);
                inventario.AddField("ondefunciona", parameters[1]);
                inventario.AddField("voltagem", parameters[2]);
                inventario.AddField("amperagem", parameters[3]);
                break;
            default:
                inventario.AddField("patrimonio", parameters[0]);
                inventario.AddField("modelo", parameters[1]);
                inventario.AddField("voltagem", parameters[2]);
                inventario.AddField("amperagem", parameters[3]);
                break;
        }
        return inventario;
    }

    public static WWWForm GetCarregadorForm(string appPassword, List<string> parameters)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);

        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                inventario.AddField("modelo", parameters[0]);
                inventario.AddField("ondefunciona", parameters[1]);
                inventario.AddField("voltagem", parameters[2]);
                inventario.AddField("amperagem", parameters[3]);
                break;
            default:
                inventario.AddField("patrimonio", parameters[0]);
                inventario.AddField("modelo", parameters[1]);
                inventario.AddField("voltagem", parameters[2]);
                inventario.AddField("amperagem", parameters[3]);
                break;
        }
        return inventario;
    }

    public static WWWForm GetDesktopForm(string appPassword, List<string> parameters)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
            case CurrentEstoque.Testing:
                inventario.AddField("patrimonio", parameters[0]);
                inventario.AddField("modeloplacamae", parameters[1]);
                inventario.AddField("fonte", parameters[2]);
                inventario.AddField("memoria", parameters[3]);
                inventario.AddField("hd", parameters[4]);
                inventario.AddField("placavideo", parameters[5]);
                inventario.AddField("placarede", parameters[6]);
                inventario.AddField("leitordvd", parameters[7]);
                inventario.AddField("processador", parameters[8]);
                break;
            default:
                inventario.AddField("patrimonio", parameters[0]);
                inventario.AddField("modelo", parameters[1]);
                inventario.AddField("fabricante", parameters[2]);
                inventario.AddField("hd", parameters[3]);
                inventario.AddField("memoria", parameters[4]);
                inventario.AddField("processador", parameters[5]);
                inventario.AddField("windows", parameters[6]);
                break;
        }
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

    public static WWWForm GetMonitorForm(string appPassword, List<string> parameters)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                inventario.AddField("modelo", parameters[0]);
                inventario.AddField("fabricante", parameters[1]);
                inventario.AddField("polegadas", parameters[2]);
                inventario.AddField("tiposentradas", parameters[3]);
                break;
            default:
                inventario.AddField("patrimonio", parameters[0]);
                inventario.AddField("modelo", parameters[1]);
                inventario.AddField("fabricante", parameters[2]);
                inventario.AddField("polegadas", parameters[3]);
                inventario.AddField("tiposentradas", parameters[4]);
                break;
        }
        return inventario;
    }

    public static WWWForm GetNotebookForm(string appPassword, List<string> parameters)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                inventario.AddField("patrimonio", parameters[0]);
                inventario.AddField("modelo", parameters[1]);
                inventario.AddField("fabricante", parameters[2]);
                inventario.AddField("hd", parameters[3]);
                inventario.AddField("memoria", parameters[4]);
                inventario.AddField("entradarj49", parameters[5]);
                inventario.AddField("bateria", parameters[6]);
                inventario.AddField("adaptadorac", parameters[7]);
                inventario.AddField("windows", parameters[8]);
                break;
            default:
                inventario.AddField("patrimonio", parameters[0]);
                inventario.AddField("modelo", parameters[1]);
                inventario.AddField("fabricante", parameters[2]);
                inventario.AddField("hd", parameters[3]);
                inventario.AddField("memoria", parameters[4]);
                inventario.AddField("processador", parameters[5]);
                inventario.AddField("windows", parameters[6]);
                break;
        }
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

    public static WWWForm GetRoteadorForm(string appPassword, List<string> parameters)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                inventario.AddField("modelo", parameters[0]);
                inventario.AddField("wireless", parameters[1]);
                inventario.AddField("quantasentradas", parameters[2]);
                inventario.AddField("bandamax", parameters[3]);
                inventario.AddField("voltagem", parameters[4]);
                break;
            default:
                inventario.AddField("patrimonio", parameters[0]);
                inventario.AddField("modelo", parameters[1]);
                inventario.AddField("fabricante", parameters[2]);
                inventario.AddField("wireless", parameters[3]);
                inventario.AddField("quantasentradas", parameters[4]);
                break;
        }
        return inventario;
    }

    public static WWWForm GetServidorForm(string appPassword, List<string> parameters)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                inventario.AddField("patrimonio", parameters[0]);
                inventario.AddField("modelo", parameters[1]);
                inventario.AddField("fabricante", parameters[2]);
                inventario.AddField("modeloplacamae", parameters[3]);
                inventario.AddField("fonte", parameters[4]);
                inventario.AddField("memoria", parameters[5]);
                inventario.AddField("hd", parameters[6]);
                inventario.AddField("placadevideo", parameters[7]);
                inventario.AddField("placaderede", parameters[8]);
                inventario.AddField("processador", parameters[9]);
                inventario.AddField("memoriassuportadas", parameters[10]);
                inventario.AddField("quantasmemorias", parameters[11]);
                inventario.AddField("ordemdasmemorias", parameters[12]);
                inventario.AddField("capacidaderamtotal", parameters[13]);
                inventario.AddField("soquete", parameters[14]);
                inventario.AddField("placacontroladora", parameters[15]);
                inventario.AddField("atequantoshds", parameters[16]);
                inventario.AddField("tiposdehd", parameters[17]);
                inventario.AddField("tiposderaid", parameters[18]);
                break;
                       default:
                inventario.AddField("patrimonio", parameters[0]);
                inventario.AddField("modelo", parameters[1]);
                inventario.AddField("fabricante", parameters[2]);
                inventario.AddField("hd", parameters[3]);
                inventario.AddField("memoria", parameters[4]);
                inventario.AddField("processador", parameters[5]);
                inventario.AddField("windows", parameters[6]);
                break;
        }        
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

    public static WWWForm GetSwitchForm(string appPassword, List<string> parameters)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                inventario.AddField("modelo", parameters[0]);
                inventario.AddField("quantasentradas", parameters[1]);
                inventario.AddField("capacidademaxporta", parameters[2]);
                break;                          
            default:
                inventario.AddField("patrimonio", parameters[0]);
                inventario.AddField("modelo", parameters[1]);
                inventario.AddField("fabricante", parameters[2]);
                inventario.AddField("quantasquaisportas", parameters[3]);
                break;
        }
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
    public static WWWForm GetMoveNoPaNoSeItemForm(string appPassword, string nome, int quantidade, string usuario, string data,
        string deOnde, string paraOnde)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", appPassword);
        inventario.AddField("itemname", nome);
        inventario.AddField("itemQuantity", quantidade);
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

    public static WWWForm GetMoveItemForm(string appPassword, string itemName, int itemQuantity, string usuario,
        string data, string deOnde, string paraOnde)
    {
        WWWForm item = new WWWForm();
        item.AddField("apppassword", appPassword);
        item.AddField("itemname", itemName);
        item.AddField("itemQuantity", itemQuantity.ToString());
        item.AddField("usuario", usuario);
        item.AddField("data", data);
        item.AddField("deonde", deOnde);
        item.AddField("paraonde", paraOnde);

        return item;
    }

    public static WWWForm GetMovementsForm(string appPassword, string parameter)
    {
        WWWForm movementForm = new WWWForm();
        movementForm.AddField("apppassword", appPassword);
        movementForm.AddField("parameter", parameter);

        return movementForm;
    }

    public static WWWForm GetCheckAccessLevelForm(string appPassword, string username, string password)
    {
        WWWForm checkAccesssLevelForm = new WWWForm();
        checkAccesssLevelForm.AddField("apppassword", appPassword);
        checkAccesssLevelForm.AddField("username", username);
        checkAccesssLevelForm.AddField("password", password);

        return checkAccesssLevelForm;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class CreateAddItemForm
{
    public static WWWForm GetInventarioForm(string entrada, string patrimonio, string status, string serial,
        string fabricante, string modelo, string local, string saida, string observacao)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("entrada", entrada);
        inventario.AddField("patrimonio", patrimonio);
        inventario.AddField("status", status);
        inventario.AddField("serial", serial);
        inventario.AddField("fabricante", fabricante);
        inventario.AddField("modelo", modelo);
        inventario.AddField("local", local);
        inventario.AddField("saida", saida);
        inventario.AddField("observacao", observacao);

        return inventario;
    }

    public static WWWForm GetHDForm(string modelo, string fabricante, string Interface, string tamanho, string formaDeArmazenamento,
       string Capacidade, string RPM, string VelocidadeLeitura, string Enterprise)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
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

    public static WWWForm GetMemoriaForm(string modelo, string fabricante, string capacidade, string velocidade,
       string lowvoltage, string rank, string dimm, string taxatransmissao, string simbolo)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("fabricante", fabricante);
        inventario.AddField("capacidade", capacidade);
        inventario.AddField("velocidade", velocidade);
        inventario.AddField("lowvoltage", lowvoltage);
        inventario.AddField("rank", rank);
        inventario.AddField("dimm", dimm);
        inventario.AddField("taxatransmissao", taxatransmissao);
        inventario.AddField("simbolo", simbolo);

        return inventario;
    }

    public static WWWForm GetPlacaDeRedeForm(string modelo, string fabricante, string Interface, string quantidadeportas,
       string suportafibraoptica, string desempenho)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("fabricante", fabricante);
        inventario.AddField("interface", Interface);
        inventario.AddField("quantidadeportas", quantidadeportas);
        inventario.AddField("suportafibraoptica", suportafibraoptica);
        inventario.AddField("desempenho", desempenho);

        return inventario;
    }

    public static WWWForm GetiDracForm(string modelo, string fabricante, string porta, string velocidade,
       string entradasd, string servidoressuportados)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("fabricante", fabricante);
        inventario.AddField("porta", porta);
        inventario.AddField("velocidade", velocidade);
        inventario.AddField("entradasd", entradasd);
        inventario.AddField("seridoressuportados", servidoressuportados);

        return inventario;
    }

    public static WWWForm GetPlacaControladoraForm(string modelo, string tipoconexao, string quantidadeportas,
        string tiporaid, string tipohd, string capacidademaxhd, string quantoshd, string barramento)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("tipoconexao", tipoconexao);
        inventario.AddField("quantidadeportas", quantidadeportas);
        inventario.AddField("tiporaid", tiporaid);
        inventario.AddField("tipohd", tipohd);
        inventario.AddField("capacidademaxhd", capacidademaxhd);
        inventario.AddField("quantoshd", quantoshd);
        inventario.AddField("barramento", barramento);

        return inventario;
    }

    public static WWWForm GetProcessadorForm(string modelo, string soquete, string nucleosfisicos, string nucleoslogicos,
       string aceitavirtualizacao, string turboboost, string hyperthreading)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("soquete", soquete);
        inventario.AddField("nucleosfisicos", nucleosfisicos);
        inventario.AddField("nucleoslogicos", nucleoslogicos);
        inventario.AddField("aceitavirtualizacao", aceitavirtualizacao);
        inventario.AddField("turboboost", turboboost);
        inventario.AddField("hyperthreading", hyperthreading);

        return inventario;
    }
    public static WWWForm GetDesktopForm(string patrimonio, string modeloplacamae, string fonte, string memoria,
       string hd, string placavideo, string placarede, string leitordvd, string processador)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
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
    public static WWWForm GetFonteForm(string modelo, string watts, string ondefunciona, string conectores)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("watts", watts);
        inventario.AddField("ondefunciona", ondefunciona);
        inventario.AddField("conectores", conectores);

        return inventario;
    }

    public static WWWForm GetSwitchForm(string modelo, string quantasentradas, string capacidademaxporta)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("quantasentradas", quantasentradas);
        inventario.AddField("capacidademaxporta", capacidademaxporta);
       
        return inventario;
    }
    public static WWWForm GetRoteadorForm(string modelo, string wireless, string quantasentradas,
        string bandamax, string voltagem)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("wireless", wireless);
        inventario.AddField("quantasentradas", quantasentradas);
        inventario.AddField("bandamax", bandamax);
        inventario.AddField("voltagem", voltagem);
       
        return inventario;
    }
    public static WWWForm GetCarregadorForm(string modelo, string ondefunciona,
        string voltagem, string amperagem)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("ondefunciona", ondefunciona);
        inventario.AddField("voltagem", voltagem);
        inventario.AddField("amperagem", amperagem);

        return inventario;
    }
    public static WWWForm GetAdaptadorACForm(string modelo, string ondefunciona,
        string voltagem, string amperagem)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("ondefunciona", ondefunciona);
        inventario.AddField("voltagem", voltagem);
        inventario.AddField("amperagem", amperagem);

        return inventario;
    }
    public static WWWForm GetStorageNASForm(string modelo, string tamanhohd, string tiporaid,
        string tipohd, string capacidademaxhd, string quantoshd)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("tamanhohd", tamanhohd);
        inventario.AddField("tiporaid", tiporaid);
        inventario.AddField("tipohd", tipohd);
        inventario.AddField("capacidademaxhd", capacidademaxhd);
        inventario.AddField("quantoshd", quantoshd);

        return inventario;
    }
    public static WWWForm GetGBICForm(string modelo, string fabricante, string desempenho)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("fabricante", fabricante);
        inventario.AddField("desempenho", desempenho);
        
        return inventario;
    }
    public static WWWForm GetPlacaVideoForm(string modelo, string quantasentradas, 
        string quaisentradas)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("quantasentradas", quantasentradas);
        inventario.AddField("quaisentradas", quaisentradas);

        return inventario;
    }
    public static WWWForm GetPlacaSomForm(string modelo, string quantoscanais)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("quantoscanais", quantoscanais);
        
        return inventario;
    }
    public static WWWForm GetPlacaCapturaVideoForm(string modelo, string quantasentradas)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("quantasentradas", quantasentradas);
        
        return inventario;
    }
    public static WWWForm GetServidorForm(string modelo, string fabricante)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("fabricante", fabricante);
       
        return inventario;
    }
    public static WWWForm GetNotebookForm(string modelo, string fabricante)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("fabricante", fabricante);
       
        return inventario;
    }
    public static WWWForm GetMonitorForm(string modelo, string fabricante, string polegadas,
        string tiposentradas)
    {
        WWWForm inventario = new WWWForm();
        inventario.AddField("apppassword", "AddNewItem");
        inventario.AddField("modelo", modelo);
        inventario.AddField("fabricante", fabricante);
        inventario.AddField("polegadas", polegadas);
        inventario.AddField("tiposentradas", tiposentradas);
       
        return inventario;
    }
}

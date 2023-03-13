using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.InteropServices;

public class ExportCSVs : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void CreateCsv(string joined, string filename);

    private string fileName = "";

    /// <summary>
    /// Creates an Adaptador AC CSV file
    /// </summary>
    public void CreateAdaptadorACDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.AdaptadorAC + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Patrimônio, Modelo, Voltagem de saída, Amperagem de saída (A)");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.AdaptadorAC];
            InternalDatabase.Instance.testingSheet = tempSheet;
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Patrimonio + "," + item.Modelo + "," + item.VoltagemDeSaida + "," +
                        item.AmperagemDeSaida);
                }
            }
        }
        textWriter.Close();
    }

    ///// <summary>
    ///// Creates an Carregador CSV file
    ///// </summary>
    //public void CreateCarregadorDetailsSheet()
    //{
    //    Sheet tempSheet = new Sheet();
    //    fileName = Application.dataPath + "/" + ConstStrings.Carregador + ".csv";
    //    TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
    //    textWriter.WriteLine("Modelo, Onde funciona?, Voltagem de saída, Amperagem de saída (mA)");
    //    textWriter.Close();
    //    if (InternalDatabase.Instance != null)
    //    {
    //        tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Carregador];
    //        if (tempSheet != null)
    //        {
    //            foreach (ItemColumns item in tempSheet.itens)
    //            {
    //                textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
    //                textWriter.WriteLine(item.Modelo + "," + item.OndeFunciona + "," + item.VoltagemDeSaida + "," +
    //                    item.AmperagemDeSaida);
    //            }
    //        }
    //    }
    //    textWriter.Close();
    //}

    /// <summary>
    /// Creates an Desktop CSV file
    /// </summary>
    public void CreateDesktopDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Desktop + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Patrimônio, Modelo, Fabricante, HD, Memória, Procesador, Qual Windows");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Desktop];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Patrimonio + "," + item.Modelo + "," + item.Fabricante + item.HD + "," +
                   item.Memoria + "," + item.Processador + "," + item.Windows);
                }
            }
        }
        textWriter.Close();
    }

    ///// <summary>
    ///// Creates an Fonte CSV file
    ///// </summary>
    //public void CreateFonteDetailsSheet()
    //{
    //    Sheet tempSheet = new Sheet();
    //    fileName = Application.dataPath + "/" + ConstStrings.Fonte + ".csv";
    //    TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
    //    textWriter.WriteLine("Modelo, Watts, Onde funciona, Conectores");
    //    textWriter.Close();
    //    if (InternalDatabase.Instance != null)
    //    {
    //        tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Fonte];
    //        if (tempSheet != null)
    //        {
    //            foreach (ItemColumns item in tempSheet.itens)
    //            {
    //                textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
    //                textWriter.WriteLine(item.Modelo + "," + item.Watts + "," + item.OndeFunciona + "," +
    //                    item.Conectores + "," + item.EstoqueAtual);
    //            }
    //        }
    //    }
    //    textWriter.Close();
    //}

    ///// <summary>
    ///// Creates an GBIC CSV file
    ///// </summary>
    //public void CreateGbicDetailsSheet()
    //{
    //    Sheet tempSheet = new Sheet();
    //    fileName = Application.dataPath + "/" + ConstStrings.Gbic + ".csv";
    //    TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
    //    textWriter.WriteLine("Modelo, Fabricante, Desempenho máx (GB)");
    //    textWriter.Close();
    //    if (InternalDatabase.Instance != null)
    //    {
    //        tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Gbic];
    //        if (tempSheet != null)
    //        {
    //            foreach (ItemColumns item in tempSheet.itens)
    //            {
    //                textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
    //                textWriter.WriteLine(item.Modelo + "," + item.Fabricante + "," + item.Desempenho);
    //            }
    //        }
    //    }
    //    textWriter.Close();
    //}

    ///// <summary>
    ///// Creates a HD CSV file
    ///// </summary>
    //public void CreateHDDetailsSheet()
    //{
    //    Sheet tempSheet = new Sheet();
    //    fileName = Application.dataPath + "/" + ConstStrings.HD + ".csv";
    //    TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
    //    textWriter.WriteLine("Modelo, Fabricante, Interface, Tamanho, Forma de armazenamento, " +
    //                    "Capacidade (GB), RPM, Velocidade de leitura, Enterprise?");
    //    textWriter.Close();
    //    if (InternalDatabase.Instance != null)
    //    {
    //        tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.HD];
    //        if (tempSheet != null)
    //        {
    //            foreach (ItemColumns item in tempSheet.itens)
    //            {
    //                textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
    //                textWriter.WriteLine(item.Modelo + "," + item.Fabricante + "," + item.Interface + "," +
    //                    item.Tamanho + "," + item.FormaDeArmazenamento + "," + item.CapacidadeEmGB + "," +
    //                    item.RPM + "," + item.VelocidadeDeLeitura + "," + item.Enterprise);
    //            }
    //        }
    //    }
    //    textWriter.Close();
    //}

    ///// <summary>
    ///// Creates an iDrac CSV file
    ///// </summary>
    //public void CreateiDracDetailsSheet()
    //{
    //    Sheet tempSheet = new Sheet();
    //    fileName = Application.dataPath + "/" + ConstStrings.Idrac + ".csv";
    //    TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
    //    textWriter.WriteLine("Modelo, Fabricante, Porta, Velocidade (GB/s), Entrada SD, Servidores suportados");
    //    textWriter.Close();
    //    if (InternalDatabase.Instance != null)
    //    {
    //        tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Idrac];
    //        if (tempSheet != null)
    //        {
    //            foreach (ItemColumns item in tempSheet.itens)
    //            {
    //                textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
    //                textWriter.WriteLine(item.Modelo + "," + item.Fabricante + "," + item.QuaisConexoes + "," +
    //                    item.VelocidadeGBs + "," + item.EntradaSD + "," + item.ServidoresSuportados);
    //            }
    //        }
    //    }
    //    textWriter.Close();
    //}

    ///// <summary>
    ///// Creates an Inventário_Sysnetpro CSV file
    ///// </summary>
    public void CreateInventarioSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.InventarioSnPro + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Aquisição, Entrada, Patrimônio, Status, Serial, Categoria, " +
                        "Fabricante, Modelo, Local, Pessoa, Centro de Custo, Saída");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Aquisicao + "," + item.Entrada + "," + item.Patrimonio + "," + item.Status + "," +
                        item.Serial + "," + item.Categoria + "," + item.Fabricante + "," +
                        item.Modelo + "," + item.Local + "," + item.Pessoa + "," + item.CentroDeCusto + ","  + item.Saida);
                }
            }
        }
        textWriter.Close();
    }

    public void CreateInventarioSheet2()
    {
        List<string[]> rowData = new List<string[]>();
        List<string> rowDataTemp = new List<string>();
        switch (InternalDatabase.Instance.currentEstoque)
        {
            case CurrentEstoque.SnPro:
                rowDataTemp.Add("Aquisição");
                rowDataTemp.Add("Entrada");
                rowDataTemp.Add("Patrimônio");
                rowDataTemp.Add("Status");
                rowDataTemp.Add("Serial");
                rowDataTemp.Add("Categoria");
                rowDataTemp.Add("Fabricante");
                rowDataTemp.Add("Modelo");
                rowDataTemp.Add("Local");
                rowDataTemp.Add("Saída");
                rowDataTemp.Add("Observação");
                break;
            case CurrentEstoque.Funsoft:
                break;
            case CurrentEstoque.ESF:
                break;
            case CurrentEstoque.Testing:
                break;
            case CurrentEstoque.Clientes:
                break;
            case CurrentEstoque.Concert:
                rowDataTemp.Add("Aquisição");
                rowDataTemp.Add("Entrada");
                rowDataTemp.Add("Patrimônio");
                rowDataTemp.Add("Status");
                rowDataTemp.Add("Serial");
                rowDataTemp.Add("Categoria");
                rowDataTemp.Add("Fabricante");
                rowDataTemp.Add("Modelo");
                rowDataTemp.Add("Local");
                rowDataTemp.Add("Pessoa");
                rowDataTemp.Add("Centro de Custo");
                rowDataTemp.Add("Saída");
                break;
            default:
                break;
        }
        foreach (ItemColumns item in InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro].itens)
        {
            rowDataTemp.Add(item.Aquisicao);
            rowDataTemp.Add(item.Entrada);
            rowDataTemp.Add(item.Patrimonio.ToString());
            rowDataTemp.Add(item.Status);
            rowDataTemp.Add(item.Serial);
            rowDataTemp.Add(item.Categoria);
            rowDataTemp.Add(item.Fabricante);
            rowDataTemp.Add(item.Modelo);
            rowDataTemp.Add(item.Local);
            rowDataTemp.Add(item.Saida);
            rowDataTemp.Add(item.Observacao);
            rowData.Add(rowDataTemp.ToArray());
        }

        string joined = "";

        for (int i = 0; i < rowData.Count; i++)
        {
            for (int j = 0; j < rowData[i].Length; j++)
            {
                joined += rowData[i][j] + "|";
            }
            joined += ",";
        }

        CreateCsv(joined, "Inventário.csv");
    }

    private IEnumerator Post()
    {
        WWWForm form = CreateForm.GetInventarioForm( );
        form.AddField
    }

    ///// <summary>
    ///// Creates a Memoria CSV file
    ///// </summary>
    //public void CreateMemoriaDetailsSheet()
    //{
    //    Sheet tempSheet = new Sheet();
    //    fileName = Application.dataPath + "/" + ConstStrings.Memoria + ".csv";
    //    TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
    //    textWriter.WriteLine("Modelo, Fabricante, Tipo, Capacidade (GB), Velocidace (MHz), Low voltage?, " +
    //                    "Rank, DIMM, Taxa de transmissão, Símbolo");
    //    textWriter.Close();
    //    if (InternalDatabase.Instance != null)
    //    {
    //        tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Memoria];
    //        if (tempSheet != null)
    //        {
    //            foreach (ItemColumns item in tempSheet.itens)
    //            {
    //                textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
    //                textWriter.WriteLine(item.Modelo + "," + item.Fabricante + "," + item.Tipo + "," +
    //                    item.CapacidadeEmGB + "," + item.VelocidadeMHz + "," + item.LowVoltage + "," +
    //                    item.Rank + "," + item.DIMM + "," + item.TaxaDeTransmissao + "," + item.Simbolo);
    //            }
    //        }
    //    }
    //    textWriter.Close();
    //}

    /// <summary>
    /// Creates an Monitor CSV file
    /// </summary>
    public void CreateMonitorDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Monitor + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Patrimônio, Modelo, Fabricante, Polegadas, Tipos de entrada");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Monitor];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Patrimonio + "," + item.Modelo + "," + item.Fabricante + "," + item.Polegadas + "," +
                        item.QuaisConexoes);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an Notebook CSV file
    /// </summary>
    public void CreateNotebookDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Notebook + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Patrimônio, Modelo, Fabricante, HD, Memoria, Processador, Qual Windows");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Notebook];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Patrimonio + "," + item.Modelo + "," + item.Fabricante + item.HD + "," +
                    item.Memoria + "," + item.Processador + "," + item.Windows);
                }
            }
        }
        textWriter.Close();
    }

    ///// <summary>
    ///// Creates an Placa Controladora CSV file
    ///// </summary>
    //public void CreatePlacaControladoraDetailsSheet()
    //{
    //    Sheet tempSheet = new Sheet();
    //    fileName = Application.dataPath + "/" + ConstStrings.PlacaControladora + ".csv";
    //    TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
    //    textWriter.WriteLine("Modelo, Tipo de conexão, Quantidade de portas, Tipos de RAID, Tipo de HD, " +
    //                    "Capacidade máxima do HD, Até quantos HDs, Bateria inclusa?, Barramento");
    //    textWriter.Close();
    //    if (InternalDatabase.Instance != null)
    //    {
    //        tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaControladora];
    //        if (tempSheet != null)
    //        {
    //            foreach (ItemColumns item in tempSheet.itens)
    //            {
    //                textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
    //                textWriter.WriteLine(item.Modelo + "," + item.QuaisConexoes + "," + item.QuantidadeDePortas + "," +
    //                    item.TipoDeRAID + "," + item.TipoDeHD + "," + item.CapacidadeMaxHD + "," +
    //                    item.AteQuantosHDs + "," + item.BateriaInclusa + "," + item.Barramento);
    //            }
    //        }
    //    }
    //    textWriter.Close();
    //}

    ///// <summary>
    ///// Creates an Placa de captura de vídeo CSV file
    ///// </summary>
    //public void CreatePlacaDeCapturaDeVideoDetailsSheet()
    //{
    //    Sheet tempSheet = new Sheet();
    //    fileName = Application.dataPath + "/" + ConstStrings.PlacaDeCapturaDeVideo + ".csv";
    //    TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
    //    textWriter.WriteLine("Modelo, Quantas entradas?");
    //    textWriter.Close();
    //    if (InternalDatabase.Instance != null)
    //    {
    //        tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeCapturaDeVideo];
    //        if (tempSheet != null)
    //        {
    //            foreach (ItemColumns item in tempSheet.itens)
    //            {
    //                textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
    //                textWriter.WriteLine(item.Modelo + "," + item.QuantidadeDePortas);
    //            }
    //        }
    //    }
    //    textWriter.Close();
    //}

    ///// <summary>
    ///// Creates a Placa de rede CSV file
    ///// </summary>
    //public void CreatePlacaDeRedeDetailsSheet()
    //{
    //    Sheet tempSheet = new Sheet();
    //    fileName = Application.dataPath + "/" + ConstStrings.PlacaDeRede + ".csv";
    //    TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
    //    textWriter.WriteLine("Modelo, Fabricante, Interface, Quantidade de portas, Quais portas?, " +
    //                    "Suporta fibra óptica?, Desempenho (Mb/s)");
    //    textWriter.Close();
    //    if (InternalDatabase.Instance != null)
    //    {
    //        tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeRede];
    //        if (tempSheet != null)
    //        {
    //            foreach (ItemColumns item in tempSheet.itens)
    //            {
    //                textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
    //                textWriter.WriteLine(item.Modelo + "," + item.Fabricante + "," + item.Interface + "," + item.QuantidadeDePortas + "," +
    //                    item.QuaisConexoes + "," + item.SuportaFibraOptica + "," + item.Desempenho);
    //            }
    //        }
    //    }
    //    textWriter.Close();
    //}

    ///// <summary>
    ///// Creates an Placa de som CSV file
    ///// </summary>
    //public void CreatePlacaDeSomDetailsSheet()
    //{
    //    Sheet tempSheet = new Sheet();
    //    fileName = Application.dataPath + "/" + ConstStrings.PlacaDeSom + ".csv";
    //    TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
    //    textWriter.WriteLine("Modelo, Quantos canais?");
    //    textWriter.Close();
    //    if (InternalDatabase.Instance != null)
    //    {
    //        tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeSom];
    //        if (tempSheet != null)
    //        {
    //            foreach (ItemColumns item in tempSheet.itens)
    //            {
    //                textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
    //                textWriter.WriteLine(item.Modelo + "," + item.QuantosCanais);
    //            }
    //        }
    //    }
    //    textWriter.Close();
    //}

    ///// <summary>
    ///// Creates an Placa de vídeo CSV file
    ///// </summary>
    //public void CreatePlacaDeVideoDetailsSheet()
    //{
    //    Sheet tempSheet = new Sheet();
    //    fileName = Application.dataPath + "/" + ConstStrings.PlacaDeVideo + ".csv";
    //    TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
    //    textWriter.WriteLine("Modelo, Quantas entradas?, Quais entradas?");
    //    textWriter.Close();
    //    if (InternalDatabase.Instance != null)
    //    {
    //        tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeVideo];
    //        if (tempSheet != null)
    //        {
    //            foreach (ItemColumns item in tempSheet.itens)
    //            {
    //                textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
    //                textWriter.WriteLine(item.Modelo + "," + item.QuantidadeDePortas + "," + item.QuaisConexoes);
    //            }
    //        }
    //    }
    //    textWriter.Close();
    //}

    ///// <summary>
    ///// Creates an Processador CSV file
    ///// </summary>
    //public void CreateProcessadorDetailsSheet()
    //{
    //    Sheet tempSheet = new Sheet();
    //    fileName = Application.dataPath + "/" + ConstStrings.Processador + ".csv";
    //    TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
    //    textWriter.WriteLine("Modelo, Soquete, Nº Núcleos físicos, Nº Núcleos lógicos, Aceita virtualização?, " +
    //                    "Turbo boost?, Hyper-Threading?");
    //    textWriter.Close();
    //    if (InternalDatabase.Instance != null)
    //    {
    //        tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Processador];
    //        if (tempSheet != null)
    //        {
    //            foreach (ItemColumns item in tempSheet.itens)
    //            {
    //                textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
    //                textWriter.WriteLine(item.Modelo + "," + item.Soquete + "," + item.NucleosFisicos + "," +
    //                    item.NucleosLogicos + "," + item.AceitaVirtualizacao + "," + item.TurboBoost + "," +
    //                    item.HyperThreading);
    //            }
    //        }
    //    }
    //    textWriter.Close();
    //}

    /// <summary>
    /// Creates an Roteador CSV file
    /// </summary>
    public void CreateRoteadorDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Roteador + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Patrimônio, Modelo, Fabricante, Wireless?, Quantas entradas?");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Roteador];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Patrimonio + "," + item.Modelo + "," + item.Fabricante + "," +
                        item.Wireless + "," + item.QuantidadeDePortas);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an Servidor CSV file
    /// </summary>
    public void CreateServidorDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Servidor + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Patrimônio, Modelo, Fabricante, HD, Memoria, Processador, Qual Windows");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Servidor];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Patrimonio + "," + item.Modelo + "," + item.Fabricante + item.HD + "," +
                    item.Memoria + "," + item.Processador + "," + item.Windows);
                }
            }
        }
        textWriter.Close();
    }

    ///// <summary>
    ///// Creates an Storage NAS CSV file
    ///// </summary>
    //public void CreateStorageNASDetailsSheet()
    //{
    //    Sheet tempSheet = new Sheet();
    //    fileName = Application.dataPath + "/" + ConstStrings.StorageNAS + ".csv";
    //    TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
    //    textWriter.WriteLine("Modelo, Tamanho de HDs, Tipos de Raid, Tipo de HD, Capacidade máx do HD, Até quantos HDs?");
    //    textWriter.Close();
    //    if (InternalDatabase.Instance != null)
    //    {
    //        tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.StorageNAS];
    //        if (tempSheet != null)
    //        {
    //            foreach (ItemColumns item in tempSheet.itens)
    //            {
    //                textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
    //                textWriter.WriteLine(item.Modelo + "," + item.Tamanho + "," + item.TipoDeRAID + "," +
    //                    item.TipoDeHD + "," + item.CapacidadeMaxHD + "," + item.AteQuantosHDs);
    //            }
    //        }
    //    }
    //    textWriter.Close();
    //}

    /// <summary>
    /// Creates an Switch CSV file
    /// </summary>
    public void CreateSwitchDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Switch + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Patrimônio, Modelo, Fabricante, Quantas e quais portas");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Switch];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Patrimonio + "," + item.Modelo + "," + item.Fabricante + "," + item.QuaisConexoes);
                }
            }
        }
        textWriter.Close();
    }

    ///// <summary>
    ///// Creates a Movimentação CSV file
    ///// </summary>
    //public void CreateMovimentacaoSheet()
    //{
    //    fileName = Application.dataPath + "/" + ConstStrings.Movimentacao + ".csv";
    //    TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
    //    textWriter.WriteLine("Patrimônio, Serial, Usuário, Data, De onde, Para onde");
    //    textWriter.Close();
    //    if (InternalDatabase.Instance != null)
    //    {
    //        if (InternalDatabase.movementRecords != null)
    //        {
    //            foreach (MovementRecords item in InternalDatabase.movementRecords)
    //            {
    //                textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
    //                textWriter.WriteLine(item.item.Patrimonio + "," + item.item.Serial + "," + item.username + "," +
    //                    item.date + "," + item.fromWhere + "," + item.toWhere);
    //            }
    //        }
    //    }
    //    textWriter.Close();
    //}
}

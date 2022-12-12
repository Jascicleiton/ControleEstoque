using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExportCSVs : MonoBehaviour
{
    private string fileName = "";

    /// <summary>
    /// Creates an Inventário_Sysnetpro CSV file
    /// </summary>
    public void CreateInventarioSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.InventarioSnPro + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Entrada, Patrimônio, Status, Serial, Categoria, " +
                        "Fabricante, Modelo, Local, Saída, Observação");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.InventarioSnPro];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Entrada + "," + item.Patrimonio + "," + item.Status + "," +
                        item.Serial + "," + item.Categoria + "," + item.Fabricante + "," +
                        item.Modelo + "," + item.Local + "," + item.Saida + "," + item.Observacao);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates a HD CSV file
    /// </summary>
    public void CreateHDDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.HD + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Fabricante, Interface, Tamanho, Forma de armazenamento, " +
                        "Capacidade (GB), RPM, Velocidade de leitura, Enterprise");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.HD];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.Fabricante + "," + item.Interface + "," +
                        item.Tamanho + "," + item.FormaDeArmazenamento + "," + item.CapacidadeEmGB + "," +
                        item.RPM + "," + item.VelocidadeDeLeitura + "," + item.Enterprise);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates a Memoria0 CSV file
    /// </summary>
    public void CreateMemoriaDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Memoria + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Fabricante, Tipo, Capacidade (GB), Velocidace (MHz), Low voltage?, " +
                        "Rank, DIMM, Taxa de transmissão, Símbolo");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Memoria];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.Fabricante + "," + item.Tipo + "," +
                        item.CapacidadeEmGB + "," + item.VelocidadeMHz + "," + item.LowVoltage + "," +
                        item.Rank + "," + item.DIMM + "," + item.TaxaDeTransmissao + "," + item.Simbolo);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates a Placa de rede CSV file
    /// </summary>
    public void CreatePlacaDeRedeDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.PlacaDeRede + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Fabricante, Interface, Quantidade de portas, Quais portas?, " +
                        "Suporta fibra óptica?, Desempenho (Mb/s)");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeRede];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.Fabricante + "," + item.Interface + "," + item.QuantidadeDePortas + "," +
                        item.QuaisConexoes + "," + item.SuportaFibraOptica + "," + item.Desempenho);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an iDrac CSV file
    /// </summary>
    public void CreateiDracDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Idrac + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Fabricante, Porta, Velocidade (GB/s), Entrada SD, Servidores suportados");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Idrac];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.Fabricante + "," + item.QuaisConexoes + "," +
                        item.VelocidadeGBs + "," + item.EntradaSD + "," + item.ServidoresSuportados);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an Placa Controladora CSV file
    /// </summary>
    public void CreatePlacaControladoraDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.PlacaControladora + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Tipo de conexão, Quantidade de portas, Tipos de RAID, Tipo de HD, " +
                        "Capacidade máxima do HD, Até quantos HDs, Bateria inclusa?, Barramento");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaControladora];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.QuaisConexoes + "," + item.QuantidadeDePortas + "," +
                        item.TipoDeRAID + "," + item.TipoDeHD + "," + item.CapacidadeMaxHD + "," +
                        item.AteQuantosHDs + "," + item.BateriaInclusa + "," + item.Barramento);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an Processador CSV file
    /// </summary>
    public void CreateProcessadorDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Processador + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Soquete, Nº Núcleos físicos, Nº Núcleos lógicos, Aceita virtualização?, " +
                        "Turbo boost?, Hyper-Threading?");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Processador];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.Soquete + "," + item.NucleosFisicos + "," +
                        item.NucleosLogicos + "," + item.AceitaVirtualizacao + "," + item.TurboBoost + "," +
                        item.HyperThreading);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an Desktop CSV file
    /// </summary>
    public void CreateDesktopDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Desktop + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Patrimônio, Modelo placa mãe, Fonte, Memória, HD, Placa de vídeo, Placa de rede," +
                            " Placa de Rede, Leitor de DVD, Processador");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Desktop];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Patrimonio + "," + item.ModeloPlacaMae + "," + item.Fonte + "," +
                        item.Memoria + "," + item.HD + "," + item.PlacaDeVideo + "," +
                        item.PlacaDeRede + "," + item.LeitorDeDVD + "," + item.Processador);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an Fonte CSV file
    /// </summary>
    public void CreateFonteDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Fonte + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Watts, Onde funciona, Conectores");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Fonte];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.Watts + "," + item.OndeFunciona + "," +
                        item.Conectores + "," + item.EstoqueAtual);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an Switch CSV file
    /// </summary>
    public void CreateSwitchDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Switch + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Quantas entradas?, Capacidade máx de cada porta (MB/s)");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Switch];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.QuantidadeDePortas + "," + item.Desempenho);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an Roteador CSV file
    /// </summary>
    public void CreateRoteadorDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Roteador + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Wireless?, Quantas entradas?, Banda máxima (Mb/s), Voltagem");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Roteador];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.Wireless + "," + item.QuantidadeDePortas + "," +
                        item.BandaMaxima + "," + item.VoltagemDeSaida);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an Carregador CSV file
    /// </summary>
    public void CreateCarregadorDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Carregador + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Onde funciona?, Voltagem de saída, Amperagem de saída (mA)");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Carregador];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.OndeFunciona + "," + item.VoltagemDeSaida + "," +
                        item.AmperagemDeSaida);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an Adaptador AC CSV file
    /// </summary>
    public void CreateAdaptadorACDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.AdaptadorAC + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Onde funciona, Voltagem de saída, Amperagem de saída (A)");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.AdaptadorAC];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.OndeFunciona + "," + item.VoltagemDeSaida + "," +
                        item.AmperagemDeSaida);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an Storage NAS CSV file
    /// </summary>
    public void CreateStorageNASDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.StorageNAS + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Tamanho de HDs, Tipo de HD, Capacidade máx do HD, Até quantos HDs?");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.StorageNAS];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.Tamanho + "," + item.TipoDeRAID + "," +
                        item.TipoDeHD + "," + item.CapacidadeMaxHD + "," + item.AteQuantosHDs);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an GBIC CSV file
    /// </summary>
    public void CreateGbicDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Gbic + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Fabricante, Desempenho máx (GB)");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Gbic];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.Fabricante + "," + item.Desempenho);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an Placa de vídeo CSV file
    /// </summary>
    public void CreatePlacaDeVideoDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.PlacaDeVideo + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Quantas entradas?, Quais entradas?");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeVideo];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.QuantidadeDePortas + "," + item.QuaisConexoes);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an Placa de som CSV file
    /// </summary>
    public void CreatePlacaDeSomDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.PlacaDeSom + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Quantos canais?");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeSom];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.QuantosCanais);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an Placa de captura de vídeo CSV file
    /// </summary>
    public void CreatePlacaDeCapturaDeVideoDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.PlacaDeCapturaDeVideo + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Quantas entradas?");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeCapturaDeVideo];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.QuantidadeDePortas);
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
        textWriter.WriteLine("Modelo, Fabricante");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Servidor];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.Fabricante);
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
        textWriter.WriteLine("Modelo, Fabricante");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Notebook];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.Fabricante);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates an Monitor CSV file
    /// </summary>
    public void CreateMonitorDetailsSheet()
    {
        Sheet tempSheet = new Sheet();
        fileName = Application.dataPath + "/" + ConstStrings.Monitor + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Modelo, Fabricante, Polegadas, Tipos de entrada");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            tempSheet = InternalDatabase.Instance.splitDatabase[ConstStrings.Monitor];
            if (tempSheet != null)
            {
                foreach (ItemColumns item in tempSheet.itens)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.Modelo + "," + item.Fabricante + "," + item.Polegadas + "," +
                        item.QuaisConexoes);
                }
            }
        }
        textWriter.Close();
    }

    /// <summary>
    /// Creates a Movimentação CSV file
    /// </summary>
    public void CreateMovimentacaoSheet()
    {
        fileName = Application.dataPath + "/" + ConstStrings.Movimentacao + ".csv";
        TextWriter textWriter = new StreamWriter(fileName, false); // It is false to create an empty file with the first line being what is typed below
        textWriter.WriteLine("Patrimônio, Serial, Usuário, Data, De onde, Para onde");
        textWriter.Close();
        if (InternalDatabase.Instance != null)
        {
            if (InternalDatabase.movementRecords != null)
            {
                foreach (MovementRecords item in InternalDatabase.movementRecords)
                {
                    textWriter = new StreamWriter(fileName, true); // it is true to add new lines to the already created file
                    textWriter.WriteLine(item.item.Patrimonio + "," + item.item.Serial + "," + item.username + "," +
                        item.date + "," + item.fromWhere + "," + item.toWhere);
                }
            }
        }
        textWriter.Close();
    }
}

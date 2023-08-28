using UnityEngine;
using SimpleJSON;
using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Misc;

namespace Assets.Scripts.Inventory
{

    /// <summary>
    /// Class used to fill the sheet from a specific inventory table that was imported and generated a JSONNode
    /// </summary>
    public class ImportingInventoryFunctions
    {
        public static void ImportAdaptadorAC(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();

                        newRow.Modelo = item[0];
                        newRow.OndeFunciona = item[1];
                        newRow.VoltagemDeSaida = item[2];
                        newRow.AmperagemDeSaida = item[3];
                        newRow.Categoria = ConstStrings.C_AdaptadorAC;
                        importSheet.itens.Add(newRow);
                    }
                    break;
                case CurrentEstoque.Fumsoft:
                case CurrentEstoque.ESF:
                case CurrentEstoque.Clientes:
                case CurrentEstoque.Concert:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Patrimonio = item[0];
                        newRow.Modelo = item[1];
                        newRow.VoltagemDeSaida = item[2];
                        newRow.AmperagemDeSaida = item[3];
                        newRow.Categoria = ConstStrings.C_AdaptadorAC;
                        importSheet.itens.Add(newRow);
                    }
                    break;
                default:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();

                        newRow.Modelo = item[0];
                        newRow.OndeFunciona = item[1];
                        newRow.VoltagemDeSaida = item[2];
                        newRow.AmperagemDeSaida = item[3];
                        newRow.Categoria = ConstStrings.C_AdaptadorAC;
                        importSheet.itens.Add(newRow);
                    }
                    break;
            }
            InternalDatabase.Instance.testingSheet = importSheet;
        }

        public static void ImportCarregador(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            foreach (JSONNode item in inventario)
            {
                ItemColumns newRow = new ItemColumns();
                newRow.Modelo = item[0];
                newRow.OndeFunciona = item[1];
                newRow.VoltagemDeSaida = item[2];
                newRow.AmperagemDeSaida = item[3];
                newRow.Categoria = ConstStrings.C_Carregador;
                importSheet.itens.Add(newRow);
            }
        }

        public static void ImportDesktop(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();

            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                case CurrentEstoque.ESF:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Patrimonio = item[0];
                        newRow.ModeloPlacaMae = item[1];
                        newRow.Fonte = item[2];
                        newRow.Memoria = item[3];
                        newRow.HD = item[4];
                        newRow.PlacaDeVideo = item[5];
                        newRow.PlacaDeRede = item[6];
                        newRow.LeitorDeDVD = item[7];
                        newRow.Processador = item[8];
                        newRow.Categoria = ConstStrings.C_Desktop;
                        importSheet.itens.Add(newRow);
                    }
                    break;
                case CurrentEstoque.Fumsoft:
                case CurrentEstoque.Clientes:
                case CurrentEstoque.Concert:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Patrimonio = item[0];
                        newRow.Modelo = item[1];
                        newRow.Fabricante = item[2];
                        newRow.HD = item[3];
                        newRow.Memoria = item[4];
                        newRow.Processador = item[5];
                        newRow.Windows = item[6];
                        newRow.Categoria = ConstStrings.C_Desktop;
                        importSheet.itens.Add(newRow);
                    }
                    break;
                default:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Patrimonio = item[0];
                        newRow.ModeloPlacaMae = item[1];
                        newRow.Fonte = item[2];
                        newRow.Memoria = item[3];
                        newRow.HD = item[4];
                        newRow.PlacaDeVideo = item[5];
                        newRow.PlacaDeRede = item[6];
                        newRow.LeitorDeDVD = item[7];
                        newRow.Processador = item[8];
                        newRow.Categoria = ConstStrings.C_Desktop;
                        importSheet.itens.Add(newRow);
                    }
                    break;
            }
        }

        public static void ImportFonte(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();

            foreach (JSONNode item in inventario)
            {
                ItemColumns newRow = new ItemColumns();
                newRow.Modelo = item[0];
                newRow.Watts = item[1];
                newRow.OndeFunciona = item[2];
                newRow.Conectores = item[3];
                newRow.Categoria = ConstStrings.C_Fonte;
                importSheet.itens.Add(newRow);
            }
        }

        public static void ImportGBIC(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            foreach (JSONNode item in inventario)
            {
                ItemColumns newRow = new ItemColumns();
                newRow.Modelo = item[0];
                newRow.Fabricante = item[1];
                newRow.Desempenho = item[2];
                newRow.Categoria = ConstStrings.C_Gbic;
                importSheet.itens.Add(newRow);
            }
        }

        public static void ImportHD(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            foreach (JSONNode item in inventario)
            {
                ItemColumns newRow = new ItemColumns();

                newRow.Modelo = item[0];
                newRow.Fabricante = item[1];
                newRow.Interface = item[2];
                newRow.Tamanho = item[3];
                newRow.FormaDeArmazenamento = item[4];
                newRow.CapacidadeEmGB = item[5];
                newRow.RPM = item[6];
                newRow.VelocidadeDeLeitura = item[7];
                newRow.Enterprise = item[8];
                newRow.EstoqueAtual = item[9];
                newRow.Categoria = ConstStrings.C_HD;
                importSheet.itens.Add(newRow);
            }
        }

        public static void ImportIdrac(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            foreach (JSONNode item in inventario)
            {
                ItemColumns newRow = new ItemColumns();
                newRow.Modelo = item[0];
                newRow.Fabricante = item[1];
                newRow.QuaisConexoes = item[2];
                newRow.VelocidadeGBs = item[3];
                newRow.EntradaSD = item[4];
                newRow.ServidoresSuportados = item[5];
                newRow.Categoria = ConstStrings.C_Idrac;
                importSheet.itens.Add(newRow);
            }
        }

        public static void ImportInventory(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            if (inventario != null)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.Concert:
                        foreach (JSONNode item in inventario)
                        {
                            ItemColumns newRow = new ItemColumns();
                            newRow.Aquisicao = item[0];
                            newRow.Entrada = item[1];
                            newRow.Patrimonio = item[2];
                            newRow.Status = item[3];
                            newRow.Serial = item[4];
                            newRow.Categoria = item[5];
                            newRow.Fabricante = item[6];
                            newRow.Modelo = item[7];
                            newRow.Local = item[8];
                            newRow.Pessoa = item[9];
                            newRow.CentroDeCusto = item[10];
                            newRow.Saida = item[11];
                            importSheet.itens.Add(newRow);
                        }
                        break;
                    default:
                        foreach (JSONNode item in inventario)
                        {
                            ItemColumns newRow = new ItemColumns();
                            newRow.Aquisicao = item[0];
                            newRow.Entrada = item[1];
                            newRow.Patrimonio = item[2];
                            newRow.Status = item[3];
                            newRow.Serial = item[4];
                            newRow.Categoria = item[5];
                            newRow.Fabricante = item[6];
                            newRow.Modelo = item[7];
                            newRow.Local = item[8];
                            newRow.Saida = item[9];
                            newRow.Observacao = item[10];
                            importSheet.itens.Add(newRow);
                        }
                        break;
                }
            }
            else
            {
                Debug.LogWarning("inventario JSON is null");
            }

        }

        public static void ImportMemoria(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            foreach (JSONNode item in inventario)
            {
                ItemColumns newRow = new ItemColumns();

                newRow.Modelo = item[0];
                newRow.Fabricante = item[1];
                newRow.Tipo = item[2];
                newRow.CapacidadeEmGB = item[3];
                newRow.VelocidadeMHz = item[4];
                newRow.LowVoltage = item[5];
                newRow.Rank = item[6];
                newRow.DIMM = item[7];
                newRow.TaxaDeTransmissao = item[8];
                newRow.Simbolo = item[9];
                newRow.EstoqueAtual = item[10];
                newRow.Categoria = ConstStrings.C_Memoria;

                importSheet.itens.Add(newRow);
            }
        }

        public static void ImportMonitor(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Modelo = item[0];
                        newRow.Fabricante = item[1];
                        newRow.Polegadas = item[2];
                        newRow.QuaisConexoes = item[3];
                        newRow.Categoria = ConstStrings.C_Monitor;
                        importSheet.itens.Add(newRow);
                    }
                    break;
                default:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Patrimonio = item[0];
                        newRow.Modelo = item[1];
                        newRow.Fabricante = item[2];
                        newRow.Polegadas = item[3];
                        newRow.QuaisConexoes = item[4];
                        newRow.Categoria = ConstStrings.C_Monitor;
                        importSheet.itens.Add(newRow);
                    }
                    break;
            }
        }

        public static void ImportNotebook(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                case CurrentEstoque.Testing:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Patrimonio = item[0];
                        newRow.Modelo = item[1];
                        newRow.Fabricante = item[2];
                        newRow.HD = item[3];
                        newRow.Memoria = item[4];
                        newRow.EntradaRJ45 = item[5];
                        newRow.BateriaInclusa = item[6];
                        newRow.AdaptadorAC = item[7];
                        newRow.Windows = item[8];
                        newRow.Categoria = ConstStrings.C_Notebook;
                        importSheet.itens.Add(newRow);
                    }
                    break;
                case CurrentEstoque.Fumsoft:
                case CurrentEstoque.ESF:
                case CurrentEstoque.Clientes:
                case CurrentEstoque.Concert:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Patrimonio = item[0];
                        newRow.Modelo = item[1];
                        newRow.Fabricante = item[2];
                        newRow.HD = item[3];
                        newRow.Memoria = item[4];
                        newRow.Processador = item[5];
                        newRow.Windows = item[6];
                        newRow.Categoria = ConstStrings.C_Notebook;
                        importSheet.itens.Add(newRow);
                    }
                    break;
                default:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Patrimonio = item[0];
                        newRow.Modelo = item[1];
                        newRow.Fabricante = item[2];
                        newRow.HD = item[3];
                        newRow.Memoria = item[4];
                        newRow.EntradaRJ45 = item[5];
                        newRow.BateriaInclusa = item[6];
                        newRow.AdaptadorAC = item[7];
                        newRow.Windows = item[8];
                        newRow.Categoria = ConstStrings.C_Notebook;
                        importSheet.itens.Add(newRow);
                    }
                    break;
            }
        }

        public static void ImportPlacaControladora(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            foreach (JSONNode item in inventario)
            {
                ItemColumns newRow = new ItemColumns();
                newRow.Modelo = item[0];
                newRow.QuaisConexoes = item[1];
                newRow.QuantidadeDePortas = item[2];
                newRow.TipoDeRAID = item[3];
                newRow.TipoDeHD = item[4];
                newRow.CapacidadeMaxHD = item[5];
                newRow.AteQuantosHDs = item[6];
                newRow.BateriaInclusa = item[7];
                newRow.Barramento = item[8];
                newRow.Categoria = ConstStrings.C_PlacaControladora;
                importSheet.itens.Add(newRow);
            }
        }

        public static void ImportPlacaDeCapturaDeVideo(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            foreach (JSONNode item in inventario)
            {
                ItemColumns newRow = new ItemColumns();
                newRow.Modelo = item[0];
                newRow.QuantidadeDePortas = item[1];
                newRow.Categoria = ConstStrings.C_PlacaDeCapturaDeVideo;
                importSheet.itens.Add(newRow);
            }
        }

        public static void ImportPlacaDeRede(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            foreach (JSONNode item in inventario)
            {
                ItemColumns newRow = new ItemColumns();
                newRow.Modelo = item[0];
                newRow.Fabricante = item[1];
                newRow.Interface = item[2];
                newRow.QuantidadeDePortas = item[3];
                newRow.QuaisConexoes = item[4];
                newRow.SuportaFibraOptica = item[5];
                newRow.Desempenho = item[6];
                newRow.Categoria = ConstStrings.C_PlacaDeRede;
                importSheet.itens.Add(newRow);
            }
        }

        public static void ImportPlacaDeSom(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            foreach (JSONNode item in inventario)
            {
                ItemColumns newRow = new ItemColumns();
                newRow.Modelo = item[0];
                newRow.QuantosCanais = item[1];
                newRow.Categoria = ConstStrings.C_PlacaDeSom;
                importSheet.itens.Add(newRow);
            }
        }

        public static void ImportPlacaDeVideo(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            foreach (JSONNode item in inventario)
            {
                ItemColumns newRow = new ItemColumns();
                newRow.Modelo = item[0];
                newRow.QuantidadeDePortas = item[1];
                newRow.QuaisConexoes = item[2];
                newRow.Categoria = ConstStrings.C_PlacaDeVideo;
                importSheet.itens.Add(newRow);
            }
        }

        public static void ImportPlacaSAS(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            if (inventario.Count > 0)
            {
                foreach (JSONNode item in inventario)
                {
                    ItemColumns newRow = new ItemColumns();
                    newRow.Modelo = item[0];
                    newRow.QuantidadeDePortas = item[1];
                    newRow.VelocidadeGBs = item[2];
                    newRow.Categoria = ConstStrings.C_PlacaSAS;
                    importSheet.itens.Add(newRow);
                }
            }
        }

        public static void ImportProcessador(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            foreach (JSONNode item in inventario)
            {
                ItemColumns newRow = new ItemColumns();
                newRow.Modelo = item[0];
                newRow.Soquete = item[1];
                newRow.NucleosFisicos = item[2];
                newRow.NucleosLogicos = item[3];
                newRow.Categoria = ConstStrings.C_Processador;
                importSheet.itens.Add(newRow);
            }
        }

        public static void ImportRoteador(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                case CurrentEstoque.Testing:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Modelo = item[0];
                        newRow.Wireless = item[1];
                        newRow.QuantidadeDePortas = item[2];
                        newRow.BandaMaxima = item[3];
                        newRow.VoltagemDeSaida = item[4];
                        newRow.Categoria = ConstStrings.C_Roteador;
                        importSheet.itens.Add(newRow);
                    }
                    break;
                case CurrentEstoque.Fumsoft:
                case CurrentEstoque.ESF:
                case CurrentEstoque.Clientes:
                case CurrentEstoque.Concert:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Patrimonio = item[0];
                        newRow.Modelo = item[1];
                        newRow.Fabricante = item[2];
                        newRow.Wireless = item[3];
                        newRow.QuantidadeDePortas = item[4];
                        newRow.Categoria = ConstStrings.C_Roteador;
                        importSheet.itens.Add(newRow);
                    }
                    break;
                default:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Modelo = item[0];
                        newRow.Wireless = item[1];
                        newRow.QuantidadeDePortas = item[2];
                        newRow.BandaMaxima = item[3];
                        newRow.VoltagemDeSaida = item[4];
                        newRow.Categoria = ConstStrings.C_Roteador;
                        importSheet.itens.Add(newRow);
                    }
                    break;
            }

        }

        public static void ImportServidor(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                case CurrentEstoque.Testing:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Patrimonio = item[0];
                        newRow.Modelo = item[1];
                        newRow.Fabricante = item[2];
                        newRow.ModeloPlacaMae = item[3];
                        newRow.Fonte = item[4];
                        newRow.Memoria = item[5];
                        newRow.HD = item[6];
                        newRow.PlacaDeVideo = item[7];
                        newRow.PlacaDeRede = item[8];
                        newRow.Processador = item[9];
                        newRow.MemoriasSuportadas = item[10];
                        newRow.QuantasMemorias = item[11];
                        newRow.OrdemDasMemorias = item[12];
                        newRow.CapacidadeRAMTotal = item[13];
                        newRow.Soquete = item[14];
                        newRow.PlacaControladora = item[15];
                        newRow.AteQuantosHDs = item[16];
                        newRow.TipoDeHD = item[17];
                        newRow.TipoDeRAID = item[18];
                        newRow.Categoria = ConstStrings.C_Servidor;
                        importSheet.itens.Add(newRow);
                    }
                    break;
                case CurrentEstoque.Fumsoft:
                case CurrentEstoque.ESF:
                case CurrentEstoque.Clientes:
                case CurrentEstoque.Concert:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Patrimonio = item[0];
                        newRow.Modelo = item[1];
                        newRow.Fabricante = item[2];
                        newRow.HD = item[3];
                        newRow.Memoria = item[4];
                        newRow.Memoria = item[5];
                        newRow.Processador = item[6];
                        newRow.Windows = item[7];
                        newRow.Categoria = ConstStrings.C_Servidor;
                        importSheet.itens.Add(newRow);
                    }
                    break;
                default:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Patrimonio = item[0];
                        newRow.Modelo = item[1];
                        newRow.Fabricante = item[2];
                        newRow.ModeloPlacaMae = item[3];
                        newRow.Fonte = item[4];
                        newRow.Memoria = item[5];
                        newRow.HD = item[6];
                        newRow.PlacaDeVideo = item[7];
                        newRow.PlacaDeRede = item[8];
                        newRow.Processador = item[9];
                        newRow.MemoriasSuportadas = item[10];
                        newRow.QuantasMemorias = item[11];
                        newRow.OrdemDasMemorias = item[12];
                        newRow.CapacidadeRAMTotal = item[13];
                        newRow.Soquete = item[14];
                        newRow.PlacaControladora = item[15];
                        newRow.AteQuantosHDs = item[16];
                        newRow.TipoDeHD = item[17];
                        newRow.TipoDeRAID = item[18];
                        newRow.Categoria = ConstStrings.C_Servidor;
                        importSheet.itens.Add(newRow);
                    }
                    break;
            }
        }

        public static void ImportStorageNas(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            foreach (JSONNode item in inventario)
            {
                ItemColumns newRow = new ItemColumns();
                newRow.Modelo = item[0];
                newRow.Tamanho = item[1];
                newRow.TipoDeRAID = item[2];
                newRow.TipoDeHD = item[3];
                newRow.CapacidadeMaxHD = item[4];
                newRow.AteQuantosHDs = item[5];
                newRow.EstoqueAtual = item[6];
                newRow.Categoria = ConstStrings.C_StorageNAS;
                importSheet.itens.Add(newRow);
            }
        }

        public static void ImportSwitch(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = new Sheet();
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.SnPro:
                case CurrentEstoque.Testing:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Modelo = item[0];
                        newRow.QuaisConexoes = item[1];
                        newRow.Desempenho = item[2];
                        newRow.Categoria = ConstStrings.C_Switch;
                        importSheet.itens.Add(newRow);
                    }
                    break;
                case CurrentEstoque.Fumsoft:
                case CurrentEstoque.ESF:
                case CurrentEstoque.Clientes:
                case CurrentEstoque.Concert:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Patrimonio = item[0];
                        newRow.Modelo = item[1];
                        newRow.Fabricante = item[2];
                        newRow.QuaisConexoes = item[3];
                        newRow.Categoria = ConstStrings.C_Switch;
                        importSheet.itens.Add(newRow);
                    }
                    break;
                default:
                    foreach (JSONNode item in inventario)
                    {
                        ItemColumns newRow = new ItemColumns();
                        newRow.Modelo = item[0];
                        newRow.QuaisConexoes = item[1];
                        newRow.Desempenho = item[2];
                        newRow.Categoria = ConstStrings.C_Switch;
                        importSheet.itens.Add(newRow);
                    }
                    break;
            }

        }
    }
}
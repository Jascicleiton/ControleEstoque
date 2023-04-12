/// <summary>
/// Class used to fill all category databases locally. Either for saving or for consult
/// </summary>
public class FillCategoryDatabasesFunctions
{
    #region adaptadorAC
    public static void AdaptadorAC(ItemColumns item, Sheet adaptadoracTempSheet)
    {       
        if(adaptadoracTempSheet != null && adaptadoracTempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in adaptadoracTempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if(item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.OndeFunciona = itemToAdd.OndeFunciona;
                            item.VoltagemDeSaida = itemToAdd.VoltagemDeSaida;
                            item.AmperagemDeSaida = itemToAdd.AmperagemDeSaida;
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.VoltagemDeSaida = itemToAdd.VoltagemDeSaida;
                            item.AmperagemDeSaida = itemToAdd.AmperagemDeSaida;
                        }
                        break;
                }             
            }
        }
        InternalDatabase.adaptadorAC.itens.Add(item);
    }
    #endregion
    #region carregador
    public static void Carregador(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.OndeFunciona = itemToAdd.OndeFunciona;
                            item.VoltagemDeSaida = itemToAdd.VoltagemDeSaida;
                            item.AmperagemDeSaida = itemToAdd.AmperagemDeSaida;
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.VoltagemDeSaida = itemToAdd.VoltagemDeSaida;
                            item.AmperagemDeSaida = itemToAdd.AmperagemDeSaida;
                        }
                        break;
                }
            }
        }
        InternalDatabase.carregador.itens.Add(item);
    }
    #endregion
    #region desktop
    public static void Desktop(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.ModeloPlacaMae = itemToAdd.OndeFunciona;
                            item.Fonte = itemToAdd.Fonte;
                            item.Memoria = itemToAdd.Memoria;
                            item.HD = itemToAdd.HD;
                            item.PlacaDeVideo = itemToAdd.PlacaDeVideo;
                            item.PlacaDeRede = itemToAdd.PlacaDeRede;
                            item.LeitorDeDVD = itemToAdd.LeitorDeDVD;
                            item.Processador = itemToAdd.Processador;
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.HD = itemToAdd.HD;
                            item.Memoria = itemToAdd.Memoria;
                            item.Processador = itemToAdd.Processador;
                            item.Windows = itemToAdd.Windows;
                        }
                        break;
                }
            }
        }
        InternalDatabase.desktop.itens.Add(item);
    }
    #endregion
    #region fonte
    public static void Fonte(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.Watts = itemToAdd.Watts;
                            item.OndeFunciona = itemToAdd.OndeFunciona;
                            item.Conectores = itemToAdd.Conectores;
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.Watts = itemToAdd.Watts;
                            item.Conectores = itemToAdd.Conectores;
                        }
                        break;
                }
            }
        }
        InternalDatabase.fonte.itens.Add(item);
    }
    #endregion
    #region gbic
    public static void Gbic(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.Desempenho = itemToAdd.Desempenho;
                                 }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.Desempenho = itemToAdd.Desempenho;
                         }
                        break;
                }
            }
        }
        InternalDatabase.gbic.itens.Add(item);
    }
    #endregion
    #region hd
    public static void HD(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.Interface = itemToAdd.Interface;
                            item.Tamanho = itemToAdd.Tamanho;
                            item.FormaDeArmazenamento = itemToAdd.FormaDeArmazenamento;
                            item.CapacidadeEmGB = itemToAdd.CapacidadeEmGB;
                            item.RPM = itemToAdd.RPM;
                            item.VelocidadeDeLeitura = itemToAdd.VelocidadeDeLeitura;
                            item.Enterprise = itemToAdd.Enterprise;
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.Interface = itemToAdd.Interface;
                            item.Tamanho = itemToAdd.Tamanho;
                            item.FormaDeArmazenamento = itemToAdd.FormaDeArmazenamento;
                            item.CapacidadeEmGB = itemToAdd.CapacidadeEmGB;
                            item.RPM = itemToAdd.RPM;
                            item.VelocidadeDeLeitura = itemToAdd.VelocidadeDeLeitura;
                            item.Enterprise = itemToAdd.Enterprise;
                        }
                        break;
                }
            }
        }
        InternalDatabase.hd.itens.Add(item);
    }

    #endregion
    #region idrac
    public static void Idrac(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.QuaisConexoes = itemToAdd.QuaisConexoes;
                            item.VelocidadeGBs = itemToAdd.VelocidadeGBs;
                            item.EntradaSD = itemToAdd.EntradaSD;
                            item.ServidoresSuportados = itemToAdd.ServidoresSuportados;
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.QuaisConexoes = itemToAdd.QuaisConexoes;
                            item.VelocidadeGBs = itemToAdd.VelocidadeGBs;
                            item.EntradaSD = itemToAdd.EntradaSD;
                            item.ServidoresSuportados = itemToAdd.ServidoresSuportados;
                        }
                        break;
                }
            }
        }
        InternalDatabase.idrac.itens.Add(item);
    }

    #endregion
    #region memoria
    public static void Memoria(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.Tipo = itemToAdd.Tipo;
                            item.CapacidadeEmGB = itemToAdd.CapacidadeEmGB;
                            item.VelocidadeMHz = itemToAdd.VelocidadeMHz;
                            item.LowVoltage = itemToAdd.LowVoltage;
                            item.Rank = itemToAdd.Rank;
                            item.DIMM = itemToAdd.DIMM;
                            item.TaxaDeTransmissao = itemToAdd.TaxaDeTransmissao;
                            item.Simbolo = itemToAdd.Simbolo;
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.Tipo = itemToAdd.Tipo;
                            item.CapacidadeEmGB = itemToAdd.CapacidadeEmGB;
                            item.VelocidadeMHz = itemToAdd.VelocidadeMHz;
                            item.LowVoltage = itemToAdd.LowVoltage;
                            item.Rank = itemToAdd.Rank;
                            item.DIMM = itemToAdd.DIMM;
                            item.TaxaDeTransmissao = itemToAdd.TaxaDeTransmissao;
                            item.Simbolo = itemToAdd.Simbolo;
                        }
                        break;
                }
            }
        }
        InternalDatabase.memoria.itens.Add(item);
    }

    #endregion
    #region monitor
    public static void Monitor(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.Polegadas = itemToAdd.Polegadas;
                            item.QuaisConexoes = itemToAdd.QuaisConexoes;
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.Polegadas = itemToAdd.Polegadas;
                            item.QuaisConexoes = itemToAdd.QuaisConexoes;
                        }
                        break;
                }
            }
        }
        InternalDatabase.monitor.itens.Add(item);
    }

    #endregion
    #region notebook
    public static void Notebook(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.HD = itemToAdd.HD;
                            item.Memoria = itemToAdd.Memoria;
                            item.EntradaRJ45 = itemToAdd.EntradaRJ45;
                            item.BateriaInclusa = itemToAdd.BateriaInclusa;
                            item.AdaptadorAC = itemToAdd.AdaptadorAC;
                            item.Windows = itemToAdd.Windows;
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.HD = itemToAdd.HD;
                            item.Memoria = itemToAdd.Memoria;
                            item.Processador = itemToAdd.Processador;
                            item.Windows = itemToAdd.Windows;
                        }
                        break;
                }
            }
        }
        InternalDatabase.notebook.itens.Add(item);
    }
    #endregion
    #region placaControladora
    public static void PlacaControladora(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.QuaisConexoes = itemToAdd.QuaisConexoes;
                            item.QuantidadeDePortas = itemToAdd.QuantidadeDePortas;
                            item.TipoDeRAID = itemToAdd.TipoDeRAID;
                            item.TipoDeHD = itemToAdd.TipoDeHD;
                            item.CapacidadeMaxHD = itemToAdd.CapacidadeMaxHD;
                            item.AteQuantosHDs = itemToAdd.AteQuantosHDs;
                            item.BateriaInclusa = itemToAdd.BateriaInclusa;
                            item.Barramento = itemToAdd.Barramento;
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.QuaisConexoes = itemToAdd.QuaisConexoes;
                            item.QuantidadeDePortas = itemToAdd.QuantidadeDePortas;
                            item.TipoDeRAID = itemToAdd.TipoDeRAID;
                            item.TipoDeHD = itemToAdd.TipoDeHD;
                            item.CapacidadeMaxHD = itemToAdd.CapacidadeMaxHD;
                            item.AteQuantosHDs = itemToAdd.AteQuantosHDs;
                            item.BateriaInclusa = itemToAdd.BateriaInclusa;
                            item.Barramento = itemToAdd.Barramento;
                        }
                        break;
                }
            }
        }
        InternalDatabase.placaControladora.itens.Add(item);
    }
        #endregion
    #region placaDeCapturaDeVideo
    public static void PlacaDeCapturaDeVideo(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.QuantidadeDePortas = itemToAdd.QuantidadeDePortas;
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.QuantidadeDePortas = itemToAdd.QuantidadeDePortas;
                        }
                        break;
                }
            }
        }
        InternalDatabase.placaDeCapturaDeVideo.itens.Add(item);
    }
    #endregion
    #region placaDeRede
    public static void PlacaDeRede(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.Interface = itemToAdd.Interface;
                            item.QuantidadeDePortas = itemToAdd.QuantidadeDePortas;
                            item.QuaisConexoes = itemToAdd.QuaisConexoes;
                            item.SuportaFibraOptica = itemToAdd.SuportaFibraOptica;
                            item.Desempenho = itemToAdd.Desempenho;
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.Interface = itemToAdd.Interface;
                            item.QuantidadeDePortas = itemToAdd.QuantidadeDePortas;
                            item.QuaisConexoes = itemToAdd.QuaisConexoes;
                            item.SuportaFibraOptica = itemToAdd.SuportaFibraOptica;
                            item.Desempenho = itemToAdd.Desempenho;
                        }
                        break;
                }
            }
        }
        InternalDatabase.placaDeRede.itens.Add(item);
    }
    #endregion
    #region placaDeSom
    public static void PlacaDeSom(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.QuantidadeDePortas = itemToAdd.QuantidadeDePortas;
                                                    }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.QuantidadeDePortas = itemToAdd.QuantidadeDePortas;
                                                   }
                        break;
                }
            }
        }
        InternalDatabase.placaDeSom.itens.Add(item);
    }
    #endregion
    #region placaDeVideo
    public static void PlacaDeVideo(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.QuantidadeDePortas = itemToAdd.QuantidadeDePortas;
                            item.QuaisConexoes = itemToAdd.QuaisConexoes;
                                                   }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.QuantidadeDePortas = itemToAdd.QuantidadeDePortas;
                            item.QuaisConexoes = itemToAdd.QuaisConexoes;
                        }
                        break;
                }
            }
        }
        InternalDatabase.placaDeVideo.itens.Add(item);
    }
    #endregion
    #region processador
    public static void Processador(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.Soquete = itemToAdd.Soquete;
                            item.NucleosFisicos = itemToAdd.NucleosFisicos;
                            item.NucleosLogicos = itemToAdd.NucleosLogicos;                     
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.Soquete = itemToAdd.Soquete;
                            item.NucleosFisicos = itemToAdd.NucleosFisicos;
                            item.NucleosLogicos = itemToAdd.NucleosLogicos;
                        }
                        break;
                }
            }
        }
        InternalDatabase.processador.itens.Add(item);
    }
    #endregion
    #region roteador
    public static void Roteador(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.Wireless = itemToAdd.Wireless;
                            item.QuantidadeDePortas = itemToAdd.QuantidadeDePortas;
                            item.BandaMaxima = itemToAdd.BandaMaxima;
                             }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.Wireless = itemToAdd.Wireless;
                            item.QuantidadeDePortas = itemToAdd.QuantidadeDePortas;                          
                        }
                        break;
                }
            }
        }
        InternalDatabase.roteador.itens.Add(item);
    }
    #endregion
    #region servidor
    public static void Servidor(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.ModeloPlacaMae = itemToAdd.ModeloPlacaMae;
                            item.Fonte = itemToAdd.Fonte;
                            item.Memoria = itemToAdd.Memoria;
                            item.HD = itemToAdd.HD;
                            item.PlacaDeVideo = itemToAdd.PlacaDeVideo;
                            item.PlacaDeRede = itemToAdd.PlacaDeRede;
                            item.Processador = itemToAdd.Processador;
                            item.MemoriasSuportadas = itemToAdd.MemoriasSuportadas;
                            item.QuantasMemorias = itemToAdd.QuantasMemorias;
                            item.OrdemDasMemorias = itemToAdd.OrdemDasMemorias;
                            item.CapacidadeRAMTotal = itemToAdd.CapacidadeRAMTotal;
                            item.Soquete = itemToAdd.Soquete;
                            item.PlacaControladora = itemToAdd.PlacaControladora;
                            item.AteQuantosHDs = itemToAdd.AteQuantosHDs;
                            item.TipoDeHD = itemToAdd.TipoDeHD;
                            item.TipoDeRAID = itemToAdd.TipoDeRAID;
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.HD = itemToAdd.HD;
                            item.Memoria = itemToAdd.Memoria;
                            item.Processador = itemToAdd.Processador;
                            item.Windows = itemToAdd.Windows;
                        }
                        break;
                }
            }
        }
        InternalDatabase.servidor.itens.Add(item);
    }
    #endregion
    #region storageNAS
    public static void StorageNas(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                            item.Tamanho = itemToAdd.Tamanho;
                            item.TipoDeRAID = itemToAdd.TipoDeRAID;
                            item.TipoDeHD = itemToAdd.TipoDeHD;
                            item.CapacidadeMaxHD = itemToAdd.CapacidadeMaxHD;
                            item.CapacidadeMaxHD = itemToAdd.CapacidadeMaxHD;
                            item.AteQuantosHDs = itemToAdd.AteQuantosHDs;
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.Tamanho = itemToAdd.Tamanho;
                            item.TipoDeRAID = itemToAdd.TipoDeRAID;
                            item.TipoDeHD = itemToAdd.TipoDeHD;
                            item.CapacidadeMaxHD = itemToAdd.CapacidadeMaxHD;
                            item.CapacidadeMaxHD = itemToAdd.CapacidadeMaxHD;
                            item.AteQuantosHDs = itemToAdd.AteQuantosHDs;
                        }
                        break;
                }
            }
        }
        InternalDatabase.storageNAS.itens.Add(item);
    }
    #endregion
    #region Switch
    public static void Switch(ItemColumns item, Sheet tempSheet)
    {
        if (tempSheet != null && tempSheet.itens.Count > 0)
        {
            foreach (ItemColumns itemToAdd in tempSheet.itens)
            {
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if (item.Modelo.Trim().Equals(itemToAdd.Modelo.Trim(), System.StringComparison.OrdinalIgnoreCase))
                        {
                                                       item.QuaisConexoes = itemToAdd.QuaisConexoes;
                            item.Desempenho = itemToAdd.Desempenho;
                        }
                        break;
                    default:
                        if (item.Patrimonio == itemToAdd.Patrimonio)
                        {
                            item.QuaisConexoes = itemToAdd.QuaisConexoes;
                                                    }
                        break;
                }
            }
        }
        InternalDatabase.Switch.itens.Add(item);
    }
    #endregion
}

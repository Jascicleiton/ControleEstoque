using UnityEngine.UIElements;

public class AddItemLocal 
{
    /// <summary>
    /// Add a new item to all internal databases
    /// </summary>
    public static void AddItem(TextField[] parameterValues, string category)
    {
        ItemColumns itemToAddFullDatabase = new ItemColumns();
        ItemColumns itemToAddCategoryDatabase = new ItemColumns();

        #region Inventario
        itemToAddFullDatabase.Aquisicao = parameterValues[0].text;
        itemToAddCategoryDatabase.Aquisicao = parameterValues[0].text;
        itemToAddFullDatabase.Entrada = parameterValues[1].text;
        itemToAddCategoryDatabase.Entrada = parameterValues[1].text;
        if (!int.TryParse(parameterValues[2].text, out itemToAddFullDatabase.Patrimonio))
        {
            itemToAddFullDatabase.Patrimonio = -666;
            itemToAddCategoryDatabase.Patrimonio = -666;
        }
        itemToAddCategoryDatabase.Patrimonio = itemToAddFullDatabase.Patrimonio;
        itemToAddFullDatabase.Status = parameterValues[3].text;
        itemToAddCategoryDatabase.Status = parameterValues[3].text;
        itemToAddFullDatabase.Serial = parameterValues[4].text;
        itemToAddCategoryDatabase.Serial = parameterValues[4].text;
        itemToAddFullDatabase.Fabricante = parameterValues[6].text;
        itemToAddCategoryDatabase.Fabricante = parameterValues[6].text;
        itemToAddFullDatabase.Modelo = parameterValues[7].text;
        itemToAddCategoryDatabase.Modelo = parameterValues[7].text;
        itemToAddFullDatabase.Local = parameterValues[8].text;
        itemToAddCategoryDatabase.Local = parameterValues[8].text;
        itemToAddFullDatabase.Saida = "";
        itemToAddCategoryDatabase.Saida = "";
        if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.Concert)
        {
            itemToAddFullDatabase.Observacao = parameterValues[10].text;
            itemToAddCategoryDatabase.Observacao = parameterValues[10].text;
        }
        else
        {
            itemToAddFullDatabase.Pessoa = parameterValues[9].text;
            itemToAddFullDatabase.CentroDeCusto = parameterValues[10].text;
            itemToAddCategoryDatabase.Pessoa = parameterValues[9].text;
            itemToAddCategoryDatabase.CentroDeCusto = parameterValues[10].text;
        }
        #endregion
        switch (category)
        {
            #region HD
            case ConstStrings.HD:
                itemToAddFullDatabase.Categoria = ConstStrings.HD;
                //InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.Concert)
                {
                    itemToAddFullDatabase.Interface = parameterValues[12].text;
                    itemToAddCategoryDatabase.Interface = parameterValues[12].text;
                    if (!float.TryParse(parameterValues[13].text, out itemToAddFullDatabase.Tamanho))
                    {
                        itemToAddFullDatabase.Tamanho = 0f;
                        itemToAddCategoryDatabase.Tamanho = 0f;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.Tamanho = itemToAddFullDatabase.Tamanho;
                    }
                    itemToAddFullDatabase.FormaDeArmazenamento = parameterValues[14].text;
                    itemToAddCategoryDatabase.FormaDeArmazenamento = parameterValues[14].text;
                    if (!int.TryParse(parameterValues[15].text, out itemToAddFullDatabase.CapacidadeEmGB))
                    {
                        itemToAddFullDatabase.CapacidadeEmGB = 0;
                        itemToAddCategoryDatabase.CapacidadeEmGB = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.CapacidadeEmGB = itemToAddFullDatabase.CapacidadeEmGB;
                    }
                    if (!int.TryParse(parameterValues[16].text, out itemToAddFullDatabase.RPM))
                    {
                        itemToAddFullDatabase.RPM = 0;
                        itemToAddCategoryDatabase.RPM = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.RPM = itemToAddFullDatabase.RPM;
                    }
                    if (!float.TryParse(parameterValues[17].text, out itemToAddFullDatabase.VelocidadeDeLeitura))
                    {
                        itemToAddFullDatabase.VelocidadeDeLeitura = 0f;
                        itemToAddCategoryDatabase.VelocidadeDeLeitura = 0f;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.VelocidadeDeLeitura = itemToAddFullDatabase.VelocidadeDeLeitura;
                    }
                    itemToAddFullDatabase.Enterprise = parameterValues[18].text;
                    itemToAddCategoryDatabase.Enterprise = parameterValues[18].text;
                }
                else
                {
                    itemToAddFullDatabase.Interface = parameterValues[11].text;
                    itemToAddCategoryDatabase.Interface = parameterValues[11].text;
                    if (!float.TryParse(parameterValues[12].text, out itemToAddFullDatabase.Tamanho))
                    {
                        itemToAddFullDatabase.Tamanho = 0f;
                        itemToAddCategoryDatabase.Tamanho = 0f;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.Tamanho = itemToAddFullDatabase.Tamanho;
                    }
                    itemToAddFullDatabase.FormaDeArmazenamento = parameterValues[13].text;
                    itemToAddCategoryDatabase.FormaDeArmazenamento = parameterValues[13].text;
                    if (!int.TryParse(parameterValues[14].text, out itemToAddFullDatabase.CapacidadeEmGB))
                    {
                        itemToAddFullDatabase.CapacidadeEmGB = 0;
                        itemToAddCategoryDatabase.CapacidadeEmGB = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.CapacidadeEmGB = itemToAddFullDatabase.CapacidadeEmGB;
                    }
                    if (!int.TryParse(parameterValues[15].text, out itemToAddFullDatabase.RPM))
                    {
                        itemToAddFullDatabase.RPM = 0;
                        itemToAddCategoryDatabase.RPM = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.RPM = itemToAddFullDatabase.RPM;
                    }
                    if (!float.TryParse(parameterValues[16].text, out itemToAddFullDatabase.VelocidadeDeLeitura))
                    {
                        itemToAddFullDatabase.VelocidadeDeLeitura = 0f;
                        itemToAddCategoryDatabase.VelocidadeDeLeitura = 0f;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.VelocidadeDeLeitura = itemToAddFullDatabase.VelocidadeDeLeitura;
                    }
                    itemToAddFullDatabase.Enterprise = parameterValues[17].text;
                    itemToAddCategoryDatabase.Enterprise = parameterValues[17].text;
                }
               // InternalDatabase.Instance.splitDatabase[ConstStrings.HD].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.hd.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Memoria
            case ConstStrings.Memoria:
                itemToAddFullDatabase.Categoria = ConstStrings.Memoria;
                //InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.Concert)
                {
                    itemToAddFullDatabase.Tipo = parameterValues[12].text;
                    itemToAddCategoryDatabase.Tipo = parameterValues[12].text;
                    if (!int.TryParse(parameterValues[13].text, out itemToAddFullDatabase.CapacidadeEmGB))
                    {
                        itemToAddFullDatabase.CapacidadeEmGB = 0;
                        itemToAddCategoryDatabase.CapacidadeEmGB = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.CapacidadeEmGB = itemToAddFullDatabase.CapacidadeEmGB;
                    }
                    if (!int.TryParse(parameterValues[14].text, out itemToAddFullDatabase.VelocidadeMHz))
                    {
                        itemToAddFullDatabase.VelocidadeMHz = 0;
                        itemToAddCategoryDatabase.VelocidadeMHz = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.VelocidadeMHz = itemToAddFullDatabase.VelocidadeMHz;
                    }
                    itemToAddFullDatabase.LowVoltage = parameterValues[15].text;
                    itemToAddCategoryDatabase.LowVoltage = parameterValues[15].text;
                    itemToAddFullDatabase.Rank = parameterValues[16].text;
                    itemToAddCategoryDatabase.Rank = parameterValues[16].text;
                    itemToAddFullDatabase.DIMM = parameterValues[17].text;
                    itemToAddCategoryDatabase.DIMM = parameterValues[17].text;
                    if (!int.TryParse(parameterValues[18].text, out itemToAddFullDatabase.TaxaDeTransmissao))
                    {
                        itemToAddFullDatabase.TaxaDeTransmissao = 0;
                        itemToAddCategoryDatabase.TaxaDeTransmissao = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.TaxaDeTransmissao = itemToAddFullDatabase.TaxaDeTransmissao;
                    }
                    itemToAddFullDatabase.Simbolo = parameterValues[19].text;
                    itemToAddCategoryDatabase.Simbolo = parameterValues[19].text;
                }
                else
                {
                    itemToAddFullDatabase.Tipo = parameterValues[11].text;
                    itemToAddCategoryDatabase.Tipo = parameterValues[11].text;
                    if (!int.TryParse(parameterValues[12].text, out itemToAddFullDatabase.CapacidadeEmGB))
                    {
                        itemToAddFullDatabase.CapacidadeEmGB = 0;
                        itemToAddCategoryDatabase.CapacidadeEmGB = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.CapacidadeEmGB = itemToAddFullDatabase.CapacidadeEmGB;
                    }
                    if (!int.TryParse(parameterValues[13].text, out itemToAddFullDatabase.VelocidadeMHz))
                    {
                        itemToAddFullDatabase.VelocidadeMHz = 0;
                        itemToAddCategoryDatabase.VelocidadeMHz = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.VelocidadeMHz = itemToAddFullDatabase.VelocidadeMHz;
                    }
                    itemToAddFullDatabase.LowVoltage = parameterValues[14].text;
                    itemToAddCategoryDatabase.LowVoltage = parameterValues[14].text;
                    itemToAddFullDatabase.Rank = parameterValues[15].text;
                    itemToAddCategoryDatabase.Rank = parameterValues[15].text;
                    itemToAddFullDatabase.DIMM = parameterValues[16].text;
                    itemToAddCategoryDatabase.DIMM = parameterValues[16].text;
                    if (!int.TryParse(parameterValues[17].text, out itemToAddFullDatabase.TaxaDeTransmissao))
                    {
                        itemToAddFullDatabase.TaxaDeTransmissao = 0;
                        itemToAddCategoryDatabase.TaxaDeTransmissao = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.TaxaDeTransmissao = itemToAddFullDatabase.TaxaDeTransmissao;
                    }
                    itemToAddFullDatabase.Simbolo = parameterValues[18].text;
                    itemToAddCategoryDatabase.Simbolo = parameterValues[18].text;
                }
              //  InternalDatabase.Instance.splitDatabase[ConstStrings.Memoria].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.memoria.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Placa de rede
            case ConstStrings.PlacaDeRede:
                itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeRede;
                //InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.Concert)
                {
                    itemToAddFullDatabase.Interface = parameterValues[12].text;
                    itemToAddCategoryDatabase.Interface = parameterValues[14].text;
                    if (!int.TryParse(parameterValues[13].text, out itemToAddFullDatabase.QuantidadeDePortas))
                    {
                        itemToAddFullDatabase.QuantidadeDePortas = 0;
                        itemToAddCategoryDatabase.QuantidadeDePortas = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.QuantidadeDePortas = itemToAddFullDatabase.QuantidadeDePortas;
                    }
                    itemToAddFullDatabase.QuaisConexoes = parameterValues[14].text;
                    itemToAddCategoryDatabase.QuaisConexoes = parameterValues[14].text;
                    itemToAddFullDatabase.SuportaFibraOptica = parameterValues[15].text;
                    itemToAddCategoryDatabase.SuportaFibraOptica = parameterValues[15].text;
                    itemToAddFullDatabase.Desempenho = parameterValues[16].text;
                    itemToAddCategoryDatabase.Desempenho = parameterValues[16].text;
                }
                else
                {
                    itemToAddFullDatabase.Interface = parameterValues[11].text;
                    itemToAddCategoryDatabase.Interface = parameterValues[11].text;
                    if (!int.TryParse(parameterValues[12].text, out itemToAddFullDatabase.QuantidadeDePortas))
                    {
                        itemToAddFullDatabase.QuantidadeDePortas = 0;
                        itemToAddCategoryDatabase.QuantidadeDePortas = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.QuantidadeDePortas = itemToAddFullDatabase.QuantidadeDePortas;
                    }
                    itemToAddFullDatabase.QuaisConexoes = parameterValues[13].text;
                    itemToAddCategoryDatabase.QuaisConexoes = parameterValues[13].text;
                    itemToAddFullDatabase.SuportaFibraOptica = parameterValues[14].text;
                    itemToAddCategoryDatabase.SuportaFibraOptica = parameterValues[14].text;
                    itemToAddFullDatabase.Desempenho = parameterValues[15].text;                    
                    itemToAddCategoryDatabase.Desempenho = parameterValues[15].text;
                }
              //  InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeRede].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.placaDeRede.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region iDrac
            case ConstStrings.Idrac:
                itemToAddFullDatabase.Categoria = ConstStrings.Idrac;
                //InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                if(InternalDatabase.Instance.currentEstoque == CurrentEstoque.Concert)
                {
                    itemToAddFullDatabase.QuaisConexoes = parameterValues[12].text;
                    itemToAddCategoryDatabase.QuaisConexoes = parameterValues[12].text;
                    if (!float.TryParse(parameterValues[13].text, out itemToAddFullDatabase.VelocidadeGBs))
                    {
                        itemToAddFullDatabase.VelocidadeGBs = 0;
                        itemToAddCategoryDatabase.VelocidadeGBs = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.VelocidadeGBs = itemToAddFullDatabase.VelocidadeGBs;
                    }
                    itemToAddFullDatabase.EntradaSD = parameterValues[14].text;
                    itemToAddCategoryDatabase.EntradaSD = parameterValues[14].text;
                    itemToAddFullDatabase.ServidoresSuportados = parameterValues[15].text;                    
                    itemToAddCategoryDatabase.ServidoresSuportados = parameterValues[15].text;
                }
                else
                {
                    itemToAddFullDatabase.QuaisConexoes = parameterValues[11].text;
                    if (!float.TryParse(parameterValues[12].text, out itemToAddFullDatabase.VelocidadeGBs))
                    {
                        itemToAddFullDatabase.VelocidadeGBs = 0;
                        itemToAddCategoryDatabase.VelocidadeGBs = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.VelocidadeGBs = itemToAddFullDatabase.VelocidadeGBs;
                    }
                    itemToAddFullDatabase.EntradaSD = parameterValues[13].text;
                    itemToAddFullDatabase.ServidoresSuportados = parameterValues[14].text;
                    itemToAddCategoryDatabase.QuaisConexoes = parameterValues[11].text;
                    itemToAddCategoryDatabase.EntradaSD = parameterValues[13].text;
                    itemToAddCategoryDatabase.ServidoresSuportados = parameterValues[14].text;
                }               
               // InternalDatabase.Instance.splitDatabase[ConstStrings.Idrac].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.idrac.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Placa controladora
            case ConstStrings.PlacaControladora:
                itemToAddFullDatabase.Categoria = ConstStrings.PlacaControladora;
               // InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);              
                itemToAddFullDatabase.QuaisConexoes = parameterValues[11].text;
                if (!int.TryParse(parameterValues[12].text, out itemToAddFullDatabase.QuantidadeDePortas))
                {
                    itemToAddFullDatabase.QuantidadeDePortas = 0;
                    itemToAddCategoryDatabase.QuantidadeDePortas = 0;
                }
                else
                {
                    itemToAddCategoryDatabase.QuantidadeDePortas = itemToAddFullDatabase.QuantidadeDePortas;
                }
                itemToAddFullDatabase.TipoDeRAID = parameterValues[13].text;
                itemToAddFullDatabase.TipoDeHD = parameterValues[14].text;
                itemToAddFullDatabase.CapacidadeMaxHD = parameterValues[15].text;
                if (!int.TryParse(parameterValues[16].text, out itemToAddFullDatabase.AteQuantosHDs))
                {
                    itemToAddFullDatabase.AteQuantosHDs = 0;
                    itemToAddCategoryDatabase.AteQuantosHDs = 0;
                }
                else
                {
                    itemToAddCategoryDatabase.AteQuantosHDs = itemToAddFullDatabase.AteQuantosHDs;
                }
                itemToAddFullDatabase.BateriaInclusa = parameterValues[17].text;
                itemToAddFullDatabase.Barramento = parameterValues[18].text;
                itemToAddCategoryDatabase.QuaisConexoes = parameterValues[11].text;
                itemToAddCategoryDatabase.TipoDeRAID = parameterValues[13].text;
                itemToAddCategoryDatabase.TipoDeHD = parameterValues[14].text;
                itemToAddCategoryDatabase.CapacidadeMaxHD = parameterValues[15].text;
                itemToAddCategoryDatabase.BateriaInclusa = parameterValues[17].text;
                itemToAddCategoryDatabase.Barramento = parameterValues[18].text;

               // InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaControladora].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.placaControladora.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Processador
            case ConstStrings.Processador:
                itemToAddFullDatabase.Categoria = ConstStrings.Processador;
                //InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Soquete = parameterValues[11].text;
                itemToAddCategoryDatabase.Soquete = parameterValues[11].text;
                if (!int.TryParse(parameterValues[12].text, out itemToAddFullDatabase.NucleosFisicos))
                {
                    itemToAddFullDatabase.NucleosFisicos = 0;
                    itemToAddCategoryDatabase.NucleosFisicos = 0;
                }
                else
                {
                    itemToAddCategoryDatabase.NucleosFisicos = itemToAddFullDatabase.NucleosFisicos;
                }
                if(!int.TryParse(parameterValues[13].text, out itemToAddFullDatabase.NucleosLogicos))
                    {
                    itemToAddFullDatabase.NucleosLogicos = 0;
                    itemToAddCategoryDatabase.NucleosLogicos = 0;
                }
                else
                {
                    itemToAddCategoryDatabase.NucleosLogicos = itemToAddFullDatabase.NucleosLogicos;
                }
               // InternalDatabase.Instance.splitDatabase[ConstStrings.Processador].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.processador.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Desktop
            case ConstStrings.Desktop:
                itemToAddFullDatabase.Categoria = ConstStrings.Desktop;
                //InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.Concert)
                {
                                       itemToAddFullDatabase.HD = parameterValues[12].text;
                    itemToAddFullDatabase.Memoria = parameterValues[13].text;                    
                    itemToAddFullDatabase.Processador = parameterValues[14].text;
                    itemToAddFullDatabase.Windows = parameterValues[15].text;
                    itemToAddCategoryDatabase.HD = parameterValues[12].text;
                    itemToAddCategoryDatabase.Memoria = parameterValues[13].text;
                    itemToAddCategoryDatabase.Processador = parameterValues[14].text;
                    itemToAddCategoryDatabase.Windows = parameterValues[15].text;
                }
                else if(InternalDatabase.Instance.currentEstoque == CurrentEstoque.SnPro)
                {
                    itemToAddFullDatabase.ModeloPlacaMae = parameterValues[11].text;
                    itemToAddFullDatabase.Fonte = parameterValues[12].text;
                    itemToAddFullDatabase.Memoria = parameterValues[13].text;
                    itemToAddFullDatabase.HD = parameterValues[14].text;
                    itemToAddFullDatabase.PlacaDeVideo = parameterValues[15].text;
                    itemToAddFullDatabase.PlacaDeRede = parameterValues[16].text;
                    itemToAddFullDatabase.LeitorDeDVD = parameterValues[17].text;
                    itemToAddFullDatabase.Processador = parameterValues[18].text;
                    itemToAddCategoryDatabase.ModeloPlacaMae = parameterValues[11].text;
                    itemToAddCategoryDatabase.Fonte = parameterValues[12].text;
                    itemToAddCategoryDatabase.Memoria = parameterValues[13].text;
                    itemToAddCategoryDatabase.HD = parameterValues[14].text;
                    itemToAddCategoryDatabase.PlacaDeVideo = parameterValues[15].text;
                    itemToAddCategoryDatabase.PlacaDeRede = parameterValues[16].text;
                    itemToAddCategoryDatabase.LeitorDeDVD = parameterValues[17].text;
                    itemToAddCategoryDatabase.Processador = parameterValues[18].text;
                }
                else
                {
                    itemToAddFullDatabase.HD = parameterValues[11].text;
                    itemToAddFullDatabase.Memoria = parameterValues[12].text;
                    itemToAddFullDatabase.Processador = parameterValues[13].text;
                    itemToAddFullDatabase.Windows = parameterValues[14].text;
                    itemToAddCategoryDatabase.HD = parameterValues[11].text;
                    itemToAddCategoryDatabase.Memoria = parameterValues[12].text;
                    itemToAddCategoryDatabase.Processador = parameterValues[13].text;
                    itemToAddCategoryDatabase.Windows = parameterValues[14].text;
                }
             //   InternalDatabase.Instance.splitDatabase[ConstStrings.Desktop].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.desktop.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Fonte
            case ConstStrings.Fonte:
                itemToAddFullDatabase.Categoria = ConstStrings.Fonte;
                //InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                if(!int.TryParse(parameterValues[11].text, out itemToAddFullDatabase.Watts))
                {
                    itemToAddFullDatabase.Watts = 0;
                    itemToAddCategoryDatabase.Watts = 0;
                }
                else
                {
                    itemToAddCategoryDatabase.Watts = itemToAddFullDatabase.Watts;
                }
                itemToAddFullDatabase.OndeFunciona = parameterValues[12].text;
                itemToAddCategoryDatabase.OndeFunciona = parameterValues[12].text;
                itemToAddFullDatabase.Conectores = parameterValues[13].text;                             
                itemToAddCategoryDatabase.Conectores = parameterValues[13].text;

               // InternalDatabase.Instance.splitDatabase[ConstStrings.Fonte].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.fonte.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Switch
            case ConstStrings.Switch:
                itemToAddFullDatabase.Categoria = ConstStrings.Switch;
                //  InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        if(int.TryParse(parameterValues[11].text, out itemToAddFullDatabase.QuantidadeDePortas))
                        {
                            itemToAddFullDatabase.QuantidadeDePortas = 0;
                            itemToAddCategoryDatabase.QuantidadeDePortas = 0;
                        }
                        else
                        {
                            itemToAddCategoryDatabase.QuantidadeDePortas = itemToAddFullDatabase.QuantidadeDePortas;
                        }
                        itemToAddFullDatabase.Desempenho = parameterValues[12].text;
                        itemToAddCategoryDatabase.Desempenho = parameterValues[12].text;
                        break;
                    case CurrentEstoque.Concert:
                        itemToAddFullDatabase.QuaisConexoes = parameterValues[12].text;
                        itemToAddCategoryDatabase.QuaisConexoes = parameterValues[12].text;
                        break;
                    default:
                        itemToAddFullDatabase.QuaisConexoes = parameterValues[12].text;
                        itemToAddCategoryDatabase.QuaisConexoes = parameterValues[12].text;
                        break;
                }
             //   InternalDatabase.Instance.splitDatabase[ConstStrings.Switch].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.Switch.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Roteador
            case ConstStrings.Roteador:
                itemToAddFullDatabase.Categoria = ConstStrings.Roteador;
                // InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        itemToAddFullDatabase.Wireless = parameterValues[11].text;
                        itemToAddCategoryDatabase.Wireless = parameterValues[11].text;
                        if (int.TryParse(parameterValues[12].text, out itemToAddFullDatabase.QuantidadeDePortas))
                        {
                            itemToAddFullDatabase.QuantidadeDePortas = 0;
                            itemToAddCategoryDatabase.QuantidadeDePortas = 0;
                        }
                        if(int.TryParse(parameterValues[13].text, out itemToAddFullDatabase.BandaMaxima))
                        {
                            itemToAddFullDatabase.BandaMaxima = 0;
                            itemToAddCategoryDatabase.BandaMaxima = 0;
                        }
                        else
                        {
                            itemToAddCategoryDatabase.BandaMaxima = itemToAddFullDatabase.BandaMaxima;
                        }
                        if(float.TryParse(parameterValues[14].text, out itemToAddFullDatabase.VoltagemDeSaida))
                        {
                            itemToAddFullDatabase.VoltagemDeSaida = 0f;
                            itemToAddCategoryDatabase.VoltagemDeSaida = 0f;
                        }
                        else
                        {
                            itemToAddCategoryDatabase.VoltagemDeSaida = itemToAddFullDatabase.VoltagemDeSaida;
                        }                        
                        break;
                    case CurrentEstoque.Concert:
                        break;
                    default:
                        break;
                }       
           //     InternalDatabase.Instance.splitDatabase[ConstStrings.Roteador].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.roteador.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Carregador
            case ConstStrings.Carregador:
                itemToAddFullDatabase.Categoria = ConstStrings.Carregador;
                //InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.Concert)
                {
                    if (!float.TryParse(parameterValues[12].text, out itemToAddFullDatabase.VoltagemDeSaida))
                    {
                        itemToAddFullDatabase.VoltagemDeSaida = 0f;
                        itemToAddCategoryDatabase.VoltagemDeSaida = 0f;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.VoltagemDeSaida = itemToAddFullDatabase.VoltagemDeSaida;
                    }
                    if (!float.TryParse(parameterValues[13].text, out itemToAddFullDatabase.AmperagemDeSaida))
                    {
                        itemToAddFullDatabase.AmperagemDeSaida = 0;
                        itemToAddCategoryDatabase.AmperagemDeSaida = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.AmperagemDeSaida = itemToAddFullDatabase.AmperagemDeSaida;
                    }
                }
                else
                {
                    itemToAddFullDatabase.OndeFunciona = parameterValues[11].text;
                    itemToAddCategoryDatabase.OndeFunciona = parameterValues[11].text;
                    if (!float.TryParse(parameterValues[12].text, out itemToAddFullDatabase.VoltagemDeSaida))
                    {
                        itemToAddFullDatabase.VoltagemDeSaida = 0f;
                        itemToAddCategoryDatabase.VoltagemDeSaida = 0f;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.VoltagemDeSaida = itemToAddFullDatabase.VoltagemDeSaida;
                    }
                    if (!float.TryParse(parameterValues[13].text, out itemToAddFullDatabase.AmperagemDeSaida))
                    {
                        itemToAddFullDatabase.AmperagemDeSaida = 0;
                        itemToAddCategoryDatabase.AmperagemDeSaida = 0;
                    }
                    else
                    {
                        itemToAddCategoryDatabase.AmperagemDeSaida = itemToAddFullDatabase.AmperagemDeSaida;
                    }
                }
            //    InternalDatabase.Instance.splitDatabase[ConstStrings.Carregador].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.carregador.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Adaptador AC
            case ConstStrings.AdaptadorAC:
                itemToAddFullDatabase.Categoria = ConstStrings.AdaptadorAC;
                //InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        itemToAddFullDatabase.OndeFunciona = parameterValues[11].text;
                        itemToAddCategoryDatabase.OndeFunciona = parameterValues[11].text;
                        if (!float.TryParse(parameterValues[12].text, out itemToAddFullDatabase.VoltagemDeSaida))
                        {
                            itemToAddFullDatabase.VoltagemDeSaida = 0f;
                            itemToAddCategoryDatabase.VoltagemDeSaida = 0f;
                        }
                        else
                        {
                            itemToAddCategoryDatabase.VoltagemDeSaida = itemToAddFullDatabase.VoltagemDeSaida;
                        }
                        if (!float.TryParse(parameterValues[13].text, out itemToAddFullDatabase.AmperagemDeSaida))
                        {
                            itemToAddFullDatabase.AmperagemDeSaida = 0;
                            itemToAddCategoryDatabase.AmperagemDeSaida = 0;
                        }
                        else
                        {
                            itemToAddCategoryDatabase.AmperagemDeSaida = itemToAddFullDatabase.AmperagemDeSaida;
                        }
                        break;
                    case CurrentEstoque.Concert:
                        if (!float.TryParse(parameterValues[12].text, out itemToAddFullDatabase.VoltagemDeSaida))
                        {
                            itemToAddFullDatabase.VoltagemDeSaida = 0f;
                            itemToAddCategoryDatabase.VoltagemDeSaida = 0f;
                        }
                        else
                        {
                            itemToAddCategoryDatabase.VoltagemDeSaida = itemToAddFullDatabase.VoltagemDeSaida;
                        }
                        if (!float.TryParse(parameterValues[13].text, out itemToAddFullDatabase.AmperagemDeSaida))
                        {
                            itemToAddFullDatabase.AmperagemDeSaida = 0;
                            itemToAddCategoryDatabase.AmperagemDeSaida = 0;
                        }
                        else
                        {
                            itemToAddCategoryDatabase.AmperagemDeSaida = itemToAddFullDatabase.AmperagemDeSaida;
                        }
                        break;
                    default:
                        if (!float.TryParse(parameterValues[11].text, out itemToAddFullDatabase.VoltagemDeSaida))
                        {
                            itemToAddFullDatabase.VoltagemDeSaida = 0f;
                            itemToAddCategoryDatabase.VoltagemDeSaida = 0f;
                        }
                        else
                        {
                            itemToAddCategoryDatabase.VoltagemDeSaida = itemToAddFullDatabase.VoltagemDeSaida;
                        }
                        if (!float.TryParse(parameterValues[12].text, out itemToAddFullDatabase.AmperagemDeSaida))
                        {
                            itemToAddFullDatabase.AmperagemDeSaida = 0;
                            itemToAddCategoryDatabase.AmperagemDeSaida = 0;
                        }
                        else
                        {
                            itemToAddCategoryDatabase.AmperagemDeSaida = itemToAddFullDatabase.AmperagemDeSaida;
                        }
                        break;
                }              
               // InternalDatabase.Instance.splitDatabase[ConstStrings.AdaptadorAC].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.adaptadorAC.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Storage NAS
            case ConstStrings.StorageNAS:
                itemToAddFullDatabase.Categoria = ConstStrings.StorageNAS;
                InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                if(!float.TryParse(parameterValues[11].text, out itemToAddFullDatabase.Tamanho))
                {
                    itemToAddFullDatabase.Tamanho = 0f;
                }
                itemToAddFullDatabase.TipoDeRAID = parameterValues[12].text;
                itemToAddFullDatabase.TipoDeHD = parameterValues[13].text;
                itemToAddFullDatabase.CapacidadeMaxHD = parameterValues[14].text;
                if(!int.TryParse(parameterValues[15].text, out itemToAddFullDatabase.AteQuantosHDs))
                {
                    itemToAddFullDatabase.AteQuantosHDs = 0;
                }
                //        InternalDatabase.Instance.splitDatabase[ConstStrings.StorageNAS].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.storageNAS.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region GBIC
            case ConstStrings.Gbic:
                itemToAddFullDatabase.Categoria = ConstStrings.Gbic;
          //      InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                itemToAddFullDatabase.Desempenho = parameterValues[11].text;
                             //  InternalDatabase.Instance.splitDatabase[ConstStrings.Gbic].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.gbic.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Placa de Video
            case ConstStrings.PlacaDeVideo:
                itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeVideo;
               //InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                if(!int.TryParse(parameterValues[11].text, out itemToAddFullDatabase.QuantidadeDePortas))
                {
                    itemToAddFullDatabase.QuantidadeDePortas = 0;
                }
                itemToAddFullDatabase.QuaisConexoes = parameterValues[12].text;
                //InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeVideo].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.placaDeVideo.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Placa de som
            case ConstStrings.PlacaDeSom:
                itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeSom;
                //InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                if(!int.TryParse(parameterValues[11].text, out itemToAddFullDatabase.QuantosCanais))
                {
                    itemToAddFullDatabase.QuantosCanais = 0;
                }               
                //InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeSom].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.placaDeSom.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Placa de captura de video
            case ConstStrings.PlacaDeCapturaDeVideo:
                itemToAddFullDatabase.Categoria = ConstStrings.PlacaDeCapturaDeVideo;
              //  InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                if(!int.TryParse(parameterValues[11].text, out itemToAddFullDatabase.QuantidadeDePortas))
                {
                    itemToAddFullDatabase.QuantidadeDePortas = 0;
                }                
                //InternalDatabase.Instance.splitDatabase[ConstStrings.PlacaDeCapturaDeVideo].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.placaDeCapturaDeVideo.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Servidor
            case ConstStrings.Servidor:
                itemToAddFullDatabase.Categoria = ConstStrings.Servidor;
                //InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        itemToAddFullDatabase.ModeloPlacaMae = parameterValues[11].text;
                        itemToAddFullDatabase.Fonte = parameterValues[12].text;
                        itemToAddFullDatabase.Memoria = parameterValues[13].text;
                        itemToAddFullDatabase.HD = parameterValues[14].text;
                        itemToAddFullDatabase.PlacaDeVideo = parameterValues[15].text;
                        itemToAddFullDatabase.PlacaDeRede = parameterValues[16].text;
                        itemToAddFullDatabase.Processador = parameterValues[17].text;
                        itemToAddFullDatabase.MemoriasSuportadas = parameterValues[18].text;
                        if(!int.TryParse(parameterValues[19].text, out itemToAddFullDatabase.QuantasMemorias))
                        {
                            itemToAddFullDatabase.QuantasMemorias = 0;
                        }
                        itemToAddFullDatabase.OrdemDasMemorias = parameterValues[20].text;
                        if(!int.TryParse(parameterValues[21].text, out itemToAddFullDatabase.CapacidadeRAMTotal))
                        {
                            itemToAddFullDatabase.CapacidadeRAMTotal = 0;
                        }
                        itemToAddFullDatabase.Soquete = parameterValues[22].text;
                        itemToAddFullDatabase.PlacaControladora = parameterValues[23].text;
                        if(!int.TryParse(parameterValues[24].text, out itemToAddFullDatabase.AteQuantosHDs))
                        {
                            itemToAddFullDatabase.AteQuantosHDs = 0;
                        }
                        itemToAddFullDatabase.TipoDeHD = parameterValues[25].text;
                        itemToAddFullDatabase.TipoDeRAID = parameterValues[26].text;
                        break;
                   case CurrentEstoque.Concert:
                        itemToAddFullDatabase.HD = parameterValues[12].text;
                        itemToAddFullDatabase.Memoria = parameterValues[13].text;
                        itemToAddFullDatabase.Processador = parameterValues[14].text;
                        itemToAddFullDatabase.Windows = parameterValues[15].text;
                        break;
                    default:
                        itemToAddFullDatabase.HD = parameterValues[11].text;
                        itemToAddFullDatabase.Memoria = parameterValues[12].text;
                        itemToAddFullDatabase.Processador = parameterValues[13].text;
                        itemToAddFullDatabase.Windows = parameterValues[14].text;
                        break;
                }               
             //                InternalDatabase.Instance.splitDatabase[ConstStrings.Servidor].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.servidor.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Notebook
            case ConstStrings.Notebook:
                itemToAddFullDatabase.Categoria = ConstStrings.Notebook;
                //InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                switch (InternalDatabase.Instance.currentEstoque)
                {
                    case CurrentEstoque.SnPro:
                        itemToAddFullDatabase.HD = parameterValues[11].text;
                        itemToAddFullDatabase.Memoria = parameterValues[12].text;
                        itemToAddFullDatabase.EntradaRJ45 = parameterValues[13].text;
                        itemToAddFullDatabase.BateriaInclusa = parameterValues[14].text;
                        itemToAddFullDatabase.AdaptadorAC = parameterValues[15].text;
                        itemToAddFullDatabase.Windows = parameterValues[16].text;
                        break;
                    case CurrentEstoque.Concert:
                        itemToAddFullDatabase.HD = parameterValues[12].text;
                        itemToAddFullDatabase.Memoria = parameterValues[13].text;
                        itemToAddFullDatabase.Processador = parameterValues[14].text;
                        itemToAddFullDatabase.Windows = parameterValues[15].text;
                        break;
                    default:
                        itemToAddFullDatabase.HD = parameterValues[11].text;
                        itemToAddFullDatabase.Memoria = parameterValues[12].text;
                        itemToAddFullDatabase.Processador = parameterValues[13].text;
                        itemToAddFullDatabase.Windows = parameterValues[14].text;
                        break;
                }               
             //   InternalDatabase.Instance.splitDatabase[ConstStrings.Notebook].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.notebook.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Monitor
            case ConstStrings.Monitor:
                itemToAddFullDatabase.Categoria = ConstStrings.Monitor;
              //  InternalDatabase.Instance.splitDatabase[ConstStrings.Inventario].itens.Add(itemToAddFullDatabase);
                if(InternalDatabase.Instance.currentEstoque == CurrentEstoque.Concert)
                {
                    if (!float.TryParse(parameterValues[12].text, out itemToAddFullDatabase.Polegadas))
                    {
                        itemToAddFullDatabase.Polegadas = 0f;
                    }
                    itemToAddFullDatabase.QuaisConexoes = parameterValues[13].text;
                }
                else
                {
                    if (!float.TryParse(parameterValues[11].text, out itemToAddFullDatabase.Polegadas))
                    {
                        itemToAddFullDatabase.Polegadas = 0f;
                    }
                    itemToAddFullDatabase.QuaisConexoes = parameterValues[12].text;
                }
                
                
                InternalDatabase.Instance.splitDatabase[ConstStrings.Monitor].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.monitor.itens.Add(itemToAddFullDatabase);
                break;
            #endregion
            #region Outros
            case ConstStrings.Outros:
            default:
                itemToAddFullDatabase.Categoria = parameterValues[5].text;
                itemToAddCategoryDatabase.Categoria = parameterValues[5].text;
//                InternalDatabase.Instance.splitDatabase[ConstStrings.Outros].itens.Add(itemToAddCategoryDatabase);
                InternalDatabase.outros.itens.Add(itemToAddFullDatabase);
                break;
                #endregion
        }
        InternalDatabase.Instance.fullDatabase.itens.Add(itemToAddFullDatabase);
        // ShowMessage();
         EventHandler.CallDatabaseUpdatedEvent();
        if(InternalDatabase.Instance.isOfflineProgram)
        {
            EventHandler.CallIsOneMessageOnlyEvent(true);
            EventHandler.CallOpenMessageEvent("Added");
        }
    }

}

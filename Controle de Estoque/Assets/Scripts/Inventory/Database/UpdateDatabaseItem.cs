using Assets.Scripts.Misc;
using System.Collections.Generic;

namespace Assets.Scripts.Inventory.Database
{
    /// <summary>
    /// Updates a specific item on the fullDatabase (internal database)
    /// </summary>
    public class UpdateDatabaseItem
    {
        public static void UpdateItem(List<string> parameters, int itemIndexFullDatabase)
        {
            ConsultDatabase consultDatabase = new ConsultDatabase();
            int categoryItemToUpdateIndex = 0;

            ItemColumns itemToUpdate = new ItemColumns();
            #region Inventario
            switch (InternalDatabase.Instance.currentEstoque)
            {
                case CurrentEstoque.Concert:
                    itemToUpdate.Aquisicao = parameters[0];
                    itemToUpdate.Entrada = parameters[1];
                    if (!int.TryParse(parameters[2], out itemToUpdate.Patrimonio))
                    {
                        itemToUpdate.Patrimonio = -666;
                    }
                    itemToUpdate.Status = parameters[3];
                    itemToUpdate.Serial = parameters[4];
                    itemToUpdate.Categoria = parameters[5];
                    itemToUpdate.Fabricante = parameters[6];
                    itemToUpdate.Modelo = parameters[7];
                    itemToUpdate.Local = parameters[8];
                    itemToUpdate.Pessoa = parameters[9];
                    itemToUpdate.CentroDeCusto = parameters[10];
                    itemToUpdate.Saida = parameters[11];
                    break;
                default:
                    itemToUpdate.Aquisicao = parameters[0];
                    itemToUpdate.Entrada = parameters[1];
                    if (!int.TryParse(parameters[2], out itemToUpdate.Patrimonio))
                    {
                        itemToUpdate.Patrimonio = -666;
                    }
                    itemToUpdate.Status = parameters[3];
                    itemToUpdate.Serial = parameters[4];
                    itemToUpdate.Categoria = parameters[5];
                    itemToUpdate.Fabricante = parameters[6];
                    itemToUpdate.Modelo = parameters[7];
                    itemToUpdate.Local = parameters[8];
                    itemToUpdate.Saida = parameters[9];
                    itemToUpdate.Observacao = parameters[10];
                    break;
            }
            #endregion
            switch (itemToUpdate.Categoria)
            {
                #region Adaptador AC
                case ConstStrings.C_AdaptadorAC:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            itemToUpdate.OndeFunciona = parameters[11];
                            if (!float.TryParse(parameters[12], out itemToUpdate.VoltagemDeSaida))
                            {
                                itemToUpdate.VoltagemDeSaida = 0f;
                            }
                            if (!float.TryParse(parameters[13], out itemToUpdate.AmperagemDeSaida))
                            {
                                itemToUpdate.AmperagemDeSaida = 0f;
                            }
                            break;
                        case CurrentEstoque.Concert:
                            if (!float.TryParse(parameters[12], out itemToUpdate.VoltagemDeSaida))
                            {
                                itemToUpdate.VoltagemDeSaida = 0f;
                            }
                            if (!float.TryParse(parameters[13], out itemToUpdate.AmperagemDeSaida))
                            {
                                itemToUpdate.AmperagemDeSaida = 0f;
                            }
                            break;
                        default:
                            if (!float.TryParse(parameters[11], out itemToUpdate.VoltagemDeSaida))
                            {
                                itemToUpdate.VoltagemDeSaida = 0f;
                            }
                            if (!float.TryParse(parameters[12], out itemToUpdate.AmperagemDeSaida))
                            {
                                itemToUpdate.AmperagemDeSaida = 0f;
                            }
                            break;
                    }
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.adaptadorAC, itemToUpdate.Patrimonio);
                    InternalDatabase.adaptadorAC.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Carregador
                case ConstStrings.C_Carregador:
                    itemToUpdate.OndeFunciona = parameters[11];
                    if (!float.TryParse(parameters[12], out itemToUpdate.VoltagemDeSaida))
                    {
                        itemToUpdate.VoltagemDeSaida = 0f;
                    }
                    if (!float.TryParse(parameters[13], out itemToUpdate.AmperagemDeSaida))
                    {
                        itemToUpdate.AmperagemDeSaida = 0f;
                    }
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.carregador, itemToUpdate.Patrimonio);
                    InternalDatabase.carregador.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Desktop
                case ConstStrings.C_Desktop:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            itemToUpdate.ModeloPlacaMae = parameters[11];
                            itemToUpdate.Fonte = parameters[12];
                            itemToUpdate.Memoria = parameters[13];
                            itemToUpdate.HD = parameters[14];
                            itemToUpdate.PlacaDeVideo = parameters[15];
                            itemToUpdate.LeitorDeDVD = parameters[16];
                            break;
                        case CurrentEstoque.Concert:
                            itemToUpdate.HD = parameters[12];
                            itemToUpdate.Memoria = parameters[13];
                            itemToUpdate.Processador = parameters[14];
                            itemToUpdate.Windows = parameters[15];
                            break;
                        default:
                            itemToUpdate.HD = parameters[11];
                            itemToUpdate.Memoria = parameters[12];
                            itemToUpdate.Processador = parameters[13];
                            itemToUpdate.Windows = parameters[14];
                            break;
                    }
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.desktop, itemToUpdate.Patrimonio);
                    InternalDatabase.desktop.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Fone Ramal
                case ConstStrings.C_FoneRamal:
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.foneRamal, itemToUpdate.Patrimonio);
                    InternalDatabase.foneRamal.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Fonte
                case ConstStrings.C_Fonte:
                    if (!int.TryParse(parameters[11], out itemToUpdate.Watts))
                    {
                        itemToUpdate.Watts = 0;
                    }
                    itemToUpdate.OndeFunciona = parameters[12];
                    itemToUpdate.Conectores = parameters[13];
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.fonte, itemToUpdate.Patrimonio);
                    InternalDatabase.fonte.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region GBIC
                case ConstStrings.C_Gbic:
                    itemToUpdate.Desempenho = parameters[11];
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.gbic, itemToUpdate.Patrimonio);
                    InternalDatabase.gbic.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region HD
                case ConstStrings.C_HD:
                    itemToUpdate.Interface = parameters[11];
                    if (!float.TryParse(parameters[12], out itemToUpdate.Tamanho))
                    {
                        itemToUpdate.Tamanho = 0f;
                    }
                    itemToUpdate.FormaDeArmazenamento = parameters[13];
                    if (!int.TryParse(parameters[14], out itemToUpdate.CapacidadeEmGB))
                    {
                        itemToUpdate.CapacidadeEmGB = 0;
                    }
                    if (!int.TryParse(parameters[15], out itemToUpdate.RPM))
                    {
                        itemToUpdate.RPM = 0;
                    }
                    if (!float.TryParse(parameters[16], out itemToUpdate.VelocidadeDeLeitura))
                    {
                        itemToUpdate.VelocidadeDeLeitura = 0f;
                    }
                    itemToUpdate.Enterprise = parameters[17];
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.hd, itemToUpdate.Patrimonio);
                    InternalDatabase.hd.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region iDrac
                case ConstStrings.C_Idrac:
                    itemToUpdate.QuaisConexoes = parameters[11];
                    if (!float.TryParse(parameters[12], out itemToUpdate.VelocidadeGBs))
                    {
                        itemToUpdate.VelocidadeGBs = 0f;
                    }
                    itemToUpdate.EntradaSD = parameters[13];
                    itemToUpdate.ServidoresSuportados = parameters[14];
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.idrac, itemToUpdate.Patrimonio);
                    InternalDatabase.idrac.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Memoria
                case ConstStrings.C_Memoria:
                    itemToUpdate.Tipo = parameters[11];
                    if (!int.TryParse(parameters[12], out itemToUpdate.CapacidadeEmGB))
                    {
                        itemToUpdate.CapacidadeEmGB = 0;
                    }
                    if (!int.TryParse(parameters[13], out itemToUpdate.VelocidadeMHz))
                    {
                        itemToUpdate.VelocidadeMHz = 0;
                    }
                    itemToUpdate.LowVoltage = parameters[14];
                    itemToUpdate.Rank = parameters[15];
                    itemToUpdate.DIMM = parameters[16];
                    if (int.TryParse(parameters[17], out itemToUpdate.TaxaDeTransmissao))
                    {
                        itemToUpdate.TaxaDeTransmissao = 0;
                    }
                    itemToUpdate.Simbolo = parameters[18];
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.memoria, itemToUpdate.Patrimonio);
                    InternalDatabase.memoria.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Monitor
                case ConstStrings.C_Monitor:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.Concert:
                            if (!float.TryParse(parameters[12], out itemToUpdate.Polegadas))
                            {
                                itemToUpdate.Polegadas = 0;
                            }
                            itemToUpdate.QuaisConexoes = parameters[13];
                            break;
                        default:
                            if (!float.TryParse(parameters[11], out itemToUpdate.Polegadas))
                            {
                                itemToUpdate.Polegadas = 0;
                            }
                            itemToUpdate.QuaisConexoes = parameters[12];
                            break;
                    }
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.monitor, itemToUpdate.Patrimonio);
                    InternalDatabase.monitor.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Mouse
                case ConstStrings.C_Mouse:
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.mouse, itemToUpdate.Patrimonio);
                    InternalDatabase.mouse.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region NoBreak
                case ConstStrings.C_Nobreak:
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.nobreak, itemToUpdate.Patrimonio);
                    InternalDatabase.nobreak.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Notebook
                case ConstStrings.C_Notebook:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            itemToUpdate.HD = parameters[11];
                            itemToUpdate.Memoria = parameters[12];
                            itemToUpdate.EntradaRJ45 = parameters[13];
                            itemToUpdate.BateriaInclusa = parameters[14];
                            itemToUpdate.AdaptadorAC = parameters[15];
                            itemToUpdate.Windows = parameters[16];
                            break;
                        case CurrentEstoque.Concert:
                            itemToUpdate.HD = parameters[12];
                            itemToUpdate.Memoria = parameters[13];
                            itemToUpdate.Processador = parameters[14];
                            itemToUpdate.Windows = parameters[15];
                            break;
                        default:
                            itemToUpdate.HD = parameters[11];
                            itemToUpdate.Memoria = parameters[12];
                            itemToUpdate.Processador = parameters[13];
                            itemToUpdate.Windows = parameters[14];
                            break;
                    }
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.notebook, itemToUpdate.Patrimonio);
                    InternalDatabase.notebook.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Placa controladora
                case ConstStrings.C_PlacaControladora:
                    itemToUpdate.QuaisConexoes = parameters[11];
                    if (!int.TryParse(parameters[12], out itemToUpdate.QuantidadeDePortas))
                    {
                        itemToUpdate.QuantidadeDePortas = 0;
                    }
                    itemToUpdate.TipoDeRAID = parameters[13];
                    itemToUpdate.CapacidadeMaxHD = parameters[14];
                    if (!int.TryParse(parameters[15], out itemToUpdate.AteQuantosHDs))
                    {
                        itemToUpdate.AteQuantosHDs = 0;
                    }
                    itemToUpdate.BateriaInclusa = parameters[16];
                    itemToUpdate.Barramento = parameters[17];
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.placaControladora, itemToUpdate.Patrimonio);
                    InternalDatabase.placaControladora.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Placa de captura de vídeo
                case ConstStrings.C_PlacaDeCapturaDeVideo:
                    if (!int.TryParse(parameters[11], out itemToUpdate.QuantidadeDePortas))
                    {
                        itemToUpdate.QuantidadeDePortas = 0;
                    }
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.placaDeCapturaDeVideo, itemToUpdate.Patrimonio);
                    InternalDatabase.placaDeCapturaDeVideo.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Placa de rede
                case ConstStrings.C_PlacaDeRede:
                    itemToUpdate.Interface = parameters[11];
                    if (!int.TryParse(parameters[12], out itemToUpdate.QuantidadeDePortas))
                    {
                        itemToUpdate.QuantidadeDePortas = 0;
                    }
                    itemToUpdate.QuaisConexoes = parameters[13];
                    itemToUpdate.SuportaFibraOptica = parameters[14];
                    itemToUpdate.Desempenho = parameters[15];
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.placaDeRede, itemToUpdate.Patrimonio);
                    InternalDatabase.placaDeRede.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Placa de som
                case ConstStrings.C_PlacaDeSom:
                    if (!int.TryParse(parameters[11], out itemToUpdate.QuantosCanais))
                    {
                        itemToUpdate.QuantosCanais = 0;
                    }
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.placaDeSom, itemToUpdate.Patrimonio);
                    InternalDatabase.placaDeSom.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Placa de vídeo
                case ConstStrings.C_PlacaDeVideo:
                    if (!int.TryParse(parameters[11], out itemToUpdate.QuantidadeDePortas))
                    {
                        itemToUpdate.QuantidadeDePortas = 0;
                    }
                    itemToUpdate.QuaisConexoes = parameters[12];
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.placaDeVideo, itemToUpdate.Patrimonio);
                    InternalDatabase.placaDeVideo.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Processador
                case ConstStrings.C_Processador:
                    itemToUpdate.Soquete = parameters[11];
                    if (!int.TryParse(parameters[12], out itemToUpdate.NucleosFisicos))
                    {
                        itemToUpdate.NucleosFisicos = 0;
                    }
                    if (!int.TryParse(parameters[13], out itemToUpdate.NucleosLogicos))
                    {
                        itemToUpdate.NucleosLogicos = 0;
                    }
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.processador, itemToUpdate.Patrimonio);
                    InternalDatabase.processador.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Ramal
                case ConstStrings.C_Ramal:
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.ramal, itemToUpdate.Patrimonio);
                    InternalDatabase.ramal.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Roteador
                case ConstStrings.C_Roteador:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            itemToUpdate.Wireless = parameters[11];
                            if (!int.TryParse(parameters[12], out itemToUpdate.QuantidadeDePortas))
                            {
                                itemToUpdate.QuantidadeDePortas = 0;
                            }
                            if (!int.TryParse(parameters[13], out itemToUpdate.BandaMaxima))
                            {
                                itemToUpdate.BandaMaxima = 0;
                            }
                            break;
                        case CurrentEstoque.Concert:
                            itemToUpdate.Wireless = parameters[12];
                            if (!int.TryParse(parameters[13], out itemToUpdate.QuantidadeDePortas))
                            {
                                itemToUpdate.QuantidadeDePortas = 0;
                            }
                            break;
                        default:
                            itemToUpdate.Wireless = parameters[11];
                            if (!int.TryParse(parameters[12], out itemToUpdate.QuantidadeDePortas))
                            {
                                itemToUpdate.QuantidadeDePortas = 0;
                            }
                            break;
                    }
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.roteador, itemToUpdate.Patrimonio);
                    InternalDatabase.roteador.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Servidor
                case ConstStrings.C_Servidor:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            itemToUpdate.ModeloPlacaMae = parameters[11];
                            itemToUpdate.Fonte = parameters[12];
                            itemToUpdate.Memoria = parameters[13];
                            itemToUpdate.HD = parameters[14];
                            itemToUpdate.PlacaDeVideo = parameters[15];
                            itemToUpdate.PlacaDeRede = parameters[16];
                            itemToUpdate.Processador = parameters[17];
                            itemToUpdate.MemoriasSuportadas = parameters[18];
                            if (!int.TryParse(parameters[19], out itemToUpdate.QuantasMemorias))
                            {
                                itemToUpdate.QuantasMemorias = 0;
                            }
                            itemToUpdate.OrdemDasMemorias = parameters[20];
                            if (!int.TryParse(parameters[21], out itemToUpdate.CapacidadeRAMTotal))
                            {
                                itemToUpdate.CapacidadeRAMTotal = 0;
                            }
                            itemToUpdate.Soquete = parameters[22];
                            itemToUpdate.PlacaControladora = parameters[23];
                            if (!int.TryParse(parameters[24], out itemToUpdate.AteQuantosHDs))
                            {
                                itemToUpdate.AteQuantosHDs = 0;
                            }
                            itemToUpdate.TipoDeHD = parameters[25];
                            itemToUpdate.TipoDeRAID = parameters[26];
                            break;
                        case CurrentEstoque.Concert:
                            itemToUpdate.HD = parameters[12];
                            itemToUpdate.Memoria = parameters[13];
                            itemToUpdate.Processador = parameters[14];
                            itemToUpdate.Windows = parameters[15];
                            break;
                        default:
                            itemToUpdate.HD = parameters[11];
                            itemToUpdate.Memoria = parameters[12];
                            itemToUpdate.Processador = parameters[13];
                            itemToUpdate.Windows = parameters[14];
                            break;
                    }
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.servidor, itemToUpdate.Patrimonio);
                    InternalDatabase.servidor.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Storage NAS
                case ConstStrings.C_StorageNAS:
                    if (!float.TryParse(parameters[11], out itemToUpdate.Tamanho))
                    {
                        itemToUpdate.Tamanho = 0f;
                    }
                    itemToUpdate.TipoDeRAID = parameters[12];
                    itemToUpdate.TipoDeHD = parameters[13];
                    itemToUpdate.CapacidadeMaxHD = parameters[14];
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.storageNAS, itemToUpdate.Patrimonio);
                    InternalDatabase.storageNAS.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Switch
                case ConstStrings.C_Switch:
                    switch (InternalDatabase.Instance.currentEstoque)
                    {
                        case CurrentEstoque.SnPro:
                            itemToUpdate.QuaisConexoes = parameters[11];
                            itemToUpdate.Desempenho = parameters[12];
                            break;
                        case CurrentEstoque.Concert:
                            itemToUpdate.QuaisConexoes = parameters[12];
                            break;
                        default:
                            itemToUpdate.QuaisConexoes = parameters[11];
                            break;
                    }
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.Switch, itemToUpdate.Patrimonio);
                    InternalDatabase.Switch.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Teclado
                case ConstStrings.C_Teclado:
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.teclado, itemToUpdate.Patrimonio);
                    InternalDatabase.teclado.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                #endregion
                #region Outras categorias
                default:
                    categoryItemToUpdateIndex = consultDatabase.GetCategoryItemIndex(InternalDatabase.outros, itemToUpdate.Patrimonio);
                    InternalDatabase.outros.itens[categoryItemToUpdateIndex] = itemToUpdate;
                    break;
                    #endregion
            }
            InternalDatabase.Instance.fullDatabase.itens[itemIndexFullDatabase] = itemToUpdate;
        }

    }
}
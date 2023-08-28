using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Assets.Scripts.Inventory.Database
{
    public class HandleSheetsForSaveAndLoad
    {
        /// <summary>
        /// Get all the informations from every item in a specific Database (sheet) to be used by the saving system
        /// </summary>
        public static JArray GetJObject(Sheet sheetToConvert)
        {
            JArray state = new JArray();
            IList<JToken> stateList = state;
            foreach (var item in sheetToConvert.itens)
            {
                JObject jObjectToReturn = new JObject();
                IDictionary<string, JToken> stateDict = jObjectToReturn;
                stateDict["Aquisicao"] = item.Aquisicao;
                stateDict["Entrada"] = item.Entrada;
                stateDict["Patrimonio"] = item.Patrimonio;
                stateDict["Status"] = item.Status;
                stateDict["Serial"] = item.Serial;
                stateDict["Categoria"] = item.Categoria;
                stateDict["Fabricante"] = item.Fabricante;
                stateDict["Modelo"] = item.Modelo;
                stateDict["Local"] = item.Local;
                stateDict["Saida"] = item.Saida;
                stateDict["Observacao"] = item.Observacao;
                stateDict["Interface"] = item.Interface;
                stateDict["Tamanho"] = item.Tamanho;
                stateDict["FormaDeArmazenamento"] = item.FormaDeArmazenamento;
                stateDict["CapacidadeEmGB"] = item.CapacidadeEmGB;
                stateDict["RPM"] = item.RPM;
                stateDict["VelocidadeDeLeitura"] = item.VelocidadeDeLeitura;
                stateDict["Enterprise"] = item.Enterprise;
                stateDict["Tipo"] = item.Tipo;
                stateDict["VelocidadeMHz"] = item.VelocidadeMHz;
                stateDict["LowVoltage"] = item.LowVoltage;
                stateDict["Rank"] = item.Rank;
                stateDict["DIMM"] = item.DIMM;
                stateDict["TaxaDeTransmissao"] = item.TaxaDeTransmissao;
                stateDict["Simbolo"] = item.Simbolo;
                stateDict["QuantidadeDePortas"] = item.QuantidadeDePortas;
                stateDict["QuaisConexoes"] = item.QuaisConexoes;
                stateDict["SuportaFibraOptica"] = item.SuportaFibraOptica;
                stateDict["Desempenho"] = item.Desempenho;
                stateDict["VelocidadeGBs"] = item.VelocidadeGBs;
                stateDict["EntradaSD"] = item.EntradaSD;
                stateDict["ServidoresSuportados"] = item.ServidoresSuportados;
                stateDict["TipoDeHD"] = item.TipoDeHD;
                stateDict["TipoDeRAID"] = item.TipoDeRAID;
                stateDict["CapacidadeMaxHD"] = item.CapacidadeMaxHD;
                stateDict["AteQuantosHDs"] = item.AteQuantosHDs;
                stateDict["BateriaInclusa"] = item.BateriaInclusa;
                stateDict["Barramento"] = item.Barramento;
                stateDict["Soquete"] = item.Soquete;
                stateDict["NucleosFisicos"] = item.NucleosFisicos;
                stateDict["NucleosLogicos"] = item.NucleosLogicos;
                stateDict["ModeloPlacaMae"] = item.ModeloPlacaMae;
                stateDict["Fonte"] = item.Fonte;
                stateDict["Memoria"] = item.Memoria;
                stateDict["HD"] = item.HD;
                stateDict["PlacaDeVideo"] = item.PlacaDeVideo;
                stateDict["PlacaDeRede"] = item.PlacaDeRede;
                stateDict["LeitorDeDVD"] = item.LeitorDeDVD;
                stateDict["Watts"] = item.Watts;
                stateDict["OndeFunciona"] = item.OndeFunciona;
                stateDict["Conectores"] = item.Conectores;
                stateDict["Wireless"] = item.Wireless;
                stateDict["BandaMaxima"] = item.BandaMaxima;
                stateDict["VoltagemDeSaida"] = item.VoltagemDeSaida;
                stateDict["AmperagemDeSaida"] = item.AmperagemDeSaida;
                stateDict["QuantosCanais"] = item.QuantosCanais;
                stateDict["Polegadas"] = item.Polegadas;
                stateDict["Processador"] = item.Processador;
                stateDict["MemoriasSuportadas"] = item.MemoriasSuportadas;
                stateDict["QuantasMemorias"] = item.QuantasMemorias;
                stateDict["OrdemDasMemorias"] = item.OrdemDasMemorias;
                stateDict["CapacidadeRAMTotal"] = item.CapacidadeRAMTotal;
                stateDict["PlacaControladora"] = item.PlacaControladora;
                stateDict["EntradaRJ45"] = item.EntradaRJ45;
                stateDict["AdaptadorAC"] = item.AdaptadorAC;
                stateDict["Windows"] = item.Windows;
                stateDict["CapacidadeRAMTotal"] = item.CapacidadeRAMTotal;
                stateDict["Pessoa"] = item.Pessoa;
                stateDict["CentroDeCusto"] = item.CentroDeCusto;
                stateList.Add(jObjectToReturn);
            }
            return state;
        }

        /// <summary>
        /// Load all items from a specific Database (sheet)
        /// </summary>
        public static void LoadJObject(JToken state, out Sheet sheetToLoad)
        {
            sheetToLoad = new Sheet();
            if (state is JArray stateArray)
            {
                IList<JToken> stateList = stateArray;
                foreach (var item in stateList)
                {
                    if (item is JObject itemState)
                    {
                        ItemColumns itemToLoad = new ItemColumns();
                        IDictionary<string, JToken> itemStateDict = itemState;
                        itemToLoad.Aquisicao = itemStateDict["Aquisicao"].ToObject<string>();
                        itemToLoad.Entrada = itemStateDict["Entrada"].ToObject<string>();
                        itemToLoad.Patrimonio = itemStateDict["Patrimonio"].ToObject<int>();
                        itemToLoad.Status = itemStateDict["Status"].ToObject<string>();
                        itemToLoad.Serial = itemStateDict["Serial"].ToObject<string>();
                        itemToLoad.Categoria = itemStateDict["Categoria"].ToObject<string>();
                        itemToLoad.Fabricante = itemStateDict["Fabricante"].ToObject<string>();
                        itemToLoad.Modelo = itemStateDict["Modelo"].ToObject<string>();
                        itemToLoad.Local = itemStateDict["Local"].ToObject<string>();
                        itemToLoad.Saida = itemStateDict["Saida"].ToObject<string>();
                        itemToLoad.Observacao = itemStateDict["Observacao"].ToObject<string>();
                        itemToLoad.Interface = itemStateDict["Interface"].ToObject<string>();
                        itemToLoad.Tamanho = itemStateDict["Tamanho"].ToObject<float>();
                        itemToLoad.FormaDeArmazenamento = itemStateDict["FormaDeArmazenamento"].ToObject<string>();
                        itemToLoad.CapacidadeEmGB = itemStateDict["CapacidadeEmGB"].ToObject<int>();
                        itemToLoad.RPM = itemStateDict["RPM"].ToObject<int>();
                        itemToLoad.VelocidadeDeLeitura = itemStateDict["VelocidadeDeLeitura"].ToObject<int>();
                        itemToLoad.Enterprise = itemStateDict["Enterprise"].ToObject<string>();
                        itemToLoad.Tipo = itemStateDict["Tipo"].ToObject<string>();
                        itemToLoad.VelocidadeMHz = itemStateDict["VelocidadeMHz"].ToObject<int>();
                        itemToLoad.LowVoltage = itemStateDict["LowVoltage"].ToObject<string>();
                        itemToLoad.Rank = itemStateDict["Rank"].ToObject<string>();
                        itemToLoad.DIMM = itemStateDict["DIMM"].ToObject<string>();
                        itemToLoad.TaxaDeTransmissao = itemStateDict["TaxaDeTransmissao"].ToObject<int>();
                        itemToLoad.Simbolo = itemStateDict["Simbolo"].ToObject<string>();
                        itemToLoad.QuantidadeDePortas = itemStateDict["QuantidadeDePortas"].ToObject<int>();
                        itemToLoad.QuaisConexoes = itemStateDict["QuaisConexoes"].ToObject<string>();
                        itemToLoad.SuportaFibraOptica = itemStateDict["SuportaFibraOptica"].ToObject<string>();
                        itemToLoad.Desempenho = itemStateDict["Desempenho"].ToObject<string>();
                        itemToLoad.VelocidadeGBs = itemStateDict["VelocidadeGBs"].ToObject<int>();
                        itemToLoad.EntradaSD = itemStateDict["EntradaSD"].ToObject<string>();
                        itemToLoad.ServidoresSuportados = itemStateDict["ServidoresSuportados"].ToObject<string>();
                        itemToLoad.TipoDeHD = itemStateDict["TipoDeHD"].ToObject<string>();
                        itemToLoad.TipoDeRAID = itemStateDict["TipoDeRAID"].ToObject<string>();
                        itemToLoad.CapacidadeMaxHD = itemStateDict["CapacidadeMaxHD"].ToObject<string>();
                        itemToLoad.AteQuantosHDs = itemStateDict["AteQuantosHDs"].ToObject<int>();
                        itemToLoad.BateriaInclusa = itemStateDict["BateriaInclusa"].ToObject<string>();
                        itemToLoad.Barramento = itemStateDict["Barramento"].ToObject<string>();
                        itemToLoad.Soquete = itemStateDict["Soquete"].ToObject<string>();
                        itemToLoad.NucleosFisicos = itemStateDict["NucleosFisicos"].ToObject<int>();
                        itemToLoad.NucleosLogicos = itemStateDict["NucleosLogicos"].ToObject<int>();
                        itemToLoad.ModeloPlacaMae = itemStateDict["ModeloPlacaMae"].ToObject<string>();
                        itemToLoad.Fonte = itemStateDict["Fonte"].ToObject<string>();
                        itemToLoad.Memoria = itemStateDict["Memoria"].ToObject<string>();
                        itemToLoad.HD = itemStateDict["HD"].ToObject<string>();
                        itemToLoad.PlacaDeVideo = itemStateDict["PlacaDeVideo"].ToObject<string>();
                        itemToLoad.PlacaDeRede = itemStateDict["PlacaDeRede"].ToObject<string>();
                        itemToLoad.LeitorDeDVD = itemStateDict["LeitorDeDVD"].ToObject<string>();
                        itemToLoad.Watts = itemStateDict["Watts"].ToObject<int>();
                        itemToLoad.OndeFunciona = itemStateDict["OndeFunciona"].ToObject<string>();
                        itemToLoad.Conectores = itemStateDict["Conectores"].ToObject<string>();
                        itemToLoad.Wireless = itemStateDict["Wireless"].ToObject<string>();
                        itemToLoad.BandaMaxima = itemStateDict["BandaMaxima"].ToObject<int>();
                        itemToLoad.VoltagemDeSaida = itemStateDict["VoltagemDeSaida"].ToObject<float>();
                        itemToLoad.AmperagemDeSaida = itemStateDict["AmperagemDeSaida"].ToObject<float>();
                        itemToLoad.QuantosCanais = itemStateDict["QuantosCanais"].ToObject<int>();
                        itemToLoad.Polegadas = itemStateDict["Polegadas"].ToObject<float>();
                        itemToLoad.Processador = itemStateDict["Processador"].ToObject<string>();
                        itemToLoad.MemoriasSuportadas = itemStateDict["MemoriasSuportadas"].ToObject<string>();
                        itemToLoad.QuantasMemorias = itemStateDict["QuantasMemorias"].ToObject<int>();
                        itemToLoad.OrdemDasMemorias = itemStateDict["OrdemDasMemorias"].ToObject<string>();
                        itemToLoad.CapacidadeRAMTotal = itemStateDict["CapacidadeRAMTotal"].ToObject<int>();
                        itemToLoad.PlacaControladora = itemStateDict["PlacaControladora"].ToObject<string>();
                        itemToLoad.EntradaRJ45 = itemStateDict["EntradaRJ45"].ToObject<string>();
                        itemToLoad.AdaptadorAC = itemStateDict["AdaptadorAC"].ToObject<string>();
                        itemToLoad.Windows = itemStateDict["Windows"].ToObject<string>();
                        itemToLoad.CapacidadeRAMTotal = itemStateDict["CapacidadeRAMTotal"].ToObject<int>();
                        itemToLoad.CentroDeCusto = itemStateDict["CentroDeCusto"].ToObject<string>();
                        sheetToLoad.itens.Add(itemToLoad);
                    }
                }
            }
        }
    }
}
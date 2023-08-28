using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [System.Serializable]
    public class ItemColumns
    {
        /// <summary>
        /// Get a specific value based on it's name
        /// </summary>
        public string GetValue(string valueToGet)
        {
            string[] allValues = { Aquisicao, Entrada, Patrimonio.ToString(), Status, Serial, Categoria, Fabricante, Modelo, Local,
        Saida, Observacao, Interface, Tamanho.ToString("0.0"), FormaDeArmazenamento, CapacidadeEmGB.ToString(), RPM.ToString(),
        VelocidadeDeLeitura.ToString("0.0"), Enterprise, EstoqueAtual.ToString(), Tipo, VelocidadeMHz.ToString(), LowVoltage, Rank, DIMM,
        TaxaDeTransmissao.ToString(), Simbolo, QuantidadeDePortas.ToString(), QuaisConexoes, SuportaFibraOptica, Desempenho,
        VelocidadeGBs.ToString("0.0"), EntradaSD, ServidoresSuportados, TipoDeRAID, TipoDeHD, CapacidadeMaxHD, AteQuantosHDs.ToString(),
        BateriaInclusa, Barramento, Soquete, NucleosFisicos.ToString(), NucleosLogicos.ToString(), ModeloPlacaMae, Fonte, Memoria, HD,
        PlacaDeVideo, PlacaDeRede, LeitorDeDVD, Watts.ToString(), OndeFunciona, Conectores, Wireless, BandaMaxima.ToString(),
        VoltagemDeSaida.ToString("0.0"), AmperagemDeSaida.ToString("0.0"), QuantosCanais.ToString(), Polegadas.ToString("0.0"),
        Processador, CentroDeCusto, Pessoa};
            for (int i = 0; i < allValues.Length; i++)
            {
                if (string.Compare(allValues[i], valueToGet, true) == 0)
                {
                    return allValues[i];
                }
            }
            return null;
        }

        #region All possible values an item can have
        //public string Itens; // sem patrimônio e serial
        //public int Quantidade; // todos
        public string Aquisicao; // todos
        public string Entrada; // todos
        public int Patrimonio;
        public string Status; // todos
        public string Serial;
        public string Categoria;
        public string Fabricante; // todos
        public string Modelo; // todos
        public string Local; // todos
        public string Saida; // todos
        public string Observacao; // Inventário
        public string Interface; // HD, placa de rede
        public float Tamanho; // HD, Storage NAS
        public string FormaDeArmazenamento; // HD
        public int CapacidadeEmGB; // HD, memória
        public int RPM; //HD
        public float VelocidadeDeLeitura; //HD
        public string Enterprise; //HD
        public int EstoqueAtual; //todos
        public string Tipo; // memória
        public int VelocidadeMHz; // memória
        public string LowVoltage; // memória
        public string Rank; // memória
        public string DIMM; // memória
        public int TaxaDeTransmissao; // memória
        public string Simbolo; // memória
        public int QuantidadeDePortas; // Placa de rede, Placa controladora, switch, Roteador, Placa de Vídeo, Placa de captura de vídeo
        public string QuaisConexoes; // Placa de rede, Placa de Vídeo, iDrac, Placa controladora
        public string SuportaFibraOptica; // placa de rede
        public string Desempenho; // placa de rede, Switch, GBIC
        public float VelocidadeGBs; // iDrac
        public string EntradaSD; // iDrac
        public string ServidoresSuportados; // iDrac
        public string TipoDeRAID; // placa controladora, Storage NAS
        public string TipoDeHD; // placa controladora, Storage NAS
        public string CapacidadeMaxHD; // placa controladora, Storage NAS
        public int AteQuantosHDs; // placa controladora, Servidores, Storage NAS
        public string BateriaInclusa; // placa controladora
        public string Barramento; // placa controladora
        public string Soquete; // processador
        public int NucleosFisicos; // processador
        public int NucleosLogicos; // processador
        public string ModeloPlacaMae; // Desktop, Notebook
        public string Fonte; // Desktop, Notebook, Servidor
        public string Memoria; // Desktop, Notebook, Servidor
        public string HD; // Desktop, Notebook, Servidor
        public string PlacaDeVideo; // Desktop, Servidor
        public string PlacaDeRede; // Desktop, Servidor
        public string LeitorDeDVD; // Desktop, Servidor, Notebook
        public int Watts; // Fonte
        public string OndeFunciona; // Fonte, Carregador
        public string Conectores; // Fonte
        public string Wireless; // Roteador
        public int BandaMaxima; // Roteador
        public float VoltagemDeSaida; // Carregador, Adaptador AC
        public float AmperagemDeSaida; // Carregador, Adaptador AC
        public int QuantosCanais; // Placa de Som
        public float Polegadas; // Monitor
        public string Processador; // Desktop, Servidores
        public string MemoriasSuportadas; // Servidores
        public int QuantasMemorias; // Servidores
        public string OrdemDasMemorias; // Servidores
        public int CapacidadeRAMTotal; // Servidores
        public string PlacaControladora; // Servidores
        public string EntradaRJ45;// notebook
        public string AdaptadorAC; // notebook
        public string Windows; // notebook
        public string Pessoa; // Concert
        public string CentroDeCusto; // Concert
        #endregion
    }
}
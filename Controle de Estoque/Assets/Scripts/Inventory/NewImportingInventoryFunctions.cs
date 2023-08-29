using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Inventory.PatrimonioItem;
using Assets.Scripts.Misc;
using SimpleJSON;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Inventory
{
    public class NewImportingInventoryFunctions : IImportingInventoryFunctions
    {
        private readonly IInternalDatabase _internalDatabase;

        public NewImportingInventoryFunctions(IInternalDatabase internalDatabase)
        {
            _internalDatabase = internalDatabase;
        }

        public void ImportAdaptadorAC(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;

        }

        public void ImportCarregador(JSONNode inventario, out Sheet importSheet)
        {
            throw new NotImplementedException();
        }

        public void ImportDesktop(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Patrimonio_I, item[0] },
                    { ConstStrings.ModeloPlacaMae, item[1] },
                    { ConstStrings.Fonte, item[2] },
                    { ConstStrings.Memoria, item[3] },
                    { ConstStrings.Hd, item[4] },
                    { ConstStrings.PlacaDeVideo, item[5] },
                    { ConstStrings.PlacaDeRede, item[6] },
                    { ConstStrings.LeitorDeDvd, item[7] },
                    { ConstStrings.Processador, item[8] }
                };
                var desktop = new Desktop();
                desktop.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(desktop);
            }
        }

        public void ImportFonte(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },
                    { ConstStrings.Watts_I, item[1] },
                    { ConstStrings.OndeFunciona, item[2] },
                    { ConstStrings.Conectores, item[3] }
                };
                var fonte = new Fonte();
                fonte.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(fonte);
            }
        }

        public void ImportFonteDeAlimentacao(JSONNode inventario)
        {
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },
                    { ConstStrings.OndeFunciona, item[1] },
                    { ConstStrings.Voltagem_D, item[2] },
                    { ConstStrings.Amperagem_D, item[3] }
                };
                var fonteDeAlimentacao = new FonteDeAlimentacao();
                fonteDeAlimentacao.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(fonteDeAlimentacao);
            }
        }

        public void ImportGBIC(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                   { ConstStrings.Modelo, item[0] },
                   { ConstStrings.DesempenhoMax_F, item[2] }
                };
                var gbic = new GBIC();
                gbic.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(gbic);
            }
        }

        public void ImportHD(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },
                    { ConstStrings.Interface, item[2] },
                    { ConstStrings.Tamanho_D, item[3] },
                    { ConstStrings.FormaDeArmazenamento, item[4] },
                    { ConstStrings.Capacidade_I, item[5] },
                    { ConstStrings.RPM_I, item[6] },
                    { ConstStrings.VelocidadeDeLeitura_I, item[7] },
                    { ConstStrings.Enterprise, item[8] }
                };
                var hd = new HD();
                hd.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(hd);
            }
        }

        public void ImportIdrac(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },
                    { ConstStrings.Porta, item[2] },
                    { ConstStrings.Velocidade_I, item[3] },
                    { ConstStrings.EntradaSD, item[4] },
                    { ConstStrings.ServidoresSuportados, item[5] },                    
                };
                var idrac = new Idrac();
                idrac.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(idrac);
            }
        }

        public void ImportInventory(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Aquisicao, item[0] },
                    { ConstStrings.Entrada, item[1] },
                    { ConstStrings.Patrimonio_I, item[2] },
                    { ConstStrings.Status, item[3] },
                    { ConstStrings.Serial, item[4] },
                    { ConstStrings.Categoria, item[5] },
                    { ConstStrings.Fabricante, item[6] },
                    { ConstStrings.Modelo, item[7] },
                    { ConstStrings.Local, item[8] },
                    { ConstStrings.Saida, item[9] },
                    { ConstStrings.Observacao, item[10] },
                };
                var inventoryItem = new PatrimonioItemParent();
                inventoryItem.SetParameters(importedData);
                _internalDatabase.AddPatrimonioItem(inventoryItem);
            }
        }

        public void ImportMemoria(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },
                    { ConstStrings.Tipo, item[2] },
                    { ConstStrings.Capacidade_I, item[3] },
                    { ConstStrings.Velocidade_I, item[4] },
                    { ConstStrings.LowVoltage, item[5] },
                    { ConstStrings.Rank, item[6] },
                    { ConstStrings.DIMM, item[7] },
                    { ConstStrings.TaxaDeTransmissao_I, item[8] },
                    { ConstStrings.Simbolo, item[9] },
                };
                var hd = new HD();
                hd.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(hd);
            }
        }

        public void ImportMonitor(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },
                    { ConstStrings.Polegadas, item[2] },
                    { ConstStrings.QuaisEntradas, item[3] }                    
                };
                var monitor = new Monitor();
                monitor.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(monitor);
            }
        }

        public void ImportNotebook(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Patrimonio_I, item[0] },
                    { ConstStrings.Hd, item[3] },
                    { ConstStrings.Memoria, item[4] },
                    { ConstStrings.Processador, item[5] },
                    { ConstStrings.EntradaRJ45, item[6] },
                    { ConstStrings.Bateria, item[7] },
                    { ConstStrings.FonteDeAlimentacao, item[8] },
                    { ConstStrings.QualWindows, item[9] },                    
                };
                var notebook = new Notebook_SNPro();
                notebook.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(notebook);
            }
        }

        public void ImportPlacaControladora(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },
                    { ConstStrings.TipoDeConexao, item[1] },
                    { ConstStrings.QuantasPortas_I, item[2] },
                    { ConstStrings.TiposDeRaid, item[3] },
                    { ConstStrings.CapacidadeMaxHD_I, item[4] },
                    { ConstStrings.AteQuantosHds_I, item[5] },
                    { ConstStrings.BateriaInclusa, item[6] },                    
                };
                var placaControladora = new PlacaControladora();
                placaControladora.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(placaControladora);
            }
        }

        public void ImportPlacaDeCapturaDeVideo(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },
                    { ConstStrings.QuantasEntradas_I, item[1] }                    
                };
                var placaDeCapturaDeVideo = new PlacaDeCapturaDeVideo();
                placaDeCapturaDeVideo.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(placaDeCapturaDeVideo);
            }
        }

        public void ImportPlacaDeRede(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },                    
                    { ConstStrings.Interface, item[2] },
                    { ConstStrings.QuantasPortas_I, item[3] },
                    { ConstStrings.QuaisPortas, item[4] },
                    { ConstStrings.SuportaFibra, item[5] },
                    { ConstStrings.DesempenhoMax_F, item[6] },                    
                };
                var placaDeRede = new PlacaDeRede();
                placaDeRede.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(placaDeRede);
            }
        }

        public void ImportPlacaDeSom(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },                    
                    { ConstStrings.QuantosCanais_I, item[1] },                    
                };
                var placaDeSom = new PlacaDeSom();
                placaDeSom.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(placaDeSom);
            }
        }

        public void ImportPlacaDeVideo(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },
                    { ConstStrings.QuantasEntradas_I, item[1] },
                    { ConstStrings.QuaisEntradas, item[2] }
                };
                var placaDeVideo = new PlacaDeVideo();
                placaDeVideo.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(placaDeVideo);
            }
        }

        public void ImportPlacaSAS(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },
                    { ConstStrings.QuantasEntradas_I, item[1] },
                    { ConstStrings.Velocidade_I, item[2] }
                };
                var placaSas = new PlacaSas();
                placaSas.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(placaSas);
            }
        }

        public void ImportProcessador(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },
                    { ConstStrings.Soquete, item[1] },
                    { ConstStrings.NucleosFisicos_I, item[2] },
                    { ConstStrings.NucleosLogicos_I, item[3] }
                };
                var processador = new Processador();
                processador.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(processador);
            }
        }

        public void ImportRoteador(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },
                    { ConstStrings.Wireless, item[1] },
                    { ConstStrings.QuantasEntradas_I, item[2] },
                    { ConstStrings.BandaMax_I, item[3] }
                };
                var roteador = new Roteador();
                roteador.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(roteador);
            }
        }

        public void ImportServidor(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Patrimonio_I, item[0] },
                    { ConstStrings.ModeloPlacaMae, item[3] },
                    { ConstStrings.Fonte, item[4] },
                    { ConstStrings.Memoria, item[5] },
                    { ConstStrings.Hd, item[6] },
                    { ConstStrings.QualSistemaOperacional, item[7] },
                    { ConstStrings.PlacaDeVideo, item[8] },
                    { ConstStrings.PlacaDeRede, item[9] },
                    { ConstStrings.Processador, item[10] },
                    { ConstStrings.MemoriasSuportadas, item[11] },
                    { ConstStrings.AteQuantasMemorias_I, item[12] },
                    { ConstStrings.OrdemDasMemorias, item[13] },
                    { ConstStrings.CapacidadeRamTotal_I, item[14] },
                    { ConstStrings.Soquete, item[15] },
                    { ConstStrings.PlacaControladora, item[16] },
                    { ConstStrings.AteQuantosHds_I, item[17] },
                    { ConstStrings.TiposDeHd, item[18] },
                    { ConstStrings.TiposDeRaid, item[19] }                    
                };
                var servidor = new Servidor();
                servidor.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(servidor);
            }
        }

        public void ImportStorageNas(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },
                    { ConstStrings.TamanhoDosHds, item[1] },
                    { ConstStrings.TiposDeRaid, item[2] },
                    { ConstStrings.TiposDeHd, item[3] },
                    { ConstStrings.CapacidadeMaxHD_I, item[4] },
                    { ConstStrings.AteQuantosHds_I, item[5] }
                };
                var storageNas = new StorageNas();
                storageNas.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(storageNas);
            }
        }

        public void ImportSwitch(JSONNode inventario, out Sheet importSheet)
        {
            importSheet = null;
            foreach (JSONNode item in inventario)
            {
                Dictionary<string, string> importedData = new Dictionary<string, string>
                {
                    { ConstStrings.Modelo, item[0] },
                    { ConstStrings.QuantasEQuaisPortas, item[1] },
                    { ConstStrings.CapacidadeMaxCadaPorta, item[2] },                    
                };
                var @switch = new Switch();
                @switch.SetParameters(importedData);
                _internalDatabase.AddTemporaryPatrimonioItem(@switch);
            }
        }
    }
}

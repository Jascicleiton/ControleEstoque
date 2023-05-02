using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsultCategoryHelperMethods
{
    public static string GetItemValue(ItemColumns item, int inputindex)
    {
        switch (item.Categoria)
        {
            case ConstStrings.AdaptadorAC:
            case ConstStrings.Carregador:
                return GetFonteDeAlimentacao(item, inputindex);                

            case ConstStrings.Desktop:
                return GetDesktop(item, inputindex);

            case ConstStrings.Fonte:
                return GetFonte(item, inputindex);

            case ConstStrings.Gbic:
                return GetGbic(item, inputindex);

            case ConstStrings.HD:
                return GetHD(item, inputindex);

            case ConstStrings.Idrac:
                return GetIdrac(item, inputindex);

            case ConstStrings.Memoria:
                return GetMemoria(item, inputindex);

            case ConstStrings.Monitor:
                return GetMonitor(item, inputindex);

            case ConstStrings.Mouse:
                return GetMouse(item, inputindex);

            case ConstStrings.Nobreak:
                return GetNoBreak(item, inputindex);

            case ConstStrings.Notebook:
                return GetNotebook(item, inputindex);

            case ConstStrings.PlacaControladora:
                return GetPlacaControladora(item, inputindex);

            case ConstStrings.PlacaDeCapturaDeVideo:
                return GetPlacaDeCapturaDeVideo(item, inputindex);

            case ConstStrings.PlacaDeRede:
                return GetPlacaDeRede(item, inputindex);

            case ConstStrings.PlacaDeSom:
                return GetPlacaDeSom(item, inputindex);

            case ConstStrings.PlacaDeVideo:
                return GetPlacaDeVideo(item, inputindex);

            case ConstStrings.Processador:
                return GetProcessador(item, inputindex);

            case ConstStrings.Roteador:
                return GetRoteador(item, inputindex);

            case ConstStrings.Servidor:
                return GetServidor(item, inputindex);

            case ConstStrings.StorageNAS:
                return GetStorageNas(item, inputindex);

            case ConstStrings.Switch:
                return GetSwitch(item, inputindex);

            case ConstStrings.Teclado:
                return GetTeclado(item, inputindex);

            case ConstStrings.USB:
                return GetUSB(item, inputindex);

            default:
                return GetOutros(item, inputindex);
                
        }
    }

    private static string GetFonteDeAlimentacao(ItemColumns item, int index)
    {
        if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.SnPro)
        {
            switch (index)
            {
                case 0:
                    return item.Status;
                case 1:
                    return item.Modelo;
                case 2:
                    return item.Local;
                case 3:
                    return item.OndeFunciona;
                case 4:
                    return item.VoltagemDeSaida.ToString();
                case 5:
                    return item.AmperagemDeSaida.ToString();
                default:
                    return null;
            }
        }
        else
        {
            switch (index)
            {
                case 0:
                    return item.Status;
                case 1:
                    return item.Modelo;
                case 2:
                    return item.Local;
                case 3:
                    return item.VoltagemDeSaida.ToString();
                case 4:
                    return item.AmperagemDeSaida.ToString();
                default:
                    return null;
            }
        }
    }

    private static string GetDesktop(ItemColumns item, int index)
    {
        if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.SnPro)
        {
            switch (index)
            {
                case 0:
                    return item.Status;
                case 1:
                    return item.Modelo;
                case 2:
                    return item.Local;
                case 3:
                    return item.ModeloPlacaMae;
                case 4:
                    return item.Fonte;
                case 5:
                    return item.Memoria;
                case 6:
                    return item.HD;
                case 7:
                    return item.PlacaDeVideo;
                case 8:
                    return item.LeitorDeDVD;
                case 9:
                    return item.Processador;
                default:
                    return null;
            }
        }
        else
        {
            switch (index)
            {
                case 0:
                    return item.Status;
                case 1:
                    return item.Modelo;
                case 2:
                    return item.Local;
                case 3:
                    return item.HD;
                case 4:
                    return item.Memoria;
                case 5:
                    return item.Processador;
                case 6:
                    return item.Windows;
                default:
                    return null;
            }
        }
    }

    private static string GetFonte(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            case 3:
                return item.Watts.ToString();
            case 4:
                return item.OndeFunciona;
            case 5:
                return item.Conectores;
            default:
                return null;
        }
    }

    private static string GetGbic(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            case 3:
                return item.Desempenho.ToString();
            default:
                return null;
        }
    }

    private static string GetHD(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            case 3:
                return item.Interface;
            case 4:
                return item.Tamanho.ToString();
            case 5:
                return item.FormaDeArmazenamento;
            case 6:
                return item.CapacidadeEmGB.ToString();
            case 7:
                return item.RPM.ToString();
            case 8:
                return item.VelocidadeDeLeitura.ToString();
            case 9:
                return item.Enterprise;
            default:
                return null;
        }
    }

    private static string GetIdrac(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            case 3:
                return item.QuaisConexoes;
            case 4:
                return item.VelocidadeGBs.ToString();
            case 5:
                return item.EntradaSD;
            case 6:
                return item.ServidoresSuportados;
            default:
                return null;
        }
    }

    private static string GetMemoria(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            case 3:
                return item.Tipo;
            case 4:
                return item.CapacidadeEmGB.ToString();
            case 5:
                return item.VelocidadeMHz.ToString();
            case 6:
                return item.LowVoltage;
            case 7:
                return item.Rank;
            case 8:
                return item.DIMM;
            case 9:
                return item.TaxaDeTransmissao.ToString();
            case 10:
                return item.Simbolo;
            default:
                return null;
        }
    }

    private static string GetMonitor(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            case 3:
                return item.Polegadas.ToString();
            case 4:
                return item.QuaisConexoes;
            default:
                return null;
        }
    }

    private static string GetMouse(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            case 3:
            default:
                return null;
        }
    }

    private static string GetNoBreak(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            default:
                return null;
        }
    }

    private static string GetNotebook(ItemColumns item, int index)
    {
        if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.SnPro)
        {
            switch (index)
            {
                case 0:
                    return item.Status;
                case 1:
                    return item.Modelo;
                case 2:
                    return item.Local;
                case 3:
                    return item.HD;
                case 4:
                    return item.Memoria;
                case 5:
                    return item.EntradaRJ45;
                case 6:
                    return item.BateriaInclusa;
                case 7:
                    return item.AdaptadorAC;
                case 8:
                    return item.Windows;
                default:
                    return null;
            }
        }
        else
        {
            switch (index)
            {
                case 0:
                    return item.Status;
                case 1:
                    return item.Modelo;
                case 2:
                    return item.Local;
                case 3:
                    return item.HD;
                case 4:
                    return item.Memoria;
                case 5:
                    return item.Processador;
                case 6:
                    return item.Windows;
                default:
                    return null;
            }
        }
    }

    private static string GetOutros(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            case 3:
                return item.Categoria;
            default:
                return null;
        }
    }

    private static string GetPlacaControladora(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            case 3:
                return item.QuaisConexoes;
            case 4:
                return item.QuantidadeDePortas.ToString();
            case 5:
                return item.TipoDeRAID;
            case 6:
                return item.CapacidadeMaxHD;
            case 7:
                return item.AteQuantosHDs.ToString();
            case 8:
                return item.BateriaInclusa;
            case 9:
                return item.Barramento;
            default:
                return null;
        }
    }

    private static string GetPlacaDeCapturaDeVideo(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            case 3:
                return item.QuantidadeDePortas.ToString();
            default:
                return null;
        }
    }

    private static string GetPlacaDeRede(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            case 3:
                return item.Interface;
            case 4:
                return item.QuantidadeDePortas.ToString();
            case 5:
                return item.QuaisConexoes;
            case 6:
                return item.SuportaFibraOptica;
            case 7:
                return item.Desempenho;
            default:
                return null;
        }
    }

    private static string GetPlacaDeSom(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            case 3:
                return item.QuantosCanais.ToString();
            default:
                return null;
        }
    }

    private static string GetPlacaDeVideo(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            case 3:
                return item.QuantidadeDePortas.ToString();
            case 4:
                return item.QuaisConexoes;
            default:
                return null;
        }
    }

    private static string GetProcessador(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            case 3:
                return item.Soquete;
            case 4:
                return item.NucleosFisicos.ToString();
            case 5:
                return item.NucleosLogicos.ToString();
            default:
                return null;
        }
    }

    private static string GetRoteador(ItemColumns item, int index)
    {
        if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.SnPro)
        {
            switch (index)
            {
                case 0:
                    return item.Status;
                case 1:
                    return item.Modelo;
                case 2:
                    return item.Local;
                case 3:
                    return item.Wireless;
                case 4:
                    return item.QuantidadeDePortas.ToString();
                case 5:
                    return item.BandaMaxima.ToString();
                default:
                    return null;
            }
        }
        else
        {
            switch (index)
            {
                case 0:
                    return item.Status;
                case 1:
                    return item.Modelo;
                case 2:
                    return item.Local;
                case 3:
                    return item.Wireless;
                case 4:
                    return item.QuantidadeDePortas.ToString();
                default:
                    return null;
            }
        }
    }

    private static string GetServidor(ItemColumns item, int index)
    {
        if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.SnPro)
        {
            switch (index)
            {
                case 0:
                    return item.Status;
                case 1:
                    return item.Modelo;
                case 2:
                    return item.Local;
                case 3:
                    return item.ModeloPlacaMae;
                case 4:
                    return item.Fonte;
                case 5:
                    return item.Memoria;
                case 6:
                    return item.HD;
                case 7:
                    return item.PlacaDeVideo;
                case 8:
                    return item.PlacaDeRede;
                case 9:
                    return item.Processador;
                case 10:
                    return item.MemoriasSuportadas;
                case 11:
                    return item.QuantasMemorias.ToString();
                case 12:
                    return item.OrdemDasMemorias;
                case 13:
                    return item.CapacidadeRAMTotal.ToString();
                case 14:
                    return item.Soquete;
                case 15:
                    return item.PlacaControladora;
                case 16:
                    return item.AteQuantosHDs.ToString();
                case 17:
                    return item.TipoDeHD;
                case 18:
                    return item.TipoDeRAID;
                default:
                    return null;
            }
        }
        else
        {
            switch (index)
            {
                case 0:
                    return item.Status;
                case 1:
                    return item.Modelo;
                case 2:
                    return item.Local;
                case 3:
                    return item.HD;
                case 4:
                    return item.Memoria;
                case 5:
                    return item.Processador;
                case 6:
                    return item.Windows;
                default:
                    return null;
            }
        }
    }

    private static string GetStorageNas(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            case 3:
                return item.Tamanho.ToString();
            case 4:
                return item.TipoDeRAID;
            case 5:
                return item.TipoDeHD;
            case 6:
                return item.CapacidadeMaxHD;
            case 7:
                return item.AteQuantosHDs.ToString();
            default:
                return null;
        }
    }

    private static string GetSwitch(ItemColumns item, int index)
    {
        if (InternalDatabase.Instance.currentEstoque == CurrentEstoque.SnPro)
        {
            switch (index)
            {
                case 0:
                    return item.Status;
                case 1:
                    return item.Modelo;
                case 2:
                    return item.Local;
                case 3:
                    return item.QuaisConexoes;
                case 4:
                    return item.Desempenho;
                default:
                    return null;
            }
        }
        else
        {
            switch (index)
            {
                case 0:
                    return item.Status;
                case 1:
                    return item.Modelo;
                case 2:
                    return item.Local;
                case 3:
                    return item.QuaisConexoes;
                default:
                    return null;
            }
        }
    }

    private static string GetTeclado(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            default:
                return null;
        }
    }

    private static string GetUSB(ItemColumns item, int index)
    {
        switch (index)
        {
            case 0:
                return item.Status;
            case 1:
                return item.Modelo;
            case 2:
                return item.Local;
            default:
                return null;
        }
    }
}

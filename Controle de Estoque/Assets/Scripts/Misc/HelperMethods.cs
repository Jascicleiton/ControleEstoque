using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperMethods
{
    public static string GetCategoryString(int value)
    {
        switch (value)
        {
            case 0:
                return ConstStrings.HD;
            case 1:
                return ConstStrings.Memoria;
            case 2:
                return ConstStrings.PlacaDeRede;
            case 3:
                return ConstStrings.Idrac;
            case 4:
                return ConstStrings.PlacaControladora;
            case 5:
                return ConstStrings.Processador;
            case 6:
                return ConstStrings.Desktop;
            case 7:
                return ConstStrings.Fonte;
            case 8:
                return ConstStrings.Switch;
            case 9:
                return ConstStrings.Roteador;
            case 10:
                return ConstStrings.Carregador;
            case 11:
                return ConstStrings.AdaptadorAC;
            case 12:
                return ConstStrings.StorageNAS;
            case 13:
                return ConstStrings.Gbic;
            case 14:
                return ConstStrings.PlacaDeVideo;
            case 15:
                return ConstStrings.PlacaDeSom;
            case 16:
                return ConstStrings.PlacaDeCapturaDeVideo;
            case 17:
                return ConstStrings.Servidor;
            case 18:
                return ConstStrings.Notebook;
            case 19:
                return ConstStrings.Monitor;
            default:
                return ConstStrings.HD;
        }
    }
}

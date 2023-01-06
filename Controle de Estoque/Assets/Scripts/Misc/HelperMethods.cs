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
                return ConstStrings.AdaptadorAC;
            case 1:
                return ConstStrings.Carregador;
            case 2:
                return ConstStrings.Desktop;
            case 3:
                return ConstStrings.Fonte;
            case 4:
                return ConstStrings.Gbic;
            case 5:
                return ConstStrings.HD;
            case 6:
                return ConstStrings.Idrac;
            case 7:
                return ConstStrings.Memoria;
            case 8:
                return ConstStrings.Monitor;
            case 9:
                return ConstStrings.Notebook;
            case 10:
                return ConstStrings.PlacaControladora;
            case 11:
                return ConstStrings.PlacaDeCapturaDeVideo;
            case 12:
                return ConstStrings.PlacaDeRede;
            case 13:
                return ConstStrings.PlacaDeSom;
            case 14:
                return ConstStrings.PlacaDeVideo;
            case 15:
                return ConstStrings.Processador;
            case 16:
                return ConstStrings.Roteador;
            case 17:
                return ConstStrings.Servidor;
            case 18:
                return ConstStrings.StorageNAS;
            case 19:
                return ConstStrings.Switch;
            default:
                return "Adicionar nova categoria";
        }
    }
}

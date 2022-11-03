using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;

public class InternalDatabase : Singleton<InternalDatabase>, ISaveable
{
    public static Dictionary<string, Sheet> splitDatabase = new Dictionary<string, Sheet>();
    public Sheet fullDatabase = new Sheet();

    public void FillFullDatabase()
    {
        Sheet inventario = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.InventarioSnPro, out inventario);
        Sheet hd = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.HD, out hd);
        Sheet memoria = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Memoria, out memoria);
        Sheet PlacaDeRede = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeRede, out PlacaDeRede);
        Sheet Idrac = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Idrac, out Idrac);
        Sheet PlacaControladora = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaControladora, out PlacaControladora);
        Sheet Processador = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Processador, out Processador);
        Sheet Gabinete = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Gabinete, out Gabinete);
        Sheet Fonte = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Fonte, out Fonte);
        Sheet Switch = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Switch, out Switch);
        Sheet Roteador = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Roteador, out Roteador);
        Sheet Carregador = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Carregador, out Carregador);
        Sheet AdaptadorAC = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.AdaptadorAC, out AdaptadorAC);
        Sheet StorageNAS = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.StorageNAS, out StorageNAS);
        Sheet Gbic = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Gbic, out Gbic);
        Sheet PlacaDeVideo = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeVideo, out PlacaDeVideo);
        Sheet PlacaDeSom = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeSom, out PlacaDeSom);
        Sheet PlacaDeCapturaDeVideo = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeCapturaDeVideo, out PlacaDeCapturaDeVideo);

        // Get all itens from "Inventario SnPro into the full database
        foreach (SheetColumns item in inventario.itens)
        {
            fullDatabase.itens.Add(item);
        }

        // Get the values of the detail sheet based on the "modelo" of the item on Inventario SnPro
        foreach (SheetColumns item in fullDatabase.itens)
        {
            
        }
        
    }

    public object CaptureState()
    {
        return splitDatabase;
    }

    public void RestoreState(object state)
    {
        splitDatabase = (Dictionary<string, Sheet>)state;
    }
}

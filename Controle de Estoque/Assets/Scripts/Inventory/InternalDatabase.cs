using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;

public class InternalDatabase : Singleton<InternalDatabase>, ISaveable
{
    public static Dictionary<string, Sheet> splitDatabase = new Dictionary<string, Sheet>();
    public Sheet fullDatabase = new Sheet();
    public Sheet testingSheet = new Sheet();

    /// <summary>
    /// Get all Sheet classes saved on splitDatabase and join them into a single Sheet class
    /// </summary>
    public void FillFullDatabase()
    {
        Sheet inventario = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.InventarioSnPro, out inventario);
        Sheet hd = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.HD, out hd);
        Sheet memoria = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Memoria, out memoria);
        Sheet placaDeRede = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeRede, out placaDeRede);
        Sheet idrac = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Idrac, out idrac);
        Sheet placaControladora = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaControladora, out placaControladora);
        Sheet processador = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Processador, out processador);
        Sheet gabinete = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Desktop, out gabinete);
        Sheet fonte = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Fonte, out fonte);
        Sheet Switch = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Switch, out Switch);
        Sheet roteador = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Roteador, out roteador);
        Sheet carregador = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Carregador, out carregador);
        Sheet adaptadorAC = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.AdaptadorAC, out adaptadorAC);
        Sheet storageNAS = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.StorageNAS, out storageNAS);
        Sheet gbic = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.Gbic, out gbic);
        Sheet placaDeVideo = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeVideo, out placaDeVideo);
        Sheet placaDeSom = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeSom, out placaDeSom);
        Sheet placaDeCapturaDeVideo = new Sheet();
        splitDatabase.TryGetValue(ConstStrings.PlacaDeCapturaDeVideo, out placaDeCapturaDeVideo);

        // Get all itens from "Inventario SnPro into the full database
        if (inventario != null && inventario.itens.Count > 0)
        {
            foreach (SheetColumns item in inventario.itens)
            {
                fullDatabase.itens.Add(item);
            }
        }

        // Get the values of the detail sheet based on the "modelo" of the item on Inventario SnPro
        foreach (SheetColumns item in fullDatabase.itens)
        {
            if (item.Categoria.Trim() == ConstStrings.HD.Trim())
            {
                if (hd != null && hd.itens.Count > 0)
                {
                    foreach (SheetColumns hdItem in hd.itens)
                    {
                        
                        if (item.Modelo.Trim().Equals(hdItem.Modelo.Trim()))
                        {
                            item.Interface = hdItem.Interface;
                            item.Tamanho = hdItem.Tamanho;
                            item.FormaDeArmazenamento = hdItem.FormaDeArmazenamento;
                            item.CapacidadeEmGB = hdItem.FormaDeArmazenamento;
                            item.RPM = hdItem.RPM;
                            item.VelocidadeDeLeitura = hdItem.VelocidadeDeLeitura;
                            item.Enterprise = hdItem.Enterprise;
                           Debug.Log("Found");
                            //Debug.LogWarning(fullDatabase.itens.IndexOf(item));
                        }
                        //else
                        //{
                        //    Debug.Log(item.Modelo.Length);
                        //    Debug.LogWarning(fullDatabase.itens.IndexOf(item));
                        //    Debug.Log(hdItem.Modelo.Length);
                        //    Debug.LogWarning(hd.itens.IndexOf(hdItem));
                        //}
                    }
                }
                else
                {
                    Debug.LogWarning("hd não encontrado");
                }
            }
        
            //else if(item.Categoria == ConstStrings.Memoria)
            //{

            //}
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

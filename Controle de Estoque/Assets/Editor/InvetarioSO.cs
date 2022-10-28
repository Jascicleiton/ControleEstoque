using UnityEngine;
using UnityEditor;
using System.IO;

public class InvetarioSO
{
    private static string inventarioCSVPath = "/Sheets/Inventario_Sysnetpro.csv";

    [MenuItem("Utilitis/Generate CSV")]
    public static void GenerateInventarioCSV()
    {
        string[] allLines = File.ReadAllLines(Application.dataPath + inventarioCSVPath);

        InventarioList inventario = ScriptableObject.CreateInstance<InventarioList>();
        for (int i = 0; i < allLines.Length; i++)
        {
            string[] splitData = allLines[i].Split(',');
            inventario.item[i].Entrada = splitData[0];
            inventario.item[i].Patrimônio = int.Parse(splitData[1]);
            inventario.item[i].Status = splitData[2];
            inventario.item[i].Serial = splitData[3];
            inventario.item[i].Categoria = splitData[4];
            inventario.item[i].Fabricante = splitData[5];
            inventario.item[i].Modelo = splitData[6];
            inventario.item[i].Local = splitData[7];
            inventario.item[i].Saída = splitData[8];
            inventario.item[i].Observação = splitData[9];
        }
        AssetDatabase.CreateAsset(inventario, $"Assets/{inventario}.asset");
        AssetDatabase.SaveAssets();

    }
}

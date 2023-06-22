using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Inventory.Item
{
    public class Desktop : ItemParent
    {
        public string ModeloPlacaMae = "";
        public string Processador = "";
        public string Fonte = "";
        public string HD = "";
        public string Memoria = "";        
        public string PlacaDeVideo = "";
        public string PlacaDeRede = "";
        public string LeitorDeDVD = "";

        protected override void Awake()
        {
            base.Awake();
            allValues.Add(ModeloPlacaMae);
            allValues.Add(Processador);
            allValues.Add(Fonte);
            allValues.Add(HD);
            allValues.Add(Memoria);
            allValues.Add(PlacaDeVideo);
            allValues.Add(PlacaDeRede);
            allValues.Add(LeitorDeDVD);
        }
    }
}
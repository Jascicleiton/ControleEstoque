using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inventory.Item
{
    public class FonteDeAlimentacao : ItemParent
    {
        public string OndeFunciona = "";
        public float Voltagem;
        public float Amperagem;

        protected override void Awake()
        {
            base.Awake();
            allValues.Add(OndeFunciona);
            allValues.Add(Voltagem.ToString());
            allValues.Add(Amperagem.ToString());
        }
    }
}
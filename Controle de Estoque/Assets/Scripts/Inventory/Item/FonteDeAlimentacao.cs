using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inventory.Item
{
    public class FonteDeAlimentacao : ItemParent
    {
        public string OndeFunciona = "";
        public float Voltagem_F;
        public float Amperagem_F;

        protected override void Awake()
        {
            base.Awake();
            allValues.Add(OndeFunciona);
            allValues.Add(Voltagem_F.ToString());
            allValues.Add(Amperagem_F.ToString());
        }
    }
}
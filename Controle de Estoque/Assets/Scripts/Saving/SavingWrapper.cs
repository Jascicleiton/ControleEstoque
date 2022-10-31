using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;

namespace Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        private SavingSystem saving;
        const string defaultSaveFile = "save";
        // Update is called once per frame
        private void Start()
        {
            saving = GetComponent<SavingSystem>();
        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                Load();
            }
        }

        public void Save()
        {
            if (saving == null)
            {
                saving = GetComponent<SavingSystem>();
            }
            saving.Save(defaultSaveFile);
        }

        public void Load()
        {
            if (saving == null)
            {
                saving = GetComponent<SavingSystem>();
            }
            saving.Load(defaultSaveFile);
        }
    }
}

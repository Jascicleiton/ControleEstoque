using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;
using System;

namespace Saving
{
    public class SavingWrapper : Singleton<SavingWrapper>
    {
        private JsonSavingSystem saving;
        const string defaultSaveFile = "save";
        // Update is called once per frame
        private void Start()
        {
            saving = GetComponent<JsonSavingSystem>();
            DontDestroyOnLoad(this.gameObject);
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F12))
            {
                Save("BKP ");
            }
            if(Input.GetKeyDown(KeyCode.F11))
            {
                Load("BKP ");
            }
        }

        public void Save(string saveFileName)
        {
            if (saving == null)
            {
                saving = GetComponent<JsonSavingSystem>();
            }
            saving.Save(saveFileName);
        }

        public void Load(string saveFileName)
        {
            if (saving == null)
            {
                saving = GetComponent<JsonSavingSystem>();
            }
            saving.Load(saveFileName);
        }
    }
}

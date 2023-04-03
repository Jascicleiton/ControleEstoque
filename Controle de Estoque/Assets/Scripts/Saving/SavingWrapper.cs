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
        const string defaultSaveFile = "BKP - ";

        // Update is called once per frame
        private void Start()
        {
            saving = GetComponent<JsonSavingSystem>();
            if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                DontDestroyOnLoad(this.gameObject);           
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void OnEnable()
        {
            EventHandler.DatabaseUpdatedEvent += Save;
            EventHandler.DisconectedFromInternet += Load;
        }

        private void OnDisable()
        {
            EventHandler.DatabaseUpdatedEvent -= Save;
            EventHandler.DisconectedFromInternet -= Load;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F10))
            {
                Save();
            }
        }

        public void Save()
        {
            if (saving == null)
            {
                saving = GetComponent<JsonSavingSystem>();
            }
            saving.Save(defaultSaveFile + InternalDatabase.Instance.currentEstoque);
        }

        public void Load()
        {
            Debug.Log("Event called");
            if (saving == null)
            {
                saving = GetComponent<JsonSavingSystem>();
            }
            saving.Load(defaultSaveFile + InternalDatabase.Instance.currentEstoque);
        }
    }
}

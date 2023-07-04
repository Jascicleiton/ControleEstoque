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
        private int bkpIndex = 1;
        [SerializeField] private int savesCounter = 0;

  
        // Update is called once per frame
        private void Start()
        {
            saving = GetComponent<JsonSavingSystem>();
            if (!InternalDatabase.Instance.isOfflineProgram)
            {
                Destroy(this.gameObject);
            }
            if (InternalDatabase.Instance.isOfflineProgram == true && PlayerPrefs.HasKey(ConstStrings.SavingCounter))
            {
                savesCounter = PlayerPrefs.GetInt(ConstStrings.SavingCounter);
            }
            if (InternalDatabase.Instance.isOfflineProgram == true && PlayerPrefs.HasKey(ConstStrings.BkpIndex))
            {
                bkpIndex = PlayerPrefs.GetInt(ConstStrings.BkpIndex);
            }
        }

        private void OnEnable()
        {
            EventHandler.DatabaseUpdatedEvent += Save;
  //          EventHandler.DisconectedFromInternet += Load;
        }

        private void OnDisable()
        {
            EventHandler.DatabaseUpdatedEvent -= Save;
    //        EventHandler.DisconectedFromInternet -= Load;
        }

        private void Update()
        {

            if(Input.GetKeyDown(KeyCode.F10))
            {
                savesCounter = 5;
                Save();
            }
        }

        public void Save()
        {
            savesCounter++;
            if (saving == null)
            {
                saving = GetComponent<JsonSavingSystem>();
            }
            saving.Save(defaultSaveFile + InternalDatabase.Instance.currentEstoque.ToString());
            if (savesCounter >= 5)
            {
                savesCounter = 0;
                saving.Save(defaultSaveFile + InternalDatabase.Instance.currentEstoque.ToString() + " - " + DateTime.Now.ToString("dd-MM-yy") + $" - {bkpIndex}");
                bkpIndex++;
            }
            
            PlayerPrefs.SetInt(ConstStrings.SavingCounter, savesCounter);
            PlayerPrefs.SetInt(ConstStrings.BkpIndex, bkpIndex);
            PlayerPrefs.Save();
        }

        public void Load()
        {
            //Debug.Log("Event called");
            if (saving == null)
            {
                saving = GetComponent<JsonSavingSystem>();
            }
            saving.Load(defaultSaveFile + InternalDatabase.Instance.currentEstoque.ToString());
        }
    }
}

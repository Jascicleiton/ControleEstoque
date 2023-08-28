using UnityEngine;
using System;
using Assets.Scripts.Inventory.Database;
using Assets.Scripts.Misc;

namespace Assets.Scripts.Saving
{
    public class SavingWrapper : Singleton<SavingWrapper>
    {
        private JsonSavingSystem _saving;
        const string defaultSaveFile = "BKP - ";
        private int _bkpIndex = 1;
        [SerializeField] private int _savesCounter = 0;


        // Update is called once per frame
        private void Start()
        {
            _saving = GetComponent<JsonSavingSystem>();
            if (!InternalDatabase.Instance.isOfflineProgram)
            {
                Destroy(gameObject);
            }
            if (InternalDatabase.Instance.isOfflineProgram == true && PlayerPrefs.HasKey(ConstStrings.SavingCounter))
            {
                _savesCounter = PlayerPrefs.GetInt(ConstStrings.SavingCounter);
            }
            if (InternalDatabase.Instance.isOfflineProgram == true && PlayerPrefs.HasKey(ConstStrings.BkpIndex))
            {
                _bkpIndex = PlayerPrefs.GetInt(ConstStrings.BkpIndex);
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

            if (Input.GetKeyDown(KeyCode.F10))
            {
                _savesCounter = 5;
                Save();
            }
        }

        public void Save()
        {
            _savesCounter++;
            if (_saving == null)
            {
                _saving = GetComponent<JsonSavingSystem>();
            }
            _saving.Save(defaultSaveFile + InternalDatabase.Instance.currentEstoque.ToString());
            if (_savesCounter >= 5)
            {
                _savesCounter = 0;
                _saving.Save(defaultSaveFile + InternalDatabase.Instance.currentEstoque.ToString() + " - " + DateTime.Now.ToString("dd-MM-yy") + $" - {_bkpIndex}");
                _bkpIndex++;
            }

            PlayerPrefs.SetInt(ConstStrings.SavingCounter, _savesCounter);
            PlayerPrefs.SetInt(ConstStrings.BkpIndex, _bkpIndex);
            PlayerPrefs.Save();
        }

        public void Load()
        {
            //Debug.Log("Event called");
            if (_saving == null)
            {
                _saving = GetComponent<JsonSavingSystem>();
            }
            _saving.Load(defaultSaveFile + InternalDatabase.Instance.currentEstoque.ToString());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;

namespace Saving
{
    public class SavingWrapper : Singleton<SavingWrapper>
    {
        private SavingSystem saving;
        const string defaultSaveFile = "save";
        // Update is called once per frame
        private void Start()
        {
            saving = GetComponent<SavingSystem>();
            DontDestroyOnLoad(this.gameObject);
            StartCoroutine(LoadUserDatabaseRoutine());
        }

        /// <summary>
        /// for testing purposes
        /// </summary>
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F5))
            {
                Save(ConstStrings.DataDatabaseSaveFile);
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                Load(ConstStrings.DataDatabaseSaveFile);
            }
        }

        /// <summary>
        /// This coroutine waits two seconds to make sure everything is active on the scene
        /// before loading the UserDatabase
        /// </summary>
        private IEnumerator LoadUserDatabaseRoutine()
        {
            yield return new WaitForSeconds(2);
            Load(ConstStrings.UserDatabaseSaveFile);
        }

        public void Save(string saveFileName)
        {
            if (saving == null)
            {
                saving = GetComponent<SavingSystem>();
            }
            saving.Save(saveFileName);
        }

        public void Load(string saveFileName)
        {
            if (saving == null)
            {
                saving = GetComponent<SavingSystem>();
            }
            saving.Load(saveFileName);
        }
    }
}

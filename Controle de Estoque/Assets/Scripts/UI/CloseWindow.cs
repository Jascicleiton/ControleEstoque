using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    /// <summary>
    /// Used to close a pop up window using Esc
    /// </summary>
    public class CloseWindow : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EventHandler.CallWindowClosed();
                gameObject.SetActive(false);
            }
        }
    }
}
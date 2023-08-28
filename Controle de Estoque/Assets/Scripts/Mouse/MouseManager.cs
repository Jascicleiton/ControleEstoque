using UnityEngine;

namespace Assets.Scripts.Mouse
{
    public class MouseManager : Singleton<MouseManager>
    {
        [SerializeField] private Texture2D _defaultCursor = null;
        [SerializeField] private Texture2D _waitingCursor = null;

        // Start is called before the first frame update
        void Start()
        {
            //  DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Set the mouse cursor to it's default sprite
        /// </summary>
        public void SetDefaultCursor()
        {
            Cursor.SetCursor(_defaultCursor, Vector2.zero, CursorMode.Auto);
        }

        /// <summary>
        /// Set the mouse cursor to it's waiting sprite
        /// </summary>
        public void SetWaitingCursor()
        {
            // Debug.Log(callingObject.name + " Called");
            Cursor.SetCursor(_waitingCursor, Vector2.zero, CursorMode.Auto);
        }

    }
}
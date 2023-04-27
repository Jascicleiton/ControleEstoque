using UnityEngine;

public class MouseManager : Singleton<MouseManager>
{
    [SerializeField] private Texture2D defaultCursor = null;
    [SerializeField] private Texture2D waitingCursor = null;

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
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    /// <summary>
    /// Set the mouse cursor to it's waiting sprite
    /// </summary>
    public void SetWaitingCursor()
    {
        // Debug.Log(callingObject.name + " Called");
        Cursor.SetCursor(waitingCursor, Vector2.zero, CursorMode.Auto);
    }

}

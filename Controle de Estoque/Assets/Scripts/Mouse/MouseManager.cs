using UnityEngine;

public class MouseManager : Singleton<MouseManager>
{
    [SerializeField] private Texture2D defaultCursor = null;
    [SerializeField] private Texture2D waitingCursor = null;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetDefaultCursor()
    {
     //   Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetWaitingCursor()
    {
      //  Cursor.SetCursor(waitingCursor, Vector2.zero, CursorMode.Auto);
    }
   
}

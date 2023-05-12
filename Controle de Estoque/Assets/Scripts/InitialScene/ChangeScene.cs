using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private Scenes sceneToLoad = Scenes.InitialScene;

    /// <summary>
    /// Change to the appropriate scene according to the scenes variable. This variable is set on the inspector
    /// </summary>
    public void ButtonClicked()
    {

        switch (sceneToLoad)
        {
            case Scenes.MainMenu:
                UsersManager.Instance.currentUser = new User("pessoa", "");
                ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, sceneToLoad);
                break;
            case Scenes.ConsultScene:
                ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, sceneToLoad);
                break;
            case Scenes.AddItemScene:
                ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, sceneToLoad);
                break;
           case Scenes.MovementScene:
                ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, sceneToLoad);
                break;
          case Scenes.UpdateItemScene:
                ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, sceneToLoad);
                break;
            case Scenes.ExportTablesScene:
                ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, sceneToLoad);
                break;
            case Scenes.RecoverBKP:
                ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, sceneToLoad);
                break;
            case Scenes.NoPaNoSeScene:
                ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, sceneToLoad);
                break;
            case Scenes.MovementRecordsScene:
                ChangeScreenManager.Instance.OpenScene(Scenes.InitialScene, sceneToLoad);
                break;
            default:
                break;
        }
    }
}

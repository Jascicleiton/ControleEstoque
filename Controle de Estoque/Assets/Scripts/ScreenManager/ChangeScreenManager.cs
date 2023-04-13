using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScreenManager : MonoBehaviour
{
    [SerializeField] private Scenes scenes = Scenes.SceneInitial;

    /// <summary>
    /// Change to the appropriate scene according to the scenes variable. This variable is set on the inspector
    /// </summary>
    public void ButtonClicked()
    {
        switch (scenes)
        {
            case Scenes.SceneMainMenu:
                UsersManager.Instance.currentUser = new User("pessoa", "");
                SceneManager.LoadScene(ConstStrings.SceneMainMenu);
                break;
            case Scenes.SceneConsult:
                SceneManager.LoadScene(ConstStrings.SceneConsult);
                break;
            case Scenes.SceneAddItem:
                SceneManager.LoadScene(ConstStrings.SceneAddItem);
                break;
            case Scenes.SceneInitial:
                SceneManager.LoadScene(ConstStrings.SceneInitial);
                break;
            case Scenes.SceneMovement:
                SceneManager.LoadScene(ConstStrings.SceneMovement);
                break;
            case Scenes.SceneSplash:
                SceneManager.LoadScene(ConstStrings.SceneSplash);
                break;
            case Scenes.SceneUpdateItem:
                SceneManager.LoadScene(ConstStrings.SceneUpdateItem);
                break;
            case Scenes.ExportTablesScene:
                SceneManager.LoadScene(ConstStrings.SceneExportTables);
                break;
            case Scenes.SceneConsultInventoryAll:
                SceneManager.LoadScene(ConstStrings.SceneConsultInventoryAll);
                break;
            case Scenes.SceneConsultDetailsAll:
                SceneManager.LoadScene(ConstStrings.SceneConsultDetailsAll);
                break;
            case Scenes.SceneNoPaNoSe:
                SceneManager.LoadScene(ConstStrings.SceneNoPaNoSe);
                break;
            case Scenes.SceneShowAllMovements:
                SceneManager.LoadScene(ConstStrings.SceneShowAllMovements);
                break;
            default:
                break;
        }
    }
    
}

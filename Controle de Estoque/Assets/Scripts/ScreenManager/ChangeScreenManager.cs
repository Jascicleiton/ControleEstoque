using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScreenManager : MonoBehaviour
{
    [SerializeField] private Scenes scenes = Scenes.SceneInitial;

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
            case Scenes.SceneAddRemoveItem:
                SceneManager.LoadScene(ConstStrings.SceneAddRemoveItem);
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
            case Scenes.SceneExportSchets:
                SceneManager.LoadScene(ConstStrings.SceneExportSheets);
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
            default:
                break;
        }
    }
    
}

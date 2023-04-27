using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScreenManager : Singleton<ChangeScreenManager>
{
    /// <summary>
    /// Open a specific scene
    /// </summary>
        public void OpenScene(Scenes sceneToUnload, Scenes sceneToOpen)
    {
        StartCoroutine(OpenSceneRoutine(sceneToUnload, sceneToOpen));
    }   

    private IEnumerator OpenSceneRoutine(Scenes sceneToUnload, Scenes sceneToOpen)
    {
       yield return SceneManager.UnloadSceneAsync(sceneToUnload.ToString());

       yield return SceneManager.LoadSceneAsync(sceneToOpen.ToString(), LoadSceneMode.Additive);

        // Find the scene that was most recently loaded (the one at the last index of the loaded scenes).
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        // Set the newly loaded scene as the active scene (this marks it as the one to ben unloaded next).
        SceneManager.SetActiveScene(newlyLoadedScene);
    }
}

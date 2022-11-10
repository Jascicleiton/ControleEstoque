using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{ 
    private IEnumerator Start()
    {
        // Start the first scene loading and wait for it to finish.
        yield return StartCoroutine(LoadSceneAndSetActive("SplashScreen"));        
    }

    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        // Allow the given scene to load over several frames and add it to the already loaded scenes (just the persistent scene at this point).
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // Find the scene that was most recently loaded (the one at the last index of the loaded scenes).
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);

        // Set the newly loaded scene as the active scene (this marks it as the one to ben unloaded next).
        SceneManager.SetActiveScene(newlyLoadedScene);
    }

    // Unload the current active scene
    private IEnumerator UnloadScene()
    {
        yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(UnloadScene());
        StartCoroutine(LoadSceneAndSetActive(sceneName));
    }




}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] GameObject background;
    [SerializeField] Image logo;
    [SerializeField] TMP_Text text;

    [SerializeField] Sprite snpLogo;

    private CanvasGroup backgroundImage;
    private float fadeDuration = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        backgroundImage = background.GetComponent<CanvasGroup>();
        StartCoroutine(Fade());
    }

    /// <summary>
    /// Fade in and out the screen
    /// </summary>
    private IEnumerator Fade()
    {
                
        // Make sure the CanvasGroup blocks raycasts into the scene so no more input can be accepted
        backgroundImage.blocksRaycasts = true;

        //// Calculate hwo fast the CanvasGroup should fade based on it´s current alpha, it's final alpha and how long it has to change between the two
        //float fadeSpeed = Mathf.Abs(backgroundImage.alpha - 1) / fadeDuration;

        //// while the CanvasGroup hasn't reached the final alpha...
        //while (!Mathf.Approximately(backgroundImage.alpha, 1))
        //{
        //    // ... move the alpha towards it's target alpha
        //    backgroundImage.alpha = Mathf.MoveTowards(backgroundImage.alpha, 1, fadeSpeed * Time.deltaTime);

        //    // wait for a frame, then continue
        //    yield return null;
        //}

        //// Stop the CanvasGroup from blocking raycasts so input is no longer ignored
        //backgroundImage.blocksRaycasts = false;

        //yield return new WaitForSeconds(4f);
        //// Make sure the CanvasGroup blocks raycasts into the scene so no more input can be accepted
        //backgroundImage.blocksRaycasts = true;

        //fadeSpeed = Mathf.Abs(backgroundImage.alpha - 0) / fadeDuration;

        //// while the CanvasGroup hasn't reached the final alpha...
        //while (!Mathf.Approximately(backgroundImage.alpha, 0))
        //{
        //    // ... move the alpha towards it's target alpha
        //    backgroundImage.alpha = Mathf.MoveTowards(backgroundImage.alpha, 0, fadeSpeed * Time.deltaTime);

        //    // wait for a frame, then continue
        //    yield return null;
        //}

        logo.sprite = snpLogo;
        text.text = "";

        // Calculate hwo fast the CanvasGroup should fade based on it´s current alpha, it's final alpha and how long it has to change between the two
        float fadeSpeed = Mathf.Abs(backgroundImage.alpha - 1) / fadeDuration;

        // while the CanvasGroup hasn't reached the final alpha...
        while (!Mathf.Approximately(backgroundImage.alpha, 1))
        {
            // ... move the alpha towards it's target alpha
            backgroundImage.alpha = Mathf.MoveTowards(backgroundImage.alpha, 1, fadeSpeed * Time.deltaTime);

            // wait for a frame, then continue
            yield return null;
        }

        // Stop the CanvasGroup from blocking raycasts so input is no longer ignored
        backgroundImage.blocksRaycasts = false;

        yield return new WaitForSeconds(4f);
        // Make sure the CanvasGroup blocks raycasts into the scene so no more input can be accepted
        backgroundImage.blocksRaycasts = true;

        fadeSpeed = Mathf.Abs(backgroundImage.alpha - 0) / fadeDuration;

        // while the CanvasGroup hasn't reached the final alpha...
        while (!Mathf.Approximately(backgroundImage.alpha, 0))
        {
            // ... move the alpha towards it's target alpha
            backgroundImage.alpha = Mathf.MoveTowards(backgroundImage.alpha, 0, fadeSpeed * Time.deltaTime);

            // wait for a frame, then continue
            yield return null;
        }

        // Stop the CanvasGroup from blocking raycasts so input is no longer ignored
        backgroundImage.blocksRaycasts = false;
              

        SceneManager.LoadScene(ConstStrings.SceneMainMenu);
    }
}

using Assets.Scripts.ScreenManager;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.SplashScreen
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField] GameObject _background;
        [SerializeField] Image _logo;

        [SerializeField] Sprite _snpLogo;

        private CanvasGroup _backgroundImage;
        private const float _fadeDuration = 2f;

        // Start is called before the first frame update
        void Start()
        {
            _backgroundImage = _background.GetComponent<CanvasGroup>();
            StartCoroutine(Fade());
        }

        /// <summary>
        /// Fade in and out the screen
        /// </summary>
        private IEnumerator Fade()
        {

            // Make sure the CanvasGroup blocks raycasts into the scene so no more input can be accepted
            _backgroundImage.blocksRaycasts = true;

            _logo.sprite = _snpLogo;
            
            // Calculate hwo fast the CanvasGroup should fade based on it´s current alpha, it's final alpha and how long it has to change between the two
            float fadeSpeed = Mathf.Abs(_backgroundImage.alpha - 1) / _fadeDuration;

            // while the CanvasGroup hasn't reached the final alpha...
            while (!Mathf.Approximately(_backgroundImage.alpha, 1))
            {
                // ... move the alpha towards it's target alpha
                _backgroundImage.alpha = Mathf.MoveTowards(_backgroundImage.alpha, 1, fadeSpeed * Time.deltaTime);

                // wait for a frame, then continue
                yield return null;
            }

            // Stop the CanvasGroup from blocking raycasts so input is no longer ignored
            _backgroundImage.blocksRaycasts = false;

            yield return new WaitForSeconds(4f);
            // Make sure the CanvasGroup blocks raycasts into the scene so no more input can be accepted
            _backgroundImage.blocksRaycasts = true;

            fadeSpeed = Mathf.Abs(_backgroundImage.alpha - 0) / _fadeDuration;

            // while the CanvasGroup hasn't reached the final alpha...
            while (!Mathf.Approximately(_backgroundImage.alpha, 0))
            {
                // ... move the alpha towards it's target alpha
                _backgroundImage.alpha = Mathf.MoveTowards(_backgroundImage.alpha, 0, fadeSpeed * Time.deltaTime);

                // wait for a frame, then continue
                yield return null;
            }

            // Stop the CanvasGroup from blocking raycasts so input is no longer ignored
            _backgroundImage.blocksRaycasts = false;


            ChangeScreenManager.Instance.OpenScene(Scenes.SplashScreen, Scenes.MainMenu);
            //ChangeScreenManager.Instance.OpenScene(Scenes.SplashScreen, Scenes.TestScene);
        }
    }
}
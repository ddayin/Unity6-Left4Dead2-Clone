using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace L4D2Clone
{
    public class LoadingScreen : MonoBehaviour
    {
        [Header("UI Elements")]
        public Slider progressBar;
        public Text loadingText;
        public Text percentageText;

        [Header("Settings")]
        public string sceneToLoad;
        public float minimumLoadTime = 2f; // Minimum time to show loading screen

        private void Start()
        {
            StartCoroutine(LoadSceneAsync());
        }

        private IEnumerator LoadSceneAsync()
        {
            float startTime = Time.time;

            // Start loading the scene asynchronously
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
            operation.allowSceneActivation = false; // Prevent automatic scene activation

            // Update progress bar while loading
            while (!operation.isDone)
            {
                // Unity's progress goes from 0 to 0.9, then jumps to 1 when ready
                float progress = Mathf.Clamp01(operation.progress / 0.9f);

                // Update UI elements
                if (progressBar != null)
                    progressBar.value = progress;

                if (percentageText != null)
                    percentageText.text = Mathf.Round(progress * 100f) + "%";

                if (loadingText != null)
                    loadingText.text = "Loading" + GetLoadingDots();

                // Check if loading is complete and minimum time has passed
                if (operation.progress >= 0.9f && Time.time - startTime >= minimumLoadTime)
                {
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }
        }

        private string GetLoadingDots()
        {
            int dotCount = Mathf.FloorToInt(Time.time * 2f) % 4;
            return new string('.', dotCount);
        }
    }
}
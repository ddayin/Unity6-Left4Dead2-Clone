using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Framework;

namespace L4D2Clone
{
    public class LoadingScreen : MonoBehaviour
    {
        public Slider progressBar;
        public float minimumLoadTime = 2f;

        private void Start()
        {
            StartCoroutine(LoadSceneAsync());
        }

        private IEnumerator LoadSceneAsync()
        {
            float startTime = Time.time;

            AsyncOperation operation = SceneController.Instance.LoadAsnc(SceneName.Prototype);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                // Unity's progress goes from 0 to 0.9, then jumps to 1 when ready
                float progress = Mathf.Clamp01(operation.progress / 0.9f);

                if (progressBar != null)
                    progressBar.value = progress;

                if (operation.progress >= 0.9f && Time.time - startTime >= minimumLoadTime)
                {
                    operation.allowSceneActivation = true;
                }

                yield return null;
            }
        }
    }
}
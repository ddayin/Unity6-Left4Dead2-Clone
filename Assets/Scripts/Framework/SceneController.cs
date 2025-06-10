using UnityEngine;
using UnityEngine.SceneManagement;

namespace Framework
{
    public enum SceneName
    {
        Title = 0,
        Loading,
        Prototype,
    }

    public class SceneController : Singleton<SceneController>
    {
        public SceneName CurrentScene { get; private set; }

        protected override void Awake()
        {
            base.Awake();
        }

        public void Load(SceneName _name)
        {
            CurrentScene = _name;
            SceneManager.LoadScene(_name.ToString());
        }

        public AsyncOperation LoadAsnc(SceneName _name)
        {
            CurrentScene = _name;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(_name.ToString(), LoadSceneMode.Single);
            return asyncOperation;
        }
    }
}
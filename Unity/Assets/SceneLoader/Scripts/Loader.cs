using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoader
{
	public class Loader : MonoBehaviour
	{
        public static Loader Instance;
        private void Awake()
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Load scene from streaming assets.
        /// </summary>
        /// <param name="sceneName"></param>
        public static void LoadScene (string sceneName)
        {
            Instance.StartCoroutine(Instance.Load(sceneName));
        }

        protected IEnumerator Load(string sceneName)
        {
            Debug.Log("Loading scene...");

            UI.Components.Instance.Loading.Set (true);

            AssetBundleCreateRequest bundle = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/scenes/" + sceneName);

            yield return bundle.isDone;

            string [] scenes = bundle.assetBundle.GetAllScenePaths();

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scenes[0], LoadSceneMode.Single);

            asyncOperation.allowSceneActivation = true;

            yield return asyncOperation.isDone;

            UI.Components.Instance.Loading.Set(false);

            bundle.assetBundle.Unload(false);
        }
    }
}


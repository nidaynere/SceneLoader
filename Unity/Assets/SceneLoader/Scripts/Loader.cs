using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.VR;

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

        AssetBundleCreateRequest assetBundleAsync;
        AsyncOperation sceneLoadAsync;
        protected IEnumerator Load(string sceneName)
        {
            UI.Components.Instance.LoadingFill.SetFill(0);

            Debug.Log("Loading scene...");

            UI.Components.Instance.Loading.Set (true);

            yield return new WaitForSeconds (1/ (UI.Components.Instance.Loading.TransitionSpeed));

            assetBundleAsync = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/scenes/" + sceneName);

            while (!assetBundleAsync.isDone)
            {
                UI.Components.Instance.LoadingFill.fillAmount = assetBundleAsync.progress / 2f;
                yield return null;
            }

            string [] scenes = assetBundleAsync.assetBundle.GetAllScenePaths();

            Application.backgroundLoadingPriority = ThreadPriority.High;

            sceneLoadAsync = SceneManager.LoadSceneAsync(scenes[0], LoadSceneMode.Single);
            //sceneLoadAsync.allowSceneActivation = true;

            while (!sceneLoadAsync.isDone)
            {
                UI.Components.Instance.LoadingFill.fillAmount = 0.5f + sceneLoadAsync.progress / 2f;
                yield return null;
            }

            UI.Components.Instance.Loading.Set(false);

            assetBundleAsync.assetBundle.Unload(false);
        }
    }
}


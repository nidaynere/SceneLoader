using UnityEngine;
using System.IO;
using UnityEngine.UI;

namespace SceneLoader.UI
{
    /// <summary>
    /// Draw all scenes to UI and interactable.
    /// </summary>
    public class SceneSelector : MonoBehaviour
    {
        private void Start()
        {
            ReadScenes();
        }

        /// <summary>
        /// Read scenes on Streaming assets.
        /// </summary>
        public static void ReadScenes()
        {
            string [] scenes = Directory.GetFiles(Application.streamingAssetsPath + "/scenes", "*.unity3d");

            Debug.Log("Found scenes: " + scenes.Length);

            Transform _t;
            string _sceneName;
            foreach (string scene in scenes)
            {
                _sceneName = Path.GetFileName(scene);
                _t = Instantiate(Components.Instance.SceneSelector.transform.GetChild(0), Components.Instance.SceneSelector.transform);
                _t.gameObject.SetActive(true);
                _t.GetComponentInChildren<Text>().text = _sceneName;
                string _scene = scene;
                _t.GetComponent<Button>().onClick.AddListener (
                    () => 
                        {
                        //Components.Instance.SceneSelector.Set(false); Scene selector is always be active. to test jumping between scenes.
                        Loader.LoadScene(_sceneName);
                        }
                    );
            }
        }
    }
}

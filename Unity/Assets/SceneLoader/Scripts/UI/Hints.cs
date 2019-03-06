using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace SceneLoader.UI
{
    [System.Serializable]
    public class Hints : MonoBehaviour
    {
        Utilities.RandomString randomString;

        // Use this for initialization
        void Awake ()
        {
            randomString = JsonUtility.FromJson<Utilities.RandomString>(File.ReadAllText (Application.streamingAssetsPath + "/hints.json"));
        }

        public Text HintText;
        public void SetHint()
        {
            HintText.text = randomString.Values[Random.Range(0, randomString.Values.Length)];
        }
    }
}

